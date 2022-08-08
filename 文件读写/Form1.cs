using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 文件读写
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Text = "json";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Judge_path(2,"./data");
            Judge_path(1, "./data/data.json");
            Judge_path(1, "./data/data.ini");
            Judge_path(1, "./data/data.xml");

        }
        //读取按钮
        private void button1_Click(object sender, EventArgs e)
        {
            read();
        }
        //写入按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("请将数据填写完整");
            }
            else
            {
                write_in();
            }
        }
        //重置按钮
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Judge_path(int mod, string path)
        {
            if (mod == 1)
            {
                //string path = "c:/test/1.txt"
                //判断文件是否存在
                if (!File.Exists(path))
                {
                    //创建文件
                    try
                    {
                        File.Create(path);
                        MessageBox.Show("no");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("yes");
                    }

                }
            }
            else if (mod == 2)
            {
                //string path = "c:/test"
                //判断文件夹是否存在
                if (!Directory.Exists(path))
                {
                    //创建文件夹
                    try
                    {
                        Directory.CreateDirectory(path);
                        MessageBox.Show("no");

                    }
                    catch (Exception )
                    {
                        MessageBox.Show("yes");
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "json")
            {
               // MessageBox.Show("json");
                textBox1.Text = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+ "data\\data.json";
            }
            else if(comboBox1.Text == "ini")
            {
                // MessageBox.Show("ini");
                textBox1.Text = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "data\\data.ini";
            }
            else if (comboBox1.Text == "xml")
            {
                //MessageBox.Show("xml");
                textBox1.Text = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "data\\data.xml";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        //写数据
        private void write_in()
        {
            if (comboBox1.Text=="json")
            {
                Person per = new Person();
                per.Name = textBox2.Text;
                per.Sex = textBox3.Text;
                per.Age = textBox4.Text;
                per.Height = textBox5.Text;
                per.Weight = textBox6.Text;
                string js = JsonConvert.SerializeObject(per);//序列化
                textBox1.Text = js;
            }
            else if (comboBox1.Text == "ini")
            {

            }
            else if (comboBox1.Text == "xml")
            {

            }
        }
        //读数据
        private void read()
        {
            if (comboBox1.Text == "json")
            {
                var jobject1 = JObject.Parse(textBox1.Text);
                textBox2.Text = jobject1["Name"].ToString();
                textBox3.Text = jobject1["Sex"].ToString();
                textBox4.Text = jobject1["Age"].ToString();
                textBox5.Text = jobject1["Height"].ToString();
                textBox6.Text = jobject1["Weight"].ToString();

            }
            else if (comboBox1.Text == "ini")
            {

            }
            else if (comboBox1.Text == "xml")
            {

            }
        }
    }
    public class Person
    {
        public string Age { get; set; }//年龄
        public string Weight { get; set; }//体重
        public string Height { get; set; }//身高
        public string Sex { get; set; }//性别
        public string Name { get; set; }//名字
    }

}
