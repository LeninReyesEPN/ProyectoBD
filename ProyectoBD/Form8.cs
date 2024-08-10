using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ProyectoBD
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.FormClosing += Form4_FormClosing;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Manejo de eventos para el label, si es necesario
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox txtCliente
            string clienteNombre = txtCliente.Text;

            // Validar que el campo no esté vacío
            if (string.IsNullOrEmpty(clienteNombre))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL con un parámetro
            string userId = "alexander";
            string password = "lenin2003";
            string query = "SELECT * FROM clientes_sucursalsur WHERE nombre = :nombre";

            // Obtener la cadena de conexión
            string connectionString = DatabaseConfig.GetConnectionString(userId, password);

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        // Agregar el parámetro a la consulta
                        command.Parameters.Add(new OracleParameter("nombre", clienteNombre));

                        // Usar un OracleDataAdapter para llenar el DataTable
                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Asignar el DataTable al DataGridView
                            dataGridView1.DataSource = dataTable;

                            // Ajustar el tamaño de las columnas
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox txtNombre y txtId
            string nombre = txtNombre.Text;
            string id = txtId.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Por favor, ingrese el nombre y el ID del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL para eliminar el registro
            string query = "DELETE FROM clientes_sucursalsur WHERE nombre = :nombre AND id = :id";

            // Obtener la cadena de conexión
            string userId = "alexander";
            string password = "lenin2003";
            string connectionString = DatabaseConfig.GetConnectionString(userId, password);

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    connection.Open();

                    // Iniciar una transacción
                    transaction = connection.BeginTransaction();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta
                        command.Parameters.Add(new OracleParameter("nombre", nombre));
                        command.Parameters.Add(new OracleParameter("id", id));

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Commit de la transacción si la eliminación fue exitosa
                            transaction.Commit();
                            MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo encontrar el registro para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Deshacer la transacción en caso de error
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
