using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Settings.Interface;

namespace Services.ERP.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsBLLManager _settingsBLLManager;
        public SettingsController(ISettingsBLLManager settingsBLLManager)
        {
            _settingsBLLManager = settingsBLLManager;
        }

        [HttpPost("AddModule")]
        public async Task<IActionResult>AddModule(ModuleViewModel model)
        {
            try
            {
                var result = await _settingsBLLManager.AddModule(model);
                return Ok(new { status = true ,result= result }); ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("GetAllModule")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _settingsBLLManager.GetAllModule();
                return Ok(new { status = true, result = result }); ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("AddPage")]
        public async Task<IActionResult> AddPage(PageViewModel model)
        {
            try
            {
                var result = await _settingsBLLManager.AddPages(model);
                return Ok(new { status = true, result = result }); ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAllPages()
        {
            try
            {
                var result = await _settingsBLLManager.GetAllPages();
                return Ok( result ); ;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
