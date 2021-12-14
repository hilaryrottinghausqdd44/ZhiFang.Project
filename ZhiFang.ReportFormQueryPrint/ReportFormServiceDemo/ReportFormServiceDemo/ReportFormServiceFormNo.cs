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
    public partial class ReportFormServiceFormNo : Form
    {
        public ReportFormServiceFormNo()
        {
            InitializeComponent();
        }

        private void ReportFormServiceFormNo_Load(object sender, EventArgs e)
        {

        }

        private void BtnCallService_Click(object sender, EventArgs e)
        {
            if (TxtCardNo.Text == null || TxtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("FormNo不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            //rfsif.Url = this.TxtServiceUrl.Text;
            string[] reportformpathlist;
            string errorinfo;
            bool flag = rfs.GetReportFormPDFByFormNo(this.TxtCardNo.Text.Trim(), "Receivedate;SectionNo;Sampleno;TestTypeno", "",90,0, out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void BtnCallReportFormID_Click(object sender, EventArgs e)
        {
            if (TxtReportFormID.Text == null || TxtReportFormID.Text.Trim() == "")
            {
                MessageBox.Show("TxtReportFormID不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            //rfsif.Url = this.TxtServiceUrl.Text;
            string[] reportformpathlist;
            string errorinfo;
            bool flag = rfs.GetReportFormPDFByReportFormID(this.TxtReportFormID.Text.Trim(), 90, 0, out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void BtnBarCode_Click(object sender, EventArgs e)
        {
            if (TxtBarCode.Text == null || TxtBarCode.Text.Trim() == "")
            {
                MessageBox.Show("TxtBarCode不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            //rfsif.Url = this.TxtServiceUrl.Text;
            string[] reportformpathlist;
            string errorinfo;
            bool flag = rfs.GetReportFormPDFByBarCode(this.TxtBarCode.Text.Trim(), 90, 0, out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }       

        private void BtnPatNo_Click(object sender, EventArgs e)
        {
            if (TxtPatNo.Text == null || TxtPatNo.Text.Trim() == "")
            {
                MessageBox.Show("TxtPatNo不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            //rfsif.Url = this.TxtServiceUrl.Text;
            string[] reportformpathlist;
            string errorinfo;
            bool flag = rfs.GetReportFormPDFByPatNo(this.TxtPatNo.Text.Trim(), 90, 0, out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void TxtCardNo_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
