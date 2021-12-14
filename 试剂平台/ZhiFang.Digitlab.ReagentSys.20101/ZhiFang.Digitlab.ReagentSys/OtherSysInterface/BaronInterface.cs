using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;

namespace ZhiFang.Digitlab.ReagentSys.OtherSysInterface
{
    public class BaronInterface
    {

        public static BaseResultDataValue Baron_UDTO_OrderCreatSub(long compID, string userNo, string customerAccount, string goodsJson)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string jsoncode = "";
            string token = "";
            string fileTime = DateTime.Now.ToFileTime().ToString();
            if (!string.IsNullOrEmpty(userNo))
            {
                jsoncode = GetBaronOrderJson(fileTime, userNo, customerAccount, goodsJson);
                token = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(fileTime + "0ecc6701d5f42298");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            ServiceReference_Baron.Order_CreatSubRequest request = new ServiceReference_Baron.Order_CreatSubRequest();
            request.Body = new ServiceReference_Baron.Order_CreatSubRequestBody();
            request.Body.jsoncode = jsoncode;
            request.Body.token = token;
            ServiceReference_Baron.UserApiSoap userApiSoap = new ServiceReference_Baron.UserApiSoapClient();
            ZhiFang.Common.Log.Log.Info("Order_CreatSub");
            ZhiFang.Common.Log.Log.Info(request.Body.jsoncode);
            ZhiFang.Common.Log.Log.Info(request.Body.token);
            ServiceReference_Baron.Order_CreatSubResponse response = userApiSoap.Order_CreatSub(request);
            ZhiFang.Common.Log.Log.Info(response.Body.Order_CreatSubResult);
            string strResult = response.Body.Order_CreatSubResult;
            JObject jsonObject = JObject.Parse(strResult);
            strResult = jsonObject["resultvalue"].ToString();
            if (strResult == "-1")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-1";
                baseResultDataValue.ErrorInfo = "验证或生成订单失败";
            }
            else if (strResult == "-2")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-2";
                baseResultDataValue.ErrorInfo = " 验证异常Errinfo:" + jsonObject["errinfo"].ToString();
            }
            else if (strResult == "1")
            {
                baseResultDataValue.ResultDataValue = "1";
            }
            return baseResultDataValue;
        }

        public static BaseResultDataValue Baron_UDTO_OrderCreatSubAPI(long compID, string userNo, string customerAccount, string goodsJson)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string jsoncode = "";
            string token = "";
            string fileTime = DateTime.Now.ToFileTime().ToString();
            if (!string.IsNullOrEmpty(userNo))
            {
                jsoncode = GetBaronOrderJson(fileTime, userNo, customerAccount, goodsJson);
                token = ZhiFang.Common.Public.SecurityHelp.NetMd5EncryptStr32(fileTime + "0ecc6701d5f42298");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据用户获取到的组织机构代码为空";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            ServiceReference_Baron.Order_CreatSubAPIRequest request = new ServiceReference_Baron.Order_CreatSubAPIRequest();
            request.Body = new ServiceReference_Baron.Order_CreatSubAPIRequestBody();
            request.Body.jsoncode = jsoncode;
            request.Body.token = token;
            ServiceReference_Baron.UserApiSoap userApiSoap = new ServiceReference_Baron.UserApiSoapClient();
            ZhiFang.Common.Log.Log.Info("OrderCreatSubAPI");
            ZhiFang.Common.Log.Log.Info(request.Body.jsoncode);
            ZhiFang.Common.Log.Log.Info(request.Body.token);
            ServiceReference_Baron.Order_CreatSubAPIResponse response = userApiSoap.Order_CreatSubAPI(request);
            ZhiFang.Common.Log.Log.Info(response.Body.Order_CreatSubAPIResult);
            string strResult = response.Body.Order_CreatSubAPIResult;
            JObject jsonObject = JObject.Parse(strResult);
            strResult = jsonObject["resultvalue"].ToString();
            if (strResult == "-1")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-1";
                baseResultDataValue.ErrorInfo = "验证或生成订单失败";
            }
            else if (strResult == "-2")
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = "-2";
                baseResultDataValue.ErrorInfo = " 验证异常Errinfo:" + jsonObject["errinfo"].ToString();
            }
            else if (strResult == "1")
            {
                baseResultDataValue.ResultDataValue = "1";
            }
            return baseResultDataValue;
        }



        public static string GetBaronOrderJson(string fileTime, string userNo, string logName, string goodsJson)
        {
            //return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + userNo + "\",\"OrderType\":\"1\"" +
            //       ",\"LogName\":\"" + logName + "\",\"username\":\"\"" + "," + goodsJson + "}";
            return "{\"filetime\":\"" + fileTime + "\"" + ",\"user\":\"" + logName + "\",\"OrderType\":\"1\"" +
                    ",\"LogName\":\"" + "" + "\",\"username\":\"\"" + "," + goodsJson + "}";
        }

        public static string GetBaronOrderJson(string fileTime, string OrderNo, string goodsJson)
        {
            return "{\"filetime\":\"" + fileTime + "\"" + ",\"OrderNo\":\"" + OrderNo + "\"," + goodsJson + "}";
        }

        public static string GetBaronGoodsJson(IList<BmsCenOrderDtl> listBmsCenOrderDtl, string userNo)
        {
            string strResult = "";
            StringBuilder strBuilder = new StringBuilder();
            foreach (BmsCenOrderDtl bmsCenOrderDtl in listBmsCenOrderDtl)
            {

                strBuilder.Append(",{\"goodsid\":\"" + bmsCenOrderDtl.ProdGoodsNo +
                    "\",\"count\":\"" + bmsCenOrderDtl.GoodsQty.ToString() +
                    "\",\"cCusCode\":\"" + userNo +
                    "\"}");
            }
            if (strBuilder.Length > 0)
            {
                strResult = strBuilder.ToString();
                strResult = "\"goods\":[" + strResult.Remove(0, 1) + "]";
            }
            return strResult;
        }

    }
}