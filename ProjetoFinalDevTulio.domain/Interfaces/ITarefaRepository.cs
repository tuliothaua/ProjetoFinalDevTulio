using ProjetoFinalDevTulio.domain.Entities;

namespace ProjetoFinalDevTulio.domain.Interfaces;

public interface ITarefaRepository
{
    Task<IEnumerable<TarefaItem>> ObterTodos();

    Task<TarefaItem?> ObterPorId(int id);

    Task<TarefaItem> Adicionar(TarefaItem tarefaItem);

    Task<TarefaItem> Atualizar (TarefaItem tarefaItem);

    Task<bool> Excluir(int id);
}
