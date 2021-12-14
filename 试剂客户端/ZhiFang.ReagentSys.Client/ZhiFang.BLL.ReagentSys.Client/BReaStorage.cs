using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaStorage : BaseBLL<ReaStorage>, ZhiFang.IBLL.ReagentSys.Client.IBReaStorage
    {
        IBReaPlace IBReaPlace { get; set; }

        public BaseResultTree GetReaStorageTree(long employeeID, bool isEmpPermission, string operType)
        {
            BaseResultTree tree = new BaseResultTree();
            tree.Tree = new List<tree>();

            IList<ReaStorage> reaStorageList = new List<ReaStorage>();
            string storageHql = "reastorage.Visible=1", linkHql = "";
            if (isEmpPermission)
            {
                linkHql = "reauserstoragelink.OperID=" + employeeID;
                if (!string.IsNullOrEmpty(operType))
                {
                    linkHql += " and reauserstoragelink.OperType=" + operType;
                }
                reaStorageList = ((IDReaStorageDao)base.DBDao).SearchReaStorageListByStorageAndLinHQL(linkHql, linkHql, operType, "", -1, -1);
            }
            else
            {
                reaStorageList = this.SearchListByHQL(storageHql);
            }

            if (reaStorageList != null && reaStorageList.Count > 0)
            {
                reaStorageList = reaStorageList.OrderBy(p => p.DispOrder).ToList();
                foreach (var item in reaStorageList)
                {
                    tree treePlace = GetReaPlaceTree(item.Id);
                    //当前库房节点
                    treePlace.tid = item.Id.ToString();
                    treePlace.text = item.CName.ToString();
                    treePlace.pid = "0";
                    treePlace.leaf = false;
                    treePlace.iconCls = treePlace.leaf ? "orgImg16" : "orgsImg16";
                    tree.Tree.Add(treePlace);
                }
            }
            return tree;

        }
        public tree GetReaPlaceTree(long storagId)
        {
            tree treeStorage = new tree();
            treeStorage.Tree = new List<tree>();
            IList<ReaPlace> reaPlaceList = new List<ReaPlace>();
            string tempWhereStr = "";
            if (storagId > 0)
                tempWhereStr = " reaplace.ReaStorage.Id=" + storagId.ToString();

            EntityList<ReaPlace> tempEntityList = IBReaPlace.SearchListByHQL(tempWhereStr, -1, -1);
            if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
            {

                foreach (var item in tempEntityList.list)
                {
                    //当前货架节点
                    tree treePlace = new tree();
                    treePlace.tid = item.Id.ToString();
                    treePlace.text = item.CName.ToString();
                    treePlace.pid = storagId.ToString();
                    treePlace.expanded = true;
                    treePlace.leaf = true;
                    treePlace.iconCls = treePlace.leaf ? "orgImg16" : "orgsImg16";
                    treeStorage.Tree.Add(treePlace);
                }
            }
            return treeStorage;
        }
        public IList<ReaStorage> SearchReaStorageListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count)
        {
            if (string.IsNullOrEmpty(storageHql) && string.IsNullOrEmpty(linkHql))
            {
                return new List<ReaStorage>();
            }
            IList<ReaStorage> entityList = new List<ReaStorage>();
            entityList = ((IDReaStorageDao)base.DBDao).SearchReaStorageListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
            //移库申请或出库申请,直接移库源库房
            if (!string.IsNullOrEmpty(operType) && operType != ReaUserStorageLinkOperType.库房管理权限.Key && entityList.Count <= 0)
            {
                //如果没有配置移库申请的库房人员权限或没有配置移库申请的库房人员权限,申请的库房选择范围为全部在用的库房
                linkHql = " reauserstoragelink.OperType=" + operType;
                entityList = ((IDReaStorageDao)base.DBDao).SearchReaStorageListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
                IList<ReaStorage> entityList2 = ((IDReaStorageDao)base.DBDao).SearchReaStorageListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
                //移库申请/出库申请的库房人员权限关系不存在,取全部在用库房信息
                if (entityList2.Count <= 0)
                {
                    entityList = ((IDReaStorageDao)base.DBDao).GetListByHQL(storageHql, page, count).list;
                }
            }
            return entityList;
        }
        /// <summary>
        /// 根据员工权限获取库房信息
        /// </summary>
        /// <param name="storageHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaStorage> SearchListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count)
        {
            if (string.IsNullOrEmpty(storageHql) && string.IsNullOrEmpty(linkHql))
            {
                return new EntityList<ReaStorage>();
            }
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            entityList = ((IDReaStorageDao)base.DBDao).SearchEntityListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
            //移库申请或出库申请,直接移库源库房
            if (!string.IsNullOrEmpty(operType) && operType != ReaUserStorageLinkOperType.库房管理权限.Key && entityList.count <= 0)
            {
                //如果没有配置移库申请的库房人员权限或没有配置移库申请的库房人员权限,申请的库房选择范围为全部在用的库房
                linkHql = " reauserstoragelink.OperType=" + operType;
                IList<ReaStorage> entityList2 = ((IDReaStorageDao)base.DBDao).SearchReaStorageListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
                //移库申请/出库申请的库房人员权限关系不存在,取全部在用库房信息
                if (entityList2.Count <= 0)
                {
                    entityList = this.SearchListByHQL(storageHql, "", page, count);
                }
            }
            return entityList;
        }

        public BaseResultData AddReaStorageSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue)
        {
            BaseResultData baseResultData = new BaseResultData();
            EntityList<ReaStorage> listReaStorage = this.SearchListByHQL(" reastorage." + syncField + "=\'" + syncFieldValue + "\'", 0, 0);
            bool isEdit = (listReaStorage != null && listReaStorage.count > 0);
            ReaStorage ReaStorage = null;
            if (isEdit)
                ReaStorage = listReaStorage.list[0];
            else
                ReaStorage = new ReaStorage();

            foreach (KeyValuePair<string, object> kv in dicFieldAndValue)
            {
                try
                {
                    System.Reflection.PropertyInfo propertyInfo = ReaStorage.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && kv.Value != null)
                        propertyInfo.SetValue(ReaStorage, ExcelDataCommon.DataConvert(propertyInfo, kv.Value), null);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("库房实体属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                }
            }
            if (string.IsNullOrEmpty(ReaStorage.ShortCode))
                ReaStorage.ShortCode = "";//非空属性
            ReaStorage.Visible = true;
            this.Entity = ReaStorage;
            if (isEdit)
            {
                ReaStorage.DataUpdateTime = DateTime.Now;
                baseResultData.success = this.Edit();
            }
            else
            {
                ReaStorage.DataUpdateTime = DateTime.Now;
                ReaStorage.DataAddTime = DateTime.Now;
                baseResultData.success = this.Add();
            }
            baseResultData.data = this.Entity.Id.ToString();
            return baseResultData;
        }

    }
}