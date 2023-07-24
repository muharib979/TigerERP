

using Infrustracture.ERP.RepositorySetup.ForBranch;
using Infrustracture.ERP.RepositorySetup.ForBrand;
using Infrustracture.ERP.RepositorySetup.ForChallan.ForChallanDetais;
using Infrustracture.ERP.RepositorySetup.ForChallan.ForChallanMaster;
using Infrustracture.ERP.UnitOfWorkSetup.ForBranch;
using Infrustracture.ERP.UnitOfWorkSetup.ForBrand;
using Infrustracture.ERP.UnitOfWorkSetup.ForChallan.ForChallanDetails;
using Infrustracture.ERP.UnitOfWorkSetup.ForChallan.ForChallanMaster;

namespace Services.ERP.Extentions
{
    public static class UnitOfworkDeclareInServiceExtention
    {
        public static IServiceCollection AddScopedServicesForUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            //Repository
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IChallanDetailsRepository, ChallanDetailsRepository>();
            services.AddScoped<IChallanMasterRepository, ChallanMasterRepository>();



            //Unit Of Work
            services.AddScoped<IBranchUnitOfWork, BranchUnitOfWork>();
            services.AddScoped<IBrandUnitOfWork, BrandUnitOfWork>();
            services.AddScoped<IChallanDetailsUnitOfWork, ChallanDetailsUnitOfWork>();
            services.AddScoped<IChallanMasterUnitOfWork, ChallanMasterUnitOfWork>();


            return services;
        }
    }
    
}
