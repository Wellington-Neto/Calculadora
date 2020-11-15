using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace Calculadora
{
    public partial class CalculadoraForms : Form
    {        
        public static Func<double, double, double> somar = (a, b) => a + b;
        public static Func<double, double, double> subtrair = (a, b) => a - b;
        public static Func<double, double, double> multiplicar = (a, b) => a * b;
        public static Func<double, double, double> dividir = (a, b) => (a / b);
        public static Func<double, double, double> potencia = (a, b) => Math.Pow(a, b);
        public static Func<double, double> raiz = a => Math.Sqrt(a);
        public static Func<double, double, double> porcentagem = (a, b) => multiplicar(dividir(b, 100), a);
        public static Func<double, double, double> porcentagemSomar = (a, b) => somar(multiplicar(dividir(b, 100), a), a);
        public static Func<double, double, double> porcentagemSubtrair = (a, b) => subtrair(a, multiplicar(dividir(b, 100), a));
        public static Func<double, double, double> porcentagemMultiplicar = (a, b) => multiplicar(dividir(b, 100), a);
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
                    
                    if(expressao[1] == "+")
                    {
                        resultado = somar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                    }
                    else if(expressao[1] == "-")
                    {
                        resultado = subtrair(double.Parse(expressao[0]), double.Parse(expressao[2]));
                    }
                    else if(expressao[1] == "x")
                    {
                        resultado = multiplicar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                    }
                    else if(expressao[1] == "÷")
                    {
                        resultado = dividir(double.Parse(expressao[0]), double.Parse(expressao[2]));
                        if(resultado.ToString() == "∞")
                        {

                            visorInferior.Text = "Não é possível dividir por Zero";
                            visorInferior.Font = new Font("Microsoft Sans Serif", 22);
                            expressao[1] = "9";
                            break;
                        }                    
                    }
                    else if(expressao[1] == "^")
                    {
                        resultado = potencia(double.Parse(expressao[0]), double.Parse(expressao[2]));
                    }
                    visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + expressao[2] + " =";
                    visorInferior.Text = resultado.ToString();
                    expressao = new string[3] { resultado.ToString(), "1", "0" };
                    break;                   
                case "√":
                    int escolha = 0;                  
                    if (expressao[1] != "0" && expressao[1] != "1") escolha = 2;

                    resultado = raiz(double.Parse(expressao[escolha]));
                    if (escolha == 2)
                    {
                        visorSuperior.Text = expressao[0] + " " + expressao[1] + " √(" + expressao[2] + ") ";
                    }
                    else
                    {
                        visorSuperior.Text = " √(" + expressao[0] + ") ";
                    }                                       
                    expressao[escolha] = resultado.ToString();                    
                    if(expressao[escolha] == "NaN")
                    {
                        visorInferior.Text = "Entrada inválida";
                        expressao[1] = "9";
                        break;
                    }
                    visorInferior.Text = expressao[escolha];                    
                    break;
                case "%":                       
                    if(expressao[1] == "+")
                    {
                        resultado = porcentagemSomar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                        visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + porcentagem(double.Parse(expressao[0]), double.Parse(expressao[2])) + " (" + expressao[2] + "%) =";
                    }
                    else if(expressao[1] == "-")
                    {
                        resultado = porcentagemSubtrair(double.Parse(expressao[0]), double.Parse(expressao[2]));
                        visorSuperior.Text = expressao[0] + " " + expressao[1] + " " + porcentagem(double.Parse(expressao[0]), double.Parse(expressao[2])) + " (" + expressao[2] + "%) =";
                    }
                    else if(expressao[1] == "x")
                    {
                        resultado = porcentagemMultiplicar(double.Parse(expressao[0]), double.Parse(expressao[2]));
                        visorSuperior.Text = expressao[0] + " " + expressao[1] + " " +dividir(double.Parse(expressao[2]), 100) +" (" + expressao[2] + "%) =";
                    }
                    else
                    {
                        break;
                    }
                    visorInferior.Text = resultado.ToString();
                    expressao = new string[3] { resultado.ToString(), "1", "0" };                  
                    break;
            }
        }
        
        private void Numeros(char numero)
        {
            codigoErro();                      
                int escolha = 0;
                if (expressao[1] == "1") botaoResetar.PerformClick();
                if (expressao[1] != "0") escolha = 2;
                if (numero >= '0' && numero <= '9' && expressao[escolha].Length < 16)
                {
                    if (expressao[escolha] == "0") expressao[escolha] = "";
                    expressao[escolha] += numero;
                }
                else if (numero == ',')
                {
                    if (!expressao[escolha].Contains(","))
                    {
                        expressao[escolha] += numero;
                    }

                }
                visorInferior.Text = expressao[escolha];           
        }
        private void Simbolos(string simbolo)
        {
            codigoErro();               
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
                case "-":
                    if (expressao[1] == "1")
                    {
                        escolha = 0;
                        visorSuperior.Text = "0";
                    }
                        expressao[escolha] = (double.Parse(expressao[escolha]) * -1).ToString();
                    visorInferior.Text = expressao[escolha];                   
                    break;
                default:
                    break;
            }
        }
        private void codigoErro()
        {
            if (expressao[1] == "9")
            {
                Opcoes("C");
                visorInferior.Font = new Font("Microsoft Sans Serif", 34);
            }             
        }

       //interação com botões no forms
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
            if (e.KeyCode == Keys.Add)
            {
                botaoMais.PerformClick();
            }
            //Botão -
            if (e.KeyCode == Keys.Subtract)
            {
                botaoMenos.PerformClick();
            }
            //Botão x
            if (e.KeyCode == Keys.Multiply)
            {
                botaoMultiplicar.PerformClick();
            }
            //Botão ÷
            if (e.KeyCode == Keys.Divide)
            {
                botaoDividir.PerformClick();
            }
            //botao virgula (,)
            if (e.KeyCode == Keys.Oemcomma)
            {
                botaoVirgula.PerformClick();
            }
            //Botão BlackSpace
            if (e.KeyCode == Keys.Back)
            {
                botaoBackSpace.PerformClick();
            }
            //botão Delete(limpar)
            if(e.KeyCode == Keys.Delete)
            {
                botaoLimpar.PerformClick();
            }
            //botão ESC (resetar)
            if (e.KeyCode == Keys.Escape)
            {
                botaoResetar.PerformClick();
            }
        }

        #region Botoes 
        private void botao0_Click(object sender, EventArgs e)
        {
            Numeros('0');
            botaoIgual.Focus();
        }
        private void botao1_Click(object sender, EventArgs e)
        {
            Numeros('1');
            botaoIgual.Focus();
        }

        private void botao2_Click(object sender, EventArgs e)
        {
            Numeros('2');
            botaoIgual.Focus();
        }

        private void botao3_Click(object sender, EventArgs e)
        {
            Numeros('3'); 
            botaoIgual.Focus();
        }

        private void botao4_Click(object sender, EventArgs e)
        {
            Numeros('4');
            botaoIgual.Focus();
        }

        private void botao5_Click(object sender, EventArgs e)
        {
            Numeros('5');
            botaoIgual.Focus();
        }

        private void botao6_Click(object sender, EventArgs e)
        {
            Numeros('6');
            botaoIgual.Focus();
        }

        private void botao7_Click(object sender, EventArgs e)
        {
            Numeros('7');
            botaoIgual.Focus();
        }

        private void botao8_Click(object sender, EventArgs e)
        {
            Numeros('8');
            botaoIgual.Focus();
        }

        private void botao9_Click(object sender, EventArgs e)
        {
            Numeros('9');
            botaoIgual.Focus();
        }
        private void botaoMais_Click(object sender, EventArgs e)
        {
            Simbolos("+");
            botaoIgual.Focus();
        }

        private void botaoMenos_Click(object sender, EventArgs e)
        {
            Simbolos("-");
            botaoIgual.Focus();
        }

        private void botaoMultiplicar_Click(object sender, EventArgs e)
        {
            Simbolos("x"); 
            botaoIgual.Focus();
        }

        private void botaoDividir_Click(object sender, EventArgs e)
        {
            Simbolos("÷");
            botaoIgual.Focus();
        }

        private void botaoRaiz_Click(object sender, EventArgs e)
        {            
            Calcular("√");
            botaoIgual.Focus();
        }

        private void botaoExpoente_Click(object sender, EventArgs e)
        {
            Simbolos("^");
            botaoIgual.Focus();
        }

        private void botaoPorcentagem_Click(object sender, EventArgs e)
        {
            Calcular("%");
            botaoIgual.Focus();
        }
        private void botaoIgual_Click(object sender, EventArgs e)
        {
            Calcular("=");            
        }

        private void botaoResetar_Click(object sender, EventArgs e)
        {
            Opcoes("C");
            botaoIgual.Focus();
        }

        private void botaoBackSpace_Click(object sender, EventArgs e)
        {
            Opcoes("BS"); 
            botaoIgual.Focus();
        }

        private void botaoLimpar_Click(object sender, EventArgs e)
        {
            Opcoes("CE"); 
            botaoIgual.Focus();
        }

        private void botaoSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botaoVirgula_Click(object sender, EventArgs e)
        {
            Numeros(',');
            botaoIgual.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Opcoes("-");
            botaoIgual.Focus();
        }
        #endregion;
    }
}
