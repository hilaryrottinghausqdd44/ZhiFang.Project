using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDQCMatDao : IDBaseDao<QCMat, long>
    {
        /// <summary>
        /// 根据仪器ID获取质控物列表
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatByEquipID(long longEquipID);

        /// <summary>
        /// 根据仪器ID获取质控物列表(自定义返回列)
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatCustomColumnByEquipID(long longEquipID);

        /// <summary>
        /// 根据仪器ID、仪器模块ID获取质控物列表(自定义返回列)
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <param name="longModuleID">仪器模块ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatCustomColumnByEquipModuleID(long longEquipID, long longModuleID);

        /// <summary>
        /// 根据仪器ID、仪器模块ID获取质控物列表
        /// </summary>
        /// <param name="longEquipID">仪器ID</param>
        /// <param name="longModuleID">仪器模块ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatByEquipModuleID(long longEquipID, long longModuleID);

        /// <summary>
        /// 根据检验项目ID获取质控物列表
        /// </summary>
        /// <param name="longItemID">检验项目ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatByItemID(long longItemID);
        /// <summary>
        /// 根据检验项目ID获取质控物列表
        /// </summary>
        /// <param name="longItemID">检验项目ID</param>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatByItemIDAndEquipID(long longItemID, long longEquipID);

        /// <summary>
        /// 根据HQL语句获取质控物列表
        /// </summary>
        /// <param name="strHqlWhere">HQL条件</param>
        /// <param name="start">页数</param>
        /// <param name="count">每页显示记录数</param>
        /// <returns>EntityList&lt;QCMat&gt;</returns>
        EntityList<QCMat> SearchQCMatListByHQL(string strHqlWhere, string Order, int start, int count);

        /// <summary>
        /// 根据质控项目ID列表获取质控物列表
        /// </summary>
        /// <param name="qcItemIDList">质控项目ID列表</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        IList<QCMat> SearchQCMatByQCItemIDList(IList<string> qcItemIDList);
    }
}
