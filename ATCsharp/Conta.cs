using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCsharp
{
    public class Conta : IOperacoesConta
        {
            public int Id { get; private set; }
            public string Nome { get; private set; }
            public double Saldo { get; private set; }

            public Conta(int id, string nome, double saldo)
            {
                Id = id;
                Nome = nome;
                Saldo = saldo;
            }

            public void Creditar(double valor)
            {
                if (valor > 0)
                    Saldo += valor;
                else
                    throw new ArgumentException("Valor inválido para crédito.");
            }

            public void Debitar(double valor)
            {
                if (valor > 0 && Saldo >= valor)
                    Saldo -= valor;
                else
                    throw new ArgumentException("Valor inválido para débito ou saldo insuficiente.");
            }

            public override string ToString()
            {
                return $"Número da Conta: {Id}\nNome: {Nome}\nSaldo: {Saldo:0.00}";
            }
        }
    }

