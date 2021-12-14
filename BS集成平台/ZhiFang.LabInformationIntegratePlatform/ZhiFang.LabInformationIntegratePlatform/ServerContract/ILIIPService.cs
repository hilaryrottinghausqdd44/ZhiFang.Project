using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract
{
    [ServiceContract]
    public interface ILIIPService
    {
        #region IntergrateSystemSet

        [ServiceContractDescription(Name = "新增分布式系统设置表", Desc = "新增分布式系统设置表", Url = "LIIPServiceService.svc/ST_UDTO_AddIntergrateSystemSet", Get = "", Post = "IntergrateSystemSet", Return = "BaseResultDataValue", ReturnType = "IntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddIntergrateSystemSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddIntergrateSystemSet(IntergrateSystemSet entity);

        //[ServiceContractDescription(Name = "修改分布式系统设置表", Desc = "修改分布式系统设置表", Url = "LIIPServiceService.svc/ST_UDTO_UpdateIntergrateSystemSet", Get = "", Post = "IntergrateSystemSet", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateIntergrateSystemSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateIntergrateSystemSet(IntergrateSystemSet entity);

        [ServiceContractDescription(Name = "修改分布式系统设置表指定的属性", Desc = "修改分布式系统设置表指定的属性", Url = "LIIPServiceService.svc/ST_UDTO_UpdateIntergrateSystemSetByField", Get = "", Post = "IntergrateSystemSet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateIntergrateSystemSetByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateIntergrateSystemSetByField(IntergrateSystemSet entity, string fields);

        [ServiceContractDescription(Name = "删除分布式系统设置表", Desc = "删除分布式系统设置表", Url = "LIIPServiceService.svc/ST_UDTO_DelIntergrateSystemSet?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelIntergrateSystemSet?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelIntergrateSystemSet(long id);

        [ServiceContractDescription(Name = "查询分布式系统设置表", Desc = "查询分布式系统设置表", Url = "LIIPServiceService.svc/ST_UDTO_SearchIntergrateSystemSet", Get = "", Post = "IntergrateSystemSet", Return = "BaseResultList<IntergrateSystemSet>", ReturnType = "ListIntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchIntergrateSystemSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchIntergrateSystemSet(IntergrateSystemSet entity);

        [ServiceContractDescription(Name = "查询分布式系统设置表(HQL)", Desc = "查询分布式系统设置表(HQL)", Url = "LIIPServiceService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<IntergrateSystemSet>", ReturnType = "ListIntergrateSystemSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchIntergrateSystemSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchIntergrateSystemSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询分布式系统设置表", Desc = "通过主键ID查询分布式系统设置表", Url = "LIIPServiceService.svc/ST_UDTO_SearchIntergrateSystemSetById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<IntergrateSystemSet>", ReturnType = "IntergrateSystemSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchIntergrateSystemSetById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchIntergrateSystemSetById(long id, string fields, bool isPlanish);
        #endregion
    }
}
