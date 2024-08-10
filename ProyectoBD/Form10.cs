using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client; // Asegúrate de tener esta referencia

namespace ProyectoBD
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            this.Load += Form10_Load; // Asegúrate de que se llame al método LoadData cuando se cargue el formulario
            this.FormClosing += Form10_FormClosing;
        }
        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            LoadData(); // Carga los datos cuando el formulario se cargue
        }

        private void LoadData()
        {
            string connectionString = DatabaseConfig.GetConnectionString("alexander", "lenin2003"); // Usa la cadena de conexión adecuada
            string query = "SELECT * FROM vw_empleados";
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
