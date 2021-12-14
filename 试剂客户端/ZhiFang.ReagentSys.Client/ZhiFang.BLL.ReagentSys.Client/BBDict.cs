using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using System.Collections.Generic;
using System;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public  class BBDict : BaseBLL<BDict>, IBBDict
	{
        public IDBDictTypeDao IDBDictTypeDao { get; set; }

        /// <summary>
        /// 接口同步保存品牌（厂商）
        /// </summary>
        /// <returns></returns>
        public BaseResultData SaveProdOrgByInterface(string matchCode, Dictionary<string, object> dicFieldAndValue, ref BDict bDict)
        {
            BaseResultData baseResultData = new BaseResultData();
            IList<BDict> orgList = this.SearchListByHQL(string.Format("MatchCode='{0}' and BDictType.DictTypeCode='ProdOrg'", matchCode));
            bool isEdit = (orgList.Count > 0);

            //获取字典类型
            IList<BDictType> dicTypeList = IDBDictTypeDao.GetListByHQL(string.Format("DictTypeCode='ProdOrg'"));

            if (isEdit)
            {
                bDict = orgList[0];
                bDict.CName = dicFieldAndValue["CName"].ToString();
                bDict.IsUse = true;                
                this.Entity = bDict;
                this.Edit();
            }
            else
            {
                bDict = new BDict();
                bDict.BDictType = dicTypeList[0];
                bDict.CName = dicFieldAndValue["CName"].ToString();
                bDict.IsUse = true;
                bDict.DataAddTime = DateTime.Now;
                bDict.MatchCode = matchCode;
                this.Entity = bDict;
                this.Add();
            }
            return baseResultData;
        }

    }
}
