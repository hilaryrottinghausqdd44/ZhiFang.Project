using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.Model;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    /// <summary>
    /// ҵ���߼���PGroupPrint ��ժҪ˵����
    /// </summary>
    public class PGroupPrint : ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint
    {
        Tools.ListTool LT = new Tools.ListTool();
        private readonly IDPGroupPrint dal = DalFactory<IDPGroupPrint>.GetDalByClassName("PGroupPrint");
        public PGroupPrint()
        { }
        #region  ��Ա����

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();

        }


        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.PGroupPrint model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(Model.PGroupPrint model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public int Delete(string Id)
        {

            return dal.Delete(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.PGroupPrint GetModel(string Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public Model.PGroupPrint GetModelByCache(string Id)
        {

            string CacheKey = "PGroupPrintModel-" + Id;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.PGroupPrint)objModel;
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(Model.PGroupPrint model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList_No_Name(Model.PGroupPrint model)
        {
            return dal.GetList_No_Name(model);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.PGroupPrint> DataTableToList(DataTable dt)
        {
            List<Model.PGroupPrint> modelList = new List<Model.PGroupPrint>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.PGroupPrint model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.PGroupPrint();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Rows[n]["PrintFormatNo"].ToString() != "")
                    {
                        model.PrintFormatNo = int.Parse(dt.Rows[n]["PrintFormatNo"].ToString());
                    }
                    if (dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    if (dt.Rows[n]["SpecialtyItemNo"].ToString() != "")
                    {
                        model.SpecialtyItemNo = int.Parse(dt.Rows[n]["SpecialtyItemNo"].ToString());
                    }
                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }


                    if (dt.Rows[n]["ImageFlag"].ToString() != "")
                    {
                        model.ImageFlag = int.Parse(dt.Rows[n]["ImageFlag"].ToString());
                    }
                    if (dt.Rows[n]["AntiFlag"].ToString() != "")
                    {
                        model.AntiFlag = int.Parse(dt.Rows[n]["AntiFlag"].ToString());
                    }
                    if (dt.Rows[n]["ItemMinNumber"].ToString() != "")
                    {
                        model.ItemMinNumber = int.Parse(dt.Rows[n]["ItemMinNumber"].ToString());
                    }
                    if (dt.Rows[n]["ItemMaxNumber"].ToString() != "")
                    {
                        model.ItemMaxNumber = int.Parse(dt.Rows[n]["ItemMaxNumber"].ToString());
                    }
                    if (dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }
                    if (dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.PGroupPrint> GetModelList(Model.PGroupPrint model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  ��Ա����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionno"></param>
        /// <param name="clientno"></param>
        /// <param name="itemno"></param>
        /// <returns></returns>
        public int PrintFormatNo(int sectionno, int clientno, string itemno)
        {
            Model.PGroupPrint pgp_m = new Model.PGroupPrint();
            pgp_m.SectionNo = sectionno;
            pgp_m.UseFlag = 1;
            string[] tmpitemarry = itemno.Split(',');
            string tmpitemsql = "";
            if (tmpitemarry.Length > 0)
            {

                for (int i = 0; i < tmpitemarry.Length; i++)
                {
                    tmpitemsql += " SpecialtyItemNo = " + tmpitemarry[i] + " or";
                }
                tmpitemsql = "(" + tmpitemsql.Substring(0, tmpitemsql.LastIndexOf("or")) + ")";
            }

            DataTable dt = dal.GetList(pgp_m).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dra = dt.Select(" ClientNo=" + clientno + " and " + tmpitemsql, " Sort asc,Id desc ");
                int pid = this.PrintFormatFilter(dra, tmpitemarry.Length);
                if (dra.Count() > 0 && pid > -1)
                {
                    return pid;
                }
                else
                {
                    dra = dt.Select(" ClientNo=" + clientno + " or " + tmpitemsql, " Sort asc,Id desc ");
                    pid = this.PrintFormatFilter(dra, tmpitemarry.Length);
                    if (dra.Count() > 0 && pid > -1)
                    {
                        return pid;
                    }
                    else
                    {
                        return Convert.ToInt32(dt.Rows[0]["PrintFormatNo"].ToString().Trim());
                    }
                }
            }
            else
            {
                return -1;
            }
            /*
            if (dt.Rows.Count > 0)
            {
                var values = from u in dt.Select("  ", " Sort asc,Id desc ")
                             where u["SpecialtyItemNo"].ToString().Trim() == itemno.ToString() && u["ClientNo"].ToString().Trim() == clientno.ToString()
                             orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                             select u;
                if (values.Count<DataRow>() > 0)
                {
                    return Convert.ToInt32(values.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
                }
                else
                {
                    var Svalues = from u in dt.Select("")
                                  where u["ClientNo"].ToString().Trim() == clientno.ToString()
                                  orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                                  select u;
                    if (Svalues.Count<DataRow>() > 0)
                    {
                        return Convert.ToInt32(Svalues.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
                    }
                    else
                    {
                        var SSvalues = from u in dt.Select("")
                                       where u["SpecialtyItemNo"].ToString().Trim() == itemno.ToString()
                                       orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                                       select u;
                        if (SSvalues.Count<DataRow>() > 0)
                        {
                            return Convert.ToInt32(Svalues.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
                        }
                        else
                        {
                            return Convert.ToInt32(dt.Rows[0]["PrintFormatNo"].ToString().Trim());
                        }
                    }
                }
            }*/
            //return dal.GetList(sectionno ,clientno ,itemno );
        }
        private int PrintFormatFilter(DataRow[] dra, int itemcount)
        {
            var t = from u in dra
                    where u["ItemMinNumber"] != DBNull.Value && u["ItemMaxNumber"] != DBNull.Value && ZhiFang.Common.Public.Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount && ZhiFang.Common.Public.Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount
                    orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                    select u;
            if (t.Count() > 0)
            {
                return Convert.ToInt32(t.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
            }
            else
            {
                t = from u in dra
                    where (u["ItemMinNumber"] != DBNull.Value && ZhiFang.Common.Public.Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount) || (u["ItemMaxNumber"] != DBNull.Value && ZhiFang.Common.Public.Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount)
                    orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                    select u;
                if (t.Count() > 0)
                {
                    return Convert.ToInt32(t.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
                }
                else
                {
                    return -1;
                }
            }
        }

        public string GetFormatPrint(int? clientno, DataTable dtPrintFormat)
        {
            ZhiFang.Common.Log.Log.Info("clientno:" + clientno);

            if (clientno.HasValue)
            {
                if (dtPrintFormat.Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Info("dtPrintFormat.Rows.Count:" + dtPrintFormat.Rows.Count);
                    DataRow[] source = dtPrintFormat.Select(" ClientNo=" + clientno + " ", " Sort asc,Id desc ");
                    if (source.Count<DataRow>() > 0)
                    {
                        ZhiFang.Common.Log.Log.Info("dtPrintFormat.Rows.Count:" + dtPrintFormat.Rows.Count);
                        return source[0]["PrintFormatNo"].ToString().Trim();
                    }
                    DataRow[] rowArray2 = dtPrintFormat.Select(" ClientNo is null and  SpecialtyItemNo is null  ", " Sort asc,Id desc ");
                    if (rowArray2.Count<DataRow>() > 0)
                    {
                        return rowArray2[0]["PrintFormatNo"].ToString().Trim();
                    }
                    return dtPrintFormat.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
                }
                return "";
            }
            return dtPrintFormat.Select(" ClientNo is null ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
        }

        public string GetFormatPrint(string itemno, DataTable dtPrintFormat)
        {
            try
            {
                ZhiFang.Common.Log.Log.Info(itemno);
                ZhiFang.Common.Log.Log.Info("����GetFormatPrint�ķ�������clientno��dtPrintFormat");
                string str = "";
                string[] strArray = itemno.Split(new char[] { ',' });
                if (strArray.Length > 0)
                {

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        str = str + " SpecialtyItemNo = " + strArray[i] + " or";
                    }
                    str = "(" + str.Substring(0, str.LastIndexOf("or")) + ")";
                }
                ZhiFang.Common.Log.Log.Info("str:" + str);
                if (dtPrintFormat.Rows.Count > 0)
                {
                    DataRow[] dra = dtPrintFormat.Select(" " + str, " Sort asc,Id desc ");
                    string str2 = this.PrintFormatFilter_Weblis(dra, strArray.Length);
                    ZhiFang.Common.Log.Log.Info("str2:" + str2);
                    if ((dra.Count<DataRow>() > 0) && (str2 != "-1"))
                    {
                        ZhiFang.Common.Log.Log.Info("dra.Count<DataRow>():" + dra.Count<DataRow>());
                        return str2;
                    }
                    DataRow[] source = dtPrintFormat.Select(" ClientNo is null and  SpecialtyItemNo is null  ", " Sort asc,Id desc ");
                    if (source.Count<DataRow>() > 0)
                    {
                        ZhiFang.Common.Log.Log.Info("source.Count<DataRow>():" + source.Count<DataRow>());
                        return source[0]["PrintFormatNo"].ToString().Trim();
                    }
                    //return dtPrintFormat.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
                }
                return "";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return "";
            }
        }

        public string GetFormatPrint(int clientno, string itemno, DataTable dtPrintFormat)
        {
            string[] strArray = itemno.Split(new char[] { ',' });
            string str = "";
            if (strArray.Length > 0)
            {

                for (int i = 0; i < strArray.Length; i++)
                {
                    str = str + " SpecialtyItemNo = " + strArray[i] + " or";
                }
                str = "(" + str.Substring(0, str.LastIndexOf("or")) + ")";
            }
            if (dtPrintFormat.Rows.Count > 0)
            {
                DataRow[] dra = dtPrintFormat.Select(string.Concat(new object[] { " ClientNo=", clientno, " and ", str }), " Sort asc,Id desc ");
                string str2 = this.PrintFormatFilter_Weblis(dra, strArray.Length);
                if ((dra.Count<DataRow>() > 0) && (str2 != "-1"))
                {
                    return str2;
                }
                dra = dtPrintFormat.Select(string.Concat(new object[] { " ClientNo=", clientno, " or ", str }), " Sort asc,Id desc");
                str2 = this.PrintFormatFilter_Weblis(dra, strArray.Length);
                if ((dra.Count<DataRow>() > 0) && (str2 != "-1"))
                {
                    return str2;
                }
                DataRow[] source = dtPrintFormat.Select(" ClientNo is null and  SpecialtyItemNo is null  ", " Sort asc,Id desc ");
                if (source.Count<DataRow>() > 0)
                {
                    return source[0]["PrintFormatNo"].ToString().Trim();
                }
                return dtPrintFormat.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
            }
            return "";
        }
        public string PrintFormatFilter_Weblis(DataRow[] dra, int itemcount)
        {
            Func<DataRow, bool> predicate = null;
            IOrderedEnumerable<DataRow> source = (from u in dra
                                                  where (((u["ItemMinNumber"] != DBNull.Value) && (u["ItemMaxNumber"] != DBNull.Value)) && (Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount)) && (Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount)
                                                  orderby Convert.ToInt32(u["Sort"].ToString().Trim())
                                                  select u).ThenByDescending<DataRow, int>(u => Convert.ToInt32(u["Id"].ToString().Trim()));
            //ZhiFang.Common.Log.Log.Info("source.Count()"+source.Count());
            if (source.Count<DataRow>() > 0)
            {
                return source.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim();
            }
            if (predicate == null)
            {
                predicate = u => ((u["ItemMinNumber"] != DBNull.Value) && (Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount)) || ((u["ItemMaxNumber"] != DBNull.Value) && (Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount));
            }
            source = (from u in dra.Where<DataRow>(predicate)
                      orderby Convert.ToInt32(u["Sort"].ToString().Trim())
                      select u).ThenByDescending<DataRow, int>(u => Convert.ToInt32(u["Id"].ToString().Trim()));
            if (source.Count<DataRow>() > 0)
            {
                ZhiFang.Common.Log.Log.Info(source.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
                return source.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim();
            }
            return "-1";
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.PGroupPrint model)
        {
            return dal.GetTotalCount(model);
        }
        public DataSet GetListByPage(ZhiFang.Model.PGroupPrint model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }

        #region IBBase<PGroupPrint> ��Ա


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion


        #region С��ģ������

        /// <summary>
        /// ����һ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.PGroupFormat GetModelByID(string id)
        {
            EntityList<Model.PGroupFormat> entitylist = new EntityList<Model.PGroupFormat>();
            DataSet ds = dal.GetModelByID(id);
            return DataTableToModel(ds.Tables[0]);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="l_m"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<Model.PGroupFormat> GetAllReportGroupModelSet(Model.PGroupFormat l_m, int page, int rows, string fields, string where, string sort)
        {
            EntityList<Model.PGroupFormat> entitylist = new EntityList<Model.PGroupFormat>();
            DataSet ds = dal.GetList_No_Name(l_m, page, rows);
            PGroupPrint pgp = new PGroupPrint();
            entitylist.list = this.DataTableToLists(ds.Tables[0]);
            //PGroupFormat c = new PGroupFormat();
            //entitylist.list = LT.GetListColumns<Model.PGroupFormat>(ds.Tables[0]);
            entitylist.count = dal.GetTotalCount(l_m);

            return entitylist;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.PGroupFormat> DataTableToLists(DataTable dt)
        {
            List<Model.PGroupFormat> modelList = new List<Model.PGroupFormat>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.PGroupFormat model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.PGroupFormat();
                    if (dt.Columns.Contains("Id") && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Columns.Contains("SectionNo") && dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Columns.Contains("SectionName") && dt.Rows[n]["SectionName"].ToString() != "")
                    {
                        model.SectionName = dt.Rows[n]["SectionName"].ToString();
                    }
                    if (dt.Columns.Contains("PrintFormatNo") && dt.Rows[n]["PrintFormatNo"].ToString() != "")
                    {
                        model.PrintFormatNo = int.Parse(dt.Rows[n]["PrintFormatNo"].ToString());
                    }
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Columns.Contains("ClientName") && dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Columns.Contains("Sort") && dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    if (dt.Columns.Contains("SpecialtyItemNo") && dt.Rows[n]["SpecialtyItemNo"].ToString() != "")
                    {
                        model.SpecialtyItemNo = int.Parse(dt.Rows[n]["SpecialtyItemNo"].ToString());
                    }
                    if (dt.Columns.Contains("SpecialtyItemName") && dt.Rows[n]["SpecialtyItemName"].ToString() != "")
                    {
                        model.SpecialtyItemName = dt.Rows[n]["SpecialtyItemName"].ToString();
                    }
                    if (dt.Columns.Contains("UseFlag") && dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }
                    if (dt.Columns.Contains("BatchPrint") && dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }
                    if (dt.Columns.Contains("ModelTitleType") && dt.Rows[n]["ModelTitleType"].ToString() != "")
                    {
                        model.ModelTitleType = int.Parse(dt.Rows[n]["ModelTitleType"].ToString());
                    }
                    if (dt.Columns.Contains("ImageFlag") && dt.Rows[n]["ImageFlag"].ToString() != "")
                    {
                        model.ImageFlag = int.Parse(dt.Rows[n]["ImageFlag"].ToString());
                    }
                    if (dt.Columns.Contains("AntiFlag") && dt.Rows[n]["AntiFlag"].ToString() != "")
                    {
                        model.AntiFlag = int.Parse(dt.Rows[n]["AntiFlag"].ToString());
                    }
                    if (dt.Columns.Contains("ItemMinNumber") && dt.Rows[n]["ItemMinNumber"].ToString() != "")
                    {
                        model.ItemMinNumber = int.Parse(dt.Rows[n]["ItemMinNumber"].ToString());
                    }
                    if (dt.Columns.Contains("ItemMaxNumber") && dt.Rows[n]["ItemMaxNumber"].ToString() != "")
                    {
                        model.ItemMaxNumber = int.Parse(dt.Rows[n]["ItemMaxNumber"].ToString());
                    }
                    if (dt.Columns.Contains("BatchPrint") && dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }
                    if (dt.Columns.Contains("SickTypeNo") && dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    if (dt.Columns.Contains("PrintFormatName") && dt.Rows[n]["PrintFormatName"].ToString() != "")
                    {
                        model.PrintFormatName = dt.Rows[n]["PrintFormatName"].ToString();
                    }
                    if (dt.Columns.Contains("PintFormatAddress") && dt.Rows[n]["PintFormatAddress"].ToString() != "")
                    {
                        model.PintFormatAddress = dt.Rows[n]["PintFormatAddress"].ToString();
                    }
                    if (dt.Columns.Contains("PintFormatFileName") && dt.Rows[n]["PintFormatFileName"].ToString() != "")
                    {
                        model.PintFormatFileName = dt.Rows[n]["PintFormatFileName"].ToString();
                    }
                    if (dt.Columns.Contains("ItemParaLineNum") && dt.Rows[n]["ItemParaLineNum"].ToString() != "")
                    {
                        model.ItemParaLineNum = int.Parse(dt.Rows[n]["ItemParaLineNum"].ToString());
                    }
                    if (dt.Columns.Contains("PaperSize") && dt.Rows[n]["PaperSize"].ToString() != "")
                    {
                        model.PaperSize = dt.Rows[n]["PaperSize"].ToString();
                    }
                    if (dt.Columns.Contains("PrintFormatDesc") && dt.Rows[n]["PrintFormatDesc"].ToString() != "")
                    {
                        model.PrintFormatDesc = dt.Rows[n]["PrintFormatDesc"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public Model.PGroupFormat DataTableToModel(DataTable dt)
        {
            Model.PGroupFormat model;
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                model = new Model.PGroupFormat();
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = int.Parse(dt.Rows[n]["SectionNo"].ToString());
                    }
                    if (dt.Rows[n]["SectionName"].ToString() != "")
                    {
                        model.SectionName = dt.Rows[n]["SectionName"].ToString();
                    }
                    if (dt.Rows[n]["PrintFormatNo"].ToString() != "")
                    {
                        model.PrintFormatNo = int.Parse(dt.Rows[n]["PrintFormatNo"].ToString());
                    }
                    if (dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    if (dt.Rows[n]["SpecialtyItemNo"].ToString() != "")
                    {
                        model.SpecialtyItemNo = int.Parse(dt.Rows[n]["SpecialtyItemNo"].ToString());
                    }
                    if (dt.Rows[n]["SpecialtyItemName"].ToString() != "")
                    {
                        model.SpecialtyItemName = dt.Rows[n]["SpecialtyItemName"].ToString();
                    }


                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
                    }


                    if (dt.Rows[n]["BatchPrint"].ToString() != "")
                    {
                        model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    }

                    if (dt.Rows[n]["ModelTitleType"].ToString() != "")
                    {
                        model.ModelTitleType = int.Parse(dt.Rows[n]["ModelTitleType"].ToString());
                    }
                    if (dt.Rows[n]["ImageFlag"].ToString() != "")
                    {
                        model.ImageFlag = int.Parse(dt.Rows[n]["ImageFlag"].ToString());
                    }
                    if (dt.Rows[n]["AntiFlag"].ToString() != "")
                    {
                        model.AntiFlag = int.Parse(dt.Rows[n]["AntiFlag"].ToString());
                    }
                    if (dt.Rows[n]["ItemMinNumber"].ToString() != "")
                    {
                        model.ItemMinNumber = int.Parse(dt.Rows[n]["ItemMinNumber"].ToString());
                    }
                    if (dt.Rows[n]["ItemMaxNumber"].ToString() != "")
                    {
                        model.ItemMaxNumber = int.Parse(dt.Rows[n]["ItemMaxNumber"].ToString());
                    }
                    //if (dt.Rows[n]["BatchPrint"].ToString() != "")
                    //{
                    //    model.BatchPrint = int.Parse(dt.Rows[n]["BatchPrint"].ToString());
                    //}
                    if (dt.Rows[n]["SickTypeNo"].ToString() != "")
                    {
                        model.SickTypeNo = int.Parse(dt.Rows[n]["SickTypeNo"].ToString());
                    }
                    model.PrintFormatName = dt.Rows[n]["PrintFormatName"].ToString();
                    //model.PintFormatAddress = dt.Rows[n]["PintFormatAddress"].ToString();
                    //model.PintFormatFileName = dt.Rows[n]["PintFormatFileName"].ToString();
                    //if (dt.Rows[n]["ItemParaLineNum"].ToString() != "")
                    //{
                    //    model.ItemParaLineNum = int.Parse(dt.Rows[n]["ItemParaLineNum"].ToString());
                    //}
                    //model.PaperSize = dt.Rows[n]["PaperSize"].ToString();
                    //model.PrintFormatDesc = dt.Rows[n]["PrintFormatDesc"].ToString();
                }
                return model;
            }
            else
                return null;


        }

        #endregion



    }
}

