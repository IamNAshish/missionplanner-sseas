using MissionPlanner.GCSViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissionPlanner
{
    public partial class VirtualControllerForm : Form
    {
        // 04nov2026_VjoystickPopup this windows form is for vir popup joystick
        private bool[] buttonStates = new bool[4];

        public VirtualControllerForm()
        {
            InitializeComponent();

            btn_v1_ch1.Click += (s, e) => ToggleButton(0, 1);
            btn_v2_ch2.Click += (s, e) => ToggleButton(1, 2);
            btn_v3_ch3.Click += (s, e) => ToggleButton(2, 3);
            btn_v4_ch4.Click += (s, e) => ToggleButton(3, 4);
        }

        private void ToggleButton(int index, int channel)
        {
            try
            {
                bool newState = !buttonStates[index];

                int pwmOn = 1900;
                int pwmOff = 1100;

                switch (index)
                {
                    case 0: // CH1
                        pwmOn = 1900;
                        pwmOff = 1100;
                        break;

                    case 1: // CH2
                        pwmOn = 1800;
                        pwmOff = 1200;
                        break;

                    case 2: // CH3
                        pwmOn = 2000;
                        pwmOff = 1000;
                        break;

                    case 3: // CH4
                        pwmOn = 1700;
                        pwmOff = 1300;
                        break;
                }

                int pwm = newState ? pwmOn : pwmOff;

                string code = string.Format(
                    "Script.SendRC({0}, {1}, True)",
                    channel,
                    pwm);

                FlightData.instance.RunVirtualButtonPythonCommand(code);

                buttonStates[index] = newState;

                switch (index)
                {
                    case 0: btn_v1_ch1.BackColor = newState ? Color.LightGreen : SystemColors.Control; break;
                    case 1: btn_v2_ch2.BackColor = newState ? Color.LightGreen : SystemColors.Control; break;
                    case 2: btn_v3_ch3.BackColor = newState ? Color.LightGreen : SystemColors.Control; break;
                    case 3: btn_v4_ch4.BackColor = newState ? Color.LightGreen : SystemColors.Control; break;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Failed to send RC command: " + ex.Message);
            }
        }
    }
}
