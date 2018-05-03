using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hidden;//
using System.Windows.Forms;
using Name;
using System.IO;
using System.Net;
using System.Management;
using JF_CrossFireMemoryEditor;
using System.Diagnostics;
namespace Name
{
    public partial class Form2 : Form
    {
 


        public static string username = "";
       
        public Form2()
        {
  
            InitializeComponent();
        }
        private string Crypt(string text)
        {
            string rtnStr = string.Empty;
            foreach (char c in text) 
            {
                rtnStr += (char)((int)c ^ 1);
            }
            return rtnStr; 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                string lol1 = (Crypt(diskLetter.ToString()));
                textBox1.Text = lol1;

            }
            catch (Exception)
            {
                textBox1.Text = "Error to generate SYS code!";
            }
        }


        private void flatButton1_Click(object sender, EventArgs e)
        {
        string PolucheniiNomer = "";
        username = textBox2.Text;

            bool catchRun = false; 
            try
            {
                String password = "log=admin&pas=admin";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://unknowncheat.maxmizban.com/login.php");
                request.UserAgent = "Opera/9.80";
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] EncodedPostParams = Encoding.ASCII.GetBytes(password);
                request.ContentLength = EncodedPostParams.Length;
                request.GetRequestStream().Write(EncodedPostParams, 0, EncodedPostParams.Length);
                request.GetRequestStream().Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string html = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("windows-1251")).ReadToEnd();
                string[] stringSeparators = new string[] { "\n" };
                string[] result = html.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);


                

                foreach (string stroka in result)
                {
                    if (stroka.IndexOf(textBox2.Text) != -1)
                    {
                        string[] NashaStroka = stroka.ToString().Split((Convert.ToChar("|")));

                        string reLoL0 = (Crypt(NashaStroka[1].ToString()));
                        PolucheniiNomer = reLoL0.ToString();
                        
                    }
                }
            }
            catch (IndexOutOfRangeException ex2) {

                MessageBox.Show("Please enter a username" ,"Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                catchRun = true; 
            }
            string HoldingAdress = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                HoldingAdress = diskLetter;

            }
            catch (Exception)
            {
                MessageBox.Show("Run as admin program", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
/////////////////////////////////////
           
////////////////////////////////////
            if (PolucheniiNomer == HoldingAdress)
            {
                MessageBox.Show("Successfully logged in", "Login" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.Beep(400, 210);
                this.Hide();
                Form1 frm;
                frm = new Form1();
                frm.ShowDialog();
                frm.Dispose();
            }
            else if(!catchRun)
            {
                MessageBox.Show("Username not available", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        

        private void flatButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Telegram : @UnknownCheat_Bot", "Support", MessageBoxButtons.OK);
            Process.Start("http://t.me/unknowncheat_bot");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
