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
    public partial class Prestamos : Form
    {

        string nombre_cliente;
        string[] sexos_disponibles = { "Masculino", "Femenino", "Otro" };
        int[] cuotas_disponibles = { 12, 24, 36, 60, 120 };
        string[] provincia_disponibles;
        Dictionary<int, double> intereses_base;



        public Prestamos(string nombre)
        {
            InitializeComponent();
            nombre_cliente = nombre;

            string listado_provincias = Properties.Resources.Lugares.ToString();
            provincia_disponibles = listado_provincias.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            intereses_base = new Dictionary<int, double>();
            double interes;
            int i;
            for (i = 0, interes = 3.0; i < cuotas_disponibles.Length; i++, interes += 0.5)
            {
                intereses_base[cuotas_disponibles[i]] = interes;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Prestamos_Load(object sender, EventArgs e)
        {
            popularCuotas();
            popularSexo();
            popularProvincia();
            saludo.Text += nombre_cliente;
        }
        void popularCuotas()
        {
            for (int i = 0; i < cuotas_disponibles.Length; i++)
            {
                cuotas.Items.Add(cuotas_disponibles[i]);
            }
        }
        void popularSexo()
        {
            for (int i = 0; i < sexos_disponibles.Length; i++)
            {
                sexo.Items.Add(sexos_disponibles[i]);
            }
        }
        void popularProvincia()
        {
            for (int i = 0; i < provincia_disponibles.Length; i++)
            {
                provincia.Items.Add (provincia_disponibles[i]);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        double calcular_Interes()
        {
            int cuotas_Pedidas = (int)cuotas.SelectedItem;
            string sexo_Seleccionado = sexo.SelectedItem.ToString().ToLower();
            string provincia_Seleccionada = provincia.SelectedItem.ToString().ToLower();
            double interes = intereses_base[cuotas_Pedidas];
            if (new[] { "otro" }.Contains(sexo_Seleccionado)) { interes += 0.5; }
            if (new[] { "tucuman", "misiones", "mendoza" }.Contains(provincia_Seleccionada)) { interes -= 0.3; }
            return interes;
        }

        private void btnconfirmasolicitud_Click(object sender, EventArgs e)
        {
            switch (validaciones())
            {
                case 0:
                    {
                        errorProvider1.SetError(DatosPersonales, "");
                        errorProvider1.SetError(DatosPrestamo, "");
                        double interes_mensual=calcular_Interes();
                        double monto_Pedido = double.Parse(monto.Text); 
                        int cuota_Pedida=(int)cuotas.SelectedItem;
                        double interes_Total = monto_Pedido * (interes_mensual / 100) * cuota_Pedida;
                        double monto_A_Pagar = monto_Pedido + interes_Total;
                        string mensaje = "Prestamo" + monto_Pedido + "en " + cuota_Pedida + "cuotas, el interes mensual es " + interes_mensual + "% mensual\n El monto final a pagar es " + monto_A_Pagar;
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show (mensaje,"Calculo de intereses",buttons);   
                        break;
                    }
                case 1:
                    {
                        errorProvider1.SetError(DatosPersonales, "Debe completar todos los campos");
                        errorProvider1.SetError(DatosPrestamo, "");
                        break;
                    }
                case 2:
                    {
                        errorProvider1.SetError(DatosPrestamo, "Debe completar todos los campos");
                        errorProvider1.SetError(DatosPersonales , "");
                        break;
                    }
            }

        }
        int validaciones()
        {
            if ((sexo.SelectedIndex <= -1) || (provincia.SelectedIndex <= -1)) { return 1; }
            else
            {
                if(!(monto.Text.All(char.IsDigit)) || (monto.Text==" ") || (cuotas.SelectedIndex <= -1) ) { return 2; }
                else { return 0; }
            }
                
        }
    }

}