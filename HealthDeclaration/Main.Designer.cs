
namespace HealthDeclaration
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wbSamsung = new System.Windows.Forms.WebBrowser();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCn = new System.Windows.Forms.CheckBox();
            this.cb7 = new System.Windows.Forms.CheckBox();
            this.cb6 = new System.Windows.Forms.CheckBox();
            this.cb5 = new System.Windows.Forms.CheckBox();
            this.cb4 = new System.Windows.Forms.CheckBox();
            this.cb3 = new System.Windows.Forms.CheckBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.cbScripts = new System.Windows.Forms.ComboBox();
            this.SystemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wbSamsung);
            this.groupBox1.Location = new System.Drawing.Point(12, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 486);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Website";
            // 
            // wbSamsung
            // 
            this.wbSamsung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbSamsung.Location = new System.Drawing.Point(3, 16);
            this.wbSamsung.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSamsung.Name = "wbSamsung";
            this.wbSamsung.Size = new System.Drawing.Size(790, 467);
            this.wbSamsung.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReload);
            this.groupBox2.Controls.Add(this.btnSubmit);
            this.groupBox2.Controls.Add(this.lbMessage);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(414, 14);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(414, 45);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.ForeColor = System.Drawing.Color.Black;
            this.lbMessage.Location = new System.Drawing.Point(6, 19);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(35, 13);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.Text = "label1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbSource);
            this.groupBox3.Controls.Add(this.cbCn);
            this.groupBox3.Controls.Add(this.cb7);
            this.groupBox3.Controls.Add(this.cb6);
            this.groupBox3.Controls.Add(this.cb5);
            this.groupBox3.Controls.Add(this.cb4);
            this.groupBox3.Controls.Add(this.cb3);
            this.groupBox3.Controls.Add(this.cb2);
            this.groupBox3.Controls.Add(this.cbAuto);
            this.groupBox3.Controls.Add(this.cbScripts);
            this.groupBox3.Location = new System.Drawing.Point(507, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 81);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            // 
            // cbCn
            // 
            this.cbCn.AutoSize = true;
            this.cbCn.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbCn.Location = new System.Drawing.Point(269, 45);
            this.cbCn.Name = "cbCn";
            this.cbCn.Size = new System.Drawing.Size(26, 31);
            this.cbCn.TabIndex = 10;
            this.cbCn.Text = "CN";
            this.cbCn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCn.UseVisualStyleBackColor = true;
            this.cbCn.CheckedChanged += new System.EventHandler(this.cbCn_CheckedChanged);
            // 
            // cb7
            // 
            this.cb7.AutoSize = true;
            this.cb7.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb7.Location = new System.Drawing.Point(246, 45);
            this.cb7.Name = "cb7";
            this.cb7.Size = new System.Drawing.Size(17, 31);
            this.cb7.TabIndex = 9;
            this.cb7.Text = "7";
            this.cb7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb7.UseVisualStyleBackColor = true;
            this.cb7.CheckedChanged += new System.EventHandler(this.cb7_CheckedChanged);
            // 
            // cb6
            // 
            this.cb6.AutoSize = true;
            this.cb6.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb6.Location = new System.Drawing.Point(223, 45);
            this.cb6.Name = "cb6";
            this.cb6.Size = new System.Drawing.Size(17, 31);
            this.cb6.TabIndex = 8;
            this.cb6.Text = "6";
            this.cb6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb6.UseVisualStyleBackColor = true;
            this.cb6.CheckedChanged += new System.EventHandler(this.cb6_CheckedChanged);
            // 
            // cb5
            // 
            this.cb5.AutoSize = true;
            this.cb5.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb5.Location = new System.Drawing.Point(200, 45);
            this.cb5.Name = "cb5";
            this.cb5.Size = new System.Drawing.Size(17, 31);
            this.cb5.TabIndex = 7;
            this.cb5.Text = "5";
            this.cb5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb5.UseVisualStyleBackColor = true;
            this.cb5.CheckedChanged += new System.EventHandler(this.cb5_CheckedChanged);
            // 
            // cb4
            // 
            this.cb4.AutoSize = true;
            this.cb4.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb4.Location = new System.Drawing.Point(177, 45);
            this.cb4.Name = "cb4";
            this.cb4.Size = new System.Drawing.Size(17, 31);
            this.cb4.TabIndex = 6;
            this.cb4.Text = "4";
            this.cb4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb4.UseVisualStyleBackColor = true;
            this.cb4.CheckedChanged += new System.EventHandler(this.cb4_CheckedChanged);
            // 
            // cb3
            // 
            this.cb3.AutoSize = true;
            this.cb3.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb3.Location = new System.Drawing.Point(154, 45);
            this.cb3.Name = "cb3";
            this.cb3.Size = new System.Drawing.Size(17, 31);
            this.cb3.TabIndex = 5;
            this.cb3.Text = "3";
            this.cb3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb3.UseVisualStyleBackColor = true;
            this.cb3.CheckedChanged += new System.EventHandler(this.cb3_CheckedChanged);
            // 
            // cb2
            // 
            this.cb2.AutoSize = true;
            this.cb2.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cb2.Location = new System.Drawing.Point(131, 45);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(17, 31);
            this.cb2.TabIndex = 4;
            this.cb2.Text = "2";
            this.cb2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb2.UseVisualStyleBackColor = true;
            this.cb2.CheckedChanged += new System.EventHandler(this.cb2_CheckedChanged);
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Location = new System.Drawing.Point(134, 19);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(163, 17);
            this.cbAuto.TabIndex = 3;
            this.cbAuto.Text = "Auto Samsung Health Check";
            this.cbAuto.UseVisualStyleBackColor = true;
            this.cbAuto.CheckedChanged += new System.EventHandler(this.cbAuto_CheckedChanged);
            // 
            // cbScripts
            // 
            this.cbScripts.FormattingEnabled = true;
            this.cbScripts.Location = new System.Drawing.Point(6, 15);
            this.cbScripts.Name = "cbScripts";
            this.cbScripts.Size = new System.Drawing.Size(121, 21);
            this.cbScripts.TabIndex = 2;
            this.cbScripts.SelectedIndexChanged += new System.EventHandler(this.cbScripts_SelectedIndexChanged);
            // 
            // SystemTrayIcon
            // 
            this.SystemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SystemTrayIcon.Icon")));
            this.SystemTrayIcon.Text = "Health Check";
            this.SystemTrayIcon.Visible = true;
            // 
            // cbSource
            // 
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Location = new System.Drawing.Point(6, 54);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(121, 21);
            this.cbSource.TabIndex = 11;
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 597);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Health Declaration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.WebBrowser wbSamsung;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbAuto;
        private System.Windows.Forms.ComboBox cbScripts;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.NotifyIcon SystemTrayIcon;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.CheckBox cbCn;
        private System.Windows.Forms.CheckBox cb7;
        private System.Windows.Forms.CheckBox cb6;
        private System.Windows.Forms.CheckBox cb5;
        private System.Windows.Forms.CheckBox cb4;
        private System.Windows.Forms.CheckBox cb3;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.ComboBox cbSource;
    }
}

