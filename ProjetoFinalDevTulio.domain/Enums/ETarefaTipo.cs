namespace ProjetoFinalDevTulio.domain.Enums;

[Flags]
public enum ETarefaTipo
    //Sempre é uma boa prática o (E) e a associação no nome (E Tarefa Tipo)
{
    Nome = 0,
    Trabalho = 1,
    Estudo = 2,
    Casa = 4,
    Saude = 8,
    Financeiro = 16,
    Compras = 32,
    Lazer = 64,
    Familia = 128,
    Pessoal = 256,
}

//Recurso do enum usando a diretiva: [Flag]
//Sequencia numerica tem que ser base de 2.

// Diferente do Tarefa Status
// pro tipo eu posso ter valores combinados nesse enum
// eu posso ter uma tarefa que ela é trabalho familiar.
