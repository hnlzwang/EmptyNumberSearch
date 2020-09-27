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
    public partial class FrmNumber:Form
    {
        public event Action<string> ReturnValue;
        public FrmNumber()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = this.textBox1.Text;
            string pattern2 = @"^(3|4|5|6|7|8|9)$";
            if(Regex.IsMatch(txt, pattern2))
            {
                this.textBox1.Text=txt;
            }
            else
            {
                this.textBox1.Text="";
            }
        }

        private void valiteNumber(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string txt = textBox.Text;
            string pattern2 = @"^\d{1}$";
            if(Regex.IsMatch(txt, pattern2))
            {
                textBox.Text=txt;
            }
            else
            {
                textBox.Text="";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            valiteNumber(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string txt1 = this.textBox1.Text;
            string txt2 = this.textBox2.Text;
            string txt3 = this.textBox3.Text;
            string txt4 = this.textBox4.Text;
            string txt5 = this.textBox5.Text;
            string txt6 = this.textBox6.Text;
            string txt7 = this.textBox7.Text;
            string txt8 = this.textBox8.Text;
            string txt9 = this.textBox9.Text;
            string txt10 = this.textBox10.Text;
            string pattern = @"^1";
            if(txt1!="")
            {
                pattern+=txt1;
            }
            else
            {
                pattern+="(3|4|5|6|7|8|9)";
            }
            if(txt2!="")
            {
                pattern+=txt2;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt3!="")
            {
                pattern+=txt3;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt4!="")
            {
                pattern+=txt4;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt5!="")
            {
                pattern+=txt5;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt6!="")
            {
                pattern+=txt6;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt7!="")
            {
                pattern+=txt7;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt8!="")
            {
                pattern+=txt8;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt9!="")
            {
                pattern+=txt9;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            if(txt10!="")
            {
                pattern+=txt10;
            }
            else
            {
                pattern+=@"\d{1}";
            }
            pattern+="$";
            if(ReturnValue!=null)
                ReturnValue(pattern);
            this.Close();
        }
    }
}
