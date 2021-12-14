
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBTestItemControl : ZhiFang.BLL.Base.BaseBLL<BTestItemControl>, ZhiFang.WeiXin.IBLL.IBBTestItemControl
    {
        public string GetCenterNo(string LabCode, string LabPrimaryNo)
        {
            string CenterNo = "";
            try
            {
                IList<BTestItemControl> ds = DBDao.GetListByHQL("btestitemcontrol.ControlLabNo='"+ LabCode.Trim()+ "' and btestitemcontrol.ControlItemNo='"+ LabPrimaryNo.Trim()+"'");
                if (ds != null && ds.Count > 0 )
                {
                    CenterNo = ds[0].ItemNo.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                CenterNo = "";
                ZhiFang.Common.Log.Log.Error("BBTestItemControl.GetCenterNo异常 ||| LabCode=" + LabCode + " ||| LabPrimaryNo=" + LabPrimaryNo, ex);
            }
            return CenterNo;
        }
    }
}