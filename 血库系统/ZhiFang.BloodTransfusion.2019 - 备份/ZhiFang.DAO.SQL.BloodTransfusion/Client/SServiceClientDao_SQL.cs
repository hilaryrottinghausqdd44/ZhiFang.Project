using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using ZhiFang.DAO.SQL.BloodTransfusion.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.SQL.BloodTransfusion
{
    public class SServiceClientDao_SQL : IDBaseDao<SServiceClient, long>, IDSServiceClientDao_SQL
    {
        //查询字段
        private string FieldStr = "LabID,Name,SName,Shortcode,PinYinZiTou,DispOrder,Comment,CountryID,ProvinceID,CityID,CountryName,ProvinceName,CityName,SClevelID,Principal,LinkMan,PhoneNum,PhoneNum2,Address,MailNo,Emall,ClientType,Bman,UploadType,InputDataType,ClientArea,ClientStyleID,ClientStyleName,WebLisSourceOrgID,GroupName,IsUse,DataAddTime,DataTimeStamp";
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM S_ServiceClient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
		/// 获取记录总数
		/// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM S_ServiceClient ");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public int GetListCountByHQL(string strSqlWhere)
        {
            return GetRecordCount(strSqlWhere);
        }

        public object GetTotalByHQL(string strSqlWhere, string field)
        {
            return GetRecordCount(strSqlWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" FROM S_ServiceClient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.QuerySql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SServiceClient GetModel(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from S_ServiceClient ");
            strSql.Append(" where LabID=" + id + " ");
            //SServiceClient model = new SServiceClient();
            DataSet ds = DbHelperSQL.QuerySql(strSql.ToString());
            if (ds!=null&&ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SServiceClient DataRowToModel(DataRow row)
        {
            SServiceClient model = new SServiceClient();
            if (row != null)
            {
                if (row["LabID"] != null && row["LabID"].ToString() != "")
                {
                    model.LabID = long.Parse(row["LabID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row["Shortcode"] != null)
                {
                    model.Shortcode = row["Shortcode"].ToString();
                }
                if (row["PinYinZiTou"] != null)
                {
                    model.PinYinZiTou = row["PinYinZiTou"].ToString();
                }
                if (row["DispOrder"] != null && row["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(row["DispOrder"].ToString());
                }
                if (row["Comment"] != null)
                {
                    model.Comment = row["Comment"].ToString();
                }
                if (row["CountryID"] != null && row["CountryID"].ToString() != "")
                {
                    model.CountryID = long.Parse(row["CountryID"].ToString());
                }
                if (row["ProvinceID"] != null && row["ProvinceID"].ToString() != "")
                {
                    model.ProvinceID = long.Parse(row["ProvinceID"].ToString());
                }
                if (row["CityID"] != null && row["CityID"].ToString() != "")
                {
                    model.CityID = long.Parse(row["CityID"].ToString());
                }
                if (row["CountryName"] != null)
                {
                    model.CountryName = row["CountryName"].ToString();
                }
                if (row["ProvinceName"] != null)
                {
                    model.ProvinceName = row["ProvinceName"].ToString();
                }
                if (row["CityName"] != null)
                {
                    model.CityName = row["CityName"].ToString();
                }
                if (row["SClevelID"] != null && row["SClevelID"].ToString() != "")
                {
                    model.SClevelID = long.Parse(row["SClevelID"].ToString());
                }
                if (row["Principal"] != null)
                {
                    model.Principal = row["Principal"].ToString();
                }
                if (row["LinkMan"] != null)
                {
                    model.LinkMan = row["LinkMan"].ToString();
                }
                if (row["PhoneNum"] != null)
                {
                    model.PhoneNum = row["PhoneNum"].ToString();
                }
                if (row["PhoneNum2"] != null)
                {
                    model.PhoneNum2 = row["PhoneNum2"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["MailNo"] != null)
                {
                    model.MailNo = row["MailNo"].ToString();
                }
                if (row["Emall"] != null)
                {
                    model.Emall = row["Emall"].ToString();
                }
                if (row["ClientType"] != null && row["ClientType"].ToString() != "")
                {
                    model.ClientType = long.Parse(row["ClientType"].ToString());
                }
                if (row["Bman"] != null)
                {
                    model.Bman = row["Bman"].ToString();
                }
                if (row["UploadType"] != null)
                {
                    model.UploadType = row["UploadType"].ToString();
                }
                if (row["InputDataType"] != null)
                {
                    model.InputDataType = row["InputDataType"].ToString();
                }
                if (row["ClientArea"] != null)
                {
                    model.ClientArea = row["ClientArea"].ToString();
                }
                if (row["ClientStyleID"] != null && row["ClientStyleID"].ToString() != "")
                {
                    model.ClientStyleID = long.Parse(row["ClientStyleID"].ToString());
                }
                if (row["ClientStyleName"] != null)
                {
                    model.ClientStyleName = row["ClientStyleName"].ToString();
                }
                if (row["WebLisSourceOrgID"] != null)
                {
                    model.WebLisSourceOrgID = row["WebLisSourceOrgID"].ToString();
                }
                if (row["GroupName"] != null)
                {
                    model.GroupName = row["GroupName"].ToString();
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
                    }
                }
                if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
                {
                    model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
                }
                if (row["DataTimeStamp"] != null && row["DataTimeStamp"].ToString() != "")
                {
                    model.DataTimeStamp = row["DataTimeStamp"] as byte[];
                }
            }
            return model;
        }
        public IList<SServiceClient> GetListByHQL(string strSqlWhere)
        {
            IList<SServiceClient> tempList = new List<SServiceClient>();
            DataSet ds = GetList(strSqlWhere);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }
        public SServiceClient Get(long id)
        {
            return GetModel(id);
        }
        public SServiceClient ObtainById(long labId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(this.FieldStr);
            strSql.Append(" from S_ServiceClient ");
            strSql.Append(" where LabID=" + labId + " ");
            //SServiceClient model = new SServiceClient();
            DataSet ds = DbHelperSQL.ObtainSql(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.LabID desc");
            }
            strSql.Append(")AS Row, T.*  from S_ServiceClient T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.QuerySql(strSql.ToString());
        }

        public EntityList<SServiceClient> GetListByHQL(string strSqlWhere, int start, int count)
        {
            EntityList<SServiceClient> entityList = new EntityList<SServiceClient>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, "", start, count);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public EntityList<SServiceClient> GetListByHQL(string strSqlWhere, string Order, int start, int count)
        {
            EntityList<SServiceClient> entityList = new EntityList<SServiceClient>();
            entityList.count = GetRecordCount(strSqlWhere);
            if (entityList.count <= 0) return entityList;

            DataSet ds = GetListByPage(strSqlWhere, Order, start, count);
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entityList.list.Add(DataRowToModel(row));
                }
            }
            else
            {
                return entityList;
            }
            return entityList;
        }

        public IList<SServiceClient> LoadAll()
        {
            IList<SServiceClient> tempList = new List<SServiceClient>();

            DataSet ds = GetList("");
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tempList.Add(DataRowToModel(row));
                }
            }
            else
            {
                return tempList;
            }
            return tempList;
        }

        public IList<SServiceClient> GetObjects(SServiceClient entity)
        {
            throw new NotImplementedException();
        }
        public bool BatchSaveVO(SServiceClient voList)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteByHql(string hql)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByHQL(long id)
        {
            throw new NotImplementedException();
        }

        public void Evict(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public IList<T> Find<T>(string hql)
        {
            throw new NotImplementedException();
        }
        public void Flush()
        {
            throw new NotImplementedException();
        }
        public bool Save(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public object SaveByEntity(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrUpdate(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(string[] strParas)
        {
            throw new NotImplementedException();
        }

        public bool Update(SServiceClient entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateByHql(string hql)
        {
            throw new NotImplementedException();
        }
    }
}
