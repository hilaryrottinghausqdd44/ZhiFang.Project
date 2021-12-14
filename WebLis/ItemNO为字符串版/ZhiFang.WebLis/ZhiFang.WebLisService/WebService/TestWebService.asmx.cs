using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;

using ECDS.Common;
using System.Web.UI.MobileControls;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Data;
using System.IO;
using ZhiFang.Common.Dictionary;
using Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// WebLisRepor 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org1/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    //下面一行是为了DELPHI调用时参数传入值不为NULL(但在IE浏览页面出错！！！)
    [SoapRpcService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
    //解决上一行出现的错误
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    public class TestWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string name)
        {
            Log.Info("TestWebService--HelloWorld:" + name);
            return "Hello World " + name;
        }
    }
}
