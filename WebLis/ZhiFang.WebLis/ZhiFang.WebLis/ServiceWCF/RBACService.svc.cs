using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.WebLis.ServerContract;
using ZhiFang.WebLis.Class;
using System.Xml;
using System.Web.UI.WebControls;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using Newtonsoft.Json;
using System.Web;
using ZhiFang.Model;
using ZhiFang.Model.RBAC.Entity;
using System.Data;
using System.Dynamic;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : IRBACService
    {
        protected XmlNodeList ModuleList = null;
        StringBuilder result = new StringBuilder();
        StringBuilder sb = new StringBuilder();


        #region IRBACService 成员
        public List<ZhiFang.Model.tree> RBAC_GetModuleTreeByCookie()
        {
            try
            {
                if (string.IsNullOrEmpty(ConfigHelper.GetConfigString("WebLisInCluderRMS")) || ConfigHelper.GetConfigString("WebLisInCluderRMS").Trim() == "0")
                {
                    ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeByCookie.读取RMS模块。");
                    List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
                    var aaa = HttpContext.Current.Request;
                    string cookiesName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");

                    string pwd = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangPwd");
                    User u = new User(cookiesName);
                    ModuleManager m = new ModuleManager();
                    u.SetModulesList();
                    XmlDocument xmlstrmodules1 = m.GetModulesXml(u.ModulesList);
                    string xmlStr = xmlstrmodules1.InnerXml;

                    XmlDocument xmlstrmodules = new XmlDocument();
                    //GetXml.SingleLoginWS singleLoginWS = new GetXml.SingleLoginWS();

                    string descript = "";
                    string userID = "";
                    string xmlNew = "";
                    string isHasService = ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsHasService");
                    if (isHasService == "1")
                    {
                        Log.Info("传入的xml:" + xmlStr);
                        try
                        {
                            SingleLogin.SingleLoginWS singleLoginWS = new SingleLogin.SingleLoginWS();
                            xmlNew = singleLoginWS.GetMergeCWAndWeblisModulesXML(cookiesName, pwd, xmlStr, out descript, out userID);

                        }
                        catch (Exception ee)
                        {
                            Log.Info("获取XML出错:" + descript);
                        }
                        Log.Info("返回的xml:" + xmlNew);
                    }
                    if (isHasService == "1" && !String.IsNullOrEmpty(xmlNew))
                    {
                        xmlstrmodules.LoadXml(xmlNew);
                        Cookie.CookieHelper.Write("UserID", userID);
                        Log.Info("descriptdescriptdescript:" + descript);
                    }
                    else
                    {
                        xmlstrmodules = xmlstrmodules1;
                        if (xmlstrmodules.InnerText.IndexOf("SC") >= 0)
                        {
                            XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");

                            nodeLst.RemoveAll();
                            nodeLst.ParentNode.RemoveChild(nodeLst);
                        }
                        Log.Info("获取xml为空!");
                    }

                    if (xmlstrmodules != null && xmlstrmodules.OuterXml.Trim() != "")
                    {
                        //更新时间
                        XmlAttribute UpdateTime = xmlstrmodules.CreateAttribute("UpdateTime");
                        UpdateTime.Value = DateTime.Now.ToString();
                        xmlstrmodules.DocumentElement.Attributes.Append(UpdateTime);
                        moduletree = bindTreeView(xmlstrmodules.DocumentElement);
                    }
                    else
                    {
                        return null;// "无模块信息！";

                    }
                    return moduletree;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeByCookie.读取WebLis模块。");
                    List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
                    string cookiesName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    ModuleManager m = new ModuleManager();
                    XmlDocument xmlstrmodules = new XmlDocument();
                    if (cookiesName == "zhifangkj8001" || (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("adminaccount")) && cookiesName == ConfigHelper.GetConfigString("adminaccount")))
                    {
                        XmlDocument xmlstrmodules1 = m.GetModulesXmlAll();
                        string xmlStr = xmlstrmodules1.InnerXml;
                        xmlstrmodules = xmlstrmodules1;
                        if (xmlstrmodules.InnerText.IndexOf("SC") >= 0)
                        {
                            XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");
                            nodeLst.RemoveAll();
                            nodeLst.ParentNode.RemoveChild(nodeLst);
                        }
                    }
                    else
                    {
                        //所属角色
                        var brdv = CheckCookie("GetRolesByEmpId");
                        if (!brdv.success)
                        {
                            ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeByCookie.身份验证失败：" + brdv.ErrorInfo);
                            return null;
                        }
                        string EmpId = Cookie.CookieHelper.Read("ZhiFangUserID");
                        BLL.RBAC.RBAC_EmplRoles bllemproles = new BLL.RBAC.RBAC_EmplRoles();
                        var roleslist = bllemproles.GetModelList(" emplid=" + EmpId + " and postID is not null ");
                        List<string> rolesidlist = new List<string>();
                        roleslist.ForEach(a =>
                        {
                            if (a.PostID.HasValue && !rolesidlist.Contains(a.PostID.Value.ToString()))
                                rolesidlist.Add(a.PostID.Value.ToString());

                        });
                        //角色模块
                        BLL.RBAC.RBAC_RoleModuleLink rml = new BLL.RBAC.RBAC_RoleModuleLink();
                        var rolemodulelist = rml.GetModelList(" roleid in (" + string.Join(",", rolesidlist) + ") ");
                        List<string> moduleidlist = new List<string>();
                        rolemodulelist.ForEach(a =>
                        {
                            if (a.ModuleId.HasValue && !moduleidlist.Contains(a.ModuleId.Value.ToString()))
                                moduleidlist.Add(a.ModuleId.Value.ToString());
                        });
                        //模块列表
                        IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
                        DataSet ds = ibm.GetListByPage(" id in (" + string.Join(",", moduleidlist) + ") ", 1, 100000, " SN asc");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            XmlDocument xmlstrmodules1 = m.GetModulesXml(ds);
                            string xmlStr = xmlstrmodules1.InnerXml;
                            xmlstrmodules = xmlstrmodules1;
                            if (xmlstrmodules.InnerText.IndexOf("SC") >= 0)
                            {
                                XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");
                                nodeLst.RemoveAll();
                                nodeLst.ParentNode.RemoveChild(nodeLst);
                            }
                        }
                    }

                    if (xmlstrmodules != null && xmlstrmodules.OuterXml.Trim() != "")
                    {
                        //更新时间
                        XmlAttribute UpdateTime = xmlstrmodules.CreateAttribute("UpdateTime");
                        UpdateTime.Value = DateTime.Now.ToString();
                        xmlstrmodules.DocumentElement.Attributes.Append(UpdateTime);
                        moduletree = bindTreeView(xmlstrmodules.DocumentElement);
                    }
                    else
                    {
                        return null;// "无模块信息！";

                    }

                    return moduletree;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeByCookie.异常：" + e.ToString());
                return null;
            }
        }

        private List<ZhiFang.Model.tree> bindTreeView(XmlNode document)
        {
            List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
            foreach (XmlNode node in document.ChildNodes)
            {
                ZhiFang.Model.tree modulesubtree = new Model.tree();
                modulesubtree.id = node.Attributes["NodeData"].Value;
                modulesubtree.text = node.Attributes["Text"].Value;
                modulesubtree.state = "open";
                string NavigateUrl = node.Attributes["NavigateUrl"].Value;
                string ImageUrl = " ../images/icons/zhi1.gif";
                string Para = "";
                string SN = "00";
                if (node.Attributes["Para"] != null)
                {
                    Para = node.Attributes["Para"].Value;
                }
                if (node.Attributes["SN"] != null)
                {
                    SN = node.Attributes["SN"].Value;
                    modulesubtree.Target = node.Attributes["SN"].Value;
                }
                modulesubtree.attributes = "NavigateUrl:'" + NavigateUrl + "',ImageUrl:'" + ImageUrl + "',Para:'" + Para + "',SN:'" + SN + "'";
                if (node.ChildNodes.Count > 0)
                {
                    modulesubtree.children = bindTreeView(node);
                }
                moduletree.Add(modulesubtree);
            }
            return moduletree;
        }

        public List<ZhiFang.Model.tree> RBAC_GetModuleTreeAll()
        {
            try
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeAll.读取WebLis模块。");
                List<ZhiFang.Model.tree> moduletree = new List<ZhiFang.Model.tree>();
                string cookiesName = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                ModuleManager m = new ModuleManager();
                XmlDocument xmlstrmodules = new XmlDocument();

                XmlDocument xmlstrmodules1 = m.GetModulesXmlAll();
                string xmlStr = xmlstrmodules1.InnerXml;
                xmlstrmodules = xmlstrmodules1;
                if (xmlstrmodules.InnerText.IndexOf("SC") >= 0)
                {
                    XmlNode nodeLst = xmlstrmodules.SelectSingleNode("//TREENODES/treenode[@ModuleCode='SC']");
                    nodeLst.RemoveAll();
                    nodeLst.ParentNode.RemoveChild(nodeLst);
                }


                if (xmlstrmodules != null && xmlstrmodules.OuterXml.Trim() != "")
                {
                    //更新时间
                    XmlAttribute UpdateTime = xmlstrmodules.CreateAttribute("UpdateTime");
                    UpdateTime.Value = DateTime.Now.ToString();
                    xmlstrmodules.DocumentElement.Attributes.Append(UpdateTime);
                    moduletree = bindTreeView(xmlstrmodules.DocumentElement);
                }
                else
                {
                    return null;// "无模块信息！";

                }

                return moduletree;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeAll.异常：" + e.ToString());
                return null;
            }
        }

        public BaseResultDataValue RBAC_GetModuleList(string where, int page, int limit, string sort, string order, string sn)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(sn))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }
                IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
                if (where == null || where.Trim() == "")
                {
                    where = "1=1";
                }
                where += " and sn like '" + sn + "%' and len(sn)=" + (sn.Length + 2).ToString() + "  ";

                string Sort = (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order)) ? sort + " " + order : null;
                DataSet ds = ibm.GetListByPage(where, page, limit, Sort);
                EntityListEasyUI<Modules> entitymoduleList = new EntityListEasyUI<Modules>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = ibm.GetRecordCount(where);
                    var modulelist = ibm.DataTableToList(ds.Tables[0]);
                    entitymoduleList.total = count;
                    entitymoduleList.rows = modulelist;
                }
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entitymoduleList);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeByCookie.异常：" + e.ToString());
                return new BaseResultDataValue() { ErrorInfo = "程序异常！", success = false };
            }
        }

        public BaseResultDataValue RBAC_ADDModule(Modules entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
            if (entity != null)
            {
                try
                {
                    //brdv = CheckCookie("RBAC_AddEmp");身份验证
                    if (!brdv.success)
                        return brdv;
                    brdv = ibm.Addentity(entity);
                    if (brdv.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        brdv.ErrorInfo = "新增模块失败！";
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "新增模块异常！";
                    ZhiFang.Common.Log.Log.Error("RBAC_ADDModule.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;
        }

        public BaseResultDataValue RBAC_DelModule(string Id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    //brdv = CheckCookie("RBAC_AddEmp");身份验证
                    if (!brdv.success)
                        return brdv;
                    brdv = ibm.Delete(int.Parse(Id));
                    return brdv;
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "删除模块异常！";
                    ZhiFang.Common.Log.Log.Error("RBAC_DelModule.删除模块异常：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;
        }
        #endregion

        public BaseResultDataValue RBAC_GetDeptTree()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<ZhiFang.Model.tree> depttree = new List<ZhiFang.Model.tree>();
                BLL.RBAC.HR_Departments blldept = new BLL.RBAC.HR_Departments();
                depttree = blldept.RBAC_GetDeptTree();
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(depttree);
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("RBAC_GetDeptTree.异常：" + e.ToString());
                return brdv;
            }
        }

        public BaseResultDataValue RBAC_GetDeptList(string wherestr, int page, int rows, string sort, string order)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Departments blldept = new BLL.RBAC.HR_Departments();
            try
            {
                List<ZhiFang.Model.RBAC.Entity.HR_Departments> deptlist = new List<Model.RBAC.Entity.HR_Departments>();
                deptlist = new List<ZhiFang.Model.RBAC.Entity.HR_Departments>();
                deptlist = blldept.GetListByPage(wherestr, sort, page, rows);
                int count = blldept.GetRecordCount(wherestr);
                EntityListEasyUI<Model.RBAC.Entity.HR_Departments> entityList = new EntityListEasyUI<Model.RBAC.Entity.HR_Departments>();
                entityList.total = count;
                entityList.rows = deptlist;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("RBAC_GetDeptList.异常：" + e.ToString());
                return brdv;
            }
        }

        public BaseResultDataValue RBAC_AddDept(HR_Departments entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BLL.RBAC.HR_Departments blldept = new BLL.RBAC.HR_Departments();
            if (entity != null)
            {
                try
                {
                    baseResultDataValue = blldept.Add(entity);
                    if (baseResultDataValue.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "新增部门失败！";
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("RBAC_AddDept.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue RBAC_UpdateDept(HR_Departments entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BLL.RBAC.HR_Departments blldept = new BLL.RBAC.HR_Departments();
            if (entity != null)
            {
                try
                {
                    baseResultDataValue = blldept.Update(entity);
                    if (baseResultDataValue.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "编辑部门失败！";
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("RBAC_UpdateDept.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue RBAC_DelDept(string DeptId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BLL.RBAC.HR_Departments blldept = new BLL.RBAC.HR_Departments();
            int id;
            if (!int.TryParse(DeptId, out id))
            {
                baseResultDataValue.ErrorInfo = "删除异常参数错误！";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            try
            {
                baseResultDataValue.success = blldept.Delete(id);
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_DelDept.异常：" + e.ToString());
                baseResultDataValue.ErrorInfo = "删除异常！";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
        }

        //public BaseResultBool RBAC_UpdateDept(N_News entity)
        //{
        //    BaseResultBool baseResultBool = new BaseResultBool();
        //    if (entity != null)
        //    {
        //        entity.DataUpdateTime = DateTime.Now;
        //        try
        //        {
        //            entity.Status = int.Parse(NNewsStatus.起草.Key);
        //            entity.StatusName = NNewsStatus.起草.Value.Name;
        //            baseResultBool.success = BNNews.Update(entity);
        //            if (baseResultBool.success)
        //            {
        //                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            baseResultBool.success = false;
        //            baseResultBool.ErrorInfo = "错误信息：" + ex.ToString();
        //            //throw new Exception(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        baseResultBool.success = false;
        //        baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
        //    }
        //    return baseResultBool;
        //}

        public BaseResultDataValue RBAC_GetEmpList(string wherestr, int page, int rows, string sort, string order)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();
            brdv = CheckCookie("RBAC_GetEmpList");
            if (!brdv.success)
                return brdv;
            try
            {
                List<HR_Employees> emplist = new List<HR_Employees>();
                emplist = new List<HR_Employees>();
                if (string.IsNullOrEmpty(wherestr))
                    wherestr = " DeptId is not null";
                else
                    wherestr += " and DeptId is not null";
                emplist = bllemp.GetListByPage(wherestr, sort, page, rows);
                int count = bllemp.GetRecordCount(wherestr);
                EntityListEasyUI<HR_Employees> entityList = new EntityListEasyUI<HR_Employees>();
                entityList.total = count;
                entityList.rows = emplist;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("RBAC_GetEmpList.异常：" + e.ToString());
                return brdv;
            }
        }

        public BaseResultDataValue RBAC_AddEmp(HR_Employees entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();
            if (entity != null)
            {
                try
                {
                    brdv = CheckCookie("RBAC_AddEmp");
                    if (!brdv.success)
                        return brdv;
                    brdv = bllemp.Add(entity);
                    if (brdv.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        brdv.ErrorInfo = "新增人员失败！";
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("RBAC_AddEmp.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;

        }

        public BaseResultDataValue RBAC_AddEmpAndAccount(HR_Employees entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();
            if (entity != null)
            {
                try
                {
                    brdv = CheckCookie("RBAC_AddEmp");
                    if (!brdv.success)
                        return brdv;
                    brdv = bllemp.Add(entity);
                    if (brdv.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        brdv.ErrorInfo = "新增人员失败！";
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("RBAC_AddEmpAndAccount.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;

        }

        public BaseResultDataValue RBAC_UpdateEmp(HR_Employees entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();
            if (entity != null)
            {
                try
                {
                    brdv = CheckCookie("RBAC_UpdateEmp");
                    if (!brdv.success)
                        return brdv;
                    brdv = bllemp.Update(entity);
                    if (brdv.success)
                    {
                        //baseResultDataValue.ResultDataValue = entity.ID.ToString();
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                    else
                    {
                        brdv.ErrorInfo = "编辑部门失败！";
                    }
                }
                catch (Exception ex)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：" + ex.ToString();
                    ZhiFang.Common.Log.Log.Error("RBAC_UpdateEmp.错误信息：" + ex.ToString());
                    //throw new Exception(ex.ToString());
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return brdv;

        }

        public BaseResultDataValue RBAC_DelEmp(string EmpId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();
            brdv = CheckCookie("RBAC_DelEmp");
            if (!brdv.success)
                return brdv;
            int id;
            if (!int.TryParse(EmpId, out id))
            {
                brdv.ErrorInfo = "删除异常参数错误！";
                brdv.success = false;
                return brdv;
            }
            try
            {
                brdv.success = bllemp.Delete(id);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_DelEmp.异常：" + e.ToString());
                brdv.ErrorInfo = "删除异常！";
                brdv.success = false;
                return brdv;
            }

        }
        public BaseResultDataValue CheckCookie(string methodname)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserID");
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read("EmployeeName");
            string account = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
            if (string.IsNullOrEmpty(empid) || string.IsNullOrEmpty(empname) || string.IsNullOrEmpty(account))
            {
                brdv.ErrorInfo = "未能识别身份信息请重新登录！";
                brdv.success = false;
                return brdv;
            }
            ZhiFang.Common.Log.Log.Debug($"{methodname}.empid:{empid},empname:{empname},account:{account}.");
            return brdv;
        }

        public BaseResultDataValue GetRoles(string groupname)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(groupname))
            {
                groupname = "WebLis角色";
            }
            brdv = CheckCookie("GetRoles");
            if (!brdv.success)
                return brdv;
            try
            {
                BLL.RBAC.HR_Posts bllpost = new BLL.RBAC.HR_Posts();
                var roleslist = bllpost.GetModelList(" GroupName='" + groupname + "' ");
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(roleslist);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetRoles.异常：" + e.ToString());
                brdv.ErrorInfo = "获取角色列表异常！";
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue GetRolesByEmpId(string EmpId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(EmpId))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }
            brdv = CheckCookie("GetRolesByEmpId");
            if (!brdv.success)
                return brdv;
            try
            {
                BLL.RBAC.RBAC_EmplRoles bllemproles = new BLL.RBAC.RBAC_EmplRoles();
                var roleslist = bllemproles.GetModelList(" emplid=" + EmpId + " and postID is not null ");
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(roleslist);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetRolesByEmpId.异常：" + e.ToString());
                brdv.ErrorInfo = "获取角色列表异常！";
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SetEmpRoles(string EmpId, string Roles)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(EmpId))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }
            List<string> Roleslist = new List<string>();
            if (!string.IsNullOrEmpty(Roles))
            {
                Roleslist = Roles.Split(',').ToList();
            }
            brdv = CheckCookie("AddEmpRoles");
            if (!brdv.success)
                return brdv;
            try
            {
                BLL.RBAC.RBAC_EmplRoles bllemproles = new BLL.RBAC.RBAC_EmplRoles();
                brdv = bllemproles.SetEmpRoles(EmpId, Roleslist);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("SetEmpRoles.异常：" + e.ToString());
                brdv.ErrorInfo = "获取角色列表异常！";
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SetEmpPWD(string EmpId, string Pwd)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(EmpId))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }
            if (string.IsNullOrEmpty(Pwd))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }

            brdv = CheckCookie("SetEmpPWD");
            if (!brdv.success)
                return brdv;
            try
            {
                ZhiFang.BLL.RBAC.RBAC_Users blluser = new BLL.RBAC.RBAC_Users();
                brdv = blluser.SetEmpPWD(EmpId, Pwd);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("SetEmpPWD.异常：" + e.ToString());
                brdv.ErrorInfo = "获取角色列表异常！";
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue GetUserByEmpId(string EmpId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(EmpId))
            {
                brdv.ErrorInfo = "参数错误！";
                brdv.success = false;
                return brdv;
            }

            brdv = CheckCookie("GetUserByEmpId");
            if (!brdv.success)
                return brdv;
            try
            {
                ZhiFang.BLL.RBAC.RBAC_Users blluser = new BLL.RBAC.RBAC_Users();
                var emp = blluser.GetModelList(" EmpId= " + EmpId);
                if (emp == null || emp.Count <= 0)
                {
                    brdv.ErrorInfo = "未能获取账户信息！";
                    brdv.success = false;
                    return brdv;
                }
                brdv.success = true;
                brdv.ResultDataValue = "{\"Account\":\"" + emp[0].Account + "\"}";
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("AddEmpRoles.异常：" + e.ToString());
                brdv.ErrorInfo = "获取角色列表异常！";
                brdv.success = false;
                return brdv;
            }
        }


        public BaseResultDataValue RBAC_GetModuleTreeGird()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
                DataSet ds = ibm.GetListByPage("", 1, 100000, " SN asc");
                EntityListEasyUI<ModulesVO> entitymoduleList = new EntityListEasyUI<ModulesVO>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int count = ibm.GetRecordCount("");
                    var modulelist = ibm.DataTableToList(ds.Tables[0]);
                    List<Model.ModulesVO> moduleVOlist = new List<ModulesVO>();
                    foreach (var module in modulelist)
                    {
                        ModulesVO vo = new ModulesVO() { ID = module.ID, CName = module.CName, SN = module.SN, SName = module.SName, ModuleCode = module.ModuleCode, URL = module.URL };
                        if (modulelist.Count(a => a.SN == module.SN.Substring(0, module.SN.Length - 2)) > 0)
                        {
                            vo.PSN = modulelist.Where(a => a.SN == module.SN.Substring(0, module.SN.Length - 2)).First().SN;
                            vo._parentId = modulelist.Where(a => a.SN == module.SN.Substring(0, module.SN.Length - 2)).First().ID;
                        }
                        moduleVOlist.Add(vo);
                    }
                    entitymoduleList.total = count;
                    entitymoduleList.rows = moduleVOlist;
                }
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entitymoduleList);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetModuleTreeGird.异常：" + e.ToString());
                return new BaseResultDataValue() { success = false, ErrorInfo = "程序异常！" };
            }
        }

        public BaseResultDataValue RBAC_GetRoleModuleByRoleId(string RoleId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(RoleId))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }
                BLL.RBAC.RBAC_RoleModuleLink bllrmlink = new BLL.RBAC.RBAC_RoleModuleLink();
                //string Sort = (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order)) ? sort + " " + order : null;
                DataSet ds = bllrmlink.GetListByPage(" RoleId=" + RoleId, " ModuleSN asc", 1, 100000);
                List<RBAC_RoleModuleLinkModel> entitymoduleList = new List<RBAC_RoleModuleLinkModel>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    entitymoduleList = bllrmlink.DataTableToList(ds.Tables[0]);
                }
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entitymoduleList);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_GetRoleModuleByRoleId.异常：" + e.ToString());
                return new BaseResultDataValue() { success = false, ErrorInfo = "程序异常！" };
            }
        }

        public BaseResultDataValue RBAC_SaveRoleModule(string RoleId, List<string> ModuleIlist)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(RoleId))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }
                if (ModuleIlist == null || ModuleIlist.Count <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }
                BLL.RBAC.RBAC_RoleModuleLink bllrmlink = new BLL.RBAC.RBAC_RoleModuleLink();
                IBLL.Common.BaseDictionary.IBModules ibm = BLLFactory.BLLFactory<IBLL.Common.BaseDictionary.IBModules>.GetBLL();
                DataSet dsmoduleall = ibm.GetAllList();
                if (dsmoduleall == null || dsmoduleall.Tables.Count <= 0 || dsmoduleall.Tables[0].Rows.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("SaveRoleModule.。");
                    return new BaseResultDataValue() { success = false, ErrorInfo = "未能查找到所有模块！" };
                }
                List<Model.Modules> modulealllist = ibm.DataTableToList(dsmoduleall.Tables[0]);
                brdv.success = bllrmlink.SaveRoleModule(RoleId, ModuleIlist, modulealllist);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RBAC_SaveRoleModule.异常：" + e.ToString());
                return new BaseResultDataValue() { success = false, ErrorInfo = "程序异常！" };
            }
        }

        public BaseResultDataValue GetUserInfoByEmpId(string EmpId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(EmpId))
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }

                BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();

                BLL.RBAC.RBAC_Users blluser = new BLL.RBAC.RBAC_Users();

                var HR_Employees = bllemp.GetModel(int.Parse(EmpId));

                var RBAC_Users = blluser.GetModelList( " empid= "+EmpId);
                dynamic UserInfo = new ExpandoObject();
                if (HR_Employees != null)
                {                   
                    UserInfo.EmpInfo = HR_Employees;
                }
                if (RBAC_Users != null&& RBAC_Users.Count>0)
                {
                    UserInfo.User = RBAC_Users[0];
                }
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(UserInfo);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetUserInfoByEmpId.异常：" + e.ToString());
                return new BaseResultDataValue() { success = false, ErrorInfo = "程序异常！" };
            }
        }
        public BaseResultDataValue SetUserInfoByEmpId(HR_Employees entity)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if(entity==null)
                {
                    return new BaseResultDataValue() { ErrorInfo = "参数错误！", success = false };
                }
                if (entity.ID <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "人员参数错误！", success = false };
                }

                BLL.RBAC.HR_Employees bllemp = new BLL.RBAC.HR_Employees();

                BLL.RBAC.RBAC_Users blluser = new BLL.RBAC.RBAC_Users();

                var HR_Employees = bllemp.Update(entity);

                //var RBAC_Users = blluser.UpdateAccount("  ")
                //dynamic UserInfo = new ExpandoObject();
                //if (HR_Employees != null)
                //{
                //    UserInfo.EmpInfo = HR_Employees;
                //}
                //if (RBAC_Users != null && RBAC_Users.Count > 0)
                //{
                //    UserInfo.User = RBAC_Users[0];
                //}
               // brdv.success = true;
                brdv = bllemp.Update(entity);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ". SetUserInfoByEmpIdUrl.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
    }
}
