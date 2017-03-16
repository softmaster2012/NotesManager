using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesManager
{
    public partial class FindDialog : Form
    {
        public string SearchText { get; set; }
        
        public FindDialog()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchText = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
