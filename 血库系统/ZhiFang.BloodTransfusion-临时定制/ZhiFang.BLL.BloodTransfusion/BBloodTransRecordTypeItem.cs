
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using Newtonsoft.Json.Linq;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodTransRecordTypeItem : BaseBLL<BloodTransRecordTypeItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodTransRecordTypeItem
    {
        public BaseResultDataValue SearchTransfusionAntriesOfBloodTransByHQL(string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where))
            {
                where = "bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=1 and bloodtransrecordtypeitem.IsVisible=1";
            }
            if (string.IsNullOrEmpty(sort))
            {
                sort = "bloodtransrecordtypeitem.BloodTransRecordType.DispOrder ASC,bloodtransrecordtypeitem.DispOrder ASC";
            }
            EntityList<BloodTransRecordTypeItem> entityList = ((IDBloodTransRecordTypeItemDao)base.DBDao).SearchBloodTransRecordTypeItemOfLeftJoinByHQL(where, sort, -1, -1);
            // this.SearchListByHQL(where, sort, -1, -1);
            var groupByList = entityList.list.GroupBy(p => p.BloodTransRecordType);
            JObject jresult = new JObject();
            IList<string> notContains = new List<string>();
            notContains.Add("BloodTransRecordType");
            JArray jitemList = new JArray();
            foreach (var groupBy in groupByList)
            {
                if (groupBy.Key.IsVisible == false) continue;

                JObject jrecordType = JsonHelper.GetPropertyInfo<BloodTransRecordType>(groupBy.Key, true);
                JArray jdtlList = new JArray();
                foreach (var item in groupBy)
                {
                    if (item.IsVisible == false) continue;

                    BloodTransRecordTypeItem recordTypeItem = ClassMapperHelp.GetMapper<BloodTransRecordTypeItem, BloodTransRecordTypeItem>(item);
                    JObject jdtl = JsonHelper.GetPropertyInfo<BloodTransRecordTypeItem>(recordTypeItem, notContains, true);
                    jdtlList.Add(jdtl);
                }
                jrecordType.Add("ItemList", jdtlList);
                jitemList.Add(jrecordType);
            }
            jresult.Add("list", jitemList);
            string dataStr = jresult.ToString().Replace(Environment.NewLine, "").Replace(" ", "");
            //dataStr = "{\"list\":\"" + dataStr.Replace("\"", "\\\"") + "\"}";
            //ZhiFang.Common.Log.Log.Debug("dataStr:" + dataStr);
            tempBaseResultDataValue.ResultDataValue = dataStr;
            return tempBaseResultDataValue;
        }
    }
}