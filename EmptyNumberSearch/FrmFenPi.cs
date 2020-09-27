using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmptyNumberSearch
{
    public partial class FrmFenPi:Form
    {
        public event Action<string> ReturnValue;
        public FrmFenPi()
        {
            InitializeComponent();
            this.button1.Enabled=false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Length>=1)
            {
                if(ReturnValue!=null)
                    ReturnValue(this.textBox1.Text);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = this.textBox1.Text;
            string pattern1 = @"^\d+$";
            if(Regex.IsMatch(txt, pattern1))
            {
                this.textBox1.Text=txt;
            }
            else
            {
                this.textBox1.Text="";
            }
            if(this.textBox1.Text.Length>=1)
            {
                this.button1.Enabled=true;
            }
        }
    }
}
