using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.LabInformationIntegratePlatform.ServerContract.Customization;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization
{
   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DicReceiveService:ServerContract.Customization.IDicReceiveService
    {
        IBLL.LIIP.IBBHospitalArea IBBHospitalArea { get; set; }
        IBLL.LIIP.IBBHospital IBBHospital { get; set; }
        public BaseResultBool ReceiveHospitalAndArea(string areaJosn, string hospitalJson)
        {
            BaseResultBool brb = new BaseResultBool();
            try
            {
                if (string.IsNullOrEmpty(areaJosn))
                {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.接收参数：areaJosn:为空.IP:{BusinessObject.Utils.IPHelper.GetClientIP()}");
                    //brb.ErrorInfo = "接收参数：areaJosn:为空!";
                    //brb.success = false;
                    //return brb;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.接收参数：areaJosn:{areaJosn}.IP:{ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP()}");
                    #region 接收区域列表
                    List<AreaVO> areavolist = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<List<AreaVO>>(areaJosn);

                    if (areavolist != null && areavolist.Count > 0)
                    {
                        brb = IBBHospitalArea.ReceiveAndAdd(areavolist);
                        if (brb.success == false)
                        {
                            ZhiFang.Common.Log.Log.Error($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.ReceiveAndAdd.同步区域失败！brb.ErrorInfo:{brb.ErrorInfo}");
                            brb.ErrorInfo = "同步区域失败！";
                            brb.success = false;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.区域列表为空！");
                    }
                    #endregion
                }
                if (string.IsNullOrEmpty(hospitalJson))
                {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.接收参数：hospitalJson:为空.IP:{BusinessObject.Utils.IPHelper.GetClientIP()}");
                    //brb.ErrorInfo = "接收参数：hospitalJson:为空!";
                    //brb.success = false;
                    //return brb;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.接收参数：hospitalJson:{hospitalJson}.IP:{ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils.IPHelper.GetClientIP()}");
                    #region 接收医院列表
                    List<AreaVO> hospitalsvolist = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<List<AreaVO>>(hospitalJson);

                    if (hospitalsvolist != null && hospitalsvolist.Count > 0)
                    {
                        brb = IBBHospital.ReceiveAndAdd(hospitalsvolist);
                        if (brb.success == false)
                        {
                            ZhiFang.Common.Log.Log.Error($"ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.ReceiveAndAdd.同步医院失败！brb.ErrorInfo:{brb.ErrorInfo}");
                            brb.ErrorInfo = "同步医院失败！";
                            brb.success = false;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.医院列表为空！");
                    }
                    #endregion
                }
                return brb;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization.ReceiveHospitalAndArea.异常：" + e.ToString());
                brb.ErrorInfo = e.ToString();
                brb.success = false;
            }
            return brb;
        }
    }
   
}
