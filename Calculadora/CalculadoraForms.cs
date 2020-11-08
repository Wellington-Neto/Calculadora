using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Operacoes;




namespace Calculadora
{
    public partial class CalculadoraForms : Form
    {
        string[] expressao = new string[3] { "0", "0", "0" };



        public CalculadoraForms()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(CalculadoraForms_KeyDown);
            this.visorInferior.ReadOnly = true;
            
            
        }

     
        private void CalculadoraForms_Load(object sender, EventArgs e)
        {

        }

        private void Calcular(string simbolo)
        {
            double resultado = 0d;
            switch (simbolo)
            {
                case "=":
                    switch (expressao[1])
                    {
                        case "+":
                            resultado = Operacao.somar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                            visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                            visorInferior.Text = resultado.ToString();
                            expressao = new string[3] { resultado.ToString(), "1", "0" };
                            break;
                        case "-":
                            resultado = Operacao.subtrair(double.Parse(expressao[0]), double.Parse(expressao[2]));
                            visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                            visorInferior.Text = resultado.ToString();
                            expressao = new string[3] { resultado.ToString(), "1", "0" };
                            break;
                        case "x":
                            resultado = Operacao.multiplicar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                            visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                            visorInferior.Text = resultado.ToString();
                            expressao = new string[3] { resultado.ToString(), "1", "0" };
                            break;
                        case "÷":
                            resultado = Operacao.dividir(double.Parse(expressao[0]), double.Parse(expressao[2]));
                            visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                            visorInferior.Text = resultado.ToString();
                            expressao = new string[3] { resultado.ToString(), "1", "0" };
                            break;
                        case "^":
                            resultado = Operacao.potencia(double.Parse(expressao[0]), double.Parse(expressao[2]));
                            visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                            visorInferior.Text = resultado.ToString();
                            expressao = new string[3] { resultado.ToString(), "1", "0" };
                            break;                       
                    }
                    break;
                case "√":

                    if (expressao[1] != "0")
                    {
                        resultado = Operacao.raiz(double.Parse(expressao[2]));
                        visorSuperior.Text = expressao[0] + " " + expressao[1] + " √(" + expressao[2] + ") ";
                        string casas = Convert.ToInt32(resultado).ToString();
                        expressao[2] = Math.Round(resultado, (13 - casas.Length - 1)).ToString();
                        visorInferior.Text = expressao[2];
                    }
                    else
                    {
                        resultado = Operacao.raiz(double.Parse(expressao[0]));
                        visorSuperior.Text = " √(" + expressao[0] + ") ";
                        string casas = Convert.ToInt32(resultado).ToString();
                        expressao[1] = Math.Round(resultado, (13 - casas.Length - 1)).ToString();
                        visorInferior.Text = expressao[1];                                              
                    }
                    break;
            }
        }


    

       
        
        private void Numeros(char numero)
        {
            int escolha = 0;
            if (expressao[1] == "1") expressao = new string[3] { "0", "0", "0" };
            if (expressao[1] != "0") escolha = 2;
            if (numero >= '0' && numero <= '9')
            {
                if (expressao[escolha] == "0") expressao[escolha] = "";
                expressao[escolha] += numero;
            }
            visorInferior.Text = expressao[escolha];
        }
        private void Simbolos(string simbolo)
        {            
            expressao[1] = simbolo.ToString();
            visorSuperior.Text = expressao[0] + " " + expressao[1];
        }

        private void Opcoes(string opcao)
        {
            int escolha = 0;
            if (expressao[1] != "0") escolha = 2;
            
            string alterar = expressao[escolha];

            switch (opcao)
            {
                case "BS":
                    if (expressao[escolha] != "0")
                    {
                        expressao[escolha] = expressao[escolha].Remove(expressao[escolha].Length - 1, 1);
                    }
                    if (expressao[escolha] == "") expressao[escolha] = "0";
                    visorInferior.Text = expressao[escolha];
                    break;
                case "C":
                    this.expressao = new string[3] { "0", "0", "0" };
                    visorSuperior.Text = expressao[0];
                    visorInferior.Text = expressao[0];
                    break;
                case "CE":
                    expressao[escolha] = "0";
                    visorInferior.Text = expressao[escolha];
                    break;
                case "off":
                    this.Close();
                    break;
                default:
                    break;
            }
        }



        private void CalculadoraForms_KeyDown(object sender, KeyEventArgs e)
        {
            //Botão Igual
            if (e.KeyCode == Keys.Enter)
            {
                botaoIgual.PerformClick();
            }
            //Botão 0
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                botao0.PerformClick();
            }
            //Botão 1
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                botao1.PerformClick();
            }
            //Botão 2
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                botao2.PerformClick();
            }
            //Botão 3
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                botao3.PerformClick();
            }
            //Botão 4
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                botao4.PerformClick();
            }
            //Botão 5
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                botao5.PerformClick();
            }
            //Botão 6
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                botao6.PerformClick();
            }
            //Botão 7
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                botao7.PerformClick();
            }
            //Botão 8
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                botao8.PerformClick();
            }
            //Botão 9
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                botao9.PerformClick();
            }
            //Botão +
            if ((e.KeyCode == Keys.D && e.KeyCode == Keys.Oemplus) || e.KeyCode == Keys.Add)
            {
                botaoMais.PerformClick();
            }
            //Botão BlackSpace
            if (e.KeyCode == Keys.Back)
            {
                botaoBackSpace.PerformClick();
            }
        }
        
        private void botao0_Click(object sender, EventArgs e)
        {
            Numeros('0');
        }
        private void botao1_Click(object sender, EventArgs e)
        {
            Numeros('1');
        }

        private void botao2_Click(object sender, EventArgs e)
        {
            Numeros('2');
        }

        private void botao3_Click(object sender, EventArgs e)
        {
            Numeros('3');
        }

        private void botao4_Click(object sender, EventArgs e)
        {
            Numeros('4');
        }

        private void botao5_Click(object sender, EventArgs e)
        {
            Numeros('5');

        }

        private void botao6_Click(object sender, EventArgs e)
        {
            Numeros('6');
        }

        private void botao7_Click(object sender, EventArgs e)
        {
            Numeros('7');
        }

        private void botao8_Click(object sender, EventArgs e)
        {
            Numeros('8');
        }

        private void botao9_Click(object sender, EventArgs e)
        {
            Numeros('9');
        }
        private void botaoMais_Click(object sender, EventArgs e)
        {
            Simbolos("+");
        }

        private void botaoMenos_Click(object sender, EventArgs e)
        {
            Simbolos("-");
        }

        private void botaoMultiplicar_Click(object sender, EventArgs e)
        {
            Simbolos("x");
        }

        private void botaoDividir_Click(object sender, EventArgs e)
        {
            Simbolos("÷");
        }

        private void botaoRaiz_Click(object sender, EventArgs e)
        {            
            Calcular("√");
        }

        private void botaoExpoente_Click(object sender, EventArgs e)
        {
            Simbolos("^");
        }

        private void botaoPorcentagem_Click(object sender, EventArgs e)
        {

        }
        private void botaoIgual_Click(object sender, EventArgs e)
        {
            Calcular("=");
        }

        private void botaoResetar_Click(object sender, EventArgs e)
        {
            Opcoes("C");
        }

        private void botaoBackSpace_Click(object sender, EventArgs e)
        {
            Opcoes("BS");
        }

        private void botaoLimpar_Click(object sender, EventArgs e)
        {
            Opcoes("CE");
        }

        private void botaoSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
