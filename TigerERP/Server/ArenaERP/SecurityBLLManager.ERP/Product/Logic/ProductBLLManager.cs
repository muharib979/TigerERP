using AutoMapper;
using Common.ERP.UtilityManagement;
using Dapper;
using DatabaseContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.IProductBLL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.ProductBLLManage.Logic
{
    public class ProductBLLManager : IProductBLLManager
    {
        private readonly DatabaseContextDb _contextDb;
        private readonly IMapper _mapper;

        public ProductBLLManager(DatabaseContextDb contextDb,IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }

        public async Task<bool> AddProduct(ProductViewModel model)
        {
            try
            {
                Product data;
                int productId = 0;
                data = await _contextDb.Product.FirstOrDefaultAsync(p => p.Id == model.Id);
                model.Name = model.Name.Trim();
                await _contextDb.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Product();
                    productId = await _contextDb.Product.Where(u => u.CompId == model.CompId).MaxAsync(e => (int?)e.ProductId) ?? 0;
                    model.ProductId = productId + 1;
                    data.CreatedBy = "Bappy";
                    data.CreatedDate = DateTime.UtcNow;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _contextDb.Product.AddAsync(data);
                }
                if (_contextDb.Product.Any(p => p.Name.ToLower() == model.Name.ToLower() && p.ProductId != model.ProductId && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.Name);
                data.UpdatedBy = "Bappy";
                data.UpdatedDate = DateTime.UtcNow;
                data = _mapper.Map(model, data);
                var result = await _contextDb.SaveChangesAsync();
                await _contextDb.Database.CommitTransactionAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                await _contextDb.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<ProductViewModel>> GetAllProductByComp(int compId)
        {
            List<ProductViewModel> result;
            using (var con = new SqlConnection(Connection.ConnectionString()))
            {
                var param = new
                {
                    CompId = compId
                };
                result = con.Query<ProductViewModel>("GetAllActiveProductByComp", param:param, commandType:CommandType.StoredProcedure).ToList();
            }
            return result;

        }

        public async Task<bool> DeleteProduct(int deletedId)
        {
            try
            {
                await _contextDb.Database.BeginTransactionAsync();
                Product ? product;
                product = await _contextDb.Product.FirstOrDefaultAsync(p => p.Id == deletedId);
                product.UpdatedBy = "Bappy";
                product.UpdatedDate = DateTime.UtcNow;
                product.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
                var result = await _contextDb.SaveChangesAsync();
                await _contextDb.Database.CommitTransactionAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                await _contextDb.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }



        public async Task<ProductViewModel> GetProductById(int id)
        {
            try
            {
                ProductViewModel model=new ProductViewModel();
                var data = await _contextDb.Product.Where(p => p.Id == id).FirstOrDefaultAsync();
                var result =_mapper.Map(data, model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
