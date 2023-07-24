using AutoMapper;
using Common.ERP.UtilityManagement;
using DatabaseContext;
using Infrustracture.ERP.UnitOfWorkSetup.ForChallan.ForChallanDetails;
using Infrustracture.ERP.UnitOfWorkSetup.ForChallan.ForChallanMaster;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Inventory.Challan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.ERP.Enum;

namespace SecurityBLLManager.ERP.Inventory.Challan.Logic
{
    public class ChallanBLLManager : IChallanBLLManager
    {
        private readonly IChallanMasterUnitOfWork _challanunitOfWork;
        private readonly IChallanDetailsUnitOfWork _challandetailsunitOfWork;
        private readonly IMapper _mapper;

        public ChallanBLLManager(IChallanMasterUnitOfWork challanunitOfWork, IChallanDetailsUnitOfWork challandetailsunitOfWork ,IMapper mapper)
        {
            _challanunitOfWork = challanunitOfWork;
            _challandetailsunitOfWork = challandetailsunitOfWork;
            _mapper = mapper;
        }

        #region Save Challan
        public async Task<(bool IsSaved, int ChallanId)> SaveChallan(ChallanViewModel model)
        {
            if (model.ChallanDetails.Count == 0)
            {
                throw new Exception("No Product found!");
            }

            try
            {
                await _challanunitOfWork.BeginTransactionAsync();
                bool IsSuccess = false;
                int ChallanId = 0;
                int ChallanNo = 0;
                decimal totalPrice = 0;
                decimal totalQty = 0;
                decimal unitPrice = 0;
                ChallanMaster masterData = new ChallanMaster();
                ChallanDetails ChallanDetails = new ChallanDetails();
                if (model.ChallanMaster.NatureId == 1 || model.ChallanMaster.NatureId == 4 || model.ChallanMaster.NatureId == 49 || model.ChallanMaster.NatureId == 5)
                {
                    model.ChallanMaster.ChallanType = "DC";
                }
                else if (model.ChallanMaster.NatureId == 2 || model.ChallanMaster.NatureId == 3 || model.ChallanMaster.NatureId == 50)
                {
                    model.ChallanMaster.ChallanType = "RC";

                }
                else if (model.ChallanMaster.NatureId == 51)
                {
                    model.ChallanMaster.ChallanType = "CM";

                }
                else if (model.ChallanMaster.NatureId == 8)
                {
                    model.ChallanMaster.ChallanType = "WR";
                }
                else
                {
                    model.ChallanMaster.ChallanType = "NO";
                }

                ChallanId = CustomValidation.MaxNumber<int>("ChallanMaster", "ChallanId", "CompId", model.ChallanMaster.CompId);
                model.ChallanMaster.ChallanId=ChallanId+1;

                ChallanNo = CustomValidation.MaxNumberForThreeParemeter<int>("ChallanMaster", "ChallanNo", "CompId", model.ChallanMaster.CompId, "BranchId", model.ChallanMaster.BranchId, "ChallanType", model.ChallanMaster.ChallanType);
                model.ChallanMaster.ChallanNo = ChallanNo + 1;
                if (model.ChallanMaster.Id==0 || model.ChallanMaster.Id == null)
                {
                    masterData = new ChallanMaster();
                    masterData.CreatedBy = "Bappy";
                    masterData.CreatedDate = DateTime.Now;
                    masterData.Status = (int)AvailableStatus.Active;
                    await _challanunitOfWork.ChallanMaster.AddAsync(masterData);
                }
                masterData = _mapper.Map(model.ChallanMaster, masterData);

                sbyte  multiplier = (sbyte)(model.ChallanMaster.ChallanType == "DC" ? -1 : 1);

                foreach (ChallanDetailsViewModel data in model.ChallanDetails)
                {
                    if (data.Id == 0 || data.Id == null)
                    {
                        ChallanDetails = new ChallanDetails();
                        ChallanDetails.CreatedBy = "Bappy";
                        ChallanDetails.CreatedDate = DateTime.Now;
                        ChallanDetails.Status = (int)AvailableStatus.Active;
                        await _challandetailsunitOfWork.ChallanDetails.AddRangeAsync(ChallanDetails);
                    }
                    data.ChallanId = model.ChallanMaster.ChallanId;
                    data.ChallanNo= model.ChallanMaster.ChallanNo;
                    data.ChallanType= model.ChallanMaster.ChallanType;
                    data.PcsQty = data.PcsQty * multiplier;
                    data.UnitQty = data.UnitQty * multiplier;
                    data.UniqueQty= (data.UnitQty * data.Factor) +data.PcsQty;
                    if (model.ChallanMaster.ChallanType == "DC")
                    {
                        totalPrice = CustomValidation.SumForUniquePriceAndQty<int>("ChallanDetail", "UniquePrice", "ProductId", data.ProductId, "CompId", data.CompId, "BranchId", data.BranchId);
                         totalQty = CustomValidation.SumForUniquePriceAndQty<int>("ChallanDetail", "UniqueQty", "ProductId", data.ProductId, "CompId", data.CompId, "BranchId", data.BranchId);
                         unitPrice = totalPrice / totalQty;
                    }
                    data.UnitPrice = unitPrice > 0 ? unitPrice : data.UnitPrice;
                    data.UniquePrice =(data.UnitPrice *  data.UniqueQty);
                    ChallanDetails = _mapper.Map(data, ChallanDetails);

                }

                var result = await _challanunitOfWork.SaveAsync();
                await _challanunitOfWork.CommitTransactionAsync();
                return (result, model.ChallanMaster.ChallanId);

            }
            catch (Exception ex)
            {
                await _challanunitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetChallan No

        public async Task<int> GetChallanNoByComp(int compId, int branchId, ChallanType challanType)
        {
            string type = "";
            switch (challanType)
            {
                case ChallanType.Delivery:
                    type = "DC";
                    break;
                case ChallanType.Receive:
                    type = "RC";
                    break;
                case ChallanType.DeliveryReturn:
                    type = "RC";
                    break;
                case ChallanType.ReceiveReturn:
                    type = "DC";
                    break;
                case ChallanType.Transfer:
                    type = "TC";
                    break;
                case ChallanType.Manufacture:
                    type = "MC";
                    break;
                case ChallanType.Consumption:
                    type = "CM";
                    break;

                case ChallanType.WReceive:
                    type = "WR";
                    break;

                default:
                    throw new Exception("Invalid Challan Type");
            }
            int ChallanNo = CustomValidation.MaxNumberForThreeParemeter<int>("ChallanMaster", "ChallanNo", "CompId",compId, "BranchId", branchId, "ChallanType", type);
            return ChallanNo +1;

        }
        #endregion
    }
}
