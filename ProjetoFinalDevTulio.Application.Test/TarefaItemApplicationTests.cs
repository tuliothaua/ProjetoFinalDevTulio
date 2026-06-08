using Microsoft.Extensions.DependencyInjection;
using ProjetoFinalDevTulio.Application.DTOs;
using ProjetoFinalDevTulio.Application.Enums;
using ProjetoFinalDevTulio.Application.Interfaces;
using ProjetoFinalDevTulio.Application.Tests;
namespace ProjetoFinalDevTulio.Application.Tests;

public class TarefaItemApplicationTests
{
    [Fact(Timeout = 60000)]
    public async Task TarefaItemService_Integracao_Adicionar_Obter_Atualizar_Remover()
    {
        // Arrange: DI usando repositório real (Infra)
        // Configuração dos serviços usando a classe DependencyInjection
        var services = DependencyInjection.ConfigureServices();
        // Cria o provedor de serviços
        var provider = DependencyInjection.BuildServiceProvider(services);
        // Obtém os serviços necessários via injeção de dependência
        var tarefaItemService = provider.GetRequiredService<ITarefaService>();
        // Dados de teste
        var dtoTarefa = new TarefaItemDTO
        {
            Titulo = "Tarefa de teste via aplicação",
            Descricao = "Nova tarefa de teste via camada de aplicação",
            DataCriacao = DateTime.Now,
            DataLimite = DateTime.Now.AddDays(7),
            Status = EAppTarefaStatus.Pendente,
            Tipo = EAppTarefaTipo.Pessoal
        };
        // Act - Adicionar
        var criado = await tarefaItemService.AdicionarService(dtoTarefa);
        // Assert - criação
        Assert.NotNull(criado);
        Assert.True(criado!.Id > 0);
        // Act - Obter todos
        var todos = await tarefaItemService.ObterTodosService();
        // Assert - obter todos
        Assert.NotNull(todos);
        // Act - Obter por id o adicionado anteriormente para editar
        var obtido = await tarefaItemService.ObterPorIdService(criado.Id);
        // Assert - obter
        Assert.NotNull(obtido);
        Assert.Equal(criado.Id, obtido!.Id);
        // Act - Atualizar
        var atualizar = new TarefaItemDTO
        {
            Id = criado.Id,
            Titulo = "Tarefa de teste via aplicação - Atualizada",
            Descricao = criado.Descricao,
            DataCriacao = criado.DataCriacao,
            DataLimite = criado.DataLimite,
            Status = criado.Status,
            Tipo = criado.Tipo
        };
        // Act - Atualizar
        var atualizado = await tarefaItemService.AtualizarService(atualizar);
        // Assert - atualizar
        Assert.NotNull(atualizado);
        Assert.Equal("TAREFA DE TESTE VIA APLICAÇÃO - ATUALIZADA", atualizado.Titulo);
        // Act - Remover
        var removido = await tarefaItemService.ExcluirService(criado.Id);
        Assert.True(removido);
        // Act - Conferir remoção
        var aposRemocao = await tarefaItemService.ObterPorIdService(criado.Id);
        Assert.Null(aposRemocao);
    }
}