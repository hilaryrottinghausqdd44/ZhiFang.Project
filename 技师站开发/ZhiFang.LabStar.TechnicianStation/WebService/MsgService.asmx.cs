using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using ZhiFang.Entity.LIIP;
using ZhiFang.LabStar.DAO.ADO;

namespace ZhiFang.LabStar.TechnicianStation.WebService
{
    /// <summary>
    /// MsgService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MsgService : System.Web.Services.WebService
    {
        IApplicationContext context = ContextRegistry.GetContext();


        private int TimeSpan = 5;
        /// <summary>
        /// 客户端请求
        /// </summary>
        /// <param name="requestmsg">请求xml消息</param>
        /// <returns></returns>
        [WebMethod]
        public string ClientRequest(string requestmsg)
        {
            string strResult = "0";
            ZhiFang.LabStar.Common.LogHelp.Info("ClientRequest服务请求参数：" + requestmsg);
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl");
            if (string.IsNullOrEmpty(url))
            {
                ZhiFang.LabStar.Common.LogHelp.Error("服务地址不能为空");
                return "-1";
            }
            else
                url += "/ServerWCF/IMService.svc/ST_UDTO_AddSCMsg";
            if (CheckSCMsg(requestmsg) == "-1")
                return "-1";
            TimeSpan = GetMsgJudgeTimeSpan();
            SCMsg scMsg = DisposeSCMsg(requestmsg);
            try
            {
                if (scMsg != null)
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;//日期序列化为“/Data()/”格式，否则无法反序列化
                    string StrJson = Newtonsoft.Json.JsonConvert.SerializeObject(scMsg, Newtonsoft.Json.Formatting.None, settings);
                    string para = "{\"entity\":" + StrJson + "}";
                    strResult = ZhiFang.LabStar.Common.HTTPRequest.WebRequestHttpPost(url, para, "application/json");
                }
                else
                    strResult = "-1";
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("新增【SCMsg】信息出错：" + ex.Message);
                strResult = "-1";
            }
            return strResult;
        }

        private string CheckSCMsg(string msg)
        {
            try
            {
                StringReader StrStream = new StringReader(msg);
                XDocument xmlDoc = XDocument.Load(StrStream);
                XElement xeRoot = xmlDoc.Root;//根目录
                XElement xeMsgBigType = xeRoot.Element("MSGBIGTYPE");
                XElement xeMsgSmallType = xeRoot.Element("MSGSMALLTYPE");
                if (xeMsgBigType == null || GetXMLNodeValue(xeMsgBigType) != "EMGENCY")
                {
                    ZhiFang.LabStar.Common.LogHelp.Error("MSGBIGTYPE小节信息不符");
                    return "-1";

                }
                if (xeMsgSmallType == null || GetXMLNodeValue(xeMsgSmallType) != "1001")
                {
                    ZhiFang.LabStar.Common.LogHelp.Error("MSGSMALLTYPE小节信息不符");
                    return "-1";

                }
                XElement xeBody = xeRoot.Element("MSGCONTENT").Element("MSGKEY");
                return "0";
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("检查消息信息出错：" + ex.Message);
                return "-1";
            }
        }

