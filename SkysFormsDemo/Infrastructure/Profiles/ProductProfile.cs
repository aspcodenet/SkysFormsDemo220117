using AutoMapper;

namespace SkysFormsDemo.Infrastructure.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Data.Product, SkysFormsDemo.ViewModels.ProductViewModel>()
            .ReverseMap();

    }
}