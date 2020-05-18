namespace DemoLicenseApp 
{ 
    partial class LicenseManagerUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Load = new System.Windows.Forms.Button();
            this.PropertyGrid_License = new System.Windows.Forms.PropertyGrid();
            this.button_GenLicFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Load
            // 
            this.button_Load.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Load.Location = new System.Drawing.Point(383, 359);
            this.button_Load.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(317, 76);
            this.button_Load.TabIndex = 13;
            this.button_Load.Text = "Import Valid License";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.Btn_Import_Click);
            // 
            // PropertyGrid_License
            // 
            this.PropertyGrid_License.Location = new System.Drawing.Point(4, 445);
            this.PropertyGrid_License.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PropertyGrid_License.Name = "PropertyGrid_License";
            this.PropertyGrid_License.Size = new System.Drawing.Size(696, 562);
            this.PropertyGrid_License.TabIndex = 12;
            this.PropertyGrid_License.ToolbarVisible = false;
            // 
            // button_GenLicFile
            // 
            this.button_GenLicFile.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_GenLicFile.Location = new System.Drawing.Point(4, 359);
            this.button_GenLicFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_GenLicFile.Name = "button_GenLicFile";
            this.button_GenLicFile.Size = new System.Drawing.Size(339, 76);
            this.button_GenLicFile.TabIndex = 11;
            this.button_GenLicFile.Text = "Export License Request";
            this.button_GenLicFile.UseVisualStyleBackColor = true;
            this.button_GenLicFile.Click += new System.EventHandler(this.Btn_Export_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(4, 302);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 43);
            this.label1.TabIndex = 15;
            this.label1.Text = "Demo License App";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DemoLicenseApp.Properties.Resources.logo_orbnet_big;
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(266, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // LicenseManagerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.PropertyGrid_License);
            this.Controls.Add(this.button_GenLicFile);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LicenseManagerUserControl";
            this.Size = new System.Drawing.Size(704, 1012);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.PropertyGrid PropertyGrid_License;
        private System.Windows.Forms.Button button_GenLicFile;
        private System.Windows.Forms.Label label1;
    }
}
