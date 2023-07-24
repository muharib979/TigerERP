using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBLLManager.ERP.Settings.Interface
{
    public interface ISettingsBLLManager
    {

        //Modules Interface
        Task<bool> AddModule(ModuleViewModel model);
        Task<List<ModuleViewModel>> GetAllModule();

        //Pages Interfaces

        Task<bool> AddPages(PageViewModel model);
        Task<List<PageViewModel>> GetAllPages();

    }
}
