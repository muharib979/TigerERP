using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrustracture.ERP.UnitOfWorkSetup.ForBrand;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.BrandBLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.ProductBrand.Logic
{
    public class BrandBLLManager : IBrandBLLManager
    {
        private readonly IBrandUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandBLLManager(IBrandUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork =  unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddBrand(BrandViewModel model)
        {
            try
            {
                Brand brand;
                brand = await _unitOfWork.Brand.GetByIdAsync(model.Id);
                model.BrandName = model.BrandName.Trim();
                if (brand == null)
                {
                    brand = new Brand();
                    brand.CreatedBy = "Bappy";
                    brand.CreatedDate = DateTime.UtcNow;
                    brand.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _unitOfWork.Brand.AddAsync(brand);
                }

                if ( await _unitOfWork.Brand.IsExist(p => p.BrandName.ToLower() == model.BrandName.ToLower() && p.Id != model.Id && p.CompId==model.CompId) )
                    throw new DuplicateWaitObjectException("Name", model.BrandName);
                brand.UpdatedBy = "Bappy";
                brand.UpdatedDate = DateTime.UtcNow;

                brand = _mapper.Map((BrandViewModel)model, brand);
                var result=await _unitOfWork.SaveAsync();
                return result ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrandByComp(int compId)
        {
            IEnumerable<BrandViewModel> result = new BrandViewModel[] { };
            var models = await _unitOfWork.Brand.GetAsync(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active, string.Empty);
            result = _mapper.Map(models, result);
            return result;
        }


        public async Task<bool> DeleteBrand(int deleteId)
        {
            try
            {
                Brand brand;
                brand = await _unitOfWork.Brand.GetByIdAsync(deleteId);
                brand.UpdatedBy = "Bappy";
                brand.UpdatedDate = DateTime.UtcNow;
                brand.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
                var result = await _unitOfWork.SaveAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public async Task<BrandViewModel> GetBrandById(int Id)
        {
            try
            {
                BrandViewModel model=new BrandViewModel();
                Brand data = await _unitOfWork.Brand.GetByIdAsync(Id);
                model= _mapper.Map(data, model);
                return model;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
