using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NotesManager
{
    public partial class Form2 : Form
    {
        Form1 parent;
        
        string path1;
        string path2;
        string path3;

        XDocument doc1;
        XDocument doc2;
        XDocument doc3;
        
        public Form2(Form1 x)
        {
            InitializeComponent();
            
            this.parent = x;
            
            path1 = @"..\..\categories.xml";
            path2 = @"..\..\themes.xml";
            path3 = @"..\..\notes.xml";
            
            doc1 = XDocument.Load(path1);
            doc2 = XDocument.Load(path2);
            doc3 = XDocument.Load(path3);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (XElement c in doc1.Element("root").Elements("category"))
                comboBox1.Items.Add(c.Attribute("name").Value);
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var res = from t in doc2.Element("root").Elements("theme")
                      where t.Attribute("category").Value == comboBox1.SelectedItem.ToString()
                      select t;
            listBox1.Items.Clear();
            foreach (XElement x in res)
                listBox1.Items.Add(x.Attribute("name").Value);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var res = from n in doc3.Element("root").Elements("note")
                      where n.Attribute("theme").Value == listBox1.SelectedItem.ToString()
                      select n;
            listBox2.Items.Clear();
            foreach (XElement x in res)
                listBox2.Items.Add(x.Attribute("name").Value);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var res = from n in doc3.Element("root").Elements("note")
                      where n.Attribute("name").Value == listBox2.SelectedItem.ToString()
                      select n;
            string path = res.First().Attribute("path").Value;
            richTextBox1.LoadFile(path);
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void addThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FontX_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fd.Font;
            }
        }

        private void Color_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = cd.Color;
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
                richTextBox1.SelectedText = "";
            else
            {
                // ... отложено - определение позиции курса
            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            // - простой вариант:
            // модальное окно для олпределения искомого текста

            FindDialog fd = new FindDialog();
            List<string> list1 = new List<string>();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                int k = richTextBox1.Find(fd.SearchText);
                richTextBox1.Find(fd.SearchText);
                string textX = richTextBox1.SelectedText;
                richTextBox1.SelectionColor = Color.Red;
                //MessageBox.Show();
            }

            // - продвинутый вариант:
            // немодальное окно с мносерийным поиском при 
            // редактировании искомого текста
        }
    }
}
