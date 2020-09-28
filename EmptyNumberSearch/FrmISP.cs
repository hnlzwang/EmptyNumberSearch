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
    public partial class FrmISP:Form
    {
        public event Action<string> ReturnValue;
        public FrmISP()
        {
            InitializeComponent();
            this.textBox1.Enabled=false;
            this.textBox2.Enabled=false;
            this.radioButton1.Checked=true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.radioButton1.Checked==true)
            {
                if(ReturnValue!=null)
                {
                    ReturnValue("1");
                }
            }
            if(this.radioButton2.Checked==true)
            {
                if(ReturnValue!=null)
                {
                    ReturnValue("2");
                }
            }
            if(this.radioButton3.Checked==true)
            {
                if(ReturnValue!=null)
                {
                    ReturnValue("3");
                }
            }
            if(this.radioButton4.Checked==true)
            {
                if(ReturnValue!=null)
                {
                    ReturnValue(this.textBox1.Text+this.textBox2.Text);
                }
            }
            this.Close();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton4.Checked)
            {
                this.textBox1.Enabled=true;
                this.textBox2.Enabled=true;
                this.button1.Enabled=false;
            }
            else
            {
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.button1.Enabled=true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton3.Checked)
            {
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.button1.Enabled=true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton3.Checked)
            {
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.button1.Enabled=true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton1.Checked)
            {
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.button1.Enabled=true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar!='\b')//这是允许输入退格键
            {
                if((e.KeyChar<'0')||(e.KeyChar>'9'))//这是允许输入0-9数字
                {
                    e.Handled=true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern3 = @"^1(3|4|5|6|7|8|9)\d{9}$";
            if(Regex.IsMatch(this.textBox1.Text, pattern3)&&Regex.IsMatch(this.textBox2.Text, pattern3))
            {
                this.button1.Enabled=true;
            }
            
        }
    }
}
