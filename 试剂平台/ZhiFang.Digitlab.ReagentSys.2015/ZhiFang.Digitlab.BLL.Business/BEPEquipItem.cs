using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    public class BEPEquipItem : ZhiFang.Digitlab.BLL.BaseBLL<EPEquipItem>, ZhiFang.Digitlab.IBLL.Business.IBEPEquipItem
    {

        #region IBEPEquipItem 成员

        public IList<EPEquipItem> SearchEPEquipItemByItemValueType(int intValueType)
        {
            return ((IDAO.IDEPEquipItemDao)DBDao).SearchEPEquipItemByItemValueType(intValueType);
        }

        public IList<EPEquipItem> SearchEPEquipItemBySpecialtyIDAndItemValueType(long longSpecialtyID, int intValueType)
        {
            return ((IDAO.IDEPEquipItemDao)DBDao).SearchEPEquipItemBySpecialtyIDAndItemValueType(longSpecialtyID, intValueType);
        }

        #endregion
    }
}
