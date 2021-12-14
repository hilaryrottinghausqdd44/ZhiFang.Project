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
    public partial class ZhongRiYiYuanZiZhuDaDanReportFormService : Form
    {
        public ZhongRiYiYuanZiZhuDaDanReportFormService()
        {
            InitializeComponent();
        }

        private void BtnCallService_Click(object sender, EventArgs e)
        {
            if (TxtCardNo.Text == null || TxtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("卡号不能为空！");
                return;
            }
            if (TxtServiceUrl.Text == null || TxtServiceUrl.Text.Trim() == "")
            {
                MessageBox.Show("服务地址不能为空！");
                return;
            }
            ReportFormServiceInterFace.ReportFormServiceInterFace rfsif = new ReportFormServiceInterFace.ReportFormServiceInterFace();
            //rfsif.Url = this.TxtServiceUrl.Text;
            string[] reportformpathlist;
            string errorinfo;
            bool flag=rfsif.ZhongRiYiYuanZiZhuDaDanReportFormService(this.TxtCardNo.Text.Trim(), out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败："+ errorinfo;
            }
        }
    }
}
