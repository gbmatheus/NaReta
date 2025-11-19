using AutoMapper;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Domain.Entities;

namespace NaReta.Application.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        EntityToOutput();
    }

    private void EntityToOutput()
    {
        CreateMap<Account, OutputAccount>();
        CreateMap<Account, OutputShortAccount>();
        CreateMap<Category, OutputCategory>();
        CreateMap<Transaction, OutputTransaction>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Transaction, OutputTransactionIntoAccount>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
    }
}
