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
    public partial class InputSegment:Form
    {
        public event Action<string> ReturnValue;
        public InputSegment()
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
            if(this.textBox1.Text.Length==7)
            {
                if(ReturnValue!=null)
                    ReturnValue(this.textBox1.Text);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = this.textBox1.Text;
            string pattern1 = @"^1$";
            string pattern2= @"^1(3|4|5|6|7|8|9)$";
            string pattern3= @"^1(3|4|5|6|7|8|9)\d{0,5}$";
            if(Regex.IsMatch(txt, pattern1)||Regex.IsMatch(txt, pattern2)||Regex.IsMatch(txt, pattern3))
            {
                this.textBox1.Text=txt;
            }
            else
            {
                if(txt.Length>=1)
                {
                    this.textBox1.Text=txt.Substring(0, txt.Length-1);
                }
                else
                {
                    this.textBox1.Text="";
                }
            }
            if(this.textBox1.Text.Length==7)
            {
                this.button1.Enabled=true;
            }
        }

        private void InputSegment_Load(object sender, EventArgs e)
        {

        }
    }
}
