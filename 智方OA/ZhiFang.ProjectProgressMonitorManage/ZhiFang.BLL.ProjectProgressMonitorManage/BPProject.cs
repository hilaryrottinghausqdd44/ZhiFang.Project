
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;


namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPProject : BaseBLL<PProject>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProject
    {
        public ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectTask IBPProjectTask { get; set; }
        /// <summary>
        /// 项目信息保存前的计算处理
        /// </summary>
        public void CalcProcessing()
        {
            //if (!this.Entity.EstiAllDays.HasValue && this.Entity.EstiEndTime.HasValue && this.Entity.EstiStartTime.HasValue)
            //{
            //    //计划人工作量
            //    this.Entity.EstiAllDays = this.Entity.EstiEndTime.Value.CompareTo(this.Entity.EstiStartTime.Value);
            //}

            //if (!this.Entity.AllDays.HasValue && this.Entity.EndTime.HasValue && this.Entity.StartTime.HasValue)
            //{
            //    //实际人工作量
            //    this.Entity.AllDays = this.Entity.EndTime.Value.CompareTo(this.Entity.StartTime.Value);
            //}

            #region 进度延期天数计算
            //进度延期天数=动态完成时间-计划完成时间
            if (this.Entity.EndTime.HasValue && this.Entity.EstiEndTime.HasValue)
            {
                this.Entity.ScheduleDelayDays = this.Entity.EndTime.Value.CompareTo(this.Entity.EstiEndTime.Value);
            }
            else
            {
                this.Entity.ScheduleDelayDays = null;
            }
            #endregion

            #region 进度延期百分比计算
            //进度延期百分比=(动态完成时间-计划完成时间)/(计划完成时间-计划开始时间)
            if (this.Entity.ReqEndTime.HasValue && this.Entity.EstiEndTime.HasValue && this.Entity.EstiStartTime.HasValue)
            {
                int days1 = this.Entity.ReqEndTime.Value.CompareTo(this.Entity.EstiEndTime.Value);

                int days2 = this.Entity.EstiEndTime.Value.CompareTo(this.Entity.EstiStartTime.Value);
                if (days2 != 0)
                    this.Entity.ScheduleDelayPercent = double.Parse(String.Format("{0:F2}", days1 / days2 / 100), System.Globalization.NumberStyles.Float);
                else
                    this.Entity.ScheduleDelayPercent = null;
            }
            else
            {
                this.Entity.ScheduleDelayPercent = null;
            }
            #endregion

            #region 工作量超百分比计算
            //工作量超百分比=(实际工作量-计划工作量)/计划工作量
            if (this.Entity.AllDays.HasValue && this.Entity.EstiAllDays.HasValue && this.Entity.EstiAllDays.Value != 0)
            {
                double? moreWorkPercent = (this.Entity.AllDays - this.Entity.EstiAllDays) / this.Entity.EstiAllDays / 100;
                moreWorkPercent = double.Parse(String.Format("{0:F2}", moreWorkPercent.Value), System.Globalization.NumberStyles.Float);
                this.Entity.MoreWorkPercent = moreWorkPercent;
            }
            else
            {
                this.Entity.MoreWorkPercent = null;
            }
            #endregion
            #region 工作量超天数计算
            //工作量超天数=实际工作量-计划工作量
            if (this.Entity.AllDays.HasValue && this.Entity.EstiAllDays.HasValue)
            {
                this.Entity.MoreWorkDays = this.Entity.AllDays - this.Entity.EstiAllDays;
            }
            else
            {
                this.Entity.MoreWorkDays = null;
            }
            #endregion
        }

        public bool AddProject(PProject entity)
        {
            if (entity == null)
                return false;
            IList<PProject> listProject = this.SearchListByHQL(" pproject.TypeID=" + entity.TypeID.ToString() + " and pproject.IsStandard=1 ");
            if (listProject != null && listProject.Count > 0)
            {
                IList<PProjectTask> listProjectTask = IBPProjectTask.SearchListByHQL(" pprojecttask.PProject.Id=" + listProject[0].Id.ToString());
                IList<PProjectTask> listLevelTask = listProjectTask.Where(p => (p.PTaskID == null || p.PTaskID == 0)).ToList();
                if (listProjectTask != null && listProjectTask.Count > 0)
                    _addChildProjectTask(entity, listLevelTask, listProjectTask, null, false,true);
            }
            this.Entity = entity;
            return this.Add();
        }

        /// <summary>
        /// 实体复制，反射实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sEntity">源实体</param>
        /// <param name="tEntity">目标实体</param>
        /// <param name="properties">不需复制的属性列表，如时间戳、映射的列表属性</param>
        private void EntityCopy<T>(T sEntity, T tEntity, IList<string> properties)
        {
            foreach (var p in tEntity.GetType().GetProperties()) // 以目标表为参照对象
            {
                if (properties.IndexOf(p.Name) >= 0)
                    continue;
                var sProperty = sEntity.GetType().GetProperty(p.Name);
                if (sProperty != null)
                {
                    if (p.Name == "DataAddTime")
                        p.SetValue(tEntity, DateTime.Now, null);
                    else
                        p.SetValue(tEntity, sProperty.GetValue(sEntity, null), null);
                }
                //var sField = sEntity.GetType().GetField(p.Name);
                //if (sField != null)
                //{
                //    p.SetValue(tEntity, sField.GetValue(null),null);
                //}
                //else
                //{
                //var sProperty = sEntity.GetType().GetProperty(p.Name);
                //if (sProperty != null) 
                //p.SetValue(tEntity, sProperty.GetValue(sEntity, null), null);
                //}
            }
        }


        private void _addChildProjectTask(PProject newProject, IList<PProjectTask> listLevelTask, IList<PProjectTask> listProjectTask, long? ptaskID, bool isStandard,bool isDefalut)
        {
            if (listProjectTask != null && listProjectTask.Count > 0)
            {
                foreach (PProjectTask pt in listLevelTask)
                {
                    PProjectTask newPT = new PProjectTask();
                    EntityCopy(pt, newPT, new List<string>() { "Id", "DataTimeStamp", "PProjectTaskProgressList" });
                    newPT.PProject = newProject;
                    IList<PProjectTask> listChildTask = listProjectTask.Where(p => p.PTaskID == pt.Id).ToList();
                    if (listChildTask != null || listChildTask.Count > 0)
                        _addChildProjectTask(newProject, listChildTask, listProjectTask, newPT.Id, isStandard, isDefalut);
                    newPT.PTaskID = ptaskID;
                    newPT.IsStandard = isStandard;

                    //新增项目需要初始化预计开始时间，预计完成时间
                    if (isDefalut )
                    {
                        //预计开始时间
                        DateTime EstiStartTime = Convert.ToDateTime(newProject.EstiStartTime);
                        if (pt.PlanTheNextFewDays > 0)
                        {
                            DateTime StartTime = EstiStartTime.AddDays(Convert.ToInt32(pt.PlanTheNextFewDays) - 1);
                            if(newProject.EstiEndTime >= StartTime)
                            {
                                newPT.EstiStartTime = StartTime;
                            }
                        }

                        if (pt.PlanTheEndFewDays > 0)
                        {
                            //预计完成时间
                            DateTime EndTime = EstiStartTime.AddDays(Convert.ToInt32(pt.PlanTheEndFewDays) - 1);
                            if (newProject.EstiStartTime <= EndTime && newProject.EstiEndTime >= EndTime)
                            {
                                newPT.EstiEndTime = EndTime;
                            }
                        }
                    }
                    IBPProjectTask.Entity = newPT;
                    IBPProjectTask.Add();
                }
            }
        }
     
        private BaseResultDataValue AddProjectByID(long projectID, long typeID, bool isStandard)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PProject entity = this.Get(projectID);
            if (entity != null)
            {
                PProject newEntity = new PProject();
                IList<PProjectTask> listProjectTask = IBPProjectTask.SearchListByHQL(" pprojecttask.PProject.Id=" + projectID.ToString());
                IList<PProjectTask> listLevelTask = listProjectTask.Where(p => (p.PTaskID == null || p.PTaskID == 0)).ToList();
                if (listProjectTask != null && listProjectTask.Count > 0)
                    _addChildProjectTask(newEntity, listLevelTask, listProjectTask, null, isStandard,false);
                EntityCopy(entity, newEntity, new List<string>() { "Id", "DataTimeStamp" });
                if (isStandard)
                    newEntity.TypeID = typeID;
                this.Entity = newEntity;
                if (this.Add())
                    baseResultDataValue.ResultDataValue = "{ id:" + "\"" + entity.Id.ToString() + "\"" + "}";
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "复制拷贝标准项目信息失败，ID：" + projectID.ToString();
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据指定的ID获取标准项目信息，ID：" + projectID.ToString();
            }
            return baseResultDataValue;
        }


        public BaseResultDataValue AddCopyProjectByID(long projectID, long typeID, bool isStandard)
        {
            //projectID = 5561167545414247637;
            //typeID = 5282197594002203309;
            //isStandard = true;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (isStandard)
            {
                IList<PProject> listProject = this.SearchListByHQL(" pproject.TypeID=" + typeID.ToString()+ " and pproject.IsStandard=1 ");
                if (listProject == null || listProject.Count == 0)
                    AddProjectByID(projectID, typeID, isStandard);
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "所选项目类型的标准项目已经存在！";
                }
            }
            else
                AddProjectByID(projectID, typeID, isStandard);
            return baseResultDataValue;
        }

        public BaseResultDataValue AddProjectTaskByProjectID(long fromProjectID, long toProjectID, bool isStandard)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PProject entity = this.Get(toProjectID);
            if (entity != null)
            {
                IList<PProjectTask> listProjectTask = IBPProjectTask.SearchListByHQL(" pprojecttask.PProject.Id=" + fromProjectID.ToString());
                IList<PProjectTask> listLevelTask = listProjectTask.Where(p => (p.PTaskID == null || p.PTaskID == 0)).ToList();
                if (listProjectTask != null && listProjectTask.Count > 0)
                    _addChildProjectTask(entity, listLevelTask, listProjectTask, null, isStandard,false);
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "复制项目任务信息失败，ID：" + toProjectID.ToString();
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据指定的ID获取项目信息，ID：" + toProjectID.ToString();
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddStandardTask(long projectID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            PProject entity = this.Get(projectID);
            if (entity != null)
            {
                IList<PProject> listProject = this.SearchListByHQL(" pproject.TypeID=" + entity.TypeID.ToString() + " and pproject.IsStandard=1 ");
                if (listProject != null && listProject.Count > 0)
                {
                    IList<PProjectTask> listProjectTask = IBPProjectTask.SearchListByHQL(" pprojecttask.PProject.Id=" + listProject[0].Id.ToString());
                    IList<PProjectTask> listLevelTask = listProjectTask.Where(p => (p.PTaskID == null || p.PTaskID == 0)).ToList();
                    if (listProjectTask != null && listProjectTask.Count > 0)
                        _addChildProjectTask(entity, listLevelTask, listProjectTask, null, false,false);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "所选项目类型的标准项目已经不存在！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据指定的ID获取项目信息，ID：" + projectID.ToString();
            }
            return baseResultDataValue;
        }
    }
}