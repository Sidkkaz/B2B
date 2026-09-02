class ClienteSerivce{

    IRepositorio repo = new ClienteRepositorio(); 

    public static void AdicionarCliente(Cliente c){
        repo.Add(c);
    }

    public static List<Cliente> ListarClientes(){
        return repo.Query();
    }

    public static void RemoverCliente(Cliente c){
        repo.Remove(c)
    }

    public static Cliente Busca(string cpf){
        List<Cliente> clientes = ListarClientes();
        return clientes.Where(x => x.CPF == cpf).ToList();
    }
    
}