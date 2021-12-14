using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    public class WeChatConsumptionSamplingInfoVO
    {
        public UserOrderFormVO userOrderFormVO { get; set; }
        public List<BLabTestItem> BLabTestItems { get; set; }
        public List<TestItemDetail> testItemDetails { get; set; }

    }


}
