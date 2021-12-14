using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{
    [ServiceContract]
    public interface IPublicService
    {
        //[ServiceContractDescription(Name = "文件上传服务", Desc = "文件上传服务（表单方式）", Url = "PublicService.svc/Public_File_UpLoadByFrom", Get = "", Post = "relativePath: string, isGUIDFileName: string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Public_File_UpLoadByFrom", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue Public_File_UpLoadByFrom();

        //[ServiceContractDescription(Name = "获取枚举列表通过枚举类型名", Desc = "获取枚举列表通过枚举类型名", Url = "PublicService.svc/Public_UDTO_GetEnumNameList?EnumTypeName={EnumTypeName}", Get = "EnumTypeName={EnumTypeName}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Public_UDTO_GetEnumNameList?EnumTypeName={EnumTypeName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue Public_UDTO_GetEnumNameList(string EnumTypeName);

        [OperationContract]
        byte[] DownLoadReportFile(string FileURL);
        [OperationContract]
        byte[] DownLoadFile(string FileDir);
        [OperationContract]
        byte[] DownLoadUpdateFile(string FileDir);
        //[OperationContract]
        //BaseResultDataValue Public_UDTO_CheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion);
        //[OperationContract]
        //BaseResultDataValue Public_UDTO_AddSoftWareUpdateHistory(BSoftWareUpdateHistory entity);
    }
}
