using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    public class BEPModule : ZhiFang.Digitlab.BLL.BaseBLL<EPModule>, ZhiFang.Digitlab.IBLL.Business.IBEPModule
    {
        /// <summary>
        /// 根据仪器ID获取仪器模块
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;EPModule&gt;</returns>
        public IList<EPModule> SearchEPModuleByEquipID(long longEquipID)
        {
            return ((IDAO.IDEPModuleDao)DBDao).SearchEPModuleByEquipID(longEquipID);
        }
    }
}
