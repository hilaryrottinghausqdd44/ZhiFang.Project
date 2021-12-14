using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
    /// GraphData
    /// </summary>
    public partial class BGraphData 
    {
        private readonly IDGraphData dal = DalFactory<IDGraphData>.GetDal("GraphData");
        public BGraphData()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {
            return dal.Exists(ReceiveDate, SectionNo, TestTypeNo, SampleNo, GraphName, GraphNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.GraphData model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.GraphData model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {

            return dal.Delete(ReceiveDate, SectionNo, TestTypeNo, SampleNo, GraphName, GraphNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.GraphData GetModel(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {

            return dal.GetModel(ReceiveDate, SectionNo, TestTypeNo, SampleNo, GraphName, GraphNo);
        }
        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            return dal.GetListByReportPublicationID
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetListByReportFormID(string ReportFormID)
        {
            return dal.GetListByReportFormID(ReportFormID);
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
        public List<Model.GraphData> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.GraphData> DataTableToList(DataTable dt)
        {
            List<Model.GraphData> modelList = new List<Model.GraphData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.GraphData model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.GraphData();
                    if (dt.Rows[n]["ReceiveDate"] != null && dt.Rows[n]["ReceiveDate"].ToString() != "")
                    {
                        model.ReceiveDate = DateTime.Parse(dt.Rows[n]["ReceiveDate"].ToString());
                    }
                    if (dt.Rows[n]["SectionNo"] != null && dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Rows[n]["TestTypeNo"] != null && dt.Rows[n]["TestTypeNo"].ToString() != "")
                    {
                        model.TestTypeNo = int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
                    }
                    if (dt.Rows[n]["SampleNo"] != null && dt.Rows[n]["SampleNo"].ToString() != "")
                    {
                        model.SampleNo = dt.Rows[n]["SampleNo"].ToString();
                    }
                    if (dt.Rows[n]["GraphName"] != null && dt.Rows[n]["GraphName"].ToString() != "")
                    {
                        model.GraphName = dt.Rows[n]["GraphName"].ToString();
                    }
                    if (dt.Rows[n]["GraphNo"] != null && dt.Rows[n]["GraphNo"].ToString() != "")
                    {
                        model.GraphNo = int.Parse(dt.Rows[n]["GraphNo"].ToString());
                    }
                    if (dt.Rows[n]["EquipNo"] != null && dt.Rows[n]["EquipNo"].ToString() != "")
                    {
                        model.EquipNo = int.Parse(dt.Rows[n]["EquipNo"].ToString());
                    }
                    if (dt.Rows[n]["PointType"] != null && dt.Rows[n]["PointType"].ToString() != "")
                    {
                        model.PointType = int.Parse(dt.Rows[n]["PointType"].ToString());
                    }
                    if (dt.Rows[n]["ShowPoint"] != null && dt.Rows[n]["ShowPoint"].ToString() != "")
                    {
                        model.ShowPoint = int.Parse(dt.Rows[n]["ShowPoint"].ToString());
                    }
                    if (dt.Rows[n]["MColor"] != null && dt.Rows[n]["MColor"].ToString() != "")
                    {
                        model.MColor = int.Parse(dt.Rows[n]["MColor"].ToString());
                    }
                    if (dt.Rows[n]["SColor"] != null && dt.Rows[n]["SColor"].ToString() != "")
                    {
                        model.SColor = dt.Rows[n]["SColor"].ToString();
                    }
                    if (dt.Rows[n]["ShowAxis"] != null && dt.Rows[n]["ShowAxis"].ToString() != "")
                    {
                        model.ShowAxis = int.Parse(dt.Rows[n]["ShowAxis"].ToString());
                    }
                    if (dt.Rows[n]["ShowLable"] != null && dt.Rows[n]["ShowLable"].ToString() != "")
                    {
                        model.ShowLable = int.Parse(dt.Rows[n]["ShowLable"].ToString());
                    }
                    if (dt.Rows[n]["MinX"] != null && dt.Rows[n]["MinX"].ToString() != "")
                    {
                        model.MinX = decimal.Parse(dt.Rows[n]["MinX"].ToString());
                    }
                    if (dt.Rows[n]["MaxX"] != null && dt.Rows[n]["MaxX"].ToString() != "")
                    {
                        model.MaxX = decimal.Parse(dt.Rows[n]["MaxX"].ToString());
                    }
                    if (dt.Rows[n]["MinY"] != null && dt.Rows[n]["MinY"].ToString() != "")
                    {
                        model.MinY = decimal.Parse(dt.Rows[n]["MinY"].ToString());
                    }
                    if (dt.Rows[n]["MaxY"] != null && dt.Rows[n]["MaxY"].ToString() != "")
                    {
                        model.MaxY = decimal.Parse(dt.Rows[n]["MaxY"].ToString());
                    }
                    if (dt.Rows[n]["ShowTitle"] != null && dt.Rows[n]["ShowTitle"].ToString() != "")
                    {
                        model.ShowTitle = int.Parse(dt.Rows[n]["ShowTitle"].ToString());
                    }
                    if (dt.Rows[n]["STitle"] != null && dt.Rows[n]["STitle"].ToString() != "")
                    {
                        model.STitle = dt.Rows[n]["STitle"].ToString();
                    }
                    if (dt.Rows[n]["GraphValue"] != null && dt.Rows[n]["GraphValue"].ToString() != "")
                    {
                        model.GraphValue = dt.Rows[n]["GraphValue"].ToString();
                    }
                    if (dt.Rows[n]["GraphMemo"] != null && dt.Rows[n]["GraphMemo"].ToString() != "")
                    {
                        model.GraphMemo = dt.Rows[n]["GraphMemo"].ToString();
                    }
                    if (dt.Rows[n]["GraphF1"] != null && dt.Rows[n]["GraphF1"].ToString() != "")
                    {
                        model.GraphF1 = dt.Rows[n]["GraphF1"].ToString();
                    }
                    if (dt.Rows[n]["GraphF2"] != null && dt.Rows[n]["GraphF2"].ToString() != "")
                    {
                        model.GraphF2 = dt.Rows[n]["GraphF2"].ToString();
                    }
                    if (dt.Rows[n]["ChartTop"] != null && dt.Rows[n]["ChartTop"].ToString() != "")
                    {
                        model.ChartTop = int.Parse(dt.Rows[n]["ChartTop"].ToString());
                    }
                    if (dt.Rows[n]["ChartHeight"] != null && dt.Rows[n]["ChartHeight"].ToString() != "")
                    {
                        model.ChartHeight = int.Parse(dt.Rows[n]["ChartHeight"].ToString());
                    }
                    if (dt.Rows[n]["ChartLeft"] != null && dt.Rows[n]["ChartLeft"].ToString() != "")
                    {
                        model.ChartLeft = int.Parse(dt.Rows[n]["ChartLeft"].ToString());
                    }
                    if (dt.Rows[n]["ChartWidth"] != null && dt.Rows[n]["ChartWidth"].ToString() != "")
                    {
                        model.ChartWidth = int.Parse(dt.Rows[n]["ChartWidth"].ToString());
                    }
                    if (dt.Rows[n]["Graphjpg"] != null && dt.Rows[n]["Graphjpg"].ToString() != "")
                    {
                        model.Graphjpg = (byte[])dt.Rows[n]["Graphjpg"];
                    }
                    if (dt.Rows[n]["IsFile"] != null && dt.Rows[n]["IsFile"].ToString() != "")
                    {
                        model.IsFile = int.Parse(dt.Rows[n]["IsFile"].ToString());
                    }
                    if (dt.Rows[n]["GraphFileName"] != null && dt.Rows[n]["GraphFileName"].ToString() != "")
                    {
                        model.GraphFileName = dt.Rows[n]["GraphFileName"].ToString();
                    }
                    if (dt.Rows[n]["GraphFileTime"] != null && dt.Rows[n]["GraphFileTime"].ToString() != "")
                    {
                        model.GraphFileTime = DateTime.Parse(dt.Rows[n]["GraphFileTime"].ToString());
                    }
                    if (dt.Rows[n]["isFileToServer"] != null && dt.Rows[n]["isFileToServer"].ToString() != "")
                    {
                        model.isFileToServer = int.Parse(dt.Rows[n]["isFileToServer"].ToString());
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        #region IBGraphData 成员

        public bool Exists(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public Model.GraphData GetModel(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public Model.GraphData GetModelByCache(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBLLBase<GraphData> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

   

        public DataSet GetList(Model.GraphData model)
        {
            return dal.GetList(model);
        }

        public List<Model.GraphData> GetModelList(Model.GraphData t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

