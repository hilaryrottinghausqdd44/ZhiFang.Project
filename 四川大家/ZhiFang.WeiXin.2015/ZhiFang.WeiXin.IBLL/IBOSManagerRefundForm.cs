using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.Base;
using System.IO;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBOSManagerRefundForm : IBGenericManager<OSManagerRefundForm>
	{

        BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName);
        FileStream GetExportExcelOSManagerRefundFormDetail(string where, ref string fileName);
        BaseResultBool OSManagerRefundFormOneReview(string refundFormCode, string reason, bool result,  string EmpId, string EmpName);
        BaseResultBool OSManagerRefundFormOneReview(long refundFormId, string reason, bool result, string EmpId, string EmpName);
        BaseResultBool OSManagerRefundFormTwoReview(long refundFormId, string reason, bool result, string EmpId, string EmpName);
        BaseResultBool OSManagerRefundFormThreeReview(SysWeiXinPayBack.PayBack paybackfunc,RefundFormThreeReviewVO refundformthreereviewvo, string EmpId, string EmpName);
        List<RFVO> SearchRefundFormInfoByUOFCode(string uOFCode, string weiXinUserID);
    }
}