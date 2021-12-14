using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ZFReaRestfulService : ZhiFang.Digitlab.ReagentSys.ServerContract.IZFReaRestfulService
    {
        /// <summary>
        /// 用户验证，验证用户的合法性
        /// </summary>
        /// <param name="userNo">用户编码</param>
        /// <param name="userPassword">用户密码</param>
        /// <returns></returns>
        private BaseResultDataValue UserAuthentication(string userNo, string userPassword, string timestamp, string sign, string token)
        { 
           BaseResultDataValue brdv = new BaseResultDataValue();
           return brdv;
        }
        /// <summary>
        /// 数据验证，验证业务数据合法性
        /// </summary>
        /// <param name="dataType">数据种类</param>
        /// <param name="dataInfo">数据信息</param>
        /// <returns></returns>
        private BaseResultDataValue DataAuthentication(int dataType, string dataInfo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            return brdv;
        }
        public string WebRequestHttpPostWMS(string saleDocNo)
        {
            return "";
        }

        public string WebRequestHttpGetWMS(string saleDocNo)
        {
            return "Get";
        }

        public void RS_OrderDocCreate()
        { 
        
        }

        /// <summary>
        /// 智方订单同步到第三方系统后
        /// 第三方系统调用此接口确认订单并传递确认相关信息到智方平台。
        /// </summary>
        public void RS_OrderDocConfirm()
        {

        }
        /// <summary>
        /// 智方订单同步到第三方系统后，如果此订单已发货
        /// 第三方系统调用此接口传递订单发货相关信息到智方平台。
        /// </summary>
        public void RS_OrderDocSendOut()
        {

        }

        public void RS_SaleDocCreate()
        {

        }

        /// <summary>
        /// 智方供货单同步到第三方系统后
        /// 第三方系统调用此接口确认供货单并传递确认相关信息到智方平台。
        /// </summary>
        public void RS_SaleDocConfirm()
        {

        }
    }
}
