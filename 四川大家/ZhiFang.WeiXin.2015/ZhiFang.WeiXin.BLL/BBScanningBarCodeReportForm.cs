
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBScanningBarCodeReportForm : ZhiFang.BLL.Base.BaseBLL<BScanningBarCodeReportForm>, ZhiFang.WeiXin.IBLL.IBBScanningBarCodeReportForm
	{
        public IBBSearchAccountReportForm IBBSearchAccountReportForm { get; set; }

        public bool AddSearch(string Barcode, string SearchUserName, out int count)
        {
            count = 0;
            if (!this.Add())
            {
                return false;
            }
            else
            {
                RFReportFormIndexInfo r = new RFReportFormIndexInfo();
                var rflist = IBBSearchAccountReportForm.SearchListByHQL(" BARCODE='" + Barcode + "' and NAME='" + SearchUserName + "'");
                if (rflist != null && rflist.Count != 0)
                {
                    count = rflist.Count;
                }
                return true;
            }
        }

        public bool UpdateReadFlag(long BScanningBarCodeReportFormID)
        {
            return (this.DBDao.UpdateByHql("update BScanningBarCodeReportForm as bsbcr set bsbcr.ReadedFlag=true where bsbcr.Id=" + BScanningBarCodeReportFormID )>0);
        }
    }
}