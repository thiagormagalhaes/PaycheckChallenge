using AutoMapper;
using PaycheckChallenge.Api.Requests;
using PaycheckChallenge.Application.Commands.CreateEmployee;

namespace PaycheckChallenge.Api.AutoMapper;

public class ApplicationToDomainProfile : Profile
{
    public ApplicationToDomainProfile()
    {
        CreateMap<CreateEmployeeRequest, CreateEmployeeCommand>();
    }
}
