using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.IDAO.RBAC;
using ZhiFang.BLL.WebAssist.InterfaceMapingStrategy;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BSCInterfaceMaping : BaseBLL<SCInterfaceMaping>, IBSCInterfaceMaping
    {
        IDBDictDao IDBDictDao { get; set; }
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
            if (!string.IsNullOrEmpty(sort) && sort.ToLower().Contains("SCInterfaceMaping"))
            {
                mapingSort = sort;
            }
            else if (!string.IsNullOrEmpty(sort))
            {
                deveSort = sort;

            }
            //获取已建立的对照关系
            IList<SCInterfaceMaping> mapingList = new List<SCInterfaceMaping>();
            if (!string.IsNullOrEmpty(mapingSort))
            {
                mapingList = ((IDSCInterfaceMapingDao)base.DBDao).GetListByHQL(mapingWhere, mapingSort, -1, -1).list;
            }
            else
            {
                mapingList = ((IDSCInterfaceMapingDao)base.DBDao).GetListByHQL(mapingWhere);
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
            else if (deveCode == InterfaceMapingBDict.检验项目.Key)
            {
                //string bdictHql = " testitem.IsUse=1 ";
                //if (!string.IsNullOrEmpty(deveWhere)) bdictHql += " and " + deveWhere;
                //IList<TestItem> deveList = new List<TestItem>();
                //if (!string.IsNullOrEmpty(deveSort))
                //{
                //    deveList = IDTestItemDao.GetListByHQL(bdictHql, deveSort, -1, -1).list;
                //}
                //else
                //{
                //    deveList = IDTestItemDao.GetListByHQL(bdictHql);
                //}
                //DimensionStrategy<TestItem> dimensionStrategy = new OfTestItemMaping();
                //DimensionContext<TestItem> dimensionContext = new DimensionContext<TestItem>(deveList, deveCode, bobjectType, dimensionStrategy);
                //ovList = dimensionContext.GetBDictMapingVOList(mapingList, ref addList);
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
            // 批量建立对照关系
            if (addList != null && addList.Count > 0)
            {
                bool result = true;
                foreach (var vo in addList)
                {
                    this.Entity = vo.SCInterfaceMaping;
                    result = this.Add();
                    if (!result)
                    {
                        ZhiFang.Common.Log.Log.Error(string.Format("保存字典类型为:{0},BobjectID为:{1}对照关系失败！", vo.SCInterfaceMaping.BobjectType.CName, vo.SCInterfaceMaping.BobjectID));
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
        public void AddSCOperation(SCInterfaceMaping serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<SCInterfaceMaping>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "SCInterfaceMaping";
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
