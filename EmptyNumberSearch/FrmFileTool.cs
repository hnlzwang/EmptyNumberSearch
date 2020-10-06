using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmptyNumberSearch
{
    public partial class FrmFileTool:Form
    {
        public FrmFileTool()
        {
            InitializeComponent();
            this.textBox1.ReadOnly=true;
            this.textBox2.ReadOnly=true;
            this.textBox3.ReadOnly=true;
        }

        private void FrmFileTool_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description="选择所有文件存放目录";
            if(folderBrowserDialog.ShowDialog()==DialogResult.OK)
            {
                string sPath = folderBrowserDialog.SelectedPath;
                if(!sPath.EndsWith("\\"))
                {
                    sPath+="\\";
                }
                this.textBox3.Text=sPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                this.textBox1.Text=openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter="txt files (*.txt)|*.txt";
            openFileDialog.Title="选择文件";
            openFileDialog.RestoreDirectory=true;
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                this.textBox2.Text=openFileDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text=="")
            {
                MessageBox.Show("请选择主文件.");
                return;
            }
            if(this.textBox2.Text=="")
            {
                MessageBox.Show("请选择辅助文件.");
                return;
            }
            if(this.textBox3.Text=="")
            {
                MessageBox.Show("请选择存储路径.");
                return;
            }
            List<string> masters = new List<string>();
            List<string> supports = new List<string>();
            List<string> intersections = new List<string>();
            new Task(() =>
            {
                //List<Task> tasks = new List<Task>();
                using(FileStream fs = new FileStream(this.textBox1.Text, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        while(!sr.EndOfStream)
                        {
                            masters.Add(sr.ReadLine());
                        }
                    }
                }
                using(FileStream fs = new FileStream(this.textBox2.Text, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        while(!sr.EndOfStream)
                        {
                            supports.Add(sr.ReadLine());
                        }
                    }
                }
                foreach(var item in masters)
                {
                    if(supports.Contains(item))
                    {
                        intersections.Add(item);
                        supports.Remove(item);
                    }
                }
                foreach(var item in intersections)
                {
                    if(masters.Contains(item))
                    {
                        masters.Remove(item);
                    }
                }
                string fileName1 = this.textBox1.Text;
                string fileName2 = this.textBox2.Text;
                fileName1=fileName1.Substring(fileName1.LastIndexOf("\\")).Replace("\\","").Replace(".txt", "");
                fileName2=fileName2.Substring(fileName2.LastIndexOf("\\")).Replace("\\", "").Replace(".txt", "");
                string newFileName1 = this.textBox3.Text+ fileName1+"_"+fileName2+"_2交集.txt";
                string newFileName2 = this.textBox3.Text+ fileName1+"_"+fileName2+"去除交集.txt";
               // Task t1=new Task(() =>
                //{
                    using(FileStream fs = new FileStream(newFileName1, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using(StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach(var item in intersections)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                //});
               // Task t2=new Task(() =>
                //{
                    using(FileStream fs = new FileStream(newFileName2, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using(StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach(var item in masters)
                            {
                                sw.WriteLine(item);
                            }
                            foreach(var item in supports)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                //});
                //t1.Start();
                //t2.Start();
                //tasks.Add(t1);
                //tasks.Add(t2);
                //Task.WhenAll(tasks);
                MessageBox.Show("导出完成.");
            }).Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text=="")
            {
                MessageBox.Show("请选择主文件.");
                return;
            }
            if(this.textBox2.Text=="")
            {
                MessageBox.Show("请选择辅助文件.");
                return;
            }
            if(this.textBox3.Text=="")
            {
                MessageBox.Show("请选择存储路径.");
                return;
            }
            List<string> masters = new List<string>();
            List<string> supports = new List<string>();
            List<string> intersections = new List<string>();
            new Task(() =>
            {
                //List<Task> tasks = new List<Task>();
                using(FileStream fs = new FileStream(this.textBox1.Text, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        while(!sr.EndOfStream)
                        {
                            masters.Add(sr.ReadLine());
                        }
                    }
                }
                using(FileStream fs = new FileStream(this.textBox2.Text, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        while(!sr.EndOfStream)
                        {
                            supports.Add(sr.ReadLine());
                        }
                    }
                }
                foreach(var item in masters)
                {
                    if(supports.Contains(item))
                    {
                        intersections.Add(item);
                        supports.Remove(item);
                    }
                }
                foreach(var item in intersections)
                {
                    if(masters.Contains(item))
                    {
                        masters.Remove(item);
                    }
                }
                string fileName1 = this.textBox1.Text;
                string fileName2 = this.textBox2.Text;
                fileName1=fileName1.Substring(fileName1.LastIndexOf("\\")).Replace("\\", "").Replace(".txt", "");
                fileName2=fileName2.Substring(fileName2.LastIndexOf("\\")).Replace("\\", "").Replace(".txt", "");
                string newFileName1 =this.textBox3+fileName1+"_"+fileName2+"_3交集.txt";
                string newFileName2 = this.textBox3+fileName2+"去除交集.txt";
                string newFileName3 = this.textBox3+fileName1+"去除交集.txt";
                //Task t3=new Task(() =>
                //{
                    using(FileStream fs = new FileStream(newFileName3, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using(StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach(var item in masters)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                // });
                //Task t2= new Task(() =>
                //{
                using(FileStream fs = new FileStream(newFileName2, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using(StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach(var item in supports)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                // });
                //Task t1=new Task(() =>
                //{
                using(FileStream fs = new FileStream(newFileName1, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using(StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach(var item in intersections)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                //});
                //t1.Start();
                //t2.Start();
                //t3.Start();
                //tasks.Add(t1);
                //tasks.Add(t2);
                //tasks.Add(t3);
                //Task.WhenAll(tasks);
                MessageBox.Show("导出完成.");
            }).Start();
        }
    }
}
