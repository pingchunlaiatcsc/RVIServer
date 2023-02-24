namespace RVIServer
{
    partial class RVIServer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RVIServer));
            this.lb_UserList = new System.Windows.Forms.ListBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.btn_KickAll = new System.Windows.Forms.Button();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecoverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_Location = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_UserList
            // 
            this.lb_UserList.FormattingEnabled = true;
            this.lb_UserList.ItemHeight = 12;
            this.lb_UserList.Location = new System.Drawing.Point(9, 19);
            this.lb_UserList.Name = "lb_UserList";
            this.lb_UserList.Size = new System.Drawing.Size(120, 88);
            this.lb_UserList.TabIndex = 0;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(9, 5);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(65, 12);
            this.lable1.TabIndex = 1;
            this.lable1.Text = "線上使用者";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server Port";
            // 
            // tb_IP
            // 
            this.tb_IP.Enabled = false;
            this.tb_IP.Location = new System.Drawing.Point(198, 61);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(100, 22);
            this.tb_IP.TabIndex = 4;
            // 
            // tb_Port
            // 
            this.tb_Port.Enabled = false;
            this.tb_Port.Location = new System.Drawing.Point(198, 89);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(100, 22);
            this.tb_Port.TabIndex = 5;
            // 
            // btn_Restart
            // 
            this.btn_Restart.Location = new System.Drawing.Point(198, 117);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(75, 23);
            this.btn_Restart.TabIndex = 7;
            this.btn_Restart.Text = "Restart";
            this.btn_Restart.UseVisualStyleBackColor = true;
            this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
            // 
            // btn_KickAll
            // 
            this.btn_KickAll.Location = new System.Drawing.Point(497, 8);
            this.btn_KickAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_KickAll.Name = "btn_KickAll";
            this.btn_KickAll.Size = new System.Drawing.Size(75, 25);
            this.btn_KickAll.TabIndex = 8;
            this.btn_KickAll.Text = "KickAll";
            this.btn_KickAll.UseVisualStyleBackColor = true;
            this.btn_KickAll.Visible = false;
            this.btn_KickAll.Click += new System.EventHandler(this.btn_KickAll_Click);
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(9, 153);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(320, 138);
            this.tb_log.TabIndex = 9;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "RVIServer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem,
            this.RecoverMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(98, 22);
            this.ExitMenuItem.Text = "關閉";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // RecoverMenuItem
            // 
            this.RecoverMenuItem.Name = "RecoverMenuItem";
            this.RecoverMenuItem.Size = new System.Drawing.Size(98, 22);
            this.RecoverMenuItem.Text = "還原";
            this.RecoverMenuItem.Click += new System.EventHandler(this.RecoverMenuItem_Click);
            // 
            // tb_Location
            // 
            this.tb_Location.Enabled = false;
            this.tb_Location.Location = new System.Drawing.Point(198, 33);
            this.tb_Location.Name = "tb_Location";
            this.tb_Location.Size = new System.Drawing.Size(100, 22);
            this.tb_Location.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Location";
            // 
            // RVIServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 295);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Location);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.btn_KickAll);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.tb_IP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lable1);
            this.Controls.Add(this.lb_UserList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RVIServer";
            this.Text = "RVIServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RVIServer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RVIServer_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_UserList;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.Button btn_Restart;
        private System.Windows.Forms.Button btn_KickAll;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RecoverMenuItem;
        private System.Windows.Forms.TextBox tb_Location;
        private System.Windows.Forms.Label label1;
    }
}

