using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.IO;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Configuration;
using System.Xml;
using System.Collections;
using ZhiFang.IBLL.Common;
using ZhiFang.Common.Public;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“RequestFormService”。
    public class RequestFormService : IRequestFormService
    {
        private readonly IBNRequestForm ibnrf = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBNRequestItem ibnri = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBSampleTypeControl ibstc = BLLFactory<IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
        private readonly IBGenderTypeControl ibgtc = BLLFactory<IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBRequestData ibrd = BLLFactory<IBRequestData>.GetBLL("RequestData");
        private readonly IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
        IBReportData ibr = BLLFactory<IBReportData>.GetBLL("ReportData");
        
        /// <summary>
        /// 申请单上传
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="orgID"></param>
        /// <param name="jzType"></param>
        /// <param name="sMsg"></param>
        /// <returns></returns>
        public bool AppliyUpLoad(byte[] xmlData, string orgID, string jzType, out string sMsg)
        {
            System.Text.UTF8Encoding converter = new UTF8Encoding();
            string drs = converter.GetString(xmlData);

            Log.Info("RequestFormService.AppliyUpLoad.xmlData:"+drs);
            Log.Info("RequestFormService.AppliyUpLoad.orgID:" + orgID);
            Log.Info("RequestFormService.AppliyUpLoad.jzType:" + jzType);
            return AppliyUpLoadStr(drs, orgID, jzType,out sMsg);
        }

        public bool AppliyUpLoadStr(string xmlData, string orgID, string jzType, out string sMsg)
        {
            string drs = xmlData;

            Log.Info("RequestFormService.AppliyUpLoadStr.xmlData:" + drs);
            Log.Info("RequestFormService.AppliyUpLoadStr.orgID:" + orgID);
            Log.Info("RequestFormService.AppliyUpLoadStr.jzType:" + jzType);
            try
            {
                int i = 0;
                sMsg = "";
                string str = "";
                if (drs == null)
                {
                    sMsg = "dr为空,不能上传申请单\r\n";
                    Log.Info(sMsg);
                }
                StringReader strTemp = new StringReader(drs);
                Model.NRequestForm model = new Model.NRequestForm();
                DataSet wsBarCode = new DataSet();

                wsBarCode.ReadXml(strTemp);
                bool flag = CheckAppliy(wsBarCode.Tables[0], "Table", out sMsg);
                if (!flag)
                {
                    ZhiFang.Common.Log.Log.Info(sMsg);
                    return false;
                }

                if (!ibbcf.CheckBarCodeLab(wsBarCode, orgID, out str))
                {
                    sMsg = "验证对照代码错误！";
                    return false;
                }
                wsBarCode = MatchCenterNo(wsBarCode, orgID);
                //DataRow dr = wsBarCode.Tables[0].Rows[0];
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                Model.NRequestItem nRequestItem = new Model.NRequestItem();

                foreach (DataRow dr in wsBarCode.Tables[0].Rows)
                {
                    DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = dr["BarCode"].ToString() });
                    if (wsBarCode.Tables[0].Columns.Contains("BarCode") && dr["BarCode"] != "")
                    {
                        barCodeForm.BarCode = dr["BarCode"].ToString();
                        if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                        {
                            if (dsBarCodeForm.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString().Trim() != orgID)
                            {
                                sMsg = "条码号:'" + dr["BarCode"].ToString() + "'已存在！";
                                Log.Info("实验室：" + orgID + "，上传了和实验室：" + dsBarCodeForm.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString().Trim() + "相同的条码号：" + wsBarCode.Tables[0].Rows[0]["BarCode"].ToString());
                                return false;
                            }
                        }

                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleTypeNo") && dr["SampleTypeNo"] != "")
                    {
                        barCodeForm.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        model.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        nRequestItem.SampleTypeNo = model.SampleTypeNo;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Birthday") && dr["Birthday"] != "")
                    {
                        model.Birthday = Convert.ToDateTime(dr["Birthday"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Birdthday") && dr["Birdthday"] != "")
                    {
                        model.Birthday = Convert.ToDateTime(dr["Birdthday"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        barCodeForm.SampleType = dr["SampleType"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgName") && dr["WebLisOrgName"] != "")
                    {
                        barCodeForm.WebLisOrgName = dr["WebLisOrgName"].ToString();
                        model.WebLisOrgName = dr["WebLisOrgName"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("PersonID") && dr["PersonID"] != "")
                    {
                        model.PersonID = dr["PersonID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        model.SampleType = dr["SampleType"].ToString();
                        nRequestItem.SampleType = dr["SampleType"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TelNo") && dr["TelNo"] != "")
                    {
                        model.TelNo = dr["TelNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        model.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("STATUSName") && dr["STATUSName"] != "")
                    {
                        model.STATUSName = dr["STATUSName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztypeName") && dr["jztypeName"] != "")
                    {
                        model.STATUSName = dr["jztypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckType") && dr["CheckType"] != "")
                    {
                        nRequestItem.CheckType = dr["CheckType"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckTypeName") && dr["CheckTypeName"] != "")
                    {
                        nRequestItem.CheckTypeName = dr["CheckTypeName"].ToString();
                    }
                    //


                    barCodeForm.WebLisSourceOrgId = orgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ClientName") && dr["ClientName"] != "")
                    {
                        barCodeForm.ClientName = dr["ClientName"].ToString();
                        model.ClientName = dr["ClientName"].ToString();
                        nRequestItem.ClientName = dr["ClientName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgName") && dr["WebLisSourceOrgName"] != "")
                    {
                        barCodeForm.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        model.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        //model.zdy5 = dr["WebLisSourceOrgName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TESTTYPENO") && dr["TESTTYPENO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["TESTTYPENO"].ToString()))
                        {
                            model.TestTypeNo = Convert.ToInt32(dr["TESTTYPENO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("STATUSNO") && dr["STATUSNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["STATUSNO"].ToString()))
                        {
                            model.StatusNo = Convert.ToInt32(dr["STATUSNO"]);
                        }

                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNO") && dr["DISTRICTNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DISTRICTNO"].ToString()))
                        {
                            model.DistrictNo = Convert.ToInt32(dr["DISTRICTNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNAME") && dr["DISTRICTNAME"] != "")
                    {
                        model.DistrictName = dr["DISTRICTNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DeptNo") && dr["DeptNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DeptNo"].ToString()))
                        {
                            model.DeptNo = Convert.ToInt32(dr["DeptNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        barCodeForm.WebLisOrgID = dr["WebLisOrgID"].ToString();
                        nRequestItem.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SerialNo") && dr["SerialNo"] != "")
                    {
                        model.SerialNo = dr["SerialNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Bed") && dr["Bed"] != "")
                    {
                        model.Bed = dr["Bed"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OldSerialNo") && dr["OldSerialNo"] != "")
                    {
                        model.OldSerialNo = dr["OldBarCode"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Color") && dr["Color"] != "")
                    {
                        barCodeForm.Color = dr["Color"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SampleTypeName") && dr["SampleTypeName"] != "")
                    {
                        barCodeForm.SampleTypeName = dr["SampleTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OrderNo") && dr["OrderNo"] != "")
                    {
                        barCodeForm.OrderNo = dr["OrderNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("LabItemName") && dr["LabItemName"] != "")
                    {
                        barCodeForm.LabItemName = dr["LabItemName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Collecter") && dr["Collecter"] != "")
                    {
                        barCodeForm.Collecter = dr["Collecter"].ToString();
                        model.Collecter = dr["Collecter"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CollecterID") && dr["CollecterID"] != "")
                    {
                        barCodeForm.CollecterID = int.Parse(dr["CollecterID"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("LabItemNo") && dr["LabItemNo"] != "")
                    {
                        barCodeForm.LabItemNo = dr["LabItemNo"].ToString();
                    }
                    barCodeForm.IsAffirm = 1;
                    if (wsBarCode.Tables[0].Columns.Contains("zdy1") && dr["zdy1"] != "")
                    {
                        model.zdy1 = dr["zdy1"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy2") && dr["zdy2"] != "")
                    {
                        model.zdy2 = dr["zdy2"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy3") && dr["zdy3"] != "")
                    {
                        model.zdy3 = dr["zdy3"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy4") && dr["zdy4"] != "")
                    {
                        model.zdy4 = dr["zdy4"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy5") && dr["zdy5"] != "")
                    {
                        model.zdy5 = dr["zdy5"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy6") && dr["zdy6"] != "")
                    {
                        model.ZDY6 = dr["zdy6"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy7") && dr["zdy7"] != "")
                    {
                        model.ZDY7 = dr["zdy7"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy8") && dr["zdy8"] != "")
                    {
                        model.ZDY8 = dr["zdy8"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy9") && dr["zdy9"] != "")
                    {
                        model.ZDY9 = dr["zdy9"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy10") && dr["zdy10"] != "")
                    {
                        model.ZDY10 = dr["zdy10"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Nationality") && dr["Nationality"] != "")
                    {
                        model.Nationality = dr["Nationality"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("PassportNo") && dr["PassportNo"] != "")
                    {
                        model.PassportNo = dr["PassportNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SMITypeId") && dr["SMITypeId"] != DBNull.Value && dr["SMITypeId"].ToString() != "")
                    {
                        model.SMITypeId = long.Parse(dr["SMITypeId"].ToString());
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SMITypeName") && dr["SMITypeName"] != DBNull.Value && dr["SMITypeName"].ToString() != "")
                    {
                        model.SMITypeName = dr["SMITypeName"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("NCPTestTypeNo") && dr["NCPTestTypeNo"] != DBNull.Value && dr["NCPTestTypeNo"].ToString() != "")
                    {
                        model.NCPTestTypeNo = dr["NCPTestTypeNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("IDCardAddress") && dr["IDCardAddress"] != DBNull.Value && dr["IDCardAddress"].ToString() != "")
                    {
                        model.IDCardAddress = dr["IDCardAddress"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("IDCardAddress") && dr["IDCardAddress"] != DBNull.Value && dr["IDCardAddress"].ToString() != "")
                    {
                        model.IDCardAddress = dr["IDCardAddress"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CollectTypeId") && dr["CollectTypeId"] != DBNull.Value && dr["CollectTypeId"].ToString() != "")
                    {
                        model.CollectTypeId = long.Parse(dr["CollectTypeId"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CollectTypeName") && dr["CollectTypeName"] != DBNull.Value && dr["CollectTypeName"].ToString() != "")
                    {
                        model.CollectTypeName = dr["CollectTypeName"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("ClientNo") && dr["ClientNo"] != "")
                    {
                        barCodeForm.ClientNo = dr["ClientNo"].ToString();
                        model.ClientNo = dr["ClientNo"].ToString();
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    else
                    {
                        barCodeForm.ClientNo = orgID;
                        model.ClientNo = orgID;
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CName") && dr["CName"] != "")
                    {
                        model.CName = dr["CName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Age") && dr["Age"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        {
                            model.Age = Convert.ToDecimal(dr["Age"].ToString());
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("AgeUnitName") && dr["AgeUnitName"] != "")
                    {
                        model.AgeUnitName = dr["AgeUnitName"].ToString();       //病人类型 如门诊/住院
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ClinicTypeName") && dr["ClinicTypeName"] != "")
                    {
                        model.ClinicTypeName = dr["ClinicTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TestTypeName") && dr["TestTypeName"] != "")  // 检验类型 如常/急/质
                    {
                        model.TestTypeName = dr["TestTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderNo") && dr["GenderNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["GenderNo"].ToString()))
                        {
                            model.GenderNo = Convert.ToInt32(dr["GenderNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderName") && dr["GenderName"] != "")
                    {
                        model.GenderName = dr["GenderName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNAME") && dr["WARDNAME"] != "")
                    {
                        model.WardName = dr["WARDNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNO") && dr["WARDNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["WARDNO"].ToString()))
                        {
                            model.WardNo = Convert.ToInt32(dr["WARDNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DoctorName") && dr["DoctorName"] != "")
                    {
                        model.DoctorName = dr["DoctorName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Doctor") && dr["Doctor"] != "")
                    {
                        model.Doctor = int.Parse(dr["Doctor"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CollectDate") && dr["CollectDate"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["CollectDate"].ToString()))
                        {
                            model.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                            barCodeForm.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OperDate") && dr["OperDate"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                        {
                            model.OperDate = Convert.ToDateTime(dr["OperDate"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OperTime") && dr["OperTime"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                        {
                            model.OperTime = Convert.ToDateTime(dr["OperTime"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("PatNo") && dr["PatNo"] != "")
                    {
                        model.PatNo = dr["PatNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztype") && dr["jztype"] != "")
                    {
                        if (!string.IsNullOrEmpty(jzType))
                        {
                            model.jztype = Convert.ToInt32(jzType);
                        }
                        if (!string.IsNullOrEmpty(dr["jztype"].ToString()))
                        {
                            model.jztype = Convert.ToInt32(dr["jztype"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Ageunitno") && dr["Ageunitno"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Ageunitno"].ToString()))
                        {
                            model.AgeUnitNo = Convert.ToInt32(dr["Ageunitno"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Collecttime") && dr["Collecttime"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Collecttime"].ToString()))
                        {
                            model.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                            barCodeForm.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                        }
                    }
                    nRequestItem.SerialNo = model.SerialNo;
                    nRequestItem.OldSerialNo = model.OldSerialNo;

                    Common.Log.Log.Info("ItemNo值:" + dr["ItemNo"]);
                    nRequestItem.WebLisSourceOrgID = model.WebLisSourceOrgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ItemNo"))
                    {
                        if (!string.IsNullOrEmpty(dr["ItemNo"].ToString()))
                        {
                            //nRequestItem.ParItemNo = Convert.ToInt32(dr["ItemNo"].ToString());
                            nRequestItem.ParItemNo = dr["ItemNo"].ToString();
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CombiItemNo"))
                    {
                        //ZhiFang.Common.Log.Log.Debug("1");
                        if (!string.IsNullOrEmpty(dr["CombiItemNo"].ToString()) && dr["CombiItemNo"].ToString().Trim() != "")
                        {
                            //ZhiFang.Common.Log.Log.Debug("2");
                            //nRequestItem.ParItemNo = Convert.ToInt32(dr["ItemNo"].ToString());
                            nRequestItem.CombiItemNo = dr["CombiItemNo"].ToString();
                            if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                            {
                                //ZhiFang.Common.Log.Log.Debug("3");
                                barCodeForm.ItemNo = dsBarCodeForm.Tables[0].Rows[0]["ItemNo"].ToString();
                                barCodeForm.ItemName = dsBarCodeForm.Tables[0].Rows[0]["ItemName"].ToString();
                                if (!dsBarCodeForm.Tables[0].Rows[0]["ItemNo"].ToString().Split(',').Contains(dr["CombiItemNo"].ToString()))
                                {
                                    //ZhiFang.Common.Log.Log.Debug("4");
                                    barCodeForm.ItemNo = barCodeForm.ItemNo + "," + dr["CombiItemNo"].ToString();
                                    barCodeForm.ItemName = barCodeForm.ItemName + "," + dr["CombiItemName"].ToString();
                                }
                            }
                            else
                            {
                                //ZhiFang.Common.Log.Log.Debug("5");
                                barCodeForm.ItemNo = dr["CombiItemNo"].ToString();
                                barCodeForm.ItemName = dr["CombiItemName"].ToString();
                            }
                            //ZhiFang.Common.Log.Log.Debug("6");
                        }
                        //ZhiFang.Common.Log.Log.Debug("7");
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("FolkName"))
                    {
                        //ZhiFang.Common.Log.Log.Debug("1");
                        if (!string.IsNullOrEmpty(dr["FolkName"].ToString()))
                        {
                            //ZhiFang.Common.Log.Log.Debug("2");
                            model.FolkName = dr["FolkName"].ToString();
                            //ZhiFang.Common.Log.Log.Debug("3");
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("DeptName"))
                    {
                        if (!string.IsNullOrEmpty(dr["DeptName"].ToString()))
                        {
                            model.DeptName = dr["DeptName"].ToString();
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("FolkNo"))
                    {
                        if (!string.IsNullOrEmpty(dr["FolkNo"].ToString()))
                        {
                            model.FolkNo = int.Parse(dr["Diag"].ToString());
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Diag"))
                    {
                        if (!string.IsNullOrEmpty(dr["Diag"].ToString()))
                        {
                            model.Diag = dr["Diag"].ToString();
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Charge"))
                    {
                        if (!string.IsNullOrEmpty(dr["Charge"].ToString()))
                        {
                            model.Charge = int.Parse(dr["Charge"].ToString());
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Operator"))
                    {
                        if (!string.IsNullOrEmpty(dr["Operator"].ToString()))
                        {
                            model.Operator = dr["Operator"].ToString();
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("jztypeName"))
                    {
                        if (!string.IsNullOrEmpty(dr["jztypeName"].ToString()))
                        {
                            model.jztypeName = dr["jztypeName"].ToString();
                        }
                    }
                    #region 申请单
                    //DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = wsBarCode.Tables[0].Rows[0]["SerialNo"].ToString() });
                    DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = model.SerialNo, ClientNo = model.ClientNo });
                    if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                    {
                        if (dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"] != null && dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim() != "" && Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"]) >= 5)
                        {
                            sMsg += "SerialNo:" + model.SerialNo + ",ClientNo:" + model.ClientNo + "已被签收不能修改!";
                            Log.Info("RequestFormService.AppliyUpLoad.AppliyUpLoad_CreatNewBarCode,已被签收不能修改:NRequestFormNo" + model.NRequestFormNo);
                            return false;
                        }
                        model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                        Log.Info("RequestFormService.AppliyUpLoad.UpdateNRequestForm:NRequestFormNo" + model.NRequestFormNo);
                        i = ibnrf.Update(model);
                    }
                    else
                    {
                        //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                        //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                        //model.NRequestFormNo = Convert.ToInt64(strGUID);
                        model.NRequestFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        Log.Info("RequestFormService.AppliyUpLoad.AddNRequestForm:NRequestFormNo" + model.NRequestFormNo);
                        i = ibnrf.Add(model);
                    }
                    #endregion

                    #region 样本单
                    if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                    {
                        barCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                        i = ibbcf.Update(barCodeForm);
                    }
                    else
                    {
                        //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                        //barCodeForm.BarCodeFormNo = DateTime.Now.Ticks;
                        //barCodeForm.BarCodeFormNo = Convert.ToInt64(strGUID);
                        barCodeForm.BarCodeFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        i = ibbcf.Add(barCodeForm);
                    }
                    #endregion

                    #region 项目表
                    nRequestItem.BarCodeFormNo = barCodeForm.BarCodeFormNo;
                    nRequestItem.NRequestFormNo = model.NRequestFormNo;
                    DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = nRequestItem.BarCodeFormNo, ParItemNo = nRequestItem.ParItemNo });
                    if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                    {
                        nRequestItem.NRequestFormNo = long.Parse(dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"].ToString().Trim());
                        nRequestItem.NRequestItemNo = long.Parse(dsNRequestItem.Tables[0].Rows[0]["NRequestItemNo"].ToString().Trim());
                        i = ibnri.Update(nRequestItem);
                    }
                    else
                    {
                        Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@1");
                        nRequestItem.NRequestItemNo =ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        i = ibnri.Add(nRequestItem);
                        Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@2");
                    }
                    #endregion
                    Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@3");
                }
                Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@4");
                if (i > 0)
                {
                    Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@5");
                    sMsg += "申请单上传成功\r\n";
                    Log.Info(sMsg);
                    Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@6");
                    return true;
                }
                else
                {
                    sMsg += "申请单上传失败\r\n";
                    Log.Info(sMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                sMsg = "上传失败" + e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 上传申请_生成新条码
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="orgID"></param>
        /// <param name="jzType"></param>
        /// <param name="sMsg"></param>
        /// <returns></returns>
        public bool AppliyUpLoad_CreatNewBarCode(byte[] xmlData, string orgID, string jzType, out string sMsg)
        {
            System.Text.UTF8Encoding converter = new UTF8Encoding();
            IBbarCodeSeq ibcs = ZhiFang.BLLFactory.BLLFactory<IBbarCodeSeq>.GetBLL();
            string drs = converter.GetString(xmlData);

            Log.Info("RequestFormService.AppliyUpLoad_CreatNewBarCode.xmlData:" + drs);
            Log.Info("RequestFormService.AppliyUpLoad_CreatNewBarCode.orgID:" + orgID);
            Log.Info("RequestFormService.AppliyUpLoad_CreatNewBarCode.jzType:" + jzType);
            try
            {
                int i = 0;
                sMsg = "";
                string str = "";
                if (drs == null)
                {
                    sMsg = "dr为空,不能上传申请单\r\n";
                    Log.Info(sMsg);
                }
                StringReader strTemp = new StringReader(drs);
                Model.NRequestForm model = new Model.NRequestForm();
                DataSet wsBarCode = new DataSet();

                wsBarCode.ReadXml(strTemp);
                bool flag = CheckAppliy(wsBarCode.Tables[0], "Table", out sMsg);
                if (!flag)
                {
                    ZhiFang.Common.Log.Log.Info(sMsg);
                    return false;
                }

                if (!ibbcf.CheckBarCodeLab(wsBarCode, orgID, out str))
                {

                    sMsg = "验证对照代码错误！";
                    return false;
                }
                wsBarCode = MatchCenterNo(wsBarCode, orgID);
                //DataRow dr = wsBarCode.Tables[0].Rows[0];
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                Model.NRequestItem nRequestItem = new Model.NRequestItem();


                foreach (DataRow dr in wsBarCode.Tables[0].Rows)
                {
                    DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { SampleSendNo = dr["BarCode"].ToString(), WebLisSourceOrgId = orgID });
                    if (wsBarCode.Tables[0].Columns.Contains("BarCode") && dr["BarCode"] != "")
                    {
                        if (dsBarCodeForm != null && dsBarCodeForm.Tables.Count > 0 && dsBarCodeForm.Tables[0].Rows.Count > 0)
                        {
                            barCodeForm.BarCode = dsBarCodeForm.Tables[0].Rows[0]["BarCode"].ToString();
                        }
                        else
                        {
                            barCodeForm.BarCode = ibcs.GetBarCode(orgID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        barCodeForm.SampleSendNo = dr["BarCode"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleTypeNo") && dr["SampleTypeNo"] != "")
                    {
                        barCodeForm.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        model.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        nRequestItem.SampleTypeNo = model.SampleTypeNo;
                    }
                    
                    if (wsBarCode.Tables[0].Columns.Contains("Birthday") && dr["Birthday"] != "")
                    {
                        model.Birthday = Convert.ToDateTime(dr["Birthday"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Birdthday") && dr["Birdthday"] != "")
                    {
                        model.Birthday = Convert.ToDateTime(dr["Birdthday"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        barCodeForm.SampleType = dr["SampleType"].ToString();
                        barCodeForm.SampleTypeName = dr["SampleType"].ToString();
                    }
                    //
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgName") && dr["WebLisOrgName"] != "")
                    {
                        barCodeForm.WebLisOrgName = dr["WebLisOrgName"].ToString();
                        model.WebLisOrgName = dr["WebLisOrgName"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("PersonID") && dr["PersonID"] != "")
                    {
                        model.PersonID = dr["PersonID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        model.SampleType = dr["SampleType"].ToString();
                        nRequestItem.SampleType = dr["SampleType"].ToString();
                       
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TelNo") && dr["TelNo"] != "")
                    {
                        model.TelNo = dr["TelNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        model.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("STATUSName") && dr["STATUSName"] != "")
                    {
                        model.STATUSName = dr["STATUSName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztypeName") && dr["jztypeName"] != "")
                    {
                        model.jztypeName = dr["jztypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckType") && dr["CheckType"] != "")
                    {
                        nRequestItem.CheckType = dr["CheckType"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckTypeName") && dr["CheckTypeName"] != "")
                    {
                        nRequestItem.CheckTypeName = dr["CheckTypeName"].ToString();
                    }
                    //


                    barCodeForm.WebLisSourceOrgId = orgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ClientName") && dr["ClientName"] != "")
                    {
                        barCodeForm.ClientName = dr["ClientName"].ToString();
                        model.ClientName = dr["ClientName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgName") && dr["WebLisSourceOrgName"] != "")
                    {
                        barCodeForm.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        model.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        nRequestItem.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        //model.zdy5 = dr["WebLisSourceOrgName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TESTTYPENO") && dr["TESTTYPENO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["TESTTYPENO"].ToString()))
                        {
                            model.TestTypeNo = Convert.ToInt32(dr["TESTTYPENO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("STATUSNO") && dr["STATUSNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["STATUSNO"].ToString()))
                        {
                            model.StatusNo = Convert.ToInt32(dr["STATUSNO"]);
                        }

                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNO") && dr["DISTRICTNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DISTRICTNO"].ToString()))
                        {
                            model.DistrictNo = Convert.ToInt32(dr["DISTRICTNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNAME") && dr["DISTRICTNAME"] != "")
                    {
                        model.DistrictName = dr["DISTRICTNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DeptNo") && dr["DeptNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DeptNo"].ToString()))
                        {
                            model.DeptNo = Convert.ToInt32(dr["DeptNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DeptName"))
                    {
                        model.DeptName = dr["DeptName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        barCodeForm.WebLisOrgID = dr["WebLisOrgID"].ToString();
                        nRequestItem.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SerialNo") && dr["SerialNo"] != "")
                    {
                        model.SerialNo = dr["SerialNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Bed") && dr["Bed"] != "")
                    {
                        model.Bed = dr["Bed"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OldSerialNo") && dr["OldSerialNo"] != "")
                    {
                        model.OldSerialNo = dr["OldBarCode"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("Color") && dr["Color"] != "")
                    {
                        barCodeForm.Color = dr["Color"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ItemName") && dr["ItemName"] != "")
                    {
                        barCodeForm.ItemName = dr["ItemName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ItemNo") && dr["ItemNo"] != "")
                    {
                        barCodeForm.ItemNo = dr["ItemNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleTypeName") && dr["SampleTypeName"] != "")
                    {
                        barCodeForm.SampleTypeName = dr["SampleTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OrderNo") && dr["OrderNo"] != "")
                    {
                        barCodeForm.OrderNo = dr["OrderNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("LabItemName") && dr["LabItemName"] != "")
                    {
                        barCodeForm.LabItemName = dr["LabItemName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("LabItemNo") && dr["LabItemNo"] != "")
                    {
                        barCodeForm.LabItemNo = dr["LabItemNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("zdy1") && dr["zdy1"] != "")
                    {
                        model.zdy1 = dr["zdy1"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy2") && dr["zdy2"] != "")
                    {
                        model.zdy2 = dr["zdy2"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy3") && dr["zdy3"] != "")
                    {
                        model.zdy3 = dr["zdy3"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy4") && dr["zdy4"] != "")
                    {
                        model.zdy4 = dr["zdy4"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy5") && dr["zdy5"] != "")
                    {
                        model.zdy5 = dr["zdy5"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy6") && dr["zdy6"] != "")
                    {
                        model.ZDY6 = dr["zdy6"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy7") && dr["zdy7"] != "")
                    {
                        model.ZDY7 = dr["zdy7"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy8") && dr["zdy8"] != "")
                    {
                        model.ZDY8 = dr["zdy8"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy9") && dr["zdy9"] != "")
                    {
                        model.ZDY9 = dr["zdy9"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy10") && dr["zdy10"] != "")
                    {
                        model.ZDY10 = dr["zdy10"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ClientNo") && dr["ClientNo"] != "")
                    {
                        barCodeForm.ClientNo = dr["ClientNo"].ToString();
                        model.ClientNo = dr["ClientNo"].ToString();
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    else
                    {
                        barCodeForm.ClientNo = orgID;
                        model.ClientNo = orgID;
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CName") && dr["CName"] != "")
                    {
                        model.CName = dr["CName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Age") && dr["Age"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        {
                            model.Age = Convert.ToDecimal(dr["Age"].ToString());
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("AgeUnitName") && dr["AgeUnitName"] != "")
                    {
                        model.AgeUnitName = dr["AgeUnitName"].ToString();       //病人类型 如门诊/住院
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ClinicTypeName") && dr["ClinicTypeName"] != "")
                    {
                        model.ClinicTypeName = dr["ClinicTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TestTypeName") && dr["TestTypeName"] != "")  // 检验类型 如常/急/质
                    {
                        model.TestTypeName = dr["TestTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderNo") && dr["GenderNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["GenderNo"].ToString()))
                        {
                            model.GenderNo = Convert.ToInt32(dr["GenderNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderName") && dr["GenderName"] != "")
                    {
                        model.GenderName = dr["GenderName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNAME") && dr["WARDNAME"] != "")
                    {
                        model.WardName = dr["WARDNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNO") && dr["WARDNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["WARDNO"].ToString()))
                        {
                            model.WardNo = Convert.ToInt32(dr["WARDNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DoctorName") && dr["DoctorName"] != "")
                    {
                        model.DoctorName = dr["DoctorName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Doctor") && dr["Doctor"] != "")
                    {
                        model.Doctor = int.Parse(dr["Doctor"].ToString());
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CollectDate") && dr["CollectDate"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["CollectDate"].ToString()))
                        {
                            model.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                            barCodeForm.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("OperDate") && dr["OperDate"] != "" && !string.IsNullOrEmpty(dr["OperDate"].ToString()))
                    {
                        model.OperDate = Convert.ToDateTime(dr["OperDate"]);
                    }
                    else
                    {
                        model.OperDate = DateTime.Now;
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("OperTime") && dr["OperTime"] != "" && !string.IsNullOrEmpty(dr["OperTime"].ToString()))
                    {
                        model.OperTime = Convert.ToDateTime(dr["OperTime"]);
                    }
                    else
                    {
                        model.OperTime = DateTime.Now;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("PatNo") && dr["PatNo"] != "")
                    {
                        model.PatNo = dr["PatNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztype") && dr["jztype"] != "")
                    {
                        if (!string.IsNullOrEmpty(jzType))
                        {
                            model.jztype = Convert.ToInt32(jzType);
                        }
                        if (!string.IsNullOrEmpty(dr["jztype"].ToString()))
                        {
                            model.jztype = Convert.ToInt32(dr["jztype"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Ageunitno") && dr["Ageunitno"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Ageunitno"].ToString()))
                        {
                            model.AgeUnitNo = Convert.ToInt32(dr["Ageunitno"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Collecttime") && dr["Collecttime"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Collecttime"].ToString()))
                        {
                            model.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                            barCodeForm.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                        }
                    }
                    nRequestItem.SerialNo = model.SerialNo;
                    nRequestItem.OldSerialNo = model.OldSerialNo;

                    Common.Log.Log.Info("ItemNo值:" + dr["ItemNo"]);
                    nRequestItem.WebLisSourceOrgID = model.WebLisSourceOrgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ItemNo"))
                    {
                        if (!string.IsNullOrEmpty(dr["ItemNo"].ToString()))
                        {
                            //nRequestItem.ParItemNo = Convert.ToInt32(dr["ItemNo"].ToString());
                            nRequestItem.ParItemNo = dr["ItemNo"].ToString();
                        }
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("CombiItemNo"))
                    {
                        if (!string.IsNullOrEmpty(dr["CombiItemNo"].ToString())&& dr["CombiItemNo"].ToString().Trim()!="")
                        {
                            //nRequestItem.ParItemNo = Convert.ToInt32(dr["ItemNo"].ToString());
                            nRequestItem.CombiItemNo= dr["CombiItemNo"].ToString();
                            if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                            {
                                barCodeForm.ItemNo = dsBarCodeForm.Tables[0].Rows[0]["ItemNo"].ToString();
                                barCodeForm.ItemName = dsBarCodeForm.Tables[0].Rows[0]["ItemName"].ToString();
                                if (dsBarCodeForm.Tables[0].Rows[0]["ItemNo"].ToString().IndexOf(dr["CombiItemNo"].ToString() + ",") < 0)
                                {
                                    barCodeForm.ItemNo = barCodeForm.ItemNo + dr["CombiItemNo"].ToString() + ",";
                                    barCodeForm.ItemName = barCodeForm.ItemName + dr["CombiItemName"].ToString() + ",";
                                }
                            }
                            else
                            {
                                barCodeForm.ItemNo = dr["CombiItemNo"].ToString() + ",";
                                barCodeForm.ItemName = dr["CombiItemName"].ToString() + ",";
                            }
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Diag"))
                    {
                        if (!string.IsNullOrEmpty(dr["Diag"].ToString()))
                        {
                            model.Diag = dr["Diag"].ToString();
                        }
                    }

                    #region 申请单
                    //DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = wsBarCode.Tables[0].Rows[0]["SerialNo"].ToString() });
                    DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = model.SerialNo, ClientNo = model.ClientNo });
                    if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                    {

                        if (dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"] != null && dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim() != "" && Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["WebLisFlag"]) >= 5)
                        {
                            sMsg += "SerialNo:" + model.SerialNo + ",ClientNo:" + model.ClientNo + "已被签收不能修改!";
                            Log.Info("RequestFormService.AppliyUpLoad.AppliyUpLoad_CreatNewBarCode,已被签收不能修改:NRequestFormNo" + model.NRequestFormNo);
                            return false;
                        }
                        model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                        i = ibnrf.Update(model);
                    }
                    else
                    {
                        //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                        //string strGUID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                        long strGUID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();//直接返回字符串类型
                        model.NRequestFormNo = Convert.ToInt64(strGUID);
                        i = ibnrf.Add(model);
                    }
                    #endregion
                    #region 样本单
                    if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                    {
                        barCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                        i = ibbcf.Update(barCodeForm);
                    }
                    else
                    {
                        //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                        //barCodeForm.BarCodeFormNo = DateTime.Now.Ticks;
                        //barCodeForm.BarCodeFormNo = Convert.ToInt64(strGUID);
                        barCodeForm.BarCodeFormNo= ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        i = ibbcf.Add(barCodeForm);
                    }
                    #endregion

                    #region 项目表
                    nRequestItem.BarCodeFormNo = barCodeForm.BarCodeFormNo;
                    nRequestItem.NRequestFormNo = model.NRequestFormNo;
                    DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = nRequestItem.BarCodeFormNo, ParItemNo = nRequestItem.ParItemNo });
                    if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                    {
                        nRequestItem.NRequestFormNo = Convert.ToInt64(dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"]);
                        nRequestItem.NRequestItemNo = Convert.ToInt32(dsNRequestItem.Tables[0].Rows[0]["NRequestItemNo"]);
                        i = ibnri.Update(nRequestItem);
                    }
                    else
                    {

                        i = ibnri.Add(nRequestItem);
                    }
                    #endregion
                }
                if (i > 0)
                {
                    sMsg += "申请单上传成功\r\n";
                    Log.Info(sMsg);
                    return true;
                }
                else
                {
                    sMsg += "申请单上传失败\r\n";
                    Log.Info(sMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                sMsg = "上传失败" + e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 申请单上传 博尔诚定制 
        /// </summary>
        /// <param name="xmlData">申请单数据</param>
        /// <param name="orgID">送检单位编号</param>
        /// <param name="sMsg">错误信息</param>
        /// <returns></returns>
        public bool AppliyUpLoad_BoErCheng(byte[] xmlData, string orgID,  out string sMsg)
        {
            System.Text.UTF8Encoding converter = new UTF8Encoding();
            string drs = converter.GetString(xmlData);

            try
            {
                int i = 0;
                sMsg = "";
                string str = "";
                if (drs == null)
                {
                    sMsg = "dr为空,不能上传申请单\r\n";
                    Log.Info(sMsg);
                }
                StringReader strTemp = new StringReader(drs);
                Model.NRequestForm model = new Model.NRequestForm();
                DataSet wsBarCode = new DataSet();

                wsBarCode.ReadXml(strTemp);
              
                if (!ibbcf.CheckBarCodeLab(wsBarCode, orgID, out str))
                {

                    sMsg += str;
                    return false;
                }
                wsBarCode = MatchCenterNo(wsBarCode, orgID);
              
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                Model.NRequestItem nRequestItem = new Model.NRequestItem();


                foreach (DataRow dr in wsBarCode.Tables[0].Rows)
                {
                 
                    if (wsBarCode.Tables[0].Columns.Contains("BarCode") && dr["BarCode"] != "")
                    {
                        barCodeForm.BarCode = dr["BarCode"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleTypeNo") && dr["SampleTypeNo"] != "")
                    {
                        barCodeForm.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        model.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                        nRequestItem.SampleTypeNo = model.SampleTypeNo;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        barCodeForm.SampleType = dr["SampleType"].ToString();
                    }
                    //
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgName") && dr["WebLisOrgName"] != "")
                    {
                        barCodeForm.WebLisOrgName = dr["WebLisOrgName"].ToString();
                        model.WebLisOrgName = dr["WebLisOrgName"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("PersonID") && dr["PersonID"] != "")
                    {
                        model.PersonID = dr["PersonID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                    {
                        model.SampleType = dr["SampleType"].ToString();
                        nRequestItem.SampleType = dr["SampleType"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TelNo") && dr["TelNo"] != "")
                    {
                        model.TelNo = dr["TelNo"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        model.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("STATUSName") && dr["STATUSName"] != "")
                    {
                        model.STATUSName = dr["STATUSName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztypeName") && dr["jztypeName"] != "")
                    {
                        model.jytype = dr["jztypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckType") && dr["CheckType"] != "")
                    {
                        nRequestItem.CheckType = dr["CheckType"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CheckTypeName") && dr["CheckTypeName"] != "")
                    {
                        nRequestItem.CheckTypeName = dr["CheckTypeName"].ToString();
                    }
                   

                    barCodeForm.WebLisSourceOrgId = orgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ClientName") && dr["ClientName"] != "")
                    {
                        barCodeForm.ClientName = dr["ClientName"].ToString();
                        model.ClientName = dr["ClientName"].ToString();
                        nRequestItem.ClientName = dr["ClientName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgName") && dr["WebLisSourceOrgName"] != "")
                    {
                        barCodeForm.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        model.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                        //model.zdy5 = dr["WebLisSourceOrgName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TESTTYPENO") && dr["TESTTYPENO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["TESTTYPENO"].ToString()))
                        {
                            model.TestTypeNo = Convert.ToInt32(dr["TESTTYPENO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("STATUSNO") && dr["STATUSNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["STATUSNO"].ToString()))
                        {
                            model.StatusNo = Convert.ToInt32(dr["STATUSNO"]);
                        }

                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNO") && dr["DISTRICTNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DISTRICTNO"].ToString()))
                        {
                            model.DistrictNo = Convert.ToInt32(dr["DISTRICTNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNAME") && dr["DISTRICTNAME"] != "")
                    {
                        model.DistrictName = dr["DISTRICTNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("DeptNo") && dr["DeptNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["DeptNo"].ToString()))
                        {
                            model.DeptNo = Convert.ToInt32(dr["DeptNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                    {
                        barCodeForm.WebLisOrgID = dr["WebLisOrgID"].ToString();
                        nRequestItem.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("SerialNo") && dr["SerialNo"] != "")
                    {
                        model.SerialNo = dr["SerialNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Bed") && dr["Bed"] != "")
                    {
                        model.Bed = dr["Bed"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OldSerialNo") && dr["OldSerialNo"] != "")
                    {
                        model.OldSerialNo = dr["OldBarCode"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("zdy1") && dr["zdy1"] != "")
                    {
                        model.zdy1 = dr["zdy1"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ClientNo") && dr["ClientNo"] != "")
                    {
                        barCodeForm.ClientNo = dr["ClientNo"].ToString();
                        model.ClientNo = dr["ClientNo"].ToString();
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    else
                    {
                        barCodeForm.ClientNo = orgID;
                        model.ClientNo = orgID;
                        model.WebLisSourceOrgID = model.ClientNo;
                        nRequestItem.ClientNo = model.ClientNo;
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("CName") && dr["CName"] != "")
                    {
                        model.CName = dr["CName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Age") && dr["Age"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                        {
                            model.Age = Convert.ToDecimal(dr["Age"].ToString());
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("AgeUnitName") && dr["AgeUnitName"] != "")
                    {
                        model.AgeUnitName = dr["AgeUnitName"].ToString();       //病人类型 如门诊/住院
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("ClinicTypeName") && dr["ClinicTypeName"] != "")
                    {
                        model.ClinicTypeName = dr["ClinicTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("TestTypeName") && dr["TestTypeName"] != "")  // 检验类型 如常/急/质
                    {
                        model.TestTypeName = dr["TestTypeName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderNo") && dr["GenderNo"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["GenderNo"].ToString()))
                        {
                            model.GenderNo = Convert.ToInt32(dr["GenderNo"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("GenderName") && dr["GenderName"] != "")
                    {
                        model.GenderName = dr["GenderName"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNAME") && dr["WARDNAME"] != "")
                    {
                        model.WardName = dr["WARDNAME"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("WARDNO") && dr["WARDNO"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["WARDNO"].ToString()))
                        {
                            model.WardNo = Convert.ToInt32(dr["WARDNO"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Doctor") && dr["Doctor"] != "")
                    {
                        model.DoctorName = dr["Doctor"].ToString();
                    }

                    if (wsBarCode.Tables[0].Columns.Contains("CollectDate") && dr["CollectDate"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["CollectDate"].ToString()))
                        {
                            model.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OperDate") && dr["OperDate"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                        {
                            model.OperDate = Convert.ToDateTime(dr["OperDate"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("OperTime") && dr["OperTime"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                        {
                            model.OperTime = Convert.ToDateTime(dr["OperTime"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("PatNo") && dr["PatNo"] != "")
                    {
                        model.PatNo = dr["PatNo"].ToString();
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("jztype") && dr["jztype"] != "")
                    {
                      
                        if (!string.IsNullOrEmpty(dr["jztype"].ToString()))
                        {
                            model.jztype = Convert.ToInt32(dr["jztype"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Ageunitno") && dr["Ageunitno"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Ageunitno"].ToString()))
                        {
                            model.AgeUnitNo = Convert.ToInt32(dr["Ageunitno"]);
                        }
                    }
                    if (wsBarCode.Tables[0].Columns.Contains("Collecttime") && dr["Collecttime"] != "")
                    {
                        if (!string.IsNullOrEmpty(dr["Collecttime"].ToString()))
                        {
                            model.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                        }
                    }
                    nRequestItem.SerialNo = model.SerialNo;
                    nRequestItem.OldSerialNo = model.OldSerialNo;

                    Common.Log.Log.Info("ItemNo值:" + dr["ItemNo"]);
                    nRequestItem.WebLisSourceOrgID = model.WebLisSourceOrgID;
                    if (wsBarCode.Tables[0].Columns.Contains("ItemNo"))
                    {
                        if (!string.IsNullOrEmpty(dr["ItemNo"].ToString()))
                        {
                            
                            nRequestItem.ParItemNo = dr["ItemNo"].ToString();
                        }
                    }
                    DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode =barCodeForm.BarCode });

                    if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                    {
                        barCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                        i = ibbcf.Update(barCodeForm);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("更新barCodeForm表是否成功:"+i);
                    }
                    else
                    {
                        //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                        //barCodeForm.BarCodeFormNo = DateTime.Now.Ticks;
                        //barCodeForm.BarCodeFormNo = Convert.ToInt64(strGUID);
                        barCodeForm.BarCodeFormNo= ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); 
                        i = ibbcf.Add(barCodeForm);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("插入barCodeForm表是否成功:" + i);
                    }
                    //DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = wsBarCode.Tables[0].Rows[0]["SerialNo"].ToString() });
                    DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = model.SerialNo,ClientNo=model.ClientNo });
                    if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                    {
                        model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                        i = ibnrf.Update(model);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("更新NRequestForm表是否成功:" + i);
                    }
                    else
                    {
                        //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                        //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                        //model.NRequestFormNo = Convert.ToInt64(strGUID);
                        model.NRequestFormNo= ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        i = ibnrf.Add(model);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("插入NRequestForm表是否成功:" + i);
                    }
                    nRequestItem.BarCodeFormNo = barCodeForm.BarCodeFormNo;
                    nRequestItem.NRequestFormNo = model.NRequestFormNo;
                    DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = nRequestItem.BarCodeFormNo, ParItemNo = nRequestItem.ParItemNo });
                    if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                    {
                        nRequestItem.NRequestFormNo = Convert.ToInt64(dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"]);
                        nRequestItem.NRequestItemNo = Convert.ToInt32(dsNRequestItem.Tables[0].Rows[0]["NRequestItemNo"]);
                        i = ibnri.Update(nRequestItem);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("更新NRequestItem表是否成功:" + i);
                    }
                    else
                    {
                        i = ibnri.Add(nRequestItem);
                        ZhiFang.Common.Log.Log.Info("RequestFormService.svc->AppliyUpLoad_BoErCheng");
                        ZhiFang.Common.Log.Log.Info("插入NRequestItem表是否成功:" + i);
                    }
                }
                if (i > 0)
                {
                    sMsg += "申请单上传成功\r\n";
                    Log.Info(sMsg);
                    return true;
                }
                else
                {
                    sMsg += "申请单上传失败\r\n";
                    Log.Info(sMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                sMsg = "上传失败" + e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 检验字段
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public bool CheckAppliy(DataTable dt, string tableName, out string messages)
        {
            messages = "";
            string Field = "";
            switch (tableName)
            {
                case "ReportForm":
                    Field = ConfigHelper.GetConfigString("UpLoadReportFormValidation");
                    break;
                case "ReportItem":
                    Field = ConfigHelper.GetConfigString("UpLoadReportItemValidation");
                    break;
                case "ReportMicro":
                    Field = ConfigHelper.GetConfigString("UpLoadReportMicroValidation");
                    break;
                case "ReportMarrow":
                    Field = ConfigHelper.GetConfigString("UpLoadReportMarrowValidation");
                    break;
                case "Table":
                    Field = ConfigHelper.GetConfigString("UpLoadBarCodeFormValidation");
                    break;
                case "RequestForm":
                    Field = ConfigHelper.GetConfigString("UpLoadRequestFormValidation");
                    break;
                case "RequestItem":
                    Field = ConfigHelper.GetConfigString("UpLoadRequestItemValidation");
                    break;
            }
            return BLL.Common.CheckField.IsColumnField(dt, Field, out messages);
        }
        //验证是否齐全
        private bool CheckXML(DataSet wsBarCode, out string error)
        {
            error = "";
            try
            {
                string[] strArray = ConfigHelper.GetConfigString("CheckXML").Split(new char[] { ';' });
                int a = 0;
                if (wsBarCode != null && wsBarCode.Tables.Count > 0 && wsBarCode.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        for (int i = 0; i < wsBarCode.Tables[0].Columns.Count; i++)
                        {
                            Log.Error(wsBarCode.Tables[0].Columns[i].ColumnName.ToUpper() + "==" + strArray[j]);
                            if (wsBarCode.Tables[0].Columns[i].ColumnName.ToUpper() == strArray[j].ToUpper())
                            {
                                a = 1;
                                break;
                            }
                            else
                            {
                                a = 0;
                            }
                        }
                        if (a == 0)
                        {
                            error = "检测到“" + strArray[j] + "”不存在,XML文件不完整！";
                            return false;
                        }

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.TargetSite + ex.Message);
                error = ex.Message;
                return false;
            }
        }

        #region 源版本（通用）
        public bool UpLoadRequestFormClient(string drs, string orgID, string jzType, out string strMsg)
        {
            Log.Info(drs);
            Log.Info(orgID);
            try
            {
                strMsg = "";
                string str = "";
                if (drs == null)
                {
                    strMsg = "dr为空,不能上传申请单\r\n";
                    Log.Info(strMsg);
                }
                int count = 0;

                StringReader strTemp = new StringReader(drs);
                Model.NRequestForm model = new Model.NRequestForm();
                DataSet wsBarCode = new DataSet();

                wsBarCode.ReadXml(strTemp);
                DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = wsBarCode.Tables[0].Rows[0]["BarCode"].ToString() });
                if (!ibbcf.CheckBarCodeLab(wsBarCode, orgID, out str))
                {
                    strMsg += str;
                    return false;
                }
                wsBarCode = MatchCenterNo(wsBarCode, orgID);
                DataRow dr = wsBarCode.Tables[0].Rows[0];
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                barCodeForm.ItemName = "";
                barCodeForm.ItemNo = "";
                Model.NRequestItem nRequestItem = new Model.NRequestItem();

                if (wsBarCode.Tables[0].Columns.Contains("BarCode"))
                {
                    barCodeForm.BarCode = dr["BarCode"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("SampleTypeNo"))
                {
                    barCodeForm.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                    model.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                    nRequestItem.SampleTypeNo = model.SampleTypeNo;
                }
                if (wsBarCode.Tables[0].Columns.Contains("DoctorName"))
                {
                    model.DoctorName = dr["DoctorName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("DeptName"))
                {
                    model.DeptName = dr["DeptName"].ToString();
                }

                if (wsBarCode.Tables[0].Columns.Contains("zdy2"))
                {
                    model.zdy2 = dr["zdy2"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy3"))
                {
                    model.zdy3 = dr["zdy3"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy4"))
                {
                    model.zdy4 = dr["zdy4"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy5"))
                {
                    model.zdy5 = dr["zdy5"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy6"))
                {
                    model.ZDY6 = dr["zdy6"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy7"))
                {
                    model.ZDY7 = dr["zdy7"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy8"))
                {
                    model.ZDY8 = dr["zdy8"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy9"))
                {
                    model.ZDY9 = dr["zdy9"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy10"))
                {
                    model.ZDY10 = dr["zdy10"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("CollectTime"))
                {
                    barCodeForm.CollectTime = Convert.ToDateTime(dr["CollectTime"]);
                }
                if (wsBarCode.Tables[0].Columns.Contains("Collecter"))
                {
                    barCodeForm.Collecter = dr["Collecter"].ToString();
                }

                if (wsBarCode.Tables[0].Columns.Contains("SampleType"))
                {
                    barCodeForm.SampleType = dr["SampleType"].ToString();
                }
                barCodeForm.WebLisSourceOrgId = orgID;
                if (wsBarCode.Tables[0].Columns.Contains("ClientName"))
                {
                    barCodeForm.ClientName = dr["ClientName"].ToString();
                    model.ClientName = dr["ClientName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgName"))
                {
                    barCodeForm.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                    model.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("TESTTYPENO"))
                {
                    if (!string.IsNullOrEmpty(dr["TESTTYPENO"].ToString()))
                    {
                        model.TestTypeNo = Convert.ToInt32(dr["TESTTYPENO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("STATUSNO"))
                {
                    if (!string.IsNullOrEmpty(dr["STATUSNO"].ToString()))
                    {
                        model.StatusNo = Convert.ToInt32(dr["STATUSNO"]);
                    }

                }
                if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNO"))
                {
                    if (!string.IsNullOrEmpty(dr["DISTRICTNO"].ToString()))
                    {
                        model.DistrictNo = Convert.ToInt32(dr["DISTRICTNO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNAME"))
                {
                    model.DistrictName = dr["DISTRICTNAME"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("DeptNo"))
                {
                    if (!string.IsNullOrEmpty(dr["DeptNo"].ToString()))
                    {
                        model.DeptNo = Convert.ToInt32(dr["DeptNo"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID"))
                {
                    barCodeForm.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    nRequestItem.WebLisOrgID = dr["WebLisOrgID"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("SerialNo"))
                {
                    model.SerialNo = dr["SerialNo"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("Bed"))
                {
                    model.Bed = dr["Bed"].ToString();
                }
                ZhiFang.Common.Log.Log.Info("判断是否存在OldSerialNo，录入OldBarCode。");
                if (wsBarCode.Tables[0].Columns.Contains("OldSerialNo"))
                {
                    ZhiFang.Common.Log.Log.Info("存在OldSerialNo:" + dr["OldBarCode"].ToString());
                    model.OldSerialNo = dr["OldBarCode"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy1"))
                {
                    model.zdy1 = dr["zdy1"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("ClientNo"))
                {
                    barCodeForm.ClientNo = dr["ClientNo"].ToString();
                    model.ClientNo = dr["ClientNo"].ToString();
                    model.WebLisSourceOrgID = model.ClientNo;
                    nRequestItem.ClientNo = model.ClientNo;
                }
                else
                {
                    barCodeForm.ClientNo = orgID;
                    model.ClientNo = orgID;
                    model.WebLisSourceOrgID = model.ClientNo;
                    nRequestItem.ClientNo = model.ClientNo;
                }
                if (wsBarCode.Tables[0].Columns.Contains("CName"))
                {
                    model.CName = dr["CName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("Age"))
                {
                    if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                    {
                        model.Age = Convert.ToDecimal(dr["Age"].ToString());
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("AgeUnitName"))
                {
                    model.AgeUnitName = dr["AgeUnitName"].ToString();       //病人类型 如门诊/住院
                }
                if (wsBarCode.Tables[0].Columns.Contains("ClinicTypeName"))
                {
                    model.ClinicTypeName = dr["ClinicTypeName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("TestTypeName"))  // 检验类型 如常/急/质
                {
                    model.TestTypeName = dr["TestTypeName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("GenderNo"))
                {
                    if (!string.IsNullOrEmpty(dr["GenderNo"].ToString()))
                    {
                        model.GenderNo = Convert.ToInt32(dr["GenderNo"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("GenderName"))
                {
                    model.GenderName = dr["GenderName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WARDNAME"))
                {
                    model.WardName = dr["WARDNAME"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WARDNO"))
                {
                    if (!string.IsNullOrEmpty(dr["WARDNO"].ToString()))
                    {
                        model.WardNo = Convert.ToInt32(dr["WARDNO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Doctor"))
                {
                    model.DoctorName = dr["Doctor"].ToString();
                }
                //string str1 = dr["CollectDate"].ToString();
                //System.IFormatProvider format = new System.Globalization.CultureInfo("zh-CN", true);
                //DateTime collectTime = DateTime.ParseExact(str1, "yyyyMMdd", format);
                //model.CollectDate = Convert.ToDateTime(collectTime);
                if (wsBarCode.Tables[0].Columns.Contains("CollectDate"))
                {
                    if (!string.IsNullOrEmpty(dr["CollectDate"].ToString()))
                    {
                        model.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                        barCodeForm.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("OperDate"))
                {
                    if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                    {
                        model.OperDate = Convert.ToDateTime(dr["OperDate"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("OperTime"))
                {
                    if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                    {
                        model.OperTime = Convert.ToDateTime(dr["OperTime"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("PatNo"))
                {
                    model.PatNo = dr["PatNo"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("jztype"))
                {
                    if (!string.IsNullOrEmpty(jzType))
                    {
                        model.jztype = Convert.ToInt32(jzType);
                    }
                    else
                    {
                        model.jztype = 0;
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Ageunitno"))
                {
                    if (!string.IsNullOrEmpty(dr["Ageunitno"].ToString()))
                    {
                        model.AgeUnitNo = Convert.ToInt32(dr["Ageunitno"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Collecttime"))
                {
                    if (!string.IsNullOrEmpty(dr["Collecttime"].ToString()))
                    {
                        model.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                    }
                }
                nRequestItem.SerialNo = model.SerialNo;
                nRequestItem.OldSerialNo = model.OldSerialNo;

                if (wsBarCode.Tables[0].Columns.Contains("ItemNo"))
                {
                    if (!string.IsNullOrEmpty(dr["ItemNo"].ToString()))
                    {
                        //nRequestItem.ParItemNo = Convert.ToInt32(dr["ItemNo"].ToString());
                        nRequestItem.ParItemNo = dr["ItemNo"].ToString();
                    }
                }
                nRequestItem.WebLisSourceOrgID = model.WebLisSourceOrgID;
                if (wsBarCode.Tables[0].Columns.Contains("ParItemNo"))
                {
                    if (!string.IsNullOrEmpty(dr["ParItemNo"].ToString()))
                    {
                        nRequestItem.CombiItemNo = dr["ParItemNo"].ToString().ToString();
                        if (barCodeForm.ItemNo == null || barCodeForm.ItemNo.Trim() == "")
                        {
                            barCodeForm.ItemNo += dr["ParItemNo"].ToString().Trim() + ",";
                        }
                        else
                        {
                            if (!barCodeForm.ItemNo.Split(',').Contains(dr["ParItemNo"].ToString().Trim()))
                            {
                                barCodeForm.ItemNo += dr["ParItemNo"].ToString().Trim() + ",";
                            }
                        }
                        barCodeForm.ItemNo.Remove(barCodeForm.ItemNo.LastIndexOf(','));
                    }
                }

                int i = 0;
                if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                {
                    barCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                    i = ibbcf.Update(barCodeForm);
                }
                else
                {
                    //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                    //barCodeForm.BarCodeFormNo = DateTime.Now.Ticks;
                    barCodeForm.BarCodeFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                    i = ibbcf.Add(barCodeForm);
                }
                DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = model.SerialNo,ClientNo=model.ClientNo });
                if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                {
                    model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                    i = ibnrf.Update(model);
                }
                else
                {
                    //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                    //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                    model.NRequestFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                    i = ibnrf.Add(model);
                }
                nRequestItem.BarCodeFormNo = barCodeForm.BarCodeFormNo;
                nRequestItem.NRequestFormNo = model.NRequestFormNo;
                DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = nRequestItem.BarCodeFormNo, ParItemNo = nRequestItem.ParItemNo });
                if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                {
                    nRequestItem.NRequestFormNo = Convert.ToInt64(dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"]);
                    nRequestItem.NRequestItemNo = Convert.ToInt32(dsNRequestItem.Tables[0].Rows[0]["NRequestItemNo"]);
                    i = ibnri.Update(nRequestItem);
                }
                else
                {

                    i = ibnri.Add(nRequestItem);
                }
                if (i > 0)
                {
                    strMsg += "申请单上传成功\r\n";
                    Log.Info(strMsg);
                    return true;
                }
                else
                {
                    strMsg += "申请单上传失败\r\n";
                    Log.Info(strMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                strMsg = "上传失败" + e.ToString();
                return false;
            }
        }

        #endregion

        #region 申请上传 PKI
        /// <summary>
        /// PKI定制 上传不需要转码
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="orgID"></param>
        /// <param name="jzType"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool UpLoadRequestFormClient_PKI(string drs, string orgID, string jzType, out string strMsg)
        {
            Log.Info("RequestFormService.svc->UpLoadRequestFormClient_PKI调用服务开始上传!");
            Log.Info("RequestFormService.svc->UpLoadRequestFormClient_PKI调用服务上传的数据:" + drs);
            try
            {
                strMsg = "";
                string str = "";
                if (drs == null)
                {
                    strMsg = "数据为空,不能上传申请单\r\n";
                    Log.Info(strMsg);
                    return false;
                }

                StringReader strTemp = new StringReader(drs);
                Model.NRequestForm model = new Model.NRequestForm();
                DataSet wsBarCode = new DataSet();

                wsBarCode.ReadXml(strTemp);
                DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = wsBarCode.Tables[0].Rows[0]["BarCode"].ToString() });

                DataRow dr = wsBarCode.Tables[0].Rows[0];
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                Model.NRequestItem nRequestItem = new Model.NRequestItem();

                if (wsBarCode.Tables[0].Columns.Contains("BarCode") && dr["BarCode"] != "")
                {
                    barCodeForm.BarCode = dr["BarCode"].ToString();
                    if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                    {
                        if (dsBarCodeForm.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString().Trim() != orgID)
                        {
                            strMsg = "条码号:'" + dr["BarCode"].ToString() + "'已存在！";
                            Log.Info("实验室："+ orgID +"，上传了和实验室："+ dsBarCodeForm.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString().Trim()+"相同的条码号："+ wsBarCode.Tables[0].Rows[0]["BarCode"].ToString());
                            return false;
                        }
                    }
                }
                else
                {
                    strMsg = "BarCode为空,不能上传申请单\r\n";
                    Log.Info(strMsg);
                    return false;
                }
                if (wsBarCode.Tables[0].Columns.Contains("SampleTypeNo") && dr["SampleTypeNo"] != "")
                {
                    barCodeForm.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                    model.SampleTypeNo = Convert.ToInt32(dr["SampleTypeNo"]);
                    nRequestItem.SampleTypeNo = model.SampleTypeNo;
                }
                if (wsBarCode.Tables[0].Columns.Contains("DoctorName") && dr["DoctorName"] != "")
                {
                    model.DoctorName = dr["DoctorName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("DeptName") && dr["DeptName"] != "")
                {
                    model.DeptName = dr["DeptName"].ToString();
                }

                if (wsBarCode.Tables[0].Columns.Contains("zdy2") && dr["zdy2"] != "")
                {
                    model.zdy2 = dr["zdy2"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy3") && dr["zdy3"] != "")
                {
                    model.zdy3 = dr["zdy3"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy4") && dr["zdy4"] != "")
                {
                    model.zdy4 = dr["zdy4"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy5") && dr["zdy5"] != "")
                {
                    model.zdy5 = dr["zdy5"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy6") && dr["zdy6"] != "")
                {
                    model.ZDY6 = dr["zdy6"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy7") && dr["zdy7"] != "")
                {
                    model.ZDY7 = dr["zdy7"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy8") && dr["zdy8"] != "")
                {
                    model.ZDY8 = dr["zdy8"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy9") && dr["zdy9"] != "")
                {
                    model.ZDY9 = dr["zdy9"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy10") && dr["zdy10"] != "")
                {
                    model.ZDY10 = dr["zdy10"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("CollectTime") && dr["CollectTime"] != "")
                {
                    barCodeForm.CollectTime = Convert.ToDateTime(dr["CollectTime"]);
                }
                if (wsBarCode.Tables[0].Columns.Contains("Collecter") && dr["Collecter"] != "")
                {
                    barCodeForm.Collecter = dr["Collecter"].ToString();
                }

                if (wsBarCode.Tables[0].Columns.Contains("SampleType") && dr["SampleType"] != "")
                {
                    barCodeForm.SampleType = dr["SampleType"].ToString();
                }
                barCodeForm.WebLisSourceOrgId = orgID;
                if (wsBarCode.Tables[0].Columns.Contains("ClientName") && dr["ClientName"] != "")
                {
                    barCodeForm.ClientName = dr["ClientName"].ToString();
                    model.ClientName = dr["ClientName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgName") && dr["WebLisSourceOrgName"] != "")
                {
                    barCodeForm.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                    model.WebLisSourceOrgName = dr["WebLisSourceOrgName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("TESTTYPENO") && dr["TESTTYPENO"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["TESTTYPENO"].ToString()))
                    {
                        model.TestTypeNo = Convert.ToInt32(dr["TESTTYPENO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("STATUSNO") && dr["STATUSNO"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["STATUSNO"].ToString()))
                    {
                        model.StatusNo = Convert.ToInt32(dr["STATUSNO"]);
                    }

                }
                if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNO") && dr["DISTRICTNO"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["DISTRICTNO"].ToString()))
                    {
                        model.DistrictNo = Convert.ToInt32(dr["DISTRICTNO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("DISTRICTNAME") && dr["DISTRICTNAME"] != "")
                {
                    model.DistrictName = dr["DISTRICTNAME"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("DeptNo") && dr["DeptNo"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["DeptNo"].ToString()))
                    {
                        model.DeptNo = Convert.ToInt32(dr["DeptNo"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("WebLisOrgID") && dr["WebLisOrgID"] != "")
                {
                    barCodeForm.WebLisOrgID = dr["WebLisOrgID"].ToString();
                    nRequestItem.WebLisOrgID = dr["WebLisOrgID"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("SerialNo") && dr["SerialNo"] != "")
                {
                    model.SerialNo = dr["SerialNo"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("Bed") && dr["Bed"] != "")
                {
                    model.Bed = dr["Bed"].ToString();
                }
                ZhiFang.Common.Log.Log.Info("判断是否存在OldSerialNo，录入OldBarCode。");
                if (wsBarCode.Tables[0].Columns.Contains("OldSerialNo") && dr["OldSerialNo"] != "")
                {
                    ZhiFang.Common.Log.Log.Info("存在OldSerialNo:" + dr["OldBarCode"].ToString());
                    model.OldSerialNo = dr["OldBarCode"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("zdy1") && dr["zdy1"] != "")
                {
                    model.zdy1 = dr["zdy1"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("ClientNo") && dr["ClientNo"] != "")
                {
                    barCodeForm.ClientNo = dr["ClientNo"].ToString();
                    model.ClientNo = dr["ClientNo"].ToString();
                    model.WebLisSourceOrgID = model.ClientNo;
                    nRequestItem.ClientNo = model.ClientNo;
                }
                else
                {
                    barCodeForm.ClientNo = orgID;
                    model.ClientNo = orgID;
                    model.WebLisSourceOrgID = model.ClientNo;
                    nRequestItem.ClientNo = model.ClientNo;
                }
                if (wsBarCode.Tables[0].Columns.Contains("CName") && dr["CName"] != "")
                {
                    model.CName = dr["CName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("Age") && dr["Age"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["Age"].ToString()))
                    {
                        model.Age = Convert.ToDecimal(dr["Age"].ToString());
                    }
                }

                if (wsBarCode.Tables[0].Columns.Contains("AgeUnitName") && dr["AgeUnitName"] != "")
                {
                    model.AgeUnitName = dr["AgeUnitName"].ToString();       
                }
                if (wsBarCode.Tables[0].Columns.Contains("ClinicTypeName") && dr["ClinicTypeName"] != "")
                {
                    model.ClinicTypeName = dr["ClinicTypeName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("TestTypeName") && dr["TestTypeName"] != "")  // 检验类型 如常/急/质
                {
                    model.TestTypeName = dr["TestTypeName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("GenderNo") && dr["GenderNo"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["GenderNo"].ToString()))
                    {
                        model.GenderNo = Convert.ToInt32(dr["GenderNo"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("GenderName") && dr["GenderName"] != "")
                {
                    model.GenderName = dr["GenderName"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WARDNAME") && dr["WARDNAME"] != "")
                {
                    model.WardName = dr["WARDNAME"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("WARDNO") && dr["WARDNO"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["WARDNO"].ToString()))
                    {
                        model.WardNo = Convert.ToInt32(dr["WARDNO"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Doctor") && dr["Doctor"] != "")
                {
                    model.DoctorName = dr["Doctor"].ToString();
                }

                if (wsBarCode.Tables[0].Columns.Contains("CollectDate") && dr["CollectDate"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["CollectDate"].ToString()))
                    {
                        model.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                        barCodeForm.CollectDate = Convert.ToDateTime(dr["CollectDate"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("OperDate") && dr["OperDate"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                    {
                        model.OperDate = Convert.ToDateTime(dr["OperDate"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("OperTime") && dr["OperTime"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["OperDate"].ToString()))
                    {
                        model.OperTime = Convert.ToDateTime(dr["OperTime"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("PatNo") && dr["PatNo"] != "")
                {
                    model.PatNo = dr["PatNo"].ToString();
                }
                if (wsBarCode.Tables[0].Columns.Contains("jztype") && dr["jztype"] != "")
                {
                    if (!string.IsNullOrEmpty(jzType))
                    {
                        model.jztype = Convert.ToInt32(jzType);
                    }
                    else
                    {
                        model.jztype = 0;
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Ageunitno") && dr["Ageunitno"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["Ageunitno"].ToString()))
                    {
                        model.AgeUnitNo = Convert.ToInt32(dr["Ageunitno"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("Collecttime") && dr["Collecttime"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["Collecttime"].ToString()))
                    {
                        model.CollectTime = Convert.ToDateTime(dr["Collecttime"]);
                    }
                }
                if (wsBarCode.Tables[0].Columns.Contains("TestAim") && dr["TestAim"] != "")//诊断类型名称
                {
                    model.TestAim = dr["TestAim"].ToString();
                }
                nRequestItem.SerialNo = model.SerialNo;
                nRequestItem.OldSerialNo = model.OldSerialNo;

                if (wsBarCode.Tables[0].Columns.Contains("ItemNo") && dr["ItemNo"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["ItemNo"].ToString()))
                    {
                        nRequestItem.ParItemNo = dr["ItemNo"].ToString();
                    }
                }
                nRequestItem.WebLisSourceOrgID = model.WebLisSourceOrgID;
                if (wsBarCode.Tables[0].Columns.Contains("ParItemNo") && dr["ParItemNo"] != "")
                {
                    if (!string.IsNullOrEmpty(dr["ParItemNo"].ToString()))
                    {
                        nRequestItem.CombiItemNo = dr["ParItemNo"].ToString().ToString();
                    }
                }

                int i = 0;
                if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                {
                    barCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                    i = ibbcf.Update(barCodeForm);
                    ZhiFang.Common.Log.Log.Info("更新barCodeForm成功！BarCodeFormNo：" + barCodeForm.BarCodeFormNo);
                }
                else
                {
                    //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                    //barCodeForm.BarCodeFormNo = DateTime.Now.Ticks;
                    barCodeForm.BarCodeFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                    i = ibbcf.Add(barCodeForm);
                    ZhiFang.Common.Log.Log.Info("插入barCodeForm成功！BarCodeFormNo：" + barCodeForm.BarCodeFormNo);
                }
                DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = model.SerialNo,ClientNo=model.ClientNo });
                if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                {
                    model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                    i = ibnrf.Update(model);
                    ZhiFang.Common.Log.Log.Info("更新NRequestForm成功！NRequestFormNo：" + model.NRequestFormNo);
                }
                else
                {
                    //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                    //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                    model.NRequestFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                    i = ibnrf.Add_PKI(model);
                    ZhiFang.Common.Log.Log.Info("插入NRequestForm成功！NRequestFormNo：" + model.NRequestFormNo);
                }
                nRequestItem.BarCodeFormNo = barCodeForm.BarCodeFormNo;
                nRequestItem.NRequestFormNo = model.NRequestFormNo;
                string strWhere = " BarCodeFormNo='" + nRequestItem.BarCodeFormNo + "' and ParItemNo=" + nRequestItem.ParItemNo;
                DataSet dsNRequestItem = ibnri.GetList_PKI(strWhere);
                if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                {
                    nRequestItem.NRequestFormNo = Convert.ToInt64(dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"]);
                    nRequestItem.NRequestItemNo = Convert.ToInt32(dsNRequestItem.Tables[0].Rows[0]["NRequestItemNo"]);
                    i = ibnri.Update(nRequestItem);
                    ZhiFang.Common.Log.Log.Info("更新NRequestItem成功！NRequestFormNo：" + nRequestItem.NRequestFormNo + ",NRequestItemNo:" + nRequestItem.NRequestItemNo);
                }
                else
                {

                    i = ibnri.Add(nRequestItem);
                    ZhiFang.Common.Log.Log.Info("插入NRequestItem成功！NRequestFormNo：" + nRequestItem.NRequestFormNo);
                }
                if (i > 0)
                {
                    strMsg += "申请单上传成功\r\n";
                    Log.Info(strMsg);
                    return true;
                }
                else
                {
                    strMsg += "申请单上传失败\r\n";
                    Log.Info(strMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                strMsg = "上传失败" + e.ToString();
                return false;
            }
        }
        #endregion

        //UpLoadRequestFormClient1测试版本
        public bool UpLoadRequestFormClient1(string drs, string orgID, string jzType, out string strMsg)
        {
            strMsg = "";
            string str = "";
            if (drs == null)
            {
                strMsg = "dr为空,不能上传申请单\r\n";
                Log.Info(strMsg);
            }
            string v1 = drs.ToUpper();
            Model.NRequestForm model = new Model.NRequestForm();
            Model.NRequestItem NRequestItem = new Model.NRequestItem();
            DataSet wsBarCode = new DataSet();
            StringReader strTemp = new StringReader(v1);
            wsBarCode.ReadXml(strTemp);
            DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = wsBarCode.Tables[0].Rows[0]["BARCODE"].ToString() });
            if (!ibbcf.CheckBarCodeLab(wsBarCode, orgID, out str))
            {
                strMsg += str;
                return false;
            }
            wsBarCode = MatchCenterNo(wsBarCode, orgID);
            DataRow dr = wsBarCode.Tables[0].Rows[0];
            Model.BarCodeForm BarCodeForm = new Model.BarCodeForm();
            int count = 0;
            //ArrayList list=new ArrayList();
            List<string> lisStrColumn = new List<string>();
            List<string> lisStrData = new List<string>();
            for (int i = 0; i < dsBarCodeForm.Tables[0].Columns.Count; i++)
            {
                for (int j = 0; j < wsBarCode.Tables[0].Columns.Count; j++)
                {
                    if (dsBarCodeForm.Tables[0].Columns[i].ColumnName.ToUpper() == wsBarCode.Tables[0].Columns[j].ColumnName)
                    {
                        lisStrColumn.Add(wsBarCode.Tables[0].Columns[j].ColumnName);
                        string ColumnData = "";
                        if (wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("DATE") > 0 || wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("TIME") > 0)
                        {
                            ColumnData = Convert.ToDateTime(wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString()).ToString();
                        }
                        else
                        {
                            ColumnData = wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString();
                        }
                        lisStrData.Add(ColumnData);
                    }
                }
            }
            if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
            {
                BarCodeForm.BarCodeFormNo = Convert.ToInt64(dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"]);
                lisStrColumn.Add("BarCodeFormNo");
                lisStrData.Add(BarCodeForm.BarCodeFormNo.ToString());
                count = ibbcf.UpdateByList(lisStrColumn, lisStrData);
            }
            else
            {
                //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(); ;//直接返回字符串类型
                BarCodeForm.BarCodeFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                lisStrColumn.Add("BarCodeFormNo");
                lisStrData.Add(BarCodeForm.BarCodeFormNo.ToString());
                count = ibbcf.AddByList(lisStrColumn, lisStrData);
            }
            List<string> lisStrColumnNf = new List<string>();
            List<string> lisStrDataNf = new List<string>();
            DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { SerialNo = wsBarCode.Tables[0].Rows[0]["SerialNo"].ToString() });
            for (int i = 0; i < dsNRequestForm.Tables[0].Columns.Count; i++)
            {
                for (int j = 0; j < wsBarCode.Tables[0].Columns.Count; j++)
                {
                    if (dsNRequestForm.Tables[0].Columns[i].ColumnName.ToUpper() == wsBarCode.Tables[0].Columns[j].ColumnName)
                    {
                        lisStrColumnNf.Add(wsBarCode.Tables[0].Columns[j].ColumnName);
                        string ColumnData = "";
                        if (wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("DATE") > 0 || wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("TIME") > 0 || wsBarCode.Tables[0].Columns[j].ColumnName == "BIRTHDAY")
                        {
                            ColumnData = Convert.ToDateTime(wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString()).ToString();
                        }
                        else
                        {
                            ColumnData = wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString();
                        }
                        lisStrDataNf.Add(ColumnData);
                    }
                }
            }
            if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
            {
                model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                lisStrColumnNf.Add("NRequestFormNo");
                lisStrDataNf.Add(model.NRequestFormNo.ToString());
                count = ibnrf.UpdateByList(lisStrColumnNf, lisStrDataNf);
            }
            else
            {
                //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                //string strGUID = Math.Abs(Guid.NewGuid().GetHashCode()).ToString();//直接返回字符串类型
                model.NRequestFormNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong(); //Convert.ToInt64(strGUID);
                lisStrColumnNf.Add("NRequestFormNo");
                lisStrDataNf.Add(model.NRequestFormNo.ToString());
                count = ibnrf.AddByList(lisStrColumnNf, lisStrDataNf);
            }
            List<string> lisStrColumnNi = new List<string>();
            List<string> lisStrDataNi = new List<string>();
            NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo;
            NRequestItem.NRequestFormNo = model.NRequestFormNo;
            //NRequestItem.ParItemNo = Convert.ToInt32(wsBarCode.Tables[0].Rows[0]["PARITEMNO"].ToString());
            NRequestItem.ParItemNo = wsBarCode.Tables[0].Rows[0]["PARITEMNO"].ToString();
            DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = NRequestItem.BarCodeFormNo, ParItemNo = NRequestItem.ParItemNo });
            for (int i = 0; i < dsNRequestItem.Tables[0].Columns.Count; i++)
            {
                for (int j = 0; j < wsBarCode.Tables[0].Columns.Count; j++)
                {
                    if (dsNRequestItem.Tables[0].Columns[i].ColumnName.ToUpper() == wsBarCode.Tables[0].Columns[j].ColumnName)
                    {
                        lisStrColumnNi.Add(wsBarCode.Tables[0].Columns[j].ColumnName);
                        string ColumnData = "";
                        if (wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("DATE") > 0 || wsBarCode.Tables[0].Columns[j].ColumnName.IndexOf("TIME") > 0)
                        {
                            ColumnData = Convert.ToDateTime(wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString()).ToString();
                        }
                        else
                        {
                            ColumnData = wsBarCode.Tables[0].Rows[0][wsBarCode.Tables[0].Columns[j].ColumnName].ToString();
                        }
                        lisStrDataNi.Add(ColumnData);
                    }
                }
            }
            if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
            {
                //model.NRequestFormNo = Convert.ToInt64(dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                //lisStrColumnNi.Add("NRequestFormNo");
                //lisStrDataNf.Add(model.NRequestFormNo.ToString());
                //count = ibnrf.UpdateByList(lisStrColumnNf, lisStrDataNf);
            }
            else
            {
                //System.Guid guid = System.Guid.NewGuid(); //Guid 类型string 
                lisStrColumnNi.Add("NRequestFormNo");
                lisStrDataNi.Add(model.NRequestFormNo.ToString());
                count = ibnri.AddByList(lisStrColumnNi, lisStrDataNi);
            }
            return true;
        }

        public bool DownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            string WebLiser,               //下载人的其他信息，下载人姓名，地点，时间等等扩展信息(本次先不开发)
            out XmlNode nodeBarCode,        //一个条码XML
            out XmlNode nodeNRequestItem,   //多少个项目
            out XmlNode nodeNRequestForm,   //多少个申请单
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            nodeBarCode = null;
            nodeNRequestForm = null;
            nodeNRequestItem = null;
            xmlWebLisOthers = null;
            ReturnDescription = "";
            ZhiFang.Common.Log.Log.Info(String.Format("下载申请开始DestiOrgID={0},BarCodeNo={1}", DestiOrgID, BarCodeNo));
            ////---------------------------------------------------------------------------------------------------------
            //-----
            if (DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "外送单位,与标本条码号不能为空";
                ZhiFang.Common.Log.Log.Error("外送单位,与标本条码号不能为空");
                return false;
            }
            DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = BarCodeNo });
            //+ "' and WebLisOrgID='" + DestiOrgID + "'");
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            //WebLisSourceOrgID=SourceOrgID
            //ZhiFang.Common.Log.Log.Info("执行查询语句:" + strsql);

            if (!(((dsBarCodeForm != null) && (dsBarCodeForm.Tables.Count > 0)) && (dsBarCodeForm.Tables[0].Rows.Count > 0)))
            {
                ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
                ZhiFang.Common.Log.Log.Error("未找到条码号为[" + BarCodeNo + "]的数据");
                return false;
            }
            try
            {

                string PreviouseWebLisFlag = "0";
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                {
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                }
                if (PreviouseWebLisFlag == "5" || PreviouseWebLisFlag == "3"
                    || Convert.ToInt32(PreviouseWebLisFlag) > 6)
                //|| PreviouseWebLisFlag == "7"
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]";
                    Log.Error("数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]");
                    return false;
                }

                //这里要讨论决定, barcodeFormNo,NRequestFormNo等重新生成唯一号，用于NRequestItem关联

                string strBarCodeFormNo = dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"].ToString();
                if (strBarCodeFormNo == "")
                {
                    ReturnDescription = "BarCodeFormNo为空";
                    Log.Error("BarCodeFormNo为空,程序退出");
                    return false;
                }

                XmlDocument docNRequestItem = new XmlDocument();   //项目信息
                //strsql = "select * from NRequestItem where BarCodeFormNo=" + strBarCodeFormNo;
                DataSet dsItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = long.Parse(strBarCodeFormNo) });

                if (!(((dsBarCodeForm != null) && (dsBarCodeForm.Tables.Count > 0)) && (dsBarCodeForm.Tables[0].Rows.Count > 0)))
                {
                    ReturnDescription = String.Format("未找到该条码所对应的项目，BarCodeFormNo:{0}", strBarCodeFormNo);
                    Log.Error(String.Format("未找到该条码所对应的项目，BarCodeFormNo:{0}", strBarCodeFormNo));
                    return false;
                }

                XmlDocument docNRequestForm = new XmlDocument();

                //strsql = "select * from NRequestForm where NRequestFormNo='" + dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString() + "'";
                DataSet dsForm = ibnrf.GetList(new Model.NRequestForm() { NRequestFormNo = long.Parse(dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString()) });

                docNRequestForm.LoadXml(dsForm.GetXml());
                nodeNRequestForm = docNRequestForm.DocumentElement;

            }
            catch (Exception ex)
            {
                ReturnDescription += "下载申请失败" + ex.Message;
                Log.Error("下载申请失败" + ex.Message);
                return false;
            }
            ////---------------------------------------------------------------------------------------------------------
            return true;
        }

        //public bool DownloadBarCodeAllx(
        //     string SourceOrgID,   //送检单位
        //     string receive,       //接受单位
        //     string start,         //起止日期
        //     string stop,          //截止日期
        //     string itemNo,        //项目编码
        //     out XmlNode applyXml,  //一个条码XML
        //     out string msg         //描述
        //     ) {
        //         msg = "";
        //         applyXml = null;
        //        return true;
        //}

        public bool DownloadBarCodeFlag(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            string WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            try
            {
                if (ibbcf.UpdateByBarCode(new Model.BarCodeForm() { WebLisFlag = 5, BarCode = BarCodeNo, WebLisOrgID = DestiOrgID }) > 0)
                {
                    ReturnDescription = "打签收标志成功！";
                    Log.Info("打签收标志成功！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return true;
                }
                else
                {
                    ReturnDescription = "打签收标志失败！";
                    Log.Error("打签收标志失败！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return false;
                }
            }
            catch (Exception e)
            {
                ReturnDescription = "打签收标志失败" + e.Message;
                Log.Error("打签收标志败" + e.Message);
                return false;
            }
        }

        public bool RefuseDownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            string WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            try
            {
                if (ibbcf.UpdateByBarCode(new Model.BarCodeForm() { WebLisFlag = 6, BarCode = BarCodeNo, WebLisOrgID = DestiOrgID }) > 0)
                {
                    ReturnDescription = "退回成功！";
                    Log.Info("退回成功！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return true;
                }
                else
                {
                    ReturnDescription = "退回失败！";
                    Log.Error("退回失败！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return false;
                }
            }
            catch (Exception e)
            {
                ReturnDescription = "退回失败" + e.Message;
                Log.Error("退回失败" + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 申请单上传（将上传的申请单保存到数据库中）
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        public int UpLoadRequestFromBytes(string token, byte[] xmlData, out string errorMsg)
        {
            errorMsg = "";
            bool validate = ZhiFang.Tools.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }

            errorMsg = "";
            return ibrd.UpLoadRequestFromBytes(xmlData, out errorMsg);
        }


        public bool UpgradeRequestForm(
           string SourceOrgID,             //送检(源)单位
           string DestiOrgID,              //外送(至)单位
           string BarCodeNo,               //条码码
           string nodeBarCodeForm,
           string nodeNRequestForm,
           string nodeNRequestItem,
           string nodeOthers,
           out string WebLisFlag,
           out string ReturnDescription)
        {
            Log.Info(String.Format("上传申请开始SourceOrgID={0},DestiOrgID={1},BarCodeNo={2}", SourceOrgID, DestiOrgID, BarCodeNo));
            WebLisFlag = "0";
            ReturnDescription = "";
            #region 判断部分
            if (SourceOrgID == null
                || SourceOrgID == ""
                || DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "送检单位,外送单位,与标本条码号不能为空";
                return false;
            }
            Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
            barCodeForm.BarCode = BarCodeNo;
            barCodeForm.WebLisSourceOrgId = SourceOrgID;
            DataSet dsBarCodeForm = ibbcf.GetList(barCodeForm);
            //DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 * from barcodeForm where BarCode='"
            //    + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
            {
                ReturnDescription = "barcodeForm出错，请检查";
                return false;
            }
            //判断是新增加条码，还是再次上传更新数据
            bool bAddNew = false;
            if (dsBarCodeForm.Tables[0].Rows.Count == 0)
                bAddNew = true;
            else
            {
                string PreviouseWebLisFlag = "0";
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                if (PreviouseWebLisFlag == "5"
                    || Convert.ToInt32(PreviouseWebLisFlag) > 6)
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能再次上传，目前状态为[" + PreviouseWebLisFlag + "]";
                    WebLisFlag = PreviouseWebLisFlag;
                    return false;
                }
            }
            #endregion
            #region 读取数据集部分
            string barcodeFormNo = ibbcf.GetNewBarCodeFormNo(Convert.ToInt32(SourceOrgID));
            StringReader strTemp = new StringReader(nodeBarCodeForm);
            DataSet wsBarCode = new DataSet();
            wsBarCode.ReadXml(strTemp);
            strTemp = new StringReader(nodeNRequestItem);
            DataSet wsNRequestItem = new DataSet();
            wsNRequestItem.ReadXml(strTemp);
            if (wsNRequestItem == null && wsNRequestItem.Tables.Count == 0)
            {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到项目数据{0}条", wsNRequestItem.Tables[0].Rows.Count));
            }
            strTemp = new StringReader(nodeNRequestForm);
            DataSet wsNRequestForm = new DataSet();
            wsNRequestForm.ReadXml(strTemp);
            if (wsNRequestForm == null && wsNRequestForm.Tables.Count == 0)
            {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到申请单数据{0}条", wsNRequestForm.Tables[0].Rows.Count));
            }
            string str = "";
            if (!ibbcf.CheckBarCodeLab(wsBarCode, DestiOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            if (!ibnrf.CheckNReportFormLab(wsNRequestForm, DestiOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            if (!ibnri.CheckNReportItemLab(wsNRequestItem, DestiOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            wsBarCode = MatchCenterNo(wsBarCode, SourceOrgID);
            wsNRequestForm = MatchCenterNo(wsNRequestForm, SourceOrgID);
            wsNRequestItem = MatchCenterNo(wsNRequestItem, SourceOrgID);
            #endregion
            try
            {
                ibrd.UpdateBarCode(barcodeFormNo, wsBarCode, SourceOrgID, DestiOrgID);
                ibrd.UpdateNRequestForm(wsNRequestForm, SourceOrgID, DestiOrgID);
                ibrd.UpdateNRequestItem(barcodeFormNo, wsNRequestItem, wsNRequestForm, SourceOrgID, DestiOrgID);
                int result = ibrd.SaveWebLisData(BarCodeNo, wsBarCode, wsNRequestItem, wsNRequestForm);
            }
            catch (Exception ex)
            {
                ReturnDescription += ex.Message;
                Log.Error(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据实验室的编码得到中心的码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchCenterNo(DataSet ds, string SourceOrgID)
        {
            if (ConfigHelper.GetConfigString("TransCodField") != "" && ConfigHelper.GetConfigString("TransCodField") != null)
            {
                string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
                foreach (string str in strArray)
                {
                    List<string> ListStr = new List<string>();
                    List<string> ListStrName = new List<string>();
                    string B_Lab_tableName = "";
                    string B_Lab_controlTableName = "";
                    switch (str)
                    {
                        case "SAMPLETYPENO":
                            B_Lab_tableName = "B_Lab_SampleType";
                            B_Lab_controlTableName = "B_SampleTypeControl";
                            break;
                        case "GENDERNO":
                            B_Lab_tableName = "b_lab_GenderType";
                            B_Lab_controlTableName = "B_GenderTypeControl";
                            break;
                        case "FOLKNO":
                            B_Lab_tableName = "B_Lab_FolkType";
                            B_Lab_controlTableName = "B_FolkTypeControl";
                            break;
                        case "ITEMNO":
                            B_Lab_tableName = "B_Lab_TestItem";
                            B_Lab_controlTableName = "B_TestItemControl";
                            break;
                        case "SUPERGROUPNO":
                            B_Lab_tableName = "B_Lab_SuperGroup";
                            B_Lab_controlTableName = "B_SuperGroupControl";
                            break;
                        default: continue;
                    }

                    if (str.ToUpper() == "ITEMNO")
                    {
                        if (ds.Tables[0].Columns.Contains("ItemNo"))
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                if (ds.Tables[0].Rows[count]["ItemNo"].ToString() != "")
                                {
                                    ListStr.Add(ds.Tables[0].Rows[count]["ItemNo"].ToString());
                                }
                            }
                        }
                        if (ds.Tables[0].Columns.Contains("ParItemNo"))
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                if (ds.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                                {
                                    ListStr.Add(ds.Tables[0].Rows[count]["ParItemNo"].ToString());
                                }
                            }
                        }
                        if (ds.Tables[0].Columns.Contains("CombiItemNo"))
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                if (ds.Tables[0].Rows[count]["CombiItemNo"].ToString() != "")
                                {
                                    ListStr.Add(ds.Tables[0].Rows[count]["CombiItemNo"].ToString());
                                }
                            }
                        }
                        if (ListStr.Count != 0)
                        {
                            DataSet CenteNo = ibr.GetCentNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                foreach (DataRow dritem in CenteNo.Tables[0].Rows)
                                {
                                    if (ds.Tables[0].Columns.Contains("ItemNo"))
                                    {
                                        //ZhiFang.Common.Log.Log.Info("drItemNo：" + dr["ItemNo"].ToString().ToLower() + "dritemControlITEMNO:" + dritem["ControlITEMNO"].ToString().ToLower());
                                        if (dr["ItemNo"].ToString().ToLower().Trim() == dritem["ControlITEMNO"].ToString().ToLower().Trim() || dr["ItemNo"].ToString() == "")
                                        {
                                            dr["ItemNo"] = dritem["ITEMNO"].ToString().Trim();
                                            //ZhiFang.Common.Log.Log.Info("ItemNo对照："+dr["ItemNo"]+"找到对照项目"+ dritem["ITEMNO"].ToString());
                                        }
                                        //ZhiFang.Common.Log.Log.Info("ItemNo对照：" + dr["ItemNo"] + "对照结束");
                                    }
                                    if (ds.Tables[0].Columns.Contains("ParItemNo"))
                                    {
                                        //ZhiFang.Common.Log.Log.Info("drParItemNo：" + dr["ParItemNo"].ToString().ToLower() + "dritemControlITEMNO:" + dritem["ControlITEMNO"].ToString().ToLower());
                                        if (dr["ParItemNo"].ToString().ToLower().Trim() == dritem["ControlITEMNO"].ToString().ToLower().Trim() || dr["ParItemNo"].ToString() == "")
                                        {
                                            dr["ParItemNo"] = dritem["ITEMNO"].ToString().Trim();
                                            //ZhiFang.Common.Log.Log.Info("ParItemNo对照：" + dr["ParItemNo"] + "找到对照项目" + dritem["ITEMNO"].ToString());
                                        }
                                        //ZhiFang.Common.Log.Log.Info("ParItemNo对照：" + dr["ParItemNo"] + "对照结束");
                                    }
                                    if (ds.Tables[0].Columns.Contains("CombiItemNo"))
                                    {
                                        //ZhiFang.Common.Log.Log.Info("drCombiItemNo：" + dr["CombiItemNo"].ToString().ToLower() + "dritemControlITEMNO:" + dritem["ControlITEMNO"].ToString().ToLower());
                                        if (dr["CombiItemNo"].ToString().ToLower().Trim() == dritem["ControlITEMNO"].ToString().ToLower().Trim() || dr["CombiItemNo"].ToString() == "")
                                        {
                                            dr["CombiItemNo"] = dritem["ITEMNO"].ToString().Trim();
                                            //ZhiFang.Common.Log.Log.Info("CombiItemNo对照：" + dr["CombiItemNo"] + "找到对照项目" + dritem["ITEMNO"].ToString());
                                        }
                                        //ZhiFang.Common.Log.Log.Info("CombiItemNo对照：" + dr["CombiItemNo"] + "对照结束");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Columns.Contains(str))
                        {
                            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                            {
                                if (ds.Tables[0].Rows[count][str].ToString() != "")
                                {
                                    ListStr.Add(ds.Tables[0].Rows[count][str].ToString());
                                }
                                else
                                {
                                    string str1 = "";
                                    if (str.IndexOf('N') > -1)
                                    {
                                        str1 = str.Substring(0, str.Length - 2);
                                    }
                                    if (ds.Tables[0].Columns.Contains(str1 + "Name"))
                                    {
                                        if (ds.Tables[0].Rows[count][str1 + "Name"].ToString() != "")
                                        {
                                            ListStrName.Add(ds.Tables[0].Rows[count][str1 + "Name"].ToString());
                                            if (ListStrName.Count > 0)
                                            {
                                                DataSet dsLabNo = ibr.GetLabNo(B_Lab_tableName, ListStrName, SourceOrgID, str);
                                                for (int j = 0; j < dsLabNo.Tables[0].Rows.Count; j++)
                                                {
                                                    if (B_Lab_tableName != "ITEMNO")
                                                    {
                                                        ListStr.Add(dsLabNo.Tables[0].Rows[j]["lab" + str].ToString());
                                                    }
                                                    else
                                                        ListStr.Add(dsLabNo.Tables[0].Rows[j][str].ToString());
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            if (ListStr.Count != 0)
                            {
                                DataSet CenteNo = ibr.GetCentNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    foreach (DataRow dritem in CenteNo.Tables[0].Rows)
                                    {
                                        if (dr[str].ToString().ToLower() == dritem["Control" + str].ToString().ToLower() || dr[str].ToString() == "")
                                        {
                                            dr[str] = dritem[str].ToString();

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ds;

        }
        /// <summary>
        /// 根据中心端的编码得到实验室的编码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchClientNo(DataSet ds, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_controlTableName = "";
                switch (str)
                {
                    case "SAMPLETYPENO":
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO":
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                if (ds.Tables[0].Columns.Contains(str))
                {
                    for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                    {
                        if (ds.Tables[0].Rows[count][str].ToString() != "")
                        {
                            ListStr.Add(ds.Tables[0].Rows[count][str].ToString());
                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet labNo = ibr.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in labNo.Tables[0].Rows)
                            {
                                if (dr[str].ToString() == dritem[str].ToString() || dr[str].ToString() == "")
                                {
                                    dr[str] = dritem[str].ToString();
                                }
                            }
                        }
                    }
                }
            }
            return ds;
        }

        #region 佛山申请单下载
        //申请单查询

        /// <summary>
        /// 佛山申请单下载
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="weblisflag">是否下载过0、1</param>
        /// <param name="ClientNo">外送单位</param>
        /// <param name="StartDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <param name="nodeAppliy">申请单xml格式</param>
        /// <param name="Msg">返回新</param>
        /// <returns>状态</returns>
        public bool QueryAppliy(int count, string weblisflag, string ClientNo, string StartDate, string EndDate, out string nodeAppliy, out string Msg)
        {
            DataSet requestForm = new DataSet();
            string strCon = string.Empty;
            //下载命令 weblisflag:0下载   1不下载
            if (Convert.ToInt32(weblisflag) == 0)
            {
                strCon = "nf.weblisflag < 5";
            }
            else if (Convert.ToInt32(weblisflag) == 1)
            {
                strCon = "nf.weblisflag >= 5";
            }

            if (count > -1 && count > 0)//按条数查询数据
            {
                //返回数据
                requestForm = ibv.GetViewData(count, "_NRequestFormFullDataSource", " nf.ClientNo='" + ClientNo + "' and nf.CollectDate between'" + StartDate + "' and '" + EndDate + "' and " + strCon + "", "");
            }
            else//全部
            {
                //返回数据
                requestForm = ibv.GetViewData(-1, "_NRequestFormFullDataSource", " nf.ClientNo='" + ClientNo + "' and nf.CollectDate between'" + StartDate + "' and '" + EndDate + "' and " + strCon + "", "");
            }

            DataTable dtData = requestForm.Tables[0];
            if (dtData != null && dtData.Rows.Count > 0)
            {
                nodeAppliy = dtData.DataSet.GetXml();
                Msg = "生成XML格式数据成功";
                return true;
            }
            else
            {
                nodeAppliy = "";
                Msg = "生成XML格式数据失败";
                return false;
            }
        }
        #endregion

        #region 返回指定申请单项目信息,一个申请单一个条码号
        /// <summary>
        /// 返回指定申请单项目信息,一个申请单一个条码号
        /// </summary>
        /// <param name="strSerialNo"></param>
        /// <param name="nodeAppliy"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool DownLoadXML(string strSerialNo, out string nodeAppliy, out string Msg)
        {
            DataSet ds = ibv.GetViewData(-1, "_NRequestFormFullDataSource", " nf.SerialNo = '" + strSerialNo + "'", "");

            DataTable dTab = ds.Tables[0];
            if (dTab != null && dTab.Rows.Count > 0)
            {
                if(dTab.Rows[0]["WeblisFlag"]!=null && int.Parse(dTab.Rows[0]["WeblisFlag"].ToString().Trim())>=5)
                {
                    nodeAppliy="";
                     Msg = "样本已经被签收！不能重复签收";
                }
                nodeAppliy = dTab.DataSet.GetXml();
                Msg = "生成XML格式数据成功";
                return true;
            }
            else
            {
                nodeAppliy = "";
                Msg = "生成XML格式数据失败";
                return false;
            }
        }
        #endregion

        #region 标记指定申请单项目、条码
        /// <summary>
        /// 标记指定申请单项目、条码
        /// </summary>
        /// <param name="strSerialNo"></param>
        /// <param name="isMark"></param>
        /// <param name="nodeAppliy"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool MarkWeblisFlag(string strSerialNo, bool isMark, out string Msg)
        {
            bool nfBool = false;
            bool barBool = false;
            if (isMark == true)
            {
                //查询指定申请单
                Model.NRequestForm nf = ibnrf.GetModelBySerialNo(strSerialNo);
                nf.Weblisflag = "5";//
                //更新指定申请单标记
                int nfSess = ibnrf.Update(nf);
                if (nfSess > 0)
                {
                    Msg = "指定申请单weblistflag更新成功";
                    nfBool = true;
                }
                else
                {
                    Msg = "指定申请单weblistflag更新失败";
                    return nfBool = false;
                }
                //根据申请单查询项目
                DataSet ds = ibnri.GetList_By_NRequestFormNo(long.Parse(nf.NRequestFormNo.ToString()));
                //更新项目标记
                string strCodeNo = string.Empty;//记录条码
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (nfBool)
                    {
                        strCodeNo = ds.Tables[0].Rows[i]["BarCodeFormNo"].ToString();
                        //根据项目条码号更新条码
                        Model.BarCodeForm bf = ibbcf.GetModel(long.Parse(strCodeNo));
                        bf.WebLisFlag = 5;
                        int barSess = ibbcf.UpdateByBarCode(bf);//更新条码表
                        if (barSess > 0)
                        {
                            Msg = "条码表weblistflag更新成功";
                            barBool = true;
                        }
                        else
                        {
                            Msg = "条码表weblistflag更新失败";
                            return barBool = false;
                        }
                    }
                    else
                    {
                        return barBool = false;
                    }
                }
            }

            Msg = "";
            return barBool;
        }
        #endregion

        #region 拒收
        // 拒收申请服务
        public bool NRequestFormRefuse(string ClientNo, string BarCode, string refuseUser, string refuseTime, string refusereason, out string ErrorInfo)
        {
            bool result = false;
            try
            {
                ErrorInfo = "";
                DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = BarCode });
                if (dsBarCodeForm != null && dsBarCodeForm.Tables[0].Rows.Count > 0)
                {
                    string strSql = "insert into barcodeform_refuse select * from BarCodeForm where BarCodeFormNo =" + dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"];
                    strSql += " update barcodeform_refuse set refuseUser='" + refuseUser + "',refuseTime='" + refuseTime + "',refusereason='" + refusereason + "' where BarCodeFormNo =" + dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"];
                    int i = ibbcf.Add(strSql);
                    if (i > 0)
                    {
                        DataSet dsNRequestItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = (long)dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"] });
                        if (dsNRequestItem != null && dsNRequestItem.Tables[0].Rows.Count > 0)
                        {
                            string itemSql = "insert into NRequestItem_refuse select * from NRequestItem where BarCodeFormNo =" + dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"];
                            int j = ibnri.Add(itemSql);
                            if (j > 0)
                            {
                                DataSet dsNRequestForm = ibnrf.GetList(new Model.NRequestForm() { ClientNo = ClientNo, NRequestFormNo = (long)dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"] });
                                if (dsNRequestForm != null && dsNRequestForm.Tables[0].Rows.Count > 0)
                                {
                                    string formSql = "insert into NRequestForm_refuse select * from NRequestForm where NRequestFormNo =" + dsNRequestItem.Tables[0].Rows[0]["NRequestFormNo"];
                                    int k = ibnrf.Add(formSql);
                                    if (k > 0)
                                    {
                                        ibnrf.Delete((long)dsNRequestForm.Tables[0].Rows[0]["NRequestFormNo"]);
                                        result = true;
                                    }
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info(ex.Message.ToString());
                ErrorInfo = ex.Message.ToString();
            }
            return result;

        }

        //查询被拒收的申请单
        public bool NRequestFormRefuseQuery(string ClientNo, string LabUploadDateStart, string LabUploadDateEnd, out string NRequestFormXML, out string ErrorInfo)
        {
            bool result = false;
            NRequestFormXML = "";
            ErrorInfo = "";
            try
            {
                string strWhere = "select * from NRequestForm_Refuse where ClientNo=" + ClientNo + " and LABUPLOADDATE>='" + LabUploadDateStart + "' and LABUPLOADDATE<'" + LabUploadDateEnd + "'";
                ZhiFang.Common.Log.Log.Info("查询NRequestForm_Refuse：" + strWhere);
                DataSet dsNRequestFormRefuse = ibnrf.GetRefuseList(strWhere);
                if (dsNRequestFormRefuse != null && dsNRequestFormRefuse.Tables[0].Rows.Count > 0)
                {
                    NRequestFormXML = dsNRequestFormRefuse.GetXml();
                }
                result = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info(ex.Message.ToString());
                ErrorInfo = ex.Message.ToString();

            }
            return result;
        }

        //根据申请单号查询条码详细信息
        public bool QueryBarcodeFormByNRequestFormNo(string ClientNo, int NRequestFormNo, out string BarcodeFormXML, out string ErrorInfo)
        {
            bool result = false;
            BarcodeFormXML = "";
            ErrorInfo = "";
            try
            {
                string strSql = "select * from NRequestItem_Refuse where NRequestFormNo=" + NRequestFormNo;
                ZhiFang.Common.Log.Log.Info("查询NRequestItem_Refuse：" + strSql);
                DataSet dsNRequestItemRefuse = ibnri.GetRefuseList(strSql);
                if (dsNRequestItemRefuse != null && dsNRequestItemRefuse.Tables[0].Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Info("NRequestItem_Refuse项目条数：" + dsNRequestItemRefuse.Tables[0].Rows.Count);
                    string Sql = "select * from BarCodeForm_Refuse where ClientNo='" + ClientNo + "' and BarCodeFormNo=" + dsNRequestItemRefuse.Tables[0].Rows[0]["BarCodeFormNo"];
                    ZhiFang.Common.Log.Log.Info("查询BarCodeForm_Refuse：" + Sql);
                    DataSet dsBarCodeFormRefuse = ibbcf.GetRefuseList(Sql);
                    if (dsBarCodeFormRefuse != null && dsBarCodeFormRefuse.Tables[0].Rows.Count > 0)
                    {
                        BarcodeFormXML = dsBarCodeFormRefuse.GetXml();
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info(ex.Message.ToString());
                ErrorInfo = ex.Message.ToString();
            }
            return result;
        }
        #endregion
    }
}

