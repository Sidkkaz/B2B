using System.Runtime.CompilerServices;
using B2B.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace B2B.Infrastructure;
public class DB
{
    private readonly string _connectedString = "Data Source = app.db";

    public DB()
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

}
    
