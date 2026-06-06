using ProjetoFinalDevTulio.domain.Entities;
using ProjetoFinalDevTulio.domain.Enums;
namespace ProjetoFinalDev1.Domain.Tests;

public class UnitTestTarefaItem
{
    [Fact]
    public void CriarTarefa_Valida_NaoDeveLancarExcecao()
    {
        // criando uma tarefa válida, deve ser criada sem lançar exceção
        // uso do operador bitwise para combinar múltiplos tipos de tarefa
        ETarefaTipo tipos = ETarefaTipo.Casa | ETarefaTipo.Trabalho | ETarefaTipo.Estudo;
        TarefaItem tarefa = TarefaItem.Criar(1, "Título da Tarefa", "Descrição da Tarefa", ETarefaStatus.Pendente, tipos, DateTime.Now, DateTime.Now.AddDays(7));
        Assert.NotNull(tarefa);
    }
    [Fact]
    public void CriarTarefa_Invalida_DeveLancarExcecao()
    {
        // validando a criação de tarefa com título vazio, deve lançar exceção
        Assert.Throws<Exception>(() => TarefaItem.Criar(1, "", "Descrição da Tarefa", ETarefaStatus.Pendente, ETarefaTipo.Casa, DateTime.Now, DateTime.Now.AddDays(7)));
    }
    [Fact]
    public void CriarTarefa_Invalida_StatusInvalido_DeveLancarExcecao()
    {
        // validando a criação de tarefa com status inválido, deve lançar exceção
        Assert.Throws<Exception>(() => TarefaItem.Criar(1, "Título da Tarefa", "Descrição da Tarefa", (ETarefaStatus)999, ETarefaTipo.Casa, DateTime.Now, DateTime.Now.AddDays(7)));
    }
    [Fact]
    public void CriarTarefa_Valida_VerificarNormalizado()
    {
        // criando uma tarefa com espaços extras e verificando a normalização
        TarefaItem tarefa = TarefaItem.Criar(1, " Título da Tarefa ", " Descrição da Tarefa ", ETarefaStatus.Pendente, ETarefaTipo.Casa, DateTime.Now, DateTime.Now.AddDays(7));
        Assert.Equal("TÍTULO DA TAREFA", tarefa.Titulo); // validando normalização do título
        Assert.Equal("Descrição da Tarefa", tarefa.Descricao); // validando normalização da descrição
    }
    [Fact]
    public void CriarTarefa_Invalida_DataLimiteMenorDataCriacao_DeveLancarExcecao()
    {
        // validando a criação de tarefa com data limite menor que data de criação, deve lançar exceção
        Assert.Throws<Exception>(() => TarefaItem.Criar(1, "Título da Tarefa", "Descrição da Tarefa", ETarefaStatus.Pendente, ETarefaTipo.Casa, DateTime.Now, DateTime.Now.AddDays(-1)));
    }
    [Fact]
    public void CriarTarefa_Invalida_verificarMessageExcecao()
    {
        var exception = Assert.Throws<Exception>(() => TarefaItem.Criar(1, "", "Descrição da Tarefa", ETarefaStatus.Pendente, ETarefaTipo.Casa, DateTime.Now, DateTime.Now.AddDays(7)));
        Assert.Equal("TITULO_OBRIGATÓRIO", exception.Message); // validando a mensagem de exceção
    }
}