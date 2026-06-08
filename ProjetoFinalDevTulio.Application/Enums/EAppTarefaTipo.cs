using System.ComponentModel.DataAnnotations;
namespace ProjetoFinalDevTulio.Application.Enums;

[Flags]
public enum EAppTarefaTipo
{
    [Display(Name = "Não definido")] None = 0,
    [Display(Name = "Trabalho")] Trabalho = 1,
    [Display(Name = "Estudo")] Estudo = 2,
    [Display(Name = "Casa")] Casa = 4,
    [Display(Name = "Saúde")] Saude = 8,
    [Display(Name = "Financeiro")] Financeiro = 16,
    [Display(Name = "Compras")] Compras = 32,
    [Display(Name = "Lazer")] Lazer = 64,
    [Display(Name = "Família")] Familia = 128,
    [Display(Name = "Pessoal")] Pessoal = 256,
}