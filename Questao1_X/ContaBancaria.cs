using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Questao1
{
    public class ContaBancaria
    {

        public int numero { get; set; }
        public string titular { get; set; }
        public double depositoInicial { get; set; }

        public ContaBancaria(int _numero, string _titular, double _depositoInicial = 0)
        {
            this.numero = _numero;
            this.titular = _titular;
            this.depositoInicial = _depositoInicial;
        }

        public void Deposito(double quantia)
        {
            Movimentacoes(quantia, "C");
        }

        public void Saque(double quantia)
        {
            Movimentacoes(quantia, "D");
            Movimentacoes(Convert.ToDouble("3,50"), "D");
        }

        public void Movimentacoes(double quantia, string tipo)
        {
            string path = string.Format("{0}\\extrato.txt", Environment.CurrentDirectory);

            StreamWriter texto = new StreamWriter(path, true, Encoding.ASCII);
            texto.WriteLine("{0}|{1}|{2}|{3}", numero, quantia, (tipo == "C" ? "C" : "D"), DateTime.Now);
            texto.Close();

        }

        public double Saldo()
        {
            double saldo = 0;

            string path = string.Format("{0}\\extrato.txt", Environment.CurrentDirectory);

            if (File.Exists(path))
            {
                string[] linhas = File.ReadAllLines(path);

                foreach (string linha in linhas)
                {
                    if (Convert.ToInt32(linha.Split('|')[0]) == numero)
                    {
                        if (linha.Split('|')[2].ToString() == "C")
                        {
                            saldo += saldo = Convert.ToDouble(linha.Split('|')[1]);
                        }
                        else if (linha.Split('|')[2].ToString() == "D")
                        {
                            saldo -= saldo = Convert.ToDouble(linha.Split('|')[1]);
                        }
                    }

                }
            }
         
            return saldo;

        }

        public void CriarConta(ContaBancaria dados)
        {
            try
            {
                StreamWriter texto = new StreamWriter(string.Format("{0}\\contas.txt", Environment.CurrentDirectory), true, Encoding.ASCII);

                texto.Write(String.Format("Conta:{0}\n", dados.numero));
                texto.Write(String.Format("Titular:{0}\n", dados.titular));
                texto.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool ValidarConta(ContaBancaria dados)
        {
            string path = string.Format("{0}\\contas.txt", Environment.CurrentDirectory);

            if (!File.Exists(path))
            {
                StreamWriter texto = new StreamWriter(string.Format("{0}\\contas.txt", Environment.CurrentDirectory), true, Encoding.ASCII);
                texto.Close();
            }

            string[] linhas = File.ReadAllLines(path);
            bool validado = false;

            foreach (string linha in linhas)
            {
                if (linha.Split(':')[0].ToString() == "Conta")
                {
                    if (Convert.ToInt32(linha.Split(':')[1]) == dados.numero)
                    {
                        validado = true;
                    }
                }
                if (validado && linha.Split(':')[0].ToString() == "Titular")
                {
                    if (linha.Split(':')[1].ToString() == dados.titular)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
