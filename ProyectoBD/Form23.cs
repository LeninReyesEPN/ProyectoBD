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
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            string userId = "dtipan";
            Form3 form3 = new Form3(userId);
            form3.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form24 nuevoFormulario = new Form24();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Form25 form25 = new Form25();
            form25.Show();
            this.Hide();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            Form26 form26 = new Form26();
            form26.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Form27 form27 = new Form27();
            form27.Show();
            this.Hide();
        }
    }
}
