using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Reflection;
using System.Web;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;

namespace ZhiFang.ServiceCommon.RBAC
{
    public class ConstructionServiceCommon
    {
        #region 服务成员
        protected virtual string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }
        protected virtual IList<string> InterfaceList { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDAppComponents IBBTDAppComponents { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDPictureTypeCon IBBTDPictureTypeCon { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDPictureType IBBTDPictureType { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDModuleType IBBTDModuleType { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDAppPicture IBBTDAppPicture { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDMacroCommand IBBTDMacroCommand { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBBTDAppComponentsOperate IBBTDAppComponentsOperate { get; set; }
        public virtual ZhiFang.IBLL.RBAC.IBRBACModuleOper IBRBACModuleOper { get; set; }
        #endregion

        #region 构建业务服务
        /// <summary>
        /// 获取数据对象列表
        /// 此服务只获取ZhiFang.Digitlab.Entity和ZhiFang.Digitlab.Entity.BusinessAnalysis对象列表
        /// 此服务获取包含有DataDescAttribute(DataDesc)声明的数据对象的列表
        /// </summary>
        /// <returns>数据对象(BaseResultEntityClassInfo)列表的Json字符串</returns>
        public BaseResultDataValue CS_BA_GetEntityList()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<BaseResultEntityClassInfo> ilistbreci = new List<BaseResultEntityClassInfo>();
                // Assembly.Load("ZhiFang.Digitlab.Entity").GetTypes()获取ZhiFang.Digitlab.Entity名空间中的对象类型列表
                foreach (var a in Assembly.Load("ZhiFang.Entity.RBAC").GetTypes())
                {
                    BaseResultEntityClassInfo breci = new BaseResultEntityClassInfo();
                    breci.EName = a.Name;
                    foreach (var ta in a.GetCustomAttributes(true))
                    {
                        if (ta.GetType() == typeof(DataDescAttribute))
                        {
                            DataDescAttribute tat = (DataDescAttribute)ta;
                            if (tat.CName != null)
                            {
                                breci.CName = tat.CName;
                            }
                            if (tat.ClassCName != null)
                            {
                                breci.ClassName = a.Name;
                            }
                            if (tat.ShortCode != null)
                            {
                                breci.ShortCode = tat.ShortCode;
                            }
                            if (tat.Desc != null)
                            {
                                breci.Description = tat.Desc;
                            }
                        }
                    }
                    if (breci.CName != null)
                    {
                        ilistbreci.Add(breci);
                    }
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace").Trim() != "")
                {
                    foreach (var a in Assembly.Load(ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace")).GetTypes())
                    {
                        BaseResultEntityClassInfo breci = new BaseResultEntityClassInfo();
                        breci.EName = a.Name;
                        foreach (var ta in a.GetCustomAttributes(true))
                        {
                            if (ta.GetType() == typeof(DataDescAttribute))
                            {
                                DataDescAttribute tat = (DataDescAttribute)ta;
                                if (tat.CName != null)
                                {
                                    breci.CName = tat.CName;
                                }
                                if (tat.ClassCName != null)
                                {
                                    breci.ClassName = a.Name;
                                }
                                if (tat.ShortCode != null)
                                {
                                    breci.ShortCode = tat.ShortCode;
                                }
                                if (tat.Desc != null)
                                {
                                    breci.Description = tat.Desc;
                                }
                            }
                        }
                        if (breci.CName != null)
                        {
                            ilistbreci.Add(breci);
                        }
                    }
                }
                string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(ilistbreci);
                brdv.ResultDataValue = result;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }
        /// <summary>
        /// 数据对象结构树
        /// </summary>
        /// <param name="EntityName">数据对象类型名</param>
        /// <returns>数据对象树(EntityFrameTree)的Json字符串</returns>
        public BaseResultDataValue CS_BA_GetEntityFrameTree(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string tmpEntityName = EntityName;
            try
            {
                if (!string.IsNullOrEmpty(EntityName))
                {
                    List<EntityFrameTree> eftl = new List<EntityFrameTree>();
                    eftl = this.GetEntityFrameTree(EntityName);
                    string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(eftl);
                    brdv.ResultDataValue = result;
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }
        /// <summary>
        /// 根据模块ID获取预定义属性，如果没有预定义属性，则根据数据对象类型名获取(数据对象结构树)
        /// </summary>
        /// <param name="ModuleOperID">模块ID</param>
        /// <param name="EntityName">数据对象类型名</param>
        /// <returns>数据对象树(EntityFrameTree)的Json字符串</returns>
        public BaseResultDataValue CS_BA_GetEntityFrameTreeByModuleOperID(long ModuleOperID, string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string tmpEntityName = EntityName;
            try
            {
                RBACModuleOper tmpRBACModuleOper = IBRBACModuleOper.Get(ModuleOperID);
                if (tmpRBACModuleOper != null && tmpRBACModuleOper.PredefinedField != null && tmpRBACModuleOper.PredefinedField.Trim() != "")
                {
                    brdv.ResultDataValue = tmpRBACModuleOper.PredefinedField;
                }
                else
                {
                    if (!string.IsNullOrEmpty(EntityName))
                    {
                        List<EntityFrameTree> eftl = new List<EntityFrameTree>();
                        eftl = this.GetEntityFrameTree(EntityName);
                        string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(eftl);
                        brdv.ResultDataValue = result;
                    }
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }
        /// <summary>
        /// 查询数据对象服务列表
        /// 按照服务声明(ServiceContractDescription)中类型名称(ReturnType)查找
        /// </summary>
        /// <param name="EntityName">数据对象类型名称</param>
        /// <returns></returns>
        public BaseResultDataValue CS_BA_SearchReturnEntityServiceListByEntityName(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<ServiceContractnfo> lsci = new List<ServiceContractnfo>();
                lsci = this.GetReturnEntityServiceList(EntityName);
                string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lsci);
                brdv.ResultDataValue = result;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
                Log.Info("异常 : " + e.ToString());
            }

            return brdv;
        }

        public List<ServiceContractnfo> GetReturnEntityServiceList(string EntityName)
        {
            List<ServiceContractnfo> lsci = new List<ServiceContractnfo>();
            if (EntityName.IndexOf('_') > 0)
            {
                string[] entity = EntityName.Split('_');
                string entitynamespace = "ZhiFang.Entity.RBAC";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
                {
                    entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
                }
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + entity[0]);
                if (t == null)
                {
                    t = Assembly.Load("ZhiFang.Entity.RBAC").GetType("ZhiFang.Entity.RBAC." + entity[0]);
                }
                for (int i = 1; i < entity.Length; i++)
                {
                    t = t.GetProperty(entity[i]).PropertyType;
                }
                EntityName = t.Name;
            }
            foreach (var a in Assembly.Load(AssemblyName).GetTypes())
            {
                //Log.Warn("类名 : " + a.Name);
                if (InterfaceList.IndexOf(a.Name) <= 0)
                    continue;
                MethodInfo[] methodsInfo = a.GetMethods();
                foreach (var method in methodsInfo)
                {
                    //Log.Warn("方法名 : " + method.Name);
                    foreach (var methodattribute in method.GetCustomAttributes(false))
                    {
                        //Log.Warn("方法属性名 : " + methodattribute.ToString());
                        if (methodattribute.ToString() == "System.ComponentModel.DescriptionAttribute")
                        {
                            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)methodattribute;
                            string description = da.Description;
                            ServiceContractDescription scd = JsonConvert.DeserializeObject<ServiceContractDescription>(description);
                            if (scd.ReturnType == EntityName || (EntityName.Trim().ToLower() == "chart" && scd.ReturnType.ToLower().IndexOf(EntityName.ToLower()) >= 0))
                            {
                                ServiceContractnfo sci = new ServiceContractnfo();
                                sci.CName = scd.Name;
                                sci.Description = scd.Desc;
                                sci.ServerUrl = scd.Url;
                                sci.EName = method.Name;
                                lsci.Add(sci);
                            }
                        }
                        if (methodattribute.ToString() == Assembly.GetExecutingAssembly().GetName().Name + ".ServiceContractDescriptionAttribute")
                        {
                            ServiceContractDescriptionAttribute da = (ServiceContractDescriptionAttribute)methodattribute;
                            if (da.ReturnType == EntityName || (EntityName.Trim().ToLower() == "chart" && da.ReturnType.ToLower().IndexOf(EntityName.ToLower()) >= 0))
                            {
                                ServiceContractnfo sci = new ServiceContractnfo();
                                sci.CName = da.Name;
                                sci.Description = da.Desc;
                                sci.ServerUrl = da.Url;
                                sci.EName = method.Name;
                                lsci.Add(sci);
                            }
                        }
                    }
                }
            }
            return lsci;
        }

        public BaseResultDataValue CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName(string EntityPropertynName)
        {
            try
            {
                if (EntityPropertynName.IndexOf('_') >= 0 && EntityPropertynName.LastIndexOf('_') < EntityPropertynName.Length - 1)
                {
                    if (EntityPropertynName.IndexOf('_') > 0)
                    {
                        string[] entity = EntityPropertynName.Split('_');
                        string entitynamespace = "ZhiFang.Entity.RBAC";
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
                        {
                            entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
                        }
                        Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + entity[0]);
                        if (t == null)
                        {
                            t = Assembly.Load("ZhiFang.Entity.RBAC").GetType("ZhiFang.Entity.RBAC." + entity[0]);
                        }
                        for (int i = 1; i < entity.Length - 1; i++)
                        {
                            t = t.GetProperty(entity[i]).PropertyType;
                        }
                        EntityPropertynName = "List" + t.Name;
                    }
                    return CS_BA_SearchReturnEntityServiceListByEntityName(EntityPropertynName);
                }
                else
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数格式错误！参数：" + EntityPropertynName, success = false };
                }
            }
            catch (Exception e)
            {
                return new BaseResultDataValue() { ErrorInfo = "函数异常！异常信息：" + e.ToString(), success = false };
            }
        }
        /// <summary>
        /// 查询使用数据对象为参数的服务列表
        /// 按照服务声明的参数查找，如果参数中包含Entity对象参数，则获取此服务
        /// </summary>
        /// <param name="EntityName">数据对象类型名称</param>
        /// <returns></returns>
        public BaseResultDataValue CS_BA_SearchParaEntityServiceListByEntityName(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<ServiceContractnfo> lsci = new List<ServiceContractnfo>();
                lsci = SearchParaEntityServiceListByEntityName(EntityName);
                string result = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lsci);
                brdv.ResultDataValue = result;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }

        private List<ServiceContractnfo> SearchParaEntityServiceListByEntityName(string EntityName)
        {
            List<ServiceContractnfo> lsci = new List<ServiceContractnfo>();
            if (EntityName.IndexOf('_') > 0)
            {
                string[] entity = EntityName.Split('_');
                string entitynamespace = "ZhiFang.Entity.RBAC";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
                {
                    entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
                }
                Type t = Assembly.Load(entitynamespace).GetType(entity[0]);
                if (t == null)
                {
                    t = Assembly.Load("ZhiFang.Entity.RBAC").GetType(entity[0]);
                }
                for (int i = 1; i < entity.Length; i++)
                {
                    t = t.GetProperty(entity[i]).PropertyType;
                }
                EntityName = t.Name;
            }
            foreach (var a in Assembly.Load(AssemblyName).GetTypes())
            {
                if (InterfaceList.IndexOf(a.Name) <= 0)
                    continue;
                MethodInfo[] methodsInfo = a.GetMethods();
                foreach (var method in methodsInfo)
                {
                    var plist = method.GetParameters();//.Where(p => p.GetType().Name == EntityName);
                    if (plist.Count() > 0 && plist[0].ParameterType.Name == EntityName)
                    {
                        foreach (var methodattribute in method.GetCustomAttributes(false))
                        {

                            if (methodattribute.ToString() == "System.ComponentModel.DescriptionAttribute")
                            {
                                System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)methodattribute;
                                string description = da.Description;
                                ServiceContractDescription scd = JsonConvert.DeserializeObject<ServiceContractDescription>(description);
                                //if (scd.ReturnType == EntityName)
                                //{
                                ServiceContractnfo sci = new ServiceContractnfo();
                                sci.CName = scd.Name;
                                sci.Description = scd.Desc;
                                sci.ServerUrl = scd.Url;
                                sci.EName = method.Name;
                                lsci.Add(sci);
                                //}
                            }
                            if (methodattribute.ToString() == Assembly.GetExecutingAssembly().GetName().Name + ".ServiceContractDescriptionAttribute")
                            {
                                ServiceContractDescriptionAttribute da = (ServiceContractDescriptionAttribute)methodattribute;
                                ServiceContractnfo sci = new ServiceContractnfo();
                                sci.CName = da.Name;
                                sci.Description = da.Desc;
                                sci.ServerUrl = da.Url;
                                sci.EName = method.Name;
                                lsci.Add(sci);
                            }
                        }
                    }
                }
            }
            return lsci;
        }

        /// <summary>
        /// 获取简单字典数据的CRUD服务和简单字典数据的对象结构
        /// </summary>
        /// <returns>数据对象(BaseResultEntityClassInfo)列表的Json字符串</returns>
        public BaseResultDataValue CS_BA_GetCRUDAndFrameByEntityName(string EntityName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            DicEntityInfo dicinfo = new DicEntityInfo();
            try
            {
                dicinfo.EntityFrameTree = this.GetEntityFrameTree(EntityName);
                List<ServiceContractnfo> lscilist = new List<ServiceContractnfo>();
                lscilist = GetReturnEntityServiceList("List" + EntityName);
                for (int i = 0; i < lscilist.Count; i++)
                {
                    if (lscilist[i].CName.IndexOf("查询") >= 0 && lscilist[i].CName.IndexOf("(HQL)") > 0)
                    {
                        dicinfo.RetrieveServiceAddress = lscilist[i].ServerUrl;
                    }
                }
                List<ServiceContractnfo> lsci = new List<ServiceContractnfo>();
                lsci = SearchParaEntityServiceListByEntityName(EntityName);
                for (int i = 0; i < lsci.Count; i++)
                {
                    if (lsci[i].CName.IndexOf("新增") >= 0)
                    {
                        dicinfo.CreatServiceAddress = lsci[i].ServerUrl;
                    }
                    if (lsci[i].CName.IndexOf("修改") >= 0 && lsci[i].CName.IndexOf("指定的属性") >= 0)
                    {
                        dicinfo.UpdateServiceAddress = lsci[i].ServerUrl;
                    }
                }
                List<ServiceContractnfo> lscidel = new List<ServiceContractnfo>();
                lscidel = GetReturnEntityServiceList("Bool");
                for (int i = 0; i < lscidel.Count; i++)
                {
                    if (lscidel[i].ServerUrl.IndexOf("_Del" + EntityName + "?id={id}") > 0)
                    {
                        dicinfo.DeleteServiceAddress = lscidel[i].ServerUrl;
                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");

                string result = tempParseObjectProperty.GetObjectPropertyNoPlanish(dicinfo);
                brdv.ResultDataValue = result;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }
            return brdv;
        }

        public BaseResultDataValue ReceiveFileService()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "false";
                    brdv.success = false;
                    return brdv;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/upload/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string filepath = Path.Combine(parentPath, Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    brdv.ResultDataValue = filepath;
                }
                else
                {
                    brdv.ErrorInfo = "文件大小为0或为空！";
                    brdv.success = false;
                    return brdv;
                }
                return brdv;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message;
                brdv.success = false;
                return brdv;
            }
        }
        /// <summary>
        /// 从HttpContext.Current.Request.Files中获取代码文件CODEFILE并把其内容转换为字符串
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue ReceiveFileServiceEx()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    tempBaseResultDataValue.ErrorInfo = "未检测到文件！";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    if (HttpContext.Current.Request.Files.AllKeys[i].ToUpper() == "CODEFILE")
                    {
                        HttpPostedFile tempFile = HttpContext.Current.Request.Files[i];
                        int len = tempFile.ContentLength;
                        if (len > 0 && !string.IsNullOrEmpty(tempFile.FileName))
                        {
                            tempBaseResultDataValue.ResultDataValue = FilesHelper.ReadFileContent(tempFile.InputStream);
                        }
                        else
                        {
                            tempBaseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                            tempBaseResultDataValue.success = false;
                        }
                    }
                }
                return tempBaseResultDataValue;
            }
            catch (Exception e)
            {
                tempBaseResultDataValue.ErrorInfo = e.Message;
                tempBaseResultDataValue.ResultDataValue = "";
                tempBaseResultDataValue.success = false;
                return tempBaseResultDataValue;
            }
        }

        public virtual BaseResultDataValue GetPinYin(string chinese)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (chinese != null && chinese.Length > 0)
                {
                    char[] tmpstr = chinese.ToCharArray();
                    foreach (char a in tmpstr)
                    {
                        brdv.ResultDataValue += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
                        //PinYinConverter.
                    }
                    brdv.success = true;
                    return brdv;
                }
                else
                {
                    brdv.ErrorInfo = "字符串格式不正确！";
                    brdv.success = false;
                    return brdv;
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message;
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue ReceiveModuleIconService()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "false";
                    brdv.success = false;
                    return brdv;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string filetype = Path.GetExtension(file.FileName).ToUpper();
                    if (filetype == ".JPG" || filetype == ".JPEG" || filetype == ".BMP" || filetype == ".PNG")
                    {
                        if (ZhiFang.Common.Public.ImageHelp.IconCheck(file.InputStream, 64, 64))
                        {
                            string imagename = ZhiFang.Common.Public.GUIDHelp.GetGUIDString() + filetype;
                            string parentPath = HttpContext.Current.Server.MapPath("~/Images/Icons/64");
                            if (!Directory.Exists(parentPath))
                            {
                                Directory.CreateDirectory(parentPath);
                            }
                            string filepath = Path.Combine(parentPath, imagename);
                            file.SaveAs(filepath);

                            parentPath = HttpContext.Current.Server.MapPath("~/Images/Icons/48");
                            if (!Directory.Exists(parentPath))
                            {
                                Directory.CreateDirectory(parentPath);
                            }
                            ZhiFang.Common.Public.ImageHelp.CutForCustom(file.InputStream, Path.Combine(parentPath, imagename), 48, 48, 100);

                            parentPath = HttpContext.Current.Server.MapPath("~/Images/Icons/32");
                            if (!Directory.Exists(parentPath))
                            {
                                Directory.CreateDirectory(parentPath);
                            }
                            ZhiFang.Common.Public.ImageHelp.CutForCustom(file.InputStream, Path.Combine(parentPath, imagename), 32, 32, 100);

                            parentPath = HttpContext.Current.Server.MapPath("~/Images/Icons/16");
                            if (!Directory.Exists(parentPath))
                            {
                                Directory.CreateDirectory(parentPath);
                            }
                            ZhiFang.Common.Public.ImageHelp.CutForCustom(file.InputStream, Path.Combine(parentPath, imagename), 16, 16, 100);

                            brdv.ResultDataValue = imagename;
                        }
                        else
                        {
                            brdv.ErrorInfo = "图片尺寸不正确！请上传64*64的图片文件！";
                            brdv.success = false;
                            return brdv;
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "图片类型不正确！目前只支持jpg、jpeg、bmp、png类型的图片！";
                        brdv.success = false;
                        return brdv;
                    }
                }
                else
                {
                    brdv.ErrorInfo = "图片大小为0或为空！";
                    brdv.success = false;
                    return brdv;
                }
                return brdv;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message;
                brdv.success = false;
                return brdv;
            }
        }
        /// <summary>
        /// 返回实体对象的结构树
        /// </summary>
        /// <param name="EntityName">实体名称</param>
        /// <returns>ListEntityFrameTree</returns>
        public List<EntityFrameTree> GetEntityFrameTree(string EntityName)
        {
            List<EntityFrameTree> eftl = new List<EntityFrameTree>();
            string entitynamespace = "ZhiFang.Entity.RBAC";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace") != "")
            {
                entitynamespace = ZhiFang.Common.Public.ConfigHelper.GetConfigString("EntityNamespace");
            }
            string tmpEntityName = EntityName;
            if (!string.IsNullOrEmpty(EntityName))
            {
                if (EntityName.IndexOf('_') > 0)
                {
                    string[] entity = EntityName.Split('_');
                    Type tmpt = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + entity[0]);
                    if (tmpt == null)
                    {
                        tmpt = Assembly.Load("ZhiFang.Entity.RBAC").GetType("ZhiFang.Entity.RBAC." + entity[0]);
                    }
                    for (int i = 1; i < entity.Length; i++)
                    {
                        tmpt = tmpt.GetProperty(entity[i]).PropertyType;
                    }
                    EntityName = tmpt.Name;
                }
                Type t = Assembly.Load(entitynamespace).GetType(entitynamespace + "." + EntityName);
                if (t == null)
                {
                    t = Assembly.Load("ZhiFang.Entity.RBAC").GetType("ZhiFang.Entity.RBAC." + EntityName);
                }
                Type[] ta = Assembly.Load("ZhiFang.Entity.RBAC").GetTypes();
                Type[] tab = Assembly.Load(entitynamespace).GetTypes();


                //判断属性是否同时定义或声明了DataContractAttribute和DataDescAttribute，如果是，则添加该属性。
                if (t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataContractAttribute)) > 0 && t.GetCustomAttributes(false).Count(a => a.GetType() == typeof(DataDescAttribute)) > 0)
                {
                    PropertyInfo[] pia = t.GetProperties();
                    foreach (var p in pia)
                    {
                        EntityFrameTree eft = new EntityFrameTree();
                        eft.InteractionField = tmpEntityName + '_' + p.Name;
                        eft.FieldClass = p.PropertyType.Name;
                        foreach (var pattribute in p.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                DataDescAttribute da = (DataDescAttribute)pattribute;
                                eft.text = da.CName;
                                //eft.Checked = false;
                            }
                        }
                        //属性类型是否存在ZhiFang.Digitlab.Entity命名空间下的实体对象列表中，如果不存在则为叶子节点
                        if (ta.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                        {
                            //属性类型是否存在ZhiFang.Digitlab.Entity.BusinessAnalysis命名空间下的实体对象列表中，如果不存在则为叶子节点
                            if (tab.Where<Type>(a => a == p.PropertyType).Count() <= 0)
                            {
                                eft.leaf = true;
                            }
                            else
                            {
                                eft.leaf = false;
                            }
                        }
                        else
                        {
                            eft.leaf = false;
                        }
                        //过滤列表
                        if (p.PropertyType.Name != "IList`1")
                        {
                            eftl.Add(eft);
                        }
                    }
                }

            }
            return eftl;
        }

        #region BTDMacroCommand宏命令

        public BaseResultDataValue CS_UDTO_SearchBTDMacroCommandByHQL(int page, int limit, string fields, string where, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDMacroCommand> tempEntityList = new EntityList<BTDMacroCommand>();
            try
            {
                Dictionary<string, BTDMacroCommand> a = IBBTDMacroCommand.Search();
                tempEntityList.list = new List<BTDMacroCommand>();
                for (int i = 0; i < a.Count; i++)
                {
                    tempEntityList.list.Add(a.ElementAt(i).Value);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (tempEntityList.count == 0 && tempEntityList.list != null)
                    tempEntityList.count = tempEntityList.list.Count;
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDMacroCommand>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDMacroCommandByKey(string key, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {

                var tempEntity = IBBTDMacroCommand.Search().Where(a => a.Key == key).ElementAt(0).Value;
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDMacroCommand>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion
        #endregion

        #region 构建单表服务

        #region 组件

        public BaseResultDataValue CS_UDTO_AddBTDAppComponents()
        {
            //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            //Log.Info(entity.DesignCode);
            BaseResultDataValue baseResule = new BaseResultDataValue();
            try
            {
                BTDAppComponents entity = new BTDAppComponents();
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "AppType":
                            if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                            {
                                entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                            }
                            break;
                        case "BuildType":
                            if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                            {
                                entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                            }
                            break;
                        case "ClassCode":
                            entity.ClassCode = HttpContext.Current.Request.Form["ClassCode"];
                            break;
                        case "CName":
                            entity.CName = HttpContext.Current.Request.Form["CName"];
                            break;
                        case "Creator":
                            entity.Creator = HttpContext.Current.Request.Form["Creator"];
                            break;
                        case "DataAddTime":
                            if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                            {
                                entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                            }
                            break;
                        case "DataUpdateTime":
                            if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                            {
                                entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                            }
                            break;
                        case "DesignCode":
                            entity.DesignCode = HttpContext.Current.Request.Form["DesignCode"];
                            break;
                        case "EName":
                            entity.EName = HttpContext.Current.Request.Form["EName"];
                            break;
                        case "ExecuteCode":
                            entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                            break;
                        case "InitParameter":
                            entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                            break;
                        case "Modifier":
                            entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                            break;
                        case "ModuleOperCode":
                            entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                            break;
                        case "ModuleOperInfo":
                            entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                            break;
                        case "PinYinZiTou":
                            entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                            break;
                        case "BTDAppComponentsRefList":
                            string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                            //JavaScriptSerializer serializer = new JavaScriptSerializer();
                            //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                            break;
                        case "BTDAppComponentsOperateList":
                            string BTDAppComponentsOperateList = HttpContext.Current.Request.Form["BTDAppComponentsOperateList"];
                            //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                            //entity.BTDAppComponentsOperateList = serializer1.Deserialize<List<BTDAppComponentsOperate>>(BTDAppComponentsOperateList);
                            break;
                        case "DataTimeStamp":
                            if (HttpContext.Current.Request.Form["DataTimeStamp"] != null && HttpContext.Current.Request.Form["DataTimeStamp"].Trim() != "")
                            {
                                string[] tmpdts = HttpContext.Current.Request.Form["DataTimeStamp"].Split(',');
                                byte[] tmpbyte = new byte[tmpdts.Length];
                                for (int j = 0; j < tmpbyte.Length; j++)
                                {
                                    tmpbyte[j] = Convert.ToByte(tmpdts[j].Replace("[", "").Replace("]", ""));
                                }
                                entity.DataTimeStamp = tmpbyte;
                            }
                            break;
                    }
                }

                entity.Id = Common.Public.GUIDHelp.GetGUIDLong();
                IBBTDAppComponents.Entity = entity;
                //baseResule.success = IBBTDAppComponents.Add();
                baseResule.success = IBBTDAppComponents.Add(entity.BTDAppComponentsRefList);
                baseResule.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDAppComponents.Entity);
            }
            catch (Exception e)
            {
                baseResule.ErrorInfo = e.Message;
                baseResule.success = false;
            }
            return baseResule;
        }

        public BaseResultDataValue CS_UDTO_AddBTDAppComponentsAndSaveJSFile()
        {
            //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            //Log.Info(entity.DesignCode);
            BaseResultDataValue baseResule = new BaseResultDataValue();
            try
            {
                BTDAppComponents entity = new BTDAppComponents();
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "AppType":
                            if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                            {
                                entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                            }
                            break;
                        case "BuildType":
                            if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                            {
                                entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                            }
                            break;
                        case "ClassCode":
                            entity.ClassCode = HttpContext.Current.Request.Form["ClassCode"];
                            break;
                        case "CName":
                            entity.CName = HttpContext.Current.Request.Form["CName"];
                            break;
                        case "Creator":
                            entity.Creator = HttpContext.Current.Request.Form["Creator"];
                            break;
                        case "DataAddTime":
                            if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                            {
                                entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                            }
                            break;
                        case "DataUpdateTime":
                            if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                            {
                                entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                            }
                            break;
                        case "DesignCode":
                            entity.DesignCode = HttpContext.Current.Request.Form["DesignCode"];
                            break;
                        case "EName":
                            entity.EName = HttpContext.Current.Request.Form["EName"];
                            break;
                        case "ExecuteCode":
                            entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                            break;
                        case "InitParameter":
                            entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                            break;
                        case "Modifier":
                            entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                            break;
                        case "ModuleOperCode":
                            entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                            break;
                        case "ModuleOperInfo":
                            entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                            break;
                        case "PinYinZiTou":
                            entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                            break;
                        case "BTDAppComponentsRefList":
                            string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                            //JavaScriptSerializer serializer = new JavaScriptSerializer();
                            //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                            break;
                        case "BTDAppComponentsOperateList":
                            string BTDAppComponentsOperateList = HttpContext.Current.Request.Form["BTDAppComponentsOperateList"];
                            //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                            //entity.BTDAppComponentsOperateList = serializer1.Deserialize<List<BTDAppComponentsOperate>>(BTDAppComponentsOperateList);
                            break;
                        case "DataTimeStamp":
                            if (HttpContext.Current.Request.Form["DataTimeStamp"] != null && HttpContext.Current.Request.Form["DataTimeStamp"].Trim() != "")
                            {
                                string[] tmpdts = HttpContext.Current.Request.Form["DataTimeStamp"].Split(',');
                                byte[] tmpbyte = new byte[tmpdts.Length];
                                for (int j = 0; j < tmpbyte.Length; j++)
                                {
                                    tmpbyte[j] = Convert.ToByte(tmpdts[j].Replace("[", "").Replace("]", ""));
                                }
                                entity.DataTimeStamp = tmpbyte;
                            }
                            break;
                    }
                }

                entity.Id = Common.Public.GUIDHelp.GetGUIDLong();
                IBBTDAppComponents.Entity = entity;
                //baseResule.success = IBBTDAppComponents.Add();
                baseResule.success = IBBTDAppComponents.AddAndJSFile(entity.BTDAppComponentsRefList);
                baseResule.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDAppComponents.Entity);
            }
            catch (Exception e)
            {
                baseResule.ErrorInfo = e.Message;
                baseResule.success = false;
            }
            return baseResule;
        }

        public BaseResultBool CS_UDTO_UpdateBTDAppComponents()
        {
            BTDAppComponents entity = new BTDAppComponents();
            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "Id":
                        if (HttpContext.Current.Request.Form["Id"].Trim() != "")
                        {
                            entity.Id = Convert.ToInt64(HttpContext.Current.Request.Form["Id"]);
                        }
                        break;
                    case "AppType":
                        if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                        {
                            entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                        }
                        break;
                    case "BuildType":
                        if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                        {
                            entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                        }
                        break;
                    case "ClassCode":
                        entity.ClassCode = HttpContext.Current.Request.Form["ClassCode"];
                        break;
                    case "CName":
                        entity.CName = HttpContext.Current.Request.Form["CName"];
                        break;
                    case "Creator":
                        entity.Creator = HttpContext.Current.Request.Form["Creator"];
                        break;
                    case "DataAddTime":
                        if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                        {
                            entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                        }
                        break;
                    case "DataUpdateTime":
                        if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                        {
                            entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                        }
                        break;
                    case "DesignCode":
                        entity.DesignCode = HttpContext.Current.Request.Form["DesignCode"];
                        break;
                    case "EName":
                        entity.EName = HttpContext.Current.Request.Form["EName"];
                        break;
                    case "ExecuteCode":
                        entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                        break;
                    case "InitParameter":
                        entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                        break;
                    case "Modifier":
                        entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                        break;
                    case "ModuleOperCode":
                        entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                        break;
                    case "ModuleOperInfo":
                        entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                        break;
                    case "PinYinZiTou":
                        entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                        break;
                    case "BTDAppComponentsRefList":
                        string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                        //JavaScriptSerializer serializer = new JavaScriptSerializer();
                        //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                        break;
                    case "BTDAppComponentsOperateList":
                        string BTDAppComponentsOperateList = HttpContext.Current.Request.Form["BTDAppComponentsOperateList"];
                        //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                        //entity.BTDAppComponentsOperateList = serializer1.Deserialize<List<BTDAppComponentsOperate>>(BTDAppComponentsOperateList);
                        break;
                    case "DataTimeStamp":
                        if (HttpContext.Current.Request.Form["DataTimeStamp"] != null && HttpContext.Current.Request.Form["DataTimeStamp"].Trim() != "")
                        {
                            string[] tmpdts = HttpContext.Current.Request.Form["DataTimeStamp"].Split(',');
                            byte[] tmpbyte = new byte[tmpdts.Length];
                            for (int j = 0; j < tmpbyte.Length; j++)
                            {
                                tmpbyte[j] = Convert.ToByte(tmpdts[j].Replace("[", "").Replace("]", ""));
                            }
                            entity.DataTimeStamp = tmpbyte;
                        }
                        break;
                }
            }
            IBBTDAppComponents.Entity = entity;
            BaseResultBool baseResule = new BaseResultBool();
            baseResule.success = IBBTDAppComponents.Edit();
            //baseResule.success = IBBTDAppComponents.Edit(entity.BTDAppComponentsRefList);
            return baseResule;
        }

        public BaseResultBool CS_UDTO_UpdateBTDAppComponentsAndUpdateJSFile()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string fields = "";
                string ClassCode = "";
                string DesignCode = "";
                if (HttpContext.Current.Request.Form["UpdateFields"] != null && HttpContext.Current.Request.Form["UpdateFields"].Trim() != "")
                {
                    fields = HttpContext.Current.Request.Form["UpdateFields"].Trim();
                    List<string> fieldList = new List<string>(fields.Split(','));
                    fieldList.Remove("DesignCode");
                    fieldList.Remove("ClassCode");
                    if (HttpContext.Current.Request.Form["ClassCode"] != null && HttpContext.Current.Request.Form["ClassCode"].Trim() != "" && HttpContext.Current.Request.Form["DesignCode"] != null && HttpContext.Current.Request.Form["DesignCode"].Trim() != "")
                    {
                        ClassCode = HttpContext.Current.Request.Form["ClassCode"].Trim();
                        DesignCode = HttpContext.Current.Request.Form["DesignCode"].Trim();
                    }
                    if (fieldList.Count > 0)
                    {
                        if (HttpContext.Current.Request.Form["ModuleOperCode"] != null && HttpContext.Current.Request.Form["ModuleOperCode"].Trim() != "")
                        {
                            string ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"].Trim();
                            BTDAppComponents entity = new BTDAppComponents();
                            for (int i = 0; i < fieldList.Count; i++)
                            {
                                switch (fieldList[i])
                                {
                                    case "Id":
                                        if (HttpContext.Current.Request.Form["Id"].Trim() != "")
                                        {
                                            entity.Id = Convert.ToInt64(HttpContext.Current.Request.Form["Id"]);
                                        }
                                        break;
                                    case "AppType":
                                        if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                                        {
                                            entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                                        }
                                        break;
                                    case "BuildType":
                                        if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                                        {
                                            entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                                        }
                                        break;
                                    case "CName":
                                        entity.CName = HttpContext.Current.Request.Form["CName"];
                                        break;
                                    case "Creator":
                                        entity.Creator = HttpContext.Current.Request.Form["Creator"];
                                        break;
                                    case "DataAddTime":
                                        if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                                        {
                                            entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                                        }
                                        break;
                                    case "DataUpdateTime":
                                        if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                                        {
                                            entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                                        }
                                        break;
                                    case "EName":
                                        entity.EName = HttpContext.Current.Request.Form["EName"];
                                        break;
                                    case "ExecuteCode":
                                        entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                                        break;
                                    case "InitParameter":
                                        entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                                        break;
                                    case "Modifier":
                                        entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                                        break;
                                    case "ModuleOperCode":
                                        entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                                        break;
                                    case "ModuleOperInfo":
                                        entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                                        break;
                                    case "PinYinZiTou":
                                        entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                                        break;
                                    case "BTDAppComponentsRefList":
                                        string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                                        //JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                                        break;
                                    case "BTDAppComponentsOperateList":
                                        string BTDAppComponentsOperateList = HttpContext.Current.Request.Form["BTDAppComponentsOperateList"];
                                        //JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                                        //entity.BTDAppComponentsOperateList = serializer1.Deserialize<List<BTDAppComponentsOperate>>(BTDAppComponentsOperateList);
                                        break;
                                }
                            }
                            fields = string.Join(",", fieldList.ToArray());
                            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                            if (tempArray != null)
                            {
                                tempBaseResultBool.success = IBBTDAppComponents.UpdateAndJSFile(tempArray, ModuleOperCode, ClassCode, DesignCode);
                            }
                        }
                        else
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "错误信息：ModuleOperCode参数不能为空！";
                        }
                    }
                    else
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：fieldList.Count参数不能为<0！";
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        //Update  BTDAppComponents
        public BaseResultBool CS_UDTO_UpdateBTDAppComponentsByField(BTDAppComponents entity, string fields)
        {
            IBBTDAppComponents.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    List<string> fieldList = new List<string>(fields.Split(','));
                    //if ((fieldList.IndexOf("") < 0) || (fieldList.IndexOf("") < 0)) 
                    //{
                    //DesignCode或ClassCode属性因数据量大，按Field更新时不做更新。
                    fieldList.Remove("DesignCode");
                    fieldList.Remove("ClassCode");
                    //}
                    if (fieldList.Count > 0)
                    {
                        fields = string.Join(",", fieldList.ToArray());
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDAppComponents.Entity, fields);
                        if (tempArray != null)
                        {
                            tempBaseResultBool.success = IBBTDAppComponents.Update(tempArray);
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDAppComponents.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDAppComponents
        public BaseResultBool CS_UDTO_DelBTDAppComponents(long longBTDAppComponentsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                var tempEntity = IBBTDAppComponents.Get(longBTDAppComponentsID);

                if (IBBTDAppComponents.JudgeBTDAppComponentsIsRef(longBTDAppComponentsID))
                    tempBaseResultBool.success = IBBTDAppComponents.Remove(longBTDAppComponentsID);
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：该应用【" + tempEntity.CName + "】不存在或已经被引用，不能删除！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultBool CS_UDTO_DelBTDAppComponentsAndJSFile(long longBTDAppComponentsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                var tempEntity = IBBTDAppComponents.Get(longBTDAppComponentsID);

                if (IBBTDAppComponents.JudgeBTDAppComponentsIsRef(longBTDAppComponentsID))
                {
                    tempBaseResultBool.success = IBBTDAppComponents.Remove(longBTDAppComponentsID);
                    if (!IBBTDAppComponents.DelJSFile(tempEntity.ModuleOperCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：该应用【" + tempEntity.CName + "：" + tempEntity.ModuleOperCode + "】JS文件未能删除！";
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：该应用【" + tempEntity.CName + "】不存在或已经被引用，不能删除！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponents(BTDAppComponents entity)
        {
            IBBTDAppComponents.Entity = entity;
            EntityList<BTDAppComponents> tempEntityList = new EntityList<BTDAppComponents>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDAppComponents.Search();
                tempEntityList.count = IBBTDAppComponents.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponents>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDAppComponents> tempEntityList = new EntityList<BTDAppComponents>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDAppComponents.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDAppComponents.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponents>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDAppComponents.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDAppComponents>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchRefBTDAppComponentsByHQLAndId(int page, int limit, string fields, string where, string sort, bool isPlanish, string AppId)
        {
            if (AppId != null && AppId.Trim() != "")
            {
                BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
                EntityList<BTDAppComponents> tempEntityList = new EntityList<BTDAppComponents>();
                if (where.Trim() == "")
                {
                    where = " btdappcomponents.Id <>" + AppId;
                }
                try
                {
                    if ((sort != null) && (sort.Length > 0))
                    {
                        tempEntityList = IBBTDAppComponents.SearchRefBTDAppComponentsByHQLAndId(where, CommonServiceMethod.GetSortHQL(sort), page, limit, long.Parse(AppId));
                    }
                    else
                    {
                        tempEntityList = IBBTDAppComponents.SearchRefBTDAppComponentsByHQLAndId(where, null, page, limit, long.Parse(AppId));
                    }
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponents>(tempEntityList);
                        }
                        else
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
                return tempBaseResultDataValue;
            }
            else
            {
                return this.CS_UDTO_SearchBTDAppComponentsByHQL(page, limit, fields, where, sort, isPlanish);
            }
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsRefListById(long BTDAppComponentsID, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDAppComponents.SearchRefAppByID(BTDAppComponentsID);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponentsRef>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_AddBTDAppComponentsFileEx()
        {
            BaseResultDataValue baseResule = new BaseResultDataValue();
            try
            {
                BTDAppComponents entity = new BTDAppComponents();
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "AppType":
                            if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                            {
                                entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                            }
                            break;
                        case "BuildType":
                            if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                            {
                                entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                            }
                            break;
                        case "CName":
                            entity.CName = HttpContext.Current.Request.Form["CName"];
                            break;
                        case "Creator":
                            entity.Creator = HttpContext.Current.Request.Form["Creator"];
                            break;
                        case "DataAddTime":
                            if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                            {
                                entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                            }
                            break;
                        case "DataUpdateTime":
                            if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                            {
                                entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                            }
                            break;
                        case "DesignCode":
                            entity.DesignCode = HttpContext.Current.Request.Form["DesignCode"];
                            break;
                        case "EName":
                            entity.EName = HttpContext.Current.Request.Form["EName"];
                            break;
                        case "ExecuteCode":
                            entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                            break;
                        case "InitParameter":
                            entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                            break;
                        case "Modifier":
                            entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                            break;
                        case "ModuleOperCode":
                            entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                            break;
                        case "ModuleOperInfo":
                            entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                            break;
                        case "PinYinZiTou":
                            entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                            break;
                        case "BTDAppComponentsRefList":
                            string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                            //JavaScriptSerializer serializer = new JavaScriptSerializer();
                            //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                            break;
                        case "DataTimeStamp":
                            if (HttpContext.Current.Request.Form["DataTimeStamp"] != null && HttpContext.Current.Request.Form["DataTimeStamp"].Trim() != "")
                            {
                                string[] tmpdts = HttpContext.Current.Request.Form["DataTimeStamp"].Split(',');
                                byte[] tmpbyte = new byte[tmpdts.Length];
                                for (int j = 0; j < tmpbyte.Length; j++)
                                {
                                    tmpbyte[j] = Convert.ToByte(tmpdts[j].Replace("[", "").Replace("]", ""));
                                }
                                entity.DataTimeStamp = tmpbyte;
                            }
                            break;
                    }
                }
                BaseResultDataValue tempValue = ReceiveFileServiceEx();
                if ((tempValue.success) && (tempValue.ResultDataValue.Length > 0))
                    entity.ClassCode = StringPlus.ConvertSpecialCharacter(tempValue.ResultDataValue);
                entity.Id = Common.Public.GUIDHelp.GetGUIDLong();
                IBBTDAppComponents.Entity = entity;
                baseResule.success = IBBTDAppComponents.Add(entity.BTDAppComponentsRefList);
                baseResule.ResultDataValue = IBBTDAppComponents.Entity.Id.ToString();
            }
            catch (Exception ex)
            {
                baseResule.ErrorInfo = ex.Message;
                baseResule.success = false;
                throw new Exception(ex.Message);
            }
            return baseResule;
        }

        public BaseResultBool CS_UDTO_UpdateBTDAppComponentsFileEx()
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                BTDAppComponents entity = new BTDAppComponents();
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "Id":
                            if (HttpContext.Current.Request.Form["Id"].Trim() != "")
                            {
                                entity.Id = Convert.ToInt64(HttpContext.Current.Request.Form["Id"]);
                            }
                            break;
                        case "AppType":
                            if (HttpContext.Current.Request.Form["AppType"].Trim() != "")
                            {
                                entity.AppType = Convert.ToInt32(HttpContext.Current.Request.Form["AppType"]);
                            }
                            break;
                        case "BuildType":
                            if (HttpContext.Current.Request.Form["BuildType"].Trim() != "")
                            {
                                entity.BuildType = Convert.ToInt32(HttpContext.Current.Request.Form["BuildType"]);
                            }
                            break;
                        case "CName":
                            entity.CName = HttpContext.Current.Request.Form["CName"];
                            break;
                        case "Creator":
                            entity.Creator = HttpContext.Current.Request.Form["Creator"];
                            break;
                        case "DataAddTime":
                            if (HttpContext.Current.Request.Form["DataAddTime"].Trim() != "")
                            {
                                entity.DataAddTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataAddTime"]);
                            }
                            break;
                        case "DataUpdateTime":
                            if (HttpContext.Current.Request.Form["DataUpdateTime"].Trim() != "")
                            {
                                entity.DataUpdateTime = Convert.ToDateTime(HttpContext.Current.Request.Form["DataUpdateTime"]);
                            }
                            break;
                        case "DesignCode":
                            entity.DesignCode = HttpContext.Current.Request.Form["DesignCode"];
                            break;
                        case "EName":
                            entity.EName = HttpContext.Current.Request.Form["EName"];
                            break;
                        case "ExecuteCode":
                            entity.ExecuteCode = HttpContext.Current.Request.Form["ExecuteCode"];
                            break;
                        case "InitParameter":
                            entity.InitParameter = HttpContext.Current.Request.Form["InitParameter"];
                            break;
                        case "Modifier":
                            entity.Modifier = HttpContext.Current.Request.Form["Modifier"];
                            break;
                        case "ModuleOperCode":
                            entity.ModuleOperCode = HttpContext.Current.Request.Form["ModuleOperCode"];
                            break;
                        case "ModuleOperInfo":
                            entity.ModuleOperInfo = HttpContext.Current.Request.Form["ModuleOperInfo"];
                            break;
                        case "PinYinZiTou":
                            entity.PinYinZiTou = HttpContext.Current.Request.Form["PinYinZiTou"];
                            break;
                        case "BTDAppComponentsRefList":
                            string tempJsonStr = HttpContext.Current.Request.Form["BTDAppComponentsRefList"];
                            //JavaScriptSerializer serializer = new JavaScriptSerializer();
                            //entity.BTDAppComponentsRefList = serializer.Deserialize<List<BTDAppComponentsRef>>(tempJsonStr);
                            break;
                        case "DataTimeStamp":
                            if (HttpContext.Current.Request.Form["DataTimeStamp"] != null && HttpContext.Current.Request.Form["DataTimeStamp"].Trim() != "")
                            {
                                string[] tmpdts = HttpContext.Current.Request.Form["DataTimeStamp"].Split(',');
                                byte[] tmpbyte = new byte[tmpdts.Length];
                                for (int j = 0; j < tmpbyte.Length; j++)
                                {
                                    tmpbyte[j] = Convert.ToByte(tmpdts[j].Replace("[", "").Replace("]", ""));
                                }
                                entity.DataTimeStamp = tmpbyte;
                            }
                            break;
                    }
                }
                IBBTDAppComponents.Entity = entity;
                BaseResultDataValue tempValue = ReceiveFileServiceEx();
                if ((!string.IsNullOrEmpty(tempValue.ResultDataValue)) && (tempValue.ResultDataValue.Length > 0))
                {
                    IBBTDAppComponents.Entity.ClassCode = StringPlus.ConvertSpecialCharacter(tempValue.ResultDataValue);
                    tempBaseResultBool.success = IBBTDAppComponents.Edit(entity.BTDAppComponentsRefList);
                }
                else
                {
                    string tempFileds = "Id,CName,ModuleOperCode,InitParameter,ModuleOperInfo";
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDAppComponents.Entity, tempFileds);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDAppComponents.Update(tempArray);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.ErrorInfo = ex.Message;
                tempBaseResultBool.success = false;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_RJ_GetBTDAppComponentsFrameTree(string strBTDAppComponentsID, string treeDataConfig)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            long tempBTDAppComponentsID = 0;
            int tempTreeDataConfig = 0;
            try
            {
                if (!string.IsNullOrEmpty(treeDataConfig))
                    tempTreeDataConfig = Int16.Parse(treeDataConfig);
                if (!((string.IsNullOrEmpty(strBTDAppComponentsID)) || (strBTDAppComponentsID.ToLower().Trim() == "root")))
                    tempBTDAppComponentsID = Int64.Parse(strBTDAppComponentsID);
                tempBaseResultTree = IBBTDAppComponents.SearchBTDAppComponentsFrameTree(tempBTDAppComponentsID, tempTreeDataConfig);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;

        }

        /// <summary>
        /// 根据应用ID获取应用列表树
        /// </summary>
        /// <param name="strRBACRoleID"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultDataValue CS_RJ_GetBTDAppComponentsFrameListTree(string strBTDAppComponentsID, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<BTDAppComponents> tempBaseResultTree = new BaseResultTree<BTDAppComponents>();
            long tempBTDAppComponentsID = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strBTDAppComponentsID.Trim())) || (strBTDAppComponentsID.ToLower().Trim() == "root")))
                    tempBTDAppComponentsID = Int64.Parse(strBTDAppComponentsID);
                Log.Info("Tree1:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo));
                tempBaseResultTree = IBBTDAppComponents.SearchBTDAppComponentsListTree(tempBTDAppComponentsID);
                Log.Info("Tree2:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo));
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        Log.Info("Tree11:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo));
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
                        Log.Info("Tree12:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo));
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_RJ_GetBTDAppComponentsRefTree(string strBTDAppComponentsID, string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<BTDAppComponents> tempBaseResultTree = new BaseResultTree<BTDAppComponents>();
            long tempBTDAppComponentsID = 0;
            try
            {
                if (!((string.IsNullOrEmpty(strBTDAppComponentsID.Trim())) || (strBTDAppComponentsID.ToLower().Trim() == "root")))
                    tempBTDAppComponentsID = Int64.Parse(strBTDAppComponentsID);
                tempBaseResultTree = IBBTDAppComponents.SearchBTDAppComponentsRefTree(tempBTDAppComponentsID);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    //tempBaseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultTree);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_RJ_GetBackgroundPicture()
        {
            //Picture图片字符串,ExpandName图片扩展名
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string strPicture = "";
            try
            {
                strPicture = HttpContext.Current.Request.Form["Picture"];
                if (!string.IsNullOrEmpty(strPicture))
                {
                    byte[] tempBuf = Convert.FromBase64String(strPicture);//把字符串读到字节数组中
                    MemoryStream tempMemoryStream = new MemoryStream(tempBuf);
                    System.Drawing.Image tempImge = System.Drawing.Image.FromStream(tempMemoryStream);
                    string tempPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UpLoadPicturePath");
                    //if (string.IsNullOrEmpty(strExpandName))
                    string tempFileName = "";
                    if (string.IsNullOrEmpty(tempPath))
                        tempFileName = "Images\\" + GUIDHelp.GetGUIDString() + ".jpg";
                    else
                        tempFileName = tempPath + "\\" + GUIDHelp.GetGUIDString() + ".jpg";
                    tempPath = System.AppDomain.CurrentDomain.BaseDirectory + tempFileName;
                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(tempPath)))
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempPath));
                    tempImge.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tempBaseResultDataValue.ResultDataValue = "{ filepath:" + tempFileName + " }";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.ErrorInfo = ex.Message;
                tempBaseResultDataValue.success = false;
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region BTDModuleType
        //Add  BTDModuleType
        public BaseResultDataValue CS_UDTO_AddBTDModuleType(BTDModuleType entity)
        {
            IBBTDModuleType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTDModuleType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTDModuleType.Get(IBBTDModuleType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDModuleType.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BTDModuleType
        public BaseResultBool CS_UDTO_UpdateBTDModuleType(BTDModuleType entity)
        {
            IBBTDModuleType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDModuleType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTDModuleType
        public BaseResultBool CS_UDTO_UpdateBTDModuleTypeByField(BTDModuleType entity, string fields)
        {
            IBBTDModuleType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDModuleType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDModuleType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDModuleType.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDModuleType
        public BaseResultBool CS_UDTO_DelBTDModuleType(long longBTDModuleTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDModuleType.Remove(longBTDModuleTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDModuleType(BTDModuleType entity)
        {
            IBBTDModuleType.Entity = entity;
            EntityList<BTDModuleType> tempEntityList = new EntityList<BTDModuleType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDModuleType.Search();
                tempEntityList.count = IBBTDModuleType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDModuleType>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDModuleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDModuleType> tempEntityList = new EntityList<BTDModuleType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDModuleType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDModuleType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDModuleType>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDModuleTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDModuleType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDModuleType>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion 

        #region BTDPictureType
        //Add  BTDPictureType
        public BaseResultDataValue CS_UDTO_AddBTDPictureType(BTDPictureType entity)
        {
            IBBTDPictureType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTDPictureType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTDPictureType.Get(IBBTDPictureType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDPictureType.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BTDPictureType
        public BaseResultBool CS_UDTO_UpdateBTDPictureType(BTDPictureType entity)
        {
            IBBTDPictureType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDPictureType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTDPictureType
        public BaseResultBool CS_UDTO_UpdateBTDPictureTypeByField(BTDPictureType entity, string fields)
        {
            IBBTDPictureType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDPictureType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDPictureType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDPictureType.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDPictureType
        public BaseResultBool CS_UDTO_DelBTDPictureType(long longBTDPictureTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDPictureType.Remove(longBTDPictureTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureType(BTDPictureType entity)
        {
            IBBTDPictureType.Entity = entity;
            EntityList<BTDPictureType> tempEntityList = new EntityList<BTDPictureType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDPictureType.Search();
                tempEntityList.count = IBBTDPictureType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDPictureType>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDPictureType> tempEntityList = new EntityList<BTDPictureType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDPictureType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDPictureType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDPictureType>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDPictureType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDPictureType>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion 

        #region BTDAppPicture
        //Add  BTDAppPicture
        public BaseResultDataValue CS_UDTO_AddBTDAppPicture(BTDAppPicture entity)
        {
            IBBTDAppPicture.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTDAppPicture.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTDAppPicture.Get(IBBTDAppPicture.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDAppPicture.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BTDAppPicture
        public BaseResultBool CS_UDTO_UpdateBTDAppPicture(BTDAppPicture entity)
        {
            IBBTDAppPicture.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDAppPicture.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTDAppPicture
        public BaseResultBool CS_UDTO_UpdateBTDAppPictureByField(BTDAppPicture entity, string fields)
        {
            IBBTDAppPicture.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDAppPicture.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDAppPicture.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDAppPicture.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDAppPicture
        public BaseResultBool CS_UDTO_DelBTDAppPicture(long longBTDAppPictureID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDAppPicture.Remove(longBTDAppPictureID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppPicture(BTDAppPicture entity)
        {
            IBBTDAppPicture.Entity = entity;
            EntityList<BTDAppPicture> tempEntityList = new EntityList<BTDAppPicture>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDAppPicture.Search();
                tempEntityList.count = IBBTDAppPicture.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppPicture>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppPictureByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDAppPicture> tempEntityList = new EntityList<BTDAppPicture>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDAppPicture.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDAppPicture.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppPicture>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppPictureById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDAppPicture.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDAppPicture>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion 

        #region BTDPictureTypeCon
        //Add  BTDPictureTypeCon
        public BaseResultDataValue CS_UDTO_AddBTDPictureTypeCon(BTDPictureTypeCon entity)
        {
            IBBTDPictureTypeCon.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTDPictureTypeCon.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTDPictureTypeCon.Get(IBBTDPictureTypeCon.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDPictureTypeCon.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BTDPictureTypeCon
        public BaseResultBool CS_UDTO_UpdateBTDPictureTypeCon(BTDPictureTypeCon entity)
        {
            IBBTDPictureTypeCon.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDPictureTypeCon.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTDPictureTypeCon
        public BaseResultBool CS_UDTO_UpdateBTDPictureTypeConByField(BTDPictureTypeCon entity, string fields)
        {
            IBBTDPictureTypeCon.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDPictureTypeCon.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDPictureTypeCon.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDPictureTypeCon.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDPictureTypeCon
        public BaseResultBool CS_UDTO_DelBTDPictureTypeCon(long longBTDPictureTypeConID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDPictureTypeCon.Remove(longBTDPictureTypeConID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureTypeCon(BTDPictureTypeCon entity)
        {
            IBBTDPictureTypeCon.Entity = entity;
            EntityList<BTDPictureTypeCon> tempEntityList = new EntityList<BTDPictureTypeCon>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDPictureTypeCon.Search();
                tempEntityList.count = IBBTDPictureTypeCon.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDPictureTypeCon>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureTypeConByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDPictureTypeCon> tempEntityList = new EntityList<BTDPictureTypeCon>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDPictureTypeCon.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDPictureTypeCon.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDPictureTypeCon>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDPictureTypeConById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDPictureTypeCon.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDPictureTypeCon>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion 

        #region BTDAppComponentsOperate
        //Add  BTDAppComponentsOperate
        public BaseResultDataValue CS_UDTO_AddBTDAppComponentsOperate(BTDAppComponentsOperate entity)
        {
            IBBTDAppComponentsOperate.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBTDAppComponentsOperate.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBTDAppComponentsOperate.Get(IBBTDAppComponentsOperate.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBTDAppComponentsOperate.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BTDAppComponentsOperate
        public BaseResultBool CS_UDTO_UpdateBTDAppComponentsOperate(BTDAppComponentsOperate entity)
        {
            IBBTDAppComponentsOperate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDAppComponentsOperate.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BTDAppComponentsOperate
        public BaseResultBool CS_UDTO_UpdateBTDAppComponentsOperateByField(BTDAppComponentsOperate entity, string fields)
        {
            IBBTDAppComponentsOperate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBTDAppComponentsOperate.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBTDAppComponentsOperate.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBTDAppComponentsOperate.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BTDAppComponentsOperate
        public BaseResultBool CS_UDTO_DelBTDAppComponentsOperate(long longBTDAppComponentsOperateID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBTDAppComponentsOperate.Remove(longBTDAppComponentsOperateID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperate(BTDAppComponentsOperate entity)
        {
            IBBTDAppComponentsOperate.Entity = entity;
            EntityList<BTDAppComponentsOperate> tempEntityList = new EntityList<BTDAppComponentsOperate>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBTDAppComponentsOperate.Search();
                tempEntityList.count = IBBTDAppComponentsOperate.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponentsOperate>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BTDAppComponentsOperate> tempEntityList = new EntityList<BTDAppComponentsOperate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBTDAppComponentsOperate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBTDAppComponentsOperate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BTDAppComponentsOperate>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBTDAppComponentsOperate.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BTDAppComponentsOperate>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #endregion

        #region 其他
        public BaseResultDataValue CS_UDTO_GetServerInformation()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.ResultDataValue = "{ \"ServerCurrentTime\":\"" + DateTime.Now.ToString(ZhiFang.Common.Public.JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo) + "\"" + "}";
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 测试

        public BaseResultDataValue ReceiveJsonStringService()
        {
            string iTotal = HttpContext.Current.Request["Id"];
            //ZhiFang.Common.Log.Log.Info(JsonStr);
            throw new NotImplementedException();
        }
        public BaseResultDataValue CS_ChartData()
        {
            BaseResultDataValue tmp = new BaseResultDataValue();
            tmp.ResultDataValue = "data: [{\'name\':\'metric one\', \'data1\':10, \'data2\':12, \'data3\':14, \'data4\':8, \'data5\':13},{\'name\':\'metric two\', \'data1\':7, \'data2\':8, \'data3\':16, \'data4\':10, \'data5\':3},{\'name\':\'metric three\', \'data1\':5, \'data2\':2, \'data3\':14, \'data4\':12, \'data5\':7},{\'name\':\'metric four\', \'data1\':2, \'data2\':14, \'data3\':6, \'data4\':1, \'data5\':23},{\'name\':\'metric five\', \'data1\':27, \'data2\':38, \'data3\':36, \'data4\':13, \'data5\':33}     ]";
            return tmp;
        }
        #endregion
    }
}

