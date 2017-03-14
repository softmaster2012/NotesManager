using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace NotesManager
{
    public partial class Form1 : Form
    {
        MD5 coder;
        string path;
        XDocument doc;
        
        public Form1()
        {
            InitializeComponent();
            coder = MD5.Create();
            path = @"..\..\accounts.xml";
            doc = XDocument.Load(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string passw = GetHash(textBox2.Text);

            if (login.Length != 0 && passw.Length != 0)
            {
                var res = from a in doc.Element("root").Elements("account")
                          where a.Attribute("login").Value == login &&
                                a.Attribute("passw").Value == passw
                          select a;
                int N = res.Count();
                if (N == 0)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка входа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Авторизация пройдена", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 f2 = new Form2(this);
                    f2.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Не заданы логин или пароль", "Ошибка входа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Reset();
            }
        }

        void Reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        string GetHash(string input)
        {
            byte[] data = coder.ComputeHash(Encoding.UTF8.GetBytes(input));            
            StringBuilder sBuilder = new StringBuilder();            
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}
