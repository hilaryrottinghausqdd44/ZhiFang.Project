using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    //Department		
    public partial class Department : IBSynchData, IBDepartment, IBBatchCopy, IBDataDownload
    {
        IDAL.IDDepartment dal;
        IDAL.IDBatchCopy dalCopy;

        public Department()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDDepartment>.GetDal("Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDDepartment>.GetDal("B_Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DeptNo)
        {
            return dal.Exists(DeptNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Department model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Department model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DeptNo)
        {
            return dal.Delete(DeptNo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string DeptIDlist)
        {
            return dal.DeleteList(DeptIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Department GetModel(int DeptNo)
        {
            return dal.GetModel(DeptNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.Department GetModelByCache(int DeptNo)
        {

            string CacheKey = "B_DepartmentModel-" + DeptNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DeptNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.Department)objModel;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.Department> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.Department> modelList = new List<ZhiFang.Model.Department>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.Department model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.Department();
                    if (dt.Columns.Contains("DeptID") && dt.Rows[n]["DeptID"].ToString() != "")
                    {
                        model.DeptID = int.Parse(dt.Rows[n]["DeptID"].ToString());
                    }
                    if (dt.Columns.Contains("DeptNo") && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
                    }
                    if (dt.Columns.Contains("CName") && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Columns.Contains("ShortName") && dt.Rows[n]["ShortName"].ToString() != "")
                    {
                        model.ShortName = dt.Rows[n]["ShortName"].ToString();
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
                    if (dt.Columns.Contains("HisOrderCode") && dt.Rows[n]["HisOrderCode"].ToString() != "")
                    {
                        model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    }
                    //if (dt.Columns.Contains("DTimeStampe") && dt.Rows[n]["DTimeStampe"].ToString() != "")
                    //{
                    //    model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    //}
                    if (dt.Columns.Contains("AddTime") && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Columns.Contains("StandCode") && dt.Rows[n]["StandCode"].ToString() != "")
                    {
                        model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    }
                    if (dt.Columns.Contains("ZFStandCode") && dt.Rows[n]["ZFStandCode"].ToString() != "")
                    {
                        model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
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
            ZhiFang.Model.Department model = new Model.Department();
            return GetList(model);
        }
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Department model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.Department model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.Department model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }
        #endregion


        #region IBDepartment 成员


        public DataSet GetListLike(Model.Department model)
        {
            return dal.GetListLike(model);
        }

        #endregion

        #region IBDepartment 成员


        public DataSet GetList(int p, Model.Department dept)
        {
            return this.GetListByPage(dept, 0, p);
        }

        #endregion

        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml结构文件
                strXML = dsAll.GetXml();//数据内容xml文件
                strMsg = "通过服务获取XML成功";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.Department.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，Department获取最新字典数据出现异常";
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<Department> 成员


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
            return dalCopy.AddByDataRow(dr);
        }

        public int UpdateByDataRow(DataRow dr)
        {
            return dalCopy.UpdateByDataRow(dr);
        }

        public int DeleteByDataRow(DataRow dr)
        {
            return dalCopy.DeleteByDataRow(dr);
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }


        public int Add(List<Model.Department> modelList)
        {
            return dal.Add(modelList);
        }

        public int Update(List<Model.Department> modelList)
        {
            return dal.Update(modelList);
        }
    }
}