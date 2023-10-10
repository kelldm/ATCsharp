using ATCsharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ATCsharp.CRUD;
using static ATCsharp.Arquivos;



class Program
{
  
    static void Main()
    {  
        List<Conta> contas = new List<Conta>();
        CarregarContasDoArquivo(contas);

        while (true)
        {
            //Menu Principal
            Console.WriteLine("---------------------------");
            Console.WriteLine("Seja bem-vindo(a)! :)");
            Console.WriteLine("Menu:");
            Console.WriteLine("---------------------------");
            Console.WriteLine("[1] Inclusão de Conta");
            Console.WriteLine("[2] Alteração de Saldo");
            Console.WriteLine("[3] Exclusão de Conta");
            Console.WriteLine("[4] Relatórios Gerenciais");
            Console.WriteLine("[5] Saída do Programa");
            Console.WriteLine("----------------------------");

            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        IncluirConta(contas);
                        break;
                    case 2:
                        AlterarSaldo(contas);
                        break;
                    case 3:
                        ExcluirConta(contas);
                        break;
                    case 4:
                        RelatoriosGerenciais(contas);
                        break;
                    case 5:
                        SalvarContasNoArquivo(contas);
                        return;
                    default:
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Opção inválida :/ Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Opção inválida :/ Tente novamente.");
            }
        }
    }


}