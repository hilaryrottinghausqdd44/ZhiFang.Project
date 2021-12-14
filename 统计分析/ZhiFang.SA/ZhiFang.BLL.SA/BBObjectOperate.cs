using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.SA;
using ZhiFang.IBLL.SA;

namespace ZhiFang.BLL.SA
{
	/// <summary>
	///
	/// </summary>
	public  class BBObjectOperate : BaseBLL<BObjectOperate>, IBBObjectOperate
    {
        public IBBParameter IBBParameter { set; get; }
        public IBBOperateObjectType IBBOperateObjectType { set; get; }
        public IBBObjectOperateType IBBObjectOperateType { set; get; }

        /// <summary>
        /// 判断对象是否可以进行某种操作
        /// </summary>
        /// <param name="operateObjectID">对象ID</param>
        /// <param name="sampleOperateID">操作类型ID</param>
        /// <returns></returns>
        public bool JudgeObjectOperate(long operateObjectID, long operateObjectTypeID)
        {
            bool returnBool = false;
            if (operateObjectID > 0 && operateObjectTypeID > 0)
            {
                EntityList<BObjectOperate> tempEntityList = this.SearchListByHQL(" OperateObjectID=" + operateObjectID.ToString() + " and boperateobjecttype.Id=" + operateObjectTypeID.ToString(), -1, -1);
                returnBool = (tempEntityList == null || tempEntityList.count <= 0);
            }
            return returnBool;
        }

