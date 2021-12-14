using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
   public   interface IDPrintHtml:IDataBase <Model .PrintHtml >
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
       bool Exists(string Formno);
        /// <summary>
        /// 删除一条数据
        /// </summary>
       int Delete(string Formno);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
       Model.PrintHtml GetModel(string Formno);
       DataSet GetHtmlPrintInfo(string formno);
    } 
}
