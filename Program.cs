using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Data;
using B2B.UI;
using B2B.Domain;
using B2B.Infrastructure;
using static B2B.UI.ConsoleIO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

class Programa 
{

    static void Main()
    {
        var DB = new DB();

        Menus.MenuEntrada();

        if(Entrada.Auth())
        {
            Menus.MenuPrincipal();
        }
        
    }
}

class Entrada
{
    public static bool Auth()
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

                    ClienteService.AdicionarCliente(new Cliente{Nome = nome, CPF = cpf});
                    ContaBancariaService.AdicionarConta(new ContaBancaria{
                        new Cliente{Nome = nome, CPF = cpf}, 0
                    });

                    return true;
                }
                catch
                {
                    throw new Exception("Tentativa Invalida");
                }

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
}