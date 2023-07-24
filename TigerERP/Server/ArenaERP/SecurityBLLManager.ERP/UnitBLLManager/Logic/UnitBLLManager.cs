using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.UnitBLLManager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.UnitBLLManager.Logic
{
    public class UnitBLLManager : IUnitBLLManager
    {
        private readonly DatabaseContextDb _contextDb;
        private readonly IMapper _mapper;

        public UnitBLLManager(DatabaseContextDb contextDb, IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }

        public async Task<bool> AddUnit(UnitViewModel model)
        {
            try
            {
                Unit data;
                int unitId = 0;
                data = await _contextDb.Unit.FirstOrDefaultAsync(p => p.Id == model.Id);
                model.UnitName = model.UnitName.Trim();
                await _contextDb.Database.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Unit();
                    unitId = await _contextDb.Unit.Where(u => u.CompId == model.CompId).MaxAsync(e => (int?)e.UnitId) ?? 0;
                    model.UnitId = unitId + 1;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _contextDb.Unit.AddAsync(data);
                }
                if (_contextDb.Unit.Any(p => p.UnitName.ToLower() == model.UnitName.ToLower() && p.UnitId != model.UnitId && p.CompId == model.CompId))
                    throw new DuplicateWaitObjectException("Name", model.UnitName);
                data = _mapper.Map((UnitViewModel)model, data);
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

        public async Task<IEnumerable<UnitViewModel>> GetAllUnitByComp(int compId)
        {
            IEnumerable<UnitViewModel> models = await _contextDb.Unit.Where(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active)
                .ProjectTo<UnitViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return models;
        }

        public async Task<UnitViewModel> GetUnitById(int Id)
        {
            try
            {
                UnitViewModel model = new UnitViewModel();
                Unit data = await _contextDb.Unit.Where(p => p.Id == Id).FirstOrDefaultAsync();
                model = _mapper.Map(data, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteUnit(int Id)
        {
            try
            {
                Unit data = new Unit();
                await _contextDb.Database.BeginTransactionAsync();

                data = await _contextDb.Unit.FirstOrDefaultAsync(p => p.Id == Id);

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
