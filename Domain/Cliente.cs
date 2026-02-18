namespace B2B.Domain;

public class Cliente
{
    public string Nome {get;}
    public string CPF {get;}

    public Cliente(string nome, string cpf)
    {
        if(string.IsNullOrWhiteSpace(nome)) throw new Exception("Nome Invalido");
        Nome = nome;


        if(string.IsNullOrWhiteSpace(cpf))
        throw new Exception("CPF Invalido");
            
        string cpflimpo = cpf.Replace(".", "").Replace("-", "");
            
        if(cpflimpo.Length != 11) throw new Exception("Cpf Invalido");

        foreach(Char c in cpflimpo){
            if (!char.IsDigit(c)) 
                throw new Exception("CPF Invalido");
        }

        CPF = cpflimpo;
    }

    public string CPFFormatado()
    {
        return CPF
        .Insert(9, "-")
        .Insert(6, ".")
        .Insert(3,".");
    }

}