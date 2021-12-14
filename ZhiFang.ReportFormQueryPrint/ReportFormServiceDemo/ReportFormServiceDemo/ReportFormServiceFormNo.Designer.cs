namespace ReportFormServiceDemo
{
    partial class ReportFormServiceFormNo
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnPatNo = new System.Windows.Forms.Button();
            this.BtnBarCode = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnCallReportFormID = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnCallService = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPatNo = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.TxtBarCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtReportFormID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCardNo = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtResult);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1192, 355);
            this.groupBox2.TabIndex = 3;
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
            this.TxtResult.Size = new System.Drawing.Size(1186, 331);
            this.TxtResult.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnPatNo);
            this.groupBox1.Controls.Add(this.BtnBarCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.BtnCallReportFormID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.BtnCallService);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtPatNo);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.TxtBarCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtReportFormID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtCardNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1192, 256);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "调用参数";
            // 
            // BtnPatNo
            // 
            this.BtnPatNo.Location = new System.Drawing.Point(466, 144);
            this.BtnPatNo.Name = "BtnPatNo";
            this.BtnPatNo.Size = new System.Drawing.Size(243, 23);
            this.BtnPatNo.TabIndex = 2;
            this.BtnPatNo.Text = "调用(默认90天内的报告)";
            this.BtnPatNo.UseVisualStyleBackColor = true;
            this.BtnPatNo.Click += new System.EventHandler(this.BtnPatNo_Click);
            // 
            // BtnBarCode
            // 
            this.BtnBarCode.Location = new System.Drawing.Point(466, 103);
            this.BtnBarCode.Name = "BtnBarCode";
            this.BtnBarCode.Size = new System.Drawing.Size(243, 23);
            this.BtnBarCode.TabIndex = 2;
            this.BtnBarCode.Text = "调用(默认90天内的报告)";
            this.BtnBarCode.UseVisualStyleBackColor = true;
            this.BtnBarCode.Click += new System.EventHandler(this.BtnBarCode_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "PatNo：";
            // 
            // BtnCallReportFormID
            // 
            this.BtnCallReportFormID.Location = new System.Drawing.Point(466, 64);
            this.BtnCallReportFormID.Name = "BtnCallReportFormID";
            this.BtnCallReportFormID.Size = new System.Drawing.Size(243, 23);
            this.BtnCallReportFormID.TabIndex = 2;
            this.BtnCallReportFormID.Text = "调用(默认90天内的报告)";
            this.BtnCallReportFormID.UseVisualStyleBackColor = true;
            this.BtnCallReportFormID.Click += new System.EventHandler(this.BtnCallReportFormID_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "BarCode：";
            // 
            // BtnCallService
            // 
            this.BtnCallService.Location = new System.Drawing.Point(791, 25);
            this.BtnCallService.Name = "BtnCallService";
            this.BtnCallService.Size = new System.Drawing.Size(243, 23);
            this.BtnCallService.TabIndex = 2;
            this.BtnCallService.Text = "调用(默认90天内的报告)";
            this.BtnCallService.UseVisualStyleBackColor = true;
            this.BtnCallService.Click += new System.EventHandler(this.BtnCallService_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "ReportFormID：";
            // 
            // TxtPatNo
            // 
            this.TxtPatNo.Location = new System.Drawing.Point(135, 145);
            this.TxtPatNo.Name = "TxtPatNo";
            this.TxtPatNo.Size = new System.Drawing.Size(325, 25);
            this.TxtPatNo.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(135, 216);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(967, 25);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "ServiceWCF/ReportFormWebService.asmx";
            // 
            // TxtBarCode
            // 
            this.TxtBarCode.Location = new System.Drawing.Point(135, 104);
            this.TxtBarCode.Name = "TxtBarCode";
            this.TxtBarCode.Size = new System.Drawing.Size(325, 25);
            this.TxtBarCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Receivedate;Section;TestTypeno;Sampleno";
            // 
            // TxtReportFormID
            // 
            this.TxtReportFormID.Location = new System.Drawing.Point(135, 65);
            this.TxtReportFormID.Name = "TxtReportFormID";
            this.TxtReportFormID.Size = new System.Drawing.Size(325, 25);
            this.TxtReportFormID.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "服务地址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "FormNo：";
            // 
            // TxtCardNo
            // 
            this.TxtCardNo.Location = new System.Drawing.Point(135, 26);
            this.TxtCardNo.Name = "TxtCardNo";
            this.TxtCardNo.Size = new System.Drawing.Size(325, 25);
            this.TxtCardNo.TabIndex = 0;
            this.TxtCardNo.TextChanged += new System.EventHandler(this.TxtCardNo_TextChanged);
            // 
            // ReportFormServiceFormNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 611);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportFormServiceFormNo";
            this.Text = "ReportFormService";
            this.Load += new System.EventHandler(this.ReportFormServiceFormNo_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TxtResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnCallService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCardNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnCallReportFormID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtReportFormID;
        private System.Windows.Forms.Button BtnBarCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox TxtBarCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnPatNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtPatNo;
    }
}