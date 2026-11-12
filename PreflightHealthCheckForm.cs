using ExifLibrary;
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
{  // 07nov2026_preflightHealth this form is added for 07nov2026_preflightHealth task
    public partial class PreflightHealthCheckForm : Form
    {
        private Label lblVehicle;
        private Label lblOverallStatus;

        private Label lblGPS;
        private Label lblSatellites;
        private Label lblBattery;
        private Label lblFuel;
        private Label lblEngine;
        private Label lblEngineTemp;
        private Label lblWaterTemp;
        private Label lblSilencerTemp;

        
        public PreflightHealthCheckForm(int sysid)
        {
            InitializeComponent();
            InitializeHealthCheckUI(sysid);

            UpdateGPSStatus();
            UpdateSatelliteCount();
            UpdateBatteryStatus();
            UpdateFuelStatus();
            UpdateEngineStatus();

            UpdateTemperatureStatus("MAV_ET", lblEngineTemp);
            UpdateTemperatureStatus("MAV_WT", lblWaterTemp);
            UpdateTemperatureStatus("MAV_ST", lblSilencerTemp);

            UpdateOverallStatus();
        }

        private void InitializeHealthCheckUI(int sysid)
        {
            Text = "Preflight Health Check";
            Size = new Size(480, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.WhiteSmoke;

            var title = new Label
            {
                Text = "PREFLIGHT HEALTH CHECK",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                Location = new Point(20, 15),
                Size = new Size(420, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(title);

            lblVehicle = new Label
            {
                Text = "Vehicle SYSID: " + sysid,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(25, 55),
                Size = new Size(400, 25)
            };

            Controls.Add(lblVehicle);

            int y = 100;

            lblGPS = AddHealthRow("GPS Signal", ref y);
            lblSatellites = AddHealthRow("Satellites", ref y);
            lblBattery = AddHealthRow("Battery", ref y);
            lblFuel = AddHealthRow("Fuel", ref y);
            lblEngine = AddHealthRow("Engine", ref y);
            lblEngineTemp = AddHealthRow("Engine Temperature", ref y);
            lblWaterTemp = AddHealthRow("Water Temperature", ref y);
            lblSilencerTemp = AddHealthRow("Silencer Temperature", ref y);

            lblOverallStatus = new Label
            {
                Text = "CHECKS PENDING",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Location = new Point(20, 345),
                Size = new Size(420, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(lblOverallStatus);

            var btnClose = new Button
            {
                Text = "CLOSE",
                Size = new Size(100, 35),
                Location = new Point(180, 400)
            };

            btnClose.Click += (s, e) => Close();

            Controls.Add(btnClose);
        }        
        private Label AddHealthRow(string parameter, ref int y)
        {
            var name = new Label
            {
                Text = parameter,
                Location = new Point(30, y),
                Size = new Size(220, 25),
                Font = new Font("Segoe UI", 9)
            };

            var value = new Label
            {
                Text = "N/A",
                Location = new Point(260, y),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Gray
            };

            Controls.Add(name);
            Controls.Add(value);

            y += 30;

            return value;
        }

        private void UpdateGPSStatus()
        {
            var mav = MainV2.comPort?.MAV;

            if (mav == null)
            {
                lblGPS.Text = "No Vehicle Selected";
                lblGPS.BackColor = Color.Gray;
                lblGPS.ForeColor = Color.White;
                return;
            }

            float gpsStatus = mav.cs.gpsstatus;
            switch (gpsStatus)
            {
                case 0:
                    lblGPS.Text = "No GPS";
                    lblGPS.BackColor = Color.FromArgb(200, 0, 0);
                    break;

                case 1:
                    lblGPS.Text = "No Fix";
                    lblGPS.BackColor = Color.FromArgb(200, 0, 0);
                    break;

                case 2:
                    lblGPS.Text = "2D Fix";
                    lblGPS.BackColor = Color.FromArgb(255, 182, 0);
                    break;

                case 3:
                    lblGPS.Text = "3D Fix";
                    lblGPS.BackColor = Color.LimeGreen;
                    break;

                case 4:
                    lblGPS.Text = "DGPS";
                    lblGPS.BackColor = Color.LimeGreen;
                    break;

                case 5:
                    lblGPS.Text = "RTK Float";
                    lblGPS.BackColor = Color.FromArgb(127, 111, 248);
                    break;

                case 6:
                    lblGPS.Text = "RTK Fixed";
                    lblGPS.BackColor = Color.FromArgb(127, 111, 248);
                    break;

                default:
                    lblGPS.Text = "Unknown";
                    lblGPS.BackColor = Color.Gray;
                    break;
            }

            lblGPS.ForeColor = Color.White;
        }

        private void UpdateSatelliteCount()
        {
            var mav = MainV2.comPort?.MAV;

            if (mav == null)
            {
                lblSatellites.Text = "No Vehicle";
                lblSatellites.BackColor = Color.Gray;
                lblSatellites.ForeColor = Color.White;
                return;
            }

            float satellites = mav.cs.satcount;
            

            lblSatellites.Text = satellites.ToString();

            if (satellites >= 8)
                lblSatellites.BackColor = Color.LimeGreen;
            else if (satellites >= 5)
                lblSatellites.BackColor = Color.Orange;
            else
                lblSatellites.BackColor = Color.FromArgb(200, 0, 0);

            lblSatellites.ForeColor = Color.White;
        }

        private void UpdateBatteryStatus()
        {
            var mav = MainV2.comPort?.MAV;

            if (mav == null)
            {
                lblBattery.Text = "No Vehicle";
                lblBattery.BackColor = Color.Gray;
                lblBattery.ForeColor = Color.White;
                return;
            }

            int battery = mav.cs.battery_remaining;

            lblBattery.Text = battery + "%";

            if (battery > 90)
                lblBattery.BackColor = Color.LimeGreen;
            else if (battery >= 40)
                lblBattery.BackColor = Color.Orange;
            else
                lblBattery.BackColor = Color.FromArgb(200, 0, 0);

            lblBattery.ForeColor = Color.White;
        }

        private bool TryGetCustomFieldValue(string fieldName, out double value)
        {
            value = 0;

            try
            {
                var mav = MainV2.comPort?.MAV;

                if (mav == null)
                    return false;

                var currentState = mav.cs;

                // Find the custom field configured with the requested name
                var customField = CurrentState.custom_field_names
                    .FirstOrDefault(x => x.Value == fieldName);

                if (string.IsNullOrEmpty(customField.Key))
                    return false;

                // Read the corresponding customfield property
                var property = currentState.GetType().GetProperty(customField.Key);

                if (property == null)
                    return false;

                var rawValue = property.GetValue(currentState, null);

                if (rawValue == null)
                    return false;

                value = Convert.ToDouble(rawValue);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void UpdateFuelStatus()
        {
            double fuel;

            if (!TryGetCustomFieldValue("MAV_FUEL", out fuel))
            {
                lblFuel.Text = "N/A";
                lblFuel.BackColor = Color.Gray;
                lblFuel.ForeColor = Color.White;
                return;
            }

            lblFuel.Text = fuel.ToString("0.0");

            if (fuel > 40)
                lblFuel.BackColor = Color.LimeGreen;
            else if (fuel > 20)
                lblFuel.BackColor = Color.Orange;
            else
                lblFuel.BackColor = Color.Red;

            lblFuel.ForeColor = Color.White;
        }
        private void UpdateTemperatureStatus(string fieldName, Label targetLabel)
        {
            double temperature;

            if (!TryGetCustomFieldValue(fieldName, out temperature))
            {
                targetLabel.Text = "N/A";
                targetLabel.BackColor = Color.Gray;
                targetLabel.ForeColor = Color.White;
                return;
            }

            targetLabel.Text = temperature.ToString("0.0") + " °C";
            targetLabel.BackColor = Color.LimeGreen;
            targetLabel.ForeColor = Color.White;
        }

        private void UpdateEngineStatus()
        {
            double engineStatus;

            if (!TryGetCustomFieldValue("MAV_ES", out engineStatus))
            {
                lblEngine.Text = "N/A";
                lblEngine.BackColor = Color.Gray;
                lblEngine.ForeColor = Color.White;
                return;
            }

            bool isEngineOn = engineStatus != 0;

            lblEngine.Text = isEngineOn ? "ON" : "OFF";
            lblEngine.BackColor = isEngineOn ? Color.LimeGreen : Color.Gray;
            lblEngine.ForeColor = isEngineOn ? Color.White : Color.Black;
        }

        private void UpdateOverallStatus()
        {
            bool allChecksPassed = true;
            bool incomplete = false;

            // GPS
            if (lblGPS.Text != "3D Fix" &&
                lblGPS.Text != "DGPS" &&
                lblGPS.Text != "RTK Float" &&
                lblGPS.Text != "RTK Fixed")
            {
                allChecksPassed = false;
            }

            // Satellites
            if (!int.TryParse(lblSatellites.Text, out int satellites))
            {
                incomplete = true;
                allChecksPassed = false;
            }
            else if (satellites < 5)
            {
                allChecksPassed = false;
            }

            // Battery
            if (!int.TryParse(lblBattery.Text.Replace("%", ""), out int battery))
            {
                incomplete = true;
                allChecksPassed = false;
            }
            else if (battery < 40)
            {
                allChecksPassed = false;
            }

            // Fuel
            if (lblFuel.Text == "N/A")
            {
                incomplete = true;
                allChecksPassed = false;
            }

            // Engine
            if (lblEngine.Text == "N/A")
            {
                incomplete = true;
                allChecksPassed = false;
            }

            // Temperatures
            if (lblEngineTemp.Text == "N/A" ||
                lblWaterTemp.Text == "N/A" ||
                lblSilencerTemp.Text == "N/A")
            {
                incomplete = true;
                allChecksPassed = false;
            }

            if (incomplete)
            {
                lblOverallStatus.Text = "CHECK INCOMPLETE";
                lblOverallStatus.ForeColor = Color.DarkOrange;
            }
            else if (allChecksPassed)
            {
                lblOverallStatus.Text = "READY";
                lblOverallStatus.ForeColor = Color.LimeGreen;
            }
            else
            {
                lblOverallStatus.Text = "WARNING";
                lblOverallStatus.ForeColor = Color.Red;
            }
        }

    }
}
