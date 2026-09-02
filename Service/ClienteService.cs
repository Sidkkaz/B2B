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
    
}