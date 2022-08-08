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
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace 文件读写
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Text = "json";
            Judge_path();
        }
        string write_data;
        private void Form1_Load(object sender, EventArgs e)
        {
            read();
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
                write_in(comboBox1.Text,textBox2.Text ,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);
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

        private void Judge_path()
        {
            if (!Directory.Exists("./data"))
            {
                    //创建文件夹
            try
               {
                    Directory.CreateDirectory("./data");
               }
            catch (Exception)
                {
                }
            }

            if (!File.Exists(@"./data/data.json"))
            {
                    //默认数据
                write_in("json","json", "男", "18", "175", "55");
            }
                
            if (!File.Exists(@"./data/data.ini"))
            {
                    //默认数据
                write_in("ini","ini", "女", "20", "175", "55");
            }
            if (!File.Exists(@"./data/data.xml"))
            {
                   
                write_in("xml","xml", "男", "18", "175", "55");

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
        //写数据
        private void write_in(string leixinb , string a1, string a2, string a3, string a4, string a5)
        {
            if (leixinb == "json")
            {
                Person per = new Person();
                per.Name = a1;
                per.Sex = a2;
                per.Age = a3;
                per.Height = a4;
                per.Weight = a5;
                write_data = JsonConvert.SerializeObject(per);//序列化
                File.AppendAllText("./data/data.json", write_data);
                File.WriteAllText("./data/data.json", write_data, System.Text.Encoding.UTF8);

            }
            else if (leixinb == "ini")
            {
                INIWrite("名","姓名",a1, "./data/data.ini");
                INIWrite("名", "性别", a2, "./data/data.ini");
                INIWrite("名", "年龄", a3, "./data/data.ini");
                INIWrite("名", "身高", a4, "./data/data.ini");
                INIWrite("名", "体重", a5, "./data/data.ini");
            }
            else if (leixinb == "xml")
            {
                var doc = new XDocument(new XElement("Contacts",
                    new XElement("Contact",
                    new XElement("Name", a1),
                    new XElement("Sex", a2),
                    new XElement("Age", a3),
                    new XElement("Height", a4),
                    new XElement("Weight", a5)
                        )
                    )
                );
                doc.Save("./data/data.xml");
            }
        }
        //读数据
        private void read()
        {
            if (comboBox1.Text == "json")
            {
                var jobject1 = JObject.Parse(GetJsonFile("./data/data.json"));
                textBox2.Text = jobject1["Name"].ToString();
                textBox3.Text = jobject1["Sex"].ToString();
                textBox4.Text = jobject1["Age"].ToString();
                textBox5.Text = jobject1["Height"].ToString();
                textBox6.Text = jobject1["Weight"].ToString();

            }
            else if (comboBox1.Text == "ini")
            {
                if(File.ReadAllText("./data/data.ini") == "")
                {
                    write_in("ini","test","女","18","175","55");
                }
                textBox2.Text = INIRead("名", "姓名",textBox1.Text);
                textBox3.Text = INIRead("名", "性别",textBox1.Text);
                textBox4.Text = INIRead("名", "年龄",textBox1.Text);
                textBox5.Text = INIRead("名", "身高",textBox1.Text);
                textBox6.Text = INIRead("名", "体重",textBox1.Text);

            }
            else if (comboBox1.Text == "xml")
            {
                DataSet ds = new DataSet();
                ds.ReadXml("./data/data.xml");
                //读取第一条数据的节点
                textBox2.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                textBox3.Text = ds.Tables[0].Rows[0]["Sex"].ToString();
                textBox4.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                textBox5.Text = ds.Tables[0].Rows[0]["Height"].ToString();
                textBox6.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
            }
        }
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string section,string key,string val,string filepath);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section,string key,string defval,StringBuilder retval,int size,string filepath);
        /// 写入INI的方法
        public void INIWrite(string section, string key, string value, string path)
        {
            // section=配置节点名称，key=键名，value=返回键值，path=路径
            WritePrivateProfileString(section, key, value, path);
        }

        //读取INI的方法
        public string INIRead(string section, string key, string path)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();

        }

        private string GetJsonFile(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
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
