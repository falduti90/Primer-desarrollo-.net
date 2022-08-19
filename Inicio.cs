using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco_San_Miguel
{
    public partial class Inicio : Form // herencia  de form  
    {
        public Inicio()// constructor
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            btnSolicitarPrestamo.Enabled = false;
        }
        private void ControlBotones()
        {
            if (nombres.Text.Trim() != string.Empty && nombres.Text.All(char.IsLetter))
            {
                btnSolicitarPrestamo.Enabled = true;
                errorProvider1.SetError(nombres, "");
            }
            else
            {
                if (!(nombres.Text.All(char.IsLetter))) { 
                    errorProvider1.SetError(nombres, "El nombre solo contiene letras"); 
                }
                else { errorProvider1.SetError(nombres, "Debe introducir su nombre"); }
            }
            //btnSolicitarPrestamo.Enabled = false;
            nombres.Focus();
        }
        private void nombres_TextChanged(object sender, EventArgs e)
        {
            ControlBotones();
        }

        private void btnSolicitarPrestamo_Click(object sender, EventArgs e)
        {
            using (Prestamos ventanaPrestamos = new Prestamos(nombres.Text))
                ventanaPrestamos.ShowDialog();
        }
    }
}
