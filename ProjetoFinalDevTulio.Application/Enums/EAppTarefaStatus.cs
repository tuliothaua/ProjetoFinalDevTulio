using System.ComponentModel.DataAnnotations;
namespace ProjetoFinalDevTulio.Application.Enums;

public enum EAppTarefaStatus
{
    [Display(Name = "Pendente")] Pendente = 0,
    [Display(Name = "Em andamento")] EmAndamento = 1,
    [Display(Name = "Concluída")] Concluida = 2,
    [Display(Name = "Adiada")] Adiada = 3,
    [Display(Name = "Cancelada")] Cancelada = 4
}