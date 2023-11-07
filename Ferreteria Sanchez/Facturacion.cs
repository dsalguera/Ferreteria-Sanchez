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
    public partial class Facturacion : UserControl
    {
        public Facturacion()
        {
            InitializeComponent();
            label3.Text = Login.universalUser;

            dataGridView1.Columns.Add("idProducto", "Cód. Producto");
            dataGridView1.Columns.Add("nombreProducto", "Descripción del Producto");
            dataGridView1.Columns.Add("cantidad", "Cantidad");
            dataGridView1.Columns.Add("precio", "Precio / U");
            dataGridView1.Columns.Add("descuento", "Desc.");
            dataGridView1.Columns.Add("total", "Total");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        }

        SqlConnection conn = new SqlConnection(@"
        Data Source=SALGUERA-RYZEN7\SQLEXPRESS;
        Initial Catalog=ferreteria_sanchez;
        Integrated Security=True");



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        String codProducto = "";
        String nombreProducto = "";
        String marcaProd = "";
        int stockProd = 0;
        double precioProd = 0;
        String categoriaProd = "";
        String tagsProd = "";
        String fotoProd = "";

        void Insertar_Producto_ID()
        {
            try
            {
                conn.Open();

                SqlCommand command = new SqlCommand("select * from productos where id_productos=@id", conn);
                command.Parameters.AddWithValue("@id", textBox1.Text);

                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        codProducto = String.Format("{0}", reader["id_productos"]);
                        nombreProducto = String.Format("{0}", reader["nombre_producto"]);
                        marcaProd = String.Format("{0}", reader["marca_producto"]);
                        stockProd = int.Parse(String.Format("{0}", reader["num_stock"]));
                        precioProd = Double.Parse(String.Format("{0}", reader["precio_unitario"]));
                        categoriaProd = String.Format("{0}", reader["categoria"]);
                        tagsProd = String.Format("{0}", reader["tags"]);
                        fotoProd = String.Format("{0}", reader["foto_producto"]);

                        label8.Text = "Cód: " + codProducto + " - " + nombreProducto + "/" + marcaProd + ", en Stock: " + stockProd + " unidades.";

                    }
                    else
                    {
                        MessageBox.Show("¡El producto no existe en el sistema! Contacte a su administrador", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Insertar_Producto_ID();
        }

        void Agregar_Memoria()
        {
            dataGridView1.Rows.Add(codProducto, nombreProducto, 1, precioProd, "0", (1* precioProd));

            /*MessageBox.Show(codProducto+"\n"+
                nombreProducto+"\n"+
                marcaProd+"\n"+
                stockProd+"\n"+
                precioProd+"\n"+
                categoriaProd+"\n"+
                tagsProd+"\n"+
                fotoProd+"\n");*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Agregar_Memoria();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                if (selectedIndex > -1)
                {
                    dataGridView1.Rows.RemoveAt(selectedIndex);
                    dataGridView1.Refresh(); // if needed
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("No se pudo eliminar esta fila.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

    }
}
