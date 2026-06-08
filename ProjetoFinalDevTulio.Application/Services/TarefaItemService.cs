using ProjetoFinalDevTulio.Application.Mappings;
using ProjetoFinalDevTulio.domain.Interfaces;
using ProjetoFinalDevTulio.Application.DTOs;
using ProjetoFinalDevTulio.Application.Interfaces;
namespace ProjetoFinalDevTulio.Application.Services;

public class TarefaItemService : ITarefaService
{
    private readonly Func<ITarefaRepository> _repoFactory;
    public TarefaItemService(Func<ITarefaRepository> repoFactory)
    {
        _repoFactory = repoFactory ?? throw new ArgumentNullException(nameof(repoFactory));
    }
    public async Task<TarefaItemDTO> AdicionarService(TarefaItemDTO tarefaItemDto)
    {
        // Cria a entidade de domínio a partir do DTO
        var tarefa = tarefaItemDto.ToEntity();
        // Salva no repositório
        await _repoFactory().Adicionar(tarefa);
        // Retorna o DTO atualizado com o ID gerado
        return tarefa.ToDto();
    }
    public async Task<TarefaItemDTO> AtualizarService(TarefaItemDTO tarefaItemDto)
    {
        // Verifica se o tarefa existe
        var tarefaExistente = await _repoFactory().ObterPorId(tarefaItemDto.Id) ?? throw new KeyNotFoundException($"TarefaItem ID {tarefaItemDto.Id} não encontrado.");
        // a partir dos dados do dto e do existente, cria uma nova instância com os valores atualizados
        var tarefaAtualizada = tarefaExistente.UpdateFromDto(tarefaItemDto);
        // Atualiza no repositório
        await _repoFactory().Atualizar(tarefaAtualizada);
        return tarefaAtualizada.ToDto();
    }
    public async Task<bool> ExcluirService(int id)
    {
        var tarefa = await _repoFactory().ObterPorId(id);
        if (tarefa == null) { return false; }
        await _repoFactory().Excluir(id);
        return true;
    }
    public async Task<TarefaItemDTO?> ObterPorIdService(int id)
    {
        var tarefa = await _repoFactory().ObterPorId(id);
        return (tarefa != null) ? tarefa.ToDto() : null!;
    }
    public async Task<IEnumerable<TarefaItemDTO>> ObterTodosService()
    {
        var tarefas = await _repoFactory().ObterTodos();
        return [.. tarefas.Select(c => c.ToDto())];
    }
}