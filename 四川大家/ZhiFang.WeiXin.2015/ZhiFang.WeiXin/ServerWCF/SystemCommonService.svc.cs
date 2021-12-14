using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.ServerContract;

namespace ZhiFang.WeiXin.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SystemCommonService: ISystemCommonService
    {
        #region 获取程序内部字典
        public BaseResultDataValue GetEnumDic(string enumname)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetEnumDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetEnumDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue GetClassDic(string classname, string classnamespace)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string entitynamespace = "ZhiFang.Entity.Base";
            if (classname == null || classname.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classname为空！";
                return tempBaseResultDataValue;
            }
            if (classnamespace == null || classnamespace.Trim() == "")
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：classnamespace为空！";
                return tempBaseResultDataValue;
            }
            try
            {
                entitynamespace = classnamespace;
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + classname);
                if (t == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：未找到类字典：" + classname + ",命名空间：" + classnamespace + "！";
                    return tempBaseResultDataValue;
                }
                string jsonstring = "";
                var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in t.GetFields())
                {
                    JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                    jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                }
                jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonstring + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDic错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDic错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (jsonpara.Length <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：参数为空！";
                    ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：参数为空");
                }
                string jsonresult = "";
                foreach (ClassDicSearchPara cdsp in jsonpara)
                {
                    if (cdsp.classname == null || cdsp.classname.Trim() == "" || cdsp.classnamespace == null || cdsp.classnamespace.Trim() == "")
                    {
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":},";
                    }
                    else
                    {
                        string entitynamespace = "";
                        entitynamespace = cdsp.classnamespace;
                        Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + cdsp.classname);
                        if (t == null)
                        {
                            ZhiFang.Common.Log.Log.Error("GetClassDicList错误信息：未找到类字典：" + cdsp.classname + ",命名空间：" + cdsp.classnamespace + "！");
                            jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[]},";
                            continue;
                        }
                        string jsonstring = "";
                        var p = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

                        foreach (FieldInfo field in t.GetFields())
                        {
                            JObject jsono = JObject.Parse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)));
                            jsonstring += jsono["Value"].ToString(Formatting.None) + ",";
                            //jsonstring += ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(field.GetValue(null)) + ",";
                        }
                        jsonstring = jsonstring.Substring(0, jsonstring.Length - 1);
                        jsonresult += "{" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(cdsp.classname) + ":[" + jsonstring + "]},";
                    }
                }
                jsonresult = jsonresult.Substring(0, jsonresult.Length - 1);
                tempBaseResultDataValue.ResultDataValue = "[" + jsonresult + "]";
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetClassDicList错误信息：" + ex.ToString();
                ZhiFang.Common.Log.Log.Debug("GetClassDicList错误信息：" + ex.ToString());
            }
            return tempBaseResultDataValue;
        }
        #endregion
    }
}
