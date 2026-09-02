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
        SELECT id, titular_id, saldo 
        FROM contabancaria
        """;

        using var reader = busca.ExecuteReader();

        while(reader.Read()){
            var id = reader.GetInt32(0);
            var titularId = reader.GetString(1);
            var saldo = reader.GetDouble(2);

            var c = new ContaBancaria{Titular = ClienteService.Buscar(TitularId), Saldo = saldo};
            c.Id = id;

            Contas.Add(c);
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