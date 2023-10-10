using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCsharp
{
    public class CRUD
    {

        public static void IncluirConta(List<Conta>contas)
        {
            Console.Write("Número da Conta: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Número da conta inválido.");
                return;
            }

            Console.Write("Nome e sobrenome: ");
            string nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome) || nome.Split(' ').Length < 2)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Deve conter pelo menos nome e sobrenome.");
                return;
            }

            Console.Write("Saldo Inicial: ");
            double saldo;
            if (!double.TryParse(Console.ReadLine(), out saldo) || saldo < 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Saldo inválido.");
                return;
            }

            if (contas.Any(c => c.Id == id))
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Já existe uma conta com este número.");
                return;
            }

            contas.Add(new Conta(id, nome, saldo));
            Console.WriteLine("--------------------------");
            Console.WriteLine("Conta adicionada com sucesso.");
        }

        public static void AlterarSaldo(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.Write("Número da Conta: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Número da conta inválido.");
                return;
            }

            Conta conta = contas.FirstOrDefault(c => c.Id == id);

            if (conta == null)
            {
                Console.WriteLine("--------------------------");
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
            Console.WriteLine("--------------------------");

            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        conta.Creditar(valor);
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Crédito realizado com sucesso.");
                        break;
                    case 2:
                        try
                        {
                            conta.Debitar(valor);
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Débito realizado com sucesso.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Opção inválida.");
            }
        }

        public static void ExcluirConta(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.Write("Número da Conta: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Número da conta inválido.");
                return;
            }

            Conta conta = contas.FirstOrDefault(c => c.Id == id);

            if (conta == null)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Conta não encontrada.");
                return;
            }

            if (conta.Saldo != 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Não é possível excluir uma conta com saldo diferente de zero.");
                return;
            }

            contas.Remove(conta);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Conta excluída com sucesso.");
        }

        public static void RelatoriosGerenciais(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }
            Console.WriteLine("--------------------------");
            Console.WriteLine("Relatórios Gerenciais:");
            Console.WriteLine("[1] Listar clientes com saldo negativo");
            Console.WriteLine("[2] Listar clientes com saldo acima de um valor");
            Console.WriteLine("[3] Listar todas as contas");
            Console.WriteLine("--------------------------");

            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        ListarClientesComSaldoNegativo(contas);
                        break;
                    case 2:
                        ListarClientesComSaldoAcimaDeUmValor(contas);
                        break;
                    case 3:
                        ListarTodasAsContas(contas);
                        break;
                    default:
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Opção inválida.");
            }
        }

        public static void ListarClientesComSaldoNegativo(List<Conta> contas)
        {
            var clientesComSaldoNegativo = contas.Where(c => c.Saldo < 0).ToList();

            if (clientesComSaldoNegativo.Count > 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Clientes com saldo negativo:");
                foreach (var cliente in clientesComSaldoNegativo)
                {
                    Console.WriteLine(cliente);
                    Console.WriteLine("--------------------------");
                }
            }
            else
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Nenhum cliente com saldo negativo encontrado.");
            }
        }

        public static void ListarClientesComSaldoAcimaDeUmValor(List<Conta> contas)
        {
            Console.WriteLine("--------------------------");
            Console.Write("Informe o valor mínimo de saldo: ");
            double valorMinimo;
            if (double.TryParse(Console.ReadLine(), out valorMinimo) && valorMinimo >= 0)
            {
                var clientesComSaldoAcimaDoValor = contas.Where(c => c.Saldo >= valorMinimo).ToList();

                if (clientesComSaldoAcimaDoValor.Count > 0)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"Clientes com saldo acima de {valorMinimo:0.00}:");
                    foreach (var cliente in clientesComSaldoAcimaDoValor)
                    {
                        Console.WriteLine(cliente);
                    }
                }
                else
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine($"Nenhum cliente com saldo acima de {valorMinimo:0.00} encontrado.");
                }
            }
            else
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Valor mínimo inválido.");
            }
        }

        public static void ListarTodasAsContas(List<Conta> contas)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Todas as contas:");
            foreach (var conta in contas)
            {
                Console.WriteLine(conta);
                Console.WriteLine("--------------------------");
            }
        }
    }
}
