using ProjetoFinalDevTulio.Application.Enums;
namespace ProjetoFinalDevTulio.Application.DTOs;

public class TarefaItemDTO
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public required EAppTarefaStatus Status { get; set; }
    public required EAppTarefaTipo Tipo { get; set; }
    public required DateTime DataCriacao { get; set; }
    public required DateTime DataLimite { get; set; }
}