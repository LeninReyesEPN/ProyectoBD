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
    public partial class Form30 : Form
    {
        public Form30()
        {
            InitializeComponent();
            this.Load += Form30_Load; // Asegúrate de que se llame al método LoadData cuando se cargue el formulario
            this.FormClosing += Form30_FormClosing;
        }
        private void Form30_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void Form30_Load(object sender, EventArgs e)
        {
            LoadData(); // Carga los datos cuando el formulario se cargue
        }

        private void LoadData()
        {
            string connectionString = DatabaseConfig.GetConnectionString("dtipan", "dtipan"); // Usa la cadena de conexión adecuada
            string query = "SELECT * FROM promociones";
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


        private void btnSalir_Click(object sender, EventArgs e)
        {
            string userId = "dtipan";
            Form3 form3 = new Form3(userId);
            form3.Show();
            this.Hide();
        }
    }
}
