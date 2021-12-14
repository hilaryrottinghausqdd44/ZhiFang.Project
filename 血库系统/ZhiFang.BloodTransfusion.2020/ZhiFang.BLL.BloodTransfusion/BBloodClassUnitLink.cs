
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodClassUnitLink : BaseBLL<BloodClassUnitLink>, ZhiFang.IBLL.BloodTransfusion.IBBloodClassUnitLink
    {
        IDBloodUnitDao IDBloodUnitDao { get; set; }
        IBSCOperation IBSCOperation { get; set; }

        #region 修改信息记录
        public void AddSCOperation(BloodClassUnitLink serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<BloodClassUnitLink>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "BloodClassUnitLink";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.血制品分类单位换算.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

        public EntityList<BloodUnit> SearchBloodUnitByClassUnitLinkHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<BloodUnit> entityList = new EntityList<BloodUnit>();

            IList<BloodClassUnitLink> linkList1 = ((IDBloodClassUnitLinkDao)base.DBDao).GetListByHQL(linkWhere);
            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<BloodUnit> entityList1 = IDBloodUnitDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的费用项目集合信息
                var linkList = (from list2 in linkList1
                                select list2.BloodUnit).ToList<BloodUnit>();
                //比较生成两个序列的差集
                List<BloodUnit> entityList3 = entityList1.Except(linkList).ToList();

                entityList.count = entityList3.Count;
                //进行分页
                if (limit > 0 && limit < entityList3.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = entityList3.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        entityList3 = list.ToList();
                }
                entityList.list = entityList3;
            }
            else
            {
                entityList = IDBloodUnitDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}