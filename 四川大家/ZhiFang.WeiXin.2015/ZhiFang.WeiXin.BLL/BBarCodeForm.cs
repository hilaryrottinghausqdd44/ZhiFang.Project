
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BBarCodeForm : BaseBLL<BarCodeForm>, ZhiFang.WeiXin.IBLL.IBBarCodeForm
    {
        public bool IsExistBarCode(string flag, List<UiBarCode> barcodelist, out string repeatbarcodestr)
        {
            bool result = false;
            //判断数据库中是否存在
            int dsRowCount = 0;
            repeatbarcodestr = "";
            foreach (var uiBarCode in barcodelist)
            {
                IList<BarCodeForm> dsTemp = DBDao.GetListByHQL("barcodeform.BarCode='"+ uiBarCode.BarCode+"'");
                if (dsTemp != null && dsTemp.Count > 0)
                {
                    dsRowCount += dsTemp.Count;
                    repeatbarcodestr += uiBarCode.BarCode + ",";
                }
            }

            if (flag == "1")
            {
                if (dsRowCount > 0)
                    result = true;
                else
                    result = false;
            }
            else
                result = false;

            return result;
        }
    }
}