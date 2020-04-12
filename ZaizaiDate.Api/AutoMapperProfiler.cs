using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaizaiDate.Api.ViewModels;
using ZaizaiDate.Database.Entity;
using ZaizaiDate.Common.Extensions;

namespace ZaizaiDate.Api
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<AppUser, UserListViewModel>()
                .ForMember(dest => dest.PhotoUrl, 
                        opt => opt.MapFrom(src => src.Photos.FirstOrDefault(b => b.IsMain).Url))
                .ForMember(dest => dest.Age,
                        opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<AppUser, UserDetailedViewModel>()
                 .ForMember(dest => dest.PhotoUrl,
                        opt => opt.MapFrom(src => src.Photos.FirstOrDefault(b => b.IsMain).Url)).ForMember(dest => dest.Age,
                        opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoViewModel>();
        }
    }
}
