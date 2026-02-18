using B2B.Domain;
using B2B.Infrastructure;

namespace B2B.UI;

class Menus
{
    static void Output(string t){
        Console.WriteLine($"{t}");
    }
    
    public static async Task TypeWrite(string texto, int delay = 40)
    {
        foreach (char c in texto)
        {
            Console.Write(c);
            await Task.Delay(delay);
        }
    }
   
    public static async Task EntradaAnim()
    {
        Console.Clear();    
        await TypeWrite("\n-----------------------------\n");
        await Task.Delay(1000);
        
        await TypeWrite("\nO seu incrivel, sla oq é isso\n");
        await Task.Delay(1000);
        
        await TypeWrite("\n------------------------------\n");
        await Task.Delay(3000);
        
        Console.Clear();
        
    }
    
    public static void MenuPrincipal(Cliente c, ContaBancaria b, DB DB){
        
        
        while(true)
        {
            int op = -1;
            double valor;

            Console.Clear();
            
            Console.WriteLine("1 - Ver Saldo\n");
            Console.WriteLine("2 - Depositar\n");
            Console.WriteLine("3 - Sacar\n");
            Console.WriteLine("4 - Dados Pessoais\n");
            Console.WriteLine("0 - Sair\n");
            Console.WriteLine("__________________\n");
            Console.WriteLine("Opcao: ");
            
            try{
                op = ConsoleIO.InputI();
            }catch(Exception ex){
                Console.WriteLine("Erro: " + ex.Message);
            }
            
            switch(op)
            {
                
                case 1:
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine($"Saldo Atual: {b.Saldo:C}");

                break;  
                
                case 2:
                try 
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("Valor a Depositar: ");
                    valor = ConsoleIO.InputD();
                    b.Depositar(valor);
                    DB.AtualizarSaldo(c, valor);
                    Output("\nValor Depositado!"); 

                }catch(Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                break;
                
                case 3:
                try
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("Valor a Sacar: ");
                    valor = ConsoleIO.InputD();
                    b.Sacar(valor);
                    DB.AtualizarSaldo(c, valor);
                    Output("\nSaque Concluido!"); 
                
                }catch(Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
                break;
                
                case 4:
                Console.Clear();
                Output("Titular: " + b.Titular.Nome);
                Output("\nCPF: " + b.Titular.CPFFormatado());
                Output($"\nSaldo: {b.Saldo:C}"); 
                break;

                case 0:
                return;
                
                default: Console.WriteLine("Tente Novamente!");
                break;
            }
            
            Console.WriteLine("\n");
            Console.WriteLine("Aperte algum botao para continuar");
            Console.ReadKey();
        }
        
    }
    
    public static void MenuEntrada(){

        Console.Clear();
        Output("              É um Prazer ve-lo aqui");
        Output("\nPara que possamos prosseguir, digite cpf abaixo: ");
    }
    
    
}