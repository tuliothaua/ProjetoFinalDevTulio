using System.Text.RegularExpressions;

namespace ProjetoFinalDevTulio.domain.Services;

public static partial class NormalizadoService
{
    //Normalizar dados de entrada

    //Verifica se o texto é nulo ou vazio
    public static bool TextoVazioOuNulo(string? texto) => string.IsNullOrWhiteSpace(texto);

    //Remove espaços repetidos e espaços no início e no final do texto
    public static string LimparEspacos(string? texto) => string.IsNullOrWhiteSpace(texto) ? string.Empty : EspacosRegex.Replace(texto, " ").Trim();

    // Converte o texto para maiúsculo

    public static string ParaMaiusculo(string? texto) => string.IsNullOrEmpty(texto) ? string.Empty : texto.ToUpperInvariant();

    private static readonly Regex EspacosRegex = new(@"\s+", RegexOptions.Compiled);
}