using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockPlot
{
    public partial class FavoritesAdd : Form
    {
        public string result;

        public FavoritesAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                result = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
