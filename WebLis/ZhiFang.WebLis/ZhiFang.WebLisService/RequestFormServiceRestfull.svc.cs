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
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Report;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.WebLisService
{
    [ServiceContract(Namespace = "ZhiFang.WebLisService")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RequestFormServiceRestfull
    {
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        IBTestItemControl tic = ZhiFang.BLLFactory.BLLFactory<IBTestItemControl>.GetBLL();
        IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBTestItem CenterTestItem = BLLFactory<IBTestItem>.GetBLL();
        IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();

        /// <summary>
        ///申请录入增加和修改服务
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/NrequestFormAddOrUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("申请录入增加和修改服务")]
        [OperationContract]
        public BaseResult NrequestFormAddOrUpdate(NrequestCombiItemBarCodeEntity_RBAC jsonentity)
        {
            BaseResult br = new BaseResult();
            ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate上传开始！");
            try
            {
                #region 验证
                if (jsonentity.Account == null || jsonentity.Account.Trim() == "")
                {
                    br.success = false;
                    br.ErrorInfo = "无法认证用户信息！";
                    ZhiFang.Common.Log.Log.Debug("jsonentity.Account为空！无法认证用户信息！");
                    return br;
                }
                //先登录
                ZhiFang.WebLisService.clsCommon.RBAC_User user = new ZhiFang.WebLisService.clsCommon.RBAC_User(jsonentity.Account);
                user.GetOrganizationsList();
                if (user == null)
                {
                    br.success = false;
                    br.ErrorInfo = "为获取到用户信息！";
                    ZhiFang.Common.Log.Log.Debug("Account:" + jsonentity.Account + ".为获取到用户信息！");
                    return br;
                }

                bool b = jsonentity.BarCodeList.GroupBy(l => l.BarCode).Where(g => g.Count() > 1).Count() > 0;

                var barcodelist = jsonentity.BarCodeList.GroupBy(l => l.BarCode);
                foreach (var bg in barcodelist)
                {
                    if (bg.GroupBy(barcode => barcode.ColorName).Count() > 1)
                    {
                        br.success = false;
                        br.ErrorInfo = "输入的条码号有重复！BarCode:" + bg.Key;
                        ZhiFang.Common.Log.Log.Debug("Account:" + jsonentity.Account + "输入的条码号有重复！BarCode:" + bg.Key);
                        return br;
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
                if (ibbcf.IsExistBarCode(jsonentity.flag, jsonentity.BarCodeList, out repeatbarcodestr))
                {
                    br.success = false;
                    br.ErrorInfo = "条码号:'" + repeatbarcodestr + "'已存在！";
                    return br;
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

                if ((long)jsonentity.NrequestForm.NRequestFormNo == 0)
                    nRequestFormNo = GUIDHelp.GetGUIDLong();
                else
                    nRequestFormNo = (long)jsonentity.NrequestForm.NRequestFormNo;


                #region 对象赋值

                #region 组合项目
                IBTestItem ibTest = BLLFactory<IBTestItem>.GetBLL();
                List<NRequestItem> nri_List = new List<NRequestItem>();
                foreach (UiCombiItem uicombiItem in jsonentity.CombiItems)
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
                        ZhiFang.Common.Log.Log.Debug("上传项目.CombiItemDetailNo:"+ uicombiItemDetail.CombiItemDetailNo.ToString());
                        nri_m.ParItemNo = uicombiItemDetail.CombiItemDetailNo.ToString();
                        nri_m.NRequestFormNo = nRequestFormNo;

                        nri_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nri_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nri_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nri_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nri_m.ClientName = jsonentity.NrequestForm.ClientName;
                        if (nri_List.Count(a => a.ParItemNo == nri_m.ParItemNo) > 0)
                        {
                            continue;
                        }
                        nri_List.Add(nri_m);
                    }
                }
                #endregion

                #region 表单对象
                nrf_m = new Model.NRequestForm();
                nrf_m = jsonentity.NrequestForm;
                if (jsonentity.NrequestForm.PersonID != null)
                {
                    ZhiFang.Common.Log.Log.Debug("RequestFormServiceRestfull.NrequestFormAddOrUpdate.jsonentity.NrequestForm.PersonID:" + jsonentity.NrequestForm.PersonID + "@nrf_m.PersonID:" + nrf_m.PersonID);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("RequestFormServiceRestfull.NrequestFormAddOrUpdate.jsonentity.NrequestForm.PersonID为空！");
                }

                if (jsonentity.NrequestForm.AreaNo != null)
                {
                    ZhiFang.Common.Log.Log.Debug("RequestFormServiceRestfull.NrequestFormAddOrUpdate.jsonentity.NrequestForm.AreaNo:" + jsonentity.NrequestForm.AreaNo + "@nrf_m.ClientNo:" + nrf_m.ClientNo);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("RequestFormServiceRestfull.NrequestFormAddOrUpdate.jsonentity.NrequestForm.AreaNo为空！");
                }

                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                //ZhiFang.Common.Log.Log.Debug("jsonentity:" + tempParseObjectProperty.GetObjectPropertyNoPlanish(jsonentity));
                nrf_m.NRequestFormNo = nRequestFormNo;
                //nrf_m.WebLisSourceOrgID = ClientNo;
                //nrf_m.WebLisSourceOrgName = txtClientNo;
                nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                nrf_m.Collecter = user.NameL + user.NameF;
                nrf_m.FormMemo = jsonentity.NrequestForm.FormMemo;
                if (jsonentity.NrequestForm.DeptNo.HasValue)
                    nrf_m.DeptNo = jsonentity.NrequestForm.DeptNo;
                nrf_m.DeptName = jsonentity.NrequestForm.DeptName;
                if (jsonentity.NrequestForm.Doctor.HasValue)
                    nrf_m.Doctor = jsonentity.NrequestForm.Doctor;
                nrf_m.DoctorName = jsonentity.NrequestForm.DoctorName;
                #endregion

                #region 条码

                List<Model.BarCodeForm> bcf_List = new List<BarCodeForm>();

                foreach (UiBarCode uibc in jsonentity.BarCodeList)
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
                    if (jsonentity.flag == "1")
                    {
                        bcf_m.BarCodeFormNo = GUIDHelp.GetGUIDLong();
                        //1条码对应多个子项目                       
                        //根据BarCodeFormNo对NRequestItem-BarCodeFormNo赋值
                        foreach (string strItem in uibc.ItemList)
                        {
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
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
                    else if (jsonentity.flag == "0")
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
                            #region 存细项
                            //NRequestItem nrequestItem = nri_List.Find(p => p.ParItemNo == strItem);
                            //if (nrequestItem != null)
                            //{
                            //    nrequestItem.BarCodeFormNo = bcf_m.BarCodeFormNo;
                            //    string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, strItem.ToString());
                            //    TestItem ti = CenterTestItem.GetModel(ItemCenterNo);
                            //    if (ti != null)
                            //    {
                            //        bcf_m.ItemName += ti.CName + ",";
                            //        bcf_m.ItemNo += ItemCenterNo + ",";
                            //    }
                            //}
                            #endregion

                            #region 存组合项
                            var nilist = nri_List.Where(p => p.ParItemNo == strItem);
                            if (nilist != null && nilist.Count() > 0)
                            {
                                nilist.ElementAt(0).BarCodeFormNo = bcf_m.BarCodeFormNo;
                                string ItemCenterNo = tic.GetCenterNo(jsonentity.NrequestForm.AreaNo, nilist.ElementAt(0).CombiItemNo.ToString());
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

                if (jsonentity.flag.Trim().ToString() == "0")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {
                        //先删除
                        rib.DeleteList_ByNRequestFormNo(nRequestFormNo);

                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                            ZhiFang.Common.Log.Log.Debug("上传项目对照后.ParItemNo:" + nri.ParItemNo.ToString());
                            nri.CombiItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString());

                            if (nri.ParItemNo.Trim() == "0")
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.出现ParItemNo为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity));
                                continue;
                            }
                            if (!nri.BarCodeFormNo.HasValue || nri.BarCodeFormNo.Value == 0)
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.出现BarCodeFormNo为空或者为0情况！Account:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity) + ",nri.NRequestFormNo=" + nri.NRequestFormNo + ",nri.ParItemNo=" + nri.ParItemNo + ",nri.CombiItemNo=" + nri.CombiItemNo);
                                continue;
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
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Update(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;

                        }
                    }

                    #endregion
                }
                else if (jsonentity.flag.Trim().ToString() == "1")
                {
                    #region NRequestItem
                    if (nri_List != null)
                    {
                        foreach (NRequestItem nri in nri_List)
                        {
                            nri.NRequestFormNo = nRequestFormNo;
                            nri.ParItemNo = tic.GetCenterNo(nrf_m.AreaNo, nri.ParItemNo);
                            ZhiFang.Common.Log.Log.Debug("上传项目对照后.ParItemNo:" + nri.ParItemNo.ToString());
                            int result = 0;

                            if (int.TryParse(tic.GetCenterNo(nrf_m.AreaNo, nri.CombiItemNo.ToString()), out result))
                            {
                                nri.CombiItemNo = result.ToString();
                            }
                            if (nri.ParItemNo.Trim() == "0")
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.flag=1.出现ParItemNo为0情况！Account:" + jsonentity.NrequestForm.Operator + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity));
                                continue;
                            }
                            if (!nri.BarCodeFormNo.HasValue || nri.BarCodeFormNo.Value == 0)
                            {
                                ZhiFang.Common.Log.Log.Error(this.GetType().ToString() + ".NrequestFormAddOrUpdate.flag=1.出现BarCodeFormNo为空或者为0情况！Account:" + jsonentity.NrequestForm.Operator + "_jsonentity:" + ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(jsonentity) + ",nri.NRequestFormNo=" + nri.NRequestFormNo + ",nri.ParItemNo=" + nri.ParItemNo + ",nri.CombiItemNo=" + nri.CombiItemNo);
                                continue;
                            }
                            int i = rib.Add(nri);
                            if (i > 0)
                            {
                                bNRequestItemRusult = true;
                            }
                            else
                            {
                                bNRequestItemRusult = false;
                                br.ErrorInfo = string.Format("NrequestFormAddOrUpdate.NRequestItem--CombiItemNo为{0},保存失败!", nri.CombiItemNo);
                                ZhiFang.Common.Log.Log.Debug(br.ErrorInfo);
                            }
                        }

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate.NRequestItemList为空!");
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
                            {
                                bBarCodeFormRusult = false;
                                br.ErrorInfo = "NrequestFormAddOrUpdate.BarCodeForm保存失败!";
                                ZhiFang.Common.Log.Log.Debug(br.ErrorInfo);
                            }
                        }

                    }
                    else if (bcf_List == null || bcf_List.Count == 0)
                    {
                        br.ErrorInfo = "NrequestFormAddOrUpdate.BarCodeFormList为空!";
                        ZhiFang.Common.Log.Log.Debug(br.ErrorInfo);
                    }
                    #endregion

                    #region NRequestForm
                    if (nrf_m != null && bBarCodeFormRusult == true)
                    {
                        nrf_m.NRequestFormNo = nRequestFormNo;
                        nrf_m.SerialNo = nRequestFormNo.ToString();
                        nrf_m.ClientNo = jsonentity.NrequestForm.ClientNo;
                        nrf_m.ClientName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisSourceOrgID = jsonentity.NrequestForm.ClientNo;
                        nrf_m.WebLisSourceOrgName = jsonentity.NrequestForm.ClientName;
                        nrf_m.WebLisOrgID = user.OrganizationsList.ElementAt(0).Value.ElementAt(0).Trim();
                        nrf_m.WebLisOrgName = user.OrganizationsList.ElementAt(0).Value.ElementAt(1).Trim();
                        if (rfb.Add(nrf_m) > 0)
                        {
                            bNRequestFormRusult = true;
                        }
                        else
                        {
                            br.ErrorInfo = "NrequestFormAddOrUpdate.NRequestForm保存失败!";
                            ZhiFang.Common.Log.Log.Debug(br.ErrorInfo);
                        }
                    }

                    #endregion
                }

                if (bNRequestFormRusult == true && bNRequestItemRusult == true && bBarCodeFormRusult == true)
                    br.success = true;
                else
                    br.success = false;
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
                ZhiFang.Common.Log.Log.Error("NrequestFormAddOrUpdate错误:" + e.ToString());
            }
            ZhiFang.Common.Log.Log.Debug("NrequestFormAddOrUpdate上传结束！");
            return br;
        }
    }
}
