class AuthService
{
    public static bool Login()
    {
        string cpf;

        Output("Coloque Seu CPF: ");
        try
        {
            cpf = Cliente.CPFLimpo(InputS());
        }
        catch
        {
            throw new Exception("Tentativa Invalida");
        }

        if(cpf.Length == 11)
        {
            
            if (!ClienteService.ListarClientes().Where(x => c.CPF == cpf))
            {
                CompletarCadastro(cpf);
                return true;
            }

            if(ClienteService.ListarClientes().Where(x => c.CPF == cpf)){
                return true;
            }

        }
        else
        {
            throw new Exception("Cpf Invalido");
        }

        return false;
        
    }

    public static void CompletarCadastro(cpf)
    {
        Output("Vi que voce ainda nao tem cadastro!\n");
        Output("Mas boas noticias! So preciso de um dado para finalizar seu cadastro.\n");
        Output("Me envie seu nome completo, pfv: ");
            
            try
            {
                var nome = InputS();

                if(string.IsNullOrWhiteSpace(nome))
                    throw new Exception("Nome Invalido");

                if(nome.Length < 3)
                    throw new Exception("Nome Invalido");

                var c = new Cliente{Nome = nome, CPF = cpf}
                ClienteService.AdicionarCliente(c);
                ContaBancariaService.AdicionarConta(new ContaBancaria{c, 0});

            }
            catch
            {
                throw new Exception("Tentativa Invalida");
            }
    }


}