using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBbarCodeSeq : IBBase<ZhiFang.Model.barCodeSeq>, IBDataPage<ZhiFang.Model.barCodeSeq>
    {
        #region  成员方法
        string GetMaxBarCodeOrderNum(string LabCode, string Operdate);

        string GetBarCode(string LabCode, string Operdate);
        #endregion  成员方法
    }
}
