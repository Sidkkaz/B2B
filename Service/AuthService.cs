using B2B.Infrastructure;
using B2B.Domain;
using B2B.UI;


namespace B2B.Service;

public class AuthService
{
    public Cliente c;

    public static bool Login()
    {
        string cpf;

        ConsoleIO.Output("Coloque Seu CPF: ");
        try
        {
            cpf = Cliente.CPFLimpo(ConsoleIO.InputS());
        }
        catch
        {
            throw new Exception("Tentativa Invalida");
        }

        if(cpf.Length == 11)
        {
            
            if (!ClienteService.ListarClientes().FistOrDefault(x => c.CPF == cpf))
            {
                CompletarCadastro(cpf);
                return true;
            }

            if(ClienteService.ListarClientes().FirstOrDefault(x => c.CPF == cpf)){
                return true;
            }

        }
        else
        {
            throw new Exception("Cpf Invalido");
        }

        return false;
        
    }

    public static void CompletarCadastro(string cpf)
    {
        ConsoleIO.Output("Vi que voce ainda nao tem cadastro!\n");
        ConsoleIO.Output("Mas boas noticias! So preciso de um dado para finalizar seu cadastro.\n");
        ConsoleIO.Output("Me envie seu nome completo, pfv: ");
            
            try
            {
                var nome = ConsoleIO.InputS();

                if(string.IsNullOrWhiteSpace(nome))
                    throw new Exception("Nome Invalido");

                if(nome.Length < 3)
                    throw new Exception("Nome Invalido");

                c = new Cliente{Nome = nome, CPF = cpf};
                ClienteService.AdicionarCliente(c);
                ContaBancariaService.AdicionarConta(new ContaBancaria{c, 0});

            }
            catch
            {
                throw new Exception("Tentativa Invalida");
            }
    }
}