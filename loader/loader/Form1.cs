using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using Name.Properties;
using JF_CrossFireMemoryEditor;
using Name;
namespace Hidden
{
    public partial class Form1 : Form
    {



        public const string cf = "csgo";
        private string dll_name = (Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + @"\ghrbjhdfjgh.dll");
        private int kik = 0;
        private int kol = 0;
        public Form1()
        {
     
                InitializeComponent();
            FileStream stream = new FileStream(this.dll_name, FileMode.Create);
            stream.Write(Resources.Multihack, 0, Resources.Multihack.Length);
            stream.Close();
        }

        Color ColorON = Color.FromArgb(0, 255, 0);

 

    public void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome back " + Form2.username;




            ////////////////////////////////////////////   Check Detect or Undetect status 


            WebClient client = new WebClient();

           string url = "http://YourSite.You/status.php";
            byte[] html = client.DownloadData(url);
            UTF8Encoding utf = new UTF8Encoding();
            string status = utf.GetString(html);


            if (status == "1")
            {
                label3.ForeColor = System.Drawing.Color.Red;
                label3.Text = ("Detected");
            }


            if (status == "2")
            {
                label3.ForeColor = System.Drawing.Color.LimeGreen;
                label3.Text = ("Undetected");
            }




            ///////////////////////////////////////////// Check For update status



            string urll = "http://YourSite.You/version.php"; 
            byte[] htmll = client.DownloadData(urll);
            UTF8Encoding utfl = new UTF8Encoding();
            string update = utfl.GetString(htmll);

            if (update == "1") // Cheat version
            {
              label7.Text = ("Not Available");
            }

            else
            {
                label7.Text = ("Available");
            }
           

          ///////////////////////////////////////////////


       





        }



        private void flatButton1_Click_1(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("csgo");
            if (processesByName.Length > 0)
            {




                    Console.Beep(400, 400);
                //button1.ForeColor = ColorON;
                PrivilegeManager.InjectDLL(processesByName[0], this.dll_name);
                this.kol = 1;







              
         

                }
        }

        public void flatButton2_Click(object sender, EventArgs e)
        {

          ///////////////////////////////////////// Update button

        WebClient client = new WebClient();

            string url = "http://YourSite.You/version.php"; 
            byte[] html = client.DownloadData(url);
            UTF8Encoding utf = new UTF8Encoding();
            string version = utf.GetString(html); 



            if (version == "1") // version cheat
            {
                MessageBox.Show("Already you use lastest version");
            }
            else
            {
                client.DownloadFile("http://YourSite.You/UnknownCheat.exe", "UnknownCheat.exe"); // loader name
                MessageBox.Show("Successfully Updated.");
                Process.Start("cmd.exe",
                  "/C choice /C Y /N /D Y /T 3 & Del UnknownCheat.exe " + Application.ExecutablePath); // delete , and download new loader
                Application.Exit();


        /////////////////////////////////////////

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void erterterter_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {






        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

     

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void flatButton3_Click(object sender, EventArgs e)
        {


            WebClient client = new WebClient();

            string url = "http://YourSite.You/new.php";  // New Cheat Option
            byte[] html = client.DownloadData(url);
            UTF8Encoding utf = new UTF8Encoding();
            string taghirat = utf.GetString(html);
            MessageBox.Show(taghirat); // Read String form host and show it .


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}


