using AutoMapper;

namespace RohamPaint.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Color, ViewModels.ColorViewModel>()
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Car.Name))
                .ForMember(dest => dest.ColorType, opt => opt.MapFrom(src => src.ColorType.Type))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Name : null));

            CreateMap<ViewModels.ColorCreateViewModel, Models.Color>();
            CreateMap<ViewModels.ColorFormulViewModel, Models.ColorFormul>().ReverseMap();
            CreateMap<ViewModels.ColorFormulEditViewModel, Models.ColorFormul>().ReverseMap();





            //CreateMap<Models.Formul, ViewModels.FormulViewModel>()
            //    .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color != null ? src.Color.Code : null))
            //    .ForMember(dest => dest.BaseName, opt => opt.MapFrom(src => src.BaseColor != null ? src.BaseColor.Name : null));
            //CreateMap<ViewModels.FormulViewModel, Models.Formul>()
            //   .ForMember(dest => dest.Color, opt => opt.Ignore())
            //   .ForMember(dest => dest.BaseColor, opt => opt.Ignore());
        }
    }
}
