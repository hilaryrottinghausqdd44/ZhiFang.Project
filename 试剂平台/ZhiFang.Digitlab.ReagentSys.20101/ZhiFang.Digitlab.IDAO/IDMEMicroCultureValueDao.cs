using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDMEMicroCultureValueDao : IDBaseDao<MEMicroCultureValue, long>
    {
        /// <summary>
        /// 依培养结果Id更新培养结果的删除标志
        ///并及更新同一培养结果下的鉴定结果的删除标志及同一培养结果下的药敏结果标志
        /// </summary>
        /// <param name="id">培养结果Id</param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        bool UpdateDeleteFlag(long id, bool deleteFlag);
    }
}