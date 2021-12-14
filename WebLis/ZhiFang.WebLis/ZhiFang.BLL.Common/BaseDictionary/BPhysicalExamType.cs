using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class BPhysicalExamType : IBBPhysicalExamType
    {
        IDAL.IDBPhysicalExamType dal;
        public BPhysicalExamType()
        {
            dal = DALFactory.DalFactory<IDAL.IDBPhysicalExamType>.GetDal("B_PhysicalExamType", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        public System.Data.DataSet GetList(Model.BPhysicalExamType model)
        {
            return dal.GetList(model);
        }

        public int Delete(long Id)
        {
            return dal.Delete(Id);
        }
        public bool Exists(long Id)
        {
            return dal.Exists(Id);
        }
        public int Add(Model.BPhysicalExamType model)
        {
            //throw new NotImplementedException();
            return dal.Add(model);
        }
        public Model.BPhysicalExamType GetModel(long Id)
        {
            return dal.GetModel(Id);
        }
        public int Update(Model.BPhysicalExamType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.BPhysicalExamType model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.BPhysicalExamType model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.BPhysicalExamType> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.BPhysicalExamType> modelList = new List<ZhiFang.Model.BPhysicalExamType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.BPhysicalExamType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.BPhysicalExamType();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = long.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortCode") && dt.Rows[n]["ShortCode"].ToString() != "")
                    {
                        model.ShortCode = dt.Rows[n]["ShortCode"].ToString();
                    }
                    if (dt.Columns.Contains("Visible") && dt.Rows[n]["Visible"].ToString() != "")
                    {
                        model.Visible = int.Parse(dt.Rows[n]["Visible"].ToString());
                    }
                    if (dt.Columns.Contains("DispOrder") && dt.Rows[n]["DispOrder"].ToString() != "")
                    {
                        model.DispOrder = int.Parse(dt.Rows[n]["DispOrder"].ToString());
                    }
                    if (dt.Rows[0]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
                    }
                    if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        System.Byte[] tmpdts = dt.Rows[n]["DTimeStampe"] as System.Byte[];
                        model.DTimeStampe = tmpdts;
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}
