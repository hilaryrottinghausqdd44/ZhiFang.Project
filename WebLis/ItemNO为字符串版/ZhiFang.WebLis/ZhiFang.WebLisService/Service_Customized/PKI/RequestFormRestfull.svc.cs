using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.BLL.Common;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Report;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.WebLisService.Service_Customized.PKI
{
    [ServiceContract(Namespace = "ZhiFang.WebLisService")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RequestFormRestfull
    {
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
        IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBOSConsumerUserOrderForm iboscuof = BLLFactory<IBOSConsumerUserOrderForm>.GetBLL("OSConsumerUserOrderForm");

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/NrequestFormAddOrUpdate_PKI", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("申请录入增加和修改服务_PKI定制")]
        [OperationContract]
        public string NrequestFormAddOrUpdate_PKI(NrequestCombiItemBarCodeEntity_Account_PWD requestformentity)
        {
            Log.Info("NrequestFormAddOrUpdate_PKI.调用。");
            BaseResultDataValue brdv = new BaseResultDataValue();
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;
            try
            {
                #region 验证
                if (requestformentity == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数为空！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:参数为空！");
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                if (requestformentity.RequestFormEntity == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数为空！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:RequestFormEntity参数为空！");
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                if (requestformentity.Account == null || requestformentity.Account.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "账户为空！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:账户为空！");
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                if (requestformentity.PWD == null || requestformentity.PWD.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "密码为空！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:密码为空！");
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate_PKI.参数:Account:" + requestformentity.Account + "@PWD:" + requestformentity.PWD);

                ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:签名字段！NRequestFormNo、CName、Age、AgeUnitNo、AgeUnitName、GenderNo、GenderName、ClientNo、ClientName、AreaNo、jztype。");
                //签名字符串格式：NRequestFormNo=\"\",CName=\"\",Age=\"\",AgeUnitNo=\"\",AgeUnitName=\"\",GenderNo=\"\",GenderName=\"\",ClientNo=\"\",ClientName=\"\",AreaNo=\"\",jztype=\"\",PWD"
                string jsonstr = "NRequestFormNo=\"" + requestformentity.RequestFormEntity.NrequestForm.NRequestFormNo + "\",CName=\"" + requestformentity.RequestFormEntity.NrequestForm.CName + "\",Age=\"" + requestformentity.RequestFormEntity.NrequestForm.Age + "\",AgeUnitNo=\"" + requestformentity.RequestFormEntity.NrequestForm.AgeUnitNo + "\",AgeUnitName=\"" + requestformentity.RequestFormEntity.NrequestForm.AgeUnitName + "\",GenderNo=\"" + requestformentity.RequestFormEntity.NrequestForm.GenderNo + "\",GenderName=\"" + requestformentity.RequestFormEntity.NrequestForm.GenderName + "\",ClientNo=\"" + requestformentity.RequestFormEntity.NrequestForm.ClientNo + "\",ClientName=\"" + requestformentity.RequestFormEntity.NrequestForm.ClientName + "\",AreaNo=\"" + requestformentity.RequestFormEntity.NrequestForm.AreaNo + "\",jztype=\"" + requestformentity.RequestFormEntity.NrequestForm.jztype + "\"";

                jsonstr += "," + requestformentity.PWD;
                ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:签名结果！json+pwd:" + jsonstr + ";ServiceSign:" + Tools.MD5Helper.md5sign(jsonstr) + ";ClientSign:" + requestformentity.MD5SignString.ToUpper());

                //签名验证
                if (Tools.MD5Helper.md5sign(jsonstr) != requestformentity.MD5SignString.ToUpper())
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "签名验证未通过！";
                    ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate_PKI:签名验证未通过！1:" + Tools.MD5Helper.md5sign(jsonstr) + ";2:" + requestformentity.MD5SignString.ToUpper());
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }

                #region 初始化权限服务
                try
                {
                    wsrbac = new WSRBAC_Service.WSRbacSoapClient();
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI.未能初始化权限服务:" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "权限异常！";
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                #endregion

                #region 登录验证
                string rbacerror;
                bool loginbool = wsrbac.Login(requestformentity.Account, requestformentity.PWD, out rbacerror);
                if (!loginbool)
                {
                    ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate_PKI.登录验证错误，可能是用户名密码错误！" + rbacerror);
                    brdv.success = false;
                    brdv.ErrorInfo = "登录验证错误，可能是用户名密码错误！";
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User(requestformentity.Account);
                user.GetOrganizationsList();
                #endregion

                bool b = requestformentity.RequestFormEntity.BarCodeList.GroupBy(l => l.BarCode).Where(g => g.Count() > 1).Count() > 0;

                var barcodelist = requestformentity.RequestFormEntity.BarCodeList.GroupBy(l => l.BarCode);
                foreach (var bg in barcodelist)
                {
                    if (bg.GroupBy(barcode => barcode.ColorName).Count() > 1)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "输入的条码号有重复！BarCode:" + bg.Key;
                        ZhiFang.Common.Log.Log.Debug("Account:" + requestformentity.Account + "输入的条码号有重复！BarCode:" + bg.Key);
                        return JsonHelp.JsonDotNetSerializer(brdv);
                    }
                }


                //if (b)
                //{
                //    br.success = false;
                //    br.ErrorInfo = "输入的条码号有重复！";
                //    return br;
                //}
                //数据库中条码为唯一值
                string repeatbarcodestr;
                if (ibbcf.IsExistBarCode(requestformentity.RequestFormEntity.flag, requestformentity.RequestFormEntity.BarCodeList, out repeatbarcodestr))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "条码号:'" + repeatbarcodestr + "'已存在！";
                    return JsonHelp.JsonDotNetSerializer(brdv);
                }
                #endregion

                Model.NRequestForm nrf_m = null;
                Model.NRequestItem nri_m = new Model.NRequestItem();
                Model.BarCodeForm bcf_m = new Model.BarCodeForm();

                #region 定义三个bool类型的变量,判断是否都成功
                bool bNRequestFormRusult = false;
                bool bNRequestItemRusult = false;
                bool bBarCodeFormRusult = false;
                #endregion

                //申请单号
                long nRequestFormNo;

                if ((long)requestformentity.RequestFormEntity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)requestformentity.RequestFormEntity.NrequestForm.NRequestFormNo;


                #region 对象赋值

                #region 组合项目
                IBTestItem ibTest = BLLFactory<IBTestItem>.GetBLL();
                List<NRequestItem> nri_List = new List<NRequestItem>();
                foreach (UiCombiItem uicombiItem in requestformentity.RequestFormEntity.CombiItems)
                {
                    //明细
                    foreach (UiCombiItemDetail uicombiItemDetail in uicombiItem.CombiItemDetailList)
                    {
                        nri_m = new NRequestItem();

                        ////假组套,组套中只包含自己
                        //if (uicombiItem.CombiItemDetailList.Count == 1 && uicombiItem.CombiItemNo == uicombiItemDetail.CombiItemDetailNo)
                        //{

                        //}
                        //else
                        nri_m.CombiItemNo = uicombiItem.CombiItemNo;//uicombiItemDetail.CombiItemDetailNo;
                        nri_m.ParItemNo = uicombiItemDetail.CombiItemDetailNo.ToString();
                        nri_m.NRequestFormNo = nRequestFormNo;

                        nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nri_m.WebLisSourceOrgID = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nri_m.ClientNo = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nri_m.ClientName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nri_List.Add(nri_m);
                    }
                }
                #endregion

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = requestformentity.RequestFormEntity.NrequestForm;
                nrf_m.NRequestFormNo = nRequestFormNo;
                //nrf_m.WebLisSourceOrgID = ClientNo;
                //nrf_m.WebLisSourceOrgName = txtClientNo;
                nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                nrf_m.Collecter = user.NameL + user.NameF;
                #endregion

                #region 条码

                List<Model.BarCodeForm> bcf_List = new List<BarCodeForm>();

                foreach (UiBarCode uibc in requestformentity.RequestFormEntity.BarCodeList)
                {
                    bcf_m = new BarCodeForm();

                    bcf_m.BarCode = uibc.BarCode;
                    int sampleTypeNo;
                    int.TryParse(uibc.SampleType, out sampleTypeNo);
                    bcf_m.SampleTypeNo = sampleTypeNo;

                    bcf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                    bcf_m.WebLisSourceOrgId = nrf_m.ClientNo;
                    bcf_m.WebLisSourceOrgName = nrf_m.ClientName;
                    bcf_m.ClientNo = nrf_m.ClientNo;
                    bcf_m.ClientName = nrf_m.ClientName;
                    bcf_m.CollectDate = nrf_m.CollectDate;
                    bcf_m.CollectTime = nrf_m.CollectTime;
                    bcf_m.Collecter = user.NameL + user.NameF;
                    bcf_m.CollecterID = user.EmplID;
                    bool flag = false;
                    if (requestformentity.RequestFormEntity.flag == "1")
                    {
                        bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();
                        //1条码对应多个子项目                       
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = nilist.ElementAt(0).CombiItemNo.ToString();
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }

                    }
                    else if (requestformentity.RequestFormEntity.flag == "0")
                    {
                        DataSet ds = ibbcf.GetList(new BarCodeForm() { BarCode = uibc.BarCode });
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            long barCodeFormNo;
                            long.TryParse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString(), out barCodeFormNo);
                            bcf_m.BarCodeFormNo = barCodeFormNo;

                        }
                        else
                            bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();

                        //1条码对应多个子项目                     
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = nilist.ElementAt(0).CombiItemNo.ToString();
                                TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                                if (ti != null && (bcf_m.ItemNo == null || !bcf_m.ItemNo.Contains(ItemCenterNo)))
                                {
                                    bcf_m.ItemName += ti.CName + ",";
                                    bcf_m.ItemNo += ItemCenterNo + ",";
                                }
                            }
                            #endregion
                        }
                    }
                    if (bcf_m.ItemName != null && bcf_m.ItemName.Length > 0)
                    {
                        bcf_m.ItemName = bcf_m.ItemName.Remove(bcf_m.ItemName.LastIndexOf(','));
                    }
                    if (bcf_m.ItemNo != null && bcf_m.ItemNo.Length > 0)
                    {
                        bcf_m.ItemNo = bcf_m.ItemNo.Remove(bcf_m.ItemNo.LastIndexOf(','));
                    }
                    bcf_List.Add(bcf_m);
                }

                #endregion

                #endregion

                if (requestformentity.RequestFormEntity.flag.Trim().ToString() == "0")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {
                        //先删除
                        rib.DeleteList_ByNRequestFormNo(nRequestFormNo);

                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = nri.ParItemNo;
                            nri.CombiItemNo = nri.CombiItemNo.ToString();
                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;
                        }

                    }

                    #endregion

                    #region BarCodeForm
                    if (bcf_List != null && bNRequestItemRusult == true)
                    {

                        foreach (BarCodeForm bcf in bcf_List)
                        {

                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = (user.NameL + user.NameF);
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            int i = 0;
                            if (ibbcf.Exists((long)bcf.BarCodeFormNo))
                            {
                                i = ibbcf.Update(bcf);
                            }
                            else
                                i = ibbcf.Add(bcf);

                            if (i > 0)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && nRequestFormNo > 0 && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.ClientNo = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nrf_m.ClientName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Update(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;

                        }
                    }

                    #endregion
                }
                else if (requestformentity.RequestFormEntity.flag.Trim().ToString() == "1")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {


                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = nri.ParItemNo;
                            int result = 0;
                            if (int.TryParse(nri.CombiItemNo.ToString(), out result))
                            {
                                nri.CombiItemNo = result.ToString();
                            }

                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                                bNRequestItemRusult = false;

                        }

                    }

                    #endregion

                    #region BarCodeForm

                    if (bcf_List != null && bNRequestItemRusult == true)
                    {
                        //ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
                        foreach (BarCodeForm bcf in bcf_List)
                        {
                            //ZhiFang.Common.Log.Log.Debug("bcf.BarCode:" + bcf.BarCode);
                            //bcf.ClientNo = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            //bcf.ClientName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.WebLisSourceOrgId = bcf.ClientNo;
                            bcf.WebLisSourceOrgName = bcf.ClientName;
                            bcf.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                            bcf.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                            bcf.Collecter = (user.NameL + user.NameF);
                            bcf.PrintCount = 0;
                            bcf.IsAffirm = 1;
                            int i = ibbcf.Add(bcf);
                            if (i > 0)
                                bBarCodeFormRusult = true;
                            else
                                bBarCodeFormRusult = false;
                        }

                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.SerialNo = nRequestFormNo.ToString();
                        nrf_m.ClientNo = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nrf_m.ClientName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = requestformentity.RequestFormEntity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = requestformentity.RequestFormEntity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Add(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;
                        }
                    }

                    #endregion
                }

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    brdv.success = true;
                else
                    brdv.success = false;

            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                Log.Error("NrequestFormAddOrUpdate_PKI.调用异常：" + e.ToString());
            }
            //Log.Error("NrequestFormAddOrUpdate_PKI.123");
            return JsonHelp.JsonDotNetSerializer(brdv);
            //Log.Error("NrequestFormAddOrUpdate_PKI.321");
        }
    }
    public class NrequestCombiItemBarCodeEntity_Account_PWD 
    {
        /// <summary>
        /// Account
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// PWD
        /// </summary>
        [DataMember]
        public string PWD { get; set; }

        /// <summary>
        /// NrequestCombiItemBarCodeEntity
        /// </summary>
        [DataMember]
        public NrequestCombiItemBarCodeEntity RequestFormEntity { get; set; }

        [DataMember]
        public string MD5SignString { get; set; }
    }
}
