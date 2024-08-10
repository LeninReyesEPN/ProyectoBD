using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBD
{
    public partial class Form3 : Form
    {
        public Form3(string userId)
        {
            InitializeComponent();
            textBoxUsuario.Text = userId;
            textBoxUsuario.ReadOnly = true;
            this.FormClosing += Form3_FormClosing;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Form23 form23 = new Form23();
            form23.Show();
            this.Hide();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            Form28 form28 = new Form28();
            form28.Show();
            this.Hide();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            Form29 form29 = new Form29();
            form29.Show();
            this.Hide();
        }

        private void BtnPromos_Click(object sender, EventArgs e)
        {
            Form30 form30 = new Form30();
            form30.Show();
            this.Hide();
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            Form31 form31 = new Form31();
            form31.Show();
            this.Hide();
        }
    }
}
