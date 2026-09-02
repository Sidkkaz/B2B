class ClienteSerivce{

    IRepositorio repo = new ClienteRepositorio(); 

    public static void AdicionarConta(Cliente c){
        repo.Add(c);
    }

    public static List<ContaBancaria> ListarContas(){
        return repo.Query();
    }

    public static void AtualizarSaldo(Cliente c){
        repo.Update(c);
    }

    public static void RemoverConta(Cliente c){
        repo.Remove(c)
    }
    
}