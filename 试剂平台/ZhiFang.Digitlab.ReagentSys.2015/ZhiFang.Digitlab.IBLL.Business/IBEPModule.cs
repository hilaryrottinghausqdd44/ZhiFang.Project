using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    public interface IBEPModule : IBGenericManager<EPModule>
    {
        /// <summary>
        /// 根据仪器ID获取仪器模块
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;EPModule&gt;</returns>
        IList<EPModule> SearchEPModuleByEquipID(long longEquipID);
    }
}
