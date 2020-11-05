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
        
        public CalculadoraForms()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(CalculadoraForms_KeyDown);
        }

      


        private void botaoIgual_Click(object sender, EventArgs e)
        {
            
          
        }

        private void CalculadoraForms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                botaoIgual.PerformClick();
            }
            if (e.KeyCode == Keys.F)
            {
                
            }
        }

        private void CalculadoraForms_Load(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }
    }
}
