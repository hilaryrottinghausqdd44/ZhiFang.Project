using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public  interface IDItemAllItemDao : IDBaseDao<ZhiFang.Digitlab.Entity.ItemAllItem, long>
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
        /// 根据专业ID和样本类型ID查找项目列表
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <param name="SampleTypeId"></param>
        IList<ItemAllItem> MEPT_UDTO_SearchSpecialtyAndSampleTypeItemList(long SpecialtyId, long SampleTypeId);

        /// <summary>
        /// 根据专业ID和样本类型ID查找项目列表
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <param name="SampleTypeId"></param>
        IList<ItemAllItem> SearchListByGroupId(long GroupId);


    }
}
