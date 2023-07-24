using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Categories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.ProductCategory.Logic
{
    public class CategoryBLLManager : ICategoriesBLLManager
    {

        private readonly DatabaseContextDb _databaseContext;
        private readonly IMapper _mapper;
        public CategoryBLLManager(DatabaseContextDb databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<bool> AddCategory(CategoryViewModel model)
        {
            try
            {
                Category data;
                int categoryId = 0;
                data = await _databaseContext.Category.FirstOrDefaultAsync(p => p.Id == model.Id);
                model.CategoryName = model.CategoryName.Trim();
                await _databaseContext.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Category();
                    categoryId = await _databaseContext.Category.Where(u => u.CompId == model.CompId).MaxAsync(e => (int?)e.CategoryId) ?? 0;
                    model.CategoryId = categoryId + 1;
                    data.CreatedBy = "Bappy";
                    data.CreatedDate = DateTime.UtcNow;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _databaseContext.Category.AddAsync(data);
                }
                if (_databaseContext.Category.Any(p => p.CategoryName.ToLower() == model.CategoryName.ToLower() && p.CategoryId != model.CategoryId && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.CategoryName);
                data.UpdatedBy = "Bappy";
                data.UpdatedDate = DateTime.UtcNow;
                data = _mapper.Map((CategoryViewModel)model, data);
                var result = await _databaseContext.SaveChangesAsync();
                await _databaseContext.Database.CommitTransactionAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                await _databaseContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoryByComp(int compId)
        {
            IEnumerable<CategoryViewModel> models = await _databaseContext.Category.Where(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return models;
        }


        public async Task<bool> DeleteCategory(int DeleteId)
        {
            try
            {
                Category category;
                category = await _databaseContext.Category.FirstOrDefaultAsync(p => p.Id == DeleteId);
                category.UpdatedBy = "Bappy";
                category.UpdatedDate = DateTime.UtcNow;
                category.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
                var result = await _databaseContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<CategoryViewModel> GetCategoryById(int Id)
        {
            try
            {
                CategoryViewModel model = new CategoryViewModel();
                Category data = await _databaseContext.Category.Where(p => p.Id == Id).FirstOrDefaultAsync();
                model = _mapper.Map(data, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
