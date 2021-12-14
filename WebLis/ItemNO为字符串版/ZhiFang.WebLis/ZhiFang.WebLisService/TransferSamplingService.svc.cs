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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
                return brdv;
            }
            try
            {
                DownloadDictEntity<D_BLabSickType> entityList = new DownloadDictEntity<D_BLabSickType>();
                entityList = ibDictInfo.DownloadBLabSickTypemByLabCode(labcode, maxDataTimeStamp);
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv = VerificationLogin(account, password);

            if (brdv.success == false)
            {
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
            brdv.success = true;//测试
            if (account == null || password.Trim() == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法认证用户信息！";
                ZhiFang.Common.Log.Log.Debug("jsonentity.Account为空！无法认证用户信息！");
                return brdv;
            }
            try
            {
                //先登录
                ZhiFang.WebLisService.clsCommon.RBAC_User user = new ZhiFang.WebLisService.clsCommon.RBAC_User(account);
                user.GetOrganizationsList();
                if (user == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "为获取到用户信息！";
                    ZhiFang.Common.Log.Log.Debug("Account:" + account + ".没有获取到用户信息！");
                }
            }
            catch (Exception ee)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Account:" + account + ".获取用户信息失败！";
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }
    }
}

