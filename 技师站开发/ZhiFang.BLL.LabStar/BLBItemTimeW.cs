using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBItemTimeW : BaseBLL<LBItemTimeW>, ZhiFang.IBLL.LabStar.IBLBItemTimeW
    {
        public BaseResultDataValue QueryLisTestItemOverTime(LisTestForm testForm, IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (testForm == null)
            {
                brdv.success = false;
                return brdv;
            }
            if (listTestItem == null || listTestItem.Count == 0)
            {
                brdv.success = false;
                return brdv;
            }

            string itemIDHQL = "";
            foreach (LisTestItem testItem in listTestItem)
            {
                if (itemIDHQL == "")
                    itemIDHQL = " lbitemtimew.ItemID=" + testItem.LBItem.Id;
                else
                    itemIDHQL += " or lbitemtimew.ItemID=" + testItem.LBItem.Id;
            }

            string whereHQL = " lbitemtimew.SectionID=" + testForm.LBSection.Id;
            if (testForm.GSampleTypeID != null)
                whereHQL += " and lbitemtimew.SampleTypeID=" + testForm.GSampleTypeID;
            if (testForm.SickTypeID != null)
                whereHQL += " and lbitemtimew.SickTypeID=" + testForm.SickTypeID;
            IList<LBItemTimeW> listLBItemTimeW = this.SearchListByHQL(whereHQL + " and (" + itemIDHQL + ")");
            if (listLBItemTimeW != null && listLBItemTimeW.Count > 0)
            {
                foreach (LisTestItem testItem in listTestItem)
                {
                    IList<LBItemTimeW> tempList = listLBItemTimeW.Where(p => p.ItemID == testItem.LBItem.Id).ToList();
                    if (tempList != null && tempList.Count > 0 && tempList[0].TimeWValue > 0)
                    {
                        TimeSpan dateTimeSpan = testItem.TestTime.Value.Subtract(testForm.CollectTime.Value);
                        if (dateTimeSpan.Minutes > tempList[0].TimeWValue)
                        {
                            brdv.success = false;
                            brdv.ErrorInfo += "," + "项目【" + testItem.LBItem.CName + "】检验超时！";
                        }
                    }
                }
            }
            return brdv;
        }
    }
}