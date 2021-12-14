using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.WebAssist
{
    public interface IBGKBarcode
    {
        /// <summary>
        /// LIS同步原院感科室人员息信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncGKBarcodeInfo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.Department> GetDepartmentListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.FeeSetUp> GetFeeSetUpListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.GKBarRed> GetGKBarRedListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.LastBarcodeS> GetLastBarcodeSListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.OperateType> GetOperateTypeListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.TestTypeInfo> GetTestTypeInfoListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.TestType> GetTestTypeListByHQL(string strSqlWhere);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSqlWhere"></param>
        /// <returns></returns>
        IList<ZhiFang.Entity.WebAssist.GKBarcode.User> GetUserListByHQL(string strSqlWhere);

        /// <summary>
        /// LIS同步原院感的科室记录项结果短语信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncTestTypeInfo();

        /// <summary>
        /// 从导入的院感记录信息同步科室记录项结果短语
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncDeptPhraseInfo();
        
        /// <summary>
        /// LIS同步原院感申请记录信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncGKBarRedInfo();

    }
}
