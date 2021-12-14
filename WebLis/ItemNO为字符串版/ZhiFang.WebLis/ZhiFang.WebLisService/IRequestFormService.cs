using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Xml;
using System.Web.Services;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IRequestFormService”。
    [ServiceContract]
    public interface IRequestFormService
    {
        [OperationContract]
        bool UpLoadRequestFormClient(string dr,string orgID,string jzType, out string strMsg);

         [OperationContract]
        bool UpLoadRequestFormClient_PKI(string drs, string orgID, string jzType, out string strMsg);
        [OperationContract]
        bool AppliyUpLoad(byte[] xmlData, string orgID, string jzType, out string sMsg);
        [OperationContract]
        bool AppliyUpLoad_BoErCheng(byte[] xmlData, string orgID,  out string sMsg);

        [OperationContract]
        bool QueryAppliy(int count, string weblisflag, string ClientNo, string StartDate, string EndDate, out string nodeAppliy, out string Msg);

        [OperationContract]
        bool DownLoadXML(string strSerialNo, out string nodeAppliy, out string Msg);

        [OperationContract]
        bool MarkWeblisFlag(string strSerialNo, bool isMark, out string Msg);
        //[OperationContract(Name="按条码签收申请")]
        //bool DownloadBarCode(
        //    string DestiOrgID,              //外送(至)单位(独立实验室编号)
        //    string BarCodeNo,               //条码码
        //    string WebLiser,               //下载人的其他信息，下载人姓名，地点，时间等等扩展信息(本次先不开发)
        //    out XmlNode nodeBarCode,        //一个条码XML
        //    out XmlNode nodeNRequestItem,   //多少个项目
        //    out XmlNode nodeNRequestForm,   //多少个申请单
        //    out string xmlWebLisOthers,     //返回更多信息
        //    out string ReturnDescription);

        //[OperationContract(Name = "申请单xml")]
        //bool DownloadBarCodeAllx(
        //    string SourceOrgID,   //送检单位
        //    string receive,       //接受单位
        //    string start,         //起止日期
        //    string stop,          //截止日期
        //    string itemNo,        //项目编码
        //    out XmlNode applyXml,  //一个条码XML
        //    out string msg         //描述
        //    );
       

        [OperationContract(Name="样本签收标志")]
        bool DownloadBarCodeFlag(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            string WebLiser,               //操作人的更多信息
            out string ReturnDescription //其他描述
            );
        [WebMethod(Description = "样本退回")]
        bool RefuseDownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            string WebLiser,               //操作人的更多信息
            out string ReturnDescription    //其他描述
            );
        [OperationContract(Name="上传申请单")]
        bool UpgradeRequestForm(
           string SourceOrgID,             //送检(源)单位
           string DestiOrgID,              //外送(至)单位
           string BarCodeNo,               //条码码
           string nodeBarCodeForm,
           string nodeNRequestForm,
           string nodeNRequestItem,
           string nodeOthers,
           out string WebLisFlag,
           out string ReturnDescription);

        //拒收申请服务 ganwh add 2015-10-6
        [OperationContract]
        bool NRequestFormRefuse(string ClientNo, string BarCode, string refuseUser, string refuseTime, string refusereason, out string ErrorInfo);

        //拒收申请单查询 ganwh add 2015-10-7
        [OperationContract]
        bool NRequestFormRefuseQuery(string ClientNo, string LabUploadDateStart, string LabUploadDateEnd, out string NRequestFormXML, out string ErrorInfo);

        //根据申请单号查询条码详细信息 ganwh add 2015-10-7
        [OperationContract]
        bool QueryBarcodeFormByNRequestFormNo(string ClientNo, int NRequestFormNo, out string BarcodeFormXML, out string ErrorInfo);
    }

}
