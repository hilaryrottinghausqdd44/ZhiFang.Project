using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisOperate : BaseBLL<LisOperate>, ZhiFang.IBLL.LabStar.IBLisOperate
    {

        public BaseResultDataValue AddLisOperate(long? empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisOperate lisOperate = new LisOperate();
            this.Entity = lisOperate;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisOperate(LisTestForm testForm, BaseClassDicEntity operateTypeEntity, long? empID, string empName, long? deptID, string deptName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisOperate lisOperate = new LisOperate();
            lisOperate.PartitionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")); //分区日期
            lisOperate.OperateType = operateTypeEntity.Name; //操作类型
            lisOperate.OperateTypeID = long.Parse(operateTypeEntity.Id); //操作类型ID
            lisOperate.OperateObject = ""; //业务表名称
            lisOperate.OperateObjectID = testForm.Id; //关联业务表主键ID
            lisOperate.OperateFormID = 0; //关联业务主表ID
            lisOperate.OperateName = ""; //操作标识名称
            lisOperate.OperateMemo = ""; //操作内容
            lisOperate.OperateMemoAuto = ""; //操作内容自动记录
            lisOperate.OperateUserID = empID;  //操作人员ID
            lisOperate.OperateUser = empName;  //操作人员
            lisOperate.OperateDeptID = deptID; //操作部门科室ID
            lisOperate.OperateDept = deptName; //操作部门科室
            lisOperate.OperateHost = ""; //操作站点
            lisOperate.OperateAddress = ""; //操作站点地址
            lisOperate.OperateHostType = ""; //操作站点类型
            lisOperate.BarCode = "";
            lisOperate.RelationUser = ""; //业务相关人员			
            lisOperate.RelationUserID = null; //业务相关人员ID
            lisOperate.TranceTime = null; //业务相关人员ID
                                          //lisOperate.IsTrance //迁移标志
                                          //lisOperate.IOFlag //数据发送标志
                                          //lisOperate.IOTime //数据发送时间
                                          //lisOperate.IOUserID //数据发送人ID
                                          //lisOperate.IOUserName //数据发送人
            this.Entity = lisOperate;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, SysCookieValue sysCookieValue)
        {
            return AddLisOperate(operateObject, operateTypeEntity, operateMemo, "", sysCookieValue);
        }


        public BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, string dataInfo, SysCookieValue sysCookieValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisOperate lisOperate = _getLisOperate(operateObject, operateTypeEntity, operateMemo, dataInfo, sysCookieValue);
            this.Entity = lisOperate;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisOperate(LisTestItem testItem, long? empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisOperate lisOperate = new LisOperate();
            this.Entity = lisOperate;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        private LisOperate _getLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, string dataInfo, SysCookieValue sysCookieValue)
        {
            LisOperate lisOperate = new LisOperate();
            string entityTypeName = operateObject.GetType().Name;
            entityTypeName = entityTypeName.Replace("Proxy", "");
            lisOperate.PartitionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")); //分区日期
            if (operateTypeEntity != null)
            {
                lisOperate.OperateType = operateTypeEntity.Memo; //操作类型
                lisOperate.OperateTypeID = long.Parse(operateTypeEntity.Id); //操作类型ID
            }
            lisOperate.OperateObject = entityTypeName; //业务表名称
            lisOperate.OperateObjectID = operateObject.Id; //关联业务表主键ID
            lisOperate.OperateFormID = 0; //关联业务主表ID
            lisOperate.OperateName = ""; //操作标识名称
            lisOperate.OperateMemo = operateMemo; //操作内容
            lisOperate.OperateMemoAuto = ""; //操作内容自动记录
            lisOperate.DataInfo = dataInfo;
            if (sysCookieValue != null)
            {
                lisOperate.OperateUserID = sysCookieValue.EmpID;  //操作人员ID
                lisOperate.OperateUser = sysCookieValue.EmpName;  //操作人员
                lisOperate.OperateDeptID = sysCookieValue.DeptID; //操作部门科室ID
                lisOperate.OperateDept = sysCookieValue.DeptName; //操作部门科室
                lisOperate.OperateHost = sysCookieValue.HostName; //操作站点
                lisOperate.OperateAddress = sysCookieValue.HostAddress; //操作站点地址
                lisOperate.OperateHostType = sysCookieValue.HostType; //操作站点类型
            }
            lisOperate.RelationUser = ""; //业务相关人员			
            lisOperate.RelationUserID = null; //业务相关人员ID
            lisOperate.TranceTime = null; //业务相关人员ID
            lisOperate.BarCode = "";
            //lisOperate.IsTrance //迁移标志
            //lisOperate.IOFlag //数据发送标志
            //lisOperate.IOTime //数据发送时间
            //lisOperate.IOUserID //数据发送人ID
            //lisOperate.IOUserName //数据发送人
            return lisOperate;
        }

        public BaseResultDataValue AddLisOperate(BaseEntity operateObject, BaseClassDicEntity operateTypeEntity, string operateMemo, SysCookieValue sysCookieValue, string RelationUser = "", long RelationUserID = 0)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisOperate lisOperate = _getLisOperate(operateObject, operateTypeEntity, operateMemo, "", sysCookieValue);
            lisOperate.RelationUser = RelationUser;
            lisOperate.RelationUserID = RelationUserID;
            this.Entity = lisOperate;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

    }
}