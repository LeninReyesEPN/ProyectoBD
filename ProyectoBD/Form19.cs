using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ProyectoBD
{
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
            this.FormClosing += Form19_FormClosing;
        }

        private void Form19_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18();
            form18.Show();
            this.Hide();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string userId = "alexander";
            string password = "lenin2003";
            string id = txtId.Text;
            string cliente = txtCliente.Text;
            string producto = txtProducto.Text;
            string cantidad = txtCantidad.Text;
            string fecha = txtFecha.Text;
            string sucursal = txtSucursal.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(cliente) || string.IsNullOrEmpty(producto) || string.IsNullOrEmpty(cantidad) || string.IsNullOrEmpty(fecha) || string.IsNullOrEmpty(sucursal))
            {
                MessageBox.Show("Todos los campos deben estar completos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convertir la fecha a tipo DateTime
            DateTime fechaVenta;
            if (!DateTime.TryParse(fecha, out fechaVenta))
            {
                MessageBox.Show("Formato de fecha no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Construir la consulta SQL
            string query = "INSERT INTO ventas (ID, ID_CLIENTE, ID_PRODUCTO, CANTIDAD, FECHA_VENTA, SUCURSAL) VALUES (:id, :cliente, :producto, :cantidad, :fecha_venta, :sucursal)";

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
                            command.Parameters.Add(new OracleParameter("cliente", cliente));
                            command.Parameters.Add(new OracleParameter("producto", producto));
                            command.Parameters.Add(new OracleParameter("cantidad", cantidad));
                            command.Parameters.Add(new OracleParameter("fecha_venta", fechaVenta));
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
    }
}
