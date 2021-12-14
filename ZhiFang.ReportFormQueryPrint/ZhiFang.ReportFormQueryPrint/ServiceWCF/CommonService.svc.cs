
using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.ServerContract;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CommonService : ICommonService
    {
        public BaseResultDataValue GetSystemVersion()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string dbversion = "1.0.0.1";
                string sysversion = ((Assembly.GetExecutingAssembly()).GetName()).Version.ToString();
                var tmpp = DBUpdate.DBUpdate.GetDataBaseCurVersion();
                if (tmpp != null)
                {
                    dbversion = tmpp;
                }
                brdv.success = true;
                brdv.ResultDataValue = "DBVersion:'" + dbversion + "',SYSVersion:'" + sysversion + "'";
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("GetSystemVersion.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "获取版本异常！";
                return brdv;
            }
        }

        public BaseResultDataValue GetUpDateVersion()
        {
            ZhiFang.Common.Log.Log.Debug("数据库升级开始.GetUpDateVersion");
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("GetUpDateVersion.操作者IP地址:" + ip);

                bool result = ZhiFang.ReportFormQueryPrint.DBUpdate.DBUpdate.DataBaseUpdate("");

                brdv.success = result;
                //brdv.ResultDataValue = result;

                ZhiFang.Common.Log.Log.Debug("数据库升级结束.GetUpDateVersion");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Debug("数据库升级错误.GetUpDateVersion.error:" + ex.Message);
            }

            return brdv;
        }
    }
}
