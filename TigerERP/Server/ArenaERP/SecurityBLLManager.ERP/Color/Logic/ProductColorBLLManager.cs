using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.ProductColor.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.ProductColorBLL.Logic
{
    public class ProductColorBLLManager : IProductColorBLLManager
    {
        private readonly DatabaseContextDb _contextDb;
        private readonly IMapper _mapper;

        public ProductColorBLLManager(DatabaseContextDb contextDb,IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }
        public async Task<bool> AddProductColor(ColorViewModel model)
        {
            try
            {
                Color data;
                data = await _contextDb.Color.FirstOrDefaultAsync(p=>p.Id==model.Id);
                model.ColorName = model.ColorName.Trim();
                await _contextDb.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Color();
                    data.CreatedBy = "Bappy";
                    data.CreatedDate = DateTime.UtcNow;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _contextDb.Color.AddAsync(data);
                }

                if (_contextDb.Color.Any(p => p.ColorName.ToLower() == model.ColorName.ToLower() && p.Id != model.Id && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.ColorName);
                data.UpdatedBy = "Bappy";
                data.UpdatedDate = DateTime.UtcNow;
                _mapper.Map(model, data);
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
        public async Task<IEnumerable<ColorViewModel>> GetAllColorByComp(int compId)
        {
            IEnumerable<ColorViewModel> models = await _contextDb.Color.Where(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active)
               .ProjectTo<ColorViewModel>(_mapper.ConfigurationProvider)
               .ToListAsync();
            return models;
        }

        public async Task<bool> DeleteColor(int deletedId)
        {
            try
            {
                Color category =new Color();
                category = await _contextDb.Color.FirstOrDefaultAsync(p => p.Id == deletedId);
                await _contextDb.Database.BeginTransactionAsync();
                category.UpdatedBy = "Bappy";
                category.UpdatedDate = DateTime.UtcNow;
                category.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
                var result = await _contextDb.SaveChangesAsync();
                await _contextDb.Database.RollbackTransactionAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                await _contextDb.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }


        public async Task<ColorViewModel> GetColorById(int Id)
        {
            ColorViewModel model = new ColorViewModel();
            Color data = await _contextDb.Color.Where(p => p.Id == Id).FirstOrDefaultAsync();
            model = _mapper.Map(data, model);
            return model;
        }
    }
}
