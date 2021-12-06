using AutoMapper;
using ProductAPP.BLLayer.DTO;
using ProductAPP.DBLayer.Entities;

namespace ProductAPP.BLLayer.MappingProfiles
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductDTO, ProductDb>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<ProductDb, ProductDTO>();
        }
    }
}
