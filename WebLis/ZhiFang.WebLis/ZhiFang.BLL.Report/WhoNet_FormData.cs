using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
namespace ZhiFang.BLL.Report
{
    //WhoNet_FormData
    public partial class WhoNet_FormData : ZhiFang.IBLL.Report.IBWhoNet_FormData
    {

        private readonly IWhoNet_FormData dal = DalFactory<IWhoNet_FormData>.GetDalByClassName("WhoNet_FormData");
        public WhoNet_FormData()
        { }

        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.WhoNet_FormData model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.WhoNet_FormData model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete()
        {

            return dal.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet JoinCount(string LABORATORY, DateTime? SPEC_DATE, string SPEC_TYPE, string ORGANISM)
        {
            return dal.JoinCount(LABORATORY, SPEC_DATE, SPEC_TYPE, ORGANISM);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.WhoNet_FormData GetModel()
        {

            return dal.GetModel();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.WhoNet_FormData> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.WhoNet_FormData> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.WhoNet_FormData> modelList = new List<ZhiFang.Model.WhoNet_FormData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.WhoNet_FormData model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.WhoNet_FormData();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["FormID"].ToString() != "")
                    {
                        model.FormID = long.Parse(dt.Rows[n]["FormID"].ToString());
                    }
                    model.country_a = dt.Rows[n]["country_a"].ToString();
                    model.laboratory = dt.Rows[n]["laboratory"].ToString();
                    model.patient_id = dt.Rows[n]["patient_id"].ToString();
                    model.last_name = dt.Rows[n]["last_name"].ToString();
                    model.first_name = dt.Rows[n]["first_name"].ToString();
                    model.sex = dt.Rows[n]["sex"].ToString();
                    model.age = dt.Rows[n]["age"].ToString();
                    model.pat_type = dt.Rows[n]["pat_type"].ToString();
                    model.ward = dt.Rows[n]["ward"].ToString();
                    model.department = dt.Rows[n]["department"].ToString();
                    model.ward_type = dt.Rows[n]["ward_type"].ToString();
                    if (dt.Rows[n]["date_brith"].ToString() != "")
                    {
                        model.date_brith = DateTime.Parse(dt.Rows[n]["date_brith"].ToString());
                    }
                    model.institut = dt.Rows[n]["institut"].ToString();
                    model.SPEC_NUM = dt.Rows[n]["SPEC_NUM"].ToString();
                    if (dt.Rows[n]["SPEC_DATE"].ToString() != "")
                    {
                        model.SPEC_DATE = DateTime.Parse(dt.Rows[n]["SPEC_DATE"].ToString());
                    }
                    model.SPEC_TYPE = dt.Rows[n]["SPEC_TYPE"].ToString();
                    model.SPEC_CODE = dt.Rows[n]["SPEC_CODE"].ToString();
                    model.SPEC_REAS = dt.Rows[n]["SPEC_REAS"].ToString();
                    if (dt.Rows[n]["DATE_ADMIS"].ToString() != "")
                    {
                        model.DATE_ADMIS = DateTime.Parse(dt.Rows[n]["DATE_ADMIS"].ToString());
                    }
                    if (dt.Rows[n]["DATE_DISCH"].ToString() != "")
                    {
                        model.DATE_DISCH = DateTime.Parse(dt.Rows[n]["DATE_DISCH"].ToString());
                    }
                    if (dt.Rows[n]["DATE_OPER"].ToString() != "")
                    {
                        model.DATE_OPER = DateTime.Parse(dt.Rows[n]["DATE_OPER"].ToString());
                    }
                    if (dt.Rows[n]["DATE_WARD"].ToString() != "")
                    {
                        model.DATE_WARD = DateTime.Parse(dt.Rows[n]["DATE_WARD"].ToString());
                    }
                    model.DIAGNOSIS = dt.Rows[n]["DIAGNOSIS"].ToString();
                    if (dt.Rows[n]["DATE_INFEC"].ToString() != "")
                    {
                        model.DATE_INFEC = DateTime.Parse(dt.Rows[n]["DATE_INFEC"].ToString());
                    }
                    model.SITEINFECT = dt.Rows[n]["SITEINFECT"].ToString();
                    model.OPERATION = dt.Rows[n]["OPERATION"].ToString();
                    model.ORDER_MD = dt.Rows[n]["ORDER_MD"].ToString();
                    model.CLNOUTCOME = dt.Rows[n]["CLNOUTCOME"].ToString();
                    model.PHYSICIAN = dt.Rows[n]["PHYSICIAN"].ToString();
                    model.PRIOR_ABX = dt.Rows[n]["PRIOR_ABX"].ToString();
                    model.RESP_TO_TX = dt.Rows[n]["RESP_TO_TX"].ToString();
                    model.SURGEON = dt.Rows[n]["SURGEON"].ToString();
                    model.STORAGELOC = dt.Rows[n]["STORAGELOC"].ToString();
                    model.STORAGENUM = dt.Rows[n]["STORAGENUM"].ToString();
                    model.RESID_TYPE = dt.Rows[n]["RESID_TYPE"].ToString();
                    model.OCCUPATION = dt.Rows[n]["OCCUPATION"].ToString();
                    model.ETHNIC = dt.Rows[n]["ETHNIC"].ToString();
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Rows[n]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(dt.Rows[n]["DataUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["DataTimeStamp"].ToString() != "")
                    {
                        model.DataTimeStamp = DateTime.Parse(dt.Rows[n]["DataTimeStamp"].ToString());
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
            return GetList("");
        }
        #endregion

    }
}