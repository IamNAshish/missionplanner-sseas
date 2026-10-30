using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MissionPlanner.Joystick
{
    // this is for popup that appears when joystick left pannel setting button clicked // 30oct2026_joystickLeft
    public partial class VirtualButtonSettings : Form
    {
        private TextBox txtLabel;
        private NumericUpDown numChannel;
        private NumericUpDown numOnPwm;
        private NumericUpDown numOffPwm;

        private Button btnSave;
        private Button btnCancel;

        public string ButtonLabel
        {
            get { return txtLabel.Text; }
        }

        public int TargetChannel
        {
            get { return (int)numChannel.Value; }
        }

        public int OnPwm
        {
            get { return (int)numOnPwm.Value; }
        }

        public int OffPwm
        {
            get { return (int)numOffPwm.Value; }
        }

        public VirtualButtonSettings(
            string label,
            int channel,
            int onPwm,
            int offPwm)
        {
            Text = "Virtual Button Settings";
            Size = new Size(340, 280);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lblName = new Label();
            lblName.Text = "Button Label:";
            lblName.Location = new Point(15, 20);
            lblName.AutoSize = true;

            txtLabel = new TextBox();
            txtLabel.Location = new Point(145, 17);
            txtLabel.Size = new Size(150, 23);
            txtLabel.Text = label;

            Label lblChannel = new Label();
            lblChannel.Text = "Target RC Channel:";
            lblChannel.Location = new Point(15, 60);
            lblChannel.AutoSize = true;

            numChannel = new NumericUpDown();
            numChannel.Location = new Point(145, 57);
            numChannel.Size = new Size(150, 23);
            numChannel.Minimum = 1;
            numChannel.Maximum = 16;
            numChannel.Value = Math.Max(1, Math.Min(16, channel));

            Label lblOn = new Label();
            lblOn.Text = "ON PWM:";
            lblOn.Location = new Point(15, 100);
            lblOn.AutoSize = true;

            numOnPwm = new NumericUpDown();
            numOnPwm.Location = new Point(145, 97);
            numOnPwm.Size = new Size(150, 23);
            numOnPwm.Minimum = 800;
            numOnPwm.Maximum = 2200;
            numOnPwm.Value = Math.Max(800, Math.Min(2200, onPwm));

            Label lblOff = new Label();
            lblOff.Text = "OFF PWM:";
            lblOff.Location = new Point(15, 140);
            lblOff.AutoSize = true;

            numOffPwm = new NumericUpDown();
            numOffPwm.Location = new Point(145, 137);
            numOffPwm.Size = new Size(150, 23);
            numOffPwm.Minimum = 800;
            numOffPwm.Maximum = 2200;
            numOffPwm.Value = Math.Max(800, Math.Min(2200, offPwm));

            btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.Location = new Point(145, 190);
            btnSave.Size = new Size(70, 30);
            btnSave.DialogResult = DialogResult.OK;

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(225, 190);
            btnCancel.Size = new Size(70, 30);
            btnCancel.DialogResult = DialogResult.Cancel;

            Controls.Add(lblName);
            Controls.Add(txtLabel);
            Controls.Add(lblChannel);
            Controls.Add(numChannel);
            Controls.Add(lblOn);
            Controls.Add(numOnPwm);
            Controls.Add(lblOff);
            Controls.Add(numOffPwm);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }
    }
}
