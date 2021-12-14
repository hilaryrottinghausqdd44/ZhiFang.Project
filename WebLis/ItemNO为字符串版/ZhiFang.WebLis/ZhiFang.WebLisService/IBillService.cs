using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBillService”。
    [ServiceContract]
    public interface IBillService
    {
        [OperationContract]
        bool UpLoadBll(DataSet ds, List<string> filesname, List<byte[]> filebyte, List<string> fileitemsname, List<byte[]> fileitemsbyte, out string errorinfo, out string strfailidforlist);
    }
}