        private SCMsg DisposeSCMsg(string msg)
        {
            SCMsg scMsg = new SCMsg();
            try
            {
                scMsg.IsUse = true;
                ///scMsg.MsgTypeID = 1;
                scMsg.MsgTypeCode = "ZF_LAB_START_CV";
                scMsg.MsgContent = XMLStrToJson(msg);
                ///scMsg.SystemID = 1001;
                ///scMsg.SystemCName = "智方_检验之星";
                ///scMsg.SystemCode = "ZF_LAB_START";
                scMsg.DataUpdateTime = DateTime.Now;
                //scMsg.RequireConfirmTime = DateTime.Now.AddMinutes(TimeSpan); 此属性已由集成平台服务赋值
                scMsg.SendIPAddress = ZhiFang.LabStar.Common.IPHelper.GetClientIP();
                StringReader StrStream = new StringReader(msg);
                XDocument xmlDoc = XDocument.Load(StrStream);
                XElement xeRoot = xmlDoc.Root;//根目录
                XElement xeBody = xeRoot.Element("MSGCONTENT").Element("MSGKEY");
                XElement xeBody1 = xeRoot.Element("MSGCONTENT").Element("MSGBODY");
                string fieldValue = "";
                //发送者信息
                scMsg.SenderName = GetXMLNodeValue(xeBody.Element("CHECKER"));
                ///fieldValue = GetFieldValueByName("PUser", "UserNo", "CName", scMsg.SenderName);
                ///scMsg.SenderID = string.IsNullOrEmpty(fieldValue) ? 0 : long.Parse(fieldValue);
                scMsg.SenderAccount = GetFieldValueByName("PUser", "ShortCode", "CName", scMsg.SenderName);
                //发送小组信息
                scMsg.SendSectionName = GetXMLNodeValue(xeBody.Element("SECTIONNAME"));
                fieldValue = GetXMLNodeValue(xeBody.Element("SECTIONNO"));
                if (!string.IsNullOrEmpty(fieldValue))
                    scMsg.SendSectionID = long.Parse(fieldValue);
                if (string.IsNullOrEmpty(scMsg.SendSectionName))
                    scMsg.SendSectionName = GetFieldValueByName("PGroup", "CName", "SectionNo", fieldValue);
                //接收就诊类型信息
                scMsg.RecSickTypeName = GetXMLNodeValue(xeBody.Element("SICKTYPENAME"));//就诊类型
                fieldValue = GetXMLNodeValue(xeBody.Element("SICKTYPENO"));
                if (!string.IsNullOrEmpty(fieldValue))
                    scMsg.RecSickTypeID = long.Parse(fieldValue);//就诊类型ID
                //接收科室信息                                                 
                scMsg.RecDeptName = GetXMLNodeValue(xeBody.Element("DEPTNAME"));//接收科室信息
                fieldValue = GetFieldValueByName("Department", "DeptNo", "CName", scMsg.RecDeptName);
                ///scMsg.RecDeptID = string.IsNullOrEmpty(fieldValue) ? 0 : long.Parse(fieldValue);
                scMsg.RecDeptCode = GetFieldValueByName("Department", "ShortCode", "CName", scMsg.RecDeptName);
                scMsg.RecDeptCodeHIS = GetFieldValueByName("Department", "HisOrderCode", "CName", scMsg.RecDeptName);
                scMsg.RecDeptPhoneCode = "";
                //接收病区信息
                scMsg.RecDistrictName = GetXMLNodeValue(xeBody.Element("DISTRICTNAME"));
                fieldValue = GetFieldValueByName("District", "DistrictNo", "CName", scMsg.RecDistrictName);
                scMsg.RecDistrictID = string.IsNullOrEmpty(fieldValue) ? 0 : long.Parse(fieldValue);

                //接收医生信息
                scMsg.RecDoctorName = GetXMLNodeValue(xeBody.Element("DOCTOR"));
                fieldValue = GetFieldValueByName("Doctor", "DoctorNo", "CName", scMsg.RecDoctorName);
                ///scMsg.RecDoctorID = string.IsNullOrEmpty(fieldValue) ? 0 : long.Parse(fieldValue);
                scMsg.RecDoctorCode = GetFieldValueByName("Doctor", "ShortCode", "CName", scMsg.RecDoctorName);
                scMsg.RecDoctorCodeHIS = GetFieldValueByName("Doctor", "HisOrderCode", "CName", scMsg.RecDoctorName);
                //确认科室信息默认是接收科室
                scMsg.ConfirmDeptName = scMsg.RecDeptName;
                scMsg.ConfirmDeptCode = scMsg.RecDeptCode;
                scMsg.ConfirmDeptCodeHIS = scMsg.RecDeptCodeHIS;

                //就诊类型为门诊时，根据设置的时间段判断接收科室
                if (scMsg.RecSickTypeName != null && scMsg.RecSickTypeName.IndexOf("门诊") >= 0)
                    scMsg = GetConfirmDeptInfo(scMsg);
                else if (scMsg.RecSickTypeName != null && scMsg.RecDistrictName != null && scMsg.RecSickTypeName.IndexOf("住院") >= 0 && scMsg.RecDistrictName.Trim().Length > 0)
                {
                    scMsg.ConfirmDeptID = scMsg.RecDistrictID;
                    scMsg.ConfirmDeptName = scMsg.RecDistrictName;
                    scMsg.ConfirmDeptCode = GetFieldValueByName("District", "ShortCode", "CName", scMsg.RecDistrictName);
                    scMsg.ConfirmDeptCodeHIS = GetFieldValueByName("District", "HisOrderCode", "CName", scMsg.RecDistrictName);
                }
                string testItemNoList = "";
                string testItemNameList = "";
                GetItemInfoByJson(scMsg.MsgContent, ref testItemNoList, ref testItemNameList);
                scMsg.TestItemNoList = testItemNoList;
                scMsg.TestItemNameList = testItemNameList;
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("生成【SCMsg】信息出错：" + ex.Message);
                scMsg = null;
            }
            return scMsg;
        }

