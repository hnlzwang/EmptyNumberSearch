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
    public partial class FrmArea:Form
    {
        public event Action<int> ReturnValue;
        public FrmArea()
        {
            InitializeComponent();
            this.radioButton1.Checked=true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.radioButton1.Checked)
            {
                ReturnValue(1);
            }
            if(this.radioButton2.Checked)
            {
                ReturnValue(2);
            }
            this.Close();
        }
    }
}
