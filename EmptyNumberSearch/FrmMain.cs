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
    public partial class FrmMain:Form
    {
        List<string> phoneNumbers = new List<string>();
        Dictionary<string, string> shengSegment = new Dictionary<string, string>();
        Dictionary<string, string> shiSegment = new Dictionary<string, string>();
        public FrmMain()
        {
            InitializeComponent();
            this.pictureBox1.Show();
            Control.CheckForIllegalCrossThreadCalls=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://khkjc.com");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                phoneNumbers.Clear();
                Task t1 = new Task(() =>
                  {
                      using(StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                      {
                          while(!sr.EndOfStream)
                          {
                              string phoneNumber = sr.ReadLine();
                              string pattern = @"^1(3|4|5|6|7|8|9)\d{9}$";
                              if(Regex.IsMatch(phoneNumber, pattern))
                              {
                                  phoneNumbers.Add(phoneNumber);
                                  this.listBox1.Items.Add(phoneNumber);
                                  
                              }
                          }
                      }
                      this.label1.Text="当前号码个数："+phoneNumbers.Count;
                      this.pictureBox1.Hide();
                  });
                t1.Start();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                phoneNumbers.Clear();
                Task t1 = new Task(() =>
                {
                    using(StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        while(!sr.EndOfStream)
                        {
                            string phoneNumber = sr.ReadLine();
                            string pattern = @"^1(3|4|5|6|7|8|9)\d{5}$";
                            if(Regex.IsMatch(phoneNumber, pattern))
                            {
                                for(int i = 0; i<=9999; i++)
                                {
                                    string pn = phoneNumber+i.ToString("0000");
                                    phoneNumbers.Add(pn);
                                    this.listBox1.Items.Add(pn);
                                }
                            }
                        }
                    }
                    this.label1.Text="当前号码个数："+phoneNumbers.Count;
                    this.pictureBox1.Hide();
                });
                t1.Start();
                    
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InputSegment frm = new InputSegment();
            frm.ReturnValue+=(value) =>
            {
                phoneNumbers.Clear();
                string pattern = @"^1(3|4|5|6|7|8|9)\d{5}$";
                if(Regex.IsMatch(value, pattern))
                {
                    for(int i = 0; i<=9999; i++)
                    {
                        string pn = value+i.ToString("0000");
                        phoneNumbers.Add(pn);
                        this.listBox1.Items.Add(pn);
                    }
                }
                this.label1.Text="当前号码个数："+phoneNumbers.Count;
                this.pictureBox1.Hide();
            };
            frm.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            Task t1 = new Task(() =>
              {
                  Util.shuffle(ref phoneNumbers);
                  foreach(string item in phoneNumbers)
                  {
                      this.listBox1.Items.Add(item);
                  }
                  this.label1.Text="当前号码个数："+phoneNumbers.Count;
                  this.pictureBox1.Hide();
              });
            t1.Start();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            Task t1 = new Task(() =>
            {
                List<string> newList = new List<string>();
                foreach(string item in phoneNumbers)
                {
                    if(!newList.Contains(item))
                    {
                        newList.Add(item);
                    }
                }
                this.listBox1.Items.Clear();
                foreach(string item in newList)
                {
                    this.listBox1.Items.Add(item);
                }
                phoneNumbers.Clear();
                phoneNumbers=newList;
                this.label1.Text="当前号码个数："+newList.Count;
                this.pictureBox1.Hide();
            });
            t1.Start();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox1.Hide();
            this.pictureBox1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                Task t1 = new Task(() =>
                {
                    this.listBox1.Items.Clear();
                    string[] tmp = clipboardText.Split(new char[2] {',','\r' });
                    foreach(string item in tmp)
                    {
                        string phoneNumber = item.Trim(new char[3] { ',', '\r','\n' });
                        string pattern = @"^1(3|4|5|6|7|8|9)\d{9}$";
                        if(Regex.IsMatch(phoneNumber, pattern))
                        {
                             this.listBox1.Items.Add(phoneNumber);
                            phoneNumbers.Add(phoneNumber);
                        }
                    }
                    this.label1.Text="当前号码个数："+phoneNumbers.Count;
                    this.pictureBox1.Hide();
                });
                t1.Start();
            }
        }
    }
}
