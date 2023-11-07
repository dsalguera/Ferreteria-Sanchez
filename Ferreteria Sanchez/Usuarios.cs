using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferreteria_Sanchez
{

    public partial class Usuarios : UserControl
    {
        public Usuarios()
        {
            InitializeComponent();   
        }

        SqlConnection conn = new SqlConnection(@"
        Data Source=SALGUERA-RYZEN7\SQLEXPRESS;
        Initial Catalog=ferreteria_sanchez;
        Integrated Security=True");

        SqlDataAdapter adapter;
        DataTable dt;

        private void Usuarios_Load(object sender, EventArgs e)
        {
            Cargar_Info("select * from usuarios", gridUsuarios);
        }

        void Cargar_Info(String query, DataGridView data)
        {
            try
            {
                data.DataSource = null;
                adapter = new SqlDataAdapter(query, conn);
                dt = new DataTable();
                adapter.Fill(dt);

                /*foreach (DataRow row in dt.Rows){ MessageBox.Show(""+ row["nombre_usuario"]); }*/

                data.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("¡Revise el Query a la Base de Datos!","Error de Consulta",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        int idUsuario = 0;
        String nombreUsuario = "";
        String contrasena = "";
        String repetir_contrasena = "";
        String tipoUsuario = "";
        String fileImage = "";
        int tipo = 0;


        //Agregar tambien este metodo a modicar y eliminar
        bool Validar_Campos()
        {
            if (textBox3.Text.Equals("") || textBox1.Text.Equals("") || textBox2.Text.Equals("") || comboBox1.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                nombreUsuario = textBox3.Text;
                contrasena = textBox1.Text;
                repetir_contrasena = textBox2.Text;
                tipoUsuario = comboBox1.SelectedItem.ToString();
            }

            if (tipoUsuario == "Administrador")
            {
                tipo = 1;
            }
            else if (tipoUsuario == "Usuario")
            {
                tipo = 2;
            }

            return true;
        }

        void Seleccionar_Imagen(PictureBox pictureBox)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Insertar Archivo";
            fdlg.InitialDirectory = @"C:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox.ImageLocation = fdlg.FileName;
                fileImage = fdlg.FileName; 
            }
        }

        void Insertar_Info()
        {
            if (Validar_Campos() == true)
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("" +
                        "insert into usuarios(nombre_usuario,contrasena,tipo_user,foto_user)" +
                        "values(@nombre,@contrasena,@tipo,@StrFoto);",
                        conn);

                    command.Parameters.AddWithValue("@nombre", nombreUsuario);
                    command.Parameters.AddWithValue("@contrasena", contrasena);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@StrFoto", fileImage);
                    // int result = command.ExecuteNonQuery();

                    command.ExecuteNonQuery();
                    MessageBox.Show("¡Los datos se ingresaron correctamente!", "Consulta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    conn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Ha ocurrido un error ingresando los datos!", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("¡Necesita rellenar todos los campos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        void Modificar_Info()
        {
            if (Validar_Campos() == true)
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("" +
                        "update usuarios set " +
                        "nombre_usuario = @nombre," +
                        "contrasena = @contrasena," +
                        "tipo_user = @tipo," +
                        "foto_user = @StrFoto " +
                        "where id_usuario = @id"
                        , conn);

                    command.Parameters.AddWithValue("@id", idUsuario);
                    command.Parameters.AddWithValue("@nombre", nombreUsuario);
                    command.Parameters.AddWithValue("@contrasena", contrasena);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@StrFoto", fileImage);
                    // int result = command.ExecuteNonQuery();

                    command.ExecuteNonQuery();
                    MessageBox.Show("¡Los datos se modificaron correctamente!", "Consulta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    conn.Close();

                    Cargar_Info("select * from usuarios", gridUsuarios);
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Ha ocurrido un error modificando los datos!", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("¡Necesita rellenar todos los campos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void Eliminar_Info()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("" +
                    "delete from usuarios where id_usuario = @id and nombre_usuario = @nombre and contrasena = @contrasena"
                    , conn);

                command.Parameters.AddWithValue("@id", label7.Text);
                command.Parameters.AddWithValue("@nombre", textBox3.Text);
                command.Parameters.AddWithValue("@contrasena", textBox1.Text);

                // int result = command.ExecuteNonQuery();

                command.ExecuteNonQuery();
                MessageBox.Show("¡Los datos se eliminaron correctamente!", "Consulta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();

                Cargar_Info("select * from usuarios", gridUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Ha ocurrido un error eliminando los datos!", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar_Info();
            Cargar_Info("select * from usuarios", gridUsuarios);
        }

        private void btnCambiarImg_Click(object sender, EventArgs e)
        {
            Seleccionar_Imagen(pictureBox1);
        }

        void Nuevo_Registro()
        {
            label7.Text = "####";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "-- Seleccione Tipo --";
            pictureBox1.Image = null;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo_Registro();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar_Info();
        }

        void Click_Datagrid(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gridUsuarios.Rows[e.RowIndex];
                
                label7.Text = row.Cells[0].Value.ToString();
                idUsuario = int.Parse(row.Cells[0].Value.ToString());

                textBox3.Text = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[2].Value.ToString();

                String tipoS = row.Cells[3].Value.ToString();

                if (tipoS.Equals("1"))
                {
                    tipoS = "Administrador";
                }
                else if (tipoS.Equals("2"))
                {
                    tipoS = "Usuario";
                }

                comboBox1.Text = tipoS;

                String urlImg = row.Cells[4].Value.ToString();
                pictureBox1.ImageLocation = urlImg;
            }
        }

        private void gridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Click_Datagrid(e); 
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format(comboBox2.SelectedItem+" like '%{0}%'",textBox5.Text);
            gridUsuarios.DataSource = dv;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea eliminar el siguiente registro?", "¿Confirmar Eliminación?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Eliminar_Info();
                Nuevo_Registro();
            }
            else if (dialogResult == DialogResult.No)
            {
                //sino nel haha que esperabas? xD
            }
 
        }
    }
}
