
using SecurityBLLManager.ERP.Accounting.AccountChart.Interface;
using SecurityBLLManager.ERP.Accounting.AccountChart.Logic;
using SecurityBLLManager.ERP.BranchServiceB.Logic;
using SecurityBLLManager.ERP.BrandBLL.Interface;
using SecurityBLLManager.ERP.Categories.Interface;
using SecurityBLLManager.ERP.GodownBLLManage.Logic;
using SecurityBLLManager.ERP.IBranchService.Interface;
using SecurityBLLManager.ERP.IGodownBLLManage.Interface;
using SecurityBLLManager.ERP.Inventory.Challan.Interface;
using SecurityBLLManager.ERP.Inventory.Challan.Logic;
using SecurityBLLManager.ERP.IProductBLL.Interface;
using SecurityBLLManager.ERP.IProductCategoryBLL.Interface;
using SecurityBLLManager.ERP.IProductSupplierBLL.Interface;
using SecurityBLLManager.ERP.IProductUnitConvBLL.Interface;
using SecurityBLLManager.ERP.ProductBLLManage.Logic;
using SecurityBLLManager.ERP.ProductBrand.Logic;
using SecurityBLLManager.ERP.ProductCategory.Logic;
using SecurityBLLManager.ERP.ProductCategoryBLLManage.Logic;
using SecurityBLLManager.ERP.ProductColor.Interface;
using SecurityBLLManager.ERP.ProductColorBLL.Logic;
using SecurityBLLManager.ERP.ProductSupplierBLLManager.Logic;
using SecurityBLLManager.ERP.ProductUnitConvBLLManager.Logic;
using SecurityBLLManager.ERP.Settings.Interface;
using SecurityBLLManager.ERP.Settings.Logic;
using SecurityBLLManager.ERP.UnitBLLManager.Interface;
using SecurityBLLManager.ERP.UnitBLLManager.Logic;

namespace Services.ERP.Extentions
{
    public static class InterfaceServiceDeclareExtentions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountChartBLLManager, AccountBLLManager>();
            services.AddScoped<IBranchServiceBLLManager, BranchServiceBLLManager>();
            services.AddScoped<IBrandBLLManager, BrandBLLManager>();
            services.AddScoped<ICategoriesBLLManager, CategoryBLLManager>();
            services.AddScoped<IChallanBLLManager, ChallanBLLManager>();
            services.AddScoped<IProductColorBLLManager, ProductColorBLLManager>();
            services.AddScoped<IGodownBLLManager, GodownBLLManager>();
            services.AddScoped<IProductBLLManager, ProductBLLManager>();
            services.AddScoped<IProductCategoryBLLManager, ProductCategoryBLLManager>();
            services.AddScoped<IProductSupplierBLLManager, ProductSupplierBLLManager>();
            services.AddScoped<IProductUnitConvBLLManager, ProductUnitConvBLLManager>();
            services.AddScoped<ISettingsBLLManager, SettingsBLLManager>();
            services.AddScoped<IUnitBLLManager, UnitBLLManager>();





            return services;
        }
    }
}
