using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_PGroup		
    public partial class Lab_PGroup : IBSynchData, IBLab_PGroup
    {
        IDAL.IDLab_PGroup dal = DALFactory.DalFactory<IDAL.IDLab_PGroup>.GetDal("B_Lab_PGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_PGroup()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode, int LabSectionNo)
        {
            return dal.Exists(LabCode, LabSectionNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_PGroup model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_PGroup model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabSectionNo)
        {
            return dal.Delete(LabCode, LabSectionNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string SectionIDlist)
        {
            return dal.DeleteList(SectionIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_PGroup GetModel(string LabCode, int LabSectionNo)
        {
            return dal.GetModel(LabCode, LabSectionNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_PGroup GetModelByCache(string LabCode, int LabSectionNo)
        {

            string CacheKey = "B_Lab_PGroupModel-" + LabCode + LabSectionNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabCode, LabSectionNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_PGroup)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_PGroup> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_PGroup> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_PGroup> modelList = new List<ZhiFang.Model.Lab_PGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_PGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_PGroup();
                    if (dt.Rows[n]["SectionID"].ToString() != "")
                    {
                        model.SectionID = int.Parse(dt.Rows[n]["SectionID"].ToString());
                    }
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Rows[n]["LabSectionNo"].ToString() != "")
                    {
                        model.LabSectionNo = int.Parse(dt.Rows[n]["LabSectionNo"].ToString());
                    } if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
                    //if (dt.Rows[n]["SuperGroupNo"].ToString() != "")
                    //{
                    //    model.SuperGroupNo = int.Parse(dt.Rows[n]["SuperGroupNo"].ToString());
                    //}
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    model.SectionDesc = dt.Rows[n]["SectionDesc"].ToString();
                    if (dt.Rows[n]["SectionType"].ToString() != "")
                    {
                        model.SectionType = int.Parse(dt.Rows[n]["SectionType"].ToString());
                    }
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[n]["OnlineTime"].ToString() != "")
                    {
                        model.OnlineTime = int.Parse(dt.Rows[n]["OnlineTime"].ToString());
                    }
                    if (dt.Rows[n]["KeyDispOrder"].ToString() != "")
                    {
                        model.KeyDispOrder = int.Parse(dt.Rows[n]["KeyDispOrder"].ToString());
                    }
                    if (dt.Rows[n]["DummyGroup"].ToString() != "")
                    {
                        model.DummyGroup = int.Parse(dt.Rows[n]["DummyGroup"].ToString());
                    }
                    if (dt.Rows[n]["UnionType"].ToString() != "")
                    {
                        model.UnionType = int.Parse(dt.Rows[n]["UnionType"].ToString());
                    }
                    if (dt.Rows[n]["SectorTypeNo"].ToString() != "")
                    {
                        model.SectorTypeNo = int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
                    }
                    if (dt.Rows[n]["SampleRule"].ToString() != "")
                    {
                        model.SampleRule = int.Parse(dt.Rows[n]["SampleRule"].ToString());
                    }
                    //if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_PGroup model)
        {
            return dal.GetList(model);
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_PGroup model)
        {
            return dal.GetListByLike(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Lab_PGroup model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Lab_PGroup model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #endregion


        #region IBBase<Lab_PGroup> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}