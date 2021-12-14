
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using System.Data;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBModuleGridControlSet : BaseBLL<BModuleGridControlSet>, IBBModuleGridControlSet
    {
        IDBModuleGridControlListDao IDBModuleGridControlListDao { get; set; }

        public bool AddBModuleGridControlSets(List<BModuleGridControlSet> bModuleGridControlSets)
        {
            var flag = true;
            foreach (var item in bModuleGridControlSets)
            {
                flag = DBDao.Save(item);
            }
            return flag;
        }

        public bool EditBModuleGridControlSets(List<BModuleGridControlSetVO> bModuleGridControlSetVOs)
        {
            var flag = true;
            foreach (var item in bModuleGridControlSetVOs)
            {
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(item.BModuleGridControlSet, item.fields);
                if (tempArray != null)
                {
                    flag = DBDao.Update(tempArray);
                }
            }
            return flag;
        }

        public List<BModuleGridControlList> SearchBModuleGridControlListByGridCode(string GridCode, string sort)
        {
            var GridSets = DBDao.GetListByHQL("GridCode='" + GridCode + "'");
            var ModuleGridSets = GridSets.Where(a => a.IsUse == true);
            //如果Set表里的数据为 不使用状态 返回空
            if (GridSets.Count != 0 && ModuleGridSets.Count() == 0)
            {
                return null;
            }
            List<long> ids = new List<long>();
            string listwhere = "GridCode='" + GridCode + "'";
            if (ModuleGridSets.Count() != 0)
            {
                ModuleGridSets.ToList().ForEach(a => ids.Add(a.GridControlID));
                listwhere = "Id in (" + string.Join(",", ids) + ")";
            }

            var ModuleGridControls = IDBModuleGridControlListDao.GetListByHQL(listwhere);
            List<BModuleGridControlList> controlLists = new List<BModuleGridControlList>();
            //list表的数据过滤 只显示 isuse=TRUE的
            foreach (var citem in ModuleGridControls.Where(a => a.IsUse == true))
            {
                BModuleGridControlList entity = new BModuleGridControlList();
                entity = citem;
                var gridsets = ModuleGridSets.Where(a => a.GridControlID == citem.Id);
                if (gridsets.Count() > 0)
                {
                    var sitem = gridsets.First();
                    entity.ColName = sitem.ColName;
                    entity.IsOrder = sitem.IsOrder;
                    entity.IsHide = sitem.IsHide;
                    entity.DispOrder = sitem.DispOrder;
                    entity.IsUse = sitem.IsUse;
                    entity.Width = sitem.Width;
                }
                controlLists.Add(entity);
            }

            if (controlLists.Count == 0)
            {
                return null;
            }
            var datatable = ZhiFang.LabStar.Common.ObjectToDataSet.EntityConvetDataTable(controlLists);
            var dv = datatable.DefaultView;
            dv.Sort = sort;
            var bModuleGridControlLists = ZhiFang.LabStar.Common.DataTableCommon<BModuleGridControlList>.FillModel(dv.ToTable());
            return bModuleGridControlLists;
        }
    }
}