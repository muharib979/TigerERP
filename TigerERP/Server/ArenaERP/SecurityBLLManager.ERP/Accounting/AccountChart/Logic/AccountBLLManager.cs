using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Accounting.AccountChart.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.Accounting.AccountChart.Logic
{
    public class AccountBLLManager : IAccountChartBLLManager
    {
        private readonly DatabaseContextDb _contextDb;
        private readonly IMapper _mapper;

        public AccountBLLManager(DatabaseContextDb contextDb,IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }

        public async Task<bool> AddAccChart(AccChartViewModel model)
        {
            try
            {
                AccChart data;
                int accountId = 0;
                data = await _contextDb.AccChart.FirstOrDefaultAsync(p => p.Id == model.Id);
                model.AccountName = model.AccountName.Trim();
                await _contextDb.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new AccChart();
                    accountId = await _contextDb.AccChart.Where(u => u.CompId == model.CompId ).MaxAsync(e => (int?)e.AccountId) ?? 0;
                    model.AccountId = accountId + 1;
                    data.CreatedBy = "Bappy";
                    data.CreatedDate = DateTime.UtcNow;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _contextDb.AccChart.AddAsync(data);
                }
                if (_contextDb.Product.Any(p => p.Name.ToLower() == model.AccountName.ToLower() && p.ProductId != model.AccountId && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.AccountName);
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

        public async Task<IEnumerable<AccChartViewModel>> GetAllAccChartByComp(int compId, int lowerGroupId)
        {
            IEnumerable<AccChartViewModel> models = await _contextDb.AccChart.Where(p => p.CompId == compId && p.LowerGroupId == lowerGroupId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active)
               .ProjectTo<AccChartViewModel>(_mapper.ConfigurationProvider)
               .ToListAsync();
            return models;
        }
        public async Task<bool> DeleteAccChart(int deleteId)
        {
            try
            {
                await _contextDb.Database.BeginTransactionAsync();
                AccChart accChart;
                accChart = await _contextDb.AccChart.FirstOrDefaultAsync(p => p.Id == deleteId);
                accChart.UpdatedBy = "Bappy";
                accChart.UpdatedDate = DateTime.UtcNow;
                accChart.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
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

        public async Task<AccChartViewModel> GetAccChartById(int Id)
        {
            try
            {
                AccChartViewModel model = new AccChartViewModel();
                var data = await _contextDb.AccChart.Where(p => p.Id == Id).FirstOrDefaultAsync();
                var result = _mapper.Map(data, model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
