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
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Mail;


namespace mail
{
    public partial class Form1 : Form
    {

        public int sayac = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                listBox1.Items.Add(textBox5.Text);
                textBox5.Text = "";
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (sayac < listBox1.Items.Count)
            {

                mailReady();
                sayac++;

            }
            else
            {
                MessageBox.Show("Eposta listesi boş bırakılmaz. Bir veya birden fazla eposta adresi ekleyin.");
            }
        }



        private void mailReady()
        {
            MailMessage mailMessage = new MailMessage();

            string user = "", subject, body, from, host = "", credent = "";
            int port = 0, timeout = 0;
            bool bodyHtml, ssl = true;


            /*for (int i = 0; i < listBox1.Items.Count; i++){
                users[i] = listBox1.Items[i].ToString();
            }*/


            if (sayac < listBox1.Items.Count)
            {
                user = listBox1.Items[sayac].ToString();
                subject = textBox4.Text;
                bodyHtml = true;
                body = richTextBox1.Text;
                from = textBox1.Text + comboBox1.Text;

                SmtpClient smtp = new SmtpClient();

                if (comboBox1.Text == "@hotmail.com")
                {
                    host = "smtp-mail.outlook.com";
                    port = 587;
                    ssl = true;
                    timeout = 190000;
                }
                else if (comboBox1.Text == "@gmail.com")
                {
                    host = "smtp.gmail.com";
                    port = 587;
                    ssl = true;

                }
                else if (comboBox1.Text == "@lnstagram.net.in")
                {
                    host = "mail.lnstagram.net.in";
                    port = 2525;
                    timeout = 190000;
                    credent = textBox1.Text + comboBox1.Text;
                    ssl = false;
                }


                try
                {
                    mailSend(user, subject, bodyHtml, body, from, host, port, timeout, credent, ssl);

                }
                catch (Exception e)
                {
                    MessageBox.Show("Bir şeyler ters gitti! Hata: " + e.ToString());
                }
            }
            else
            {
                MessageBox.Show("Tüm mailler gönderildi.");
            }
        }
        private void mailSend(string user, string subject, bool bodyHtml, string body, string from, string host, int port, int timeout, string credent, bool ssl)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            mailMessage.To.Add(user);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = bodyHtml;
            mailMessage.Body = body;
            mailMessage.From = new MailAddress(from, "Instagram");

            smtp.Host = host;
            smtp.Port = port;
            smtp.Timeout = timeout;
            smtp.Credentials = new NetworkCredential(credent, textBox2.Text);
            smtp.EnableSsl = ssl;

            try
            {
                smtp.Send(mailMessage);

                //MessageBox.Show(sayac+". Mail gönderildi!");
                mailReady();
            }
            catch (Exception e)
            {
                MessageBox.Show("Bir Mail gönderilemedi! Hata: " + e.ToString());
                mailReady();
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {

                mailReady();

            }
            else
            {
                MessageBox.Show("Eposta listesi boş bırakılmaz. Bir veya birden fazla eposta adresi ekleyin.");
            }
        }



        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hotmail ile hata almıyorum ama Gmail ile alıyorum neden?\nGmail'de bu ayarları aktif hale getirmeniz için sizi yönlendiriyorum.");
            Process.Start("https://www.google.com/settings/security/lesssecureapps");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}