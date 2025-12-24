using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using NaReta.Application.Mapper;

namespace NaReta.UnitTest.Builder.Mapper;

internal class MapperBuilder
{
    public static IMapper BuildOld()
    {
        var mapper = new MapperConfiguration(config => config.AddProfile(new AutoMapping()), new NullLoggerFactory());
        return mapper.CreateMapper();
    }

    private static readonly MapperConfiguration _mapper
       = new MapperConfiguration(config => config.AddProfile(new AutoMapping()), new NullLoggerFactory());

    public static IMapper Build()
    {
        return _mapper.CreateMapper();
    }
}
