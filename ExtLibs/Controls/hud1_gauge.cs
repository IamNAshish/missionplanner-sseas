using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPlanner.Controls
{
    internal class hud1_gauge
    {
        public float Roll { get; set; }
        public float Pitch { get; set; }
        public float RollCommand { get; set; }
        public float PitchCommand { get; set; }

        //Circular clipping region
        GraphicsPath path = new GraphicsPath();
        //path.AddEllipse(0,0,Width,Height);
        //e.Graphics.SetClip(path);

        ////Draw sky
        //e.Graphics.FillRectangle(Brushes.SkyBlue,-Width,-Height, Width*3,Height*3);

        ////Apply roll
        //e.Graphics.FillRectangle(Brushes.DarkBlue, -Width, 0, Width*3,Height*3);

    }
}
