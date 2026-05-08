using MissionPlanner.ArduPilot.Mavlink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MAVLink;

namespace MissionPlanner.Controls
{
    public partial class VideoStreamSelector : Form
    {

        public string gstreamer_pipeline = "";

        class StreamOption
        {
            public string Name { get; set; }
            public string Pipeline { get; set; }
        }

        public VideoStreamSelector()
        {
            InitializeComponent();

            //populate the combobox with available MAVLink streams + local webcams (if available).
            //tthe gstreamer backend expects an appsink named "outsink" outputting BGRA frames.
            var options = new List<StreamOption>();

            try
            {
                options.AddRange(CameraProtocol.VideoStreams.Values.Select(x =>
                {
                    var name = Encoding.UTF8.GetString(x.name).Split('\0')[0];
                    return new StreamOption
                    {
                        Name = name,
                        Pipeline = CameraProtocol.GStreamerPipeline(x),
                    };
                }));
            }
            catch
            {
                //ignore and keep UI functional even if MAVLink stream parsing fails
            }

            try
            {
                var devices = WebCamService.Capture.getDevices();
                for (int i = 0; i < devices.Count; i++)
                {
                    var devName = devices[i];

                    //Windows: ksvideosrc is generally available in GStreamer (MinGW builds).
                    //let ksvideosrc choose default caps; we enforce BGRA for our appsink.
                    var pipeline = $"ksvideosrc device-index={i} ! videoconvert ! video/x-raw,format=BGRA ! appsink name=outsink";

                    options.Add(new StreamOption
                    {
                        Name = $"Webcam: {devName}",
                        Pipeline = pipeline,
                    });
                }
            }
            catch
            {
                //ignore DirectShow may be unavailable (non-Windows/mono builds, missing deps, etc.)
            }

            cmb_detectedstreams.DisplayMember = nameof(StreamOption.Name);
            cmb_detectedstreams.ValueMember = nameof(StreamOption.Pipeline);
            cmb_detectedstreams.DataSource = options;

            Utilities.ThemeManager.ApplyThemeTo(this);
        }

        private void but_launch_Click(object sender, EventArgs e)
        {
            if(txt_gstreamraw.Text != "")
            {
                gstreamer_pipeline = txt_gstreamraw.Text;
                DialogResult = DialogResult.OK;
            }
            
            Close();
        }

        private void cmb_detectedstreams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_detectedstreams.SelectedValue == null)
                return;
            txt_gstreamraw.Text = cmb_detectedstreams.SelectedValue.ToString();
        }
    }
}
