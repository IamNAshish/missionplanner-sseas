using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Geometry.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MissionPlanner.Utilities.LTM;

namespace MissionPlanner.Controls
{
    public partial class HSI : MyUserControl
    {

        Bitmap _headingimage;
        bool drawnheading = false;

        int _heading = 0;
        int _navbearing = 0;

        //private Image boat = Image.FromFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"boat_top.png")); // 13may26_task1_3
        private Image boat = Properties.Resources.boat_top;

        //20may26_task1  start
        double _homebearing = 0;
        public double HomeBearing
        {
            get { return _homebearing; }

            set
            {
                _homebearing = value;
                this.Invalidate();
            }
        }

        public double VehicleLat { get; set; }
        public double VehicleLon { get; set; }
        public double HomeLat { get; set; }
        public double HomeLon { get; set; }
        //20may26_task1 end




        [System.ComponentModel.Browsable(true)]
        public int Heading
        {
            get { return _heading; }
            set { if (_heading == value) return; _heading = value; this.Invalidate(); }
        }

        [System.ComponentModel.Browsable(true)]
        public int NavHeading
        {
            get { return _navbearing; }
            set { if (_navbearing == value) return; _navbearing = value; }
        }

        /// <summary>
        /// Override to prevent offscreen drawing the control - mono mac
        /// </summary>
        public new void Invalidate()
        {
            if (Disposing)
                return;
            if (!ThisReallyVisible())
            {
                return;
            }

            base.Invalidate();
        }

        /// <summary>
        /// this is to fix a mono off screen drawing issue
        /// </summary>
        /// <returns></returns>
        public bool ThisReallyVisible()
        {
            //Control ctl = Control.FromHandle(this.Handle);
            return this.Visible;
        } 

        public HSI()
        {
            InitializeComponent();

            _headingimage = new Bitmap(this.Width, this.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int _radiusinside = (int)(Width / 3.6f);
            int _radiusoutside = (int)(Width / 2.2f);

           // drawnheading = false;

            if (drawnheading == false || this.DesignMode)
            {
                _headingimage = new Bitmap(Width, Height);

                using (Graphics g = Graphics.FromImage(_headingimage))
                {

                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //Graphics g = e.Graphics;

                    g.TranslateTransform(this.Width/2, this.Height/2);

                    int font = this.Width/14;

                    for (int a = 0; a <= 360; a += 5)
                    {
                        if (a == 0)
                        {
                            g.DrawString("N".PadLeft(2), new System.Drawing.Font(FontFamily.GenericSansSerif, font), Brushes.White,
                                new PointF(-font, -_radiusoutside));

                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 11);
                        }
                        else if (a == 90)
                        {
                            g.DrawString("E  ".PadLeft(2), new System.Drawing.Font(FontFamily.GenericSansSerif, font), Brushes.White,
                                new PointF(-font, -_radiusoutside));

                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 11);
                        }
                       
                        else if (a == 180)
                        {
                            g.DrawString("S".PadLeft(2), new System.Drawing.Font(FontFamily.GenericSansSerif, font), Brushes.White,
                                new PointF(-font, -_radiusoutside));

                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 11);
                        }
                        else if (a == 270)
                        {
                            g.DrawString("W".PadLeft(2), new System.Drawing.Font(FontFamily.GenericSansSerif, font), Brushes.White,
                                new PointF(-font, -_radiusoutside));

                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 11);
                        }
                        else if (a == 360)
                        {
                            // ignore it, as we process it at 0
                        }
                        else if ((a%30) == 0) // number labeled
                        {
                            g.DrawString((a/10).ToString("0").PadLeft(2), new System.Drawing.Font(FontFamily.GenericSansSerif, font),
                                Brushes.White, new PointF(-font, -_radiusoutside));

                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 11);
                        }
                        else if (a%10 == 0) // larger line
                        {
                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 7);
                        }
                        else if (a%5 == 0) // small line
                        {
                            g.DrawLine(Pens.White, 0, _radiusinside, 0, _radiusinside + 4);
                        }
                        

