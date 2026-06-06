
using ProjetoFinalDevTulio.domain.Enums;
using ProjetoFinalDevTulio.domain.Services;

namespace ProjetoFinalDevTulio.domain.Entities;

public class TarefaItem : Entity
{
    // Encapsulamento das propriedades, aplicando imutabilidade
    public string Titulo { get; private set; }
    public string Descricao { get; private set;  }
    public ETarefaStatus Status { get; private set; }
    public ETarefaTipo Tipo { get; private set; }
    public DateTime DataCriacao { get; private set;  }
    public DateTime DataLimite { get; private set; }

    //Construtor privado para 
    private TarefaItem(int id, string titulo, string descricao, ETarefaStatus status, ETarefaTipo tipo, DateTime dataCriacao, DateTime dataLimite) : base(id)
    {
        Titulo = titulo;
        Descricao = descricao;
        Status = status;
        Tipo = tipo;
        DataCriacao = dataCriacao;
    }

    //Método Fábrica, ponto de entrada para criar um objeto válido

    public static TarefaItem Criar(int id, string titulo, string descricao, ETarefaStatus status, ETarefaTipo tipo, DateTime dataCriacao, DateTime dataLimite)
    {
        //Validações e normalizações
        if (NormalizadoService.TextoVazioOuNulo(titulo)) throw new Exception("TITULO_OBRIGATÓRIO");
        titulo = NormalizadoService.LimparEspacos(titulo);
        titulo = NormalizadoService.ParaMaiusculo(titulo);

        if (NormalizadoService.TextoVazioOuNulo(descricao)) throw new Exception("DESCRIÇÂO_OBRIGATORIO");
        descricao = NormalizadoService.LimparEspacos(descricao);

        if (!Enum.IsDefined(typeof(ETarefaStatus), status)) throw new Exception("STATUS_INVALIDO");

        long allFlags = 0;
            foreach (ETarefaTipo t in Enum.GetValues(typeof(ETarefaTipo))) 
            allFlags |= Convert.ToInt64(t);
            long tipoValue = Convert.ToInt64(tipo);
        if ((tipoValue & -allFlags) != 0) throw new Exception("TIPO_INVALIDO");

        if (dataCriacao == default) throw new Exception("DATA_CRIACAO_OBRIGATORIO");
        if (dataLimite == default) throw new Exception("DATA_LIMITE_OBRIGATORIO");
        if (dataLimite < dataCriacao) throw new Exception("DATA_LIMITE_MENOR_DATA_CRIACAO");

        //criação e retorno do objeto
        return new TarefaItem(id, titulo, descricao, status, tipo, dataCriacao, dataLimite);
    }

}
