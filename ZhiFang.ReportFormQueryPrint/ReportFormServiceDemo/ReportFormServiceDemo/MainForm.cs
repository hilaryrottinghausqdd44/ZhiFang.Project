using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportFormServiceDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var zr = new ZhongRiYiYuanZiZhuDaDanReportFormService();
            zr.TopLevel = false;
            zr.Dock = DockStyle.Fill;
            zr.FormBorderStyle = FormBorderStyle.None;
            this.TabPageZR.Controls.Add(zr);
            zr.Show();

            var formno=new ReportFormServiceFormNo();
            formno.TopLevel = false;
            formno.Dock = DockStyle.Fill;
            formno.FormBorderStyle = FormBorderStyle.None;
            this.TabPageFormNo.Controls.Add(formno);
            formno.Show();

        }

        private void TabPageFormNo_Click(object sender, EventArgs e)
        {

        }
    }
}
