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
        List<string> phoneNumbers2 = new List<string>();
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
            Task t1 = new Task(() =>
            {
                phoneNumbers=phoneNumbers.Distinct().ToList();
                this.listBox1.Items.Clear();
                foreach(string item in phoneNumbers)
                {
                    this.listBox1.Items.Add(item);
                }
                this.label1.Text="当前号码个数："+phoneNumbers.Count;
                if(phoneNumbers.Count>0)
                {
                    this.pictureBox1.Hide();
                }
                else
                {
                    this.pictureBox1.Show();
                }
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

        private void button16_Click(object sender, EventArgs e)
        {
            FrmNumber frm = new FrmNumber();
            frm.ReturnValue+=(value) =>
            {
                phoneNumbers2.Clear();
                string pattern = value;
                this.listBox2.Items.Clear();
                foreach(string item in phoneNumbers)
                {
                    if(Regex.IsMatch(item, pattern))
                    {
                        phoneNumbers2.Add(item);
                        this.listBox2.Items.Add(item);
                    }
                }

                this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                if(phoneNumbers2.Count>0)
                {
                    this.pictureBox2.Hide();
                }
                foreach(string item in phoneNumbers2)
                {
                    if(phoneNumbers.Contains(item))
                    {
                        phoneNumbers.Remove(item);
                    }
                }
                this.listBox1.Items.Clear();
                foreach(string item in phoneNumbers)
                {
                    this.listBox1.Items.Add(item);
                }
                this.label1.Text="当前号码个数："+phoneNumbers.Count;
                if(phoneNumbers.Count>0)
                {
                    this.pictureBox1.Hide();
                }
                else
                {
                    this.pictureBox1.Show();
                }
            };
            frm.ShowDialog(this);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FrmNumber frm = new FrmNumber();
            frm.ReturnValue+=(value) =>
            {
                string pattern = value;
                List<string> tmpList = new List<string>();
                foreach(string item in phoneNumbers2)
                {
                    if(Regex.IsMatch(item, pattern))
                    {
                        if(!phoneNumbers.Contains(item))
                        {
                            phoneNumbers.Add(item);
                            this.listBox1.Items.Add(item);
                            tmpList.Add(item);
                        }
                    }
                }
                this.label1.Text="当前号码个数："+phoneNumbers.Count;
                if(phoneNumbers.Count>0)
                {
                    this.pictureBox1.Hide();
                }
                else
                {
                    this.pictureBox1.Show();
                }
                foreach(string item in tmpList)
                {
                    if(phoneNumbers2.Contains(item))
                    {
                        phoneNumbers2.Remove(item);
                    }
                }
                this.listBox2.Items.Clear();
                this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                if(phoneNumbers2.Count>0)
                {
                    this.pictureBox2.Hide();
                }
                else
                {
                    this.pictureBox2.Show();
                }
            };
            frm.ShowDialog(this);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            phoneNumbers2.Clear();
            this.listBox2.Items.Clear();
            Task t1 = new Task(() =>
              {
                  foreach(string item in phoneNumbers)
                  {
                      phoneNumbers2.Add(item);
                      this.listBox2.Items.Add(item);
                  }
                  this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                  if(phoneNumbers2.Count>0)
                  {
                      this.pictureBox2.Hide();
                  }
                  else
                  {
                      this.pictureBox2.Show();
                  }
                  phoneNumbers.Clear();
                  this.listBox1.Items.Clear();
                  this.label1.Text="当前号码个数：0个";
                  this.pictureBox1.Show();
              });
            t1.Start();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            phoneNumbers.Clear();
            this.listBox1.Items.Clear();
            Task t1 = new Task(() =>
            {
                foreach(string item in phoneNumbers2)
                {
                    phoneNumbers.Add(item);
                    this.listBox1.Items.Add(item);
                }
                this.label1.Text="当前号码个数："+phoneNumbers.Count;
                if(phoneNumbers.Count>0)
                {
                    this.pictureBox1.Hide();
                }
                else
                {
                    this.pictureBox1.Show();
                }
                phoneNumbers2.Clear();
                this.listBox2.Items.Clear();
                this.label6.Text="当前号码个数：0个";
                this.pictureBox2.Show();
            });
            t1.Start();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            phoneNumbers2.Clear();
            this.listBox2.Items.Clear();
            this.pictureBox2.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            Task t1 = new Task(() =>
            {
                Util.shuffle(ref phoneNumbers2);
                foreach(string item in phoneNumbers2)
                {
                    this.listBox2.Items.Add(item);
                }
                this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                this.pictureBox2.Hide();
            });
            t1.Start();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            Task t1 = new Task(() =>
            {
                phoneNumbers2.Sort();
                foreach(string item in phoneNumbers2)
                {
                    this.listBox2.Items.Add(item);
                }
                this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                this.pictureBox2.Hide();
            });
            t1.Start();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Task t1 = new Task(() =>
            {
                phoneNumbers2=phoneNumbers2.Distinct().ToList();
                this.listBox2.Items.Clear();
                foreach(string item in phoneNumbers2)
                {
                    this.listBox2.Items.Add(item);
                }
                this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                if(phoneNumbers2.Count>0)
                {
                    this.pictureBox2.Hide();
                }
                else
                {
                    this.pictureBox2.Show();
                }
            });
            t1.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                Task t1 = new Task(() =>
                  {
                      string filePath = openFileDialog.FileName;
                      using(StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                      {
                          while(!sr.EndOfStream)
                          {
                              string phoneNumber = sr.ReadLine();
                              string pattern = @"^1(3|4|5|6|7|8|9)\d{5,9}$";
                              if(Regex.IsMatch(phoneNumber, pattern))
                              {
                                  if(phoneNumbers.Contains(phoneNumber))
                                  {
                                      phoneNumbers.Remove(phoneNumber);
                                  }
                              }
                          }
                      }
                      this.listBox1.Items.Clear();
                      foreach(string item in phoneNumbers)
                      {
                          this.listBox1.Items.Add(item);
                      }
                      this.label1.Text="当前号码个数："+phoneNumbers.Count;
                      if(phoneNumbers.Count>0)
                      {
                          this.pictureBox1.Hide();
                      }
                      else
                      {
                          this.pictureBox1.Show();
                      }
                  });
                t1.Start();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string tmpString = String.Join(",", phoneNumbers2.ToArray());
            if(!String.IsNullOrEmpty(tmpString))
            {
                Clipboard.SetText(tmpString);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string tmpString = String.Join(",", phoneNumbers.ToArray());
            if(!String.IsNullOrEmpty(tmpString))
            {
                Clipboard.SetText(tmpString);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                Task t1 = new Task(() =>
                {
                    string filePath = openFileDialog.FileName;
                    using(StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                    {
                        while(!sr.EndOfStream)
                        {
                            string phoneNumber = sr.ReadLine();
                            string pattern = @"^1(3|4|5|6|7|8|9)\d{5,9}$";
                            if(Regex.IsMatch(phoneNumber, pattern))
                            {
                                if(phoneNumbers2.Contains(phoneNumber))
                                {
                                    phoneNumbers2.Remove(phoneNumber);
                                }
                            }
                        }
                    }
                    this.listBox2.Items.Clear();
                    foreach(string item in phoneNumbers)
                    {
                        this.listBox2.Items.Add(item);
                    }
                    this.label6.Text="当前号码个数："+phoneNumbers2.Count;
                    if(phoneNumbers2.Count>0)
                    {
                        this.pictureBox2.Hide();
                    }
                    else
                    {
                        this.pictureBox2.Show();
                    }
                });
                t1.Start();
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            List<string> tmpList = new List<string>();
            if(this.radioButton1.Checked==true)
            {
                tmpList=phoneNumbers2;
            }
            if(this.radioButton2.Checked==true)
            {
                tmpList=phoneNumbers;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter="txt files (*.txt)|*.txt";
            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                try
                {
                    using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach(string item in tmpList)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                    MessageBox.Show("导出完成");
                    
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            FrmFenPi frm = new FrmFenPi();
            frm.ReturnValue+=(value) =>
            {
                List<string> tmpList = new List<string>();
                if(this.radioButton1.Checked==true)
                {
                    tmpList=phoneNumbers2;
                }
                if(this.radioButton2.Checked==true)
                {
                    tmpList=phoneNumbers;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter="txt files (*.txt)|*.txt";
                if(saveFileDialog.ShowDialog()==DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    try
                    {
                        using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            using(StreamWriter sw = new StreamWriter(fs))
                            {
                                
                            }
                        }
                        int i = 1;
                        int j = 1;
                        string oldFileName = fileName;
                        foreach(string item in tmpList)
                        {
                            fileName=oldFileName.Replace(".txt", "第"+j+"次.txt");
                            using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                using(StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.WriteLine(item);
                                    i++;
                                    if(i%Convert.ToInt32(value)==0)
                                    {
                                        j++;
                                    }
                                }
                            }
                        }
                        MessageBox.Show("导出完成");

                    }
                    catch(Exception ex)
                    {

                    }
                }
            };
            frm.ShowDialog(this);
        }
    }
}
