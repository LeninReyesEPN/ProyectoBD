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
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
            this.FormClosing += Form18_FormClosing;
        }
        private void Form18_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            string userId = "alexander";
            Form2 form2 = new Form2(userId);
            form2.Show();
            this.Hide();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            Form20 form20 = new Form20();
            form20.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Form21 form21 = new Form21();
            form21.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Form22 form22 = new Form22();
            form22.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.Show();
            this.Hide();
        }
    }
}
