namespace MissionPlanner
{
    partial class VirtualControllerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_v1_ch1 = new System.Windows.Forms.Button();
            this.btn_v2_ch2 = new System.Windows.Forms.Button();
            this.btn_v3_ch3 = new System.Windows.Forms.Button();
            this.btn_v4_ch4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_v1_ch1
            // 
            this.btn_v1_ch1.Location = new System.Drawing.Point(81, 39);
            this.btn_v1_ch1.Name = "btn_v1_ch1";
            this.btn_v1_ch1.Size = new System.Drawing.Size(75, 23);
            this.btn_v1_ch1.TabIndex = 0;
            this.btn_v1_ch1.Text = "ch1";
            this.btn_v1_ch1.UseVisualStyleBackColor = true;
            // 
            // btn_v2_ch2
            // 
            this.btn_v2_ch2.Location = new System.Drawing.Point(35, 81);
            this.btn_v2_ch2.Name = "btn_v2_ch2";
            this.btn_v2_ch2.Size = new System.Drawing.Size(75, 23);
            this.btn_v2_ch2.TabIndex = 1;
            this.btn_v2_ch2.Text = "ch2";
            this.btn_v2_ch2.UseVisualStyleBackColor = true;
            // 
            // btn_v3_ch3
            // 
            this.btn_v3_ch3.Location = new System.Drawing.Point(127, 81);
            this.btn_v3_ch3.Name = "btn_v3_ch3";
            this.btn_v3_ch3.Size = new System.Drawing.Size(75, 23);
            this.btn_v3_ch3.TabIndex = 2;
            this.btn_v3_ch3.Text = "ch3";
            this.btn_v3_ch3.UseVisualStyleBackColor = true;
            // 
            // btn_v4_ch4
            // 
            this.btn_v4_ch4.Location = new System.Drawing.Point(81, 122);
            this.btn_v4_ch4.Name = "btn_v4_ch4";
            this.btn_v4_ch4.Size = new System.Drawing.Size(75, 23);
            this.btn_v4_ch4.TabIndex = 3;
            this.btn_v4_ch4.Text = "ch4";
            this.btn_v4_ch4.UseVisualStyleBackColor = true;
            // 
            // VirtualControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 199);
            this.Controls.Add(this.btn_v4_ch4);
            this.Controls.Add(this.btn_v3_ch3);
            this.Controls.Add(this.btn_v2_ch2);
            this.Controls.Add(this.btn_v1_ch1);
            this.Name = "VirtualControllerForm";
            this.Text = "VirtualControllerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_v1_ch1;
        private System.Windows.Forms.Button btn_v2_ch2;
        private System.Windows.Forms.Button btn_v3_ch3;
        private System.Windows.Forms.Button btn_v4_ch4;
    }
}