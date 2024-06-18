using AutoMapper;
using IPF.Brewery.Common.Models.DTO;
using IPF.Brewery.Common.Models.Request;
using IPF.Brewery.Common.Models.Response;

namespace IPF.Brewery.API.Service
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Common.Models.DTO.Brewery, BreweryResponseModel>();
            
            CreateMap<Common.Models.DTO.Brewery, BreweryBeer>()
                .ForMember(dest => dest.Brewery,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Beers,
                    opt => opt.MapFrom(src => src.Beer));

            CreateMap<BreweryPayload, Common.Models.DTO.Brewery>();

            CreateMap<Bar, BarResponseModel>();

            CreateMap<Common.Models.DTO.Bar, BarBeer>()
                .ForMember(dest => dest.Bar,
                    opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Beers,
                    opt => opt.MapFrom(src => src.Beer));

            CreateMap<BarPayload, Bar>();

            CreateMap<Beer, BeerResponseModel>()
                .ForMember(dest => dest.BeerType,
                    opt => opt.MapFrom(src => src.BeerType.BeerTypeName)); 

            CreateMap<BeerPayload, Beer>();

            CreateMap<BeerType, BeerTypeResponseModel>()
                .ForMember(dest => dest.BeerType,
                opt => opt.MapFrom(src => src.BeerTypeName));

            CreateMap<BeerTypePayload, BeerType>()
                .ForMember(dest => dest.BeerTypeName,
                    opt => opt.MapFrom(src => src.BeerType));


        }
    }
}
