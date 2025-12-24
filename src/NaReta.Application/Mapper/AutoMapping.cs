using AutoMapper;
using NaReta.Application.DTO;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Application.UseCases.Transaction._Common;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Domain.Entities;
using NaReta.Domain.ValueObjects;

namespace NaReta.Application.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        EntityToOutput();
        InputToValueObject();
    }

    private void EntityToOutput()
    {
        CreateMap<Account, OutputAccount>();
        CreateMap<Category, OutputCategory>();
        CreateMap<Transaction, OutputTransaction>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap(typeof(PagedResult<>), typeof(PaginationOutput<>));
    }

    private void InputToValueObject()
    {
        CreateMap<InputListTransaction, TransactionFilter>();
    }
}
