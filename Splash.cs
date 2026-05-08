using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner
{
    public partial class Splash : Form
    {
       
        public Splash()
        {
            InitializeComponent();

            string strVersion = typeof(Splash).GetType().Assembly.GetName().Version.ToString();

            //Ashishhh
            //TXT_version.Text = "Version: " + Application.ProductVersion; // +" Build " + strVersion;

            Console.WriteLine("\n\n\n\n\n"+strVersion+ "\n\n\n\n\n");

            if (Program.Logo != null)
            {
                //Ashishhh
                //pictureBox1.BackgroundImage = MissionPlanner.Properties.Resources.bgdark;
                //pictureBox1.Image = Program.Logo;
                //pictureBox1.Visible = true;
            }
            

        Console.WriteLine("Splash .ctor");
        }

        private async void Splash_Load_1(object sender, EventArgs e)
        {
            await Task.Delay(8000); // 8sec splash.. anyway it will closed by mainv2 gui if its ready
            this.Close();
        }
    }
}