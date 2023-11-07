using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ferreteria_Sanchez
{
    public partial class Menu : Form
    {

        //Disable close button
        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

        /*Metodo para interfaz circular*/

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        /*Aqui termina el metodo*/

        /**/

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        /**/


        public Menu()
        {
            InitializeComponent();
            //Aqui se incializa el comando de rectangulo
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            panelNav.Visible = false;

            PInsertado.Controls.Add(new Ferreteria_Sanchez.Mensajes());
            PInsertado.Controls.Add(new Ferreteria_Sanchez.Productos());
        }

        public void MetodoBoton(Panel p, Button b)
        {
            p.Height = b.Height;
            p.Top = b.Top;
            p.Left = b.Left;

            b.BackColor = Color.FromArgb(229, 229, 229);
            panelNav.Visible = true;
        }

        public void MetodoBotonLeave(Button b)
        {
            b.BackColor = Color.FromArgb(255, 255, 255);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button1);
            principal1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button2);
            usuarios1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button3);
            productos1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button4);
            reportes1.BringToFront();
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button1);
        }

        private void button1_Leave_1(object sender, EventArgs e)
        {
            MetodoBotonLeave(button1);
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button2);
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button3);
        }

        private void button4_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button4);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        void Parametros_Inicio()
        {
            label1.Text = Login.universalUser;
            label2.Text = Login.universalType;
            pictureBox1.ImageLocation = Login.universalImage;

            String tipo = Login.universalType;

            if (tipo == "Usuario")
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Parametros_Inicio();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button7);
            mensajes1.BringToFront();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MetodoBoton(panelNav, button8);
            facturacion2.BringToFront();
        }

        private void button8_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button8);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                Login l = new Login();
                l.ShowDialog();
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //sino nel haha que esperabas? xD
            }
        }

        private void button7_Leave(object sender, EventArgs e)
        {
            MetodoBotonLeave(button7);
        }
    }
}
