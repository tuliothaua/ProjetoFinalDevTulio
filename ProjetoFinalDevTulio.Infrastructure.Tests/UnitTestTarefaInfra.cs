using ProjetoFinalDevTulio.domain.Entities;
using ProjetoFinalDevTulio.domain.Enums;
using ProjetoFinalDevTulio.Infrastructure.Repositories;
namespace ProjetoFinalDevTulio.Infrastructure.Tests;

public class UnitTestTarefaInfra
{
    [Fact]
    public async Task Tarefa_ObterTodos()
    {
        // ObterTodos
        var repoTarefaTodos = new TarefaRepository();
        var resultado = await repoTarefaTodos.ObterTodos();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task Tarefa_Adicionar()
    {
        // cria objeto do tipo TarefaItem
        ETarefaTipo tipos = ETarefaTipo.Casa | ETarefaTipo.Trabalho;
        TarefaItem tarefa = TarefaItem.Criar(0, "Título da Tarefa2", "Descrição da Tarefa2", ETarefaStatus.Pendente, tipos, DateTime.Now, DateTime.Now.AddDays(7));
        // cria repositório e adiciona a tarefa
        var repoTarefaAdd = new TarefaRepository();
        var TarefaInserido = await repoTarefaAdd.Adicionar(tarefa);
        Assert.NotNull(TarefaInserido);
        Assert.True(TarefaInserido.Id > 0);
    }

    [Fact]
    public async Task Tarefa_ObterId_Atualizar()
    {
        // Criar e inserir uma tarefa para garantir que exista um registro para atualizar
        ETarefaTipo tipos = ETarefaTipo.Casa;
        var repoTarefaAdd = new TarefaRepository();
        var novaTarefa = TarefaItem.Criar(0, "Título para atualizar", "Descrição inicial", ETarefaStatus.Pendente, tipos, DateTime.Now, DateTime.Now.AddDays(7));
        var inserida = await repoTarefaAdd.Adicionar(novaTarefa);
        Assert.NotNull(inserida);
        // ObterPorId - existente para edição
        var repoTarefaBuscaId = new TarefaRepository();
        var TarefaPorId = await repoTarefaBuscaId.ObterPorId(inserida.Id);
        Assert.NotNull(TarefaPorId);
        // criar um novo objeto com os dados atualizados
        var TarefaAtualizado = TarefaItem.Criar(TarefaPorId.Id, "Título atualizado", "Descrição atualizada", TarefaPorId.Status, TarefaPorId.Tipo,
        TarefaPorId.DataCriacao, DateTime.Now.AddDays(20));
        // atualizar a tarefa no repositório
        var repoTarefaEdit = new TarefaRepository();
        var resultadoAtualizacao = await repoTarefaEdit.Atualizar(TarefaAtualizado);
        Assert.NotNull(resultadoAtualizacao);
        Assert.Equal("TÍTULO ATUALIZADO", resultadoAtualizacao.Titulo);
        Assert.Equal("Descrição atualizada", resultadoAtualizacao.Descricao);
    }
    [Fact]
    public async Task Tarefa_Excluir_ObterId()
    {
        // Criar e inserir uma tarefa para garantir que exista um registro para remover
        ETarefaTipo tipos = ETarefaTipo.Casa;
        var repoTarefaAdd = new TarefaRepository();
        var novaTarefa = TarefaItem.Criar(0, "Título para excluir", "Descrição remover", ETarefaStatus.Pendente, tipos, DateTime.Now, DateTime.Now.AddDays(7));
        var inserida = await repoTarefaAdd.Adicionar(novaTarefa);
        Assert.NotNull(inserida);
        var repoTarefaExcluir = new TarefaRepository();
        var resultadoExcluir = await repoTarefaExcluir.Excluir(inserida.Id);
        Assert.True(resultadoExcluir);
    }
}