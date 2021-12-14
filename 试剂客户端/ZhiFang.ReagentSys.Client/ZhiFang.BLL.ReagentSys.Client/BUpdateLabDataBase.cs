using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client
{
    public class BUpdateLabDataBase : IBUpdateLabDataBase
    {
        IBCenOrg IBCenOrg { get; set; }
        IBBParameter IBBParameter { get; set; }

        #region 帐号登录成功后,依帐号所属的LabID初始化或更新实验室在系统运行所需信息
        public bool EditDataBaseByLabId(long labId)
        {
            bool result = false;
            try
            {
                result = EditDataBaseUpdate(labId);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("实验室升级系统运行所需数据信息失败:" + ex.StackTrace);
                return false;
            }
            return result;
        }
        /// <summary>
        /// 获取数据库当前版本
        /// </summary>
        /// <returns></returns>
        private BParameter GetDataBaseCurVersion(long labId)
        {
            BParameter curBParameter = null;
            string paraNo = SYSParaNo.GetStatusDic()[SYSParaNo.实验室数据升级版本.Key].Id;
            string hql = "bparameter.IsUse=1 and bparameter.LabID=" + labId + " and ParaType='CONFIG' and bparameter.ParaNo='" + paraNo + "'";
            IList<BParameter> tempList = IBBParameter.SearchListByHQL(hql);
            if (tempList != null && tempList.Count > 0)
            {
                curBParameter = tempList[0];
            }
            return curBParameter;
        }
        private bool IsUpdateDataBase(string oldVersion, string newVersion)
        {
            bool result = false;
            //比较数据库版本号，判断是否可升级
            if (CompareDBVersion(oldVersion, newVersion))
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 比较数据库版本
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private bool CompareDBVersion(string oldVersion, string newVersion)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(oldVersion.Trim())) && (!string.IsNullOrEmpty(newVersion.Trim())))
            {
                try
                {
                    string[] oldVersionList = oldVersion.Split('.');
                    string[] newVersionList = newVersion.Split('.');
                    if (oldVersionList.Length == newVersionList.Length)
                    {
                        for (int i = 0; i < newVersionList.Length; i++)
                        {
                            if (int.Parse(oldVersionList[i]) < int.Parse(newVersionList[i]))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    else if (oldVersionList.Length < newVersionList.Length)
                        result = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error2：" + ex.Message);
                }
            }
            else if (string.IsNullOrEmpty(oldVersion.Trim()) && (!string.IsNullOrEmpty(newVersion.Trim())))
                result = true;
            return result;
        }
        private bool EditCompareVersionInfo(BParameter curBParameter, long labId, string newVersion)
        {
            bool result = false;
            if (curBParameter == null && newVersion == "1.0.0.1")
            {
                curBParameter = new BParameter();
                curBParameter.LabID = labId;
                curBParameter.ParaType = "CONFIG";
                curBParameter.Name = SYSParaNo.GetStatusDic()[SYSParaNo.实验室数据升级版本.Key].Name;
                curBParameter.ParaNo = SYSParaNo.GetStatusDic()[SYSParaNo.实验室数据升级版本.Key].Id;
                curBParameter.ParaValue = newVersion;
                curBParameter.IsUse = true;
                curBParameter.ParaDesc = "实验室数据升级版本";
                IBBParameter.Entity = curBParameter;
                result = IBBParameter.Add();
            }
            else if (curBParameter != null)
            {
                curBParameter.ParaValue = newVersion;
                IBBParameter.Entity = curBParameter;
                result = IBBParameter.Edit();
            }
            if (result)
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(" + newVersion + ") Update Success!");
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(" + newVersion + ") Update Error, Please Check The Log!");
            return result;
        }
        private Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            dicVersion.Add("1.0.0.1", "1.0.0.1");
            //dicVersion.Add("1.0.0.2", "1.0.0.2");
            //dicVersion.Add("1.0.0.3", "1.0.0.3");
            //dicVersion.Add("1.0.0.4", "1.0.0.4");
            //dicVersion.Add("1.0.0.5", "1.0.0.5");
            //dicVersion.Add("1.0.0.6", "1.0.0.6");
            //dicVersion.Add("1.0.0.7", "1.0.0.7");
            //dicVersion.Add("1.0.0.8", "1.0.0.8");
            //dicVersion.Add("1.0.0.9", "1.0.0.9");//效期预警默认已过期天数,注册证预警默认已过期天数
            //dicVersion.Add("1.0.0.10", "1.0.0.10");//启用用户UI配置
            //dicVersion.Add("1.0.0.11", "1.0.0.11");//是否需要支持直接出库
            //dicVersion.Add("1.0.0.12", "1.0.0.12");//接口数据是否需要重新生成条码
            //dicVersion.Add("1.0.0.13", "1.0.0.13");//移库或出库扫码是否允许从所有库房获取库存货品
            dicVersion.Add("1.0.0.14", "1.0.0.14");//盘库时实盘数是否取库存数,是否开启近效期,是否强制近效期出库
            return dicVersion;
        }
        /// <summary>
        /// 实验室数据升级
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <returns></returns>
        public bool EditDataBaseUpdate(long labId)
        {
            bool result = false;
            string oldVersion = "";
            BParameter curBParameter = GetDataBaseCurVersion(labId);
            if (curBParameter == null)
            {
                oldVersion = "1.0.0.0";
            }
            else
            {
                oldVersion = curBParameter.ParaValue;
            }

            #region 1.0.0.1
            if (IsUpdateDataBase(oldVersion, "1.0.0.1"))
            {
                result = EditDataBaseOfVersion1(curBParameter, labId);
            }
            #endregion
            #region 1.0.0.2
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            //{
            //    //result = EditDataBaseOfVersion2(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.3
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            //{
            //    result = EditDataBaseOfVersion3(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.4
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            //{
            //    result = EditDataBaseOfVersion4(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.5
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            //{
            //    result = EditDataBaseOfVersion5(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.6
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            //{
            //    result = EditDataBaseOfVersion6(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.7
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            //{
            //    result = EditDataBaseOfVersion7(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.8
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            //{
            //    result = EditDataBaseOfVersion8(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.9
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.9"))
            //{
            //    result = EditDataBaseOfVersion9(curBParameter, labId);
            //}
            //#endregion
            //#region 1.0.0.10
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.10"))
            //{
            //    result = EditDataBaseOfVersion10(curBParameter, labId);
            //}
            #endregion
            #region 1.0.0.11
            //else if (IsUpdateDataBase(oldVersion, "1.0.0.11"))
            //{
            //    result = EditDataBaseOfVersion11(curBParameter, labId);
            //}
            #endregion
            #region 1.0.0.13
            else if (IsUpdateDataBase(oldVersion, "1.0.0.13"))
            {
                result = EditDataBaseOfVersion13(curBParameter, labId);
            }
            #endregion
            #region 1.0.0.14
            else if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                result = EditDataBaseOfVersion14(curBParameter, labId);
            }
            #endregion
            #region 1.0.0.15
            else if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                result = EditDataBaseOfVersion15(curBParameter, labId);
            }
            #endregion
            #region 1.0.0.16
            else if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                result = EditDataBaseOfVersion16(curBParameter, labId);
            }
            #endregion
            #region 1.0.0.17
            else if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                result = EditDataBaseOfVersion17(curBParameter, labId);
            }
            #endregion
            return result;
        }
        public bool EditDataBaseOfVersion17(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //增加出库的参数：出库确认后是否上传试剂平台
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.17");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.17) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion16(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //增加采购申请的参数：平均使用量计算月数、采购系数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.16");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.16) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion15(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //增加参数：供应批次合并
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.15");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.15) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion14(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //盘库时实盘数是否取库存数,是否开启近效期,是否强制近效期出库
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.14");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.14) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion13(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //移库或出库扫码是否允许从所有库房获取库存货品
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.13");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.13) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion12(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //接口数据是否需要重新生成条码
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.12");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.12) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion11(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //是否需要支持直接出库
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.11");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.11) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion10(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //启用用户UI配置
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.10");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.10) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion9(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //效期预警默认已过期天数,注册证预警默认已过期天数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.9");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.9) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion8(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.8");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.8) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion7(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.7");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.7) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion6(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.6");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.6) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion5(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.5");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.5) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion4(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.4");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.4) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion3(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
            {
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.3");
            }
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.3) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion2(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            if (result)
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.2");
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.2) Update Error, Please Check The Log!");
            return result;
        }
        public bool EditDataBaseOfVersion1(BParameter curBParameter, long labId)
        {
            bool result = false;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //系统运行参数
            tempBaseResultDataValue = AddBParameterOfOfLabID(labId);
            result = tempBaseResultDataValue.success;

            //添加机构的一维条码序号信息
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool = IBCenOrg.AddReaBmsSerialOfLabID(labId);
            result = baseResultBool.success;

            if (result)
                result = EditCompareVersionInfo(curBParameter, labId, "1.0.0.1");
            else
                ZhiFang.Common.Log.Log.Error("LAB-DataBase(1.0.0.1) Update Error, Please Check The Log!");
            return result;
        }

        public BaseResultDataValue AddBParameterOfOfLabID(long labId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            IList<BParameter> allList = IBBParameter.SearchListByHQL("bparameter.ParaType='CONFIG' and bparameter.LabID=" + labId);
            if (allList == null || allList.Count <= 0)
                allList = new List<BParameter>();

            Dictionary<string, BaseClassDicEntity> tempDict = SYSParaNo.GetStatusDic();
            foreach (var dict in tempDict)
            {
                if (tempBaseResultDataValue.success == false)
                    break;

                BaseClassDicEntity dicEntity = dict.Value;
                var tempList = allList.Where(p => p.ParaType == "CONFIG" && p.ParaNo == dicEntity.Id).OrderByDescending(p => p.DataAddTime);
                if (tempList == null || tempList.Count() <= 0)
                {
                    BParameter param = new BParameter();
                    param.DispOrder = 0;
                    param.LabID = labId;
                    param.IsUse = true;
                    param.ParaType = "CONFIG";
                    param.Name = dicEntity.Name;
                    param.SName = dicEntity.SName;
                    param.ParaNo = dicEntity.Id;
                    param.ParaValue = dicEntity.DefaultValue;
                    param.ParaDesc = dicEntity.Memo;
                    EditItemEditInfo(ref param);
                    IBBParameter.Entity = param;
                    tempBaseResultDataValue.success = IBBParameter.Add();
                    if (tempBaseResultDataValue.success == false)
                        tempBaseResultDataValue.ErrorInfo = "新增机构运行参数出错!";
                }
                else if (tempList.Count() > 1)
                {
                    for (int i = 0; i < tempList.Count(); i++)
                    {
                        if (i != 0)
                        {
                            IBBParameter.Remove(tempList.ElementAt(i).Id);
                        }
                    }
                }
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 运行参数的默认用户设置信息赋值
        /// </summary>
        /// <param name="param"></param>
        private void EditItemEditInfo(ref BParameter param)
        {
            if (param.ParaNo == SYSParaNo.盘库时实盘数是否取库存数.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'是'},{'2':'否'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.是否开启近效期.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'开启'},{'2':'不开启'},{'3':'界面选择默认开启'},{'4':'界面选择默认不开启'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.是否强制近效期出库.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"4\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'强制'},{'2':'不强制'},{'3':'界面选择默认强制'},{'4':'界面选择默认不强制'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.供应批次合并.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"4\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'后台默认合并'},{'2':'后台默认不合并'},{'3':'界面选择默认合并'},{'4':'界面选择默认不合并'}]\"}";
            }
            else if (param.ParaNo == SYSParaNo.出库确认后是否上传试剂平台.Key)
            {
                param.ItemEditInfo = "{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{'1':'是'},{'2':'否'}]\"}";
            }
        }
        #endregion
    }
}
