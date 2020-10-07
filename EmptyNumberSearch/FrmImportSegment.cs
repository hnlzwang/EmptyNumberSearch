using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmptyNumberSearch
{
    public partial class FrmImportSegment:Form
    {
        public event Action<List<string>> ReturnValue;
        public List<string> shiPhone = new List<string>();
        DataTable dt = new DataTable();
        int allCount = 0;
        public FrmImportSegment()
        {
            InitializeComponent();
            treeView1.CheckBoxes=false;
            treeView1.FullRowSelect=true;
            treeView1.Indent=10;
            treeView1.ItemHeight=20;
            treeView1.LabelEdit=false;
            treeView1.Scrollable=true;
            treeView1.ShowPlusMinus=true;
            treeView1.ShowRootLines=true;
            this.textBox1.Enabled=false;
            this.textBox2.Enabled=false;
            dt.Columns.Add("id");
            dt.Columns.Add("all");
            dt.Columns.Add("sheng");
            dt.Columns.Add("shi");
            dt.Columns.Add("phone");
            dt.Clear();
        }

        private void FrmImportSegment_Load(object sender, EventArgs e)
        {
            List<string> shiList = new List<string>();
            string fileName = AppDomain.CurrentDomain.BaseDirectory+"addr_data\\area_shi.txt";
            using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using(StreamReader sr=new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        shiList.Add(sr.ReadLine().ToString());
                    }
                }
            }
            
            int i = 1;
            foreach(var item in shiList)
            {
                string shengshi = item.Split('_')[0];
                string phoneSegment = item.Split('_')[1];
                allCount+=phoneSegment.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries).Length;
                string[] shengs = shengshi.Split(new string[] { "省", "区", "市" }, StringSplitOptions.RemoveEmptyEntries);
                if(shengs.Length==2)
                {
                    string sheng = shengs[0];
                    string shi = shengs[1];
                    DataRow dr = dt.NewRow();
                    dr["id"]=i;
                    dr["all"]=shengshi;
                    dr["sheng"]=sheng;
                    dr["shi"]=shi;
                    dr["phone"]=phoneSegment;
                    i++;
                    dt.Rows.Add(dr);
                }
                
            }
            TreeNode tn = new TreeNode();
            tn.Name="0";
            tn.Text="号码地区";
            tn.Expand();
            foreach(DataRow row in dt.Rows)
            {
                string strValue = row["sheng"].ToString();
                if(tn.Nodes.Count>0)
                {
                    if(!tn.Nodes.ContainsKey(strValue))
                    {
                        BindTreeData(tn, dt, strValue,"0");
                    }
                }
                else
                {
                    BindTreeData(tn, dt, strValue,"0");
                }
            }
            treeView1.Nodes.Add(tn);
            this.label1.Text="共"+allCount+"个号段";
        }

        private void BindTreeData(TreeNode tn, DataTable dtData, string strValue,string strId)
        {
            TreeNode tn1 = new TreeNode();
            tn1.Name=strValue;
            tn1.Text=strValue;
            tn.Nodes.Add(tn1);

            DataRow[] rows = dtData.Select(string.Format("sheng='{0}'", strValue));
            if(rows.Length>0)
            {
                foreach(DataRow dr in rows)
                {
                    TreeNode tn2 = new TreeNode();
                    tn2.Name=dr["id"].ToString();
                    tn2.Text=dr["shi"].ToString();
                    tn1.Nodes.Add(tn2);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string tmpValue =this.treeView1.SelectedNode.Name;
            try
            {
                string condition = "id='"+tmpValue+"'";
                DataRow[] dr = dt.Select(condition);
                if(dr!=null&&dr.Length>0)
                {
                    string segment = dr[0]["phone"].ToString();
                    shiPhone=segment.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    this.radioButton1.Checked=true;
                    this.textBox1.Enabled=false;
                    this.textBox2.Enabled=false;
                    foreach(var obj in shiPhone)
                    {
                        this.listBox1.Items.Add(obj);
                    }
                    this.label5.Text="总计:["+shiPhone.Count+"]个号段，共["+shiPhone.Count*10000+"]个号码";
                }
            }catch(Exception ex)
            {

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton1.Checked)
            {
                this.textBox1.Text="";
                this.textBox2.Text="";
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.listBox1.Items.Clear();
                foreach(var obj in shiPhone)
                {
                    this.listBox1.Items.Add(obj);
                }
                this.label5.Text="总计:["+shiPhone.Count+"]个号段，共["+shiPhone.Count*10000+"]个号码";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton2.Checked)
            {
                this.textBox1.Text="";
                this.textBox2.Text="";
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.listBox1.Items.Clear();
                int i = 0;
                foreach(var obj in shiPhone)
                {
                    string preNumber1 = obj.Substring(0, 2);
                    string preNumber2 = obj.Substring(0, 3);
                    if(FrmMain.cacheDateList.ChinaMobile.Contains(preNumber1)||FrmMain.cacheDateList.ChinaMobile.Contains(preNumber2))
                    {
                        this.listBox1.Items.Add(obj);
                        i++;
                    }
                }
                this.label5.Text="总计:["+i+"]个号段，共["+i*10000+"]个号码";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton3.Checked)
            {
                this.textBox1.Text="";
                this.textBox2.Text="";
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.listBox1.Items.Clear();
                int i = 0;
                foreach(var obj in shiPhone)
                {
                    string preNumber1 = obj.Substring(0, 2);
                    string preNumber2 = obj.Substring(0, 3);
                    if(FrmMain.cacheDateList.ChinaUnicom.Contains(preNumber1)||FrmMain.cacheDateList.ChinaUnicom.Contains(preNumber2))
                    {
                        this.listBox1.Items.Add(obj);
                        i++;
                    }
                }
                this.label5.Text="总计:["+i+"]个号段，共["+i*10000+"]个号码";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton4.Checked)
            {
                this.textBox1.Text="";
                this.textBox2.Text="";
                this.textBox1.Enabled=false;
                this.textBox2.Enabled=false;
                this.listBox1.Items.Clear();
                int i = 0;
                foreach(var obj in shiPhone)
                {
                    string preNumber1 = obj.Substring(0, 2);
                    string preNumber2 = obj.Substring(0, 3);
                    if(FrmMain.cacheDateList.ChinaTelecom.Contains(preNumber1)||FrmMain.cacheDateList.ChinaTelecom.Contains(preNumber2))
                    {
                        this.listBox1.Items.Add(obj);
                        i++;
                    }
                }
                this.label5.Text="总计:["+i+"]个号段，共["+i*10000+"]个号码";
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton5.Checked)
            {
                this.textBox1.Enabled=true;
                this.textBox2.Enabled=true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern3 = @"^1(3|4|5|6|7|8|9)\d{5}$";
            string txt1 = this.textBox1.Text;
            string txt2 = this.textBox2.Text;
            if(Regex.IsMatch(txt2, pattern3)&&Regex.IsMatch(txt1, pattern3))
            {
                int i = 0;
                this.listBox1.Items.Clear();
                foreach(var obj in shiPhone)
                {
                    if(String.Compare(obj, txt1)>=0&&String.Compare(obj, txt2)<=0)
                    {
                        this.listBox1.Items.Add(obj);
                        i++;
                    }
                }
                this.label5.Text="总计:["+i+"]个号段，共["+i*10000+"]个号码";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string pattern3 = @"^1(3|4|5|6|7|8|9)\d{5}$";
            string txt1 = this.textBox1.Text;
            string txt2 = this.textBox2.Text;
            if(Regex.IsMatch(txt2,pattern3)&&Regex.IsMatch(txt1,pattern3))
            {
                int i = 0;
                this.listBox1.Items.Clear();
                foreach(var obj in shiPhone)
                {
                    if(String.Compare(obj,txt1)>=0&&String.Compare(obj,txt2)<=0)
                    {
                        this.listBox1.Items.Add(obj);
                        i++;
                    }
                }
                this.label5.Text="总计:["+i+"]个号段，共["+i*10000+"]个号码";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar!='\b')//这是允许输入退格键
            {
                if((e.KeyChar<'0')||(e.KeyChar>'9'))//这是允许输入0-9数字
                {
                    e.Handled=true;
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(ReturnValue!=null)
            {
                var tmp = this.listBox1.SelectedItems;
                List<string> tmpList = new List<string>();
                foreach(var obj in tmp)
                {
                    tmpList.Add(obj.ToString());
                }
                ReturnValue(tmpList);
            }
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("aa");
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int tmp= this.listBox1.SelectedItems.Count;
            this.label4.Text="选中:["+tmp+"]个号段，共["+tmp*10000+"]个号码";
        }
    }
}
