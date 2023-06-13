using AtividadeAvaliativa.Database;
using AtividadeAvaliativa.Models;
using Microsoft.Data.Sqlite;
namespace AtividadeAvaliativa.Repositories;

class ItensPedidosRepository {
    private readonly DatabaseConfig _databaseConfig;
    public ItensPedidosRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }


    public List<ItensPedidosRepository> Listar()
    {
        var itensPedidos = new List<Clientes>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM ItensPedidos";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var coditempedido = reader.GetInt32(0);
            var itempedidocodpedido = reader.GetString(1);
            var itempedidocodproduto = reader.GetString(2);
            var quantidade = reader.GetString(3);            
            var itensPedido =  ReaderToItensPedido(reader);
            itensPedidos.Add(itensPedido);
        }


    public Clientes Inserir(Clientes clientes)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Clientes VALUES($coditempedido, $itempedidocodpedido, $itempedidocodproduto, $quantidade)";
        command.Parameters.AddWithValue("$coditempedido", clientes.CodCliente);
        command.Parameters.AddWithValue("$itempedidocodpedido", clientes.Nome);
        command.Parameters.AddWithValue("$itempedidocodproduto", clientes.Endereco);
        command.Parameters.AddWithValue("$quantidade", clientes.Cidade);
       

        command.ExecuteNonQuery();
        connection.Close();

        return clientes;
    }

    private ItensPedidos ReaderToItensPedidos(SqliteDataReader reader)
    {
        var ItemPedido = new ItensPedidos(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));

        return ItemPedido;
    }

}