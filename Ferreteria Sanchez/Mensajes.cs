using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;


using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Ferreteria_Sanchez
{
    public partial class Mensajes : UserControl
    {
        public Mensajes()
        {
            InitializeComponent();
        }

        public void MensajeNormal(string numero, string mensaje)
        {
            var accountSid = "AC465ee4703abe2ecd6e16b572c7177d01";
            var authToken = "2d9099c6cca1b5e98241cc8f61e383cb";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+505"+numero));
            //new PhoneNumber("+50557303401"));
            messageOptions.MessagingServiceSid = "MG5e05b852af956d9b7cedbdc1663f895a";
            messageOptions.Body = mensaje;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }

        public void WhatsApp(string numero, string mensaje)
        {
            var accountSid = "AC465ee4703abe2ecd6e16b572c7177d01";
            var authToken = "2d9099c6cca1b5e98241cc8f61e383cb";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                
                new PhoneNumber("whatsapp:+505"+numero));
            //new PhoneNumber("whatsapp:+50557303401"));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = mensaje;

            foreach (ListViewItem itemRow in listView2.Items)
            {           
                messageOptions.MediaUrl.Add(new Uri(itemRow.Text));
            }

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }


        private void Mensajes_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("codebrainers@gmail.com");
                mail.To.Add(textBox1.Text);
                mail.Subject = textBox2.Text;
                mail.Body = richTextBox1.Text;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("codebrainers@gmail.com", "Windowsxp123456.");
                SmtpServer.EnableSsl = true;

                foreach (ListViewItem itemRow in listView1.Items)
                {
                    mail.Attachments.Add(new Attachment(itemRow.Text));
                }

                SmtpServer.Send(mail);
                //MessageBox.Show("El correo ha sido enviado!");
                lblEnviando.Text = "Se ha enviando el correo a "+ textBox1.Text +".";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeleccionarImagen(pictureBox1,listView1);
        }

        void SeleccionarImagen(PictureBox pictureBox, ListView listView)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Insertar Archivo";
            fdlg.InitialDirectory = @"C:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                listView.Items.Add(fdlg.FileName);
                pictureBox.ImageLocation = fdlg.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MensajeNormal(textBox3.Text,richTextBox2.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WhatsApp(textBox4.Text,richTextBox3.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SeleccionarImagen(pictureBox2,listView2);
        }
    }
}
