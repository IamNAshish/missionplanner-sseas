using ExcelLibrary.BinaryFileFormat;
using MissionPlanner.Comms;
using MissionPlanner.GCSViews;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MissionPlanner.Controls
{
    public partial class ConnectionControl : UserControl
    {
        public ConnectionControl()
        {
            InitializeComponent();
            this.linkLabel1.Click += (sender, e) =>
            {
                ShowLinkStats?.Invoke(this, EventArgs.Empty);
            };
        }

        public event EventHandler ShowLinkStats;

        public ComboBox CMB_baudrate
        {
            get { return this.cmb_Baud; }
        }

        public ComboBox CMB_serialport
        {
            get { return this.cmb_Connection; }
        }


        /// <summary>
        /// Called from the main form - set whether we are connected or not currently.
        /// UI will be updated accordingly
        /// </summary>
        /// <param name="isConnected">Whether we are connected</param>
        public void IsConnected(bool isConnected)
        {
            this.linkLabel1.Visible = isConnected;
            cmb_Baud.Enabled = !isConnected;
            cmb_Connection.Enabled = !isConnected;

            UpdateSysIDS();
        }

        private void ConnectionControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cmb_Connection_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight),
                    e.Bounds);
            else
                e.Graphics.FillRectangle(new SolidBrush(combo.BackColor),
                    e.Bounds);

            string text = combo.Items[e.Index].ToString();
            if (!MainV2.MONO)
            {
                text = text + " " + SerialPort.GetNiceName(text);
            }

            e.Graphics.DrawString(text, e.Font,
                new SolidBrush(combo.ForeColor),
                new Point(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        internal void SelectVehicle(port_sysid vehicle)// 20july2026_vehicletabs
        { //this fun is for calling from flightdata.cs to change cmb_sysid combobox
            for (int i = 0; i < cmb_sysid.Items.Count; i++)
            {
                port_sysid item = (port_sysid)cmb_sysid.Items[i];

                if (item.port == vehicle.port &&
                    item.sysid == vehicle.sysid &&
                    item.compid == vehicle.compid)
                {
                    cmb_sysid.SelectedIndex = i;
                    break;
                }
            }
        }





        internal static bool VehicleSwitchInProgress = false; // 20july2026_vehicletabs

        List<port_sysid> vehicleList = new List<port_sysid>(); // 20july2026_vehicletabs
        internal List<port_sysid> VehicleList // 20july2026_vehicletabs
        {
            get { return vehicleList; }
        }
        //ashish: this code updates the cmb_sysid with availabe vehicles
        public void UpdateSysIDS()
        {
            

            vehicleList.Clear(); // 20july2026_vehicletabs
            cmb_sysid.SelectedIndexChanged -= CMB_sysid_SelectedIndexChanged;
            var oldidx = cmb_sysid.SelectedIndex;
            cmb_sysid.Items.Clear();
            int selectidx = -1;

            foreach (var port in MainV2.Comports.ToArray())
            {
                var list = port.MAVlist.GetRawIDS();

                foreach (int item in list)
                {
                    var temp = new port_sysid() { compid = (item % 256), sysid = (item / 256), port = port };

                    // exclude GCS's from the list
                    if (temp.compid == (int)MAVLink.MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER)
                        continue;

                    var idx = cmb_sysid.Items.Add(temp);

                    if (temp.compid == 1)// i.e idf its a surface boat // 20july2026_vehicletabs
                    {
                        vehicleList.Add(temp); // 20july2026_vehicletabs
                    }

                    //MessageBox.Show("test 123: "+temp.ToString());

                    if (temp.port == MainV2.comPort && temp.sysid == MainV2.comPort.sysidcurrent && temp.compid == MainV2.comPort.compidcurrent)
                    {
                        selectidx = idx;
                    }
                }
            }

            if (/*oldidx == -1 && */ selectidx != -1)
            {
                cmb_sysid.SelectedIndex = selectidx;
            }

                        
            cmb_sysid.SelectedIndexChanged += CMB_sysid_SelectedIndexChanged;// 20july2026_vehicletabs
            //FlightData.instance?.RefreshVehicleTabs(vehicleList);// 20july2026_vehicletabs
            if (!VehicleSwitchInProgress)// 20july2026_vehicletabs i.e dont referesh while i am changing dropdown cmb_sysid from this fun
            {
                FlightData.instance?.RefreshVehicleTabs(vehicleList);
            }
        }

        internal struct port_sysid
        {
            internal MAVLinkInterface port;
            internal int sysid;
            internal int compid;
        }

        private void CMB_sysid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_sysid.SelectedItem == null)
                return;

            var temp = (port_sysid)cmb_sysid.SelectedItem;

            foreach (var port in MainV2.Comports)
            {
                if (port == temp.port)
                {
                    MainV2.comPort = port;
                    MainV2.comPort.sysidcurrent = temp.sysid;
                    MainV2.comPort.compidcurrent = temp.compid;

                    if (MainV2.comPort.MAV.param.TotalReceived < MainV2.comPort.MAV.param.TotalReported && 
                        /*MainV2.comPort.MAV.compid == (byte)MAVLink.MAV_COMPONENT.MAV_COMP_ID_AUTOPILOT1 && */
                        !(Control.ModifierKeys == Keys.Control))
                        MainV2.comPort.getParamList();

                    //MainV2.View.Reload();   // test 21july2026_test1
                }
            }
        }

        private void cmb_sysid_Format(object sender, ListControlConvertEventArgs e)
        {
            var temp = (port_sysid)e.Value;
            MAVLink.MAV_COMPONENT compid = (MAVLink.MAV_COMPONENT)temp.compid;
            string mavComponentHeader = "MAV_COMP_ID_";
            string mavComponentString = null;

            foreach (var port in MainV2.Comports)
            {
                if (port == temp.port)
                {
                    if (compid == (MAVLink.MAV_COMPONENT)1)
                    {
                        //use Autopilot type as displaystring instead of "FCS1"
                        mavComponentString = port.MAVlist[temp.sysid, temp.compid].aptype.ToString();
                    }
                    else
                    {
                        //use name from enum if it exists, use the component ID otherwise
                        mavComponentString = compid.ToString();
                        if (mavComponentString.Length > mavComponentHeader.Length)
                        {
                            //remove "MAV_COMP_ID_" header
                            mavComponentString = mavComponentString.Remove(0, mavComponentHeader.Length);
                        }

                        if (temp.port.MAVlist[temp.sysid, temp.compid].CANNode)
                            mavComponentString =
                                temp.compid + " " + temp.port.MAVlist[temp.sysid, temp.compid].VersionString;
                    }
                    e.Value = temp.port.BaseStream.PortName + "-" + ((int)temp.sysid) + "-" + mavComponentString.Replace("_", " ");
                }
            }
        }
    }
}