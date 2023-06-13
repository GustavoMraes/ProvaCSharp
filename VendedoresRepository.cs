using AtividadeAvaliativa.Database;
using AtividadeAvaliativa.Models;
using Microsoft.Data.Sqlite;
namespace AtividadeAvaliativa.Repositories;

class VendedoresRepository {
    private readonly DatabaseConfig _databaseConfig;
    public VendedoresRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }



     public List<Vendedores> Listar()
    {
        var Vendedores = new List<Vendedores>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedores";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var CodVendedor = reader.GetInt32(0);
            var Nome = reader.GetString(1);
            var SalarioFixo = reader.GetDecimal(2);
            var FaixaComissao = reader.GetString(3);
           
           
            var Vendedor = ReaderToVendedores(reader);
            Vendedores.Add(Vendedor);
        }

        connection.Close();
        
        return Vendedores;
    }



    public bool Apresentar(int CodVendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Vendedores WHERE (id = $id)";
        command.Parameters.AddWithValue("$id", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }



    public Vendedores GetById(int CodVendedor)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Vendedores WHERE (id = $CodVendedor)";
        command.Parameters.AddWithValue("$CodVendedor", CodVendedor);

        var reader = command.ExecuteReader();
        reader.Read();

        var vendedor = ReaderToVendedores(reader);

        connection.Close(); 

        return vendedor;
    }










    private Vendedores ReaderToVendedores(SqliteDataReader reader)
    {
        var vendedor = new Vendedores(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));

        return vendedor;
    }





}