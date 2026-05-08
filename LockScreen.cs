using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MissionPlanner
{
    internal sealed class LockScreen : Form
    {
        private readonly string _pin;
        private readonly TextBox _pinBox;
        private readonly Button _ok;

        public LockScreen(string pin)
        {
            _pin = (pin ?? "").Trim();

            Text = "Locked";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = true;
            TopMost = true;
            ClientSize = new Size(340, 150);

            var title = new Label
            {
                AutoSize = false,
                Text = "Enter 4-digit PIN to unlock",
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(16, 16),
                Size = new Size(ClientSize.Width - 32, 22)
            };

            var pinLabel = new Label
            {
                AutoSize = true,
                Text = "PIN:",
                Location = new Point(16, 58)
            };

            _pinBox = new TextBox
            {
                Location = new Point(60, 54),
                Size = new Size(120, 24),
                MaxLength = 4,
                UseSystemPasswordChar = true,
                TextAlign = HorizontalAlignment.Center
            };
            _pinBox.TextChanged += (_, __) => UpdateOkEnabled();
            _pinBox.KeyPress += PinBox_KeyPress;
            _pinBox.KeyDown += PinBox_KeyDown;

            var cancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(ClientSize.Width - 200, 100),
                Size = new Size(80, 28)
            };

            _ok = new Button
            {
                Text = "Unlock",
                Location = new Point(ClientSize.Width - 110, 100),
                Size = new Size(80, 28),
                Enabled = false
            };
            _ok.Click += (_, __) => TryUnlock();

            Controls.Add(title);
            Controls.Add(pinLabel);
            Controls.Add(_pinBox);
            Controls.Add(cancel);
            Controls.Add(_ok);

            AcceptButton = _ok;
            CancelButton = cancel;

            Shown += (_, __) => _pinBox.Focus();
        }

        private void PinBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void PinBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                TryUnlock();
            }
        }

        private void UpdateOkEnabled()
        {
            _ok.Enabled = _pinBox.Text.Length == 4 && _pinBox.Text.All(char.IsDigit);
        }

        private void TryUnlock()
        {
            if (_pinBox.Text.Length != 4 || !_pinBox.Text.All(char.IsDigit))
                return;

            if (string.Equals(_pinBox.Text, _pin, StringComparison.Ordinal))
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            _pinBox.SelectAll();
            System.Media.SystemSounds.Beep.Play();
        }
    }
}

