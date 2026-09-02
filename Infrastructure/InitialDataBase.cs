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
        nome TEXT NOT NULL,
        cpf TEXT NOT NULL UNIQUE
        )";

        createTable.ExecuteNonQuery();

        var criarcliente = connection.CreateCommand();
        criarcliente.CommandText = @" CREATE TABLE IF NOT EXISTS contabancaria(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        titular_id TEXT NOT NULL,
        saldo REAL NOT NULL DEFAULT 0,
        FOREIGN KEY (titular_id) REFERENCES cliente(cpf)
        )";

        criarcliente.ExecuteNonQuery();
    }

}