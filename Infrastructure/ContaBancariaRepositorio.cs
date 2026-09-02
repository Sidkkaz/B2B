using B2B.Domain;
using Microsoft.Data.Sqlite;
using B2B.Service;

namespace B2B.Infrastructure;

class ContaBancariaRepositorio : IRepositorioUpdate<ContaBancaria>{

    private const string _connectedString = "Data Source = app.db";
    
    public void Add(ContaBancaria c){
        
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var criarConta = connection.CreateCommand();

        criarConta.CommandText = """
            INSERT INTO contabancaria 
            (titular_id, saldo)
            VALUES (@titular_id, @saldo)
        """;

        criarConta.Parameters.AddWithValue("@titular_id", c.Titular.CPF);
        criarConta.Parameters.AddWithValue("@saldo", c.Saldo);

        criarConta.ExecuteNonQuery();
    }

    public void Remove(ContaBancaria c){
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var sql = connection.CreateCommand();

        sql.CommandText = "DELETE FROM contabancaria WHERE id = @id";
        sql.Parameters.AddWithValue("@id", c.Id);

        sql.ExecuteNonQuery();

    }

    public List<ContaBancaria> Query(){
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var Contas = new List<ContaBancaria>();

        var busca = connection.CreateCommand();

        busca.CommandText = """
        SELECT
        cb.id,
        cb.saldo,
        c.id,
        c.nome,
        c.cpf
        FROM contabancaria cb
        JOIN cliente c ON c.cpf = cb.titular_id
        """;

        using var reader = busca.ExecuteReader();

        while(reader.Read()){
            var contaId = reader.GetInt32(0);
            var saldo = reader.GetDouble(1);

            var clienteId = reader.GetInt32(2);
            var nome = reader.GetString(3);
            var cpf = reader.GetString(4);

            var cliente = new Cliente(nome, cpf);
            cliente.Id = clienteId;

            var conta = new ContaBancaria(cliente, saldo);
            conta.Id = contaId;

            Contas.Add(conta);
        }

        return Contas;
    }

    public void Update(ContaBancaria c){
        
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            UPDATE contabancaria 
            SET saldo = @saldo 
            WHERE id = @id
            """;

        command.Parameters.AddWithValue("@saldo", c.Saldo);
        command.Parameters.AddWithValue("@id", c.Id);

        command.ExecuteNonQuery();

    } 
}