        /// <summary>
        /// 增加操作对象的操作记录信息
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="OperateTypeCode">操作对象操作类型代码()例如:复核操作是Review</param>
        /// <param name="operateMemo">操作说明</param>
        /// <returns>bool</returns>
        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string operateTypeCode, string operateMemo)
        {
            BObjectOperate tempBObjectOperate = new BObjectOperate();
            tempBObjectOperate.LabID = baseEntity.LabID;
            tempBObjectOperate.OperateObjectID = baseEntity.Id;
            tempBObjectOperate.BOperateObjectType = IBBOperateObjectType.GetOrAddOperateObjectTypeByCode(objectTypeCode);
            tempBObjectOperate.OperateType = IBBObjectOperateType.GetOrAddObjectOperateTypeByCode(operateTypeCode, operateMemo);
            tempBObjectOperate.OperateObjectTypeCode = objectTypeCode;
            tempBObjectOperate.OperateTypeCode = operateTypeCode;
            tempBObjectOperate.OperateMemo = operateMemo;
            tempBObjectOperate.OperateHost = null;
            tempBObjectOperate.OperateTime = DateTime.Now;
            tempBObjectOperate.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(strEmployeeID))
                tempBObjectOperate.OperaterID = Int64.Parse(strEmployeeID);
            this.Entity = tempBObjectOperate;
            return this.Add();
        }

        /// <summary>
        /// 增加操作对象的操作记录信息(外部程序调用)
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="OperateTypeCode">操作对象操作类型代码()例如:复核操作是Review</param>
        /// <param name="operateMemo">操作说明</param>
        /// <returns>bool</returns>
        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string operateTypeCode, string operateMemo, string nodeName, string operaterID, string operater)
        {
            BObjectOperate tempBObjectOperate = new BObjectOperate();
            tempBObjectOperate.LabID = baseEntity.LabID;
            tempBObjectOperate.OperateObjectID = baseEntity.Id;
            tempBObjectOperate.BOperateObjectType = IBBOperateObjectType.GetOrAddOperateObjectTypeByCode(objectTypeCode);
            tempBObjectOperate.OperateType = IBBObjectOperateType.GetOrAddObjectOperateTypeByCode(operateTypeCode, operateMemo);
            tempBObjectOperate.OperateObjectTypeCode = objectTypeCode;
            tempBObjectOperate.OperateTypeCode = operateTypeCode;
            tempBObjectOperate.OperateMemo = operateMemo;
            tempBObjectOperate.OperateHost = nodeName;
            tempBObjectOperate.OperateTime = DateTime.Now;
            tempBObjectOperate.Operater = operater;// SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            //string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(operaterID))
                tempBObjectOperate.OperaterID = Int64.Parse(operaterID);
            this.Entity = tempBObjectOperate;
            return this.Add();
        }

        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string operateTypeCode, string operateMemo, string OperateHost)
        {
            BObjectOperate tempBObjectOperate = new BObjectOperate();
            tempBObjectOperate.LabID = baseEntity.LabID;
            tempBObjectOperate.OperateObjectID = baseEntity.Id;
            tempBObjectOperate.BOperateObjectType = IBBOperateObjectType.GetOrAddOperateObjectTypeByCode(objectTypeCode);
            tempBObjectOperate.OperateType = IBBObjectOperateType.GetOrAddObjectOperateTypeByCode(operateTypeCode, operateMemo);
            tempBObjectOperate.OperateObjectTypeCode = objectTypeCode;
            tempBObjectOperate.OperateTypeCode = operateTypeCode;
            tempBObjectOperate.OperateMemo = operateMemo;
            tempBObjectOperate.OperateHost = OperateHost;
            tempBObjectOperate.OperateTime = DateTime.Now;
            tempBObjectOperate.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(strEmployeeID))
                tempBObjectOperate.OperaterID = Int64.Parse(strEmployeeID);
            this.Entity = tempBObjectOperate;
            return this.Add();
        }

        /// <summary>
        /// 增加操作对象的操作记录信息
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="OperateTypeCode">操作对象操作类型代码()例如:复核操作是Review</param>
        /// <param name="operateMemo">操作说明</param>
        /// <returns>bool</returns>
        public bool AddObjectOperate(long id, long labid, string objectTypeCode, string operateTypeCode, string operateMemo)
        {
            BObjectOperate tempBObjectOperate = new BObjectOperate();
            tempBObjectOperate.LabID = labid;
            tempBObjectOperate.OperateObjectID = id;
            tempBObjectOperate.BOperateObjectType = IBBOperateObjectType.GetOrAddOperateObjectTypeByCode(objectTypeCode);
            tempBObjectOperate.OperateType = IBBObjectOperateType.GetOrAddObjectOperateTypeByCode(operateTypeCode, operateMemo);
            tempBObjectOperate.OperateObjectTypeCode = objectTypeCode;
            tempBObjectOperate.OperateTypeCode = operateTypeCode;
            tempBObjectOperate.OperateMemo = operateMemo;
            tempBObjectOperate.OperateHost = null;
            tempBObjectOperate.OperateTime = DateTime.Now;
            tempBObjectOperate.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(strEmployeeID))
                tempBObjectOperate.OperaterID = Int64.Parse(strEmployeeID);
            this.Entity = tempBObjectOperate;
            return this.Add();
        }

        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="entity">操作记录对象</param>
        /// <param name="ObjectTypeShortCode">操作对象类型ShortCode字段（MEPTOrderForm、MEPTSampleForm等）</param>
        /// <param name="OperateTypeShortCode">样本操作类型ShortCode字段</param>
        /// <returns>true/false</returns>
        public bool AddByCode(BObjectOperate entity, string ObjectTypeShortCode, string OperateTypeShortCode)
        {
            entity.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(ObjectTypeShortCode);
            entity.OperateType = IBBObjectOperateType.GetObjectOperateTypeByCode(OperateTypeShortCode);
            if (entity.OperateTime == null)
            {
                entity.OperateTime = DateTime.Now;
            }
            this.Entity = entity;
            return this.Add();
        }
        public bool DeleteBObjectOperate(long? operateObjectID)
        {
            bool retult = true;
            IList<BObjectOperate> tempBObjectOperateList = new List<BObjectOperate>();
            tempBObjectOperateList = this.SearchListByHQL("bobjectoperate.OperateObjectID=" + operateObjectID);
            foreach (BObjectOperate model in tempBObjectOperateList)
            {
                retult = this.Remove(model.Id);
            }
            return retult;
        }
	}
}