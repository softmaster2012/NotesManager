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
    }
}
