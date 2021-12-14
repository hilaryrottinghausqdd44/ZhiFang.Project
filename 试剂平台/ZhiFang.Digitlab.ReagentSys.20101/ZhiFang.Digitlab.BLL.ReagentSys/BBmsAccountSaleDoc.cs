using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using System.Web;
using System.IO;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.IBLL.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsAccountSaleDoc : BaseBLL<BmsAccountSaleDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsAccountSaleDoc
    {
        public bool DeleteBmsAccountSaleDocListByAccountID(long accountID)
        {
            bool result = true;
            string hql = " from BmsAccountSaleDoc bmsaccountsaledoc where bmsaccountsaledoc.BmsAccountInput.Id =" + accountID;
            //int counts = ((IDBmsAccountSaleDocDao)base.DBDao).DeleteByHql(hql);
            try
            {
                this.DeleteByHql(hql);
            }
            catch (Exception ex)
            {
                result = false;
                ZhiFang.Common.Log.Log.Error("DeleteBmsAccountSaleDocListByAccountID:" + ex.Message);
                throw ex;
            }
            return result;
        }
    }
}