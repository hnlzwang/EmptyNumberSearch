using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmptyNumberSearch
{
    public partial class FrmCompose:Form
    {
        public event Action<List<string>> ReturnValue;
        public string[] patterns = new string[13];
        int i1 = 0, i2 = 0, i3 = 0;
        public FrmCompose()
        {
            InitializeComponent();
            //this.button1.Enabled=false;
            this.comboBox1.Enabled=false;
            this.comboBox2.Enabled=false;
            this.comboBox3.Enabled=false;
            this.textBox1.Enabled=false;
            for(int i=0;i<patterns.Length;i++)
            {
                patterns[i]="";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCompose_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string returnValue = "";
            List<string> tmp1 = new List<string>();
            if(ReturnValue!=null)
            {
                for(int i = 0; i<patterns.Length; i++)
                {
                    string tmp = patterns[i];
                    if(!String.IsNullOrEmpty(tmp))
                    {
                        tmp1.Add(tmp);
                    }
                }
                ReturnValue(tmp1);
                this.Close();
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
            patterns[0]=this.textBox1.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked==true)
            {
                this.textBox1.Enabled=true;
                patterns[0]=this.textBox1.Text;
                this.button1.Enabled=true;
            }
            else
            {
                patterns[0]="";
                this.textBox1.Enabled=false;
                btnConfireEnable();
            }
        }

        private void btnConfireEnable()
        {
            bool flag = false;
            foreach(Control c in this.groupBox1.Controls)
            {
                if(c.GetType()==typeof(CheckBox)){
                    if(((CheckBox)c).Checked==true)
                    {
                        flag=true;
                    }
                }
            }
            if(flag)
            {
                this.button1.Enabled=true;
            }
            else
            {
                this.button1.Enabled=false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox2.Checked==true)
            {
                this.comboBox1.Enabled=true;
                patterns[1]="(?:0(?=1)|1(?=2)|2(?=3)|3(?=4)|4(?=5)|5(?=6)|6(?=7)|7(?=8)|8(?=9)){2}|(?:9(?=8)|8(?=7)|7(?=6)|6(?=5)|5(?=4)|4(?=3)|3(?=2)|2(?=1)|1(?=0)){2}";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[1]="";
                this.comboBox1.Enabled=false;
                btnConfireEnable();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox3.Checked)
            {
                this.comboBox2.Enabled=true;
                this.comboBox3.Enabled=true;
                i2 = Convert.ToInt32(this.comboBox2.SelectedIndex);
                i3 = Convert.ToInt32(this.comboBox3.SelectedIndex);
                patterns[2]=@"(?=["+i3+"])["+i3+"]{"+i2+"}";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[2]="";
                this.comboBox2.Enabled=false;
                this.comboBox3.Enabled=false;
                btnConfireEnable();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox4.Checked)
            {
                patterns[3]=@"([\d])\1{2,2}$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[3]="";
                btnConfireEnable();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox5.Checked)
            {
                patterns[4]=@"([\d])\1{3,3}$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[4]="";
                btnConfireEnable();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox6.Checked)
            {
                patterns[5]=@"([\d])\1{4,4}$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[5]="";
                btnConfireEnable();
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox7.Checked)
            {
                patterns[6]=@"([\d])\1{5,5}$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[6]="";
                btnConfireEnable();
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox8.Checked)
            {
                patterns[7]=@"([\d])(?!\1)([\d])\1\2$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[7]="";
                btnConfireEnable();
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox9.Checked)
            {
                patterns[8]=@"([\d])\1(?!\1)([\d])\2$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[8]="";
                btnConfireEnable();
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox10.Checked)
            {
                patterns[9]=@"([\d])(?!\1)([\d])(?!(\1|\2))([\d])(?!(\1|\2|\4))([\d])$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[9]="";
                btnConfireEnable();
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox11.Checked)
            {
                patterns[10]=@"([\d])(?!\1)([\d])(?!(\1|\2))([\d])(?!(\1|\2|\4))([\d])(?!(\1|\2|\4|\6))([\d])$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[10]="";
                btnConfireEnable();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i1=Convert.ToInt32(this.comboBox1.SelectedIndex)+2;
            patterns[1]="(?:0(?=1)|1(?=2)|2(?=3)|3(?=4)|4(?=5)|5(?=6)|6(?=7)|7(?=8)|8(?=9)){"+i1+"}|(?:9(?=8)|8(?=7)|7(?=6)|6(?=5)|5(?=4)|4(?=3)|3(?=2)|2(?=1)|1(?=0)){"+i1+"}";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            i2=Convert.ToInt32(this.comboBox2.SelectedIndex)+2;
            i3=Convert.ToInt32(this.comboBox3.SelectedIndex);
            patterns[2]=@"(?=["+i3+"])["+i3+"]{"+i2+"}";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            i2=Convert.ToInt32(this.comboBox2.SelectedIndex)+2;
            i3=Convert.ToInt32(this.comboBox3.SelectedIndex);
            patterns[2]=@"(?=["+i3+"])["+i3+"]{"+i2+"}";
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox12.Checked)
            {
                patterns[11]=@"([\d])(?!\1)([\d])(?!(\1|\2))([\d])(?!(\1|\2|\4))([\d])(?!(\1|\2|\4|\6))([\d])(?!(\1|\2|\4|\6|\8))([\d])$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[11]="";
                btnConfireEnable();
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox13.Checked)
            {
                patterns[12]=@"([\d])(?!\1)([\d])(?!(\1|\2))([\d])(?!(\1|\2|\4))([\d])(?!(\1|\2|\4|\6))([\d])(?!(\1|\2|\4|\6|\8))([\d])(?!(\1|\2|\4|\6|\8|\10))([\d])$";
                this.button1.Enabled=true;
            }
            else
            {
                patterns[12]="";
                btnConfireEnable();
            }
        }
    }
}
