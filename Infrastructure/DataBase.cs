using System.Runtime.CompilerServices;
using B2B.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace B2B.Infrastructure;
public class DB
{
    private readonly string _connectedString = "Data Source = app.db";

    
    public void CriarTabela()
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var pragma = connection.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();


        var createTable = connection.CreateCommand();
        createTable.CommandText = @" CREATE TABLE IF NOT EXISTS cliente(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        nome TEXT,
        cpf TEXT Unique
        )";

        createTable.ExecuteNonQuery();

        var criarcliente = connection.CreateCommand();
        criarcliente.CommandText = @" CREATE TABLE IF NOT EXISTS contabancaria(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        titular_id Text,
        saldo Real,
        FOREIGN KEY (titular_id) REFERENCES cliente(cpf)
        )";

        criarcliente.ExecuteNonQuery();
    }
    
    
    public void InserirDadosCliente(Cliente cliente)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var insert = connection.CreateCommand();

        insert.CommandText = "Insert into cliente (nome, cpf) Values (@nome, @cpf)";
        insert.Parameters.AddWithValue("@nome", cliente.Nome);
        insert.Parameters.AddWithValue("@cpf", cliente.CPF);

        insert.ExecuteNonQuery();
    }

    public void CriarContaCliente(Cliente titular)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var criarConta = connection.CreateCommand();

        criarConta.CommandText = "insert into contabancaria (titular_id, saldo) values (@cpf, 0)";
        criarConta.Parameters.AddWithValue("@cpf", titular.CPF);

        criarConta.ExecuteNonQuery();
        
    }

    public bool ClienteExiste(string cpf)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var busca = connection.CreateCommand();

        busca.CommandText = "Select cpf From cliente Where cpf = @cpf";
        busca.Parameters.AddWithValue("@cpf", cpf);

        using var reader = busca.ExecuteReader();

        return reader.Read();
    }

    public string? PuxarDados(string cpf)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var puxar = connection.CreateCommand();

        puxar.CommandText = "Select nome, cpf From cliente Where cpf = @cpf";
        puxar.Parameters.AddWithValue("@cpf", cpf);

        using var reader = puxar.ExecuteReader();

        if(reader.Read())
        {
            var nome = reader.GetString(0);
            return nome;
        }

        return null;
    }

    public void AtualizarSaldo(Cliente c, double valor)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "UPDATE contabancaria SET saldo = saldo + @valor WHERE titular_id = @cpf";

        command.Parameters.AddWithValue("@valor", valor);
        command.Parameters.AddWithValue("@cpf", c.CPF);

        command.ExecuteNonQuery();

    }

    public double BuscarSaldo(Cliente c)
    {
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "Select saldo From contabancaria Where titular_id = @cpf";
        command.Parameters.AddWithValue("@cpf", c.CPF);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var saldo = reader.GetDouble(0);
            return saldo;
        }

        return 0;
    }

}
    
