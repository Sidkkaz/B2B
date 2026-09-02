    class ClienteRepositorio : IRepositorio<Cliente>{

        private const string _connectedString = "Data Source = app.db";
        
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
            using var connection = new SqliteConnection(_connectedString);
            connection.Open();

            var sql = connection.CreateCommand();

            sql.CommandText = "Delete FROM cliente WHERE cpf = @cpf";
            sql.Parameters.AddWithValue("@cpf", c.CPF);

            sql.ExecuteNonQuery();
            
        }

        static List<Cliente> Query(){
            using var connection = new SqliteConnection(_connectedString);
            connection.Open();

            var clientes = new List<Clientes>();

            var busca = connection.CreateCommand();

            busca.CommandText = "Select id, nome, cpf FROM cliente";

            using var reader = busca.ExecuteReader();

            while(reader.Read()){
                var id = reader.GetInt32(0);
                var nome = reader.GetString(1);
                var cpf = reader.GetString(2);

                var c = new Cliente{Nome = nome, CPF = cpf};
                c.Id = id;

                clientes.Add(c);
            }

            return clientes;
        }

        private void Update(ContaBancaria c);
    }