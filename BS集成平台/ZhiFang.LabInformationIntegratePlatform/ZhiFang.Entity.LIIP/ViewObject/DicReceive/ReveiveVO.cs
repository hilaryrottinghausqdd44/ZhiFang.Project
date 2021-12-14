using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.DicReceive
{
    public class AreaVO : ZhiFang.Entity.LIIP.BHospitalArea
    {
        [DataMember]
        public List<AreaVO> ChildAreaList { get; set; }

        [DataMember]
        public List<HospitalVO> ChildHospitalList { get; set; }
    }

    public class HospitalVO : ZhiFang.Entity.LIIP.BHospital
    {
    }
}
