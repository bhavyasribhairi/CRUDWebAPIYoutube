using AutoMapper;
using CRUDWebAPIYoutube.Models;
using CRUDWebAPIYoutube.Models.DTO;

namespace CRUDWebAPIYoutube.Mappings
{
    public class AutoMapperProfile  :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Contact, ContactDTO>().ForMember(x=>x.Name , y=> y.MapFrom(z=>z.FullName)).ReverseMap();

            CreateMap<Details, DetailsDTO>().ReverseMap();
        }
    }
}
