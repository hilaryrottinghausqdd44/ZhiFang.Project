using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
   public  interface IBEPEquipItem : IBGenericManager<EPEquipItem>
   {
       /// <summary>
       /// 根据项目结果类型获取仪器项目列表
       /// </summary>
       /// <param name="intValueType">结果类型</param>
       /// <returns></returns>
       IList<EPEquipItem> SearchEPEquipItemByItemValueType(int intValueType);

       /// <summary>
       /// 根据专业ID和项目结果类型获取仪器项目列表
       /// </summary>
       /// <param name="longSpecialtyID">专业ID</param>
       /// <param name="intValueType">结果类型</param>
       /// <returns></returns>
       IList<EPEquipItem> SearchEPEquipItemBySpecialtyIDAndItemValueType(long longSpecialtyID, int intValueType);
    }
}
