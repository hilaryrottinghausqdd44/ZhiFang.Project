using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.ServiceModel.Activation;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.LabStar;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CommonService : ICommonService
    {
        IBBPara IBBPara { get; set; }

        #region 升级服务
        public BaseResultDataValue GetSystemVersion()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string dbversion = "1.0.0.1";
                string sysversion = ((Assembly.GetExecutingAssembly()).GetName()).Version.ToString();
                //var tmpp = IBBPara.QuerySystemDefaultParaValueByParaNo("SYS_DBVersion");
                dbversion = ZhiFang.DBUpdate.DBUpdate.GetExternalDataBaseCurVersion();
                brdv.success = true;
                brdv.ResultDataValue = "{DBVersion:'" + dbversion + "',SYSVersion:'" + sysversion + "'}";
                //brdv.ResultDataValue = "DBVersion:'" + dbversion + "',SYSVersion:'" + sysversion + "'";
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("GetSystemVersion.异常：" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "获取版本异常！";
                return brdv;
            }
        }

        public BaseResultDataValue GetUpDateVersion()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string EmpID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                ZhiFang.LabStar.Common.LogHelp.Warn("GetUpDateVersion,升级动作。EmpID=" + EmpID + ",EmpName=" + EmpName + ",ClientIP=" + ZhiFang.LabStar.Common.IPHelper.GetClientIP() + "");
                brdv.success = ZhiFang.DBUpdate.DBUpdate.DataBaseUpdate("");
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("GetUpDateVersion.异常：" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "版本升级异常！";
                return brdv;
            }
        }
        #endregion

        #region 获取程序内部字典
        public BaseResultDataValue GetEnumDic(string enumname)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "GetEnumDic错误信息：" + ex.ToString();
                Log.Debug("GetEnumDic错误信息：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetClassDic(string classname, string classnamespace)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string entitynamespace = "ZhiFang.Entity.LabStar";
            if (classname == null || classname.Trim() == "")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：参数classname为空！";
                return baseResultDataValue;
            }
            if (classnamespace != null && classnamespace.Trim() != "")
            {
                entitynamespace = classnamespace;
            }
            try
            {
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
                if (t == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：未找到类字典：" + classname + ",命名空间：" + classnamespace + "！";
                    return baseResultDataValue;
                }
                string jsonstring = "";
                var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in t.GetFields())
                {
                    JObject jsono = JObject.Parse(JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                    jsonstring += jsono["Value"].ToString(Newtonsoft.Json.Formatting.None) + ",";
                }
                jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                baseResultDataValue.ResultDataValue = "[" + jsonstring + "]";
                baseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "GetClassDic错误信息：" + ex.ToString();
                Log.Debug("GetClassDic错误信息：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (jsonpara.Length <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "GetClassDicList错误信息：参数为空！";
                    Log.Debug("GetClassDicList错误信息：参数为空");
                }
                string jsonresult = "";
                foreach (ClassDicSearchPara cdsp in jsonpara)
                {
                    if (cdsp.classname == null || cdsp.classname.Trim() == "" || cdsp.classnamespace == null || cdsp.classnamespace.Trim() == "")
                    {
                        jsonresult += "{" + JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":},";
                    }
                    else
                    {
                        string entitynamespace = "";
                        entitynamespace = cdsp.classnamespace;
                        Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + cdsp.classname);
                        if (t == null)
                        {
                            Log.Error("GetClassDicList错误信息：未找到类字典：" + cdsp.classname + ",命名空间：" + cdsp.classnamespace + "！");
                            jsonresult += "{" + JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[]},";
                            continue;
                        }
                        string jsonstring = "";
                        var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                        foreach (FieldInfo field in t.GetFields())
                        {
                            JObject jsono = JObject.Parse(JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                            jsonstring += jsono["Value"].ToString(Newtonsoft.Json.Formatting.None) + ",";
                            //jsonstring += JsonSerializer.JsonDotNetSerializer(field.GetValue(null)) + ",";
                        }
                        jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                        jsonresult += "{" + JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[" + jsonstring + "]},";
                    }
                }
                jsonresult = jsonresult.Substring(0, jsonresult.Length - 1);
                baseResultDataValue.ResultDataValue = "[" + jsonresult + "]";
                baseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "GetClassDicList错误信息：" + ex.ToString();
                Log.Debug("GetClassDicList错误信息：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion
    }
}
