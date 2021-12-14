
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BSCBarCodeRules : BaseBLL<SCBarCodeRules>, ZhiFang.IBLL.WebAssist.IBSCBarCodeRules
    {
        public BaseResultBool AddInitysOfLabId(long labID)
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            Dictionary<string, BaseClassDicEntity> tempDict = SCBarCodeRulesType.GetStatusDic();

            foreach (var dict in tempDict)
            {
                if (tempBaseResultDataValue.success == false)
                    break;

                BaseClassDicEntity dicEntity = dict.Value;
                SCBarCodeRules entity = new SCBarCodeRules();
                entity.LabID = labID;
                entity.BmsType = dicEntity.Code;
                entity.DataUpdateTime = DateTime.Now;
                entity.CurBarCode = 0;
                this.Entity = entity;
                tempBaseResultDataValue.success = this.Add();
                if (tempBaseResultDataValue.success == false)
                    tempBaseResultDataValue.ErrorInfo = "初始化机构信息失败(新增一维条码序号信息出错)!";
            }
            return tempBaseResultDataValue;
        }
        public string GetNextBarCode(long labId, string bmsType, ref long maxBarCode)
        {
            //maxBarCode = -1;
            long maxBarCode2 = ((IDSCBarCodeRulesDao)base.DBDao).GetMaxBarCode(labId, bmsType);

            //ZhiFang.Common.Log.Log.Debug("LabId:" + labId + "," + DateTime.Now.ToString("yy-MM-dd") + ",CurMaxBarCode:" + maxBarCode2);
            if (maxBarCode <= maxBarCode2)
                maxBarCode = maxBarCode2;
            if (maxBarCode <= 0) return "";

            StringBuilder nextBarCode = new StringBuilder();
            //当前机构类型如果不是"实验室",条码需要添加上机构编码
            //string strPrefix = "";
            //if (cenOrg != null && cenOrg.OrgTypeID.ToString() != OrgType.实验室.Key)
            //{
            //    int orgNo = cenOrg.OrgNo;
            //    if (orgNo.ToString().Length <= 4)
            //    {
            //        strPrefix = orgNo.ToString().PadLeft(4, '0');
            //    }
            //    else if (orgNo.ToString().Length > 4)
            //    {
            //        strPrefix = orgNo.ToString().Substring(orgNo.ToString().Length - 4, 4);
            //    }
            //    nextBarCode.Append(strPrefix);
            //}

            //年月日
            nextBarCode.Append(DateTime.Now.ToString("yyMMdd"));
            nextBarCode.Append(maxBarCode.ToString().PadLeft(6, '0'));//左补零
            //ZhiFang.Common.Log.Log.Debug("labId:" + labId + ",bmsType:" + bmsType + ",nextBarCode:" + nextBarCode.ToString());
            return nextBarCode.ToString();
        }
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        public IList<SCBarCodeRules> GetListOfNoLabIDByHql(string hqlWhere)
        {
            IList<SCBarCodeRules> tempList = ((IDSCBarCodeRulesDao)base.DBDao).GetListOfNoLabIDByHql(hqlWhere);
            return tempList;
        }

    }
}