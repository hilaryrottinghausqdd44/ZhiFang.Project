using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.Common.Log;
using ZhiFang.ReportFormQueryPrint.BLL;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;
using ZhiFang.ReportFormQueryPrint.ServerContract;
using static ZhiFang.ReportFormQueryPrint.Common.Cookie;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DictionaryService : IDictionaryService
    {
        public Model.BaseResultDataValue GetDeptList(string Where, string fields, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                if (page <= 0 || limit <= 0)
                {
                    brdv.ErrorInfo = "分页参数错误！";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BDepartment bdept = new BLL.BDepartment();
                //DataSet deptlist = bdept.GetList(Where, page, limit);
                DataSet deptlist = bdept.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<DepartmentVO>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.None, settings);
                    brdv.ResultDataValue = aa;
                    //brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";

                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetDeptList:" + ex.ToString());
                brdv.ErrorInfo = "GetDeptList:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public Model.BaseResultDataValue GetPGroup(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BPGroup bpgroup = new BLL.BPGroup();
                DataSet deptlist = bpgroup.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<PGroup>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetPGroup:" + ex.ToString());
                brdv.ErrorInfo = "GetPGroup:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public Model.BaseResultDataValue GetDeptListPaging(string Where, string fields, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                if (page <= 0 || limit <= 0)
                {
                    brdv.ErrorInfo = "分页参数错误！";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BDepartment bdept = new BLL.BDepartment();
                //DataSet deptlist = bdept.GetList(Where, page, limit);
                DataSet deptlist = bdept.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<DepartmentVO>(deptlist.Tables[0]);
                    Log.Debug(Result.Count.ToString() + "   || " + deptlist.Tables[0].Rows.Count.ToString());
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);

                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    brdv.success = true;
                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetDeptList:" + ex.ToString());
                brdv.ErrorInfo = "GetDeptList:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public Model.BaseResultDataValue GetPGroupPaging(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BPGroup bpgroup = new BLL.BPGroup();
                DataSet deptlist = bpgroup.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<PGroup>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetPGroup:" + ex.ToString());
                brdv.ErrorInfo = "GetPGroup:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetAllColumnsSetting() {
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();

            BaseResultDataValue brdv = new BaseResultDataValue();
            BBColumnsUnit bcs = new BBColumnsUnit();
            try
            {
                DataSet ds = bcs.GetList(" 1=1 and IsUse='True' ");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BColumnsUnit>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue AddColumnsTempale(List<BColumnsSetting> columnsTemplate) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (columnsTemplate == null | columnsTemplate.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }            
            BBColumnsSetting columnsTemplateBll = new BBColumnsSetting();
            bool flag = true;
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddColumnsTempale.操作者IP地址:"+ ip);

                foreach (var item in columnsTemplate)
                {
                    int f = columnsTemplateBll.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }

            brdv.success = flag;

            return brdv;
        }

        public BaseResultDataValue GetColumnsTemplateByAppType(string AppType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBColumnsSetting ColumnsTemplateBll = new BBColumnsSetting();
            if (AppType == null || AppType.Length < 1)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GetColumnsTemplateByAppType:AppType不能为空";
                return brdv;
            }
            try
            {
                DataSet ds = ColumnsTemplateBll.GetList(" apptype='" + AppType + "' and B_ColumnsSetting.IsUse = 'True' ", "orderNo");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BColumnsSetting>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ils, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }

            return brdv;
        }
        public BaseResultDataValue GetAllColumnsTemplate(string appType, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBColumnsSetting ColumnsTemplateBll = new BBColumnsSetting();
            try
            {
                bool v = SqlInjectHelper.CheckKeyWord(appType);
                if (v)
                {
                    brdv.ErrorInfo = "Error:存在SQL注入请注意传入条件!";
                    brdv.success = false;
                    return brdv;
                }
                DataSet ds = ColumnsTemplateBll.GetList(" appType='" + appType + "' and B_ColumnsSetting.IsUse = 'True'");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    List<BColumnsSetting> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BColumnsSetting>(ds.Tables[0]);
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<BColumnsSetting>(page, limit, ils);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + ils.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }

            return brdv;
        }

        public BaseResultDataValue deleteColumnsTempale(List<long> CTIDList) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBColumnsSetting ColumnsTemplateBll = new BBColumnsSetting();
            if (CTIDList == null || CTIDList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("deleteColumnsTempale.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in CTIDList)
                {
                    int flag = ColumnsTemplateBll.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetAllSelectSetting()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchUnit selectSettingBll = new BBSearchUnit();
            try
            {
                DataSet ds = selectSettingBll.GetList(" 1=1 ");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BSearchUnit>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue AddSelectTempale(List<Model.BSearchSetting> selectTempale)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (selectTempale == null | selectTempale.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            bool flag = true;
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddSelectTempale.操作者IP地址:" + ip);
                
                foreach (var item in selectTempale)
                {
                    int f = selectTemplateBll.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }

            brdv.success = flag;

            return brdv;
        }
        public BaseResultDataValue GetSelectTemplateByAppType(string AppType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            if (AppType == null || AppType.Length < 1)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GetSelectTemplateByAppType：Apptype不能为空";
                return brdv;
            }
            try
            {
                DataSet ds = selectTemplateBll.GetList(" appType='" + AppType + "'", "ShowOrderNo");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BSearchSetting>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ils, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }

            return brdv;
        }

        public BaseResultDataValue GetAllSelectTemplate(string appType, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            try
            {
                bool v = SqlInjectHelper.CheckKeyWord(appType);
                if (v)
                {
                    brdv.ErrorInfo = "Error:存在SQL注入请注意传入条件!";
                    brdv.success = false;
                    return brdv;
                }
                DataSet ds = selectTemplateBll.GetList(" appType='" + appType + "'");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    List<BSearchSetting> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BSearchSetting>(ds.Tables[0]);
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<BSearchSetting>(page, limit, ils);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + ils.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }

            return brdv;
        }

        public BaseResultDataValue deleteSelectTempale(List<int> STIDList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            if (STIDList == null || STIDList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("deleteSelectTempale.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in STIDList)
                {
                    int flag = selectTemplateBll.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue UpdatePublicSetting(List<BParameter> models) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBParameter bps = new BBParameter();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdatePublicSetting.操作者IP地址:" + ip);

                foreach (var item in models)
                {
                    bps.Update(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetAllPublicSetting(string pageType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBParameter bps = new BBParameter();
            try
            {
                DataSet ds = bps.GetList("sname='" + pageType + "'");
                brdv.success = true;
                if (!(ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
                {
                    if (pageType == "selfhelp")
                    {
                        ds = bps.GetList("sname='自助打印'");
                    }
                    else {
                        ds = bps.GetList("sname='allPageType'");
                    }
                    
                }
                var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BParameter>(ds.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }
        public BaseResultDataValue UserLogin(string Account, string pwd)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if (Account == null || pwd == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用户名密码不能为空";
                ZhiFang.Common.Log.Log.Debug("UserLogin:用户名密码不能为空");
                return brdv;
            }

            try
            {
                BPUser user = new BPUser();
                pwd = user.CovertPassWord(pwd);
                string where = "ShortCode = '" + Account + "' and password='" + pwd + "'";
                //ZhiFang.Common.Log.Log.Debug("UserLogin:ShortCode=" + Account + " passWord=" + pwd);
                DataSet ds = user.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.success = true;
                    if (ds.Tables[0].Rows[0]["SectorTypeNo"] != null && ds.Tables[0].Rows[0]["SectorTypeNo"].ToString().Length > 0)
                    {
                        BDepartment Bdepartment = new BDepartment();
                        Department department = Bdepartment.GetModel(int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString()));
                        if (department != null && department.CName != null)
                        {
                            Common.Cookie.CookieHelper.Write("ULdept", department.CName);
                        }
                        else {
                            Common.Cookie.CookieHelper.Write("ULdept", null);
                        } 
                    }

                    var Result = user.DataTableToList(ds.Tables[0]); ;
                    var settings = new JsonSerializerSettings();
                    string dsJson = JsonConvert.SerializeObject(Result[0], Newtonsoft.Json.Formatting.Indented, settings);

                    //HttpContext.Current.Session["user"] = dsJson;
                    Common.Cookie.CookieHelper.Write("ULUserNo", Result[0].UserNo.ToString());
                    Common.Cookie.CookieHelper.Write("ULUserCName", Result[0].CName.ToString());
                    Common.Cookie.CookieHelper.Write("ULShortCode", Result[0].ShortCode.ToString());
                    ZhiFang.Common.Log.Log.Debug("查询到用户");
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue UpdateUserPwd(int userNo, string oldPwd, string newPwd) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            ZhiFang.Common.Log.Log.Debug("UpdateUserPwd:userNo=" + userNo.ToString() + " oldPwd=" + oldPwd + " newPwd=" + newPwd);
            if (oldPwd == null || newPwd == null || oldPwd.Length < 2 || newPwd.Length < 2)
            {
                brdv.ErrorInfo = "密码格式不正确";
                return brdv;
            }
            try
            {
                BPUser user = new BPUser();
                oldPwd = user.CovertPassWord(oldPwd);
                DataSet ds = user.GetList(" userNo =" + userNo + " and password='" + oldPwd + "'");
                if (!(ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0))
                {
                    brdv.ErrorInfo = "当前密码不正确";
                    return brdv;
                }
                newPwd = user.CovertPassWord(newPwd);
                PUser puser = new PUser();
                puser.UserNo = userNo;
                puser.Password = newPwd;
                puser.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                int flag = user.Update(puser);
                if (flag > 0)
                {
                    brdv.success = true;
                }
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdateUserPwd:" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "UpdateUserPwd:出现错误请查看日志!";
                return brdv;
            }
        }

        public BaseResultDataValue SetColumnsDefaultSetting(string appType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue
            {
                success = false
            };
            BBColumnsSetting bColumnsSetting = new BBColumnsSetting();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("SetColumnsDefaultSetting.操作者IP地址:" + ip);

                bColumnsSetting.deleteByAppType(appType);
                ZhiFang.Common.Log.Log.Debug("SetColumnsDefaultSetting:原配置已删除");
                brdv.success = bColumnsSetting.SetDefaultSetting(appType);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue SetSearchDefaultSetting(string appType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue
            {
                success = false
            };
            BBSearchSetting bBSearchSetting = new BBSearchSetting();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("SetSearchDefaultSetting.操作者IP地址:" + ip);

                bBSearchSetting.deleteByAppType(appType);
                ZhiFang.Common.Log.Log.Debug("SetColumnsDefaultSetting:原配置已删除");
                brdv.success = bBSearchSetting.SetDefaultSetting(appType);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue SetPublicDefaultSetting(string appType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue
            {
                success = true
            };
            BBParameter bBParameter = new BBParameter();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("SetPublicDefaultSetting.操作者IP地址:" + ip);

                bBParameter.deleteBySName(appType);
                DataSet ds = bBParameter.GetList("sname='allPageType'");
                List<BParameter> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BParameter>(ds.Tables[0]);
                int count = 0;
                foreach (var item in Result)
                {
                    if (!appType.Equals("医生"))
                    {
                        if (item.ParaNo.Equals("defaultWhere") || item.ParaNo.Equals("requestParamsArr") || item.ParaNo.Equals("hisRequestParamsArr"))
                        {
                            continue;
                        }
                    }

                    item.SName = appType;
                    count = bBParameter.Add(item);
                    if (count < 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
            }
            return brdv;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public BaseResultDataValue UpdateColumnsSetting(List<Model.BColumnsSetting> bColumnsSetting)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (bColumnsSetting == null || bColumnsSetting.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateColumnsSetting.操作者IP地址:" + ip);

                BBColumnsSetting bbs = new BBColumnsSetting();
                int isok = bbs.Update(bColumnsSetting[0]);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue UpdateSearchSetting(List<BSearchSetting> bSearchSetting)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (bSearchSetting == null || bSearchSetting.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateSearchSetting.操作者IP地址:" + ip);

                BBSearchSetting bss = new BBSearchSetting();
                int isok = bss.Update(bSearchSetting[0]);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }
        public BaseResultDataValue GetSectionPrintList(string SectionNo, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BSectionPrint bsp = new BSectionPrint();
            brdv.success = false;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                DataSet ds = null;
                if (null != SectionNo && !("").Equals(SectionNo))
                {
                    ds = bsp.GetSectionPgroupList("a.SectionNo=" + SectionNo);
                }
                else
                {
                    ds = bsp.GetSectionPgroupList("1=1");
                }

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SelectSectionPrint>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
                brdv.success = true;


            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }
        /** 查询小组列表 */
        public BaseResultDataValue GetPGroupCNameList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.ReportFormQueryPrint.BLL.BPGroup bpgroup = new BLL.BPGroup();
                DataSet deptlist = bpgroup.GetList("1 = 1");
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<PGroup>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetPGroup:" + ex.ToString());
                brdv.ErrorInfo = "GetPGroup:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateSectionPrint(Model.SectionPrint sectionPrint)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (sectionPrint == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateSectionPrint.操作者IP地址:" + ip);

                BSectionPrint bsp = new BSectionPrint();
                int isok = bsp.Update(sectionPrint);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue AddSectionPrint(Model.SectionPrint entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddSectionPrint.操作者IP地址:" + ip);

                BSectionPrint bsp = new BSectionPrint();
                int isok = bsp.Add(entity);
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue DeleteSectionPrint(List<int> SPID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BSectionPrint bsp = new BSectionPrint();
            if (SPID == null || SPID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("DeleteSectionPrint.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in SPID)
                {
                    int flag = bsp.DeleteSectionPrint(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetChargeType(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null) {
                Where = " 1=1 ";
            }
            try
            {

                ZhiFang.ReportFormQueryPrint.BLL.ChargeType cht = new BLL.ChargeType();
                DataSet deptlist = cht.GetChargeType(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.ChargeType>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetChargeType:" + ex.ToString());
                brdv.ErrorInfo = "GetChargeType:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetDistrict(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {

                ZhiFang.ReportFormQueryPrint.BLL.BDistrict cht = new BLL.BDistrict();
                DataSet deptlist = cht.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.ChargeType>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetDistrict:" + ex.ToString());
                brdv.ErrorInfo = "GetDistrict:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetOperatorChecker(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {
                ZhiFang.ReportFormQueryPrint.BLL.BPUser cht = new BLL.BPUser();
                DataSet deptlist = cht.GetOperatorChecker(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.ReportForm>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetOperatorChecker:" + ex.ToString());
                brdv.ErrorInfo = "GetOperatorChecker:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetAllSeniorSearch(string urlwhere)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchUnit selectSettingBll = new BBSearchUnit();
            try
            {
                DataSet ds = selectSettingBll.GetList(urlwhere);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BSearchUnit>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }
        public BaseResultDataValue GetSeniorSetting(string appType, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            try
            {
                DataSet ds = selectTemplateBll.GetList(" appType='" + appType + "'");
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    List<BSearchSetting> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BSearchSetting>(ds.Tables[0]);
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<BSearchSetting>(page, limit, ils);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + ils.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }

            return brdv;
        }

        public BaseResultDataValue AddSeniorSetting(List<Model.BSearchSetting> selectTempale)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (selectTempale == null | selectTempale.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }
            BBSearchSetting selectTemplateBll = new BBSearchSetting();
            bool flag = true;
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddSeniorSetting.操作者IP地址:" + ip);

                foreach (var item in selectTempale)
                {
                    int isok = selectTemplateBll.GetIsSenior(item.STID);
                    if (isok <= 0)
                    {
                        int f = selectTemplateBll.Add(item);
                        if (f <= 0)
                        {
                            flag = false;
                        }
                    }
                    else {
                        int f = selectTemplateBll.Update(item);
                        if (f <= 0)
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }

            brdv.success = flag;

            return brdv;
        }

        public BaseResultDataValue GetSeniorPublicSetting(string SName, string ParaNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (SName == null|| ParaNo==null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }
            BBParameter bbp = new BBParameter();
            bool flag = true;
            try
            {
                var ds = bbp.GetSeniorPublicSetting(SName, ParaNo);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    List<BParameter> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<BParameter>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ils, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }

            brdv.success = flag;

            return brdv;
        }

        public BaseResultDataValue GetSickType(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
           
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BSickType bst = new BLL.BSickType();         
                DataSet deptlist = bst.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SickType>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    //brdv.ResultDataValue = aa;

                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetSickType:" + ex.ToString());
                brdv.ErrorInfo = "GetSickType:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

            return brdv;
        }
        public BaseResultDataValue GetSampleType(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BSampleType bst = new BLL.BSampleType();
                DataSet deptlist = bst.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SampleType>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    //brdv.ResultDataValue = aa;

                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetSickType:" + ex.ToString());
                brdv.ErrorInfo = "GetSickType:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue GetSectionPrintListBySectionNo(string SectionNo, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BSectionPrint bsp = new BSectionPrint();
            brdv.success = false;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                DataSet ds = null;
                if (null != SectionNo && !("").Equals(SectionNo))
                {
                    ds = bsp.GetSectionPgroupList("a.SectionNo=" + SectionNo);
                }
                else
                {
                    ds = bsp.GetSectionPgroupList("1=1");
                }

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SelectSectionPrint>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + list.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;


            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetTestItemItemDescByItemNo(string ItemNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BReportItem bri = new BReportItem();
            brdv.success = false;
            
            try
            {
                    DataSet ds = null;
              
                    ds = bri.getTestItemItemDescByitem(ItemNo);
               

                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<TestItem>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
                brdv.success = true;


            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue GetWardType(string Where, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                
                ZhiFang.ReportFormQueryPrint.BLL.BWardType bst = new BLL.BWardType();
                DataSet deptlist = bst.GetWardType(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<WardType>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    //brdv.ResultDataValue = aa;

                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetSickType:" + ex.ToString());
                brdv.ErrorInfo = "GetSickType:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue GetPUser(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {
                ZhiFang.ReportFormQueryPrint.BLL.BPUser cht = new BLL.BPUser();
                DataSet deptlist = cht.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.PUser>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetPUser:" + ex.ToString());
                brdv.ErrorInfo = "GetPUser:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        //public BaseResultDataValue AddAndUpdatePUser(Model.PUser entity) {
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    if (entity == null )
        //    {
        //        brdv.success = false;
        //        brdv.ErrorInfo = "传入参数不能为空";
        //        return brdv;
        //    }
        //    ZhiFang.ReportFormQueryPrint.BLL.BPUser cht = new BLL.BPUser();
        //    bool flag = true;
        //    try
        //    {
        //        //foreach (var item in PUser)
        //        //{
        //        entity.Password=cht.CovertPassWord(entity.Password);
        //            int isok = cht.GetIsPUser(entity.UserNo);
        //            if (isok <= 0)
        //            {
        //                int f = cht.Add(entity);
        //                if (f <= 0)
        //                {
        //                    flag = false;
        //                }
        //            }
        //            else
        //            {
        //                int f = cht.Update(entity);
        //                if (f <= 0)
        //                {
        //                    flag = false;
        //                }
        //            }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Debug(ex.ToString());
        //        brdv.success = false;
        //        brdv.ErrorInfo = ex.Message.ToString();
        //        return brdv;
        //    }

        //    brdv.success = flag;

        //    return brdv;
        //}

        public BaseResultDataValue GetEmpDeptLinks(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {
                ZhiFang.ReportFormQueryPrint.BLL.BEmpDeptLinks cht = new BLL.BEmpDeptLinks();
                DataSet deptlist = cht.GetEmpDeptLinks(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.EmpDeptLinks>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetEmpDeptLinks:" + ex.ToString());
                brdv.ErrorInfo = "GetEmpDeptLinks:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue AddEmpDeptLinks(List<EmpDeptLinks> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }
            ZhiFang.ReportFormQueryPrint.BLL.BEmpDeptLinks cht = new BLL.BEmpDeptLinks();
            bool flag = true;
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddEmpDeptLinks.操作者IP地址:" + ip);

                string userno = "";
                ZhiFang.Common.Log.Log.Debug("执行方法AddEmpDeptLinks:增加用户对应科室数据开始");
                foreach (var item in entity)
                {
                    if (userno == "") {
                        userno=item.UserNo+"";
                    }
                   
                    int f = cht.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                    ZhiFang.Common.Log.Log.Debug("用户:"+item.UserCName+"; 科室:"+item.DeptCName+";");
                }
                ZhiFang.Common.Log.Log.Debug("执行方法AddEmpDeptLinks:增加用户对应科室数据结束");
                DataSet BEmpDeptLinkslist = cht.GetEmpDeptLinks(" UserNO = "+ userno);
                var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.EmpDeptLinks>(BEmpDeptLinkslist.Tables[0]);
                StringBuilder dept = new StringBuilder();
                dept.Append("[");
                for (var i = 0; i < list.Count; i++)
                {
                    if (i == 0)
                    {
                        dept.Append("{DeptNo:" + list[i].DeptNo + ",DeptCName:\"" + list[i].DeptCName + "\"}");
                    }
                    else
                    {
                        dept.Append(",{DeptNo:" + list[i].DeptNo + ",DeptCName:\"" + list[i].DeptCName + "\"}");
                    }
                }
                dept.Append("]");
                Common.Cookie.CookieHelper.Write("dept", dept.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }
            brdv.success = flag;
            return brdv;
        }

        public BaseResultDataValue DeleteEmpDeptLinks(List<EmpDeptLinks> entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数不能为空";
                return brdv;
            }
            ZhiFang.ReportFormQueryPrint.BLL.BEmpDeptLinks cht = new BLL.BEmpDeptLinks();
            bool flag = true;
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("DeleteEmpDeptLinks.操作者IP地址:" + ip);

                ZhiFang.Common.Log.Log.Debug("执行方法DeleteEmpDeptLinks:删除用户对应科室数据开始");
                foreach (var item in entity)
                {
                    int f = cht.Delete(item.EDLID);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                    ZhiFang.Common.Log.Log.Debug("用户:" + item.UserCName + "; 科室:" + item.DeptCName + ";");
                }
                ZhiFang.Common.Log.Log.Debug("执行方法DeleteEmpDeptLinks:删除用户对应科室数据结束");

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
                return brdv;
            }
            brdv.success = flag;

            return brdv;
        }

        public BaseResultDataValue EmpDeptLinksUserLogin(string Account, string pwd)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if (Account == null || pwd == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "用户名密码不能为空";
                ZhiFang.Common.Log.Log.Debug("UserLogin:用户名密码不能为空");
                return brdv;
            }

            try
            {
                BPUser user = new BPUser();
                pwd = user.CovertPassWord(pwd);
                string where = "ShortCode = '" + Account + "' and password='" + pwd + "'";
                ZhiFang.Common.Log.Log.Debug("UserLogin:ShortCode=" + Account + " passWord=" + pwd);
                DataSet ds = user.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.success = true;
                    if (ds.Tables[0].Rows[0]["UserNo"] != null && ds.Tables[0].Rows[0]["UserNo"].ToString().Length > 0)
                    {
                        BEmpDeptLinks BEmpDeptLinks = new BLL.BEmpDeptLinks();
                        DataSet BEmpDeptLinkslist = BEmpDeptLinks.GetEmpDeptLinks(" UserNO = "+ds.Tables[0].Rows[0]["UserNo"].ToString());
                        var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.EmpDeptLinks>(BEmpDeptLinkslist.Tables[0]);
                        StringBuilder dept = new StringBuilder();
                        dept.Append("[");
                        for (var i = 0;i<list.Count;i++ ) {
                            if (i == 0)
                            {
                                dept.Append("{DeptNo:" + list[i].DeptNo + ",DeptCName:\"" + list[i].DeptCName + "\"}");
                            }
                            else
                            {
                                dept.Append(",{DeptNo:" + list[i].DeptNo + ",DeptCName:\"" + list[i].DeptCName + "\"}");
                            }
                        }
                        dept.Append("]");
                        Common.Cookie.CookieHelper.Write("dept", dept.ToString());
                    }

                    var Result = user.DataTableToList(ds.Tables[0]); ;
                    var settings = new JsonSerializerSettings();
                    string dsJson = JsonConvert.SerializeObject(Result[0], Newtonsoft.Json.Formatting.Indented, settings);

                   //HttpContext.Current.Session["user"] = dsJson;
                    ZhiFang.ReportFormQueryPrint.Common.Cookie.CookieHelper.Write("UserNo", Result[0].UserNo.ToString());
                    Common.Cookie.CookieHelper.Write("UserCName", Result[0].CName.ToString());
                    Common.Cookie.CookieHelper.Write("ShortCode", Result[0].ShortCode.ToString());
                    ZhiFang.Common.Log.Log.Debug("查询到用户");
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue GetTestItem(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                if (Where == null || Where == "") {
                    Where = "1=1";
                }
                ZhiFang.ReportFormQueryPrint.BLL.BTestItem testitem = new BLL.BTestItem();
                DataSet testitemlist = testitem.GetList(Where,fields);
                if (testitemlist != null && testitemlist.Tables != null && testitemlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<TestItem>(testitemlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetTestItem:" + ex.ToString());
                brdv.ErrorInfo = "GetTestItem:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetClentele(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                if (Where == null || Where == "")
                {
                    Where = "1=1";
                }
                ZhiFang.ReportFormQueryPrint.BLL.BCLIENTELE clientele = new BLL.BCLIENTELE();
                DataSet clientelelist = clientele.GetList(Where, fields);
                if (clientelelist != null && clientelelist.Tables != null && clientelelist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<CLIENTELE>(clientelelist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetClentele:" + ex.ToString());
                brdv.ErrorInfo = "GetClentele:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleModuleFormGridLinkByModuleID(string ModuleID, string linkType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleModuleFormGridLink moduleLink = new BLL.BModuleModuleFormGridLink();
                //通过ModuleID和linkType获取相关列表，linkType--grid：显示列，form：查询条件
                if (string.IsNullOrWhiteSpace(ModuleID))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "入参不能为空";
                    return brdv;
                }
                string where = "ModuleID=" + ModuleID;
                //显示列
                if (linkType == "grid")
                {
                    where += " and GridId !=0";
                }
                //查询条件
                if (linkType == "form")
                {
                    where += " and FormId !=0";
                }
                DataSet ds = moduleLink.GetList(where);//此方法查询条件不能为空
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleModuleFormGridLink>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string list = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + list + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleModuleFormGridLinkByModuleCode:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleModuleFormGridLinkByModuleCode:" + ex.ToString();
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue GetBModuleModuleFormGridLink(string fields, int page, int limit, string where, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleModuleFormGridLink moduleLink = new BLL.BModuleModuleFormGridLink();

                DataSet ds = moduleLink.GetList(where);//此方法查询条件不能为空
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleModuleFormGridLink>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);


                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleModuleFormGridLinkByModuleCode:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleModuleFormGridLinkByModuleCode:" + ex.ToString();
                return brdv;
            }

            return brdv;
        }
        public BaseResultDataValue AddBModuleModuleFormGridLink(List<Model.BModuleModuleFormGridLink> ModuleFormGridLink)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleModuleFormGridLink bModuleModuleFormGridLink = new BLL.BModuleModuleFormGridLink();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleModuleFormGridLink.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in ModuleFormGridLink)
                {
                    int f = bModuleModuleFormGridLink.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleModuleFormGridLink:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleModuleFormGridLink" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleModuleFormGridLink(List<Model.BModuleModuleFormGridLink> ModuleFormGridLink)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ModuleFormGridLink == null || ModuleFormGridLink.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlSet.操作者IP地址:" + ip);
                BLL.BModuleModuleFormGridLink bLLBModuleModuleFormGridLink = new BLL.BModuleModuleFormGridLink();
                for (int i = 0; i < ModuleFormGridLink.Count; i++)
                {

                    bLLBModuleModuleFormGridLink.Update(ModuleFormGridLink[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleModuleFormGridLink:" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlSet:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleModuleFormGridLinkByModuleID(List<long> ModuleFormGridLinkID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleModuleFormGridLink bmmfgl = new BLL.BModuleModuleFormGridLink();
            if (ModuleFormGridLinkID == null || ModuleFormGridLinkID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleModuleFormGridLinkByModuleID.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in ModuleFormGridLinkID)
                {
                    int flag = bmmfgl.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleModuleFormGridLinkByModuleID:" + ex.ToString());

                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleFormList(int page, int limit, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormList bModuleFormList = new BLL.BModuleFormList();
            brdv.success = true;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = bModuleFormList.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleFormList>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleFormList:" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue AddBModuleFormList(List<Model.BModuleFormList> ModuleFormList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormList bllModuleFormList = new BLL.BModuleFormList();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleFormList.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in ModuleFormList)
                {
                    int f = bllModuleFormList.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleFormList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleFormList" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleFormList(List<Model.BModuleFormList> ModuleFormList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ModuleFormList == null || ModuleFormList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleFormList.操作者IP地址:" + ip);
                BLL.BModuleFormList bLLBModuleFormList = new BLL.BModuleFormList();
                for (int i = 0; i < ModuleFormList.Count; i++)
                {

                    bLLBModuleFormList.Update(ModuleFormList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleFormList:" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleFormList:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleFormList(List<long> FormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormList bllBModuleFormList = new BLL.BModuleFormList();
            if (FormID == null || FormID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleFormList.操作者IP地址:" + ip);
                brdv.success = true;
                foreach (var item in FormID)
                {
                    int flag = bllBModuleFormList.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleFormList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleFormControlListByFormCode(string FormCode, int page, int limit, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlList moduleFormControlList = new BLL.BModuleFormControlList();
            brdv.success = true;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleFormControlList.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleFormControlList>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleFormControlListByFormCode:" + e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue AddFormControlList(List<Model.BModuleFormControlList> ModuleFormControlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlList bllBModuleFormControlList = new BLL.BModuleFormControlList();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddFormControlList.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in ModuleFormControlList)
                {
                    int f = bllBModuleFormControlList.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddFormControlList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddFormControlList" + ex.Message.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateFormControlList(List<Model.BModuleFormControlList> ModuleFormControlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ModuleFormControlList == null || ModuleFormControlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateFormControlList.操作者IP地址:" + ip);
                BLL.BModuleFormControlList bLLBModuleFormControlList = new BLL.BModuleFormControlList();
                for (int i = 0; i < ModuleFormControlList.Count; i++)
                {

                    bLLBModuleFormControlList.Update(ModuleFormControlList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateFormControlList:" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateFormControlList:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteFormControlList(List<long> FormControlID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlList bllBModuleFormControlList = new BLL.BModuleFormControlList();
            if (FormControlID == null || FormControlID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteFormControlList.操作者IP地址:" + ip);
                brdv.success = true;
                foreach (var item in FormControlID)
                {
                    int flag = bllBModuleFormControlList.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteFormControlList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleFormControlSetByFormCode(string FormCode, int page, int limit, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlSet moduleFormControlSet = new BLL.BModuleFormControlSet();
            brdv.success = true;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleFormControlSet.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleFormControlSet>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleFormControlSetByGridCode:"+e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue AddBModuleFormControlSet(List<Model.BModuleFormControlSet> columnsTemplate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlSet moduleFormControlSet = new BLL.BModuleFormControlSet();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddColumnsTempale.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in columnsTemplate)
                {
                    int f = moduleFormControlSet.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleFormControlSet" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleFormControlSet(List<Model.BModuleFormControlSet> bModuleFormControlSetList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (bModuleFormControlSetList == null || bModuleFormControlSetList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateBModuleFormControlSet.操作者IP地址:" + ip);
                BLL.BModuleFormControlSet bLLBModuleFormControlSet = new BLL.BModuleFormControlSet();
                for (int i = 0; i < bModuleFormControlSetList.Count; i++)
                {

                    bLLBModuleFormControlSet.Update(bModuleFormControlSetList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleFormControlSet:" + ex.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateBModuleFormControlSet:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleFormControlSet(long FormControSetlID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleFormControlSet bModuleFormControlSet = new BLL.BModuleFormControlSet();
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("deleteBModuleFormControlSet.操作者IP地址:" + ip);

                brdv.success = true;

                int flag = bModuleFormControlSet.deleteById(FormControSetlID);
                if (flag <= 0)
                {
                    brdv.success = false;
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "deleteBModuleFormControlSet" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleGridList(string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleGridList bModuleGridList = new BLL.BModuleGridList();
                //string where = "GridCode='" + GridCode + "'";
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = bModuleGridList.GetList(where);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridList>(ds.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleGridList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleGridList:" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleGridListByModuleID(string ModuleID)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleModuleFormGridLink moduleLink = new BLL.BModuleModuleFormGridLink();
                BLL.BModuleGridList bModuleGridList = new BLL.BModuleGridList();
                //通过ModuleID和linkType获取相关列表，linkType--grid：显示列，form：查询条件
                if (string.IsNullOrWhiteSpace(ModuleID))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "入参不能为空";
                    return brdv;
                }
                string where = "ModuleID=" + ModuleID + " and GridId != 0";
                //先查出显示列的关系表
                DataSet moduleLinkds = moduleLink.GetList(where);//此方法查询条件不能为空
                brdv.success = true;
                if (moduleLinkds != null && moduleLinkds.Tables != null && moduleLinkds.Tables.Count > 0)
                {
                    List<Model.BModuleGridList> Result = new List<Model.BModuleGridList>();
                    //再通过关系表的gridcode查出所有gridlist
                    for (int i = 0; i < moduleLinkds.Tables[0].Rows.Count; i++)
                    {
                        string GridCode = moduleLinkds.Tables[0].Rows[i]["GridCode"].ToString();
                        string where2 = "GridCode='" + GridCode + "'";
                        DataSet ds = bModuleGridList.GetList(where2);
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                        {
                            var gridList = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridList>(ds.Tables[0]);
                            Result.Add(gridList[0]);//一个gridcode对应一个gridlist
                        }
                    }
                    var settings = new JsonSerializerSettings();
                    string list = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + list + "}";
                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleGridListByModuleID:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleGridListByModuleID" + ex.ToString();
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue AddBModuleGridList(List<Model.BModuleGridList> ModuleGridList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridList bllBModuleGridList = new BLL.BModuleGridList();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleGridList.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in ModuleGridList)
                {
                    int f = bllBModuleGridList.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleGridList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleGridList:" + ex.Message.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleGridList(List<Model.BModuleGridList> ModuleGridList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ModuleGridList == null || ModuleGridList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridList.操作者IP地址:" + ip);
                BLL.BModuleGridList bLLBModuleGridList = new BLL.BModuleGridList();
                for (int i = 0; i < ModuleGridList.Count; i++)
                {
                    bLLBModuleGridList.Update(ModuleGridList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleGridList:" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridList:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleGridList(List<long> GridListID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridList bllBModuleGridList = new BLL.BModuleGridList();
            if (GridListID == null || GridListID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleGridList.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in GridListID)
                {
                    int flag = bllBModuleGridList.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleGridList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleGridList:" + ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleGridControlListByGridCode(string GridCode, int page, int limit, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridControlList moduleGridControlList = new BLL.BModuleGridControlList();
            try
            {
                //string where = "GridCode=" + GridCode;
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleGridControlList.GetList(where);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridControlList>(ds.Tables[0]);
                    List<Model.BModuleGridControlList> ResultList = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<Model.BModuleGridControlList>(Convert.ToInt32(page), Convert.ToInt32(limit), Result);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ResultList, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + ResultList.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue AddBModuleGridControlList(List<Model.BModuleGridControlList> ModuleGridControlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridControlList bllBModuleGridControlList = new BLL.BModuleGridControlList();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleGridControlList.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in ModuleGridControlList)
                {
                    int f = bllBModuleGridControlList.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleGridControlList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleGridControlList:" + ex.Message.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleGridControlList(List<Model.BModuleGridControlList> ModuleGridControlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (ModuleGridControlList == null || ModuleGridControlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;               
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlList.操作者IP地址:" + ip);
                BLL.BModuleGridControlList bLLBModuleGridControlList = new BLL.BModuleGridControlList();
                for (int i = 0; i < ModuleGridControlList.Count; i++)
                {
                    bLLBModuleGridControlList.Update(ModuleGridControlList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleGridControlList:" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlList:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleGridControlList(List<long> GridControlListID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridControlList bllBModuleGridControlList = new BLL.BModuleGridControlList();
            if (GridControlListID == null || GridControlListID.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleGridControlList.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in GridControlListID)
                {
                    int flag = bllBModuleGridControlList.deleteById(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteBModuleGridControlList:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "deleteBModuleGridControlList"+ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue AddBModuleGridControlSet(List<Model.BModuleGridControlSet> columnsTemplate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleGridControlSet bModuleGridControlSet = new BLL.BModuleGridControlSet();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("AddBModuleGridControlSet.操作者IP地址:" + ip);
                bool flag = true;
                foreach (var item in columnsTemplate)
                {
                    int f = bModuleGridControlSet.Add(item);
                    if (f <= 0)
                    {
                        flag = false;
                    }
                }
                brdv.success = flag;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.AddBModuleGridControlSet:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "AddBModuleGridControlSet:" + ex.ToString();
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue UpdateBModuleGridControlSet(List<Model.BModuleGridControlSet> bModuleGridControlSetList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (bModuleGridControlSetList == null || bModuleGridControlSetList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlSet.操作者IP地址:" + ip);
                BLL.BModuleGridControlSet bLLBModuleGridControlSet = new BLL.BModuleGridControlSet();
                for (int i = 0; i < bModuleGridControlSetList.Count; i++)
                {

                    bLLBModuleGridControlSet.Update(bModuleGridControlSetList[i]);
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "UpdateBModuleGridControlSet:" + ex.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.UpdateBModuleGridControlSet:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleGridControlSetByGridCode(string GridCode, int page, int limit, string where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleGridControlSet moduleGridControlSet = new BLL.BModuleGridControlSet();
                //where += "and GridCode='" + GridCode + "'";
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleGridControlSet.GetList(where);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {

                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridControlSet>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + result.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleGridControlSetByGridCode" + ex.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleGridControlSetByGridCode:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue deleteBModuleGridControlSet(long GridControSetlID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleGridControlSet bModuleGridControlSet = new BLL.BModuleGridControlSet();
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteColumnsTempale.操作者IP地址:" + ip);

                brdv.success = true;

                int flag = bModuleGridControlSet.deleteById(GridControSetlID);
                if (flag <= 0)
                {
                    brdv.success = false;
                }

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "deleteBModuleGridControlSet" + ex.Message.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.deleteColumnsTempale.操作者IP地址:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleGridControlSetList(string where, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                BLL.BModuleGridControlSet moduleGridControlSet = new BLL.BModuleGridControlSet();
                
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleGridControlSet.GetList(where);
                brdv.success = true;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    List<long> ids = new List<long>();
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridControlSet>(ds.Tables[0]);
                    foreach (var item in list)
                    {
                        ids.Add(item.GridControlID);
                    }
                    BLL.BModuleGridControlList moduleGridControlList = new BLL.BModuleGridControlList();
                    DataSet gridControlList = moduleGridControlList.GetListSort(" GridControlID in (" + string.Join(",", ids) + ")", sort);
                    var controlList = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleGridControlList>(gridControlList.Tables[0]);
                    //var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    for (var i=0;i< controlList.Count;i++)
                    {
                        foreach (var controlSetItem in list)
                        {
                            if (controlList[i].GridControlID== controlSetItem.GridControlID)
                            {
                                controlList[i].ColName = controlSetItem.ColName;
                                controlList[i].IsOrder = controlSetItem.IsOrder;
                                controlList[i].IsHide = controlSetItem.IsHide;
                                controlList[i].DispOrder = controlSetItem.DispOrder;
                                controlList[i].IsUse = controlSetItem.IsUse;
                                controlList[i].Width = controlSetItem.Width;
                                break;
                            }
                        }
                    }
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(controlList, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "GetBModuleGridControlSetList" + ex.ToString();
                ZhiFang.Common.Log.Log.Info("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleGridControlSetList:" + ex.ToString());
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetBModuleFormControlSetList(string where, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.BModuleFormControlSet moduleFormControlSet = new BLL.BModuleFormControlSet();
            brdv.success = true;
            
            try
            {
                if (string.IsNullOrWhiteSpace(where))
                {
                    where = "1=1";
                }
                DataSet ds = moduleFormControlSet.GetList(where);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<long> ids = new List<long>();
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleFormControlSet>(ds.Tables[0]);
                    foreach (var item in list)
                    {
                        ids.Add(item.FormControlID);
                    }
                    BLL.BModuleFormControlList moduleFormControlList = new BLL.BModuleFormControlList();
                    //JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    DataSet formControlList = moduleFormControlList.GetListSort(" FormControlID in (" + string.Join(",", ids) + ")", sort);
                    var controlList = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.BModuleFormControlList>(formControlList.Tables[0]);
                    //var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    for (var i = 0; i < controlList.Count; i++)
                    {
                        foreach (var controlSetItem in list)
                        {
                            if (controlList[i].FormControlID == controlSetItem.FormControlID)
                            {
                                controlList[i].DefaultValue = controlSetItem.DefaultValue;
                                controlList[i].Label = controlSetItem.Label;
                                controlList[i].DispOrder = controlSetItem.DispOrder;
                                controlList[i].IsUse = controlSetItem.IsUse;
                                controlList[i].IsDisplay = controlSetItem.IsDisplay;
                                controlList[i].IsReadOnly = controlSetItem.IsReadOnly;
                                break;
                            }
                        }
                    }
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(controlList, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.DictionaryService.GetBModuleFormControlSetList:" + e.ToString());
            }
            return brdv;
        }

        public Model.BaseResultDataValue LabStarGetPGroup(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BPGroup bpgroup = new BLL.BPGroup();
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        if (string.IsNullOrEmpty(Where))
                        {
                            Where += "LabID=" + labid;
                        }
                        else
                        {
                            Where += " and LabID=" + labid;
                        }
                    }
                }
                DataSet deptlist = bpgroup.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<PGroup>(deptlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarGetPGroup:" + ex.ToString());
                brdv.ErrorInfo = "LabStarGetPGroup:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue LabStarGetSectionPrintList(string SectionNo, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BSectionPrint bsp = new BSectionPrint();
            brdv.success = false;
            if (page <= 0 || limit <= 0)
            {
                brdv.ErrorInfo = "分页参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                DataSet ds = null;
                string where = "";
                if (null != SectionNo && !("").Equals(SectionNo))
                {
                    ds = bsp.GetSectionPgroupList(where+="a.SectionNo=" + SectionNo);
                }
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        if (string.IsNullOrEmpty(where))
                        {
                            where += "a.LabID="+ labid;
                        }
                        else
                        {
                            where += " and a.LabID="+ labid;
                        }
                    }
                }
                ds = bsp.GetSectionPgroupList(where);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    var list = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SelectSectionPrint>(ds.Tables[0]);
                    var result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination(page, limit, list);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"count\":" + list.Count + ",\"list\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue LabStarGetSickType(string Where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BSickType bst = new BLL.BSickType();
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        if (string.IsNullOrEmpty(Where))
                        {
                            Where += "LabID=" + labid;
                        }
                        else
                        {
                            Where += " and LabID=" + labid;
                        }
                    }
                }
                DataSet sickTypelist = bst.GetList(Where);
                if (sickTypelist != null && sickTypelist.Tables != null && sickTypelist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<SickType>(sickTypelist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    //brdv.ResultDataValue = aa;

                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarGetSickType:" + ex.ToString());
                brdv.ErrorInfo = "LabStarGetSickType:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue LabStarGetDistrict(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {

                ZhiFang.ReportFormQueryPrint.BLL.BDistrict cht = new BLL.BDistrict();
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        if (string.IsNullOrEmpty(Where))
                        {
                            Where += "LabID=" + labid;
                        }
                        else
                        {
                            Where += " and LabID=" + labid;
                        }
                    }
                }
                DataSet Districtlist = cht.GetList(Where);
                if (Districtlist != null && Districtlist.Tables != null && Districtlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Model.ChargeType>(Districtlist.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                }
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarGetDistrict:" + ex.ToString());
                brdv.ErrorInfo = "LabStarGetDistrict:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }
        public Model.BaseResultDataValue LabStarGetDeptListPaging(string Where, string fields, int page, int limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (fields == null && fields.Trim() == "")
                {
                    brdv.ErrorInfo = "字段参数错误!";
                    brdv.success = false;
                    return brdv;
                }
                if (page <= 0 || limit <= 0)
                {
                    brdv.ErrorInfo = "分页参数错误！";
                    brdv.success = false;
                    return brdv;
                }
                ZhiFang.ReportFormQueryPrint.BLL.BDepartment bdept = new BLL.BDepartment();
                //DataSet deptlist = bdept.GetList(Where, page, limit);
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        if (string.IsNullOrEmpty(Where))
                        {
                            Where += "LabID=" + labid;
                        }
                        else
                        {
                            Where += " and LabID=" + labid;
                        }
                    }
                }
                DataSet deptlist = bdept.GetList(Where);
                if (deptlist != null && deptlist.Tables != null && deptlist.Tables.Count > 0)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<DepartmentVO>(deptlist.Tables[0]);
                    Log.Debug(Result.Count.ToString() + "   || " + deptlist.Tables[0].Rows.Count.ToString());
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);

                    brdv.ResultDataValue = "{\"total\":" + Result.Count + ",\"rows\":" + aa + "}";
                    brdv.success = true;
                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetDeptList:" + ex.ToString());
                brdv.ErrorInfo = "GetDeptList:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetConfigLabStarUrl()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            
            try
            {

                string url = ConfigHelper.GetConfigString("LabStarUrl");
                brdv.success = true;
                brdv.ResultDataValue = url;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetConfigLabStarUrl:" + ex.ToString());
                brdv.ErrorInfo = "GetConfigLabStarUrl:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }
    }
}
