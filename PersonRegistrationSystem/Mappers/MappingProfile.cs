using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem;

public class MappingProfile : Profile
{

    public MappingProfile()
    {

        CreateMap<PersonInformation, PersonInformationResponse>()
        .ReverseMap()
        .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore())
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<PersonInformation, PersonInformationRequest>()
        .ReverseMap()
        .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore())
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<PlaceOfResidence, PlaceOfResidenceDto>().ReverseMap();

        CreateMap<User, UserResponse>()
        .ForMember(dest => dest.PersonInformationdata, opt => opt.MapFrom(src => src.PersonInformation.Select(pi => new PersonInformationdata
        {
            Name = pi.Name,
            LastName = pi.LastName,
            PersonId = pi.Id.ToString()
        }).ToList()));





    }
}
