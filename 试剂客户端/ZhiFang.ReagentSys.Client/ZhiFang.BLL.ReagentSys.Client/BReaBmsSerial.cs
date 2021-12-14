
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsSerial : BaseBLL<ReaBmsSerial>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsSerial
    {
        //IDCenOrgDao IDCenOrgDao { get; set; }
        public string GetNextBarCode(long labId, string bmsType, CenOrg cenOrg, ref long maxBarCode)
        {
            //maxBarCode = -1;
            long maxBarCode2 = ((IDReaBmsSerialDao)base.DBDao).GetMaxBarCode(labId, bmsType);
            ZhiFang.Common.Log.Log.Debug("LabId:"+ cenOrg.LabID+","+ DateTime.Now.ToString("yy-MM-dd")+ ",CurMaxBarCode:"+ maxBarCode2);
            if (maxBarCode <= maxBarCode2)
                maxBarCode = maxBarCode2;
            if (maxBarCode <= 0) return "";

            StringBuilder nextBarCode = new StringBuilder();
            //当前机构类型如果不是"实验室",条码需要添加上机构编码
            string strPrefix = "";
            if (cenOrg != null && cenOrg.OrgTypeID.ToString() != OrgType.实验室.Key)
            {
                int orgNo = cenOrg.OrgNo;
                if (orgNo.ToString().Length <= 4)
                {
                    strPrefix = orgNo.ToString().PadLeft(4, '0');
                }
                else if (orgNo.ToString().Length > 4)
                {
                    strPrefix = orgNo.ToString().Substring(orgNo.ToString().Length - 4, 4);
                }
                nextBarCode.Append(strPrefix);
            }

            //年月日
            nextBarCode.Append(DateTime.Now.ToString("yyMMdd"));
            nextBarCode.Append(maxBarCode.ToString().PadLeft(6, '0'));//左补零
            //ZhiFang.Common.Log.Log.Debug("labId:" + labId + ",bmsType:" + bmsType + ",nextBarCode:" + nextBarCode.ToString());
            return nextBarCode.ToString();
        }
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        public IList<ReaBmsSerial> GetListOfNoLabIDByHql(string hqlWhere)
        {
            IList<ReaBmsSerial> tempList = ((IDReaBmsSerialDao)base.DBDao).GetListOfNoLabIDByHql(hqlWhere);
            return tempList;
        }
    }
}