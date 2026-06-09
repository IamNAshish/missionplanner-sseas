using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius) // 09june26_task1
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }

        private async void Splash_Load_1(object sender, EventArgs e)
        {
            this.Region = new Region(GetRoundedRect(this.ClientRectangle, 40)); // 09june26_task1
            await Task.Delay(8000); // 8sec splash.. anyway it will closed by mainv2 gui if its ready
            this.Close();
        }
    }
}