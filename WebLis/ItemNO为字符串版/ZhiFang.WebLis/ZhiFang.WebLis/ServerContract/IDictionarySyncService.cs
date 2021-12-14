using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Model;
using System.ComponentModel;
using ZhiFang.Model.UiModel;
using System.IO;
using System.Data;

namespace ZhiFang.WebLis.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WebLis", Name = "ZhiFang.WebLis.ServiceWCF.DictionarySyncService")]
    public interface IDictionarySyncService
    {

        #region 实验室字典下载
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "DictionaryService.svc/DownloadDictionaryInfoByLabCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("实验室字典下载")]
        //[OperationContract]
        //BaseResultDataValue DownloadDictionaryInfoByLabCode(string labcode, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadDictionaryInfoByLabCode?labcode={labcode}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("实验室字典下载")]
        [OperationContract]
        BaseResultDataValue DownloadDictionaryInfoByLabCode(string labcode, string account, string password);

        #endregion

    }
}