class ContaBancariaService{

    IRepositorio repo = new ContaBancariaRepositorio(); 

    public static void AdicionarConta(ContaBancaria c){
        repo.Add(c);
    }

    public static List<ContaBancaria> ListarContas(){
        return repo.Query();
    }

    public static void AtualizarSaldo(ContaBancaria c){
        if(c.Saldo < 0) return;
        repo.Update(c);
    }

    public static void RemoverConta(ContaBancaria c){
        repo.Remove(c)
    }
    
}