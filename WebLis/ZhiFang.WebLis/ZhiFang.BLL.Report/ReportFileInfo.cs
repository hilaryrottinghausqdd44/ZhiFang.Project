using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;

namespace ZhiFang.BLL.Report
{
    //ReportFileInfo
    public partial class ReportFileInfo : ZhiFang.IBLL.Report.IReportFileInfo
    {
        private readonly IReportFileInfo dal = DalFactory<IReportFileInfo>.GetDalByClassName("ReportFileInfo");
        public ReportFileInfo()
        { }

        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportFileInfo model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportFileInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string File_Url)
        {

            return dal.Delete(File_Url);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportFileInfo GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<Model.ReportFileInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportFileInfo> DataTableToList(DataTable dt)
        {
            List<Model.ReportFileInfo> modelList = new List<Model.ReportFileInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.ReportFileInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.ReportFileInfo();
                    if (dt.Rows[n]["G_ID"].ToString() != "")
                    {
                        model.G_ID = int.Parse(dt.Rows[n]["G_ID"].ToString());
                    }
                    if (dt.Rows[n]["DataTimeStamp"].ToString() != "")
                    {
                        model.DataTimeStamp = DateTime.Parse(dt.Rows[n]["DataTimeStamp"].ToString());
                    }
                    model.OperaTion = dt.Rows[n]["OperaTion"].ToString();
                    model.PAT_ID = dt.Rows[n]["PAT_ID"].ToString();
                    model.Card_ID = dt.Rows[n]["Card_ID"].ToString();
                    model.ClinicType = dt.Rows[n]["ClinicType"].ToString();
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Age = dt.Rows[n]["Age"].ToString();
                    model.Sex = dt.Rows[n]["Sex"].ToString();
                    model.MobilePhone = dt.Rows[n]["MobilePhone"].ToString();
                    model.Report_Time = dt.Rows[n]["Report_Time"].ToString();
                    //model.Medical_Institution_ID = dt.Rows[n]["Medical_Institution_ID"].ToString();  删除
                    model.Medical_Institution_Code = dt.Rows[n]["Medical_Institution_Code"].ToString();
                    model.File_Name = dt.Rows[n]["File_Name"].ToString();
                    model.File_Url = dt.Rows[n]["File_Url"].ToString();
                    model.ChangeStatus = (int)dt.Rows[n]["ChangeStatus"];
                    model.UniqueID = (int)dt.Rows[n]["UniqueID"];
                    model.AddDataTime = (DateTime)dt.Rows[n]["AddDataTime"];
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
        //#region IReportFileInfo 成员
        //int IBLL.Report.IReportFileInfo.Update(Model.ReportFileInfo model)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Delete(long AntiID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Model.ReportFileInfo GetModel(long AntiID)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        #region IReportFileInfo 成员


        public Model.ReportFileInfo GetModel(long AntiID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}