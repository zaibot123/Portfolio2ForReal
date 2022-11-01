using AutoMapper;
using DataLayer;


namespace WebServer.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListModel>();
            //.ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Product, ProductModel>();
        }
    }
}
