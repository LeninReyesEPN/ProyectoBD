using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client; // Asegúrate de tener esta referencia

namespace ProyectoBD
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.FormClosing += Form5_FormClosing;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string userId = "alexander";
            string password = "lenin2003";
            string id = txtId.Text;
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;
            string sucursal = txtSucursal.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(sucursal))
            {
                MessageBox.Show("Todos los campos deben estar completos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL
            string query = "INSERT INTO clientes_sucursalsur (ID, NOMBRE, DIRECCION, SUCURSAL) VALUES (:id, :nombre, :direccion, :sucursal)";

            // Obtener la cadena de conexión
            string connectionString = DatabaseConfig.GetConnectionString(userId, password); // Reemplaza con los valores adecuados

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Iniciar una transacción
                    using (OracleTransaction transaction = connection.BeginTransaction())
                    {
                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            // Establecer la transacción para el comando
                            command.Transaction = transaction;

                            // Agregar los parámetros para prevenir SQL injection
                            command.Parameters.Add(new OracleParameter("id", id));
                            command.Parameters.Add(new OracleParameter("nombre", nombre));
                            command.Parameters.Add(new OracleParameter("direccion", direccion));
                            command.Parameters.Add(new OracleParameter("sucursal", sucursal));

                            // Ejecutar la consulta
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Confirmar la transacción
                                transaction.Commit();
                                MessageBox.Show("Registro agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSucursal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
