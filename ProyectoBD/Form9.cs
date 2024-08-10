using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace ProyectoBD
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            LoadData();
            this.FormClosing += Form9_FormClosing;
        }
        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }
        private void LoadData()
        {
            string userId = "alexander";
            string password = "lenin2003";
            string connectionString = DatabaseConfig.GetConnectionString(userId,password);
            string query = "SELECT * FROM auditoria";
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
            string userId = "alexander";
            Form2 form2 = new Form2(userId);
            form2.Show();
            this.Hide();
        }
    }
}
