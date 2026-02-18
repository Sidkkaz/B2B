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
        DB.CriarTabela();

        Menus.MenuEntrada();
        var (nome, cpf) = Entrada.ELogin(DB);
        var cliente = CriarCliente(nome!, cpf!, DB);
        var conta = CriarConta(cliente!, DB.BuscarSaldo(cliente!));
        Menus.MenuPrincipal(cliente!, conta, DB);
        
    }
    
    
    static Cliente? CriarCliente(string nome, string cpf, DB db)
    {
        if(string.IsNullOrWhiteSpace(nome))
        {
            nome = db.PuxarDados(cpf!) ?? throw new Exception("Nome Inexistente");
            var c = new Cliente(nome!, cpf!);
            return c;
        }
        else
        {
            var c = new Cliente(nome!, cpf!);
            db.InserirDadosCliente(c);
            db.CriarContaCliente(c);
            return c;
        }
        
    }
    static ContaBancaria CriarConta(Cliente titular, double saldo)
    {
        if(titular == null)
            throw new Exception("Erro de titular");
        
        ContaBancaria c = new ContaBancaria(titular, saldo);
        return c;
    }
    
}

class Entrada
{
    public static (string? nome, string? cpf) ELogin(DB dB)
    {
        string cpfLimpo = "";
        string cpf;

        Output("Coloque Seu CPF: ");
        try
        {
            cpf = InputS();
        }
        catch
        {
            throw new Exception("Tentativa Invalida");
        }

        if(string.IsNullOrWhiteSpace(cpf))
            throw new Exception("Cpf invalido");

        if(cpf.Length == 11 || cpf.Length == 14)
        {
            cpfLimpo = cpf.Replace(".", "").Replace("-","");

            if (!dB.ClienteExiste(cpfLimpo))
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
                    return (nome, cpfLimpo);
                }
                catch
                {
                    throw new Exception("Tentativa Invalida");
                }
            }

        }
        else
        {
            throw new Exception("Cpf Invalido");
        }
        

        return (null, cpfLimpo);
    }
}