using B2B.Domain;
using Microsoft.Data.Sqlite;

namespace B2B.Infrastructure;
    
    class ClienteRepositorio : IRepositorio<Cliente>{

        private const string _connectedString = "Data Source = app.db";
        
        public void Add(Cliente c){

            using var connection = new SqliteConnection(_connectedString);
            connection.Open();

            var insert = connection.CreateCommand();

            insert.CommandText = """
                INSERT INTO cliente (nome, cpf) 
                VALUES (@nome, @cpf)
                """;

            insert.Parameters.AddWithValue("@nome", c.Nome);
            insert.Parameters.AddWithValue("@cpf", c.CPF);

            insert.ExecuteNonQuery();
        }

        public void Remove(Cliente c){
            using var connection = new SqliteConnection(_connectedString);
            connection.Open();

            var sql = connection.CreateCommand();

            sql.CommandText = """
                DELETE FROM cliente 
                WHERE id = @id
                """;

            sql.Parameters.AddWithValue("@id", c.Id);

            sql.ExecuteNonQuery();
            
        }

        public List<Cliente> Query(){
            using var connection = new SqliteConnection(_connectedString);
            connection.Open();

            var clientes = new List<Cliente>();

            var busca = connection.CreateCommand();

            busca.CommandText = """
                SELECT id, nome, cpf 
                FROM cliente
                """;

            using var reader = busca.ExecuteReader();

            while(reader.Read()){
                var id = reader.GetInt32(0);
                var nome = reader.GetString(1);
                var cpf = reader.GetString(2);

                var c = new Cliente(nome, cpf);
                c.Id = id;

                clientes.Add(c);
            }

            return clientes;
        }
    }