using Microsoft.Extensions.DependencyInjection;
using NaReta.Application.Mapper;
using NaReta.Application.UseCases.Account.Create;
using NaReta.Application.UseCases.Account.Get;
using NaReta.Application.UseCases.Category.Create;
using NaReta.Application.UseCases.Category.List;
using NaReta.Application.UseCases.Category.Update;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Application.UseCases.Transaction.Delete;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Application.UseCases.Transaction.Update;
using NaReta.Application.UseCases.Transactions.Create;

namespace NaReta.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        AddUseCase(service);
        AddMapper(service);
    }

    private static void AddUseCase(IServiceCollection service)
    {
        service.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        service.AddScoped<IListCategoryUseCase, ListCategoryUseCase>();
        service.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

        service.AddScoped<ICreateAccountUseCase, CreateAccountUseCase>();
        service.AddScoped<IListAccountUseCase, ListAccountUseCase>();
        service.AddScoped<IGetAccountUseCase, GetAccountUseCase>();

        service.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        service.AddScoped<IListTransactionUseCase, ListTransactionUseCase>();
        service.AddScoped<IUpdateTrasanctionUseCase, UpdateTrasanctionUseCase>();
        service.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();
    }

    private static void AddMapper(IServiceCollection service)
    {
        service.AddAutoMapper(config => { }, typeof(AutoMapping));
    }

}
