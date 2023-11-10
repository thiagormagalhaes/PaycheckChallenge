using AutoMapper;
using PaycheckChallenge.Api.AutoMapper;

namespace PaycheckChallenge.Tests.Configurations;
public class AutoMapperFixture
{
    private static IMapper _mapper;

    private static IMapper Initialize()
    {
        var config = AutoMapperConfig.RegisterMappings();

        return config.CreateMapper();
    }

    public static IMapper GetInstance()
    {
        if (_mapper == null )
        {
            _mapper = Initialize();
        }

        return _mapper;
    }
}
