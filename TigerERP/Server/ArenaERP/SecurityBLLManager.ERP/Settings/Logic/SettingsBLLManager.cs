using Common.ERP.UtilityManagement;
using Dapper;
using DatabaseContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using SecurityBLLManager.ERP.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.Settings.Logic
{
    public class SettingsBLLManager : ISettingsBLLManager
    {
        private readonly DatabaseContextDb _settingsDb;
        public SettingsBLLManager(DatabaseContextDb settingsDb)
        {
            _settingsDb = settingsDb;
        }
        public async Task<bool> AddModule(ModuleViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var data = new Modules();
                    data.SerialNo = model.SerialNo;
                    data.IsParent = model.IsParent;
                    data.Name = model.Name;
                    data.ModuleRoutePath = model.ModuleRoutePath;
                    data.ParentModuleId = model.ParentModuleId;
                    data.Image = model.Image;
                    data.IsActive = model.IsActive;
                    await _settingsDb.Modules.AddAsync(data);
                }
                var result = await _settingsDb.SaveChangesAsync() > 0;
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Something is Wrong");
            }
        }


        public async Task<List<ModuleViewModel>> GetAllModule()
        {
            List<ModuleViewModel> result;
            string sql = $"SELECT * FROM Modules";
            using (var con = new SqlConnection(Connection.ConnectionString()))
            {
                result = con.Query<ModuleViewModel>(sql).ToList();
            }
            return result;

        }


        public async Task<bool> AddPages(PageViewModel model)
        {
            if(model.Id == 0)
            {
                var data = new Pages();
                data.SerialNo = model.SerialNo;
                data.Name = model.Name;
                data.ModuleId=model.ModuleId;
                data.PageRoutePath = model.PageRoutePath;
                
                await _settingsDb.Pages.AddAsync(data);
            }

            return await _settingsDb.SaveChangesAsync() > 0;
        }
        public async Task<List<PageViewModel>> GetAllPages()
        {
            List<PageViewModel> result;
            using (var con = new SqlConnection(Connection.ConnectionString()))
            {
                string sql = $"SELECT dbo.Pages.Name, dbo.Pages.PageRoutePath, dbo.Pages.SerialNo, dbo.Pages.Id, dbo.Modules.Id AS ModuleId, dbo.Modules.Name AS ModuleName FROM dbo.Modules INNER JOIN dbo.Pages ON dbo.Modules.Id = dbo.Pages.ModuleId";

                result = con.Query<PageViewModel>(sql).ToList();
            }
            return result;
        }
    }
}
