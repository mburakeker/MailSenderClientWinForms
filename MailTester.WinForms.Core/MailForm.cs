﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailTester.WinForms.Core
{
    public partial class MailForm : Form
    {
        public MailForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage netMailMessage = new System.Net.Mail.MailMessage();
            try
            {
                
                netMailMessage.IsBodyHtml = true;
                netMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                netMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                netMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                netMailMessage.Subject = this.textBox6.Text;
                netMailMessage.Body = this.richTextBox1.Text;
                if (this.textBox5.Text.Length > 0)
                {
                    var toList = this.textBox5.Text.Split(';');
                    foreach (var mailAddress in toList)
                    {
                        netMailMessage.To.Add(mailAddress);
                    }
                }

                if (this.textBox8.Text.Length > 0)
                {
                    var ccList = this.textBox8.Text.Split(';');
                    foreach (var mailAddress in ccList)
                    {
                        netMailMessage.CC.Add(mailAddress);
                    }
                }

                if (this.textBox9.Text.Length > 0)
                {
                    var bccList = this.textBox9.Text.Split(';');
                    foreach (var mailAddress in bccList)
                    {
                        netMailMessage.Bcc.Add(mailAddress);
                    }
                }
                    
                netMailMessage.From = new System.Net.Mail.MailAddress(this.textBox3.Text);

                smtp.UseDefaultCredentials = true;
                smtp.Host = this.textBox1.Text;
                smtp.Port = Convert.ToInt32(this.textBox2.Text);
                smtp.EnableSsl = this.checkBox1.Checked;
                smtp.Credentials = new System.Net.NetworkCredential(this.textBox3.Text, this.textBox4.Text);
                await smtp.SendMailAsync(netMailMessage);
                this.richTextBox2.Text = "smtp.Send(netMailMessage) is OK!";
            }
            catch (Exception ex)
            {
                this.richTextBox2.Text = ex.Message + "***" +ex.InnerException + "***" + ex.StackTrace;
                Console.WriteLine(ex);
            }
            //finally
            //{
            //    netMailMessage.Dispose();
            //    smtp.Dispose();
            //}
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }
    }
}
