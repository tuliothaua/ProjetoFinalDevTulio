using ProjetoFinalDevTulio.Application.DTOs;
using ProjetoFinalDevTulio.domain.Entities;
namespace ProjetoFinalDevTulio.Application.Mappings;

public static class TarefaItemMappings
{
    public static TarefaItemDTO ToDto(this TarefaItem tarefaItem)
    {
        return new TarefaItemDTO
        {
            Id = tarefaItem.Id,
            Titulo = tarefaItem.Titulo,
            Descricao = tarefaItem.Descricao,
            Status = tarefaItem.Status.ToApp(),
            Tipo = tarefaItem.Tipo.ToApp(),
            DataCriacao = tarefaItem.DataCriacao,
            DataLimite = tarefaItem.DataLimite
        };
    }
    public static TarefaItem ToEntity(this TarefaItemDTO tarefaItemDto)
    {
        return TarefaItem.Criar(
        tarefaItemDto.Id, // Usar ID do DTO

        tarefaItemDto.Titulo,
        tarefaItemDto.Descricao,
        tarefaItemDto.Status.ToDomain(),
        tarefaItemDto.Tipo.ToDomain(),
        tarefaItemDto.DataCriacao,
        tarefaItemDto.DataLimite

        );
    }
    public static TarefaItem UpdateFromDto(this TarefaItem tarefaItem, TarefaItemDTO tarefaItemDto)
    {
        return TarefaItem.Criar(
        tarefaItem.Id,

        tarefaItemDto.Titulo ?? tarefaItem.Titulo,
        tarefaItemDto.Descricao ?? tarefaItem.Descricao,

        (tarefaItemDto.Status != default) ? tarefaItemDto.Status.ToDomain() : tarefaItem.Status,
        (tarefaItemDto.Tipo != default) ? tarefaItemDto.Tipo.ToDomain() : tarefaItem.Tipo,
        (tarefaItemDto.DataCriacao != default) ? tarefaItemDto.DataCriacao : tarefaItem.DataCriacao,
        (tarefaItemDto.DataLimite != default) ? tarefaItemDto.DataLimite : tarefaItem.DataLimite
        );
    }
}