using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using ZhiFang.WebLis.ServerContract;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PublisherEvent : IPublisherEvents
    {
        public PublisherEvent()
        {

        }

        public Model.BaseResultDataValue GetPatientInfoByOrgIdAndPatientID(string OrdId, string PatientId)
        {
            Model.BaseResultDataValue brdv = new Model.BaseResultDataValue();
            ZhiFang.Common.Log.Log.Error("服务接收到的时间！"+ DateTime.Now.ToString());
            foreach (var pair in PubliserService.ClientCallbackList)
            {
                brdv = pair.GetPatientInfoByOrgIdAndPatientID(OrdId, PatientId);
                //break;
            }
            return brdv;
        }

        public Model.BaseResultDataValue GetPatientIdByOrgId(string OrdId)
        {
            Model.BaseResultDataValue brdv = new Model.BaseResultDataValue();
            foreach (var pair in PubliserService.ClientCallbackList)
            {
                brdv = pair.GetPatientIdByOrgId(OrdId);
                break;
            }
            return brdv;
        }

        //寿光区中心平台嵌入报告查询界面  ganwh add 2015-10-13
        public Model.BaseResult GetReportQueryUI(string user, string password)
        {
            Model.BaseResult result = new Model.BaseResult();
            result.success = false;
            string descr = "";
            try
            {
                WSRBAC.WSRbac wsrbac = new WSRBAC.WSRbac();
                bool r = wsrbac.Login(user, password, out descr);
                if (r)
                {
                    result.success = true;
                }
            }
            catch (Exception ex) {
                ZhiFang.Common.Log.Log.Info("PublisherEvent.svc.cs:跳转到weblis出错：" +descr+" "+ ex.Message.ToString());
                result.ErrorInfo = ex.Message.ToString();
            }
            return result;
        }

       
    }
}
