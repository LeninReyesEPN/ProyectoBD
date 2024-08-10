using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ProyectoBD
{
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
            LoadData();
            this.FormClosing += Form20_FormClosing;
        }

        private void Form20_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18();
            form18.Show();
            this.Hide();
        }

        private void LoadData()
        {
            string connectionString = DatabaseConfig.GetConnectionString("alexander", "lenin2003");
            string query = "SELECT * FROM ventas_sur";
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Asigna el DataTable al DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Ajustar el tamaño de las columnas
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
