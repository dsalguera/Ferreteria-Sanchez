using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferreteria_Sanchez
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"
        Data Source=SALGUERA-RYZEN7\SQLEXPRESS;
        Initial Catalog=ferreteria_sanchez;
        Integrated Security=True");

        public static string universalUser = "";
        public static string universalPass = "";
        public static string universalType = "";
        public static string universalImage = "";

        void Cargar_Info()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("select contrasena, tipo_user, foto_user from usuarios where nombre_usuario=@nombre", conn);
                command.Parameters.AddWithValue("@nombre", textBox1.Text);

                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        String nombreUser = textBox1.Text;
                        String contrasenaUser = String.Format("{0}", reader["contrasena"]);
                        String fotoUser = String.Format("{0}", reader["foto_user"]);
                        int tipoUser = int.Parse(String.Format("{0}", reader["tipo_user"]));

                        if (contrasenaUser == textBox2.Text)
                        {
                            universalUser = nombreUser;
                            universalPass = contrasenaUser;
                            universalImage = fotoUser;

                            if (tipoUser == 1)
                            {
                                universalType = "Administrador";
                            }
                            else if (tipoUser == 2)
                            {
                                universalType = "Usuario";
                            }

                            this.Hide();
                            Menu m = new Menu();
                            m.ShowDialog();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("¡El usuario existe, más la contraseña es incorrecta!", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("¡El usuario no existe en el sistema! Contacte a su administrador", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Ocurrio un error de conexión!", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Cargar_Info();
        }
    }
}
