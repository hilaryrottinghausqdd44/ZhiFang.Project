using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEGroupSampleFormDao : BaseDaoNHB<MEGroupSampleForm, long>, IDMEGroupSampleFormDao
    {
        public IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEMicroSmearValueListByHQL(string strHqlWhere, int page, int count)
        {
            IList<MEGroupSampleForm> lists = new List<MEGroupSampleForm>();
            string strHQL = "from MEGroupSampleForm megroupsampleform left join  megroupsampleform.GMGroup left join megroupsampleform.MEMicroSmearValueList";
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " where " + strHqlWhere;
            }
            else
            {
                return lists;
            }
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.MEGroupSampleForm tempModel = (Entity.MEGroupSampleForm)tmpobject[0];
                tempModel.GMGroup = (Entity.GMGroup)tmpobject[1];
                MEMicroSmearValue memicroSmearValue = (MEMicroSmearValue)tmpobject[2];
                if (memicroSmearValue != null && memicroSmearValue.BStainingMethod != null)
                {
                    tempModel.IsExistResult = true;
                    tempModel.MEMicroSmearValueList.Add(memicroSmearValue);
                }
                else
                {
                    tempModel.IsExistResult = false;
                }
                tempModel.IsBatchAdd = tempModel.IsExistResult;
                lists.Add(tempModel);
            }

            return lists;
        }
        public IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEMicroInoculantListByHQL(string strHqlWhere, int page, int count)
        {
            IList<MEGroupSampleForm> lists = new List<MEGroupSampleForm>();
            string strHQL = "from MEGroupSampleForm megroupsampleform left join megroupsampleform.GMGroup left join megroupsampleform.MEMicroInoculantList";
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " where " + strHqlWhere;
            }
            else
            {
                return lists;
            }
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.MEGroupSampleForm tempModel = (Entity.MEGroupSampleForm)tmpobject[0];
                tempModel.GMGroup = (Entity.GMGroup)tmpobject[1];
                MEMicroInoculant memicroInoculant = (MEMicroInoculant)tmpobject[2];
                if (memicroInoculant != null && memicroInoculant.DeleteFlag != true)
                {
                    if (memicroInoculant.MEMicroCultureValueList != null)
                    {
                        //有培养结果则提示并放弃保存
                        tempModel.IsBatchAdd = false;
                        tempModel.IsExistResult = true;
                    }
                    else
                    {
                        //如果有这个培养基,没有结果则新增
                        tempModel.IsExistResult = false;
                        tempModel.IsBatchAdd = true;
                    }
                    tempModel.MEMicroInoculantList.Add(memicroInoculant);
                }
                else
                {
                    //如果没有这个培养基，则提示并放弃保存
                    tempModel.IsBatchAdd = false;
                    tempModel.IsExistResult = false;
                }
                lists.Add(tempModel);
            }

            return lists;
        }
        public IList<MEGroupSampleForm> SearchMEGroupSampleFormAndMEGroupSampleItemListByHQL(string strHqlWhere, int page, int count)
        {
            IList<MEGroupSampleForm> lists = new List<MEGroupSampleForm>();
            string strHQL = "from MEGroupSampleForm megroupsampleform left join megroupsampleform.GMGroup left join megroupsampleform.MEGroupSampleItemList";
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " where " + strHqlWhere;
            }
            else
            {
                return lists;
            }
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.MEGroupSampleForm tempModel = (Entity.MEGroupSampleForm)tmpobject[0];
                tempModel.GMGroup = (Entity.GMGroup)tmpobject[1];
                MEGroupSampleItem megroupSampleItem = (MEGroupSampleItem)tmpobject[2];
                if (megroupSampleItem != null && megroupSampleItem.DeleteFlag != true)
                {
                    tempModel.IsExistResult = true;
                    tempModel.MEGroupSampleItemList.Add((MEGroupSampleItem)tmpobject[2]);
                }
                else
                {
                    tempModel.IsExistResult = false;
                }
                tempModel.IsBatchAdd = tempModel.IsExistResult;
                lists.Add(tempModel);
            }

            return lists;
        }

        public IList<MEGroupSampleForm> SearchMEGroupSampleFormByHQL(string strHqlWhere, int page, int count)
        {
            IList<MEGroupSampleForm> lists = new List<MEGroupSampleForm>();
            string strHQL = "from MEGroupSampleForm megroupsampleform left join megroupsampleform.GMGroup";//,megroupsampleform.MEPTOrderForm,megroupsampleform.MEPTSampleForm
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += " where " + strHqlWhere;
            }
            else
            {
                return lists;
            }
            var tmpobjectarray = this.HibernateTemplate.Find<object>(strHQL);

            foreach (var a in tmpobjectarray)
            {
                object[] tmpobject = (object[])a;
                Entity.MEGroupSampleForm tmpgmgroupitem = (Entity.MEGroupSampleForm)tmpobject[0];
                tmpgmgroupitem.GMGroup = (Entity.GMGroup)tmpobject[1];
                //tmpgmgroupitem.MEPTOrderForm = (Entity.MEPTOrderForm)tmpobject[2];
                //tmpgmgroupitem.MEPTSampleForm = (Entity.MEPTSampleForm)tmpobject[3];
                lists.Add(tmpgmgroupitem);
            }
            return lists;
        }
        /// <summary>
        /// 查询某一个检验小组下的所有小于当天的为检验中状态的检验单总数信息
        /// </summary>
        /// <param name="gmgroupId">检验小组Id</param>
        /// <returns></returns>
        public int GetMEGroupSampleFormCountsByGMGroupId(string gmgroupId)
        {
            int count = 0;
            string strHqlCount = "select count(megroupsampleform.Id) from MEGroupSampleForm megroupsampleform where megroupsampleform.MainState=" + (int)MEGroupSampleFormMainStateEnum.检验中 + " and megroupsampleform.DataAddTime<'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            if (!String.IsNullOrEmpty(gmgroupId))
            {
                strHqlCount = strHqlCount + " and megroupsampleform.GMGroup.Id=" + gmgroupId;
                DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strHqlCount);
                count = this.HibernateTemplate.Execute<int>(actionCount);
            }
            return count;
        }

        public EntityList<MEGroupSampleForm> SearchMEGroupSampleFormByHQL(string strHqlWhere, string strOrder, int start, int count)
        {
            EntityList<MEGroupSampleForm> list = new EntityList<MEGroupSampleForm>();

            string strHQL = "from MEGroupSampleForm megroupsampleform, " +
                            "BSampleOperate bsampleoperate join bsampleoperate.OperateType bsampleoperatetype, " +
                            "BSampleStatus bsamplestatus join bsamplestatus.BSampleStatusType bsamplestatustype " +
                            "where megroupsampleform.Id = bsampleoperate.OperateObjectID or megroupsampleform.Id = bsamplestatus.OperateObjectID ";

            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            if (strOrder != null && strOrder.Trim().Length > 0)
            {
                strHQL += " order by " + strOrder;
            }

            //获取样本单信息
            string strTempHQL = "select megroupsampleform " + strHQL;
            //获取样本单总数
            string strCount = "select count(distinct megroupsampleform.Id) " + strHQL;

            DaoNHBSearchByHqlAction<List<MEGroupSampleForm>, MEGroupSampleForm> action = new DaoNHBSearchByHqlAction<List<MEGroupSampleForm>, MEGroupSampleForm>(strTempHQL, start, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCount);

            list.list = this.HibernateTemplate.Execute<List<MEGroupSampleForm>>(action).Distinct().ToList();
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        public IList<MEGroupSampleForm> SearchMEGroupSampleFormOperateInfoByHQL(string strHqlWhere, string strOrder)
        {
            string strHQL = "select megroupsampleform from MEGroupSampleForm megroupsampleform where 1=1 ";// join megroupsampleform.BSampleOperateList bsampleoperate
            if (!String.IsNullOrEmpty(strHqlWhere))
            {
                strHQL += "and " + strHqlWhere;
            }
            if (!String.IsNullOrEmpty(strOrder))
            {
                strHQL += "order by " + strOrder;
            }
            var l = this.HibernateTemplate.Find<MEGroupSampleForm>(strHQL);
            return l;
        }
        /// <summary>
        /// 撤消核收:清空原检验单项目及检验单项目的医嘱及样本信息,物理删除及删除样本单及医嘱单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMEGroupSampleFormOfRevokeDistribution(MEGroupSampleForm model)
        {
            int result = 0;
            //前报告值,前值对比,前值对比状态
            if (model.MEGroupSampleItemList != null)
            {
                //StringBuilder strHQL = new StringBuilder();
                foreach (MEGroupSampleItem item in model.MEGroupSampleItemList)
                {
                    //strHQL.Append("update MEGroupSampleItem megroupsampleitem set megroupsampleitem.PreValue=null,megroupsampleitem.PreValueComp=null,megroupsampleitem.PreCompStatus=null,megroupsampleitem.MEPTSampleItem.Id=null where megroupsampleitem.Id =" + item.Id + ";");
                    result = this.UpdateByHql("update MEGroupSampleItem megroupsampleitem set megroupsampleitem.PreValue=null,megroupsampleitem.PreValueComp=null,megroupsampleitem.PreCompStatus=null,megroupsampleitem.MEPTSampleItem.Id=null where megroupsampleitem.Id =" + item.Id + ";");
                }
                //if (!String.IsNullOrEmpty(strHQL.ToString()))
                //{
                //    result = this.UpdateByHql(strHQL.ToString());
                //}
            }
            result = this.UpdateByHql(" update MEGroupSampleForm megroupsampleform set megroupsampleform.MEPTSampleForm.Id=null,megroupsampleform.MEPTOrderForm.Id =null,megroupsampleform.GBarCode = null,megroupsampleform.BSampleStatus.Id=null,megroupsampleform.DistributeFlag=0,megroupsampleform.IsHasNuclearAdmission=0,megroupsampleform.MainState=" + (int)MEGroupSampleFormMainStateEnum.检验中 + " where megroupsampleform.Id = " + model.Id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        public override bool Delete(long id)
        {
            int result = 0;
            result = this.HibernateTemplate.Delete(" From MEGroupSampleItem megroupsampleitem where megroupsampleitem.MEGroupSampleForm.Id = " + id);
            result = this.HibernateTemplate.Delete(" From MEGroupSampleForm megroupsampleform where megroupsampleform.Id = " + id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        #region 统计

        #region 普通统计
        /// <summary>
        /// 普通专业样本量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        public IList<PMEGroupSampleFormSampleQuantityStatistics> SearchNormalSampleQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string MainStateList, string GroupField)
        {
            List<string> paranamea = new List<string> { "DateStartDate", "DateEndDate", "GroupList", "ItemList", "DeptList", "SampleTypeList", "MainStateList", "WhereStr", "GroupField" };
            object[] paravaluea = new string[paranamea.Count];
            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }


            if (DateStartDate != null && DateStartDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateStartDate")] = DateStartDate.Trim();
            }
            if (DateEndDate != null && DateEndDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateEndDate")] = DateEndDate.Trim();
            }
            if (GroupList != null && GroupList.Trim() != "")
            {
                if (paranamea.IndexOf("GroupList") >= 0)
                    paravaluea[paranamea.IndexOf("GroupList")] = GroupList.Trim();
            }
            if (ItemList != null && ItemList.Trim() != "")
            {
                if (paranamea.IndexOf("ItemList") >= 0)
                    paravaluea[paranamea.IndexOf("ItemList")] = ItemList.Trim();
            }
            if (DeptList != null && DeptList.Trim() != "")
            {
                if (paranamea.IndexOf("DeptList") >= 0)
                    paravaluea[paranamea.IndexOf("DeptList")] = DeptList.Trim();
            }
            if (SampleTypeList != null && SampleTypeList.Trim() != "")
            {
                if (paranamea.IndexOf("SampleTypeList") >= 0)
                    paravaluea[paranamea.IndexOf("SampleTypeList")] = SampleTypeList.Trim();
            }
            if (MainStateList != null && MainStateList.Trim() != "")
            {
                if (paranamea.IndexOf("MainStateList") >= 0)
                    paravaluea[paranamea.IndexOf("MainStateList")] = MainStateList.Trim();
            }
            if (GroupField != null && GroupField.Trim() != "" && GroupField.Trim().Trim(',').Trim() != "")
            {
                if (paranamea.IndexOf("GroupField") >= 0)
                    paravaluea[paranamea.IndexOf("GroupField")] = GroupField.Trim();
            }
            var a = base.HibernateTemplate.FindByNamedQueryAndNamedParam<PMEGroupSampleFormSampleQuantityStatistics>("ME_GroupSampleForm_NormalSampleQuantityStatistics", paranamea.ToArray(), paravaluea);
            return a;
        }

        /// <summary>
        /// 普通专业工作量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        public IList<PMEGroupSampleFormWorkQuantityStatistics> SearchNormalWorkQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string MainStateList, string GroupField)
        {
            List<string> paranamea = new List<string> { "DateStartDate", "DateEndDate", "GroupList", "ItemList", "DeptList", "SampleTypeList", "MainStateList", "WhereStr" };
            object[] paravaluea = new string[paranamea.Count];
            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }

            if (DateStartDate != null && DateStartDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateStartDate")] = DateStartDate.Trim();
            }
            if (DateEndDate != null && DateEndDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateEndDate")] = DateEndDate.Trim();
            }
            if (GroupList != null && GroupList.Trim() != "")
            {
                if (paranamea.IndexOf("GroupList") >= 0)
                    paravaluea[paranamea.IndexOf("GroupList")] = GroupList.Trim();
            }
            if (ItemList != null && ItemList.Trim() != "")
            {
                if (paranamea.IndexOf("ItemList") >= 0)
                    paravaluea[paranamea.IndexOf("ItemList")] = ItemList.Trim();
            }
            if (DeptList != null && DeptList.Trim() != "")
            {
                if (paranamea.IndexOf("DeptList") >= 0)
                    paravaluea[paranamea.IndexOf("DeptList")] = DeptList.Trim();
            }
            if (SampleTypeList != null && SampleTypeList.Trim() != "")
            {
                if (paranamea.IndexOf("SampleTypeList") >= 0)
                    paravaluea[paranamea.IndexOf("SampleTypeList")] = SampleTypeList.Trim();
            }
            if (MainStateList != null && MainStateList.Trim() != "")
            {
                if (paranamea.IndexOf("MainStateList") >= 0)
                    paravaluea[paranamea.IndexOf("MainStateList")] = MainStateList.Trim();
            }
            var a = base.HibernateTemplate.FindByNamedQueryAndNamedParam<PMEGroupSampleFormWorkQuantityStatistics>("ME_GroupSampleForm_NormalWorkQuantityStatistics", paranamea.ToArray(), paravaluea);
            return a;
        }
        #endregion

        #region 微生物统计
        /// <summary>
        /// 微生物样本量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="PositiveFlag"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        public IList<PMEGroupSampleFormSampleQuantityStatistics> SearchMicroSampleQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string PositiveFlag, string MainStateList, string GroupField)
        {
            List<string> paranamea = new List<string> { "DateStartDate", "DateEndDate", "GroupList", "ItemList", "DeptList", "SampleTypeList", "PositiveFlag", "MainStateList", "WhereStr", "GroupField" };
            object[] paravaluea = new string[paranamea.Count];

            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }

            if (DateStartDate != null && DateStartDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateStartDate")] = DateStartDate.Trim();
            }

            if (DateEndDate != null && DateEndDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateEndDate")] = DateEndDate.Trim();
            }

            if (GroupList != null && GroupList.Trim() != "")
            {
                if (paranamea.IndexOf("GroupList") >= 0)
                    paravaluea[paranamea.IndexOf("GroupList")] = GroupList.Trim();
            }

            if (ItemList != null && ItemList.Trim() != "")
            {
                if (paranamea.IndexOf("ItemList") >= 0)
                    paravaluea[paranamea.IndexOf("ItemList")] = ItemList.Trim();
            }

            if (DeptList != null && DeptList.Trim() != "")
            {
                if (paranamea.IndexOf("DeptList") >= 0)
                    paravaluea[paranamea.IndexOf("DeptList")] = DeptList.Trim();
            }

            if (SampleTypeList != null && SampleTypeList.Trim() != "")
            {
                if (paranamea.IndexOf("SampleTypeList") >= 0)
                    paravaluea[paranamea.IndexOf("SampleTypeList")] = SampleTypeList.Trim();
            }

            if (PositiveFlag != null && PositiveFlag.Trim() != "")
            {
                if (paranamea.IndexOf("PositiveFlag") >= 0)
                    paravaluea[paranamea.IndexOf("PositiveFlag")] = PositiveFlag.Trim();
            }

            if (MainStateList != null && MainStateList.Trim() != "")
            {
                if (paranamea.IndexOf("MainStateList") >= 0)
                    paravaluea[paranamea.IndexOf("MainStateList")] = MainStateList.Trim();
            }
            if (GroupField != null && GroupField.Trim() != "" && GroupField.Trim().Trim(',').Trim() != "")
            {
                if (paranamea.IndexOf("GroupField") >= 0)
                    paravaluea[paranamea.IndexOf("GroupField")] = GroupField.Trim();
            }

            var a = base.HibernateTemplate.FindByNamedQueryAndNamedParam<PMEGroupSampleFormSampleQuantityStatistics>("ME_GroupSampleForm_MicroSampleQuantityStatistics", paranamea.ToArray(), paravaluea);
            return a;
        }

        /// <summary>
        /// 微生物工作量统计
        /// </summary>
        /// <param name="DateStartDate"></param>
        /// <param name="DateEndDate"></param>
        /// <param name="GroupList"></param>
        /// <param name="ItemList"></param>
        /// <param name="DeptList"></param>
        /// <param name="SampleTypeList"></param>
        /// <param name="PositiveFlag"></param>
        /// <param name="MainStateList"></param>
        /// <param name="GroupField"></param>
        /// <returns></returns>
        public IList<PMEGroupSampleFormWorkQuantityStatistics> SearchMicroWorkQuantityStatistics(string DateStartDate, string DateEndDate, string GroupList, string ItemList, string DeptList, string SampleTypeList, string PositiveFlag, string MainStateList, string GroupField)
        {
            List<string> paranamea = new List<string> { "DateStartDate", "DateEndDate", "GroupList", "ItemList", "DeptList", "SampleTypeList", "MainStateList", "WhereStr" };
            object[] paravaluea = new string[paranamea.Count];
            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }

            if (DateStartDate != null && DateStartDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateStartDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateStartDate")] = DateStartDate.Trim();
            }
            if (DateEndDate != null && DateEndDate.Trim() != "")
            {
                if (paranamea.IndexOf("DateEndDate") >= 0)
                    paravaluea[paranamea.IndexOf("DateEndDate")] = DateEndDate.Trim();
            }
            if (GroupList != null && GroupList.Trim() != "")
            {
                if (paranamea.IndexOf("GroupList") >= 0)
                    paravaluea[paranamea.IndexOf("GroupList")] = GroupList.Trim();
            }
            if (ItemList != null && ItemList.Trim() != "")
            {
                if (paranamea.IndexOf("ItemList") >= 0)
                    paravaluea[paranamea.IndexOf("ItemList")] = ItemList.Trim();
            }
            if (DeptList != null && DeptList.Trim() != "")
            {
                if (paranamea.IndexOf("DeptList") >= 0)
                    paravaluea[paranamea.IndexOf("DeptList")] = DeptList.Trim();
            }
            if (SampleTypeList != null && SampleTypeList.Trim() != "")
            {
                if (paranamea.IndexOf("SampleTypeList") >= 0)
                    paravaluea[paranamea.IndexOf("SampleTypeList")] = SampleTypeList.Trim();
            }
            if (MainStateList != null && MainStateList.Trim() != "")
            {
                if (paranamea.IndexOf("MainStateList") >= 0)
                    paravaluea[paranamea.IndexOf("MainStateList")] = MainStateList.Trim();
            }
            var a = base.HibernateTemplate.FindByNamedQueryAndNamedParam<PMEGroupSampleFormWorkQuantityStatistics>("ME_GroupSampleForm_MicroWorkQuantityStatistics", paranamea.ToArray(), paravaluea);
            return a;
        }
        #endregion

        #endregion
    }
}