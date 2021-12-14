using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL.Other;
using System.Data;


namespace ZhiFang.DAL.Oracle.weblis
{
    class applybase : BaseDALLisDB, IDapplybase
    {
        /// <summary>
        /// 读取申请DownloadBarCodeView
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetApply(string strWhere)
        {
            DataSet ds = new DataSet();
            try
            {
                string strSql = "select * From DownloadBarCodeView "+strWhere;
                ZhiFang.Common.Log.Log.Info("sql:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql); 
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("异常->", ex);
            }
            return ds;
        }
    }
}
