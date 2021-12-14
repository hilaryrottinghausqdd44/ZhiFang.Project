using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.LIIP.ViewObject.Request
{
    [DataContract]
    public class BHospitalAreaVO:BHospitalArea
    {
        [DataMember]
        /// <summary>
        /// 参数数据
        /// </summary>
        public bool IsChild { get; set; }

        [DataMember]
        /// <summary>
        /// 参数数据
        /// </summary>
        public List<BHospitalAreaVO> ChildHosps { get; set; }

       
    }
}
