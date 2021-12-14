
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BBSampleOperate : BaseBLL<BSampleOperate>, IBBSampleOperate
	{
        public IBBParameter IBBParameter { set; get; }
        public IBBOperateObjectType IBBOperateObjectType { set; get; }
        public IBBSampleOperateType IBBSampleOperateType { set; get; }

        public bool WriteSampleOperLog(BSampleOperate entity)
        {
            bool r = false;
            if (IBBParameter.GetOperLogPara())
            {
                this.Entity = entity;
                r  = Add();
            }
            return r;
        }

        public bool WriteSampleOperLog()
        {
            bool r = false;
            BSampleOperate entity = new BSampleOperate();
            entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
            entity.LabID = 1;
            entity.Operater = "测试";
            entity.OperateHost = "测试";
            entity.OperateMemo = "测试";
            entity.OperateType = IBBSampleOperateType.Get(4937945695667259574);

            entity.Zdy1 = "测试";
            entity.Zdy2 = "测试";
            entity.Zdy3 = "测试";
            entity.Zdy4 = "测试";
            entity.Zdy5 = "测试";
            r = WriteSampleOperLog(entity);
            return r;
        }
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
                EntityList<BSampleOperate> tempEntityList = this.SearchListByHQL(" OperateObjectID=" + operateObjectID.ToString() + " and boperateobjecttype.Id=" + operateObjectTypeID.ToString(), -1, -1);
                returnBool = (tempEntityList == null || tempEntityList.count <= 0);
            }
            return returnBool;
        }
        /// <summary>
        /// 批量保存样本单操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="objectIDList"></param>
        /// <returns></returns>
        public BaseResultDataValue AddMEPTSampleOperate(BSampleOperate entity, string objectIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string[] objectIDArray = objectIDList.Split(',');
            foreach (string objectID in objectIDArray)
            {
                try
                {
                    entity.OperateObjectID = Int64.Parse(objectID);
                    this.Entity = entity;
                    this.Entity.Id = -1;
                    this.Add();
                    this.DBDao.Flush();
                    this.DBDao.Evict(entity);
                }
                catch (Exception ex)
                {
                    //此处记录操作失败的记录
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 增加操作对象的操作记录信息
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="OperateTypeCode">操作对象操作类型代码()例如:复核操作是Review</param>
        /// <param name="operateMemo">操作说明</param>
        /// <returns>bool</returns>
        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo)
        {  
            BSampleOperate tempBSampleOperate = new BSampleOperate();
            tempBSampleOperate.LabID = baseEntity.LabID;
            tempBSampleOperate.OperateObjectID = baseEntity.Id;
            tempBSampleOperate.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(objectTypeCode);
            tempBSampleOperate.OperateType = IBBSampleOperateType.GetSampleOperateTypeByCode(OperateTypeCode);
            tempBSampleOperate.OperateMemo = operateMemo;
            tempBSampleOperate.OperateHost = null;
            tempBSampleOperate.OperateTime = DateTime.Now;
            tempBSampleOperate.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(strEmployeeID))
                tempBSampleOperate.OperaterID = Int64.Parse(strEmployeeID);
            this.Entity = tempBSampleOperate;
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
        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo, string nodeName, string operaterID, string operater)
        {
            BSampleOperate tempBSampleOperate = new BSampleOperate();
            tempBSampleOperate.LabID = baseEntity.LabID;
            tempBSampleOperate.OperateObjectID = baseEntity.Id;
            tempBSampleOperate.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(objectTypeCode);
            tempBSampleOperate.OperateType = IBBSampleOperateType.GetSampleOperateTypeByCode(OperateTypeCode);
            tempBSampleOperate.OperateMemo = operateMemo;
            tempBSampleOperate.OperateHost = nodeName;
            tempBSampleOperate.OperateTime = DateTime.Now;
            tempBSampleOperate.Operater = operater;// SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            //string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(operaterID))
                tempBSampleOperate.OperaterID = Int64.Parse(operaterID);
            this.Entity = tempBSampleOperate;
            return this.Add();
        }
        public bool AddObjectOperate(BaseEntity baseEntity, string objectTypeCode, string OperateTypeCode, string operateMemo, string OperateHost)
        {
            BSampleOperate tempBSampleOperate = new BSampleOperate();
            tempBSampleOperate.LabID = baseEntity.LabID;
            tempBSampleOperate.OperateObjectID = baseEntity.Id;
            tempBSampleOperate.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(objectTypeCode);
            tempBSampleOperate.OperateType = IBBSampleOperateType.GetSampleOperateTypeByCode(OperateTypeCode);
            tempBSampleOperate.OperateMemo = operateMemo;
            tempBSampleOperate.OperateHost = OperateHost;
            tempBSampleOperate.OperateTime = DateTime.Now;
            tempBSampleOperate.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(strEmployeeID))
                tempBSampleOperate.OperaterID = Int64.Parse(strEmployeeID);
            this.Entity = tempBSampleOperate;
            return this.Add();
        }

        /// <summary>
        /// 新增操作记录
        /// </summary>
        /// <param name="entity">操作记录对象</param>
        /// <param name="ObjectTypeShortCode">操作对象类型ShortCode字段（MEPTOrderForm、MEPTSampleForm等）</param>
        /// <param name="OperateTypeShortCode">样本操作类型ShortCode字段</param>
        /// <returns>true/false</returns>
        public bool AddByCode(BSampleOperate entity, string ObjectTypeShortCode, string OperateTypeShortCode)
        {
            entity.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(ObjectTypeShortCode);
            entity.OperateType = IBBSampleOperateType.GetSampleOperateTypeByCode(OperateTypeShortCode);
            if (entity.OperateTime == null)
            {
                entity.OperateTime = DateTime.Now;
            }
            this.Entity = entity;
            return this.Add();
        }
        public bool DeleteBSampleOperate(long? operateObjectID)
        {
            bool retult = true;
            IList<BSampleOperate> tempBSampleOperateList = new List<BSampleOperate>();
            tempBSampleOperateList = this.SearchListByHQL("bsampleoperate.OperateObjectID=" + operateObjectID);
            foreach (BSampleOperate model in tempBSampleOperateList)
            {
                retult = this.Remove(model.Id);
            }
            return retult;
        }
	}
}