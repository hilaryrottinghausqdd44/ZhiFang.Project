using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.ReagentSys.Client.Service_WangHai_ShiJiTan;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceWangHai
    {

        public BaseResultData GetMateType(string mateTypeCode)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //物资分类字典同步
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getMateTypeSync _getMateTypeSync = new getMateTypeSync();
            mateTypeParam _mateTypeParam = new mateTypeParam();
            _getMateTypeSync.arg0 = _mateTypeParam;
            _mateTypeParam.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _mateTypeParam.mateTypeCode = mateTypeCode;
            getMateTypeSyncResponse mateType = apiSoap.getMateTypeSync(_getMateTypeSync);
            if (mateType.@return.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = mateType.@return.head.businessSyncCode;
                brd.message = mateType.@return.head.businessSyncCode;
                return brd;
            }
            return brd;
        }

        public BaseResultData GetReaGoodsInfo(string goodCode, string pageNum, string pageSize, string lastModifyDate, ref IList<ReaCenOrg> listReaCenOrg, ref IList<ReaGoods> listReaGoods)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //物资字典同步 
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getMateInfoSync _getMateInfoSync = new getMateInfoSync();
            materialInfoParam _materialInfo = new materialInfoParam();
            _materialInfo.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _materialInfo.invCode = goodCode;
            _materialInfo.pageNum = pageNum;
            _materialInfo.pageSize = pageSize;
            _materialInfo.lastModifyDate = lastModifyDate;
            _getMateInfoSync.arg0 = _materialInfo;
            getMateInfoSyncResponse result = apiSoap.getMateInfoSync(_getMateInfoSync);
            mateInfoRes _mateInfoRes = result.@return;
            if (_mateInfoRes.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = _mateInfoRes.head.businessSyncCode;
                brd.message = _mateInfoRes.head.businessSyncMsg;
                return brd;
            }
            mateInfo[] listMateInfo = _mateInfoRes.body;
            if (listMateInfo != null && listMateInfo.Length > 0)
            {
                brd = GetGoodsInfo(serverPath, listMateInfo, ref listReaCenOrg, ref listReaGoods);
            }
            return brd;
        }

        public BaseResultData GetReaGoodsInfo(string goodCode, string pageNum, string pageSize, string lastModifyDate)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //物资字典同步 
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getMateInfoSync _getMateInfoSync = new getMateInfoSync();
            materialInfoParam _materialInfo = new materialInfoParam();
            _materialInfo.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _materialInfo.invCode = goodCode;
            _materialInfo.pageNum = pageNum;
            _materialInfo.pageSize = pageSize;
            _materialInfo.lastModifyDate = lastModifyDate;
            _getMateInfoSync.arg0 = _materialInfo;
            getMateInfoSyncResponse result = apiSoap.getMateInfoSync(_getMateInfoSync);
            mateInfoRes _mateInfoRes = result.@return;
            if (_mateInfoRes.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = _mateInfoRes.head.businessSyncCode;
                brd.message = _mateInfoRes.head.businessSyncMsg;
                return brd;
            }
            mateInfo[] listMateInfo = _mateInfoRes.body;
            if (listMateInfo != null && listMateInfo.Length > 0)
            {
                IList<ReaCenOrg> listReaCenOrg = new List<ReaCenOrg>();
                IList<ReaGoods> listReaGoods = new List<ReaGoods>();
                brd = GetGoodsInfo(serverPath, listMateInfo, ref listReaCenOrg, ref listReaGoods);
                try
                {
                    long employeeID = 0;
                    string empID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    if (!string.IsNullOrEmpty(empID))
                        employeeID = long.Parse(empID);
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                    IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                    IBReaGoodsOrgLink IBReaGoodsOrgLink = (IBReaGoodsOrgLink)context.GetObject("BReaGoodsOrgLink");
                    brd = IBReaCenOrg.SaveReaCenOrgByMatchInterface(listReaCenOrg);
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口供应商信息:" + brd.message);
                    }
                    brd = IBReaGoods.SaveReaGoodsByMatchInterface(listReaGoods, employeeID, empName);
                    string goodsId = brd.data;
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口机构货品信息:" + brd.message);
                    }
                    brd = IBReaGoodsOrgLink.SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, employeeID, empName);
                    if (!brd.success)
                    {
                        ZhiFang.Common.Log.Log.Error("保存物资接口供货货品关系信息:" + brd.message);
                    }
                    brd.data = goodsId;//同步成功后，返回最后一个同步货品的ID
                }
                catch (System.Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("保存物资接口错误信息:" + ex.Message);
                }
            }
            return brd;
        }

        public BaseResultData GetGoodsInfo(string serverPath, mateInfo[] listMateInfo, ref IList<ReaCenOrg> listReaCenOrg, ref IList<ReaGoods> listReaGoods)
        {
            BaseResultData brd = new BaseResultData();
            string xmlGoods = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\Goods.xml";
            if (System.IO.File.Exists(xmlGoods))
            {
                Dictionary<string, string> dicGoods = new Dictionary<string, string>();
                Dictionary<string, ReaCenOrg> dicCompCode = new Dictionary<string, ReaCenOrg>();
                InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlGoods, dicGoods);
                foreach (mateInfo _mateInfo in listMateInfo)
                {
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<mateInfo>(_mateInfo));
                    ReaCenOrg reaCenOrg = new ReaCenOrg();
                    ReaGoods reaGoods = new ReaGoods();
                    foreach (KeyValuePair<string, string> kv in dicGoods)
                    {
                        PropertyInfo pi = _mateInfo.GetType().GetProperty(kv.Value);
                        if (pi != null)
                        {
                            PropertyInfo piGoods = reaGoods.GetType().GetProperty(kv.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                            if (piGoods != null)
                            {
                                piGoods.SetValue(reaGoods, InterfaceCommon.DataConvert(piGoods.PropertyType, pi.PropertyType, pi.GetValue(_mateInfo, null)), null);
                            }
                            if (kv.Key.ToLower() == "reacompcode")
                            {
                                reaCenOrg.MatchCode = InterfaceCommon.DataConvert(pi.GetValue(_mateInfo, null));
                                if ((!string.IsNullOrEmpty(reaCenOrg.MatchCode)) && (!dicCompCode.ContainsKey(reaCenOrg.MatchCode)))
                                    dicCompCode.Add(reaCenOrg.MatchCode, reaCenOrg);
                            }
                            else if (kv.Key.ToLower() == "reacompanyname")
                            {
                                reaCenOrg.CName = InterfaceCommon.DataConvert(pi.GetValue(_mateInfo, null));
                            }
                        }
                    }
                    reaGoods.BarCodeMgr = int.Parse(ReaGoodsBarCodeType.批条码.Key);
                    if (reaGoods.Visible == 1)
                        reaGoods.Visible = 0;
                    else
                        reaGoods.Visible = 1;
                    listReaGoods.Add(reaGoods);
                }//foreach
                foreach (KeyValuePair<string, ReaCenOrg> kv in dicCompCode)
                    listReaCenOrg.Add(kv.Value);
            }
            return brd;
        }

        public BaseResultData GetUserInfo(string userCode, HRDept dept, IBHREmployee IBHREmployee)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //人员字典同步 
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getUserInfoSync _getUserInfos = new getUserInfoSync();
            userInfoParam _userInfoParam = new userInfoParam();
            _userInfoParam.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _userInfoParam.userCode = userCode;
            _getUserInfos.arg0 = _userInfoParam;

            getUserInfoSyncResponse result = apiSoap.getUserInfoSync(_getUserInfos);
            userInfoRes _userInfoRes = result.@return;
            if (_userInfoRes.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = _userInfoRes.head.businessSyncCode;
                brd.message = _userInfoRes.head.businessSyncMsg;
                return brd;
            }
            userInfo[] listUser = _userInfoRes.body;
            if (listUser != null && listUser.Length > 0)
            {
                if (IBHREmployee == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
                }
                foreach (userInfo user in listUser)
                {
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<userInfo>(user));
                    IBHREmployee.AddHREmployeeSyncByInterface("MatchCode", user.userCode, dept, new Dictionary<string, object> {
                        { "CName", user.userName },
                        { "MatchCode", user.userCode },
                        { "UseCode", user.userCode },
                        { "MatchDeptCode", user.deptCode }
                    }, true);
                }
            }
            return brd;
        }

        public BaseResultData GetDeptInfo(string deptCode, HRDept dept, IBHRDept IBHRDept)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //科室字典同步 
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getDeptInfoSync _getDeptInfos = new getDeptInfoSync();
            deptInfoParam _deptInfoParam = new deptInfoParam();
            _deptInfoParam.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _deptInfoParam.deptCode = deptCode;
            _getDeptInfos.arg0 = _deptInfoParam;
            getDeptInfoSyncResponse result = apiSoap.getDeptInfoSync(_getDeptInfos);
            deptInfoRes _deptInfoRes = result.@return;
            if (_deptInfoRes.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = _deptInfoRes.head.businessSyncCode;
                brd.message = _deptInfoRes.head.businessSyncMsg;
                return brd;
            }
            deptInfo[] listDept = _deptInfoRes.body;
            if (listDept != null && listDept.Length > 0)
            {
                if (IBHRDept == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                }
                foreach (deptInfo deptInfo in listDept)
                {
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<deptInfo>(deptInfo));
                    IBHRDept.AddHRDeptSyncByInterface("MatchCode", deptInfo.deptCode, new Dictionary<string, object> {
                        { "CName", deptInfo.deptName },
                        { "MatchCode", deptInfo.deptCode },
                        { "StandCode", deptInfo.deptCode },
                        { "DeveCode", deptInfo.deptCode },
                        { "ParentID", dept.Id },
                        { "UseCode", dept.UseCode }
                    });
                }
            }
            return brd;
        }

        public BaseResultData GetStoreInfo(string storeCode, IBReaStorage IBReaStorage)
        {
            BaseResultData brd = new BaseResultData();
            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //库房字典同步  
            SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
            getStoreInfoSync _getStoreInfos = new getStoreInfoSync();
            storeInfoParam _storeInfoParam = new storeInfoParam();
            _storeInfoParam.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
            _storeInfoParam.storeCode = storeCode;
            _getStoreInfos.arg0 = _storeInfoParam;
            getStoreInfoSyncResponse result = apiSoap.getStoreInfoSync(_getStoreInfos);
            storeInfoRes _storeInfoRes = result.@return;
            if (_storeInfoRes.head.businessSyncCode != "0")
            {
                brd.success = false;
                brd.code = _storeInfoRes.head.businessSyncCode;
                brd.message = _storeInfoRes.head.businessSyncMsg;
                return brd;
            }
            storeInfo[] listStorage = _storeInfoRes.body;
            if (listStorage != null && listStorage.Length > 0)
            {
                if (IBReaStorage == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                }
                foreach (storeInfo storage in listStorage)
                {
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<storeInfo>(storage));
                    brd = IBReaStorage.AddReaStorageSyncByInterface("MatchCode", storage.storeCode, new Dictionary<string, object> {
                        { "CName", storage.storeName },
                        { "ShortCode", storage.storeCode },
                        { "MatchCode", storage.storeCode } });
                }
            }
            return brd;
        }

        public BaseResultData CheckInStorageByCode(string barCode, ref ReaBmsCenSaleDoc saleDoc, ref IList<ReaBmsCenSaleDtl> saleDtlList, ref long storageID)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                string serverPath = HttpContext.Current.Server.MapPath("~/");
                //扫码验收入库同步 
                SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
                checkInStorageByCode _checkInStorageByCode = new checkInStorageByCode();
                checkInStoreParam _checkInStoreParam = new checkInStoreParam();
                _checkInStoreParam.compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
                _checkInStoreParam.billBarCode = barCode;
                _checkInStorageByCode.arg0 = _checkInStoreParam;
                ZhiFang.Common.Log.Log.Info("开始调用第三方移出库接口服务,参数：" + _checkInStoreParam.compCode + "---" + barCode);
                checkInStorageByCodeResponse aa = apiSoap.checkInStorageByCode(_checkInStorageByCode);
                ZhiFang.Common.Log.Log.Info("结束调用第三方移出库接口服务,参数：" + _checkInStoreParam.compCode + "---" + barCode);
                if (aa == null || aa.@return == null || aa.@return.body == null || aa.@return.head == null || aa.@return.body.outOrderInfo == null
                    || aa.@return.body.outOrderInfo.outOrderInfoDetail == null || aa.@return.body.outOrderInfo.outOrderInfoDetail.Length == 0)
                {
                    brd.success = false;
                    if (aa.@return.head.businessSyncCode == "0")
                        brd.message = "第三方接口错误：接口无法返回此出库单【" + _checkInStoreParam.billBarCode + "】信息";
                    else
                        brd.message = "第三方接口错误：" + aa.@return.head.businessSyncMsg;
                    brd.code = aa.@return.head.businessSyncCode;
                    return brd;
                }
                outOrderInfo _outOrderInfo = aa.@return.body.outOrderInfo;
                ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<outOrderInfo>(_outOrderInfo));
                brd = OutOrderInfoToSaleDoc(serverPath, _outOrderInfo, ref saleDoc, ref saleDtlList, ref storageID, false);               
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "接口调用错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
                throw new System.Exception(ex.Message);
            }
            return brd;
        }

        public BaseResultData OutOrderInfoToSaleDoc(string serverPath, outOrderInfo _outOrderInfo, ref ReaBmsCenSaleDoc saleDoc, ref IList<ReaBmsCenSaleDtl> saleDtlList, ref long storageID, bool isSave)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                string xmlSaleDoc = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\BmsCenSaleDoc.xml";
                string xmlSaleDtl = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\BmsCenSaleDtl.xml";
                if (System.IO.File.Exists(xmlSaleDoc) && System.IO.File.Exists(xmlSaleDtl))
                {
                    Dictionary<string, string> dicSaleDoc = new Dictionary<string, string>();
                    Dictionary<string, string> dicSaleDtl = new Dictionary<string, string>();
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlSaleDoc, dicSaleDoc);
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlSaleDtl, dicSaleDtl);
                    if (saleDoc == null)
                        saleDoc = new ReaBmsCenSaleDoc();
                    foreach (KeyValuePair<string, string> kv in dicSaleDoc)
                    {
                        ZhiFang.Common.Log.Log.Info("Doc-Key：" + kv.Key + "---" + kv.Value);
                        PropertyInfo pi = _outOrderInfo.GetType().GetProperty(kv.Value);
                        PropertyInfo piSaleDoc = saleDoc.GetType().GetProperty(kv.Key);
                        object piValue = pi.GetValue(_outOrderInfo, null);
                        if (pi != null && piValue != null)
                        {
                            if (kv.Key == "DeptStoreCode")//科室库房
                            {
                                ZhiFang.Common.Log.Log.Info("DeptStoreCode：" + piValue.ToString());
                                IApplicationContext context = ContextRegistry.GetContext();
                                IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                                IList<ReaStorage> list = IBReaStorage.SearchListByHQL(" reastorage.MatchCode=\'" + piValue.ToString() + "\'");
                                if (list != null && list.Count > 0)
                                {
                                    storageID = list[0].Id;
                                }
                                else
                                {
                                    BaseResultData baseresultdata = GetStoreInfo(piValue.ToString(), IBReaStorage);
                                    if (baseresultdata.success)
                                        storageID = long.Parse(baseresultdata.data);
                                }
                                ZhiFang.Common.Log.Log.Info("StorageID：" + storageID.ToString());
                            }
                            else if (piSaleDoc != null)
                                piSaleDoc.SetValue(saleDoc, InterfaceCommon.DataConvert(piSaleDoc.PropertyType, pi.PropertyType, piValue), null);
                        }
                    }
                    saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.暂存.Key);
                    saleDoc.Source = int.Parse(ReaBmsCenSaleDocSource.供应商.Key);
                    saleDoc.IOFlag = int.Parse(ReaBmsCenSaleDocIOFlag.已提取.Key);
                    saleDoc.Memo = "供货单接口导入";
                    string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    if (!string.IsNullOrEmpty(employeeID))
                        saleDoc.UserID = long.Parse(employeeID);
                    saleDoc.UserName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    saleDoc.OperDate = DateTime.Now;
                    saleDoc.DataAddTime = DateTime.Now;
                    saleDoc.DataUpdateTime = DateTime.Now;
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<ReaBmsCenSaleDoc>(saleDoc));
                    if (saleDtlList == null)
                        saleDtlList = new List<ReaBmsCenSaleDtl>();
                    foreach (outOrderInfoDetail detail in _outOrderInfo.outOrderInfoDetail)
                    {
                        ReaBmsCenSaleDtl reaBmsCenSaleDtl = new ReaBmsCenSaleDtl();
                        reaBmsCenSaleDtl.SaleDocID = saleDoc.Id;
                        foreach (KeyValuePair<string, string> kv in dicSaleDtl)
                        {
                            ZhiFang.Common.Log.Log.Info("Dtl-Key：" + kv.Key + "---" + kv.Value);
                            PropertyInfo pi = detail.GetType().GetProperty(kv.Value);
                            PropertyInfo piSaleDtl = reaBmsCenSaleDtl.GetType().GetProperty(kv.Key);
                            object piValue = pi.GetValue(detail, null);
                            if (pi != null && piValue != null)
                            {
                                if (kv.Key.ToLower() == "reagoodsno")//货品ID
                                {
                                    ZhiFang.Common.Log.Log.Info("ReaGoodsNo：" + piValue.ToString());
                                    reaBmsCenSaleDtl.ReaGoodsNo = piValue.ToString();
                                    IApplicationContext context = ContextRegistry.GetContext();
                                    IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                                    IList<ReaGoods> listReaGoods = IBReaGoods.SearchListByHQL(" reagoods.MatchCode=\'" + piValue.ToString() + "\'");
                                    if (listReaGoods != null && listReaGoods.Count > 0)
                                        reaBmsCenSaleDtl.ReaGoodsID = listReaGoods[0].Id;
                                    else
                                    {
                                        BaseResultData baseresultdata = GetReaGoodsInfo(piValue.ToString(), "1", "100", "");
                                        if (!string.IsNullOrEmpty(baseresultdata.data))
                                            reaBmsCenSaleDtl.ReaGoodsID = long.Parse(baseresultdata.data);
                                    }
                                    ZhiFang.Common.Log.Log.Info("ReaGoodsID：" + reaBmsCenSaleDtl.ReaGoodsID.ToString());
                                }
                                else if (kv.Key.ToLower() == "reacompcode")//供应商
                                {
                                    ZhiFang.Common.Log.Log.Info("ReaCompCode：" + piValue.ToString());
                                    IApplicationContext context = ContextRegistry.GetContext();
                                    IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                                    IList<ReaCenOrg> listReaCenOrg = IBReaCenOrg.SearchListByHQL(" reacenorg.MatchCode=\'" + piValue.ToString() + "\'");
                                    if (listReaCenOrg != null && listReaCenOrg.Count > 0)
                                    {
                                        reaBmsCenSaleDtl.ReaCompID = listReaCenOrg[0].Id;
                                        ZhiFang.Common.Log.Log.Info("ReaCompID：" + reaBmsCenSaleDtl.ReaCompID.ToString());
                                    }
                                }
                                else
                                {
                                    if (piSaleDtl != null)
                                    {

                                        piSaleDtl.SetValue(reaBmsCenSaleDtl, InterfaceCommon.DataConvert(piSaleDtl.PropertyType, pi.PropertyType, piValue), null);
                                    }
                                    else
                                        ZhiFang.Common.Log.Log.Info("无法获取供货明细单属性：" + kv.Key);
                                }

                            }
                            else if (pi == null)
                                ZhiFang.Common.Log.Log.Info("无法获取第三方出库单实体属性：" + kv.Value);
                        }
                        reaBmsCenSaleDtl.SaleDocID = saleDoc.Id;
                        reaBmsCenSaleDtl.Status = 0;
                        reaBmsCenSaleDtl.DataAddTime = DateTime.Now;
                        reaBmsCenSaleDtl.DataUpdateTime = DateTime.Now;
                        saleDtlList.Add(reaBmsCenSaleDtl);
                        ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<ReaBmsCenSaleDtl>(reaBmsCenSaleDtl));
                    }
                    if (isSave)
                    {
                        IApplicationContext context = ContextRegistry.GetContext();
                        IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc = (IBReaBmsCenSaleDoc)context.GetObject("BReaBmsCenSaleDoc");
                        IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocAndDtl(saleDoc, saleDtlList, 0, "");
                    }
                }             
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "接口数据处理错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(brd.message);
                throw new System.Exception(ex.Message);
            }
            return brd;
        }

        public BaseResultData ReaGoodsCancellingStocks(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                IList<backOrderDetailInfo> listOrderDetailInfo = new List<backOrderDetailInfo>();
                drawsBackStorage _drawsBackStorage = new drawsBackStorage();
                backOrderInfo orderInfo = new backOrderInfo();
                try
                {
                    InDocToDeptOutOrderDetailInfo(inDoc, inDtlList, ref orderInfo, ref listOrderDetailInfo);
                }
                catch (System.Exception ex)
                {
                    brd.success = false;
                    brd.message = "物资退库同步接口drawsBackStorage参数转换失败：" + ex.Message;
                    ZhiFang.Common.Log.Log.Info(brd.message);
                    return brd;
                }
                SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
                _drawsBackStorage.arg0 = orderInfo;
                _drawsBackStorage.arg1 = listOrderDetailInfo.ToArray();
                ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<drawsBackStorage>(_drawsBackStorage));
                //物资退库同步 
                ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage调用开始！");
                drawsBackStorageResponse result = apiSoap.drawsBackStorage(_drawsBackStorage);
                ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage调用成功！");
                if (result.@return.head.businessSyncCode != "0")
                {
                    brd.success = false;
                    brd.code = result.@return.head.businessSyncCode;
                    brd.message = result.@return.head.businessSyncMsg;
                    ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage返回错误：" + brd.code + "---" + brd.message);
                    return brd;
                }
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "物资退库同步接口drawsBackStorage调用失败：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public BaseResultData ReaGoodsCancellingStocks(Dictionary<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> dic)
        {
            BaseResultData brd = new BaseResultData();
            try
            {
                ZhiFang.Common.Log.Log.Info("开始调用退供应商接口！");
                foreach (KeyValuePair<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> kv in dic)
                {
                    ReaBmsInDoc inDoc = kv.Value.Key;
                    IList<ReaBmsInDtl> inDtlList = kv.Value.Value;
                    if (inDoc == null || inDtlList == null || inDtlList.Count == 0)
                    {
                        brd.success = false;
                        brd.message = "出库确认后调用退供应商接口,转换之后的入库主单与明细信息为空,不调用退库接口!";
                        ZhiFang.Common.Log.Log.Info(brd.message);
                        return brd;
                    }
                    IList<backOrderDetailInfo> listOrderDetailInfo = new List<backOrderDetailInfo>();
                    drawsBackStorage _drawsBackStorage = new drawsBackStorage();
                    backOrderInfo orderInfo = new backOrderInfo();
                    try
                    {
                        InDocToDeptOutOrderDetailInfo(inDoc, inDtlList, ref orderInfo, ref listOrderDetailInfo);
                    }
                    catch (System.Exception ex)
                    {
                        brd.success = false;
                        brd.message = "物资退库同步接口drawsBackStorage参数转换失败：" + ex.Message;
                        ZhiFang.Common.Log.Log.Info(brd.message);
                        return brd;
                    }
                    SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
                    _drawsBackStorage.arg0 = orderInfo;
                    _drawsBackStorage.arg1 = listOrderDetailInfo.ToArray();
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<drawsBackStorage>(_drawsBackStorage));
                    //物资退库同步 
                    ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage调用开始！");
                    drawsBackStorageResponse result = apiSoap.drawsBackStorage(_drawsBackStorage);
                    ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage调用成功！");
                    if (result.@return.head.businessSyncCode != "0")
                    {
                        brd.success = false;
                        brd.code = result.@return.head.businessSyncCode;
                        brd.message = result.@return.head.businessSyncMsg;
                        ZhiFang.Common.Log.Log.Info("物资退库同步接口drawsBackStorage返回错误：" + brd.code + "---" + brd.message);
                        return brd;
                    }
                }
                ZhiFang.Common.Log.Log.Info("结束调用退供应商接口！");
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "物资退库同步接口drawsBackStorage调用失败：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public void InDocToDeptOutOrderDetailInfo(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList, ref backOrderInfo orderInfo, ref IList<backOrderDetailInfo> listOrderDetailInfo)
        {
            if (inDoc != null && inDtlList != null && inDtlList.Count > 0)
            {
                if (orderInfo == null)
                    orderInfo = new backOrderInfo();
                if (listOrderDetailInfo == null)
                    listOrderDetailInfo = new List<backOrderDetailInfo>();
                string serverPath = HttpContext.Current.Server.MapPath("~/");
                string xmlInDoc = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\ReaBmsInDoc.xml";
                string xmlInDtl = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\ReaBmsInDtl.xml";
                string compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
                if (System.IO.File.Exists(xmlInDoc) && System.IO.File.Exists(xmlInDtl))
                {
                    Dictionary<string, string> dicInDoc = new Dictionary<string, string>();
                    Dictionary<string, string> dicInDtl = new Dictionary<string, string>();
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlInDoc, dicInDoc);
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlInDtl, dicInDtl);
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaBmsInDoc IBReaBmsInDoc = (IBReaBmsInDoc)context.GetObject("BReaBmsInDoc");
                    orderInfo.compCode = compCode;
                    foreach (KeyValuePair<string, string> kv in dicInDoc)
                    {
                        PropertyInfo pi = orderInfo.GetType().GetProperty(kv.Key);
                        PropertyInfo piInDoc = inDoc.GetType().GetProperty(kv.Value);
                        if (pi != null && piInDoc != null)
                        {
                            pi.SetValue(orderInfo, InterfaceCommon.DataConvert(pi.PropertyType, piInDoc.PropertyType, piInDoc.GetValue(inDoc, null)), null);
                        }
                    }//foreach
                    string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    if (!string.IsNullOrEmpty(employeeID))
                    {
                        IBHREmployee IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
                        HREmployee emp = IBHREmployee.Get(long.Parse(employeeID));
                        if (emp != null)
                        {
                            orderInfo.checker = emp.MatchCode;
                            orderInfo.checkerName = emp.CName;
                            orderInfo.checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    orderInfo.memo = "物资反向移库（退库）给OES";
                    ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<backOrderInfo>(orderInfo));
                    //Dictionary<string, ReaGoods> dicReaGoods = new Dictionary<string, ReaGoods>();
                    Dictionary<string, ReaStorage> dicReaStorage = new Dictionary<string, ReaStorage>();
                    foreach (ReaBmsInDtl inDtl in inDtlList)
                    {
                        backOrderDetailInfo detail = new backOrderDetailInfo();
                        foreach (KeyValuePair<string, string> kv in dicInDtl)
                        {
                            PropertyInfo pi = detail.GetType().GetProperty(kv.Key);
                            PropertyInfo piInDtl = inDtl.GetType().GetProperty(kv.Value);
                            if (pi != null && piInDtl != null)
                            {
                                pi.SetValue(detail, InterfaceCommon.DataConvert(pi.PropertyType, piInDtl.PropertyType, piInDtl.GetValue(inDtl, null)), null);
                            }
                        }//foreach
                        if (inDtl.ReaGoods != null)
                            detail.invCode = inDtl.ReaGoods.MatchCode;

                        if (inDtl.StorageID != null)
                        {
                            if (dicReaStorage.ContainsKey(inDtl.StorageID.ToString()))
                                orderInfo.storeCode = dicReaStorage[inDtl.StorageID.ToString()].MatchCode;
                            else
                            {
                                IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                                ReaStorage reaStorage = IBReaStorage.Get((long)inDtl.StorageID);
                                if (reaStorage != null)
                                {
                                    orderInfo.storeCode = reaStorage.MatchCode;
                                    dicReaStorage.Add(inDtl.StorageID.ToString(), reaStorage);
                                }
                            }
                        }
                        if ((string.IsNullOrEmpty(orderInfo.billBarCode)) || (string.IsNullOrEmpty(detail.detailCode)))
                        {
                            string otherDocNo = "";
                            string otherBatNo = "";
                            string otherDtlNo = "";
                            IBReaBmsInDoc.GetInterfaceNo(inDtl.GoodsSerial, inDtl.GoodsNo, inDtl.LotNo, ref otherDocNo, ref otherBatNo, ref otherDtlNo);
                            detail.detailCode = otherDtlNo;
                            orderInfo.billBarCode = otherDocNo;
                        }
                        listOrderDetailInfo.Add(detail);
                        ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<backOrderDetailInfo>(detail));
                    }//foreach
                }//if
            }//if
        }

        public BaseResultData ReaGoodsStocksSync(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultData brd = new BaseResultData();
            //LIS物资使用同步 
            try
            {
                IList<deptOutOrderDetailInfo> _deptOutOrderDetailInfo = new List<deptOutOrderDetailInfo>();
                try
                {                 
                    OutDocToDeptOutOrderDetailInfo(outDoc, outDtlList, ref _deptOutOrderDetailInfo);
                }
                catch (System.Exception ex)
                {
                    brd.success = false;
                    brd.message = "物资使用同步接口mateDetailUsed参数转换失败：" + ex.Message;
                    ZhiFang.Common.Log.Log.Info(brd.message);
                    return brd;
                }
                SysPlatformDataSync apiSoap = new Service_WangHai_ShiJiTan.SysPlatformDataSyncClient();
                mateDetailUsed _mateDetailUsed = new mateDetailUsed();
                _mateDetailUsed.arg0 = _deptOutOrderDetailInfo.ToArray();
                ZhiFang.Common.Log.Log.Info("物资使用同步接口mateDetailUsed调用开始！");
                mateDetailUsedResponse result = apiSoap.mateDetailUsed(_mateDetailUsed);
                ZhiFang.Common.Log.Log.Info("物资使用同步接口mateDetailUsed调用成功！");
                if (result.@return.head.businessSyncCode != "0")
                {
                    brd.success = false;
                    brd.code = result.@return.head.businessSyncCode;
                    brd.message = result.@return.head.businessSyncMsg;
                    ZhiFang.Common.Log.Log.Info("物资使用同步接口mateDetailUsed返回错误：" + brd.code + "---" + brd.message);
                    return brd;
                }
            }
            catch (System.Exception ex)
            {
                brd.success = false;
                brd.message = "物资使用同步接口mateDetailUsed调用失败：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public void OutDocToDeptOutOrderDetailInfo(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList, ref IList<deptOutOrderDetailInfo> _deptOutOrderDetailInfo)
        {
            if (outDoc != null && outDtlList != null && outDtlList.Count > 0)
            {
                if (_deptOutOrderDetailInfo == null)
                    _deptOutOrderDetailInfo = new List<deptOutOrderDetailInfo>();
                string serverPath = HttpContext.Current.Server.MapPath("~/");
                string xmlOutDoc = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\ReaBmsOutDoc.xml";
                string xmlOutDtl = serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\ReaBmsOutDtl.xml";
                string compCode = InterfaceCommon.GetLabCodeByXMLFile(serverPath + "\\BaseTableXML\\Interface\\SaleDocConfig\\WaiHai\\LabCode.xml");
                if (System.IO.File.Exists(xmlOutDoc) && System.IO.File.Exists(xmlOutDtl))
                {
                    Dictionary<string, string> dicOutDoc = new Dictionary<string, string>();
                    Dictionary<string, string> dicOutDtl = new Dictionary<string, string>();
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlOutDoc, dicOutDoc);
                    InterfaceCommon.GetColumnNameBySaleDocXMLFile(xmlOutDtl, dicOutDtl);
                    #region
                    Dictionary<string, ReaGoods> dicReaGoods = new Dictionary<string, ReaGoods>();
                    Dictionary<string, ReaStorage> dicReaStorage = new Dictionary<string, ReaStorage>();
                    Dictionary<string, HRDept> dicHRDept = new Dictionary<string, HRDept>();
                    Dictionary<string, ReaCenOrg> dicReaCenOrg = new Dictionary<string, ReaCenOrg>();
                    Dictionary<string, HREmployee> dicHREmployee = new Dictionary<string, HREmployee>();
                    IApplicationContext context = ContextRegistry.GetContext();
                    IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");
                    IBReaStorage IBReaStorage = (IBReaStorage)context.GetObject("BReaStorage");
                    IBHRDept IBHRDept = (IBHRDept)context.GetObject("BHRDept");
                    IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");
                    IBHREmployee IBHREmployee = (IBHREmployee)context.GetObject("BHREmployee");
                    IBReaBmsInDoc IBReaBmsInDoc = (IBReaBmsInDoc)context.GetObject("BReaBmsInDoc");
                    #endregion
                    //ZhiFang.Common.Log.Log.Info(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(outDoc));
                    foreach (ReaBmsOutDtl outDtl in outDtlList)
                    {
                        //ZhiFang.Common.Log.Log.Info(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(outDtl));
                        deptOutOrderDetailInfo detail = new deptOutOrderDetailInfo();
                        detail.compCode = compCode;
                        foreach (KeyValuePair<string, string> kv in dicOutDoc)
                        {
                            PropertyInfo pi = detail.GetType().GetProperty(kv.Key);
                            PropertyInfo piOutDoc = outDoc.GetType().GetProperty(kv.Value);
                            if (pi != null && piOutDoc != null)
                            {
                                pi.SetValue(detail, InterfaceCommon.DataConvert(pi.PropertyType, piOutDoc.PropertyType, piOutDoc.GetValue(outDoc, null)), null);
                            }
                        }//foreach

                        foreach (KeyValuePair<string, string> kv in dicOutDtl)
                        {
                            PropertyInfo pi = detail.GetType().GetProperty(kv.Key);
                            PropertyInfo piOutDtl = outDtl.GetType().GetProperty(kv.Value);
                            if (pi != null && piOutDtl != null)
                            {
                                pi.SetValue(detail, InterfaceCommon.DataConvert(pi.PropertyType, piOutDtl.PropertyType, piOutDtl.GetValue(outDtl, null)), null);
                            }
                        }//foreach

                        #region//货品对照
                        if (outDtl.GoodsID != null)
                        {
                            ReaGoods reaGoods = null;
                            if (dicReaGoods.ContainsKey(outDtl.GoodsID.ToString()))
                                reaGoods = dicReaGoods[outDtl.GoodsID.ToString()];
                            else
                                reaGoods = IBReaGoods.Get((long)outDtl.GoodsID);
                            if (reaGoods != null)
                            {
                                detail.invCode = reaGoods.MatchCode;
                                detail.invName = reaGoods.CName;
                                detail.invSpec = reaGoods.UnitMemo;
                                detail.unitName = reaGoods.UnitName;
                                detail.factoryName = reaGoods.ProdOrgName;
                                if (!dicReaGoods.ContainsKey(outDtl.GoodsID.ToString()))
                                    dicReaGoods.Add(outDtl.GoodsID.ToString(), reaGoods);
                            }
                        }
                        #endregion
                        #region//库房对照
                        if (dicReaStorage.ContainsKey(outDtl.StorageID.ToString()))
                        {
                            detail.storeCode = dicReaStorage[outDtl.StorageID.ToString()].MatchCode;
                            detail.storeName = dicReaStorage[outDtl.StorageID.ToString()].CName;
                        }
                        else
                        {
                            ReaStorage reaStorage = IBReaStorage.Get((long)outDtl.StorageID);
                            if (reaStorage != null)
                            {
                                detail.storeCode = reaStorage.MatchCode;
                                detail.storeName = reaStorage.CName;
                                dicReaStorage.Add(outDtl.StorageID.ToString(), reaStorage);
                            }
                        }
                        #endregion
                        #region//科室对照
                        if (outDoc.DeptID != null)
                        {
                            if (dicHRDept.ContainsKey(outDoc.DeptID.ToString()))
                            {
                                detail.deptCode = dicHRDept[outDoc.DeptID.ToString()].MatchCode;
                                detail.deptName = dicHRDept[outDoc.DeptID.ToString()].CName;
                            }
                            else
                            {
                                HRDept dept = IBHRDept.Get(outDoc.DeptID.Value);
                                if (dept != null)
                                {
                                    detail.deptCode = dept.MatchCode;
                                    detail.deptName = dept.CName;
                                    dicHRDept.Add(outDoc.DeptID.ToString(), dept);
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Info("接口服务-出库单【" + outDoc.OutDocNo + "】没有对应的科室信息，科室ID：" + outDoc.DeptID.Value);
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Info("接口服务-出库单【"+ outDoc.OutDocNo + "】没有对应的科室信息！");
                        }
                        #endregion
                        #region//供应商对照
                        if (outDtl.ReaCompanyID != null)
                        {
                            if (dicReaCenOrg.ContainsKey(outDtl.ReaCompanyID.ToString()))
                            {
                                detail.venCode = dicReaCenOrg[outDtl.ReaCompanyID.ToString()].MatchCode;
                                detail.venName = dicReaCenOrg[outDtl.ReaCompanyID.ToString()].CName;
                            }
                            else
                            {
                                ReaCenOrg reaCenOrg = IBReaCenOrg.Get((long)outDtl.ReaCompanyID);
                                if (reaCenOrg != null)
                                {
                                    detail.venCode = reaCenOrg.MatchCode;
                                    detail.venName = reaCenOrg.CName;
                                    dicReaCenOrg.Add(outDtl.ReaCompanyID.ToString(), reaCenOrg);
                                }
                            }
                        }
                        #endregion
                        #region//人员对照
                        if (outDoc.CreaterID != null)
                        {
                            detail.makeDate = ((DateTime)outDoc.DataAddTime).ToString("yyyy-MM-dd HH:mm:ss");
                            if (dicHREmployee.ContainsKey(outDoc.CreaterID.ToString()))
                            {
                                detail.makerEmp = dicHREmployee[outDoc.CreaterID.ToString()].MatchCode;
                            }
                            else
                            {
                                HREmployee emp = IBHREmployee.Get((long)outDoc.CreaterID);
                                if (emp != null)
                                {
                                    detail.makerEmp = emp.MatchCode;
                                    dicHREmployee.Add(outDoc.CreaterID.ToString(), emp);
                                }
                            }
                        }
                        if (outDoc.CheckID == null)
                            outDoc.CheckID = outDoc.CreaterID;
                        if (outDoc.CheckID != null)
                        {
                            if (dicHREmployee.ContainsKey(outDoc.CheckID.ToString()))
                            {
                                detail.checkerEmp = dicHREmployee[outDoc.CheckID.ToString()].MatchCode;
                            }
                            else
                            {
                                HREmployee emp = IBHREmployee.Get((long)outDoc.CheckID);
                                if (emp != null)
                                {
                                    detail.checkerEmp = emp.MatchCode;
                                    dicHREmployee.Add(outDoc.CheckID.ToString(), emp);
                                }
                            }
                        }
                        if (outDoc.CheckTime == null)
                            outDoc.CheckTime = outDoc.DataAddTime;
                        detail.checkDate = ((DateTime)outDoc.CheckTime).ToString("yyyy-MM-dd HH:mm:ss");
                        #endregion

                        if (outDoc.OutType == 1)
                            detail.inOutFlag = "0";
                        else if (outDoc.OutType == 2)
                            detail.inOutFlag = "1";
                        else
                            detail.inOutFlag = "0";
                        ReaBmsInDoc inDoc = null;
                        ReaBmsInDtl inDtl = null;
                        bool isSucc = IBReaBmsInDoc.GetInDocInfoByOutDtl(outDtl, ref inDoc, ref inDtl);
                        if (isSucc)
                        {
                            detail.billCode = inDoc.OtherDocNo;
                            detail.batchSn = inDtl.ZX3;
                            if ((string.IsNullOrEmpty(detail.billCode)) || (string.IsNullOrEmpty(detail.batchSn)))
                            {
                                string otherDocNo = "";
                                string otherBatNo = "";
                                string otherDtlNo = "";
                                IBReaBmsInDoc.GetInterfaceNo(inDtl.GoodsSerial, inDtl.GoodsNo, inDtl.LotNo, ref otherDocNo, ref otherBatNo, ref otherDtlNo);
                                detail.billCode = otherDocNo;
                                detail.batchSn = otherBatNo;
                            }
                        }
                        _deptOutOrderDetailInfo.Add(detail);
                        ZhiFang.Common.Log.Log.Info(InterfaceCommon.XmlSerialize<deptOutOrderDetailInfo>(detail));
                    }//foreach
                }//if
            }//if
        }
    }
}
