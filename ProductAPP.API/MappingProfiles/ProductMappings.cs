using AutoMapper;
using ProductAPP.API.Models.Requests;
using ProductAPP.API.Models.Responses;
using ProductAPP.BLLayer.DTO;

namespace ProductAPP.API.MappingProfiles
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductResponse, ProductDTO>().ReverseMap();
            CreateMap<ProductCreateRequest, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateRequest, ProductDTO>().ReverseMap();
        }
    }
}
