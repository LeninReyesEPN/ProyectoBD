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
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();
            this.FormClosing += Form21_FormClosing;
        }

        private void Form21_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18();
            form18.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox txtCliente
            string id = txtId.Text;

            // Validar que el campo no esté vacío
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Por favor, ingrese el id de la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL con un parámetro
            string userId = "alexander";
            string password = "lenin2003";
            string query = "SELECT * FROM ventas_sur WHERE id = :id";

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
                        command.Parameters.Add(new OracleParameter("id", id));

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
            string id = txtId.Text;
            string producto = txtProducto.Text;
            string cantidad = txtCantidad.Text;

            // Validar que los campos no estén vacíos
            if ( string.IsNullOrEmpty(producto) || string.IsNullOrEmpty(cantidad))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL para actualizar el registro
            string query = "UPDATE ventas_sur SET id_producto = :producto, cantidad = :cantidad WHERE id = :id";

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
                        command.Parameters.Add(new OracleParameter("producto", producto));
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
