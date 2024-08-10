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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
            this.FormClosing += Form14_FormClosing;

        }
        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox txtCliente
            string inventarioId = txtId.Text;

            // Validar que el campo no esté vacío
            if (string.IsNullOrEmpty(inventarioId))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL con un parámetro
            string userId = "alexander";
            string password = "lenin2003";
            string query = "SELECT * FROM inventario_sur WHERE id = :id";

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
                        command.Parameters.Add(new OracleParameter("id", inventarioId));

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string id = txtIde.Text;
            string cantidad = txtCantidad.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(cantidad))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL para actualizar el registro
            string query = "UPDATE inventario_sur SET cantidad = :cantidad WHERE id = :id";

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
                        command.Parameters.Add(new OracleParameter("cantidad", cantidad));
                        command.Parameters.Add(new OracleParameter("id", id));

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Commit de la transacción si la actualización fue exitosa
                            transaction.Commit();
                            MessageBox.Show("Registro actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro con el ID especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
