using AutoMapper;
using DataLayer;
using DataLayer.Model;

namespace WebServer.Models.Profiles
{
    public class TitlesProfile : Profile
    {
        public TitlesProfile()
        {

            //.ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Titles, TitlesModel>();
        }
    }
}
