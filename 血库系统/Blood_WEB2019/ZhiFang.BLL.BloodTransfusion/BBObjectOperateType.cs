using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  class BBObjectOperateType : BaseBLL<BObjectOperateType>, IBBObjectOperateType
    {
        public BObjectOperateType GetObjectOperateTypeByCode(string operateTypeCode)
        {
            EntityList<BObjectOperateType> tempEntityList = this.SearchListByHQL("bObjectoperatetype.Shortcode=" + "'" + operateTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }

        public BObjectOperateType GetOrAddObjectOperateTypeByCode(string operateTypeCode, string operateMemo)
        {
            EntityList<BObjectOperateType> tempEntityList = this.SearchListByHQL("bObjectoperatetype.Shortcode=" + "'" + operateTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
            {
                BObjectOperateType operateType = new BObjectOperateType();
                operateType.Name = operateTypeCode;
                operateType.SName = operateTypeCode;
                operateType.Shortcode = operateTypeCode;
                operateType.Comment = operateMemo;
                operateType.IsUse = true;
                operateType.DataAddTime = DateTime.Now;
                this.Entity = operateType;
                if (this.Add())
                    return operateType;
                else
                    return null;
            }
        }

       
    }
}