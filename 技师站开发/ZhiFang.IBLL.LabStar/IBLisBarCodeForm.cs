using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisBarCodeForm : IBGenericManager<LisBarCodeForm>
    {
        SysCookieValue SysCookieValue { get; set; }
        #region 样本条码
        BaseResultDataValue Edit_PreBarCodeAffirm(long nodetype, string barcodes,long userid,string username);
        BaseResultDataValue Edit_PreBarCodeInvalid(long nodetype, string barcodes, bool IsEditOrder,string userid,string username);
        BaseResultDataValue GetBarCodeSamppleGatherVoucherData(long nodetype, string barcode,bool? isloadtable, bool? isupdatebcitems, string modelcode);
        BaseResultDataValue GetPrintBarCodeCount(long nodetype, string barcodes,string userid,string username);
        BaseResultDataValue Edit_BarCodeFromPrintStatus(long nodetype, string barcodes, string IsAffirmBarCode, string userid, string username);
        BaseResultDataValue Add_EqueuingMachineInfo(long nodetype, string barcode, string patientType, string userid, string username);
        BaseResultDataValue Edit_BarCodeFormBarCodeByBarCodeFormID(long nodetype, string barcode, string barcodeformid, string userid, string username);
        BaseResultDataValue GetSampleAffirmDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username);
        BaseResultDataValue EditBarCodeFormRevocationAffirm(long nodetype, string barcodes, string userid, string username);
        #endregion

        #region 样本采集
        /// <summary>
        /// 根据条码号获取样本
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="iscallhis"></param>
        /// <param name="where"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        List<LisBarCodeFormVo> GetSampleGatherWantDataByBarCode(long nodetype,string barcode, string userid, string username,out bool topo_isupdate );
        /// <summary>
        /// 样本获取
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="where"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetSampleGatherWantData(long nodetype, bool? iscallhis, string where,string barcodes,string userid,string username);
        /// <summary>
        /// 样本采集状态更新
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="lisBarCodeFormVos"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> EditBarCodeFormCollect(long nodetype,string barcodes, List<LisBarCodeFormVo> lisBarCodeFormVos, string userid, string username, bool? isConstraintUpdate = false);
        /// <summary>
        /// 根据核收条件从HIS核收数据并存入LIS库返回数据列表
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetBarCodeFromByCheckWhereAndAddBarCodeForm(long nodetype, string receiveType, string value, string userid,string username);
        /// <summary>
        /// 撤销采集
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        BaseResultDataValue EditBarCodeFormRevocationCollect(long nodetype, string barcodes, string userid, string username);

        BaseResultDataValue GetSampleGatherDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username);
        /// <summary>
        /// 根据查询条件获取样本
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="where"></param>
        List<LisBarCodeFormVo> GetSampleGatherWantDataByWhere(long nodetype, string where, string userid, string username);
        BaseResultDataValue Edit_SampleGatherCreateCollectPackNoByBarCode(long nodetype, string barcodes, string userid, string username);
        
        #endregion

        #region 样本送检
        /// <summary>
        /// 根据条码号获取样本
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        List<LisBarCodeFormVo> GetSampleExchangeInspectWantDataByBarCode(long nodetype, string barcode, string userid, string username, out bool topo_isupdate);

        List<LisBarCodeFormVo> EditExchangeInspectBarCodeForm(long nodetype, string barcodes, List<LisBarCodeFormVo> lisBarCodeFormVos, string userid, string username,string suserid,string susername,string labid, bool? isConstraintUpdate = false);
        /// <summary>
        /// 撤销送检
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        BaseResultDataValue EditBarCodeFormRevocationExchangeInspect(long nodetype, string barcodes, string userid, string username);
        /// <summary>
        /// 撤销送检校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        BaseResultDataValue GetSampleExchangeInspectDataAndVerifyByBarCode(long nodetype, string barcodes, string userid, string username);
        /// <summary>
        /// 根据条码号获取样本
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="where"></param>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        List<LisBarCodeFormVo> GetSampleExchangeInspectWantDataByWhere(long nodetype, string where, string userid, string username, string relationForm);
        #endregion

        #region 样本送达
        BaseResultDataValue GetSampledeliveryBarCodeFormListAndEditBarCodeForm(long nodetype, string barcodes, string userid, string username,string loginuserid,string loginusername, string fields, bool isPlanish, bool isUpdate = false);
        BaseResultDataValue EditBarCodeFormArrive(long nodetype, string barcodes, string userid, string username, string loginuserid, string loginusername);
        #endregion

        #region 样本签收
        /// <summary>
        /// 根据查询条件获取样本列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="where"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetSignForBarCodeFormListByWhere(long nodetypeID, string where,string userID,string userName, string sortFields, string relationForm);

        /// <summary>
        /// 通过条码号获取样本列表
        /// </summary>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetSignForBarCodeFormListByBarCode(string barCodes, string sickType);

        /// <summary>
        /// 通过条码号或打包号获取所在打包号内所有样本
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodeOrPackNo"></param>
        /// <param name="sickType"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetSignForBarCodeFormListByBarCodeOrPackNo(long nodetypeID, string barCodeOrPackNo, string sickType, string userID, string userName);

        /// <summary>
        /// 通过条码号签收
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <param name="deliverier"></param>
        /// <param name="deliverierID"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="isAutoSignFor"></param>
        /// <param name="isForceSignFor"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        BaseResultDataValue EditSignForSampleByBarCode(long nodetypeID, string barCodes, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish, bool isAutoSignFor, bool isForceSignFor, string userID, string userName,string Para_MoudleType);
        
        /// <summary>
        /// 通过打包号自动签收
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="packNo"></param>
        /// <param name="sickType"></param>
        /// <param name="deliverier"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="bParas"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        BaseResultDataValue EditSampleSignForByPackNo(long nodetype, string packNo, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish, List<BPara> bParas, string userID, string userName);

        /// <summary>
        /// 取消签收
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        BaseResultDataValue EditCancelSampleSignForOrByBarCode(long nodetypeID, string barCodes, string fields, bool isPlanish,string userId,string userName);

        /// <summary>
        /// 通过条码号获取和配置参数条件获取需要打印取单凭证的样本信息
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="modelcode"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        BaseResultDataValue GetNeedPrintVoucherBarCodeFormListByBarCodeAndPara(long nodetypeID, string barCodes,  string modelcode, string userId, string userName);

        /// <summary>
        /// 通过专业大组ID获取所有小组下的所有项目ID
        /// </summary>
        /// <param name="superSectionID"></param>
        /// <returns></returns>
        BaseResultDataValue GetAllItemIdBySuperSectionID(string superSectionID);

        /// <summary>
        /// 通过小组ID获取所有小组下的所有项目ID
        /// </summary>
        /// <param name="sectionID"></param>
        /// <returns></returns>
        BaseResultDataValue GetAllItemIdBySectionID(string sectionID);
        #endregion

        #region 样本拒收
        //根据查询条件获取列表
        BaseResultDataValue GetSampleListByWhereRefuseAccept( string where, string fields, bool isPlanish, string sickTypeId, string sickTypeName);
        //拒收样本
        BaseResultDataValue EditRefuseAcceptSample(long nodetypeID, string barcodes,string refuseReason,string handleAdvice, string answerPeople,string phoneNum,string refuseRemark,  string fields, bool isPlanish, bool isForceReject, string userID,string userName, long refuseID);

        #endregion

        #region 样本分发
        /// <summary>
        /// 根据查询条件获取样本列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="where"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetDispenseBarCodeFormListByWhere(long nodetypeID, string where, string userID, string userName, string sortFields, string relationForm);

        /// <summary>
        /// 通过条码号获取样本列表
        /// </summary>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <returns></returns>
        List<LisBarCodeFormVo> GetDispenseBarCodeFormListByBarCode(string barCodes, string sickType);
        /// <summary>
        /// 根据样本单id获取所包含项目的分发信息列表
        /// </summary>
        /// <param name="barcodeFormId"></param>
        /// <returns></returns>
        List<LisBarCodeItemVoResp> GetBarCodeItemDispenseInfoListByFormId(long barcodeFormId);

        /// <summary>
        /// 通过条码号分发
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="sickType"></param>
        /// <param name="isForceDispense"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="isPlanish"></param>
        /// <param name="fields"></param>
        /// <param name="TestDate"></param>
        /// <param name="ruleType"></param>
        /// <returns></returns>
        BaseResultDataValue EditDispenseSampleByBarCode(long nodetypeID, string barCodes, string sickType, bool isForceDispense, string userID, string userName, bool isPlanish, string fields, string TestDate, string ruleType);

        BaseResultDataValue EditSampleDispenseCancelByBarCodeFormId(long nodetypeID, string barCodeFormIds, string fields, bool isPlanish);

        BaseResultDataValue PrintDispenseTagByBarCode(long nodetypeID, string barCodes);
        #endregion
    }
}