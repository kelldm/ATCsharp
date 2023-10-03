using ATCsharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Program
{
    private static List<Conta> contas = new List<Conta>();

    static void Main()
    {
        CarregarContasDoArquivo();

        while (true)
        {
            //Menu Principal

            Console.WriteLine("Menu:");
            Console.WriteLine("[1] Inclusão de Conta");
            Console.WriteLine("[2] Alteração de Saldo");
            Console.WriteLine("[3] Exclusão de Conta");
            Console.WriteLine("[4] Relatórios Gerenciais");
            Console.WriteLine("[5] Saída do Programa");

            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        IncluirConta();
                        break;
                    case 2:
                        AlterarSaldo();
                        break;
                    case 3:
                        ExcluirConta();
                        break;
                    case 4:
                        RelatoriosGerenciais();
                        break;
                    case 5:
                        SalvarContasNoArquivo();
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }

    private static void IncluirConta()
    {
        Console.Write("Número da Conta: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
        {
            Console.WriteLine("Número da conta inválido.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome) || nome.Split(' ').Length < 2)
        {
            Console.WriteLine("Deve conter pelo menos nome e sobrenome.");
            return;
        }

        Console.Write("Saldo Inicial: ");
        double saldo;
        if (!double.TryParse(Console.ReadLine(), out saldo) || saldo < 0)
        {
            Console.WriteLine("Saldo inválido.");
            return;
        }

        if (contas.Any(c => c.Id == id))
        {
            Console.WriteLine("Já existe uma conta com este número.");
            return;
        }

        contas.Add(new Conta(id, nome, saldo));
        Console.WriteLine("Conta adicionada com sucesso.");
    }

    private static void AlterarSaldo()
    {
        if (contas.Count == 0)
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        Console.Write("Número da Conta: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Número da conta inválido.");
            return;
        }

        Conta conta = contas.FirstOrDefault(c => c.Id == id);

        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.Write("Valor para crédito/débito: ");
        double valor;
        if (!double.TryParse(Console.ReadLine(), out valor) || valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        Console.WriteLine("[1] Crédito");
        Console.WriteLine("[2] Débito");

        int opcao;
        if (int.TryParse(Console.ReadLine(), out opcao))
        {
            switch (opcao)
            {
                case 1:
                    conta.Creditar(valor);
                    Console.WriteLine("Crédito realizado com sucesso.");
                    break;
                case 2:
                    try
                    {
                        conta.Debitar(valor);
                        Console.WriteLine("Débito realizado com sucesso.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    private static void ExcluirConta()
    {
        if (contas.Count == 0)
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        Console.Write("Número da Conta: ");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Número da conta inválido.");
            return;
        }

        Conta conta = contas.FirstOrDefault(c => c.Id == id);

        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        if (conta.Saldo != 0)
        {
            Console.WriteLine("Não é possível excluir uma conta com saldo diferente de zero.");
            return;
        }

        contas.Remove(conta);
        Console.WriteLine("Conta excluída com sucesso.");
    }

    private static void RelatoriosGerenciais()
    {
        if (contas.Count == 0)
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        Console.WriteLine("Relatórios Gerenciais:");
        Console.WriteLine("[1] Listar clientes com saldo negativo");
        Console.WriteLine("[2] Listar clientes com saldo acima de um valor");
        Console.WriteLine("[3] Listar todas as contas");

        int opcao;
        if (int.TryParse(Console.ReadLine(), out opcao))
        {
            switch (opcao)
            {
                case 1:
                    ListarClientesComSaldoNegativo();
                    break;
                case 2:
                    ListarClientesComSaldoAcimaDeUmValor();
                    break;
                case 3:
                    ListarTodasAsContas();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    private static void ListarClientesComSaldoNegativo()
    {
        var clientesComSaldoNegativo = contas.Where(c => c.Saldo < 0).ToList();

        if (clientesComSaldoNegativo.Count > 0)
        {
            Console.WriteLine("Clientes com saldo negativo:");
            foreach (var cliente in clientesComSaldoNegativo)
            {
                Console.WriteLine(cliente);
            }
        }
        else
        {
            Console.WriteLine("Nenhum cliente com saldo negativo encontrado.");
        }
    }

    private static void ListarClientesComSaldoAcimaDeUmValor()
    {
        Console.Write("Informe o valor mínimo de saldo: ");
        double valorMinimo;
        if (double.TryParse(Console.ReadLine(), out valorMinimo) && valorMinimo >= 0)
        {
            var clientesComSaldoAcimaDoValor = contas.Where(c => c.Saldo >= valorMinimo).ToList();

            if (clientesComSaldoAcimaDoValor.Count > 0)
            {
                Console.WriteLine($"Clientes com saldo acima de {valorMinimo:0.00}:");
                foreach (var cliente in clientesComSaldoAcimaDoValor)
                {
                    Console.WriteLine(cliente);
                }
            }
            else
            {
                Console.WriteLine($"Nenhum cliente com saldo acima de {valorMinimo:0.00} encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Valor mínimo inválido.");
        }
    }

    private static void ListarTodasAsContas()
    {
        Console.WriteLine("Todas as contas:");
        foreach (var conta in contas)
        {
            Console.WriteLine(conta);
        }
    }

    private static void CarregarContasDoArquivo()
    {
        try
        {
            if (File.Exists("contas.csv"))
            {
                string[] linhas = File.ReadAllLines("contas.csv");
                foreach (string linha in linhas.Skip(1))
                {
                    string[] campos = linha.Split(',');
                    if (campos.Length == 3 &&
                        int.TryParse(campos[0], out int id) &&
                        double.TryParse(campos[2], out double saldo))
                    {
                        contas.Add(new Conta(id, campos[1], saldo));
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Erro ao carregar contas do arquivo: " + ex.Message);
        }
    }

    private static void SalvarContasNoArquivo()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("contas.csv"))
            {
                writer.WriteLine("Número da Conta, Nome, Saldo");
                foreach (var conta in contas)
                {
                    writer.WriteLine($"{conta.Id}, {conta.Nome}, {conta.Saldo:0.00}");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Erro ao salvar contas no arquivo: " + ex.Message);
        }
    }
}
