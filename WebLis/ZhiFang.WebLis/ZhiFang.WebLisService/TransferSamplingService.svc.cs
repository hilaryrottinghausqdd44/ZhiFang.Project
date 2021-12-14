using ZhiFang.BLLFactory;
using ZhiFang.Model;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Model.UiModel;
using ZhiFang.Common.Dictionary;
using System.Web;
using System.IO;
using ZhiFang.IBLL.Report;
using System.Text;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common;
using System.Web.Script.Serialization;
using ZhiFang.BLL.Common;
using System.Xml;
using System.ServiceModel.Activation;
using ZhiFang.IBLL.Common.BaseDictionary;
using System;
using ZhiFang.WebLis.WSRBAC;
using System.Net;
using System.ServiceModel.Web;
using System.ServiceModel;
using ZhiFang.Model.DownloadDict;
//using System.ServiceModel.Web;

namespace ZhiFang.WebLisService
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TransferSamplingService : ITransferSamplingService
    {
        IBLL.Common.BaseDictionary.IBDownloadDictHelp ibDictInfo = BLLFactory<IBDownloadDictHelp>.GetBLL();
        public void DoWork()
        {
        }

        #region  移动客户端下载字典
        public BaseResultDataValue DownloadDictionaryInfoByLabCode(string tableName, string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string msgInfo = "tableName为:" + tableName + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password;
            ZhiFang.Common.Log.Log.Info(msgInfo + ",实验室字典下载开始:");
            if (string.IsNullOrEmpty(tableName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "tableName不能为空!";
                ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            //实验室字典下载时帐号验证
            ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载时帐号验证开始:");
            //brdv.success = true;
            brdv = VerificationLogin(account, password);
            ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载时帐号验证结束:验证结果为:" + brdv.success);
            if (brdv.success == false)
            {
                return brdv;
            }

            try
            {
                switch (tableName)
                {
                    case "GroupItem":
                        brdv = DownloadGroupItem(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "ItemColorDict":
                        brdv = DownloadItemColorDict(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BPhysicalExamType":
                        brdv = DownloadBPhysicalExamType(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "ItemColorAndSampleTypeDetail":
                        brdv = DownloadItemColorAndSampleTypeDetail(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabSampleType":
                        brdv = DownloadLabSampleTypeByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabTestItem":
                        brdv = DownloadLabTestItemByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabGroupItem":
                        brdv = DownloadBLabGroupItemByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabSickType":
                        brdv = DownloadBLabSickTypeByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabDoctor":
                        brdv = DownloadBLabDoctorByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "BLabFolkType":
                        brdv = DownloadBLabFolkTypeByLabCode(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "Department":
                        brdv = DownloadDepartment(labcode, maxDataTimeStamp, account, password);
                        break;
                    case "GetAreaNo":
                        brdv = GetAreaNo(labcode, account, password);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取" + tableName + "字典数据失败!";
                ZhiFang.Common.Log.Log.Error(msgInfo + "实验室字典下载错误:" + ee.Message);
            }
            return brdv;
        }

        #endregion

        #region 移动客户端单个字典下载调用
        public BaseResultDataValue GetAreaNo(string labcode, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string clientNo = ibDictInfo.GetClientNo(labcode);
            brdv.ResultDataValue = "{AreaNo:" + clientNo + "}";
            return brdv;
        }
        public BaseResultDataValue DownloadDepartment(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("获取科室字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            //if (string.IsNullOrEmpty(labcode))
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = "labcode不能为空!";
            //    ZhiFang.Common.Log.Log.Error("颜色字典下载错误:" + brdv.ErrorInfo);
            //    return brdv;
            //}
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("科室字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            try
            {
                DownloadDictEntity<D_Department> entityList = new DownloadDictEntity<D_Department>();
                entityList = ibDictInfo.DownloadDepartment(maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取科室字典下载数据失败!";
                ZhiFang.Common.Log.Log.Error("科室字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadItemColorDict(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("获取颜色字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            //if (string.IsNullOrEmpty(labcode))
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = "labcode不能为空!";
            //    ZhiFang.Common.Log.Log.Error("颜色字典下载错误:" + brdv.ErrorInfo);
            //    return brdv;
            //}
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("颜色字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_ItemColorDict> entityList = new DownloadDictEntity<D_ItemColorDict>();
                entityList = ibDictInfo.DownloadItemColorDict(maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取颜色字典下载数据失败!";
                ZhiFang.Common.Log.Log.Error("颜色字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadGroupItem(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("获取GroupItem下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            //if (string.IsNullOrEmpty(labcode))
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = "labcode不能为空!";
            //    ZhiFang.Common.Log.Log.Error("ItemColorAndSampleTypeDetail下载错误:" + brdv.ErrorInfo);
            //    return brdv;
            //}
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("GroupItem下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<Model.GroupItem> entityList = new DownloadDictEntity<Model.GroupItem>();
                entityList = ibDictInfo.DownloadGroupItem(maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取GroupItem字典数据失败!";
                ZhiFang.Common.Log.Log.Error("GroupItem下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadItemColorAndSampleTypeDetail(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("获取ItemColorAndSampleTypeDetail下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            //if (string.IsNullOrEmpty(labcode))
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = "labcode不能为空!";
            //    ZhiFang.Common.Log.Log.Error("ItemColorAndSampleTypeDetail下载错误:" + brdv.ErrorInfo);
            //    return brdv;
            //}
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("获取ItemColorAndSampleTypeDetail下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_ItemColorAndSampleTypeDetail> entityList = new DownloadDictEntity<D_ItemColorAndSampleTypeDetail>();
                entityList = ibDictInfo.DownloadItemColorAndSampleTypeDetail(maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取项目颜色与样本类型字典数据失败!";
                ZhiFang.Common.Log.Log.Error("ItemColorAndSampleTypeDetail下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadBPhysicalExamType(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("获取体检类型字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            //if (string.IsNullOrEmpty(labcode))
            //{
            //    brdv.success = false;
            //    brdv.ErrorInfo = "labcode不能为空!";
            //    ZhiFang.Common.Log.Log.Error("体检类型字典下载错误:" + brdv.ErrorInfo);
            //    return brdv;
            //}
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("体检类型字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                //IBLL.Common.BaseDictionary.IBDownloadDictHelp ibDictInfo = BLLFactory<IBDownloadDictHelp>.GetBLL();
                DownloadDictEntity<D_BPhysicalExamType> entityList = new DownloadDictEntity<D_BPhysicalExamType>();
                entityList = ibDictInfo.DownloadBPhysicalExamType(maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取体检类型字典数据失败!";
                ZhiFang.Common.Log.Log.Error("体检类型字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadLabSampleTypeByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室样本类型字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室样本类型字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室样本类型字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                //IBLL.Common.BaseDictionary.IBDownloadDictHelp ibDictInfo = BLLFactory<IBDownloadDictHelp>.GetBLL();
                DownloadDictEntity<D_Lab_SampleType> entityList = new DownloadDictEntity<D_Lab_SampleType>();
                entityList = ibDictInfo.DownloadLabSampleTypeByLabCode(labcode, maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室样本类型字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室样本类型字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadLabTestItemByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室检验项目字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_Lab_TestItem> entityList = new DownloadDictEntity<D_Lab_TestItem>();
                entityList = ibDictInfo.DownloadLabTestItemByLabCode(labcode, maxDataTimeStamp);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室检验项目字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadBLabGroupItemByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室检验项目子项明细字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目子项明细字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目子项明细字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<Model.Lab_GroupItem> entityList = new DownloadDictEntity<Model.Lab_GroupItem>();
                entityList = ibDictInfo.DownloadBLabGroupItemByLabCode(labcode, maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室检验项目子项明细字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室检验项目子项明细字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadBLabSickTypeByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室就诊类型字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室就诊类型字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室就诊类型字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_BLabSickType> entityList = new DownloadDictEntity<D_BLabSickType>();
                entityList = ibDictInfo.DownloadBLabSickTypeByLabCode(labcode, maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室就诊类型字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室就诊类型字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadBLabDoctorByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室医生字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室医生字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室医生字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_BLabDoctor> entityList = new DownloadDictEntity<D_BLabDoctor>();
                entityList = ibDictInfo.DownloadBLabDoctorByLabCode(labcode, maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室医生字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室医生字典下载错误:" + ee.Message);
            }
            return brdv;
        }
        public BaseResultDataValue DownloadBLabFolkTypeByLabCode(string labcode, string maxDataTimeStamp, string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //bool result = false;
            ZhiFang.Common.Log.Log.Info("实验室民族字典下载开始:" + ",labcode为:" + labcode + ",account为:" + account + ",password为:" + password);
            if (string.IsNullOrEmpty(labcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "labcode不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室民族字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                brdv.success = false;
                brdv.ErrorInfo = "帐号(account)或密码(password)不能为空!";
                ZhiFang.Common.Log.Log.Error("实验室民族字典下载错误:" + brdv.ErrorInfo);
                return brdv;
            }

            try
            {
                DownloadDictEntity<D_BLabFolkType> entityList = new DownloadDictEntity<D_BLabFolkType>();
                entityList = ibDictInfo.DownloadBLabFolkTypeByLabCode(labcode, maxDataTimeStamp);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取实验室民族字典数据失败!";
                ZhiFang.Common.Log.Log.Error("实验室民族字典下载错误:" + ee.Message);
            }
            return brdv;
        }

        #endregion

        /// <summary>
        /// 实验室字典下载时帐号验证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private BaseResultDataValue VerificationLogin(string account, string password)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string ClientNo = "";
            brdv.success = true;
            if (account == null || password.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法认证用户信息！";
                ZhiFang.Common.Log.Log.Debug("jsonentity.Account为空！无法认证用户信息！");
                return brdv;
            }
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;
            #region 初始化权限服务
            try
            {
                wsrbac = new WSRBAC_Service.WSRbacSoapClient();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("VerificationLogin.未能初始化权限服务:" + ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "权限异常！";
                return brdv;
            }
            #endregion

            try
            {
                string rbacerror;
                bool loginbool = wsrbac.Login(account, password, out rbacerror);
                if (!loginbool)
                {
                    ZhiFang.Common.Log.Log.Debug("VerificationLogin.登录验证错误，可能是用户名密码错误！" + rbacerror);
                    brdv.success = false;
                    brdv.ErrorInfo = "登录验证错误，可能是用户名密码错误！";
                    return brdv;
                }
                else
                {

                    IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                    EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10, "CLIENTELE.ClIENTNO", "", "");
                    if (cl != null && cl.count > 0)
                    {
                        ClientNo = cl.list[0].ClIENTNO;
                    }
                    else
                    {
                        #region 得到部门信息
                        string deptCode = "";
                        string userinfostr = wsrbac.getUserInfo(account.Trim());
                        DataSet dsuser = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                        string deptxml = wsrbac.getUserOrgInfo(dsuser.Tables[0].Rows[0]["Account"].ToString());
                        DataSet deptds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(deptxml);
                        string strsn = "0";
                        if (deptds != null && deptds.Tables.Count > 0 && deptds.Tables[0].Rows.Count > 0)
                        {  //的到层级关系
                            for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                            {
                                if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && (deptds.Tables[0].Rows[i]["SN"].ToString().Length) >= (Convert.ToInt32(strsn) + 2))
                                {
                                    strsn = deptds.Tables[0].Rows[i]["SN"].ToString();
                                }
                            }
                            for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                            {
                                if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && strsn.Length == deptds.Tables[0].Rows[i]["sn"].ToString().Length)
                                {
                                    //单位
                                    deptCode = deptds.Tables[0].Rows[i]["orgcode"].ToString();
                                    break;
                                }
                            }
                        }
                        #endregion
                        IBLL.Common.BaseDictionary.IBCLIENTELE ibctl = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
                        if (deptCode != "" && deptCode != null)
                        {
                            Model.CLIENTELE model = ibctl.GetModel(long.Parse(deptCode));

                            if (model != null && cl.list != null)
                            {
                                ClientNo = model.ClIENTNO;
                            }
                        }
                    }
                    if (ClientNo == null || ClientNo.Trim() == "")
                    {
                        ZhiFang.Common.Log.Log.Debug("VerificationLogin.根据账户密码未找到相关送检单位，可能是未配置。");
                        brdv.success = false;
                        brdv.ErrorInfo = "根据账户密码未找到相关送检单位，可能是未配置！";
                        return brdv;
                    }
                }
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Account:" + account + ".获取用户信息失败！";
                ZhiFang.Common.Log.Log.Debug("VerificationLogin.异常：" + ee.ToString());
            }
            return brdv;
        }
    }
}

