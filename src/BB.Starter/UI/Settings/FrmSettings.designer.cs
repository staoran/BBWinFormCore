namespace BB.Starter.UI.Settings
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.firefoxDialog1 = new BB.BaseUI.Settings.FirefoxDialog();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // firefoxDialog1
            // 
            this.firefoxDialog1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firefoxDialog1.ImageList = null;
            this.firefoxDialog1.Location = new System.Drawing.Point(0, 0);
            this.firefoxDialog1.Name = "firefoxDialog1";
            this.firefoxDialog1.Size = new System.Drawing.Size(669, 500);
            this.firefoxDialog1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "settings.png");
            this.imageList1.Images.SetKeyName(1, "Mail_Gmail.png");
            this.imageList1.Images.SetKeyName(2, "tele-32.png");
            this.imageList1.Images.SetKeyName(3, "sound.png");
            this.imageList1.Images.SetKeyName(4, "computer (2).png");
            this.imageList1.Images.SetKeyName(5, "product.png");
            this.imageList1.Images.SetKeyName(6, "favb32.png");
            this.imageList1.Images.SetKeyName(7, "blinklist (2).png");
            this.imageList1.Images.SetKeyName(8, "Clock (4).png");
            this.imageList1.Images.SetKeyName(9, "cog (2).png");
            this.imageList1.Images.SetKeyName(10, "database_connect.png");
            this.imageList1.Images.SetKeyName(11, "group.png");
            this.imageList1.Images.SetKeyName(12, "Home.png");
            this.imageList1.Images.SetKeyName(13, "mail32.png");
            this.imageList1.Images.SetKeyName(14, "LorryGreen.png");
            this.imageList1.Images.SetKeyName(15, "picture (2).png");
            this.imageList1.Images.SetKeyName(16, "print.png");
            this.imageList1.Images.SetKeyName(17, "security (2).png");
            this.imageList1.Images.SetKeyName(18, "Star On.png");
            this.imageList1.Images.SetKeyName(19, "statistics_32.png");
            this.imageList1.Images.SetKeyName(20, "Stats.png");
            this.imageList1.Images.SetKeyName(21, "unlock.png");
            this.imageList1.Images.SetKeyName(22, "User-32 (2).png");
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 500);
            this.Controls.Add(this.firefoxDialog1);
            this.Name = "FrmSettings";
            this.Text = "参数配置";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BB.BaseUI.Settings.FirefoxDialog firefoxDialog1;
        private System.Windows.Forms.ImageList imageList1;
    }
}