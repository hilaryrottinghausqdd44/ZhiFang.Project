
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BBSampleStatus : BaseBLL<BSampleStatus>, ZhiFang.Digitlab.IBLL.Business.IBBSampleStatus
	{
        public IBBOperateObjectType IBBOperateObjectType { set; get; }
        public IBBSampleStatusType IBBSampleStatusType { set; get; }

        public IList<BSampleStatus> SerachSampleStatusByOperateObjectID(long operateObjectID)
        {
            //BSampleStatus entity = new BSampleStatus();
            //entity.OperateObjectID =
           return  ((IDAO.IDBSampleStatusDao)DBDao).GetListByHQL(" OperateObjectID=" + operateObjectID, 0, 10).list;
        }

        /// <summary>
        /// 批量增加状态记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operateObjectIDList">被记录状态的对象ID列表</param>
        /// <returns>操作成功的对象ID列表</returns>
        public BaseResultDataValue AddMEPTSampleStatus(BSampleStatus entity, string objectIDList)
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
                    if (this.Add())
                    {
                        long tempID = this.Entity.Id;
                        this.DBDao.Flush();
                        this.DBDao.Evict(entity);
                        this.DBDao.UpdateByHql(" update MEPTSampleForm as mept set mept.BSampleStatus.Id=" + tempID.ToString() + " where mept.Id=" + objectID);
                    }
                }
                catch (Exception ex)
                {
                    //此处记录操作失败的记录
                }
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 增加操作对象的样本状态
        /// </summary>
        /// <param name="operateObjectID">操作对象Id</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="statusTypeLevel">操作对象状态类型代码()例如:已复核状态是Review</param>
        /// <param name="comment">状态说明</param>
        /// <returns>样本状态实体</returns>
        public BSampleStatus AddByStatusTypeLevel(string operateObjectID, string objectTypeCode, string statusTypeLevel, string comment)
        {
            BSampleStatus tempBSampleStatus = new BSampleStatus();
            //tempBSampleStatus.LabID = baseEntity.LabID;
            tempBSampleStatus.OperateObjectID = long.Parse(operateObjectID);
            tempBSampleStatus.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(objectTypeCode);
            tempBSampleStatus.BSampleStatusType = IBBSampleStatusType.GetSampleStatusTypeByLevel(statusTypeLevel);
            tempBSampleStatus.Comment = comment;
            this.Entity = tempBSampleStatus;
            if (this.Add())
                return this.Entity;
            else
                return null;
        }
        /// <summary>
        /// 增加操作对象的样本状态
        /// </summary>
        /// <param name="baseEntity">操作对象(例如:样本单)</param>
        /// <param name="objectTypeCode">操作对象类型代码(例如:样本单的代码是MEPTSampleForm)</param>
        /// <param name="statusTypeCode">操作对象状态类型代码()例如:已复核状态是Review</param>
        /// <param name="comment">状态说明</param>
        /// <returns>样本状态实体</returns>
        public BSampleStatus AddOperateObjectStatus(BaseEntity baseEntity, string objectTypeCode, string statusTypeCode, string comment)
        {
            BSampleStatus tempBSampleStatus = new BSampleStatus();
            tempBSampleStatus.LabID = baseEntity.LabID;
            tempBSampleStatus.OperateObjectID = baseEntity.Id;
            tempBSampleStatus.BOperateObjectType = IBBOperateObjectType.GetBOperateObjectTypeByCode(objectTypeCode);
            tempBSampleStatus.BSampleStatusType = IBBSampleStatusType.GetSampleStatusTypeByCode(statusTypeCode);
            tempBSampleStatus.Comment = comment;
            this.Entity = tempBSampleStatus;
            if (this.Add())
                //return this.Get(this.Entity.Id);
                return this.Entity;
            else
                return null;
        }
        public bool DeleteBSampleStatus(long? operateObjectID)
        {
            bool retult = true;
            IList<BSampleStatus> tempBSampleStatusList = new List<BSampleStatus>();
            tempBSampleStatusList = this.SearchListByHQL("bsamplestatus.OperateObjectID=" + operateObjectID);
            foreach (BSampleStatus model in tempBSampleStatusList)
            {
                retult = this.Remove(model.Id);
            }
            return retult;
        }
	}
}