using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
namespace ProjetoFinalDevTulio.Infrastructure.Data;

public static class DbProvider
{
    public static DbConnection CreateConnection()
    {
        try
        {
            // Registrar o provedor do SQLite no DbProviderFactories - Isso é obrigatório no .NET Core/5+ para que o GetFactory funcione.
            DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", SqliteFactory.Instance);
            // Obter a fábrica (Factory) usando o nome do provedor registrado
            DbProviderFactory factory = DbProviderFactories.GetFactory("Microsoft.Data.Sqlite");
            // String de conexão para um banco em memória: "Data Source=:memory:"; // String de conexão para um banco em arquivo: "Data Source=banco.db"
            string connectionString = "Data Source=db_tarefas.db";
            DbConnection connection = new SqliteConnection(connectionString);
            return connection ?? throw new Exception($"FALHA_CONEXAO {connectionString}");
        }
        catch (DbException ex) { throw new Exception($"FALHA_CONEXAO", ex); }
    }
    public static DbCommand CreateCommand(string commandText, DbConnection connection, CommandType commandType = CommandType.Text)
    {
        if (connection == null) { throw new ArgumentNullException($"COMMAND_CONEXAO_NULL"); }
        if (string.IsNullOrWhiteSpace(commandText)) { throw new ArgumentException($"COMMAND_TEXT_VAZIO"); }
        try
        {
            var command = connection.CreateCommand() ?? throw new Exception("FALHA_CRIAR_COMANDO");
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.CommandTimeout = 15;
            return command;
        }
        catch (DbException ex) { throw new Exception($"FALHA_CRIAR_COMANDO", ex); }
    }
    public static DbParameter CreateParameter(string name, object value, DbType dbType)
    {
        if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException($"PARAMETER_VAZIO"); }
        try
        {
            DbParameter parameter = new SqliteParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                DbType = dbType
            };
            return parameter;
        }
        catch (DbException ex) { throw new Exception($"ERRO_CRIAR_PARAMETRO", ex); }
    }
}