        public void GetItemInfoByJson(string jsonData, ref string itemNoList, ref string itemNameList)
        {
            JObject jsonObject = JObject.Parse(jsonData);
            try
            {
                if (jsonObject.Property("MSG") != null && ((JObject)jsonObject["MSG"]).Property("MSGCONTENT") != null && ((JObject)jsonObject["MSG"]["MSGCONTENT"]).Property("MSGBODY") != null
                    && ((JObject)jsonObject["MSG"]["MSGCONTENT"]["MSGBODY"]).Property("MSG") != null)
                {
                    JArray jsonArray = (JArray)jsonObject["MSG"]["MSGCONTENT"]["MSGBODY"]["MSG"];
                    if (jsonArray != null && jsonArray.Count > 0)
                    {
                        foreach (JObject jo in jsonArray)
                        {
                            if (string.IsNullOrWhiteSpace(itemNoList))
                            {
                                itemNoList = jo["ITEMNO"].ToString();
                                itemNameList = jo["TESTITEMNAME"].ToString();
                            }
                            else
                            {
                                itemNoList += "," + jo["ITEMNO"].ToString();
                                itemNameList += "," + jo["TESTITEMNAME"].ToString();
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(itemNoList))
                            itemNoList = "," + itemNoList + ",";
                        if (!string.IsNullOrWhiteSpace(itemNameList))
                            itemNameList = "," + itemNameList + ",";
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("生成【SCMsg】时，获取TestItemNoList，TestItemNameList信息出错：" + ex.Message);
            }
        }

        private SCMsg GetConfirmDeptInfo(SCMsg scMsg)
        {
            if (scMsg.RecDeptName != null && scMsg.RecDeptName.IndexOf("急诊") > 0)
                return scMsg;
            ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon = (ZhiFang.IBLL.LabStar.IBLisCommon)context.GetObject("BLisCommon");
            if (IBLisCommon == null)
                return scMsg;

            DataSet ds = IBLisCommon.QuerySQL("select name from sysobjects where name=\'CV_SendDeptTimeConfig\'");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds = IBLisCommon.QuerySQL("select * from CV_SendDeptTimeConfig order by DispOrder");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string sendDeptTimeConfig = dr["SendDeptTimeConfig"].ToString();
                        if (sendDeptTimeConfig != null && sendDeptTimeConfig.Trim().Length > 0)
                        {
                            string[] array = sendDeptTimeConfig.Split('|');
                            DateTime curTime = DateTime.Parse(DateTime.Now.ToString("HH:mm:ss"));
                            //TimeSpan curTime = DateTime.Now.TimeOfDay;
                            bool isTimeFlag = false;
                            foreach (string strTime in array)
                            {
                                string[] tempArray = strTime.Split('-');
                                if (tempArray.Length == 2)
                                {
                                    DateTime beginTime = DateTime.Parse(tempArray[0]);
                                    DateTime endTime = DateTime.Parse(tempArray[1]);
                                    if (beginTime <= curTime && curTime <= endTime)
                                    {
                                        scMsg.ConfirmDeptName = dr["DeptCName"].ToString();//确认科室名称
                                        scMsg.ConfirmDeptCode = dr["DeptCode"].ToString();
                                        scMsg.ConfirmDeptCodeHIS = dr["DeptHisCode"].ToString();
                                        scMsg.ConfirmDeptID = dr["DeptID"] == DBNull.Value ? 0 : ((long)dr["DeptID"]);
                                        isTimeFlag = true;
                                    }
                                }
                            }
                            if (isTimeFlag)
                                break;
                        }
                    }
                }//
            }
            return scMsg;
        }

