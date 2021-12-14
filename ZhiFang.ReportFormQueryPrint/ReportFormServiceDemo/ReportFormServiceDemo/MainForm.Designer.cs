namespace ReportFormServiceDemo
{
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageZR = new System.Windows.Forms.TabPage();
            this.TabPageFormNo = new System.Windows.Forms.TabPage();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabPageZR);
            this.TabControl.Controls.Add(this.TabPageFormNo);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1281, 707);
            this.TabControl.TabIndex = 1;
            // 
            // TabPageZR
            // 
            this.TabPageZR.Location = new System.Drawing.Point(4, 25);
            this.TabPageZR.Name = "TabPageZR";
            this.TabPageZR.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageZR.Size = new System.Drawing.Size(1273, 678);
            this.TabPageZR.TabIndex = 0;
            this.TabPageZR.Text = "中日医院自助打印服务测试";
            this.TabPageZR.UseVisualStyleBackColor = true;
            // 
            // TabPageFormNo
            // 
            this.TabPageFormNo.Location = new System.Drawing.Point(4, 25);
            this.TabPageFormNo.Name = "TabPageFormNo";
            this.TabPageFormNo.Size = new System.Drawing.Size(1273, 678);
            this.TabPageFormNo.TabIndex = 1;
            this.TabPageFormNo.Text = "四个关键字获取报告单地址";
            this.TabPageFormNo.UseVisualStyleBackColor = true;
            this.TabPageFormNo.Click += new System.EventHandler(this.TabPageFormNo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 707);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BS报告服务测试工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageZR;
        private System.Windows.Forms.TabPage TabPageFormNo;
    }
}

