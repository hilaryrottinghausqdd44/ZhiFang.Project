using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IReaADOService
    {
        [ServiceContractDescription(Name = "获取数据库连接", Desc = "获取数据库连接", Url = "ReaADOService.svc/RADOS_UDTO_GetDataBaseLink?orgNo={orgNo}&orgName={orgName}", Get = "orgNo={orgNo}&orgName={orgName}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RADOS_UDTO_GetDataBaseLink?orgNo={orgNo}&orgName={orgName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RADOS_UDTO_GetDataBaseLink(string orgNo, string orgName);

        [ServiceContractDescription(Name = "测试数据库ADO连接字符串", Desc = "测试数据库ADO连接字符串", Url = "ReaADOService.svc/RADOS_UDTO_CheckDataBaseLinkByConnectStr?orgNo={orgNo}&orgName={orgName}&dbConnectStr={dbConnectStr}", Get = "orgNo={orgNo}&orgName={orgName}&dbConnectStr={dbConnectStr}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RADOS_UDTO_CheckDataBaseLinkByConnectStr?orgNo={orgNo}&orgName={orgName}&dbConnectStr={dbConnectStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RADOS_UDTO_CheckDataBaseLinkByConnectStr(string orgNo, string orgName, string dbConnectStr);

        [ServiceContractDescription(Name = "测试数据库连接", Desc = "测试数据库连接", Url = "ReaADOService.svc/RADOS_UDTO_CheckDataBaseLink?orgNo={orgNo}&orgName={orgName}", Get = "orgNo={orgNo}&orgName={orgName}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RADOS_UDTO_CheckDataBaseLink?orgNo={orgNo}&orgName={orgName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RADOS_UDTO_CheckDataBaseLink(string orgNo, string orgName);

        [ServiceContractDescription(Name = "获取实验室库存数量", Desc = "获取实验室库存数量", Url = "ReaADOService.svc/RADOS_UDTO_GetLabInStockCount?orgNo={orgNo}&orgName={orgName}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", Get = "orgNo={orgNo}&orgName={orgName}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RADOS_UDTO_GetLabInStockCount?orgNo={orgNo}&orgName={orgName}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RADOS_UDTO_GetLabInStockCount(string orgNo, string orgName, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo);

        [ServiceContractDescription(Name = "实验室库存统计", Desc = "实验室库存统计", Url = "ReaADOService.svc/RADOS_UDTO_GetTestConsumeCountResult?orgNo={orgNo}&orgName={orgName}&beginDate={beginDate}&endDate={endDate}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", Get = "orgNo={orgNo}&orgName={orgName}&beginDate={beginDate}&endDate={endDate}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RADOS_UDTO_GetTestConsumeCountResult?orgNo={orgNo}&orgName={orgName}&beginDate={beginDate}&endDate={endDate}&goodsID={goodsID}&goodsNo={goodsNo}&prodGoodsNo={prodGoodsNo}&goodsLotNo={goodsLotNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RADOS_UDTO_GetTestConsumeCountResult(string orgNo, string orgName, string beginDate, string endDate, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo);

    }
}
