using AutoMapper;

namespace SkysFormsDemo.Infrastructure.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Data.Person, SkysFormsDemo.Pages.Person.EditModel>()
            .ForMember(dest => dest.Namnet, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<Data.Person, SkysFormsDemo.Pages.Person.NewModel>()
            .ReverseMap();

    }
}