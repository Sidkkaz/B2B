using B2B.Infrastructure;
using B2B.Domain;
using static B2B.UI.ConsoleIO;

namespace B2B.Service;

public class ClienteService{

    private IRepositorio<Cliente> repo = new ClienteRepositorio();
    private ContaBancariaService ContaService = new ContaBancariaService();

    public void AdicionarCliente(Cliente c){
        repo.Add(c);
    }

    public List<Cliente> ListarClientes(){
        return repo.Query();
    }

    public void RemoverCliente(Cliente c){
        repo.Remove(c);
    }

    public Cliente? Buscar(string cpf)
    {
        return ListarClientes()
            .FirstOrDefault(x => x.CPF == cpf);
    }

    public void MostrarDados(Cliente c){
        var b = ContaService.Buscar(c.CPF);

        if(b == null)
            throw new Exception("Conta não encontrada");

        Output("Titular: " + c.Nome);
        Output("\nCPF: " + c.CPFFormatado());
        Output($"\nSaldo: {b.Saldo:C}"); 
    }
    
}