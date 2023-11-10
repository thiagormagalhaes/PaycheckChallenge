using AutoMapper;

namespace PaycheckChallenge.Api.AutoMapper;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DomainToApplicationProfile>();
            cfg.AddProfile<ApplicationToDomainProfile>();
        });
}
