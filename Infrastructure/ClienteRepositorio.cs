class ClienteRepositorio : IRepositorio<Cliente>{

    private readonly string _connectedString = "Data Source = app.db";
    
    static void Add(Cliente c){

        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var insert = connection.CreateCommand();

        insert.CommandText = "Insert into cliente (nome, cpf) Values (@nome, @cpf)";
        insert.Parameters.AddWithValue("@nome", cliente.Nome);
        insert.Parameters.AddWithValue("@cpf", cliente.CPF);

        insert.ExecuteNonQuery();
    }

    static void Remove(Cliente c){

    }

    static List<Cliente> Query(){
        using var connection = new SqliteConnection(_connectedString);
        connection.Open();

        var clientes = new List<Clientes>();

        var busca = connection.CreateCommand();

        busca.CommandText = "Select nome, cpf FROM cliente";

        using var reader = busca.ExecuteReader();

        if(reader.Read()){
            var nome = reader.GetString(0);
            var cpf = reader.GetString(1);

            clientes.Add(new Cliente{Nome = nome, CPF = cpf});
        }

        return clientes;
    }
}