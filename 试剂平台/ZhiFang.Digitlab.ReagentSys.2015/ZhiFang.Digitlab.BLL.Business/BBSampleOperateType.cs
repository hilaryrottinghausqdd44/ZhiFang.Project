
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BBSampleOperateType : BaseBLL<BSampleOperateType>, ZhiFang.Digitlab.IBLL.Business.IBBSampleOperateType
	{
        public BSampleOperateType GetSampleOperateTypeByCode(string operateTypeCode)
        {
            EntityList<BSampleOperateType> tempEntityList = this.SearchListByHQL("bsampleoperatetype.Shortcode=" + "'" + operateTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
                return null;
        }

        public BSampleOperateType GetOrAddSampleOperateTypeByCode(string operateTypeCode, string operateMemo)
        {
            EntityList<BSampleOperateType> tempEntityList = this.SearchListByHQL("bsampleoperatetype.Shortcode=" + "'" + operateTypeCode + "'", 0, 0);
            if (tempEntityList != null && tempEntityList.count > 0)
                return tempEntityList.list[0];
            else
            {
                BSampleOperateType operateType = new BSampleOperateType();
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