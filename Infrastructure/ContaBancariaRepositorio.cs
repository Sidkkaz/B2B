class ContaBancariaRepositorio : IRepositorio<ContaBancaria>{

    private readonly string _connectedString = "Data Source = app.db";
    
    static void Add(ContaBancaria c){
        
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var criarConta = connection.CreateCommand();

        criarConta.CommandText = "insert into contabancaria (titular_id, saldo) values (@cpf, 0)";
        criarConta.Parameters.AddWithValue("@cpf", titular.CPF);

        criarConta.ExecuteNonQuery();
    }

    static void Remove(ContaBancaria c){

    }

    static List<ContaBancaria> Query(){

    }

    static void Update(ContaBancaria c){
        
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "UPDATE contabancaria SET saldo = @saldo WHERE titular_id = @cpf";

        command.Parameters.AddWithValue("@saldo", c.Saldo);
        command.Parameters.AddWithValue("@cpf", c.Titular.CPF);

        command.ExecuteNonQuery();

    } 
}