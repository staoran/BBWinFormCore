namespace BB.Updater;

partial class MainForm
{
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.panel1 = new System.Windows.Forms.Panel();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.MenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
        this.lblTitle = new System.Windows.Forms.Label();
        this.lblUpdateLog = new System.Windows.Forms.Label();
        this.lab_percent = new System.Windows.Forms.Label();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.lab_fileinfo = new System.Windows.Forms.Label();
        this.lab_filename = new System.Windows.Forms.Label();
        this.progressBar1 = new System.Windows.Forms.ProgressBar();
        this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        this.panel1.SuspendLayout();
        this.contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // panel1
        // 
        this.panel1.BackColor = System.Drawing.Color.Transparent;
        this.panel1.BackgroundImage = BB.Updater.Resources.Backgroup2;
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        this.panel1.ContextMenuStrip = this.contextMenuStrip1;
        this.panel1.Controls.Add(this.lblTitle);
        this.panel1.Controls.Add(this.lblUpdateLog);
        this.panel1.Controls.Add(this.lab_percent);
        this.panel1.Controls.Add(this.pictureBox1);
        this.panel1.Controls.Add(this.lab_fileinfo);
        this.panel1.Controls.Add(this.lab_filename);
        this.panel1.Controls.Add(this.progressBar1);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panel1.Location = new System.Drawing.Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(496, 239);
        this.panel1.TabIndex = 0;
        this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
        this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
        this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_exit});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
        // 
        // MenuItem_exit
        // 
        this.MenuItem_exit.Image = BB.Updater.Resources.close;
        this.MenuItem_exit.Name = "MenuItem_exit";
        this.MenuItem_exit.Size = new System.Drawing.Size(94, 22);
        this.MenuItem_exit.Text = "关闭";
        this.MenuItem_exit.Click += new System.EventHandler(this.MenuItem_exit_Click);
        // 
        // lblTitle
        // 
        this.lblTitle.AutoSize = true;
        this.lblTitle.BackColor = System.Drawing.Color.Transparent;
        this.lblTitle.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)), true);
        this.lblTitle.ForeColor = System.Drawing.Color.Gold;
        this.lblTitle.Location = new System.Drawing.Point(53, 19);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(208, 18);
        this.lblTitle.TabIndex = 6;
        this.lblTitle.Text = "仓库系统框架-版本更新";
        // 
        // lblUpdateLog
        // 
        this.lblUpdateLog.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.lblUpdateLog.ForeColor = System.Drawing.Color.Lime;
        this.lblUpdateLog.Location = new System.Drawing.Point(20, 151);
        this.lblUpdateLog.Name = "lblUpdateLog";
        this.lblUpdateLog.Size = new System.Drawing.Size(454, 79);
        this.lblUpdateLog.TabIndex = 5;
        this.lblUpdateLog.Text = "更新说明：（无）";
        // 
        // lab_percent
        // 
        this.lab_percent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.lab_percent.ForeColor = System.Drawing.Color.Gold;
        this.lab_percent.Location = new System.Drawing.Point(438, 91);
        this.lab_percent.Name = "lab_percent";
        this.lab_percent.Size = new System.Drawing.Size(51, 23);
        this.lab_percent.TabIndex = 4;
        this.lab_percent.Text = "0%";
        this.lab_percent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // pictureBox1
        // 
        this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
        this.pictureBox1.Image = BB.Updater.Resources.close;
        this.pictureBox1.Location = new System.Drawing.Point(451, 3);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(42, 22);
        this.pictureBox1.TabIndex = 3;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
        // 
        // lab_fileinfo
        // 
        this.lab_fileinfo.BackColor = System.Drawing.Color.Transparent;
        this.lab_fileinfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.lab_fileinfo.ForeColor = System.Drawing.Color.Gold;
        this.lab_fileinfo.Location = new System.Drawing.Point(19, 123);
        this.lab_fileinfo.Name = "lab_fileinfo";
        this.lab_fileinfo.Size = new System.Drawing.Size(410, 18);
        this.lab_fileinfo.TabIndex = 1;
        this.lab_fileinfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // lab_filename
        // 
        this.lab_filename.BackColor = System.Drawing.Color.Transparent;
        this.lab_filename.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.lab_filename.ForeColor = System.Drawing.Color.Lime;
        this.lab_filename.Location = new System.Drawing.Point(21, 67);
        this.lab_filename.Name = "lab_filename";
        this.lab_filename.Size = new System.Drawing.Size(410, 18);
        this.lab_filename.TabIndex = 1;
        this.lab_filename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // progressBar1
        // 
        this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        this.progressBar1.ForeColor = System.Drawing.Color.DarkCyan;
        this.progressBar1.Location = new System.Drawing.Point(21, 86);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new System.Drawing.Size(410, 28);
        this.progressBar1.TabIndex = 0;
        // 
        // notifyIcon1
        // 
        this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
        this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
        this.notifyIcon1.Text = "招拍挂客户端更新";
        this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(496, 239);
        this.Controls.Add(this.panel1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "PC式采集器终端版本更新";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lab_filename;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lab_percent;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem MenuItem_exit;
    private System.Windows.Forms.Label lab_fileinfo;
    private System.Windows.Forms.Label lblUpdateLog;
    private System.Windows.Forms.Label lblTitle;

}