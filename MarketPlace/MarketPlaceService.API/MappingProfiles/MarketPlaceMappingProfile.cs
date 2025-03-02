using System;
using AutoMapper;
using MarketPlaceService.API.DataTransfer;
using MarketPlaceService.API.Models;

namespace MarketPlaceService.API.MappingProfiles
{
    public class MarketPlaceMappingProfile : Profile
    {
        public MarketPlaceMappingProfile()
        {
            CreateMap<MarketPlaceCreateDTO,MarketPlace>();
            CreateMap<MarketPlaceDTO,MarketPlace>().ReverseMap();
        }
    }
}
