using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.BLL.LabStar;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LabStar;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    //检验业务系统公共服务
    public class LabStarCommonService : ILabStarCommonService
    {

        #region

        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        #endregion

        public BaseResultDataValue GetPinYinZiTou(string chinese)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (chinese != null && chinese.Length > 0)
                {
                    char[] tmpstr = chinese.ToCharArray();
                    foreach (char a in tmpstr)
                    {
                        baseResultDataValue.ResultDataValue += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "字符串【" + chinese + "】格式不正确！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "字符串【" + chinese + "】获取拼音字头出错：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetMaxNoByEntityField(string entityName, string entityField)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = IBLisCommon.GetMaxNoByFieldName(entityName, entityField);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetLabStarEnumType(string enumTypeName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (!string.IsNullOrWhiteSpace(enumTypeName))
                {
                    Dictionary<string, BaseClassDicEntity> dic = null;
                    enumTypeName = enumTypeName.ToLower();
                    switch (enumTypeName)
                    {
                        case "sectiontype":
                            dic = SectionType.GetStatusDic();
                            break;
                        case "itemphrase":
                            dic = ItemPhrase.GetStatusDic();
                            break;
                        case "samplephrase":
                            dic = SamplePhrase.GetStatusDic();
                            break;
                        case "resultvaluetype":
                            dic = ResultValueType.GetStatusDic();
                            break;
                    }
                    if (dic != null)
                        baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(dic);
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "无此【" + enumTypeName + "】枚举类型的信息！";
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="collectTime">样本单采样时间</param>
        /// <param name="testTime">样本单核收时间</param>
        /// <param name="DataAddTime">样本单新增时间</param>
        /// <param name="birthday">出生日期</param>
        /// <returns></returns>
        public BaseResultDataValue GetPatientAge(string collectTime, string testTime, string DataAddTime, string birthday)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (!string.IsNullOrEmpty(collectTime))
                    baseResultDataValue.ResultDataValue = LisCommonMethod.GetAge(birthday, collectTime);
                else if (!string.IsNullOrEmpty(testTime))
                    baseResultDataValue.ResultDataValue = LisCommonMethod.GetAge(birthday, testTime);
                else if (!string.IsNullOrEmpty(DataAddTime))
                    baseResultDataValue.ResultDataValue = LisCommonMethod.GetAge(birthday, DataAddTime);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            return baseResultDataValue;
        }

        public Message ExecSQL()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string strSQL = "";
            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "strSQL":
                        strSQL = HttpContext.Current.Request.Form["strSQL"];
                        break;
                }
            }
            ZhiFang.LabStar.Common.LogHelp.Info("执行SQL语句的用户信息如下：");
            ZhiFang.LabStar.Common.LogHelp.Info("EmployeeID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeID));
            ZhiFang.LabStar.Common.LogHelp.Info("EmployeeName:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            ZhiFang.LabStar.Common.LogHelp.Info("UserID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.UserID));
            ZhiFang.LabStar.Common.LogHelp.Info("UserAccount:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.UserAccount));
            ZhiFang.LabStar.Common.LogHelp.Info("HRDeptID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptID));
            ZhiFang.LabStar.Common.LogHelp.Info("HRDeptName:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptName));
            try
            {
                baseResultDataValue = IBLisCommon.ExecSQL(strSQL);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        public Message QuerySQL()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string strSQL = "";
            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "strSQL":
                        strSQL = HttpContext.Current.Request.Form["strSQL"];
                        break;
                }
            }
            ZhiFang.LabStar.Common.LogHelp.Info("执行SQL语句的用户信息如下：");
            ZhiFang.LabStar.Common.LogHelp.Info("EmployeeID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeID));
            ZhiFang.LabStar.Common.LogHelp.Info("EmployeeName:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeName));
            ZhiFang.LabStar.Common.LogHelp.Info("UserID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.UserID));
            ZhiFang.LabStar.Common.LogHelp.Info("UserAccount:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.UserAccount));
            ZhiFang.LabStar.Common.LogHelp.Info("HRDeptID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptID));
            ZhiFang.LabStar.Common.LogHelp.Info("HRDeptName:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptName));
            try
            {
                DataSet ds = IBLisCommon.QuerySQL(strSQL);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        baseResultDataValue.ResultDataValue = "{\"count\":" + ds.Tables[0].Rows.Count + ",\"list\":" + ZhiFang.Common.Public.JsonHelp.DataSetToJson(ds) + "}";
                    else
                    {
                        string columnName = "";
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (columnName == "")
                                columnName = dc.ColumnName;
                            else
                                columnName += "," + dc.ColumnName;
                        }
                        baseResultDataValue.ResultDataValue = "{\"count\":0,\"fields\":\"" + columnName + "\"}";
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
    }
}
