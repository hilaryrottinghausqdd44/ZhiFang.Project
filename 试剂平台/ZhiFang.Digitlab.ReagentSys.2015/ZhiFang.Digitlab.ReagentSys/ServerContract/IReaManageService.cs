using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using System.IO;


namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IReaManageService
    {

        [ServiceContractDescription(Name = "从Excel文件中导入货品信息", Desc = "从Excel文件中导入货品信息", Url = "ReaManageService.svc/RM_UDTO_UploadGoodsDataByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RM_UDTO_UploadGoodsDataByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RM_UDTO_UploadGoodsDataByExcel();


        [ServiceContractDescription(Name = "根据HRDeptID查询货品列表", Desc = "根据HRDeptID查询货品列表", Url = "ReaManageService.svc/RM_UDTO_SearchReaGoodsByHRDeptID?deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RM_UDTO_SearchReaGoodsByHRDeptID?deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RM_UDTO_SearchReaGoodsByHRDeptID(long deptID, string where, int page, int limit, string fields, string sort, bool isPlanish);

    }
}
