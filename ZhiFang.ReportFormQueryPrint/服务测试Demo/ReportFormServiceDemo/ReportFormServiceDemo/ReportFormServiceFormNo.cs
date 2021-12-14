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
            bool flag = rfs.GetReportFormPDFByFormNo(this.TxtCardNo.Text.Trim(), "Receivedate;SectionNo;TestTypeno;Sampleno", "",90,0, out reportformpathlist, out errorinfo);
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
            bool flag = rfs.GetReportFormPDFByPatNo(this.TxtPatNo.Text.Trim(), 90, 0,out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void btnZDY17_Click(object sender, EventArgs e)
        {
            if (TextZDY17.Text == null || TextZDY17.Text.Trim() == "")
            {
                MessageBox.Show("TxtZDY17不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            
            string[] reportformpathlist;
            string errorinfo;
            bool flag = rfs.GetReportFormPDFByZDY17(this.TextZDY17.Text.Trim(), 90, 0, out reportformpathlist, out errorinfo);
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TextFields.Text == null || TextFields.Text.Trim() == "")
            {
                MessageBox.Show("TextFields不能为空！");
                return;
            }
            if (TextValues.Text == null || TextValues.Text.Trim() == "")
            {
                MessageBox.Show("TextValues不能为空！");
                return;
            }
            if (isPrintTimes.Text == null || isPrintTimes.Text.Trim() == "")
            {
                MessageBox.Show("TextValues不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();

            string[] reportformpathlist;
            string errorinfo;
            bool flag = false;
            string[] fields = TextFields.Text.ToString().Split(',');
            string[] values = TextValues.Text.ToString().Split(',');
            string[] order = TextOrder.Text.ToString().Split(',');
            if (!(TextValues.Text == null || TextValues.Text.Trim() == ""))
            {
                flag = rfs.GetReportFormPDFByFields(fields, values, order, 90,int.Parse(isPrintTimes.Text.ToString()), 0, out reportformpathlist, out errorinfo);
            }else
            {
                string[] a = new string[0];
                 flag = rfs.GetReportFormPDFByFields(fields, values, a, 90, int.Parse(isPrintTimes.Text.ToString()), 0, out reportformpathlist, out errorinfo);
            }
            
            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", reportformpathlist);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + errorinfo;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (reportformid.Text == null || reportformid.Text.Trim() == "")
            {
                MessageBox.Show("TextValues不能为空！");
                return;
            }
            ReportFormWebService.ReportFormWebService rfs = new ReportFormWebService.ReportFormWebService();
            string reportformidstr = reportformid.Text.ToString().Trim();
            string ErrorInfo;
            bool flag = false;
            flag = rfs.UpdateReportPrintTimes(reportformidstr, out ErrorInfo);

            if (flag)
            {
                this.TxtResult.Text = string.Join(",\r\n", flag);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + ErrorInfo;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (reportfromid_ftp.Text == null || reportfromid_ftp.Text.Trim() == "")
            //{
            //    MessageBox.Show("TextValues不能为空！");
            //    return;
            //}
            ReportFormServiceInterFace.ReportFormServiceInterFace rfs = new ReportFormServiceInterFace.ReportFormServiceInterFace();
            string reportformidstr = reportfromid_ftp.Text.ToString().Trim();
            
            string ReportId = "";
            bool flag = false;
            string d="";
            flag = rfs.NeiJiangGetReportFormPDFListByUserId("","","","123123", "2016-04-19;2;1;1", out ReportId);

            if (flag)
            {
                this.TxtResult.Text = ReportId + string.Join(",\r\n", d);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + ReportId;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportFormServiceInterFace.ReportFormServiceInterFace rfs = new ReportFormServiceInterFace.ReportFormServiceInterFace();
            string reportformidstr = reportfromid_ftp.Text.ToString().Trim();

            string ReportId = "";
            bool flag = true;
            string d = "";
            flag = rfs.NeiJiangGetReportFormPDFListBitsByDSXML("", "", "", "123123", "2016-04-19;2;1;1", out ReportId);
            // flag = rfs.NeiJiangUpdateReportPrintTimes("", "", "", "123123", "", out ReportId);
            //string aa = "<Request><CardType></CardType><CardNo>1604190002</CardNo><BeginDate>2016-04-19</BeginDate><EndDate>2016-04-19</EndDate></Request>";
            //ReportId = rfs.GetLisReportBase64(aa);
            if (flag)
            {
                this.TxtResult.Text = ReportId + string.Join(",\r\n", d);
            }
            else
            {
                this.TxtResult.Text = "调用失败：" + ReportId;
            }
        }  
    }
}
