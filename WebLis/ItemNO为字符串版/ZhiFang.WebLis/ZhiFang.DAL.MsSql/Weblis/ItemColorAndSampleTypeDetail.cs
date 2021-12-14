using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using System.Data.SqlClient;
using System.Data;
namespace ZhiFang.DAL.MsSql.Weblis
{
    public class ItemColorAndSampleTypeDetail : BaseDALLisDB, IDItemColorAndSampleTypeDetail
    {

        public ItemColorAndSampleTypeDetail(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public ItemColorAndSampleTypeDetail()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }

        public bool Exists(int ColorId, int SampleTypeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ItemColorAndSampleTypeDetail");
            strSql.Append(" where ColorID=" + ColorId + " ");
            strSql.Append(" and  SampleTypeNo=" + SampleTypeNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int Add(Model.ItemColorAndSampleTypeDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ItemColorAndSampleTypeDetail(");
            strSql.Append("ColorId,SampleTypeNo)");
            strSql.Append(" values (");
            strSql.Append("@ColorId,@SampleTypeNo)");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ColorId", SqlDbType.Int),
					new SqlParameter("@SampleTypeNo", SqlDbType.Int)};
            parameters[0].Value = model.ColorID;
            parameters[1].Value = model.SampleTypeNo;

            object obj = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);//.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(Model.ItemColorAndSampleTypeDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ItemColorAndSampleTypeDetail set ");
            if (model.ColorID != null)
            {
                strSql.Append("ColorID='" + model.ColorID + "',");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo='" + model.SampleTypeNo + "',");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ColorID=" + model.ColorID + " ");
            strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }

        public DataSet GetList(Model.ItemColorAndSampleTypeDetail t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.ItemColorAndSampleTypeDetail t, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ItemColorAndSampleTypeDetail t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Model.ItemColorAndSampleTypeDetail t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        private DataSet GetItemColorAndSampleDetailByColorId(string ColorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ColorId,SampleTypeNo from ItemColorAndSampleTypeDetail ");
            strSql.Append(" where ColorId=" + ColorId + " ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColorId"></param>
        /// <returns></returns>
        public Model.UiModel.UiItemColorSampleTypeNo GetItemColorAndSampleDetail(string ColorId)
        {
            DataSet ds = GetItemColorAndSampleDetailByColorId(ColorId);
            Model.UiModel.UiItemColorSampleTypeNo model = new Model.UiModel.UiItemColorSampleTypeNo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<string> sampleTypeNoList = new List<string>();
                List<string> sampleTypeNameList = new List<string>();
                DataRowCollection drs = ds.Tables[0].Rows;
                foreach (DataRow dr in drs)
                {
                    if (dr["ColorId"].ToString() != "")
                    {
                        model.ColorId = dr["ColorId"].ToString();
                        ItemColorDict itemColorDict = new ItemColorDict();
                        Model.ItemColorDict m = itemColorDict.GetModel(int.Parse(dr["ColorId"].ToString()));
                        if (m != null)
                            model.ColorName = m.ColorName;
                    }


                    if (dr["SampleTypeNo"].ToString() != "")
                    {
                        sampleTypeNoList.Add(dr["SampleTypeNo"].ToString());
                        SampleType samptyle = new SampleType();
                        Model.SampleType sampletypeModel = samptyle.GetModel(int.Parse(dr["SampleTypeNo"].ToString()));
                        if (sampletypeModel != null)
                            sampleTypeNameList.Add(sampletypeModel.CName);

                    }

                }
                model.SampleTypeNoList = sampleTypeNoList;
                model.SampleTypeNameList = sampleTypeNameList;
            }

            return model;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Listmodel"></param>
        /// <returns></returns>
        public bool SaveToItemColorAndSampleTypeDetail(List<Model.ItemColorAndSampleTypeDetail> ListModel)
        {
            bool result = false;

            foreach (Model.ItemColorAndSampleTypeDetail model in ListModel)
            {               
                if (Add(model) > 0)
                {
                    result = true;
                }
                else
                    result = false;
                
            }
            return result;
        }

        public int DeleteItemColorAndSampleTypeDetailByColorId(string ColorId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" delete from ItemColorAndSampleTypeDetail ");
            strSql.Append(" where ColorId=" + ColorId + " ");
            object obj = DbHelperSQL.ExecuteNonQuery(strSql.ToString());//.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }

        }
        public DataSet DownloadItemColorAndSampleTypeDetail(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ItemColorAndSampleTypeDetail.Id,SampleType.SampleTypeNo,SampleType.CName as SampleTypeName,ItemColorDict.ColorID,ColorName,ColorValue,ItemColorAndSampleTypeDetail.DataTimeStamp  FROM dbo.SampleType INNER JOIN  ItemColorAndSampleTypeDetail ON SampleType.SampleTypeNo = ItemColorAndSampleTypeDetail.SampleTypeNo  INNER JOIN ItemColorDict ON ItemColorAndSampleTypeDetail.ColorId = dbo.ItemColorDict.ColorID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            throw new NotImplementedException();
        }
    }
}
