using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEMicroInoculantDao : BaseDaoNHB<MEMicroInoculant, long>, IDMEMicroInoculantDao
    {
        public bool UpdateDeleteFlag(long id, bool deleteFlag)
        {
            int result = 0;

            result = this.UpdateByHql("update MEMicroDSTValue memicrodstvalue set memicrodstvalue.DeleteFlag=" + deleteFlag + " where memicrodstvalue.MEMicroCultureValue in (select memicroculturevalue from MEMicroCultureValue memicroculturevalue where memicroculturevalue.MEMicroInoculant.Id =" + id + ")");

            result = this.UpdateByHql("update MEMicroAppraisalValue memicroappraisalvalue set memicroappraisalvalue.DeleteFlag=" + deleteFlag + " where memicroappraisalvalue.MEMicroCultureValue in (select memicroculturevalue from MEMicroCultureValue memicroculturevalue where memicroculturevalue.MEMicroInoculant.Id =" + id + ")");

            result = this.UpdateByHql("update MEMicroCultureValue memicroculturevalue set memicroculturevalue.DeleteFlag=" + deleteFlag + " where memicroculturevalue.MEMicroInoculant.Id = " + id);
            result = this.UpdateByHql("update MEMicroInoculant memicroinoculant set memicroinoculant.DeleteFlag=" + deleteFlag + " where memicroinoculant.Id = " + id);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}