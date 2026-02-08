using AutoMapper;

namespace RohamPaint.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Color, ViewModels.ColorViewModel>()
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Car.Make))
                .ForMember(dest => dest.ColorType, opt => opt.MapFrom(src => src.ColorType.Type))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Name : null));
        }
    }
}
