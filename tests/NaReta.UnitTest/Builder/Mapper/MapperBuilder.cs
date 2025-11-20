using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using NaReta.Application.Mapper;

namespace NaReta.UnitTest.Builder.Mapper;

internal class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config => config.AddProfile(new AutoMapping()), new NullLoggerFactory());
        return mapper.CreateMapper();
    }
}
