
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.BLL.BloodTransfusion.InterfaceMapingStrategy;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Common;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodInterfaceMaping : BaseBLL<BloodInterfaceMaping>, ZhiFang.IBLL.BloodTransfusion.IBBloodInterfaceMaping
    {
        IDBDictDao IDBDictDao { get; set; }
        IDBloodStyleDao IDBloodStyleDao { get; set; }
        IDBloodBTestItemDao IDBloodBTestItemDao { get; set; }
        IDBloodBagProcessTypeDao IDBloodBagProcessTypeDao { get; set; }
        IDBloodUnitDao IDBloodUnitDao { get; set; }
        IDBloodABODao IDBloodABODao { get; set; }
        IDBloodChargeItemDao IDBloodChargeItemDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }
        IDDepartmentDao IDDepartmentDao { get; set; }
        IBSCOperation IBSCOperation { get; set; }

        public EntityList<BDictMapingVO> GetBDictMapingVOListByHQL(string deveWhere, string sort, int page, int limit, string deveCode, string useCode, string mapingWhere, long objectTypeId)
        {
            EntityList<BDictMapingVO> tempEntityList = new EntityList<BDictMapingVO>();
            //对照字典类型信息
            BDict bobjectType = IDBDictDao.Get(objectTypeId);
            //待新增保存的对照关系集合
            IList<BDictMapingVO> addList = new List<BDictMapingVO>();

            string mapingSort = "";
            string deveSort = "";
            if (!string.IsNullOrEmpty(sort) && sort.ToLower().Contains("bloodinterfacemaping"))
            {
                mapingSort = sort;
            }
            else if (!string.IsNullOrEmpty(sort))
            {
                deveSort = sort;

            }
            //获取已建立的对照关系
            IList<BloodInterfaceMaping> mapingList = new List<BloodInterfaceMaping>();
            if (!string.IsNullOrEmpty(mapingSort))
            {
                mapingList = ((IDBloodInterfaceMapingDao)base.DBDao).GetListByHQL(mapingWhere, mapingSort, -1, -1).list;
            }
            else
            {
                mapingList = ((IDBloodInterfaceMapingDao)base.DBDao).GetListByHQL(mapingWhere);
            }


            IList<BDictMapingVO> ovList = new List<BDictMapingVO>();

            if (deveCode == InterfaceMapingBDict.公共字典.Key)
            {
                long dictTypeId = -1;
                if (!string.IsNullOrEmpty(useCode)) dictTypeId = long.Parse(useCode);

                string bdictHql = " bdict.IsUse=1 and bdict.BDictType.Id=" + dictTypeId;
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BDict> deveList = new List<BDict>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBDictDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBDictDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BDict> dimensionStrategy = new OfBDictMaping();
                DimensionContext<BDict> dimensionContext = new DimensionContext<BDict>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.血制品字典.Key)
            {
                string bdictHql = " bloodstyle.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodStyle> deveList = new List<BloodStyle>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodStyleDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodStyleDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BloodStyle> dimensionStrategy = new OfBloodStyleMaping();
                DimensionContext<BloodStyle> dimensionContext = new DimensionContext<BloodStyle>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.血制品单位.Key)
            {
                string bdictHql = " bloodunit.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodUnit> deveList = new List<BloodUnit>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodUnitDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodUnitDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BloodUnit> dimensionStrategy = new OfBloodUnitMaping();
                DimensionContext<BloodUnit> dimensionContext = new DimensionContext<BloodUnit>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.血型ABO.Key)
            {
                string bdictHql = " bloodabo.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodABO> deveList = new List<BloodABO>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodABODao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodABODao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BloodABO> dimensionStrategy = new OfBloodABOMaping();
                DimensionContext<BloodABO> dimensionContext = new DimensionContext<BloodABO>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.加工类型.Key)
            {
                string bdictHql = " bloodbagprocesstype.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodBagProcessType> deveList = new List<BloodBagProcessType>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodBagProcessTypeDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodBagProcessTypeDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BloodBagProcessType> dimensionStrategy = new OfBloodBagProcessTypeMaping();
                DimensionContext<BloodBagProcessType> dimensionContext = new DimensionContext<BloodBagProcessType>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.检验项目.Key)
            {
                string bdictHql = " bloodbtestitem.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodBTestItem> deveList = new List<BloodBTestItem>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodBTestItemDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodBTestItemDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<BloodBTestItem> dimensionStrategy = new OfBloodBTestItemMaping();
                DimensionContext<BloodBTestItem> dimensionContext = new DimensionContext<BloodBTestItem>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.人员字典对照.Key)
            {
                string bdictHql = " puser.Visible=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<PUser> deveList = new List<PUser>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDPUserDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDPUserDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<PUser> dimensionStrategy = new OfPUserMaping();
                DimensionContext<PUser> dimensionContext = new DimensionContext<PUser>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.科室字典对照.Key)
            {
                string bdictHql = " department.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<Department> deveList = new List<Department>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDDepartmentDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDDepartmentDao.GetListByHQL(bdictHql);
                }
                DimensionStrategy<Department> dimensionStrategy = new OfDepartmentMaping();
                DimensionContext<Department> dimensionContext = new DimensionContext<Department>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            else if (deveCode == InterfaceMapingBDict.费用项目对照.Key)
            {
                string bdictHql = " bloodchargeitem.IsUse=1 ";
                if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                IList<BloodChargeItem> deveList = new List<BloodChargeItem>();
                if (!string.IsNullOrEmpty(deveSort))
                {
                    deveList = IDBloodChargeItemDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                }
                else
                {
                    deveList = IDBloodChargeItemDao.GetListByHQL(bdictHql);
                }

                DimensionStrategy<BloodChargeItem> dimensionStrategy = new OfBloodChargeItemMaping();
                DimensionContext<BloodChargeItem> dimensionContext = new DimensionContext<BloodChargeItem>(deveList, deveCode, bobjectType, dimensionStrategy);
                ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
            }
            // 批量建立对照关系
            if (addList != null && addList.Count > 0)
            {
                bool result = true;
                foreach (var vo in addList)
                {
                    this.Entity = vo.BloodInterfaceMaping;
                    result = this.Add();
                    if (!result)
                    {
                        ZhiFang.Common.Log.Log.Error(string.Format("保存字典类型为:{0},BobjectID为:{1}对照关系失败！", vo.BloodInterfaceMaping.BobjectType.CName, vo.BloodInterfaceMaping.BobjectID));
                    }
                }
            }

            //分页处理
            tempEntityList.count = ovList.Count;
            //分页处理
            if (ovList.Count > 0)
            {
                if (limit > 0 && limit < ovList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = ovList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        ovList = list.ToList();
                    }
                }
            }
            tempEntityList.list = ovList;

            return tempEntityList;
        }

        #region 修改信息记录
        public void AddSCOperation(BloodInterfaceMaping serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<BloodInterfaceMaping>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "BloodInterfaceMaping";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.字典对照记录.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

    }
}