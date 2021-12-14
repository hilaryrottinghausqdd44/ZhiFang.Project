using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaCenOrg : BaseBLL<ReaCenOrg>, ZhiFang.IBLL.ReagentSys.Client.IBReaCenOrg
    {
        public BaseResultBool EditVerification(ReaCenOrg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (entity.PlatformOrgNo.HasValue)
                {
                    IList<ReaCenOrg> tempList = this.SearchListByHQL(string.Format("reacenorg.PlatformOrgNo={0} and reacenorg.Id!={1} and reacenorg.OrgType={2}", entity.PlatformOrgNo.Value, entity.Id, entity.OrgType.Value));
                    if (tempList != null && tempList.Count > 0)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = string.Format("平台机构码为{0},已存在,请不要重复维护!", entity.PlatformOrgNo.Value);
                        return baseResultBool;
                    }
                }
            }
            return baseResultBool;
        }
        public override bool Add()
        {
            int orgNo = 0;
            if (!this.Entity.OrgNo.HasValue)
                this.Entity.OrgNo = orgNo;
            if (this.Entity.OrgNo <= 0)
                orgNo = ((IDReaCenOrgDao)base.DBDao).GetMaxOrgNo();
            if (this.Entity.OrgNo.Value < orgNo)
                this.Entity.OrgNo = orgNo;
            //如果为本地供应商,其所属机构平台编码(PlatformOrgNo)等于机构编码(OrgNo)
            if (!this.Entity.PlatformOrgNo.HasValue)
                this.Entity.PlatformOrgNo = this.Entity.OrgNo;
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        #region 获取机构树信息
        public BaseResultTree SearchReaCenOrgTreeByOrgID(long id, int orgType)
        {
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (id > 0)
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.Id={1}", orgType, id);
                else
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.POrgID={1}", orgType, id);
                EntityList<ReaCenOrg> tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (ReaCenOrg tempModel in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = id.ToString();
                        //tempTree.objectType = tempReaCenOrg.GetType().Name;
                        tempTree.objectType = "ReaCenOrg";
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTree(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
                tempBaseResultTree.Tree = tempListTree;
                tempBaseResultTree.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }
        public List<tree> GetChildTree(long parentID)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                EntityList<ReaCenOrg> tempEntityList = this.SearchListByHQL(" reacenorg.POrgID=" + parentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (ReaCenOrg tempModel in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = parentID.ToString();
                        tempTree.objectType = "ReaCenOrg";
                        tempTree.Tree = GetChildTree(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree;
        }
        public BaseResultTree<ReaCenOrg> SearchReaCenOrgListTreeByOrgID(long id, int orgType)
        {
            BaseResultTree<ReaCenOrg> tempBaseResultTree = new BaseResultTree<ReaCenOrg>();
            EntityList<ReaCenOrg> tempEntityList = new EntityList<ReaCenOrg>();
            try
            {
                string tempWhereStr = "";
                if (id > 0)
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.Id={1}", orgType, id);
                else
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.POrgID={1}", orgType, id);
                List<tree<ReaCenOrg>> tempListTree = new List<tree<ReaCenOrg>>();
                ;
                tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (ReaCenOrg tempModel in tempEntityList.list)
                    {
                        tree<ReaCenOrg> tempTree = new tree<ReaCenOrg>();
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = id.ToString();
                        tempTree.objectType = "ReaCenOrg";
                        tempTree.value = tempModel;
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTreeList(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
                tempBaseResultTree.Tree = tempListTree;
                tempBaseResultTree.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }
        public tree<ReaCenOrg>[] GetChildTreeList(long parentID)
        {
            List<tree<ReaCenOrg>> tempListTree = new List<tree<ReaCenOrg>>();
            try
            {
                EntityList<ReaCenOrg> tempEntityList = this.SearchListByHQL(" reacenorg.POrgID=" + parentID.ToString(), " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (ReaCenOrg tempModel in tempEntityList.list)
                    {
                        tree<ReaCenOrg> tempTree = new tree<ReaCenOrg>();
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = parentID.ToString();
                        tempTree.objectType = "ReaCenOrg";
                        tempTree.value = tempModel;
                        tempTree.Tree = GetChildTreeList(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree.ToArray();
        }
        #endregion
        #region 列表数据格式
        public EntityList<ReaCenOrg> SearchReaCenOrgAndChildListByHQL(string where, string sort, int page, int limit, bool isSearchChild)
        {
            EntityList<ReaCenOrg> tempEntityList = new EntityList<ReaCenOrg>();
            if (string.IsNullOrEmpty(sort))
                sort = "reacenorg.POrgID ASC and reacenorg DispOrder ASC";
            EntityList<ReaCenOrg> tempEntityList2 = this.SearchListByHQL(where, sort, 0, 0);
            IList<ReaCenOrg> tempList = tempEntityList2.list;

            List<ReaCenOrg> tempResultList = new List<ReaCenOrg>();
            if (isSearchChild == true)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    ReaCenOrg tempEntity = tempList[i];
                    List<ReaCenOrg> tempChildList = GetChildList(tempEntity.Id, sort);
                    if ((tempChildList != null) && (tempChildList.Count > 0))
                    {
                        if (!tempResultList.Contains(tempEntity))
                            tempResultList.Add(tempEntity);
                        foreach (var item in tempChildList)
                        {
                            if (!tempResultList.Contains(item))
                                tempResultList.Add(item);
                        }
                    }
                    else
                    {
                        if (!tempResultList.Contains(tempEntity))
                            tempResultList.Add(tempEntity);
                    }
                }
            }
            else
            {
                tempResultList = (List<ReaCenOrg>)tempList;
                tempResultList = tempResultList.Distinct().ToList();
            }

            tempEntityList.count = tempResultList.Count;
            //分页处理
            if (tempResultList.Count > 0)
            {
                tempResultList = tempResultList.OrderBy(s => s.POrgID).OrderBy(s => s.DispOrder).ToList();
                if (limit > 0 && limit < tempResultList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = tempResultList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        tempResultList = list.ToList();
                    }
                }
            }
            tempEntityList.list = tempResultList;
            if (tempEntityList.list == null) tempEntityList.list = new List<ReaCenOrg>();
            return tempEntityList;
        }
        public List<ReaCenOrg> GetChildList(long ParentID, string sort)
        {
            List<ReaCenOrg> tempList = new List<ReaCenOrg>();
            try
            {
                EntityList<ReaCenOrg> tempEntityList2 = this.SearchListByHQL(" reacenorg.POrgID=" + ParentID, sort, 0, 0);
                IList<ReaCenOrg> tempChildList = tempEntityList2.list;

                if ((tempChildList != null) && (tempChildList.Count > 0))
                {
                    for (int i = 0; i < tempChildList.Count; i++)
                    {
                        ReaCenOrg tempEntity = tempChildList[i];
                        List<ReaCenOrg> tempChild2 = GetChildList(tempEntity.Id, sort);
                        if ((tempChild2 != null) && (tempChild2.Count > 0))
                        {
                            if (!tempList.Contains(tempEntity))
                                tempList.Add(tempEntity);
                            foreach (var item in tempChild2)
                            {
                                if (!tempList.Contains(item))
                                    tempList.Add(item);
                            }
                        }
                        else
                        {
                            if (!tempList.Contains(tempEntity))
                                tempList.Add(tempEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempList;
        }
        #endregion

        public string SearchOrgIDStrListByOrgID(long orgId, int orgType)
        {
            string orgIDStr = "";
            IList<ReaCenOrg> tempList = new List<ReaCenOrg>();
            try
            {
                string tempWhereStr = "";
                if (orgId > 0)
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.Id={1}", orgType, orgId);
                else
                    tempWhereStr = string.Format(" reacenorg.OrgType={0} and reacenorg.POrgID={1}", orgType, orgId);
                EntityList<ReaCenOrg> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (ReaCenOrg tempModel in tempEntityList.list)
                    {
                        IList<ReaCenOrg> tempList2 = GetChildList(tempModel.Id, "");
                        tempList = tempList.Union(tempList2).ToList();
                        if (!tempList.Contains(tempModel))
                            tempList.Add(tempModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (tempList.Count > 0)
            {
                foreach (ReaCenOrg dept in tempList)
                {
                    if (string.IsNullOrEmpty(orgIDStr))
                        orgIDStr = dept.Id.ToString();
                    else
                        orgIDStr += "," + dept.Id.ToString();
                }
            }
            return orgIDStr;
        }

        public BaseResultData AddReaCenOrgSyncByInterface(ReaBmsCenSaleDoc saleDoc, ref ReaCenOrg reaOrg, ref int orgNo)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaCenOrg> listReaCenOrg = this.SearchListByHQL(" reacenorg.MatchCode=\'" + saleDoc.ReaCompCode + "\'", 0, 0);
            bool isEdit = (listReaCenOrg != null && listReaCenOrg.count > 0);
            ReaCenOrg reaCenOrg = null;
            if (isEdit)
                reaCenOrg = listReaCenOrg.list[0];
            else
            {
                reaCenOrg = new ReaCenOrg();
                reaCenOrg.MatchCode = saleDoc.ReaCompCode;
                if (orgNo <= 0)
                    reaCenOrg.OrgNo = ((IDReaCenOrgDao)base.DBDao).GetMaxOrgNo();
                else
                    reaCenOrg.OrgNo = orgNo + 1;
                orgNo = reaCenOrg.OrgNo.Value;
                reaCenOrg.OrgType = int.Parse(ReaCenOrgType.供货方.Key);
                reaCenOrg.POrgID = 0;
                reaCenOrg.POrgNo = 0;
            }
            reaCenOrg.LabID = saleDoc.LabID;
            reaCenOrg.CName = saleDoc.CompanyName;
            reaCenOrg.Address = saleDoc.CompAddress;
            reaCenOrg.Contact = saleDoc.CompContact;
            reaCenOrg.Tel = saleDoc.CompTel;
            reaCenOrg.HotTel = saleDoc.CompHotTel;
            reaCenOrg.Email = saleDoc.CompEmail;
            reaCenOrg.Fox = saleDoc.CompFox;
            reaCenOrg.WebAddress = saleDoc.CompWebAddress;
            reaCenOrg.Visible = 1;
            this.Entity = reaCenOrg;
            saleDoc.CompID = reaCenOrg.Id;
            saleDoc.ReaCompID = reaCenOrg.Id;
            baseResultData.data = reaCenOrg.Id.ToString();
            if (isEdit)
            {
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Edit();
            }
            else
            {
                reaCenOrg.DataAddTime = DateTime.Now;
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Add();
            }
            reaOrg = reaCenOrg;
            return baseResultData;
        }

        public BaseResultData AddReaCenOrgSyncByInterface(ReaBmsCenSaleDtl saleDtl, ref ReaCenOrg reaOrg, ref int orgNo)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaCenOrg> listReaCenOrg = this.SearchListByHQL(" reacenorg.MatchCode=\'" + saleDtl.ReaCompCode + "\'", 0, 0);
            bool isEdit = (listReaCenOrg != null && listReaCenOrg.count > 0);
            ReaCenOrg reaCenOrg = null;
            if (isEdit)
                reaCenOrg = listReaCenOrg.list[0];
            else
            {
                reaCenOrg = new ReaCenOrg();
                reaCenOrg.MatchCode = saleDtl.ReaCompCode;
                if (orgNo <= 0)
                    reaCenOrg.OrgNo = ((IDReaCenOrgDao)base.DBDao).GetMaxOrgNo();
                else
                    reaCenOrg.OrgNo = orgNo + 1;
                orgNo = reaCenOrg.OrgNo.Value;
                reaCenOrg.OrgType = int.Parse(ReaCenOrgType.供货方.Key);
                reaCenOrg.POrgID = 0;
                reaCenOrg.POrgNo = 0;
            }
            reaCenOrg.LabID = saleDtl.LabID;
            reaCenOrg.CName = saleDtl.ReaCompanyName;
            reaCenOrg.Visible = 1;
            this.Entity = reaCenOrg;
            saleDtl.ReaCompID = reaCenOrg.Id;
            baseResultData.data = reaCenOrg.Id.ToString();
            if (isEdit)
            {
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Edit();
            }
            else
            {
                reaCenOrg.DataAddTime = DateTime.Now;
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Add();
            }
            reaOrg = reaCenOrg;
            return baseResultData;
        }

        public BaseResultData AddReaCenOrgSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaCenOrg> listReaCenOrg = this.SearchListByHQL(" reacenorg." + syncField + "=\'" + syncFieldValue + "\'", 0, 0);
            bool isEdit = (listReaCenOrg != null && listReaCenOrg.count > 0);
            ReaCenOrg reaCenOrg = null;
            if (isEdit)
                reaCenOrg = listReaCenOrg.list[0];
            else
                reaCenOrg = new ReaCenOrg();

            foreach (KeyValuePair<string, object> kv in dicFieldAndValue)
            {
                try
                {
                    System.Reflection.PropertyInfo propertyInfo = reaCenOrg.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && kv.Value != null)
                        propertyInfo.SetValue(reaCenOrg, ExcelDataCommon.DataConvert(propertyInfo, kv.Value), null);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("供应商实体属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                }
            }
            reaCenOrg.Visible = 1;
            reaCenOrg.OrgNo = ((IDReaCenOrgDao)base.DBDao).GetMaxOrgNo();
            reaCenOrg.OrgType = int.Parse(ReaCenOrgType.供货方.Key);
            this.Entity = reaCenOrg;
            if (isEdit)
            {
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Edit();
            }
            else
            {
                reaCenOrg.POrgID = 0;
                reaCenOrg.POrgNo = 0;
                reaCenOrg.DataAddTime = DateTime.Now;
                reaCenOrg.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Add();
            }
            return baseResultData;
        }

        public BaseResultData SaveReaCenOrgByMatchInterface(IList<ReaCenOrg> editReaCenOrgList)
        {
            BaseResultData baseResultData = new BaseResultData();

            IList<ReaCenOrg> reaCenOrgAllList = this.LoadAll();
            bool isEdit = false;
            ReaCenOrg serverEntity = null;
            int orgNo = ((IDReaCenOrgDao)base.DBDao).GetMaxOrgNo();
            foreach (ReaCenOrg editEntity in editReaCenOrgList)
            {
                isEdit = false;

                if (string.IsNullOrEmpty(editEntity.MatchCode))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商名称为：" + editEntity.CName + ",物资对照码(MatchCode)值为空");
                    continue;
                }
                var tempList = reaCenOrgAllList.Where(p => p.MatchCode == editEntity.MatchCode);
                if (tempList.Count() > 0)
                {
                    serverEntity = tempList.ElementAt(0);
                    isEdit = true;
                }
                if (isEdit)
                {
                    serverEntity.CName = editEntity.CName;
                    serverEntity.EName = editEntity.EName;
                    serverEntity.DataUpdateTime = DateTime.Now;
                    //serverEntity = ClassMapperHelp.GetMapper<ReaCenOrg, ReaCenOrg>(editEntity);
                    this.Entity = serverEntity;
                    baseResultData.success = this.Edit();
                }
                else
                {
                    editEntity.DataAddTime = DateTime.Now;
                    editEntity.DataUpdateTime = DateTime.Now;
                    editEntity.Visible = 1;
                    editEntity.OrgNo = orgNo;
                    editEntity.PlatformOrgNo = editEntity.OrgNo;
                    editEntity.POrgID = 0;
                    editEntity.POrgNo = 0;
                    editEntity.OrgType = int.Parse(ReaCenOrgType.供货方.Key);
                    this.Entity = editEntity;
                    baseResultData.success = this.Add();
                    orgNo = orgNo + 1;
                }
            }
            return baseResultData;
        }
    }
}