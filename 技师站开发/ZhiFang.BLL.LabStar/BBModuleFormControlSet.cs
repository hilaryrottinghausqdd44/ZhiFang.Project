
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBModuleFormControlSet : BaseBLL<BModuleFormControlSet>, IBBModuleFormControlSet
    {
        IDBModuleFormControlListDao IDBModuleFormControlListDao { get; set; }

        public bool AddBModuleFormControlSets(List<BModuleFormControlSet> bModuleFormControlSets)
        {
            var flag = true;
            foreach (var item in bModuleFormControlSets)
            {
                flag = DBDao.Save(item);
            }
            return flag;
        }

        public bool EditBModuleFormControlSets(List<BModuleFormControlSetVO> bModuleFormControlSetVOs)
        {
            var flag = true;
            foreach (var item in bModuleFormControlSetVOs)
            {
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(item.BModuleFormControlSet, item.fields);
                if (tempArray != null)
                {
                    flag = DBDao.Update(tempArray);
                }
            }
            return flag;
        }

        public List<BModuleFormControlList> SearchBModuleFormControlSetListByFormCode(string FormCode, string sort)
        {
            var ConSets = DBDao.GetListByHQL("FormCode='" + FormCode + "'");
            var FormConSets = ConSets.Where(a => a.IsUse);
            //如果Set表里的数据为 不使用状态 返回空
            if (ConSets.Count != 0 && FormConSets.Count() == 0)
            {
                return null;
            }
            List<long> ids = new List<long>();
            string listwhere = "FormCode='" + FormCode + "'";
            if (FormConSets.Count() != 0)
            {
                FormConSets.ToList().ForEach(a => ids.Add(a.FormControlID));
                listwhere = " Id in (" + string.Join(",", ids) + ")";
            }

            var FormControlLists = IDBModuleFormControlListDao.GetListByHQL(listwhere);
            List<BModuleFormControlList> controlLists = new List<BModuleFormControlList>();
            //list表的数据过滤 只显示 isuse=TRUE的
            foreach (var citem in FormControlLists.Where(a => a.IsUse == true))
            {
                BModuleFormControlList entity = new BModuleFormControlList();
                entity = citem;
                var consets = FormConSets.Where(a => a.FormControlID == citem.Id);
                if (consets.Count() > 0)
                {
                    var sitem = consets.First();
                    entity.DefaultValue = sitem.DefaultValue;
                    entity.Label = sitem.Label;
                    entity.DispOrder = sitem.DispOrder;
                    entity.IsUse = sitem.IsUse;
                    entity.IsDisplay = sitem.IsDisplay;
                    entity.IsReadOnly = sitem.IsReadOnly;
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
            var bModuleFormControlLists = ZhiFang.LabStar.Common.DataTableCommon<BModuleFormControlList>.FillModel(dv.ToTable());

            return bModuleFormControlLists;
        }
    }
}