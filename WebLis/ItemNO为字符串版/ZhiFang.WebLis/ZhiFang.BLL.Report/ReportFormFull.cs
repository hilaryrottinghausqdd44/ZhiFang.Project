using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;

namespace ZhiFang.BLL.Report
{
    /// <summary>
    /// ReportFormFull
    /// </summary>
    public partial class ReportFormFull : ZhiFang.IBLL.Report.IBReportFormFull
    {
        private readonly IDReportFormFull dal = DalFactory<IDReportFormFull>.GetDalByClassName("ReportFormFull");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDLab_SampleType idls = DalFactory<IDLab_SampleType>.GetDalByClassName("B_Lab_SampleType");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");
        public ReportFormFull()
        {

        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReportFormID)
        {
            return dal.Exists(ReportFormID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ReportFormFull model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.ReportFormFull model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ReportFormID)
        {
            return dal.Delete(ReportFormID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ReportFormFull GetModel(string ReportFormID)
        {

            return dal.GetModel(ReportFormID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.ReportFormFull GetModelByCache(string ReportFormID)
        {

            string CacheKey = "ReportFormFullModel-" + ReportFormID;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ReportFormID);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.ReportFormFull)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.ReportFormFull> DataTableToList(DataTable dt)
        {
            List<Model.ReportFormFull> modelList = new List<Model.ReportFormFull>();
            try
            {
                int rowsCount = dt.Rows.Count;
                if (rowsCount > 0)
                {
                    Model.ReportFormFull model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new Model.ReportFormFull();
                        if (dt.Rows[n]["ReportFormID"] != null && dt.Rows[n]["ReportFormID"].ToString() != "")
                        {
                            model.ReportFormID = dt.Rows[n]["ReportFormID"].ToString();
                        }
                        if (dt.Rows[n]["CLIENTNO"] != null && dt.Rows[n]["CLIENTNO"].ToString() != "")
                        {
                            model.CLIENTNO = dt.Rows[n]["CLIENTNO"].ToString();
                        }
                        if (dt.Rows[n]["CNAME"] != null && dt.Rows[n]["CNAME"].ToString() != "")
                        {
                            model.CNAME = dt.Rows[n]["CNAME"].ToString();
                        }
                        if (dt.Rows[n]["AGEUNITNAME"] != null && dt.Rows[n]["AGEUNITNAME"].ToString() != "")
                        {
                            model.AGEUNITNAME = dt.Rows[n]["AGEUNITNAME"].ToString();
                        }
                        if (dt.Rows[n]["GENDERNAME"] != null && dt.Rows[n]["GENDERNAME"].ToString() != "")
                        {
                            model.GENDERNAME = dt.Rows[n]["GENDERNAME"].ToString();
                        }
                        if (dt.Rows[n]["DEPTNAME"] != null && dt.Rows[n]["DEPTNAME"].ToString() != "")
                        {
                            model.DEPTNAME = dt.Rows[n]["DEPTNAME"].ToString();
                        }
                        if (dt.Rows[n]["DOCTORNAME"] != null && dt.Rows[n]["DOCTORNAME"].ToString() != "")
                        {
                            model.DOCTORNAME = dt.Rows[n]["DOCTORNAME"].ToString();
                        }
                        if (dt.Rows[n]["DISTRICTNAME"] != null && dt.Rows[n]["DISTRICTNAME"].ToString() != "")
                        {
                            model.DISTRICTNAME = dt.Rows[n]["DISTRICTNAME"].ToString();
                        }
                        if (dt.Rows[n]["WARDNAME"] != null && dt.Rows[n]["WARDNAME"].ToString() != "")
                        {
                            model.WARDNAME = dt.Rows[n]["WARDNAME"].ToString();
                        }
                        if (dt.Rows[n]["FOLKNAME"] != null && dt.Rows[n]["FOLKNAME"].ToString() != "")
                        {
                            model.FOLKNAME = dt.Rows[n]["FOLKNAME"].ToString();
                        }
                        if (dt.Rows[n]["SICKTYPENAME"] != null && dt.Rows[n]["SICKTYPENAME"].ToString() != "")
                        {
                            model.SICKTYPENAME = dt.Rows[n]["SICKTYPENAME"].ToString();
                        }
                        if (dt.Rows[n]["SAMPLETYPENAME"] != null && dt.Rows[n]["SAMPLETYPENAME"].ToString() != "")
                        {
                            model.SAMPLETYPENAME = dt.Rows[n]["SAMPLETYPENAME"].ToString();
                        }
                        if (dt.Rows[n]["SECTIONNAME"] != null && dt.Rows[n]["SECTIONNAME"].ToString() != "")
                        {
                            model.SECTIONNAME = dt.Rows[n]["SECTIONNAME"].ToString();
                        }
                        if (dt.Rows[n]["TESTTYPENAME"] != null && dt.Rows[n]["TESTTYPENAME"].ToString() != "")
                        {
                            model.TESTTYPENAME = dt.Rows[n]["TESTTYPENAME"].ToString();
                        }
                        if (dt.Rows[n]["RECEIVEDATE"] != null && dt.Rows[n]["RECEIVEDATE"].ToString() != "")
                        {
                            model.RECEIVEDATE = DateTime.Parse(dt.Rows[n]["RECEIVEDATE"].ToString());
                        }
                        if (dt.Rows[n]["SECTIONNO"] != null && dt.Rows[n]["SECTIONNO"].ToString() != "")
                        {
                            model.SECTIONNO = dt.Rows[n]["SECTIONNO"].ToString();
                        }
                        if (dt.Rows[n]["TESTTYPENO"] != null && dt.Rows[n]["TESTTYPENO"].ToString() != "")
                        {
                            model.TESTTYPENO = dt.Rows[n]["TESTTYPENO"].ToString();
                        }
                        if (dt.Rows[n]["SAMPLENO"] != null && dt.Rows[n]["SAMPLENO"].ToString() != "")
                        {
                            model.SAMPLENO = dt.Rows[n]["SAMPLENO"].ToString();
                        }
                        if (dt.Rows[n]["STATUSNO"] != null && dt.Rows[n]["STATUSNO"].ToString() != "")
                        {
                            int a = -1;
                            int.TryParse(dt.Rows[n]["STATUSNO"].ToString(), out a);
                            model.STATUSNO = a;
                            //model.STATUSNO = int.Parse(dt.Rows[n]["STATUSNO"].ToString());
                        }
                        if (dt.Rows[n]["SAMPLETYPENO"] != null && dt.Rows[n]["SAMPLETYPENO"].ToString() != "")
                        {
                            int a = -1;
                            int.TryParse(dt.Rows[n]["SAMPLETYPENO"].ToString(), out a);
                            model.SAMPLETYPENO = a;//int.Parse(dt.Rows[n]["SAMPLETYPENO"].ToString());
                        }
                        if (dt.Rows[n]["PATNO"] != null && dt.Rows[n]["PATNO"].ToString() != "")
                        {
                            model.PATNO = dt.Rows[n]["PATNO"].ToString();
                        }
                        if (dt.Rows[n]["PERSONID"] != null && dt.Rows[n]["PERSONID"].ToString() != "")
                        {
                            model.PERSONID = dt.Rows[n]["PERSONID"].ToString();
                        }
                        if (dt.Rows[n]["GENDERNO"] != null && dt.Rows[n]["GENDERNO"].ToString() != "")
                        {
                            model.GENDERNO = int.Parse(dt.Rows[n]["GENDERNO"].ToString());
                        }
                        if (dt.Rows[n]["BIRTHDAY"] != null && dt.Rows[n]["BIRTHDAY"].ToString() != "")
                        {
                            model.BIRTHDAY = DateTime.Parse(dt.Rows[n]["BIRTHDAY"].ToString());
                        }
                        if (dt.Rows[n]["AGE"] != null && dt.Rows[n]["AGE"].ToString() != "")
                        {
                            model.AGE = dt.Rows[n]["AGE"].ToString();
                        }
                        if (dt.Rows[n]["AGEUNITNO"] != null && dt.Rows[n]["AGEUNITNO"].ToString() != "")
                        {
                            model.AGEUNITNO = int.Parse(dt.Rows[n]["AGEUNITNO"].ToString());
                        }
                        if (dt.Rows[n]["FOLKNO"] != null && dt.Rows[n]["FOLKNO"].ToString() != "")
                        {
                            model.FOLKNO = dt.Rows[n]["FOLKNO"].ToString();
                        }
                        if (dt.Rows[n]["DISTRICTNO"] != null && dt.Rows[n]["DISTRICTNO"].ToString() != "")
                        {
                            model.DISTRICTNO = dt.Rows[n]["DISTRICTNO"].ToString();
                        }
                        if (dt.Rows[n]["WARDNO"] != null && dt.Rows[n]["WARDNO"].ToString() != "")
                        {
                            model.WARDNO = dt.Rows[n]["WARDNO"].ToString();
                        }
                        if (dt.Rows[n]["BED"] != null && dt.Rows[n]["BED"].ToString() != "")
                        {
                            model.BED = dt.Rows[n]["BED"].ToString();
                        }
                        if (dt.Rows[n]["DEPTNO"] != null && dt.Rows[n]["DEPTNO"].ToString() != "")
                        {
                            model.DEPTNO = int.Parse(dt.Rows[n]["DEPTNO"].ToString());
                        }
                        if (dt.Rows[n]["DOCTOR"] != null && dt.Rows[n]["DOCTOR"].ToString() != "")
                        {
                            model.DOCTOR = dt.Rows[n]["DOCTOR"].ToString();
                        }
                        if (dt.Rows[n]["CHARGENO"] != null && dt.Rows[n]["CHARGENO"].ToString() != "")
                        {
                            model.CHARGENO = dt.Rows[n]["CHARGENO"].ToString();
                        }
                        if (dt.Rows[n]["CHARGE"] != null && dt.Rows[n]["CHARGE"].ToString() != "")
                        {
                            model.CHARGE = dt.Rows[n]["CHARGE"].ToString();
                        }
                        if (dt.Rows[n]["COLLECTER"] != null && dt.Rows[n]["COLLECTER"].ToString() != "")
                        {
                            model.COLLECTER = dt.Rows[n]["COLLECTER"].ToString();
                        }
                        if (dt.Rows[n]["COLLECTDATE"] != null && dt.Rows[n]["COLLECTDATE"].ToString() != "")
                        {
                            model.COLLECTDATE = DateTime.Parse(dt.Rows[n]["COLLECTDATE"].ToString());
                        }
                        if (dt.Rows[n]["COLLECTTIME"] != null && dt.Rows[n]["COLLECTTIME"].ToString() != "")
                        {
                            model.COLLECTTIME = DateTime.Parse(dt.Rows[n]["COLLECTTIME"].ToString());
                        }
                        if (dt.Rows[n]["FORMMEMO"] != null && dt.Rows[n]["FORMMEMO"].ToString() != "")
                        {
                            model.FORMMEMO = dt.Rows[n]["FORMMEMO"].ToString();
                        }
                        if (dt.Rows[n]["TECHNICIAN"] != null && dt.Rows[n]["TECHNICIAN"].ToString() != "")
                        {
                            model.TECHNICIAN = dt.Rows[n]["TECHNICIAN"].ToString();
                        }
                        if (dt.Rows[n]["TESTDATE"] != null && dt.Rows[n]["TESTDATE"].ToString() != "")
                        {
                            model.TESTDATE = DateTime.Parse(dt.Rows[n]["TESTDATE"].ToString());
                        }
                        if (dt.Rows[n]["TESTTIME"] != null && dt.Rows[n]["TESTTIME"].ToString() != "")
                        {
                            model.TESTTIME = DateTime.Parse(dt.Rows[n]["TESTTIME"].ToString());
                        }
                        if (dt.Rows[n]["OPERATOR"] != null && dt.Rows[n]["OPERATOR"].ToString() != "")
                        {
                            model.OPERATOR = dt.Rows[n]["OPERATOR"].ToString();
                        }
                        if (dt.Rows[n]["OPERDATE"] != null && dt.Rows[n]["OPERDATE"].ToString() != "")
                        {
                            model.OPERDATE = DateTime.Parse(dt.Rows[n]["OPERDATE"].ToString());
                        }
                        if (dt.Rows[n]["OPERTIME"] != null && dt.Rows[n]["OPERTIME"].ToString() != "")
                        {
                            model.OPERTIME = DateTime.Parse(dt.Rows[n]["OPERTIME"].ToString());
                        }
                        if (dt.Rows[n]["CHECKER"] != null && dt.Rows[n]["CHECKER"].ToString() != "")
                        {
                            model.CHECKER = dt.Rows[n]["CHECKER"].ToString();
                        }
                        if (dt.Rows[n]["PRINTTIMES"] != null && dt.Rows[n]["PRINTTIMES"].ToString() != "")
                        {
                            int a = -1;
                            int.TryParse(dt.Rows[n]["PRINTTIMES"].ToString(), out a);
                            model.PRINTTIMES = a;// int.Parse(dt.Rows[n]["PRINTTIMES"].ToString());
                        }
                        if (dt.Rows[n]["resultfile"] != null && dt.Rows[n]["resultfile"].ToString() != "")
                        {
                            model.resultfile = dt.Rows[n]["resultfile"].ToString();
                        }
                        if (dt.Rows[n]["CHECKDATE"] != null && dt.Rows[n]["CHECKDATE"].ToString() != "")
                        {
                            model.CHECKDATE = DateTime.Parse(dt.Rows[n]["CHECKDATE"].ToString());
                        }
                        if (dt.Rows[n]["CHECKTIME"] != null && dt.Rows[n]["CHECKTIME"].ToString() != "")
                        {
                            model.CHECKTIME = DateTime.Parse(dt.Rows[n]["CHECKTIME"].ToString());
                        }
                        if (dt.Rows[n]["SERIALNO"] != null && dt.Rows[n]["SERIALNO"].ToString() != "")
                        {
                            model.SERIALNO = dt.Rows[n]["SERIALNO"].ToString();
                        }
                        if (dt.Rows[n]["REQUESTSOURCE"] != null && dt.Rows[n]["REQUESTSOURCE"].ToString() != "")
                        {
                            model.REQUESTSOURCE = dt.Rows[n]["REQUESTSOURCE"].ToString();
                        }
                        if (dt.Rows[n]["DIAGNO"] != null && dt.Rows[n]["DIAGNO"].ToString() != "")
                        {
                            model.DIAGNO = dt.Rows[n]["DIAGNO"].ToString();
                        }
                        if (dt.Rows[n]["SICKTYPENO"] != null && dt.Rows[n]["SICKTYPENO"].ToString() != "")
                        {
                            model.SICKTYPENO = dt.Rows[n]["SICKTYPENO"].ToString();
                        }
                        if (dt.Rows[n]["FORMCOMMENT"] != null && dt.Rows[n]["FORMCOMMENT"].ToString() != "")
                        {
                            model.FORMCOMMENT = dt.Rows[n]["FORMCOMMENT"].ToString();
                        }
                        if (dt.Rows[n]["ARTIFICERORDER"] != null && dt.Rows[n]["ARTIFICERORDER"].ToString() != "")
                        {
                            model.ARTIFICERORDER = dt.Rows[n]["ARTIFICERORDER"].ToString();
                        }
                        if (dt.Rows[n]["SICKORDER"] != null && dt.Rows[n]["SICKORDER"].ToString() != "")
                        {
                            model.SICKORDER = dt.Rows[n]["SICKORDER"].ToString();
                        }
                        if (dt.Rows[n]["SICKTYPE"] != null && dt.Rows[n]["SICKTYPE"].ToString() != "")
                        {
                            model.SICKTYPE = dt.Rows[n]["SICKTYPE"].ToString();
                        }
                        if (dt.Rows[n]["CHARGEFLAG"] != null && dt.Rows[n]["CHARGEFLAG"].ToString() != "")
                        {
                            model.CHARGEFLAG = dt.Rows[n]["CHARGEFLAG"].ToString();
                        }
                        if (dt.Rows[n]["TESTDEST"] != null && dt.Rows[n]["TESTDEST"].ToString() != "")
                        {
                            model.TESTDEST = dt.Rows[n]["TESTDEST"].ToString();
                        }
                        if (dt.Rows[n]["SLABLE"] != null && dt.Rows[n]["SLABLE"].ToString() != "")
                        {
                            model.SLABLE = dt.Rows[n]["SLABLE"].ToString();
                        }
                        if (dt.Rows[n]["ZDY1"] != null && dt.Rows[n]["ZDY1"].ToString() != "")
                        {
                            model.ZDY1 = dt.Rows[n]["ZDY1"].ToString();
                        }
                        if (dt.Rows[n]["ZDY2"] != null && dt.Rows[n]["ZDY2"].ToString() != "")
                        {
                            model.ZDY2 = dt.Rows[n]["ZDY2"].ToString();
                        }
                        if (dt.Rows[n]["ZDY3"] != null && dt.Rows[n]["ZDY3"].ToString() != "")
                        {
                            model.ZDY3 = dt.Rows[n]["ZDY3"].ToString();
                        }
                        if (dt.Rows[n]["ZDY4"] != null && dt.Rows[n]["ZDY4"].ToString() != "")
                        {
                            model.ZDY4 = dt.Rows[n]["ZDY4"].ToString();
                        }
                        if (dt.Rows[n]["ZDY5"] != null && dt.Rows[n]["ZDY5"].ToString() != "")
                        {
                            model.ZDY5 = dt.Rows[n]["ZDY5"].ToString();
                        }
                        if (dt.Rows[n]["ZDY8"] != null && dt.Rows[n]["ZDY8"].ToString() != "")
                        {
                            model.ZDY8 = dt.Rows[n]["ZDY8"].ToString();
                        }
                        if (dt.Rows[n]["ZDY10"] != null && dt.Rows[n]["ZDY10"].ToString() != "")
                        {
                            model.ZDY10 = dt.Rows[n]["ZDY10"].ToString();
                        }
                        if (dt.Rows[n]["INCEPTDATE"] != null && dt.Rows[n]["INCEPTDATE"].ToString() != "")
                        {
                            model.INCEPTDATE = DateTime.Parse(dt.Rows[n]["INCEPTDATE"].ToString());
                        }
                        if (dt.Rows[n]["INCEPTTIME"] != null && dt.Rows[n]["INCEPTTIME"].ToString() != "")
                        {
                            model.INCEPTTIME = DateTime.Parse(dt.Rows[n]["INCEPTTIME"].ToString());
                        }
                        if (dt.Rows[n]["INCEPTER"] != null && dt.Rows[n]["INCEPTER"].ToString() != "")
                        {
                            model.INCEPTER = dt.Rows[n]["INCEPTER"].ToString();
                        }
                        if (dt.Rows[n]["ONLINEDATE"] != null && dt.Rows[n]["ONLINEDATE"].ToString() != "")
                        {
                            model.ONLINEDATE = DateTime.Parse(dt.Rows[n]["ONLINEDATE"].ToString());
                        }
                        if (dt.Rows[n]["ONLINETIME"] != null && dt.Rows[n]["ONLINETIME"].ToString() != "")
                        {
                            model.ONLINETIME = DateTime.Parse(dt.Rows[n]["ONLINETIME"].ToString());
                        }
                        if (dt.Rows[n]["BMANNO"] != null && dt.Rows[n]["BMANNO"].ToString() != "")
                        {
                            model.BMANNO = dt.Rows[n]["BMANNO"].ToString();
                        }
                        if (dt.Rows[n]["FILETYPE"] != null && dt.Rows[n]["FILETYPE"].ToString() != "")
                        {
                            model.FILETYPE = dt.Rows[n]["FILETYPE"].ToString();
                        }
                        if (dt.Rows[n]["JPGFILE"] != null && dt.Rows[n]["JPGFILE"].ToString() != "")
                        {
                            model.JPGFILE = dt.Rows[n]["JPGFILE"].ToString();
                        }
                        if (dt.Rows[n]["PDFFILE"] != null && dt.Rows[n]["PDFFILE"].ToString() != "")
                        {
                            model.PDFFILE = dt.Rows[n]["PDFFILE"].ToString();
                        }
                        if (dt.Rows[n]["FORMNO"] != null && dt.Rows[n]["FORMNO"].ToString() != "")
                        {
                            int a = -1;
                            int.TryParse(dt.Rows[n]["FORMNO"].ToString(), out a);
                            model.FORMNO = a;//int.Parse(dt.Rows[n]["FORMNO"].ToString());
                        }
                        if (dt.Rows[n]["CHILDTABLENAME"] != null && dt.Rows[n]["CHILDTABLENAME"].ToString() != "")
                        {
                            model.CHILDTABLENAME = dt.Rows[n]["CHILDTABLENAME"].ToString();
                        }
                        if (dt.Rows[n]["PRINTEXEC"] != null && dt.Rows[n]["PRINTEXEC"].ToString() != "")
                        {
                            model.PRINTEXEC = dt.Rows[n]["PRINTEXEC"].ToString();
                        }
                        if (dt.Rows[n]["LABCENTER"] != null && dt.Rows[n]["LABCENTER"].ToString() != "")
                        {
                            model.LABCENTER = dt.Rows[n]["LABCENTER"].ToString();
                        }
                        if (dt.Rows[n]["PRINTTEXEC"] != null && dt.Rows[n]["PRINTTEXEC"].ToString() != "")
                        {
                            model.PRINTTEXEC = dt.Rows[n]["PRINTTEXEC"].ToString();
                        }
                        model.SectionType = dt.Rows[n]["SECTIONTYPE"].ToString();
                        //ZhiFang.Common.Log.Log.Info(dt.Rows[n]["PRINTDATETIME"].ToString());
                        if (dt.Rows[n]["PRINTDATETIME"] != null && dt.Rows[n]["PRINTDATETIME"].ToString() != "" && dt.Rows[n]["PRINTDATETIME"] != DBNull.Value)
                        {
                            model.PRINTDATETIME = DateTime.Parse(dt.Rows[n]["PRINTDATETIME"].ToString());
                        }
                        if (dt.Rows[n]["isdown"] != null && dt.Rows[n]["isdown"].ToString() != "")
                        {
                            model.Isdown = int.Parse(dt.Rows[n]["isdown"].ToString());
                        }
                        if (dt.Rows[n]["clientcode"] != null && dt.Rows[n]["clientcode"].ToString() != "")
                        {
                            model.clientcode = dt.Rows[n]["clientcode"].ToString();
                        }
                        if (dt.Rows[n]["clientename"] != null && dt.Rows[n]["clientename"].ToString() != "")
                        {
                            model.clientename = dt.Rows[n]["clientename"].ToString();
                        }
                        if (dt.Rows[n]["noperdate"] != null && dt.Rows[n]["noperdate"].ToString() != "")
                        {
                            model.noperdate = DateTime.Parse(dt.Rows[n]["noperdate"].ToString());
                        }
                        if (dt.Columns.Contains("SenderTime2") && dt.Rows[n]["SenderTime2"] != null && dt.Rows[n]["SenderTime2"].ToString() != "")
                        {
                            model.SenderTime2 = DateTime.Parse(dt.Rows[n]["SenderTime2"].ToString());
                        }
                        modelList.Add(model);
                    }
                }
            }
            catch (Exception e)
            {

                ZhiFang.Common.Log.Log.Error(e.ToString() + e.StackTrace);
            }
            return modelList;
        }

        /// <summary>
        /// 佛山，获取报告列表 ganwh add 2015-5-12
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public List<Model.ReportFormFull> DataTableToList(DataTable dt, string city)
        {
            List<Model.ReportFormFull> modelList = new List<Model.ReportFormFull>();
            try
            {
                int rowsCount = dt.Rows.Count;
                if (rowsCount > 0)
                {
                    Model.ReportFormFull model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new Model.ReportFormFull();

                        if (dt.Rows[n]["CNAME"] != null && dt.Rows[n]["CNAME"].ToString() != "")
                        {
                            model.CNAME = dt.Rows[n]["CNAME"].ToString();
                        }

                        if (dt.Rows[n]["PERSONID"] != null && dt.Rows[n]["PERSONID"].ToString() != "")
                        {
                            model.PERSONID = dt.Rows[n]["PERSONID"].ToString();
                        }
                        modelList.Add(model);
                    }
                }
            }
            catch (Exception e)
            {

                ZhiFang.Common.Log.Log.Error(e.ToString() + e.StackTrace);
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

        public DataSet GetList(int Top, Model.ReportFormFull model, string filedOrder)
        {
            return dal.GetList(Top, model, filedOrder);
        }

        public DataSet GetColumns()
        {
            return dal.GetColumns();
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public int GetMaxId()
        {
            return dal.GetMaxId();
        }
        public DataSet GetList(Model.ReportFormFull t, string city)
        {
            return dal.GetList(t, city);
        }
        public DataSet GetList(Model.ReportFormFull t)
        {
            return dal.GetList(t);
        }
        public DataSet GetList(string where)
        {
            return dal.GetList(where);
        }
        public List<Model.ReportFormFull> GetModelList(Model.ReportFormFull t)
        {
            DataSet ds = dal.GetList(t);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion

        #region IBReportFormFull 成员


        public DataSet GetColumn(string GetColumn)
        {
            if (GetColumn.ToUpper() == "VIEW")
            {
                return dal.GetColumnByView();
            }
            else
            {
                return dal.GetColumnByTable();
            }
        }
        public DataSet GetMatchList(Model.ReportFormFull model)
        {
            return dal.GetMatchList(model);
        }
        public DataSet GetAllList(ZhiFang.Model.ReportFormFull model)
        {
            return dal.GetAllList(model);
        }
        #endregion

        /// <summary>
        /// 检验中心的码是否和实验室的进行对照
        /// </summary>
        /// <param name="dsReportFormFull"></param>
        /// <param name="DestiOrgID">实验室单位的编码</param>
        /// <param name="ReturnDescription">返回信息</param>
        /// <returns></returns>
        public bool CheckReportFormCenter(DataSet dsReportFormFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            List<string> ListNameStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {

                ZhiFang.Common.Log.Log.Info("ReportFormFull转码字段:" + str);
                switch (str)
                {
                    case "SAMPLETYPENO":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportFormFull.Tables[0].Columns.Contains("SampleTypeNo"))
                        {
                            for (int i = 0; i < dsReportFormFull.Tables[0].Rows.Count; i++)
                            {
                                if (dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                {
                                    stringList.Add(dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                                }
                                else
                                {
                                    if (dsReportFormFull.Tables[0].Rows[i]["SAMPLETYPENAME"].ToString() != null && dsReportFormFull.Tables[0].Rows[i]["SAMPLETYPENAME"].ToString() != "")
                                    {
                                        ListNameStr.Add(dsReportFormFull.Tables[0].Rows[i]["SAMPLETYPENAME"].ToString());
                                    }
                                }
                            }
                            if (stringList.Count > 0)
                            {
                                result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                                if (!result)
                                {
                                    for (int j = 0; j < stringList.Count; j++)
                                    {
                                        SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                                        SamplleTypeControl.LabCode = DestiOrgID;
                                        int count = idstc.GetTotalCount(SamplleTypeControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照\r\n", SamplleTypeControl.SampleTypeNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                DataSet ds = idls.GetLabCodeNo(DestiOrgID, ListNameStr);
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        stringList.Add(ds.Tables[0].Rows[i]["CName"].ToString());
                                    }
                                }
                                result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                                if (!result)
                                {
                                    for (int j = 0; j < stringList.Count; j++)
                                    {
                                        SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                                        SamplleTypeControl.LabCode = DestiOrgID;
                                        int count = idstc.GetTotalCount(SamplleTypeControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照\r\n", SamplleTypeControl.SampleTypeNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                        }
                        break;
                    case "GenderNo":
                        ZhiFang.Common.Log.Log.Info("成功调取判断“" + str + "”转码");
                        if (dsReportFormFull.Tables[0].Columns.Contains("GenderNo"))
                        {
                            for (int count = 0; count < dsReportFormFull.Tables[0].Rows.Count; count++)
                            {
                                if (dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                                {
                                    ListStr.Add(dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString());
                                }
                            }
                            if (ListStr.Count > 0)
                            {
                                result = idgtc.CheckIncludeCenterCode(ListStr, DestiOrgID);
                                if (!result)
                                {
                                    for (int n = 0; n < ListStr.Count; n++)
                                    {
                                        GenderType.GenderNo = Convert.ToInt32(ListStr[n].Trim());
                                        GenderType.LabCode = DestiOrgID;
                                        int count = idgtc.GetTotalCount(GenderType);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室的未对照\r\n", GenderType.GenderNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }
        public bool CheckReportFormLab(DataSet dsReportFormFull, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsReportFormFull.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsReportFormFull.Tables[0].Rows.Count; i++)
                {
                    if (dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsReportFormFull.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeLabCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeControlNo = stringList[j].Trim();
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription = String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照\r\n", SamplleTypeControl.SampleTypeControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                }
            }
            if (dsReportFormFull.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsReportFormFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportFormFull.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsReportFormFull.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsReportFormFull.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeLabCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ControlItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内ParItemNo={0}的编号和中心的未对照\r\n", TestItemControl.ControlItemNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("CheckReportFormLab实验室内项目编码为空，无法进行对照");
                }
            }
            if (dsReportFormFull.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsReportFormFull.Tables[0].Rows.Count; count++)
                {
                    if (dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsReportFormFull.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeLabCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderControlNo = ListStr[n].Trim();
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室内GenderNo={0}的编号和中心的未对照\r\n", GenderType.GenderControlNo);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                }
            }
            return true;
        }


        public DataSet GetBarCode(string DestiOrgID, string BarCodeNo)
        {
            return dal.GetBarCode(DestiOrgID, BarCodeNo);
        }

        public int GetTotalCount(ZhiFang.Model.ReportFormFull model)
        {
            return dal.GetTotalCount(model);
        }

        public DataSet GetReportFormInfo(List<string> reportFormNo)
        {
            return dal.GetReportFormInfo(reportFormNo);
        }

        #region IBReportFormFull 成员


        public int Count(string wherestr)
        {
            return dal.Count(wherestr);
        }

        #endregion

        public bool UpdatePrintTimesByReportFormID(string ReportFormID)
        {
            return dal.UpdatePrintTimesByReportFormID(ReportFormID);
        }

        public int BackUpReportFormFullByWhere(string Strwhere)
        {
            return dal.BackUpReportFormFullByWhere(Strwhere);
        }


        public bool UpdateDownLoadState(string ReportFormID)
        {
            return dal.UpdateDownLoadState(ReportFormID);
        }
    }
}

