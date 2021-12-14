using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDEPModuleDao : IDBaseDao<EPModule, long>
    {
        /// <summary>
        /// 根据仪器ID获取仪器模块
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;EPModule&gt;</returns>
        IList<EPModule> SearchEPModuleByEquipID(long longEquipID);
    }
}
