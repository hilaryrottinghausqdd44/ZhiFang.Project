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
	public  class BBOperateObjectType : BaseBLL<BOperateObjectType>, IBBOperateObjectType
	{
        public BOperateObjectType GetBOperateObjectTypeByCode(string operateObjectTypeCode)
        {
            EntityList<BOperateObjectType> tempEntityList = this.SearchListByHQL("boperateobjecttype.Shortcode=" + "'" + operateObjectTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }

        public BOperateObjectType GetOrAddOperateObjectTypeByCode(string typeCode)
        {
            EntityList<BOperateObjectType> tempEntityList = this.SearchListByHQL("boperateobjecttype.Shortcode=" + "'" + typeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
            {
                BOperateObjectType operateObjectType = new BOperateObjectType();
                operateObjectType.Name = typeCode;
                operateObjectType.SName = typeCode;
                operateObjectType.Shortcode = typeCode;
                operateObjectType.IsUse = true;
                operateObjectType.DataAddTime = DateTime.Now;
                operateObjectType.DataUpdateTime = DateTime.Now;
                this.Entity = operateObjectType;
                if (this.Add())
                    return operateObjectType;
                else
                    return null;

            };
        }
    }
}