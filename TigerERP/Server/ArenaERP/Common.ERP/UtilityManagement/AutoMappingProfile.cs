

using AutoMapper;
using ModelClass.ERP.DTO;
using ModelClass.ERP.ViewModel;

namespace Common.ERP.UtilityManagement
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<AccChartViewModel, AccChart>().ReverseMap();
            CreateMap<AccChart, AccChartViewModel>().ReverseMap();
            //Branch

            CreateMap<Branch, BranchViewModel>();
            CreateMap<BranchViewModel, Branch>();

            //Brand

            CreateMap<BrandViewModel, Brand>();
            CreateMap<Brand, BrandViewModel>();

            //Category

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();


            //Challan Details
            CreateMap<ChallanDetails, ChallanDetailsViewModel>();
            CreateMap<ChallanDetailsViewModel, ChallanDetails>();

            //Challan Master
            CreateMap<ChallanMaster, ChallanMasterViewModel>();
            CreateMap<ChallanMasterViewModel, ChallanMaster>();

            //Color
            CreateMap<Color,ColorViewModel>();
            CreateMap<ColorViewModel, Color>();

            //Godown
            CreateMap<Godown, GodownViewModel>();
            CreateMap<GodownViewModel, Godown>();

            //Modules


            //Pages



            //Product
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            //ProductCategory
            CreateMap<ProductCategories, ProductCategoryViewModel>()
                 .ForMember(d => d.ProductName, s => s.MapFrom(x => x.Product.Name + " - " + x.Product.Description))
                .ForMember(d => d.CategoryName, s => s.MapFrom(x => x.Category.CategoryName ));
            CreateMap<ProductCategoryViewModel, ProductCategories>();

            //ProductSupplier
            CreateMap<ProductSupplierViewModel, ProductSupplier>();
            CreateMap<ProductSupplier, ProductSupplierViewModel>()
                .ForMember(d => d.ProductName, s => s.MapFrom(x => x.Product.Name + " - " + x.Product.Description))
                .ForMember(d => d.AccountName, s => s.MapFrom(x => x.AccChart.AccountName + " - " + x.AccChart.AliasName));


            //ProductUnit
            CreateMap<ProductUnit, ProductUnitViewModel>();
            CreateMap<ProductUnitViewModel, ProductUnit>();


            //Unit

            CreateMap<Unit, UnitViewModel>();
            CreateMap<UnitViewModel, Unit>();

        }
    }
}
