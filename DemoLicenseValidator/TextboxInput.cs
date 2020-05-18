using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoLicenseValidator
{
    public partial class TextboxInput : Form
    {
        public string Result { get; private set; }

        public TextboxInput()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Result = textBox1.Text;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
