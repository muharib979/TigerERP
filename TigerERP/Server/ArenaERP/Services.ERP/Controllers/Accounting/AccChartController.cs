﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Accounting.AccountChart.Interface;

namespace Services.ERP.Controllers.Accounting
{
    [Route("api/AccChart")]
    [ApiController]
    public class AccChartController : ControllerBase
    {
        private readonly IAccountChartBLLManager _bLLManager;

        public AccChartController(IAccountChartBLLManager bLLManager)
        {
            _bLLManager = bLLManager;
        }

        [HttpPost]

        public async Task<IActionResult> AddAccChart(AccChartViewModel model)
        {
            try
            {
                var result = await _bLLManager.AddAccChart(model);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccChartByComp()
        {
            try
            {
                return Ok(new { statu = true, response = "" });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        
    }
}
