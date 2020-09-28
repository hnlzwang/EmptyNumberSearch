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
    public partial class FrmExportSegment:Form
    {
        public event Action<string> ReturnValue;

        public FrmExportSegment()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ReturnValue!=null)
            {
                ReturnValue(this.domainUpDown1.Text);
            }
            this.Close();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            string pattern1 = @"^\d{1}$";
            if(!Regex.IsMatch(this.domainUpDown1.Text, pattern1))
            {
                this.domainUpDown1.Text="";
            }
        }
    }
}
