using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.ServerContract;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“HujieTestService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 HujieTestService.svc 或 HujieTestService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HujieTestService : IHujieTestService
    {
        public BaseResultDataValue GetDoctorList(int Top, string Where, string FieldOrder)
        {
            BaseResultDataValue v = new BaseResultDataValue();

            try
            {
                ZhiFang.ReportFormQueryPrint.BLL.BDoctor doctor = new BLL.BDoctor();

                Top = Top == 0 ? 10 : Top;
                Where = Where == null ? "" : Where;
                FieldOrder = (FieldOrder == null || FieldOrder == "") ? "CName asc" : FieldOrder;

                DataSet list = doctor.GetList(Top, Where, FieldOrder);
                if(list != null && list.Tables != null && list.Tables.Count > 0){
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<Doctor>(list.Tables[0]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    v.ResultDataValue = aa;
                }

                v.success = true;
                //v.ResultDataValue = "欢迎 " + Name + " ,访问服务HujieTestService";
            }catch(Exception ex){
                v.success = false;
                ZhiFang.Common.Log.Log.Debug("GetInfo:" + ex.ToString());
                v.ErrorInfo = "GetInfo服务错误：" + ex.ToString();
            }

            return v;
        }
    }
}
