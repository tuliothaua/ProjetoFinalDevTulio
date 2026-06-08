using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalDevTulio.Application.Dependencylnjection;
using ProjetoFinalDevTulio.Application.Interfaces;
using ProjetoFinalDevTulio.Application.Services;
using ProjetoFinalDevTulio.domain.Interfaces;
using ProjetoFinalDevTulio.Infrastructure.Repositories;
namespace ProjetoFinalDevTulio.Application.Dependencylnjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registra os serviços da camada de aplicação

        services.AddTransient<ITarefaService, TarefaItemService>();
        // AddScoped: cria uma instância do serviço por requisição HTTP.

        // AddSingleton: cria uma única instância do serviço durante toda a vida útil da aplicação.
        // AddTransient: cria uma nova instância do serviço toda vez que ele é solicitado.
        // Registra as fábricas Func<IRepo> para criar instâncias sob demanda nos services
        // Instancias dos repositórios são criadas conforme necessário
        services.AddTransient(provider =>

        {
            var config = provider.GetRequiredService<RepositoryConfig>();
            return (Func<ITarefaRepository>)(() => new TarefaRepository());
        });
        return services;
    }
}