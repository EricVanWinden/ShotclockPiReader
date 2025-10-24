using System.Drawing;

namespace WinFormsApp1
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            bgwRestApi = new System.ComponentModel.BackgroundWorker();
            btnConnect = new System.Windows.Forms.Button();
            lblTime = new System.Windows.Forms.Label();
            rtbLog = new System.Windows.Forms.RichTextBox();
            btnDisconnect = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // bgwRestApi
            // 
            bgwRestApi.WorkerSupportsCancellation = true;
            bgwRestApi.DoWork += bgwRestApi_DoWork;
            bgwRestApi.RunWorkerCompleted += bgwRestApi_RunWorkerCompleted;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 12);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 120F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTime.Location = new Point(12, 38);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(434, 212);
            lblTime.TabIndex = 1;
            lblTime.Text = "Time";
            // 
            // rtbLog
            // 
            rtbLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbLog.Location = new Point(12, 253);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(732, 222);
            rtbLog.TabIndex = 2;
            rtbLog.Text = "";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(93, 12);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(75, 23);
            btnDisconnect.TabIndex = 3;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // Main
            // 
            ClientSize = new Size(752, 483);
            Controls.Add(btnDisconnect);
            Controls.Add(rtbLog);
            Controls.Add(lblTime);
            Controls.Add(btnConnect);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            Text = "Shotclock reader";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwRestApi;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnDisconnect;
    }
}
