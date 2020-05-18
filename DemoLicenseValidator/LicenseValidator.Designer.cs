namespace DemoLicenseValidator
{
    partial class LicenseValidator
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
            this.button_ValidateLicense = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button_Load = new System.Windows.Forms.Button();
            this.button_CreateTestLic = new System.Windows.Forms.Button();
            this.button_CheckHash = new System.Windows.Forms.Button();
            this.button_SetDays = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button_DaysLeft = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_ValidateLicense
            // 
            this.button_ValidateLicense.Location = new System.Drawing.Point(644, 125);
            this.button_ValidateLicense.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_ValidateLicense.Name = "button_ValidateLicense";
            this.button_ValidateLicense.Size = new System.Drawing.Size(112, 35);
            this.button_ValidateLicense.TabIndex = 0;
            this.button_ValidateLicense.Text = "Validate";
            this.button_ValidateLicense.UseVisualStyleBackColor = true;
            this.button_ValidateLicense.Click += new System.EventHandler(this.Button_ValidateLicense_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(22, 80);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(614, 609);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(644, 80);
            this.button_Load.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(112, 35);
            this.button_Load.TabIndex = 2;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.Button_Load_Click);
            // 
            // button_CreateTestLic
            // 
            this.button_CreateTestLic.Location = new System.Drawing.Point(644, 365);
            this.button_CreateTestLic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_CreateTestLic.Name = "button_CreateTestLic";
            this.button_CreateTestLic.Size = new System.Drawing.Size(112, 71);
            this.button_CreateTestLic.TabIndex = 3;
            this.button_CreateTestLic.Text = "Create Test License";
            this.button_CreateTestLic.UseVisualStyleBackColor = true;
            this.button_CreateTestLic.Click += new System.EventHandler(this.Button_CreateTestLicense_Click);
            // 
            // button_CheckHash
            // 
            this.button_CheckHash.Location = new System.Drawing.Point(644, 445);
            this.button_CheckHash.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_CheckHash.Name = "button_CheckHash";
            this.button_CheckHash.Size = new System.Drawing.Size(112, 69);
            this.button_CheckHash.TabIndex = 4;
            this.button_CheckHash.Text = "Check Hash";
            this.button_CheckHash.UseVisualStyleBackColor = true;
            this.button_CheckHash.Click += new System.EventHandler(this.button_CheckHash_Click);
            // 
            // button_SetDays
            // 
            this.button_SetDays.Location = new System.Drawing.Point(644, 170);
            this.button_SetDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_SetDays.Name = "button_SetDays";
            this.button_SetDays.Size = new System.Drawing.Size(112, 63);
            this.button_SetDays.TabIndex = 5;
            this.button_SetDays.Text = "Set Days Allowed";
            this.button_SetDays.UseVisualStyleBackColor = true;
            this.button_SetDays.Click += new System.EventHandler(this.button_SetDays_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(644, 242);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 63);
            this.button2.TabIndex = 5;
            this.button2.Text = "Set Client Number";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_DaysLeft
            // 
            this.button_DaysLeft.Location = new System.Drawing.Point(644, 524);
            this.button_DaysLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_DaysLeft.Name = "button_DaysLeft";
            this.button_DaysLeft.Size = new System.Drawing.Size(112, 69);
            this.button_DaysLeft.TabIndex = 4;
            this.button_DaysLeft.Text = "Get days left";
            this.button_DaysLeft.UseVisualStyleBackColor = true;
            this.button_DaysLeft.Click += new System.EventHandler(this.Button_DaysLeft_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 45);
            this.label1.TabIndex = 6;
            this.label1.Text = "You need a Tier III license to validate licenses. You may use the ORBENT License " +
    "activation portal to activate your licenses.";
            // 
            // LicenseValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 712);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_SetDays);
            this.Controls.Add(this.button_DaysLeft);
            this.Controls.Add(this.button_CheckHash);
            this.Controls.Add(this.button_CreateTestLic);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.button_ValidateLicense);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LicenseValidator";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ValidateLicense;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_CreateTestLic;
        private System.Windows.Forms.Button button_CheckHash;
        private System.Windows.Forms.Button button_SetDays;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_DaysLeft;
        private System.Windows.Forms.Label label1;
    }
}

