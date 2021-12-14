

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBSampleOperate : IBGenericManager<BSampleOperate>
    {
        IBBParameter IBBParameter { set; get; }
        IBBSampleOperateType IBBSampleOperateType { set; get; }

        /// <summary>
        /// 判断对象是否可以进行某种操作
        /// </summary>
        /// <param name="operateObjectID">对象ID</param>
        /// <param name="sampleOperateID">操作类型ID</param>
        /// <returns>bool</returns>
        bool JudgeObjectOperate(long operateObjectID, long operateObjectTypeID);

        bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo);
        bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo, string OperateHost);

        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="entity">操作记录对象</param>
        /// <param name="ObjectTypeShortCode">操作对象类型ShortCode字段（MEPTOrderForm、MEPTSampleForm等）</param>
        /// <param name="OperateTypeShortCode">样本操作类型ShortCode字段</param>
        /// <returns>true/false</returns>
        bool AddByCode(BSampleOperate entity, string ObjectTypeShortCode, string OperateTypeShortCode);
        /// <summary>
        /// 增加操作对象的操作记录信息(外部程序调用)
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="OperateTypeCode">操作对象操作类型代码()例如:复核操作是Review</param>
        /// <param name="operateMemo">操作说明</param>
        /// <returns>bool</returns>
        bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo, string nodeName, string operaterID, string operater);
        bool AddObjectOperate(long labid, long id, string objectTypeCode, string OperateTypeCode, string operateMemo);
        bool DeleteBSampleOperate(long? operateObjectID);
    }
}