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
using System.ServiceModel.Activation;

namespace ZhiFang.WebLisService
{
    [ServiceContract]
    public interface ITransferSamplingService
    {

        #region 移动客户端下载字典

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadDictionaryInfoByLabCode?tableName={tableName}&labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("依字典名及实验室编码下载字典信息(按上次客户端同步字典里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadDictionaryInfoByLabCode(string tableName, string labcode, string maxDataTimeStamp, string account, string password);

        #endregion

        #region 移动客户端单个字典下载
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadItemColorDict?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("字典颜色字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadItemColorDict(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadGroupItem?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("中心字典GroupItem下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadGroupItem(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadItemColorAndSampleTypeDetail?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("项目颜色与样本类型关系字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadItemColorAndSampleTypeDetail(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadBPhysicalExamType?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("体检类型字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadBPhysicalExamType(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadLabSampleTypeByLabCode?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("实验室样本类型字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadLabSampleTypeByLabCode(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadLabTestItemByLabCode?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("实验室检验项目字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadLabTestItemByLabCode(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadBLabGroupItemByLabCode?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("实验室检验项目子项字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadBLabGroupItemByLabCode(string labcode, string maxDataTimeStamp, string account, string password);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownloadBLabSickTypeByLabCode?labcode={labcode}&maxDataTimeStamp={maxDataTimeStamp}&account={account}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("实验室就诊类型字典下载(按上次客户端同步字典数据里最大的时间戳进行过滤查询)")]
        [OperationContract]
        BaseResultDataValue DownloadBLabSickTypeByLabCode(string labcode, string maxDataTimeStamp, string account, string password);
        #endregion

    }
}
