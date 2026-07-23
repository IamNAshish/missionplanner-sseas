using GMap.NET;
using GMap.NET.WindowsForms;
using MissionPlanner.Utilities;
using System;
using System.Drawing;
using static alglib;
//using static MissionPlanner.Controls.ConnectionControl;


namespace MissionPlanner.Maps
{
    [Serializable]
    public class GMapMarkerBoat : GMapMarkerBase
    {
        static readonly System.Drawing.Size SizeSt =
            new System.Drawing.Size(global::MissionPlanner.Maps.Resources.boat.Width,
                global::MissionPlanner.Maps.Resources.boat.Height);

        // commented orginal code under // 22july2026_colorBoatsFlightData start
        //float heading = 0;
        //float cog = -1;
        //float target = -1;
        //float nav_bearing = -1;
        // commented orginal code under // 22july2026_colorBoatsFlightData end

        // 22july2026_colorBoatsFlightData start new code
        private float heading = 0;
        private float cog = -1;
        private float target = -1;
        private float nav_bearing = -1;
        private int sysid = -1;
        // 22july2026_colorBoatsFlightData end new code

        public int Sysid { get; set; } // 22july2026_colorBoatsFlightData

        // 22july2026_colorBoatsFlightData start 
        //dont know why but these props were not public earlier
        public float Heading
        {
            get => heading;
            set => heading = value;
        }

        public float Cog
        {
            get => cog;
            set => cog = value;
        }

        public float Target
        {
            get => target;
            set => target = value;
        }

        public float Nav_bearing
        {
            get => nav_bearing;
            set => nav_bearing = value;
        }
        // 22july2026_colorBoatsFlightData end

        //public GMapMarkerBoat(PointLatLng p, float heading, float cog, float nav_bearing, float target)
        //    : base(p)
        //{
        //    this.heading = heading;
        //    this.cog = cog;
        //    this.target = target;
        //    this.nav_bearing = nav_bearing;
        //    Size = SizeSt;
        //} // commented orginal code under // 22july2026_colorBoatsFlightData


        public GMapMarkerBoat(PointLatLng p,float heading,float cog,float nav_bearing,float target,int sysid)
    : base(p) // 22july2026_colorBoatsFlightData new code
        {
            Heading = heading;
            Cog = cog;
            Target = target;
            Nav_bearing = nav_bearing;

            Sysid = sysid;

            Size = SizeSt;
        }

        public override void OnRender(IGraphics g)
        {
            if(IsHidden)
            {
                return;
            }
            
            var temp = g.Transform;
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

            g.RotateTransform(-Overlay.Control.Bearing);

            // anti NaN
            try
            {
                if (DisplayHeading)
                    g.DrawLine(new Pen(Color.Red, 2), 0.0f, 0.0f,
                        (float) Math.Cos((Heading - 90) * MathHelper.deg2rad) * length, // 22july2026_colorBoatsFlightData  orginal was -> "heading" instead of "Heading"
                        (float) Math.Sin((Heading - 90) * MathHelper.deg2rad) * length); // 22july2026_colorBoatsFlightData  orginal was -> "heading" instead of "Heading"
            } 
            catch
            {
            }

            if (DisplayNavBearing)
                g.DrawLine(new Pen(Color.Green, 2), 0.0f, 0.0f,
                    (float) Math.Cos((Nav_bearing - 90) * MathHelper.deg2rad) * length,// 22july2026_colorBoatsFlightData  orginal was -> "nav_bearing" instead of "Nav_bearing"
                    (float) Math.Sin((Nav_bearing - 90) * MathHelper.deg2rad) * length); // 22july2026_colorBoatsFlightData  orginal was -> "nav_bearing" instead of "Nav_bearing"
            if (DisplayCOG)
                g.DrawLine(new Pen(Color.Black, 2), 0.0f, 0.0f,
                    (float) Math.Cos((Cog - 90) * MathHelper.deg2rad) * length, // 22july2026_colorBoatsFlightData  orginal was -> "cog" instead of "Cog"
                    (float) Math.Sin((Cog - 90) * MathHelper.deg2rad) * length); // 22july2026_colorBoatsFlightData  orginal was -> "cog" instead of "Cog"
            if (DisplayTarget)
                g.DrawLine(new Pen(Color.Orange, 2), 0.0f, 0.0f,
                    (float) Math.Cos((Target - 90) * MathHelper.deg2rad) * length,// 22july2026_colorBoatsFlightData  orginal was -> "target" instead of "Target"
                    (float) Math.Sin((Target - 90) * MathHelper.deg2rad) * length);// 22july2026_colorBoatsFlightData  orginal was -> "target" instead of "Target"
            // anti NaN

            try
            {
                g.RotateTransform(heading);
            }
            catch
            {
            }

#if NET472_OR_GREATER
            var img = Resources.boat;
            // 22july2026_colorBoatsFlightData 

            switch (Sysid % 6)
            {
                case 1:
                    img = Resources.boat1_green;                    
                    if (IsActive)
                    {
                        // draw 20% larger or draw yellow ring or draw glow
                    }
                    break;
                case 2:
                    img = Resources.boat2_purple;
                    break;
                case 3:
                    img = Resources.boat;
                    break;
                case 4:
                    img = Resources.boat4_blue;
                    break;
                case 5:
                    img = Resources.boat5_orange;
                    break;
                default:
                    img = Resources.boat;
                    break;
            }
            g.DrawString(Sysid.ToString(), new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold), Brushes.Red, -6, -6);

            // 22july2026_colorBoatsFlightData end



            var ia = new System.Drawing.Imaging.ImageAttributes();
            if(IsTransparent)
            {
                // Draw image with transparency using a color matrix
                var cm = new System.Drawing.Imaging.ColorMatrix { Matrix33 = 0.39f };
                ia.SetColorMatrix(cm, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
            }
            g.DrawImage(img, new Rectangle(-img.Width / 2, -img.Width / 2, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
#else
            g.DrawImageUnscaled(global::MissionPlanner.Maps.Resources.boat,
                Size.Width / -2,
                Size.Height / -2);
#endif

            g.Transform = temp;
        }
    }
}