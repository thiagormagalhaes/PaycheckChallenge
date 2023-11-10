using AutoMapper;
using PaycheckChallenge.Api.Responses;
using PaycheckChallenge.CrossCutting.Extensions;
using PaycheckChallenge.Domain.Entities;

namespace PaycheckChallenge.Api.AutoMapper;

public class DomainToApplicationProfile : Profile
{
    public DomainToApplicationProfile()
    {
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<Paycheck, PaycheckResponse>();
        CreateMap<Transaction, TransactionResponse>()
            .ForMember(x => x.Type, opt => opt.MapFrom(x => EnumExtensions.GetDescriptionFromEnumValue(x.Type)));
    }
}
