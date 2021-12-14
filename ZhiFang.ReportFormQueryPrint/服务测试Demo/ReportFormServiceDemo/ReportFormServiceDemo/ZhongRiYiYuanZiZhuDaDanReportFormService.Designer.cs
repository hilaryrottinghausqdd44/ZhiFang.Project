namespace ReportFormServiceDemo
{
    partial class ZhongRiYiYuanZiZhuDaDanReportFormService
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnCallService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCardNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.TxtServiceUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCallService);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtServiceUrl);
            this.groupBox1.Controls.Add(this.TxtCardNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1296, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "调用参数";
            // 
            // BtnCallService
            // 
            this.BtnCallService.Location = new System.Drawing.Point(329, 61);
            this.BtnCallService.Name = "BtnCallService";
            this.BtnCallService.Size = new System.Drawing.Size(75, 23);
            this.BtnCallService.TabIndex = 2;
            this.BtnCallService.Text = "调用";
            this.BtnCallService.UseVisualStyleBackColor = true;
            this.BtnCallService.Click += new System.EventHandler(this.BtnCallService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "卡号：";
            // 
            // TxtCardNo
            // 
            this.TxtCardNo.Location = new System.Drawing.Point(85, 60);
            this.TxtCardNo.Name = "TxtCardNo";
            this.TxtCardNo.Size = new System.Drawing.Size(238, 25);
            this.TxtCardNo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtResult);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1296, 635);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "调用结果";
            // 
            // TxtResult
            // 
            this.TxtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtResult.Location = new System.Drawing.Point(3, 21);
            this.TxtResult.Multiline = true;
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtResult.Size = new System.Drawing.Size(1290, 611);
            this.TxtResult.TabIndex = 0;
            // 
            // TxtServiceUrl
            // 
            this.TxtServiceUrl.Location = new System.Drawing.Point(85, 24);
            this.TxtServiceUrl.Name = "TxtServiceUrl";
            this.TxtServiceUrl.Size = new System.Drawing.Size(1007, 25);
            this.TxtServiceUrl.TabIndex = 0;
            this.TxtServiceUrl.Text = "http://localhost/ZhiFang.ReportFormQueryPrint/ServiceWCF/ReportFormServiceInterFa" +
    "ce.asmx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务：";
            // 
            // ZhongRiYiYuanZiZhuDaDanReportFormService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 735);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ZhongRiYiYuanZiZhuDaDanReportFormService";
            this.Text = "ZhongRiYiYuanZiZhuDaDanReportFormService";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnCallService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCardNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtServiceUrl;
    }
}