                        g.RotateTransform(5);
                    }

                    g.ResetTransform();

                    drawnheading = true;
                }
            }

            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.RotateTransform(-Heading);

            e.Graphics.DrawImage(_headingimage, new Rectangle(-Width / 2, - Height/2,Width,Height));

            e.Graphics.RotateTransform(Heading);
           


            // 13may6_task1_3 
            // this was aircraft drawing 
            //Pen or = new Pen(Color.DarkOrange,2);
            //// body
            //e.Graphics.DrawLine(or, 0, 30, 0, -10);
            //// wing
            //e.Graphics.DrawLine(or, -30, 0, 30, 0);
            ////tail
            //e.Graphics.DrawLine(or, -10, 25, 10, 25);

            //Image boat = Image.FromFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Resources\boat_top.png"));//not working // 13may26_task1_3
            
            //Image boat = Image.FromFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"boat_top.png")); //working  sent to global // 13may26_task1_3

            //13may6_task1_3  this was for aircraft
            //e.Graphics.DrawImage(
            //    boat,
            //    -20,
            //    -20,
            //    40,
            //    40);


            int boatsize = Width / 2;
            e.Graphics.DrawImage(boat,-boatsize / 2,-boatsize / 2,boatsize,boatsize);// 13may6_task1_3



            //e.Graphics.DrawLine(new Pen(Color.White,2),0,-_radiusoutside,0,-_radiusinside); // 13may26_task1_3



        //20may26_task1 start
            e.Graphics.RotateTransform((float)(HomeBearing - Heading));

            // marker radius
            int markerRadius = _radiusoutside - 30;

            // green home dot
            //e.Graphics.FillEllipse(
            //    Brushes.Lime,
            //    -6,                 // x
            //    -markerRadius - 6,  // y
            //    12,                 // width
            //    12);                // height

            // reset rotation
            e.Graphics.RotateTransform((float)-(HomeBearing - Heading));
            //double hbearing = GetBearing(0, 0, 75, 83);//(currentLat,currentLon, FlighPlanner;,homeLon);





            // calculate home bearing
            double homeBearing = GetBearing(0, 0, 50, 50);// (VehicleLat,VehicleLon,HomeLat,HomeLon);

            // rotate toward home
            e.Graphics.RotateTransform((float)(homeBearing - Heading));

            // draw green home marker
            int markerRadius1 = _radiusoutside - 50;

            e.Graphics.FillEllipse(Brushes.Lime,-6,-markerRadius1 - 6,12,12);

            //20may26_task1 end


            // reset rotation
            e.Graphics.RotateTransform((float)-(homeBearing - Heading));

            e.Graphics.RotateTransform(NavHeading - Heading); 

            Point[] headbug = new Point[7];
            headbug[0] = new Point(-5, -_radiusoutside + 0);
            headbug[1] = new Point(-5, -_radiusoutside + 4);
            headbug[2] = new Point(-3, -_radiusoutside + 4);
            headbug[3] = new Point(0, -_radiusoutside + 8);
            headbug[4] = new Point(3, -_radiusoutside + 4);
            headbug[5] = new Point(5, -_radiusoutside + 4);
            headbug[6] = new Point(5, -_radiusoutside + 0);

            //e.Graphics.DrawLines(or, headbug); // 13may26_task1_3

            //  this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            Width = Height;
            base.OnResize(e);
            this.Invalidate();
            drawnheading = false;
        }


        //20may26_task1 start adding home marker on Gheading aguge
        double ToRadians(double deg) => deg * Math.PI / 180.0;
        double ToDegrees(double rad) => rad * 180.0 / Math.PI;

        double GetBearing(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = ToRadians(lat1);
            lon1 = ToRadians(lon1);
            lat2 = ToRadians(lat2);
            lon2 = ToRadians(lon2);

            double dLon = lon2 - lon1;

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

            double brng = Math.Atan2(y, x);

            return (ToDegrees(brng) + 360) % 360;
        }
        //20may26_task1 end adding home marker on Gheading aguge


    }
}
