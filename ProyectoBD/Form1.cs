using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ProyectoBD;






namespace ProyectoBD
{
    public partial class Form1 : Form
    {

        string userId;
        string password;
        public Form1()
        {
            InitializeComponent();
            // Configurar PasswordChar en el código
            textBoxPassword.PasswordChar = '*';
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Finaliza la aplicación cuando se cierra el formulario
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userId = textBoxUser.Text;
            password = textBoxPassword.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese el usuario y la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de credenciales y redirección a formularios específicos
            if ((userId == "alexander" && password == "lenin2003") ||
                (userId == "system" && password == "orcl"))
            {
                string connectionString = DatabaseConfig.GetConnectionString(userId, password);

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        //MessageBox.Show("Conexión exitosa");

                        // Redirección basada en las credenciales
                        if (userId == "alexander" && password == "lenin2003")
                        {
                            Form2 nuevoFormulario = new Form2(userId);
                            nuevoFormulario.Show();
                        }
                        else if (userId == "system" && password == "orcl")
                        {
                            Form3 nuevoFormulario = new Form3(userId);
                            nuevoFormulario.Show();
                        }

                        // Ocultar el formulario actual en lugar de cerrarlo
                        this.Hide();
                    }
                    catch (OracleException ex)
                    {
                        // Manejar errores específicos de Oracle
                        switch (ex.Number)
                        {
                            case 1017: // Código de error para usuario o contraseña inválidos
                                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            default:
                                MessageBox.Show("Error al conectar: " + ex.Message);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }
    }
}