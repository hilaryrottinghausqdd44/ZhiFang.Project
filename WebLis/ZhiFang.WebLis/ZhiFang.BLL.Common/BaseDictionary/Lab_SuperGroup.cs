using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Lab_SuperGroup		
    public partial class Lab_SuperGroup : IBSynchData, IBLab_SuperGroup
    {
        IDAL.IDLab_SuperGroup dal = DALFactory.DalFactory<IDAL.IDLab_SuperGroup>.GetDal("B_Lab_SuperGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public Lab_SuperGroup()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LabCode, int LabSuperGroupNo)
        {
            return dal.Exists(LabCode, LabSuperGroupNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_SuperGroup model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_SuperGroup model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, int LabSuperGroupNo)
        {
            return dal.Delete(LabCode, LabSuperGroupNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string SuperGroupIDlist)
        {
            return dal.DeleteList(SuperGroupIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_SuperGroup GetModel(string LabCode, int LabSuperGroupNo)
        {
            return dal.GetModel(LabCode, LabSuperGroupNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Lab_SuperGroup GetModelByCache(string LabCode, int LabSuperGroupNo)
        {

            string CacheKey = "B_Lab_SuperGroupModel-" + LabCode + LabSuperGroupNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LabCode, LabSuperGroupNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Lab_SuperGroup)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_SuperGroup> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(null);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Lab_SuperGroup> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Lab_SuperGroup> modelList = new List<ZhiFang.Model.Lab_SuperGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Lab_SuperGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Lab_SuperGroup();
                    if (dt.Rows[n]["SuperGroupID"].ToString() != "")
                    {
                        model.SuperGroupID = int.Parse(dt.Rows[n]["SuperGroupID"].ToString());
                    }
                    model.LabCode = dt.Rows[n]["LabCode"].ToString();
                    if (dt.Rows[n]["LabSuperGroupNo"].ToString() != "")
                    {
                        model.LabSuperGroupNo = int.Parse(dt.Rows[n]["LabSuperGroupNo"].ToString());
                    } if (dt.Rows[n]["ControlStatus"].ToString() != "")
                    {
                        model.ControlStatus = dt.Rows[n]["ControlStatus"].ToString();
                    }
                    model.CName = dt.Rows[n]["CName"].ToString();
                    model.ShortName = dt.Rows[n]["ShortName"].ToString();
                    model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    if (dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[n]["ParentNo"].ToString() != "")
                    {
                        model.ParentNo = int.Parse(dt.Rows[n]["ParentNo"].ToString());
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
        public DataSet GetList(ZhiFang.Model.Lab_SuperGroup model)
        {
            return dal.GetList(model);
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_SuperGroup model)
        {
            return dal.GetListByLike(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Lab_SuperGroup model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Lab_SuperGroup model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #endregion

        #region IBBase<Lab_SuperGroup> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            return dal.Exists(ht);
        }

        public int AddByDataRow(DataRow dr)
        {
            return dal.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dal.UpdateByDataRow(dr);
        }

        #endregion

        #region IBLab_SuperGroup 成员


        public DataSet GetList(int ListColCount, int p, Model.Lab_SuperGroup lab_SuperGroup)
        {
            return GetListByPage(lab_SuperGroup, ListColCount, p);
        }

        #endregion




        public DataSet GetParentSuperGroupNolist()
        {
            return dal.GetParentSuperGroupNolist();
        }
    }
}