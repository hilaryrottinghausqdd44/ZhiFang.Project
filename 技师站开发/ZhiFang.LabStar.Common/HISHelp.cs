using System.Collections.Generic;
using System.Net;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.Entity.LabStar.ViewObject.Response;

namespace ZhiFang.LabStar.Common
{
    public class HISHelp
    {
        public static BaseResultDataValue GetHisOrderFormAndOrderItem(string inputType, string inputValue, string StartDate, string EndDate, string SickTypeName, string HospNo, string OrgNo, string url)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();            
            if (string.IsNullOrEmpty(url)) {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "URL地址不能为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(inputType) || string.IsNullOrEmpty(inputValue)) {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "核收条件不可为空！";
                return baseResultDataValue;
            }
            HISGetOrderVO hISGetOrderVO = new HISGetOrderVO();
            hISGetOrderVO.InputType = inputType;
            hISGetOrderVO.InputValue = inputValue;
            hISGetOrderVO.StartDate = !string.IsNullOrEmpty(StartDate) ? StartDate : "";
            hISGetOrderVO.EndDate = !string.IsNullOrEmpty(EndDate) ? EndDate : "";
            hISGetOrderVO.SickTypeName = !string.IsNullOrEmpty(SickTypeName) ? SickTypeName : "";
            hISGetOrderVO.HospNo = !string.IsNullOrEmpty(HospNo) ? HospNo : "";
            hISGetOrderVO.OrgNo = !string.IsNullOrEmpty(OrgNo) ? OrgNo : "";

            string StrJson = Newtonsoft.Json.JsonConvert.SerializeObject(hISGetOrderVO);
            var result = ZhiFang.LabStar.Common.HTTPRequest.WebRequestHttpPostNotFormatting(url, StrJson, "application/json","");
            if (!result.success) {
                return result;
            }
            result.ResultDataValue = result.ResultDataValue.Replace("\\\"\\\"", "null");
            return result;
        }
    }
}
