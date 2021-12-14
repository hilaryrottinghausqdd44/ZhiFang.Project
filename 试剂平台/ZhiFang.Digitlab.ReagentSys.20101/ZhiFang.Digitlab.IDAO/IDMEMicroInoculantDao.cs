using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDMEMicroInoculantDao : IDBaseDao<MEMicroInoculant, long>
    {
        bool UpdateDeleteFlag(long id, bool deleteFlag);
    }
}