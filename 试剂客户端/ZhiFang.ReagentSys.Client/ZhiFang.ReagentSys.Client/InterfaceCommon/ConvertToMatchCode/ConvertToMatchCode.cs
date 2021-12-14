using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Common.Log;

namespace ZhiFang.ReagentSys.Client
{
    public class ConvertToMatchCode
    {
        /// <summary>
        /// 获取货品对照码
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public static string ReaGoodsConvertToMatchCode(long goodsID)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaGoods IBReaGoods = (IBReaGoods)context.GetObject("BReaGoods");

            ReaGoods reaGoods = IBReaGoods.Get(goodsID);
            Log.Info("货品[ID=" + goodsID + ",名称=" + reaGoods.CName + "]，转换为第三方对照码=" + reaGoods.MatchCode);
            return reaGoods.MatchCode;
        }

        /// <summary>
        /// 获取供应商对照码
        /// </summary>
        /// <param name="reaCendOrgId"></param>
        /// <returns></returns>
        public static string ReaCenOrgConvertToMatchCode(long reaCendOrgId)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBReaCenOrg IBReaCenOrg = (IBReaCenOrg)context.GetObject("BReaCenOrg");

            ReaCenOrg reaCendOrg = IBReaCenOrg.Get(reaCendOrgId);
            Log.Info("供应商[ID=" + reaCendOrgId + ",名称=" + reaCendOrg.CName + "]，转换为第三方对照码=" + reaCendOrg.MatchCode);
            return reaCendOrg.MatchCode;
        }

        /// <summary>
        /// 获取品牌（厂家）对照码
        /// </summary>
        /// <param name="prodOrgId"></param>
        /// <returns></returns>
        public static string ProdOrgToMatchCode(string prodOrgName)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBBDict IBBDict = (IBBDict)context.GetObject("BBDict");

            IList<BDict> orgList = IBBDict.SearchListByHQL(string.Format("CName='{0}' and BDictType.DictTypeCode='ProdOrg' and IsUse=1", prodOrgName));
            if (orgList.Count() > 0)
            {
                return orgList[0].MatchCode;
            }
            return "";
        }

        /// <summary>
        /// 获取品牌（厂家）对照码
        /// </summary>
        /// <param name="prodOrgId"></param>
        /// <returns></returns>
        public static string ProdOrgToMatchCode(long prodOrgId)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBBDict IBBDict = (IBBDict)context.GetObject("BBDict");

            IList<BDict> orgList = IBBDict.SearchListByHQL(string.Format("Id={0} and BDictType.DictTypeCode='ProdOrg' and IsUse=1", prodOrgId));
            if (orgList.Count() > 0)
            {
                return orgList[0].MatchCode;
            }
            return "";
        }

    }
}