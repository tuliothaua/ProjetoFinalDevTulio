using ProjetoFinalDevTulio.Application.DTOs;
namespace ProjetoFinalDevTulio.Application.Interfaces;

public interface ITarefaService
{
    Task<IEnumerable<TarefaItemDTO>> ObterTodosService();
    Task<TarefaItemDTO?> ObterPorIdService(int id);
    Task<TarefaItemDTO> AdicionarService(TarefaItemDTO tarefaItem);
    Task<TarefaItemDTO> AtualizarService(TarefaItemDTO tarefaItem);
    Task<bool> ExcluirService(int id);
}