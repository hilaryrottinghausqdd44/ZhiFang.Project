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
    public class BLBItemCalc : BaseBLL<LBItemCalc>, ZhiFang.IBLL.LabStar.IBLBItemCalc
    {
        public BaseResultDataValue AddDelLBItemCalc(IList<LBItemCalc> addEntityList, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (addEntityList != null && addEntityList.Count > 0)
                {
                    foreach (LBItemCalc endtity in addEntityList)
                    {
                        endtity.DataAddTime = DateTime.Now;
                        endtity.DataUpdateTime = endtity.DataAddTime;
                        this.Entity = endtity;
                        this.Add();
                    }
                }

                if (!string.IsNullOrWhiteSpace(delIDList))
                {
                    IList<string> listID = delIDList.Split(',').ToList();
                    foreach (string id in listID)
                    {
                        this.RemoveByHQL(long.Parse(id));
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddDelLBItemCalc Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public EntityList<LBItemCalc> QueryLBItemCalc(string strHqlWhere, string Order, int start, int count)
        {
            return ((ZhiFang.IDAO.LabStar.IDLBItemCalcDao)this.DBDao).QueryLBItemCalcDao(strHqlWhere, Order, start, count);
        }

    }
}