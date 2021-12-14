using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    public interface IBItemAllItem : IBGenericManager<ZhiFang.Digitlab.Entity.ItemAllItem>
    {
        /// <summary>
        /// 根据仪器ID获取检验项目列表
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;ItemAllItem&gt;<ItemAllItem></returns>
        IList<ItemAllItem> SearchItemAllItemByEquipID(long longEquipID);

        /// <summary>
        /// 根据仪器ID和结果类型获取检验项目列表
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <param name="intValueType">结果类型</param>
        /// <returns>IList&lt;ItemAllItem&gt;<ItemAllItem></returns>
        IList<ItemAllItem> SearchItemAllItemByEquipIDAndValueType(long longEquipID, int intValueType);

        /// <summary>
        /// 根据样本类型和专业ID获取项目列表
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <param name="SampleTypeId"></param>
        /// <returns></returns>
        IList<ItemAllItem> MEPT_UDTO_SearchSpecialtyAndSampleTypeItemList(long SpecialtyId, long SampleTypeId);
    }
}
