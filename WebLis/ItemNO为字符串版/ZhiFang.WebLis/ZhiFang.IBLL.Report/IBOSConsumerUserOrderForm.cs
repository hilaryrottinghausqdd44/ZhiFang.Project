using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.IBLL.Report
{
    public interface IBOSConsumerUserOrderForm
    {
        BaseResultDataValue SaveOSUserConsumerForm(long NRequestFormNo, NrequestCombiItemBarCodeEntity jsonentity);
        BaseResultDataValue ConsumerUserOrderForm(string PayCode);
    }
}
