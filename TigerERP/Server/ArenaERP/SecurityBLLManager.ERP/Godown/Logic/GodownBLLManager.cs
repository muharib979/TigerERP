using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.IGodownBLLManage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.GodownBLLManage.Logic
{
    public class GodownBLLManager : IGodownBLLManager
    {
        private readonly DatabaseContextDb _contextDb;
        private readonly IMapper _mapper;

        public GodownBLLManager(DatabaseContextDb contextDb, IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }

        public async Task<bool> AddGodown(GodownViewModel model)
        {
            try
            {
                Godown data;
                int godwonId = 0;
                data = await _contextDb.Godown.FirstOrDefaultAsync(p => p.Id == model.Id);
                model.GodownName = model.GodownName.Trim();
                await _contextDb.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Godown();
                    godwonId = await _contextDb.Godown.Where(u => u.CompId == model.CompId).MaxAsync(e => (int?)e.GodownId) ?? 0;
                    model.GodownId = godwonId + 1;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    data.CreatedBy = "Bappy";
                    data.CreatedDate= DateTime.Now;
                    await _contextDb.Godown.AddAsync(data);
                }
                if (_contextDb.Godown.Any(p => p.GodownName.ToLower() == model.GodownName.ToLower() && p.GodownId != model.GodownId && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.GodownName);
                data.UpdatedBy = "Bappy";
                data.UpdatedDate = DateTime.Now;
                data = _mapper.Map((GodownViewModel)model, data);
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

        public async Task<IEnumerable<GodownViewModel>> GetAllGodownByComp(int compId)
        {
            IEnumerable<GodownViewModel> models = await _contextDb.Godown.Where(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active)
                .ProjectTo<GodownViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return models;
        }

        public async Task<GodownViewModel> GetGodownById(int Id)
        {
            try
            {
                GodownViewModel model = new GodownViewModel();
                Godown data = await _contextDb.Godown.Where(p => p.Id == Id).FirstOrDefaultAsync();
                model = _mapper.Map(data, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteGodown(int Id)
        {
            try
            {
                Godown data = new Godown();
                await _contextDb.Database.BeginTransactionAsync();

                data = await _contextDb.Godown.FirstOrDefaultAsync(p => p.Id == Id);

                data.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
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

       
    }
}
