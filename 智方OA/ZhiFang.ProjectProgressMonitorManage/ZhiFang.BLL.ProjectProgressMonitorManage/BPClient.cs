using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using System.IO;
using System.Data;
using System.Reflection;
using ZhiFang.ProjectProgressMonitorManage.Common;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPClient : BaseBLL<PClient>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPClient
    {
        public IDPSalesManClientLinkDao IDPSalesManClientLinkDao { get; set; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IBLL.ProjectProgressMonitorManage.IBBParameter IBBParameter { get; set; }
        public override bool Add()
        {
            long clientNo = ((IDPClientDao)base.DBDao).GetNextMaxClientNo();
            //ZhiFang.Common.Log.Log.Debug("clientNo:" + clientNo);
            if (clientNo > 0) this.Entity.ClientNo = clientNo;

            long licencecode = ((IDPClientDao)base.DBDao).GetNextMaxLicenceCode();
            //ZhiFang.Common.Log.Log.Debug("clientNo:" + clientNo);
            if (licencecode > 0) this.Entity.LicenceCode = licencecode.ToString();
            return base.Add();
        }

        public EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, int page, int limit, long salesManId, bool isOwn)
        {
            return SearchPClientByHQLAndSalesManId(where, null, page, limit, salesManId, isOwn);
        }

        public EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, int page, int limit, long salesManId, bool isOwn,long OwnId)
        {
            return SearchPClientByHQLAndSalesManId(where, null, page, limit, salesManId, isOwn, OwnId);
        }       

        public EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, string sort, int page, int limit, long salesManId, bool isOwn)
        {
            IList<PSalesManClientLink> PSMCLList = IDPSalesManClientLinkDao.GetListByHQL(" SalesManID = " + salesManId + " and PClientID is not null ");
            EntityList<PClient> ELPClient = new EntityList<PClient>();
            if (isOwn)
            {
                if (PSMCLList.Count > 0)
                {
                    List<string> clientida = new List<string>();
                    foreach (var psmcl in PSMCLList)
                    {
                        clientida.Add(psmcl.PClientID.ToString());
                    }
                    if (where != null && where.Trim() != "")
                    {
                        where = where + " and Id in (" + string.Join(",", clientida.ToArray()) + ") ";
                    }
                    else
                    {
                        where = " Id in (" + string.Join(",", clientida.ToArray()) + ") ";
                    }                    
                    if (sort != null && sort != "")
                    {
                        ELPClient = base.SearchListByHQL(where, sort, page, limit);
                    }
                    else
                    {
                        ELPClient = base.SearchListByHQL(where, page, limit);
                    }

                    if (ELPClient == null || ELPClient.list.Count < 0)
                    {                       
                        return null;
                    }
                }
            }
            else
            {                
                if (sort != null && sort != "")
                {
                    ELPClient = base.SearchListByHQL(where, sort, page, limit);
                }
                else
                {
                    ELPClient = base.SearchListByHQL(where, page, limit);
                }                
            }
            if (PSMCLList!= null && PSMCLList.Count > 0)
            {
                if (ELPClient != null && ELPClient.list.Count > 0)
                {
                    foreach (var pclient in ELPClient.list)
                    {
                        var tmp = PSMCLList.Where(a => a.PClientID == pclient.Id);
                        if (tmp != null && tmp.Count() > 0)
                        {
                            pclient.PSalesManClientLinkID = tmp.ElementAt(0).Id;
                        }
                    }                    
                }
                else
                {
                    return null;
                }
            }
            return ELPClient;
        }

        public EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, string sort, int page, int limit, long salesManId, bool isOwn, long OwnId)
        {
            var rolelist = IDRBACEmpRolesDao.GetListByHQL(" HREmployee.Id= " + OwnId);
            if (rolelist != null && rolelist.Count > 0)
            {
                var tmprl = rolelist.Where(a => a.RBACRole.Id.ToString() == RoleList.总经理.Key || a.RBACRole.Id.ToString() == RoleList.副总经理.Key || a.RBACRole.Id.ToString() == RoleList.商务经理.Key);
                if (tmprl != null && tmprl.Count() > 0)
                {
                    if (sort != null && sort != "")
                        return this.SearchPClientByHQLAndSalesManId(where, sort, page, limit, salesManId, isOwn);
                    else
                        return this.SearchPClientByHQLAndSalesManId(where, page, limit, salesManId, isOwn);
                }
            }

            IList<PSalesManClientLink> PSMCLList_O = IDPSalesManClientLinkDao.GetListByHQL(" SalesManID = " + OwnId + " and PClientID is not null ");

            IList<PSalesManClientLink> PSMCLList_S = IDPSalesManClientLinkDao.GetListByHQL(" SalesManID = " + salesManId + " and PClientID is not null ");

            if (PSMCLList_O.Count > 0)
            {
                List<string> clientida = new List<string>();
                foreach (var psmcl in PSMCLList_O)
                {
                    clientida.Add(psmcl.PClientID.ToString());
                }
                string hql1 = "";
                if (where != null && where.Trim() != "")
                {
                    hql1 = where + " and Id in (" + string.Join(",", clientida.ToArray()) + ") ";
                }
                else
                {
                    hql1 = " Id in (" + string.Join(",", clientida.ToArray()) + ") ";
                }
                EntityList<PClient> ELPClient = new EntityList<PClient>();
                if (sort != null && sort != "")
                {
                    ELPClient = base.SearchListByHQL(hql1, sort, page, limit);
                }
                else
                {
                    ELPClient = base.SearchListByHQL(hql1, page, limit);
                }
                //ZhiFang.Common.Log.Log.Debug("dddELPClient.count:" + ELPClient.count + "; ELPClient.list.Count:" + ELPClient.list.Count + ";");
                if (ELPClient != null && ELPClient.list.Count > 0)
                {
                    if (PSMCLList_S != null && PSMCLList_S.Count > 0)
                    {
                        List<string> clientidb = new List<string>();
                        if (isOwn)
                        {
                            foreach (var psmcl in PSMCLList_S)
                            {
                                clientidb.Add(psmcl.PClientID.ToString());
                            }
                            string hql2 = "";
                            if (where != null && where.Trim() != "")
                            {
                                hql2 = where + " and Id in (" + string.Join(",", clientidb.ToArray()) + ") "+ " and Id in (" + string.Join(",", clientida.ToArray()) + ")";
                            }
                            else
                            {
                                hql2 = " Id in (" + string.Join(",", clientidb.ToArray()) + ") and Id in (" + string.Join(",", clientida.ToArray()) + ")";
                            }
                            if (sort != null && sort != "")
                            {
                                ELPClient = base.SearchListByHQL(hql2, sort, page, limit);
                            }
                            else
                            {
                                ELPClient = base.SearchListByHQL(hql2, page, limit);
                            }

                            foreach (var pclient in ELPClient.list)
                            {
                                var tmp = PSMCLList_S.Where(a => a.PClientID == pclient.Id);
                                if (tmp != null && tmp.Count() > 0)
                                {
                                    pclient.PSalesManClientLinkID = tmp.ElementAt(0).Id;
                                }
                            }

                            //int sum = 0;//计数器
                            //for(int i= ELPClient.list.Count-1; i >=0;i--)
                            //{
                            //    var tmp = PSMCLList_S.Where(a => a.PClientID == ELPClient.list[i].Id);
                            //    if (tmp != null && tmp.Count() > 0)
                            //    {
                            //        ELPClient.list[i].PSalesManClientLinkID = tmp.ElementAt(0).Id;
                            //    }
                            //    else
                            //    {
                            //        ELPClient.list.RemoveAt(i);
                            //        sum++;
                            //        ZhiFang.Common.Log.Log.Debug("cccELPClient.count:" + ELPClient.count + "; ELPClient.list.Count:" + ELPClient.list.Count + ";sum:" + sum);
                            //    }
                            //}
                            //ZhiFang.Common.Log.Log.Debug("aaaELPClient.count:"+ ELPClient.count + "; ELPClient.list.Count:" + ELPClient.list.Count + ";sum:"+ sum);
                            //ELPClient.count = ELPClient.count - sum;
                            //ZhiFang.Common.Log.Log.Debug("bbbELPClient.count:" + ELPClient.count + "; ELPClient.list.Count:" + ELPClient.list.Count + ";sum:" + sum);
                        }
                        else
                        {
                            foreach (var pclient in ELPClient.list)
                            {
                                var tmp = PSMCLList_S.Where(a => a.PClientID == pclient.Id);
                                if (tmp != null && tmp.Count() > 0)
                                {
                                    pclient.PSalesManClientLinkID = tmp.ElementAt(0).Id;
                                }
                            }
                        }
                        return ELPClient;
                    }
                    else
                    {
                        if (isOwn)
                        {
                            return null;
                        }
                        else
                        {
                            return ELPClient;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 客户信息Excel导出
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetPClientExportExcel(string where, ref string fileName, string type, int page, int limit, string fields, string sort, bool isPlanish, long SalesManId, bool IsOwn)
        {
            FileStream fileStream = null;
            IList<PClient> atemCount = new List<PClient>();
            EntityList<PClient> tempEntityList = new EntityList<PClient>();
            //导出客户信息
            if (type == "PClient")
            {
                if (sort != null && sort != "")
                {
                    tempEntityList = SearchListByHQL(where, sort, page, limit);
                }
                else
                {
                    tempEntityList = SearchListByHQL(where, page, limit);
                }
            }
            else
            {
                //导出客户关系
                if (SalesManId == 0)
                {
                    SalesManId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                    ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPClientByHQLAndSalesManId:SalesManId为0取当前登录者ID=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) + "为SalesManId");
                }
                long OwnId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));

                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = SearchPClientByHQLAndSalesManId(where, sort, page, limit, SalesManId, IsOwn, OwnId);
                }
                else
                {
                    tempEntityList = SearchPClientByHQLAndSalesManId(where, page, limit, SalesManId, IsOwn, OwnId);
                }
            }

            if (tempEntityList != null)
            {
                atemCount = tempEntityList.list;
            }
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = ExportExcelPClientToDataTable<PClient>(atemCount);

                string strHeaderText = fileName;
                fileName = strHeaderText + ".xlsx";

                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNo.ExcelExportSavePath.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpAttendanceEventLogDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "PClient\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                //Common.Log.Log.Debug("Excel文件导出保存路径及名称为:" + filePath);
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    bool result = MyNPOIHelper.ExportDTtoExcel(dtSource, strHeaderText, filePath);
                    if (result)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            }
            return fileStream;
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        public static DataTable ExportExcelPClientToDataTable<T>(IList<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> removeList = new List<string>();
            foreach (PropertyInfo prop in props)
            {

                Type t = GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "ClientNo":
                        columnName = "用户编号";
                        break;
                    case "Name":
                        columnName = "名称";
                        break;
                    case "Principal":
                        columnName = "负责人";
                        break;
                    case "Bman":
                        columnName = "业务员";
                        break;
                    case "LinkMan":
                        columnName = "联系人";
                        break;
                    case "PhoneNum":
                        columnName = "联系电话";
                        break;
                    case "PhoneNum2":
                        columnName = "联系电话2";
                        break;
                    case "Address":
                        columnName = "地址";
                        break;
                    case "MailNo":
                        columnName = "邮编";
                        break;
                    case "Emall":
                        columnName = "电子邮件";
                        break;
                    case "Shortcode":
                        columnName = "快捷码";
                        break;
                    case "PinYinZiTou":
                        columnName = "拼音字头";
                        break;
                    case "CountryName":
                        columnName = "国家";
                        break;
                    case "ProvinceName":
                        columnName = "省份";
                        break;
                    case "CityName":
                        columnName = "城市";
                        break;
                    case "ClientAreaName":
                        columnName = "区域";
                        break;
                    case "ClientTypeName":
                        columnName = "客户类型";
                        break;
                    case "HospitalTypeName":
                        columnName = "医院类别";
                        break;
                    case "HospitalLevelName":
                        columnName = "医院等级";
                        break;
                    case "SName":
                        columnName = "简称";
                        break;
                    case "LicenceClientName":
                        columnName = "授权名称";
                        break;
                    case "LicenceCode":
                        columnName = "授权编码";
                        break;

                    case "LRNo1":
                        columnName = "主服务器授权号";
                        break;
                    case "LRNo2":
                        columnName = "备份服务器授权号";
                        break;
                    case "ProjectSourceName":
                        columnName = "客户来源";
                        break;
                    case "AgentName":
                        columnName = "代理商";
                        break;
                    case "Url":
                        columnName = "网址";
                        break;
                
                    case "Comment":
                        columnName = "备注";
                        break;
                    default:
                        removeList.Add(columnName);
                        break;
                }

                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }
            if (tb != null)
            {
                tb.Columns["用户编号"].SetOrdinal(0);
                tb.Columns["名称"].SetOrdinal(1);
                tb.Columns["区域"].SetOrdinal(2);
                tb.Columns["客户类型"].SetOrdinal(3);
                tb.Columns["医院类别"].SetOrdinal(4);
                tb.Columns["医院等级"].SetOrdinal(5);
                tb.Columns["负责人"].SetOrdinal(6);
                tb.Columns["业务员"].SetOrdinal(7);
                tb.Columns["联系人"].SetOrdinal(8);
                tb.Columns["联系电话"].SetOrdinal(9);
                tb.Columns["联系电话2"].SetOrdinal(10);
                tb.Columns["地址"].SetOrdinal(11);
                tb.Columns["邮编"].SetOrdinal(12);
                tb.Columns["电子邮件"].SetOrdinal(13);
                tb.Columns["国家"].SetOrdinal(14);
                tb.Columns["省份"].SetOrdinal(15);
                tb.Columns["城市"].SetOrdinal(16);
                tb.Columns["简称"].SetOrdinal(17);
                tb.Columns["快捷码"].SetOrdinal(18);
                tb.Columns["拼音字头"].SetOrdinal(19);
                tb.Columns["授权名称"].SetOrdinal(20);
                tb.Columns["授权编码"].SetOrdinal(21);
                tb.Columns["主服务器授权号"].SetOrdinal(22);
                tb.Columns["备份服务器授权号"].SetOrdinal(23);
                tb.Columns["客户来源"].SetOrdinal(24);
                tb.Columns["代理商"].SetOrdinal(25);
                //tb.Columns["合约用户"].SetOrdinal(24);
                tb.Columns["网址"].SetOrdinal(26);
                tb.Columns["备注"].SetOrdinal(27);
                //排序
                tb.DefaultView.Sort = "名称 desc";
                tb = tb.DefaultView.ToTable();

            }
            return tb;
        }
    }
}