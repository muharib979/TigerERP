using AutoMapper;
using Common.ERP.UtilityManagement;
using DatabaseContext;
using Infrustracture.ERP.UnitOfWorkSetup.ForBranch;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.IBranchService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.BranchServiceB.Logic
{
    public class BranchServiceBLLManager : IBranchServiceBLLManager
    {
        private readonly IBranchUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchServiceBLLManager(IBranchUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddBranch(BranchViewModel model)
        {
            try
            {
                Branch data = new Branch();
                data = await _unitOfWork.Branch.GetByIdAsync(model.Id);
                int branchId = 0;
                model.Name = model.Name.Trim();
                await _unitOfWork.BeginTransactionAsync();
                if (data == null)
                {
                    data = new Branch();
                    branchId =  CustomValidation.MaxNumber<int>("Branch","BranchId", "CompId", model.CompId);
                    model.BranchId = branchId + 1;
                    data.Status = (int)Common.ERP.Enum.AvailableStatus.Active;
                    await _unitOfWork.Branch.AddAsync(data);
                }
                if (await _unitOfWork.Branch.IsExist(p => p.CompId == model.CompId && p.BranchId != model.BranchId && p.Name.ToLower().Trim() == model.Name.ToLower().Trim()))
                    throw new DuplicateWaitObjectException("Name", model.Name);
                data = _mapper.Map(model, data);
                 var result=await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return result;
            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BranchViewModel>> GetAllBranchByComp(int compId)
        {
            IEnumerable<BranchViewModel> result = new BranchViewModel[] { };
            var models = await _unitOfWork.Branch.GetAsync(p => p.CompId == compId && p.Status == (int)Common.ERP.Enum.AvailableStatus.Active, string.Empty);
             result=_mapper.Map(models, result);
             return result;
        }

        public async Task<BranchViewModel> GetBranchById(int Id)
        {
            try
            {
                BranchViewModel model = new BranchViewModel();
                Branch? data = await _unitOfWork.Branch.GetByIdAsync(Id);
                model = _mapper.Map(data, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(BranchViewModel model)
        {
            try
            {
                Branch data = new Branch();
                data = await _unitOfWork.Branch.GetByIdAsync(model.Id);
                
                model.Name = model.Name.Trim();
                await _unitOfWork.BeginTransactionAsync();
                if (await _unitOfWork.Branch.IsExist(p => p.CompId == model.CompId && p.BranchId != model.BranchId && p.Name.ToLower().Trim() == model.Name.ToLower().Trim()))
                    throw new DuplicateWaitObjectException("Name", model.Name);
                await _unitOfWork.Branch.EditAsync(data);
                data = _mapper.Map(model, data);
                var result=await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteBranch(int Id)
        {
            try
            {
                Branch? data = new Branch();
                data = await _unitOfWork.Branch.GetByIdAsync(Id);
                data.Name = data.Name.Trim();
                await _unitOfWork.BeginTransactionAsync();
                data.Status = (int)Common.ERP.Enum.AvailableStatus.Delete;
                await _unitOfWork.Branch.EditAsync(data);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                throw new Exception(ex.Message);
            }
        }
    }
}
