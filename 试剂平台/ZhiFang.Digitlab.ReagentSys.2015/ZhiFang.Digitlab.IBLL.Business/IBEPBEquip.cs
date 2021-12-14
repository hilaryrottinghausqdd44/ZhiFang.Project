using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    public interface IBEPBEquip : IBGenericManager<ZhiFang.Digitlab.Entity.EPBEquip>
    {

        bool ST_UDTO_RestEPBEquipModule();
    }
}
