using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Ferreteria_Sanchez
{
    public partial class Productos : UserControl
    {
        public Productos()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"
        Data Source=SALGUERA-RYZEN7\SQLEXPRESS;
        Initial Catalog=ferreteria_sanchez;
        Integrated Security=True");

        SqlDataAdapter adapter;
        DataTable dt;

        private void Productos_Load(object sender, EventArgs e)
        {
            Cargar_Info("select * from productos", gridProductos);
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
                MessageBox.Show("¡Revise el Query a la Base de Datos!", "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool Validar_Campos()
        {
            if (textBox3.Text.Equals("") || textBox1.Text.Equals("") || textBox2.Text.Equals("") || comboBox1.SelectedIndex == -1 || numericUpDown1.Value == 0)
            {
                return false;
            }
            else
            {
                nombreProducto = textBox3.Text;
                marca = textBox1.Text;
                precio = textBox2.Text;
                categoria = comboBox1.SelectedItem.ToString();
                stock = (int)numericUpDown1.Value;
                tags = richTextBox1.Text;
            }

            return true;
        }

        int idProducto = 0;
        string nombreProducto = "";
        string marca = "";
        int stock = 0;
        string precio = "";
        string categoria = "";
        string tags = "";
        string foto = "";

        void Insertar_Info()
        {
            if (Validar_Campos() == true)
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("" +
                        "insert into productos" +
                        "(nombre_producto,marca_producto,num_stock,precio_unitario,categoria,tags,foto_producto)" +
                        "values" +
                        "(@nombre,@marca,@stock,@precio,@categoria,@tags,@foto);",
                        conn);

                    command.Parameters.AddWithValue("@nombre", nombreProducto);
                    command.Parameters.AddWithValue("@marca", marca);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    command.Parameters.AddWithValue("@tags", tags);
                    command.Parameters.AddWithValue("@foto", foto);
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar_Info();
            Cargar_Info("select * from productos", gridProductos);
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
                foto = fdlg.FileName;
            }
        }

        private void btnCambiarImg_Click(object sender, EventArgs e)
        {
            Seleccionar_Imagen(pictureBox1);
        }

        void Click_Datagrid(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gridProductos.Rows[e.RowIndex];

                label7.Text = row.Cells[0].Value.ToString();
                idProducto = int.Parse(row.Cells[0].Value.ToString());

                textBox3.Text = row.Cells[1].Value.ToString();
                textBox1.Text = row.Cells[2].Value.ToString();

                numericUpDown1.Value = int.Parse(row.Cells[3].Value.ToString());

                textBox2.Text = row.Cells[4].Value.ToString();
                comboBox1.Text = row.Cells[5].Value.ToString();
                richTextBox1.Text = row.Cells[6].Value.ToString();
                String urlImg = row.Cells[7].Value.ToString();

                pictureBox1.ImageLocation = urlImg;
            }
        }

        private void gridProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Click_Datagrid(e);
        }

        void Nuevo_Registro()
        {
            label7.Text = "####";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 0;
            richTextBox1.Text = "";
            comboBox1.Text = "-- Seleccione --";
            pictureBox1.Image = null;
        }

        void Modificar_Info()
        {
            if (Validar_Campos() == true)
            {
                try
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("" +
                        "update productos set " +
                        "nombre_producto = @nombre," +
                        "marca_producto = @marca," +
                        "num_stock = @stock," +
                        "precio_unitario = @precio," +
                        "categoria = @categoria," +
                        "tags = @tags," +
                        "foto_producto = @foto " +
                        "where id_productos = @id"
                        , conn);

                    command.Parameters.AddWithValue("@id", idProducto);
                    command.Parameters.AddWithValue("@nombre", nombreProducto);
                    command.Parameters.AddWithValue("@marca", marca);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    command.Parameters.AddWithValue("@tags", tags);
                    command.Parameters.AddWithValue("@foto", foto);
                    // int result = command.ExecuteNonQuery();

                    command.ExecuteNonQuery();
                    MessageBox.Show("¡Los datos se modificaron correctamente!", "Consulta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    conn.Close();

                    Cargar_Info("select * from productos", gridProductos);
                }
                catch (Exception ex)
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
                    "delete from productos where id_productos = @id and nombre_producto = @nombre and marca_producto = @marca"
                    , conn);

                command.Parameters.AddWithValue("@id", label7.Text);
                command.Parameters.AddWithValue("@nombre", textBox3.Text);
                command.Parameters.AddWithValue("@marca", textBox1.Text);

                // int result = command.ExecuteNonQuery();

                command.ExecuteNonQuery();
                MessageBox.Show("¡Los datos se eliminaron correctamente!", "Consulta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();

                Cargar_Info("select * from productos", gridProductos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Ha ocurrido un error eliminando los datos!", "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar_Info();
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo_Registro();
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format(comboBox2.SelectedItem + " like '%{0}%'", textBox5.Text);
            gridProductos.DataSource = dv;
        }
    }
}
