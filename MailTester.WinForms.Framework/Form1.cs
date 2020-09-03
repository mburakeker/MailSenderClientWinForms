using System;
using System.Text;
using System.Windows.Forms;

namespace MailTester.WinForms.Framework
{
    public partial class MailSender : Form
    {
        public MailSender()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage netMailMessage = new System.Net.Mail.MailMessage();
            netMailMessage.IsBodyHtml = true;
            netMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            netMailMessage.SubjectEncoding = Encoding.UTF8;
            netMailMessage.BodyEncoding = Encoding.UTF8;
            netMailMessage.Subject = this.textBox6.Text;
            netMailMessage.Body = this.richTextBox1.Text;
            var toList = this.textBox5.Text.Split(';');
            var ccList = this.textBox8.Text.Split(';');
            var bccList = this.textBox9.Text.Split(';');
            netMailMessage.From = new System.Net.Mail.MailAddress(this.textBox3.Text);
            foreach (var mailAddress in toList)
            {
                netMailMessage.To.Add(mailAddress);
            }
            foreach (var mailAddress in ccList)
            {
                netMailMessage.CC.Add(mailAddress);
            }
            foreach (var mailAddress in bccList)
            {
                netMailMessage.Bcc.Add(mailAddress);
            }
            try
            {
                smtp.UseDefaultCredentials = true;
                smtp.Host = this.textBox1.Text;
                smtp.Port = Convert.ToInt32(this.textBox2.Text);
                smtp.EnableSsl = this.checkBox1.Checked;
                smtp.Credentials = new System.Net.NetworkCredential(this.textBox3.Text, this.textBox4.Text);
                smtp.Send(netMailMessage);
                this.richTextBox2.Text = "smtp.Send(netMailMessage) is OK!";
            }
            catch (Exception ex)
            {
                this.richTextBox2.Text = ex.Message + "***" + ex.InnerException + "***" + ex.StackTrace;
                Console.WriteLine(ex);
            }
            finally
            {
                netMailMessage.Dispose();
                smtp.Dispose();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

    }
}
