namespace DemoLicenseApp
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.Btn_Create = new System.Windows.Forms.Button();
            this.Btn_Manage = new System.Windows.Forms.Button();
            this.Lb_TimeLeft = new System.Windows.Forms.Label();
            this.RTB_Logs = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetTimeleftTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Btn_Create
            // 
            this.Btn_Create.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Create.Location = new System.Drawing.Point(40, 177);
            this.Btn_Create.Name = "Btn_Create";
            this.Btn_Create.Size = new System.Drawing.Size(397, 147);
            this.Btn_Create.TabIndex = 0;
            this.Btn_Create.Text = "Create Initial License";
            this.Btn_Create.UseVisualStyleBackColor = true;
            this.Btn_Create.Click += new System.EventHandler(this.Btn_Create_Click);
            // 
            // Btn_Manage
            // 
            this.Btn_Manage.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Manage.Location = new System.Drawing.Point(535, 177);
            this.Btn_Manage.Name = "Btn_Manage";
            this.Btn_Manage.Size = new System.Drawing.Size(397, 147);
            this.Btn_Manage.TabIndex = 1;
            this.Btn_Manage.Text = "Manage License";
            this.Btn_Manage.UseVisualStyleBackColor = true;
            this.Btn_Manage.Click += new System.EventHandler(this.Btn_Manage_Click);
            // 
            // Lb_TimeLeft
            // 
            this.Lb_TimeLeft.AutoSize = true;
            this.Lb_TimeLeft.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_TimeLeft.Location = new System.Drawing.Point(33, 79);
            this.Lb_TimeLeft.Name = "Lb_TimeLeft";
            this.Lb_TimeLeft.Size = new System.Drawing.Size(176, 41);
            this.Lb_TimeLeft.TabIndex = 2;
            this.Lb_TimeLeft.Text = "Time Left:";
            // 
            // RTB_Logs
            // 
            this.RTB_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_Logs.Location = new System.Drawing.Point(40, 416);
            this.RTB_Logs.Name = "RTB_Logs";
            this.RTB_Logs.Size = new System.Drawing.Size(892, 438);
            this.RTB_Logs.TabIndex = 3;
            this.RTB_Logs.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 377);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "Logs";
            // 
            // GetTimeleftTimer
            // 
            this.GetTimeleftTimer.Enabled = true;
            this.GetTimeleftTimer.Interval = 1000;
            this.GetTimeleftTimer.Tick += new System.EventHandler(this.GetTimeleftTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(972, 893);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RTB_Logs);
            this.Controls.Add(this.Lb_TimeLeft);
            this.Controls.Add(this.Btn_Manage);
            this.Controls.Add(this.Btn_Create);
            this.Name = "MainWindow";
            this.Text = "Demo License App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Create;
        private System.Windows.Forms.Button Btn_Manage;
        private System.Windows.Forms.Label Lb_TimeLeft;
        private System.Windows.Forms.RichTextBox RTB_Logs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer GetTimeleftTimer;
    }
}