        private string GetXMLNodeValue(XElement node)
        {
            string strResult = "";
            if (node != null)
                strResult = node.Value;
            return strResult;

        }

        private string XMLStrToJson(string xmlText)
        {
            string jsonText = "";
            try
            {
                XmlDocument docXml = new XmlDocument();
                docXml.LoadXml(xmlText);
                jsonText = JsonConvert.SerializeXmlNode(docXml);
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("XML转Json异常：" + ex.Message);
            }
            return jsonText;
        }

        private string XMLStrDisposeTest(string xmlText)
        {
            string jsonText = xmlText;
            try
            {
                XmlDocument docXml = new XmlDocument();
                docXml.LoadXml(xmlText);

                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, null);
                writer.Formatting = System.Xml.Formatting.None;
                docXml.Save(writer);
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                stream.Position = 0;
                jsonText = sr.ReadToEnd();
                jsonText = jsonText.Replace("\"", "\\\"");
                sr.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("XML转Json异常：" + ex.Message);
            }
            return jsonText;
        }

        private string XMLStrDispose(string xmlText)
        {
            StringBuilder strXml = new StringBuilder(xmlText);
            try
            {
                strXml = strXml.Replace("\n", "").Replace("\t", "").Replace("\r", "");
                strXml = strXml.Replace("\"", "\\\"");
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("XML字符串处理异常：" + ex.Message);
            }
            return strXml.ToString();
        }

        private string GetFieldValueByName(string tableName, string fieldName, string whereFieldName, string whereFieldValue)
        {
            string strResult = "";
            if ((!string.IsNullOrEmpty(tableName)) && (!string.IsNullOrEmpty(fieldName)) && (!string.IsNullOrEmpty(whereFieldName)) && (!string.IsNullOrEmpty(whereFieldValue)))
            {
                string sql = "select " + fieldName + " from " + tableName +
                    " where " + whereFieldName + "=\'" + whereFieldValue + "\'";
                DataSet ds = SqlServerHelper.QuerySql(sql, SqlServerHelper.DigitlabConnectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strResult = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            return strResult;
        }

        private int GetMsgJudgeTimeSpan()
        {
            int timeSpan = 5;
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl");
            if (string.IsNullOrEmpty(url))
            {
                ZhiFang.LabStar.Common.LogHelp.Error("服务地址不能为空");
                return timeSpan;
            }
            else
                url += "/ServerWCF/LIIPCommonService.svc/SC_GetMSGParameterByParaNo";

            ZhiFang.Entity.Base.BaseResultDataValue brdv = ZhiFang.LabStar.Common.HTTPRequest.WebRequestHttpGet(url, "paraNo=Msg_CV_ConfirmOutTimes", "application/json");
            if (brdv.success && (!string.IsNullOrEmpty(brdv.ResultDataValue)))
                if (!int.TryParse(brdv.ResultDataValue, out timeSpan))
                    timeSpan = 5;
            return timeSpan;
        }
    }

}
