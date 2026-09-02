using B2B.Infrastructure;
using B2B.Domain;

namespace B2B.Service;

public class ContaBancariaService{

    private IRepositorioUpdate<ContaBancaria> repo = new ContaBancariaRepositorio(); 

    public void AdicionarConta(ContaBancaria c){
        repo.Add(c);
    }

    public List<ContaBancaria> ListarContas(){
        return repo.Query();
    }

    public void AtualizarSaldo(ContaBancaria c){
        repo.Update(c);
    }

    public void RemoverConta(ContaBancaria c){
        repo.Remove(c);
    }

    public void MostrarSaldo(Cliente c){
        var conta = ListarContas().FirstOrDefault(x => x.Titular != null && x.Titular.CPF == c.CPF);

        if (conta == null)
            throw new Exception("Conta não encontrada");

        Console.WriteLine($"Saldo Atual: {conta.Saldo:C}");
    }

    public void Depositar(Cliente c, double v){
        var conta = ListarContas().FirstOrDefault(x => x.Titular.CPF == c.CPF);

        if (conta == null)
            throw new Exception("Conta não encontrada");

        conta.Depositar(v);
        AtualizarSaldo(conta);

    }

    public void Sacar(Cliente c, double v){
        var conta = ListarContas().FirstOrDefault(x => x.Titular.CPF == c.CPF);

        if (conta == null)
            throw new Exception("Conta não encontrada");

        conta.Sacar(v);
        AtualizarSaldo(conta);

    }
    
    public ContaBancaria? Buscar(String cpf){
        return ListarContas().FirstOrDefault(x => x.Titular.CPF == cpf);
    }
    
}