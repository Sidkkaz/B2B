using B2B.Infrastructure;
using B2B.Domain;
using B2B.UI;

namespace B2B.Service;

public class AuthService
{
    private ClienteService _ClienteService = new ClienteService();
    private ContaBancariaService ContaService = new ContaBancariaService();

    public Cliente? ClienteAtual;

    public bool Login()
    {
        ConsoleIO.Output("Coloque Seu CPF: ");

        string cpf;

        try
        {
            cpf = Cliente.CPFLimpo(ConsoleIO.InputS());
        }
        catch
        {
            throw new Exception("Tentativa Invalida");
        }

        if (cpf.Length != 11)
            throw new Exception("Cpf Invalido");

        var cliente = _ClienteService.ListarClientes()
            .FirstOrDefault(x => x.CPF == cpf);

        if (cliente == null)
        {
            CompletarCadastro(cpf);
            return true;
        }

        ClienteAtual = cliente;
        return true;
    }

    public void CompletarCadastro(string cpf)
    {
        Console.Clear();
        ConsoleIO.Output("Vi que voce ainda nao tem cadastro!\n");
        ConsoleIO.Output("Mas boas noticias! So preciso de um dado para finalizar seu cadastro.\n");
        ConsoleIO.Output("Me envie seu nome completo, pfv: ");

        try
        {
            var nome = ConsoleIO.InputS();

            if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3)
                throw new Exception("Nome Invalido");

            ClienteAtual = new Cliente(nome, cpf);

            _ClienteService.AdicionarCliente(ClienteAtual);

            ContaService.AdicionarConta(
                new ContaBancaria(ClienteAtual, 0)
            );
        }
        catch
        {
            throw new Exception("Tentativa Invalida");
        }
    }

    public Cliente? Sessao(){
        return ClienteAtual;
    }
}