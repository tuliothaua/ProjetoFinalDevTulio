using ProjetoFinalDevTulio.domain.Enums;
using ProjetoFinalDevTulio.domain.Interfaces;
using ProjetoFinalDevTulio.Infrastructure.Data;
using ProjetoFinalDevTulio.domain.Entities;
using System.Data;
using System.Data.Common;
namespace ProjetoFinalDevTulio.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository, IAsyncDisposable
{
    private DbConnection? _connection;
    private bool _disposed = false;
    public TarefaRepository()
    {
        CriarTabela();
        // Finalizador, destrutor, para garantir que os recursos sejam liberados - é chamado pelo Garbage Collector (GC) do .NET quando o objeto está sendo coletado.
        DisposeAsync(false).AsTask().GetAwaiter().GetResult();
    }
    public async Task<bool> CriarTabela()
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"CREATE TABLE IF NOT EXISTS tb_tarefa (" +
            $"id_tarefa INTEGER PRIMARY KEY AUTOINCREMENT, " +

            $"titulo TEXT NOT NULL, " +
            $"descricao TEXT NOT NULL, " +
            $"status INTEGER NOT NULL, " +
            $"tipo INTEGER NOT NULL, " +
            $"data_criacao DATETIME NOT NULL, " +
            $"data_limite DATETIME NOT NULL );";

            await using var command = DbProvider.CreateCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_ADD_Tarefa", ex); }
    }
    public async Task<DbConnection> GetOpenConnectionAsync()
    {
        try
        {
            if (_connection == null)
            {
                _connection = DbProvider.CreateConnection();
                await _connection.OpenAsync();
            }
            else if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
            return _connection;
        }
        catch (DbException ex) { throw new InvalidOperationException($"FALHA_ABRIR_CONEXAO", ex); }
    }
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    public async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing && _connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
            _disposed = true;
        }
    }


    public async Task<IEnumerable<TarefaItem>> ObterTodos()
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"SELECT * FROM tb_tarefa";
            await using var command = DbProvider.CreateCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();
            var entities = new List<TarefaItem>();
            while (await reader.ReadAsync())
            {
                entities.Add(await MapAsync(reader));
            }
            return entities;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_OBTER_DADOS_TODOS", ex); }
    }
    public async Task<TarefaItem?> ObterPorId(int id)
    {
        if (id <= 0) { throw new ArgumentException("ID_NAO_INFORMADO_MENOR_UM", nameof(id)); }
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"SELECT * FROM tb_tarefa WHERE id_tarefa = @Id";
            await using var command = DbProvider.CreateCommand(query, connection);
            command.Parameters.Add(DbProvider.CreateParameter("@Id", id, DbType.Int32));
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return await MapAsync(reader);
            }
            return null;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_OBTER_DADOS_ID_{id}", ex); }
    }
    public async Task<TarefaItem> Adicionar(TarefaItem entity)
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"INSERT INTO tb_tarefa (titulo, descricao, status, tipo, data_criacao, data_limite) "
            + "VALUES (@Titulo, @Descricao, @Status, @Tipo, @DataCriacao, @DataLimite) "
            + "RETURNING id_tarefa;";
            await using var command = DbProvider.CreateCommand(query, connection);
            command.Parameters.Add(DbProvider.CreateParameter("@Titulo", entity.Titulo, DbType.String));
            command.Parameters.Add(DbProvider.CreateParameter("@Descricao", entity.Descricao, DbType.String));
            command.Parameters.Add(DbProvider.CreateParameter("@Status", (int)entity.Status, DbType.Int32));
            command.Parameters.Add(DbProvider.CreateParameter("@Tipo", (int)entity.Tipo, DbType.Int32));
            command.Parameters.Add(DbProvider.CreateParameter("@DataCriacao", entity.DataCriacao, DbType.DateTime));
            command.Parameters.Add(DbProvider.CreateParameter("@DataLimite", entity.DataLimite, DbType.DateTime));
            var id = await command.ExecuteScalarAsync();
            if (id != null && id != DBNull.Value)
            {
                // Usando reflexão para definir o ID, já que a propriedade Id é herdada e não tem setor público

                var idProperty = typeof(Entity).GetProperty("Id");
                idProperty?.SetValue(entity, Convert.ToInt32(id));
            }
            return entity;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_ADD_Tarefa", ex); }
    }
    public async Task<TarefaItem> Atualizar(TarefaItem entity)
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"UPDATE tb_tarefa "
            + "SET titulo = @Titulo, "
            + "descricao = @Descricao, "
            + "status = @Status, "
            + "tipo = @Tipo, "
            + "data_criacao = @DataCriacao, "
            + "data_limite = @DataLimite "
            + "WHERE id_tarefa = @Id";
            await using var command = DbProvider.CreateCommand(query, connection);
            command.Parameters.Add(DbProvider.CreateParameter("@Id", entity.Id, DbType.Int32));
            command.Parameters.Add(DbProvider.CreateParameter("@Titulo", entity.Titulo, DbType.String));
            command.Parameters.Add(DbProvider.CreateParameter("@Descricao", entity.Descricao, DbType.String));
            command.Parameters.Add(DbProvider.CreateParameter("@Status", (int)entity.Status, DbType.Int32));
            command.Parameters.Add(DbProvider.CreateParameter("@Tipo", (int)entity.Tipo, DbType.Int32));
            command.Parameters.Add(DbProvider.CreateParameter("@DataCriacao", entity.DataCriacao, DbType.DateTime));
            command.Parameters.Add(DbProvider.CreateParameter("@DataLimite", entity.DataLimite, DbType.DateTime));
            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Tarefa_NAO_LOCALIZADO_ID_{entity.Id}");
            }
            return entity;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_UPDATE_Tarefa", ex); }
    }
    public async Task<bool> Excluir(int id)
    {
        if (id <= 0) { throw new ArgumentException("ID_NAO_INFORMADO_MENOR_UM", nameof(id)); }
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            string query = $"DELETE FROM tb_tarefa WHERE id_tarefa = @Id";
            await using var command = DbProvider.CreateCommand(query, connection);
            command.Parameters.Add(DbProvider.CreateParameter("@Id", id, DbType.Int32));
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
        catch (DbException ex) { throw new InvalidOperationException($"ERRO_REMOVER_ID_{id}", ex); }
    }
    public async Task<TarefaItem> MapAsync(DbDataReader reader)
    {
        try
        {
            var Tarefa = TarefaItem.Criar(
            id: Convert.ToInt32(reader["id_tarefa"]),
            titulo: reader["titulo"]?.ToString()!,
            descricao: reader["descricao"]?.ToString()!,
            status: (ETarefaStatus)Convert.ToInt32(reader["status"].ToString()),
            tipo: (ETarefaTipo)Convert.ToInt32(reader["tipo"].ToString()),
            dataCriacao: Convert.ToDateTime(reader["data_criacao"]),
            dataLimite: Convert.ToDateTime(reader["data_limite"]));
            return Tarefa;
        }
        catch (DbException ex) { throw new InvalidOperationException($"Erro ao mapear dados do Tarefa: {ex.Message}", ex); }
    }
}