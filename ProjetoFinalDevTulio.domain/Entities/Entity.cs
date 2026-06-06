namespace ProjetoFinalDevTulio.domain.Entities;

// Classe base para todas as entidades, garantindo identidade única e validação de ID
public abstract class Entity
{
    public int Id { get; protected set; }

    protected Entity(int id = 0)
    {
        if (id < 0) throw new Exception("ID_NEGATIVO");

        Id = id;
    }
}
