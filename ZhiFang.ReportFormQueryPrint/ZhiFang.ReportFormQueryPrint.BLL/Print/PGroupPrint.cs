using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Common;
namespace ZhiFang.ReportFormQueryPrint.BLL.Print
{
	/// <summary>
	/// 业务逻辑类PGroupPrint 的摘要说明。
	/// </summary>
    public class BPGroupPrint 
	{
        private readonly IDPGroupPrint dal = DalFactory<IDPGroupPrint>.GetDal("PGroupPrint");
        public BPGroupPrint()
		{}
		#region  成员方法
      
		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
            
		}
       

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Model.PGroupPrint model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(Model.PGroupPrint model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int Id)
		{
			
			return dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.PGroupPrint GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.PGroupPrint GetModelByCache(int Id)
		{
			
			string CacheKey = "PGroupPrintModel-" + Id;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.PGroupPrint)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PGroupPrint model)
        {
            return dal.GetList(model);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_No_Name(Model.PGroupPrint model)
        {
            return dal.GetList_No_Name(model);
        }
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PGroupPrint> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
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
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["SectionNo"].ToString()!="")
					{
						model.SectionNo=int.Parse(dt.Rows[n]["SectionNo"].ToString());
					}
					if(dt.Rows[n]["PrintFormatNo"].ToString()!="")
					{
						model.PrintFormatNo=int.Parse(dt.Rows[n]["PrintFormatNo"].ToString());
					}
					if(dt.Rows[n]["ClientNo"].ToString()!="")
					{
						model.ClientNo=int.Parse(dt.Rows[n]["ClientNo"].ToString());
					}
					if(dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					if(dt.Rows[n]["SpecialtyItemNo"].ToString()!="")
					{
						model.SpecialtyItemNo=int.Parse(dt.Rows[n]["SpecialtyItemNo"].ToString());
					}
					if(dt.Rows[n]["UseFlag"].ToString()!="")
					{
						model.UseFlag=int.Parse(dt.Rows[n]["UseFlag"].ToString());
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
        /// 获得数据列表
        /// </summary>
        public List<Model.PGroupPrint> GetModelList(Model.PGroupPrint model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法

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
            for (int i = 0; i < tmpitemarry.Length; i++)
            {
                tmpitemsql += " SpecialtyItemNo = " + tmpitemarry[i]+" or";
            }
            tmpitemsql="("+tmpitemsql.Substring(0, tmpitemsql.LastIndexOf("or"))+")";

            DataTable dt = dal.GetList(pgp_m).Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataRow[] dra = dt.Select(" ClientNo=" + clientno + " and " + tmpitemsql, " Sort asc,Id desc ");
                int pid = this.PrintFormatFilter(dra,tmpitemarry.Length);
                if (dra.Count() > 0 && pid > -1)
                {
                    return pid;
                }
                else
                {
                    dra = dt.Select(" ClientNo=" + clientno + " or " + tmpitemsql, " Sort asc,Id desc ");
                    pid = this.PrintFormatFilter(dra,tmpitemarry.Length);
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
        private int PrintFormatFilter(DataRow[] dra,int itemcount)
        {
            var t = from u in dra
                    where u["ItemMinNumber"] != DBNull.Value && u["ItemMaxNumber"] != DBNull.Value && Common.Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount && Common.Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount
                    orderby Convert.ToInt32(u["Sort"].ToString().Trim()) ascending, Convert.ToInt32(u["ID"].ToString().Trim()) descending
                    select u;
            if (t.Count() > 0)
            {
                return Convert.ToInt32(t.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim());
            }
            else
            {
                t = from u in dra
                    where (u["ItemMinNumber"] != DBNull.Value && Common.Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount) || (u["ItemMaxNumber"] != DBNull.Value && Common.Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount)
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
            if (clientno.HasValue)
            {
                if (dtPrintFormat.Rows.Count > 0)
                {
                    DataRow[] source = dtPrintFormat.Select(" ClientNo=" + clientno + " ", " Sort asc,Id desc ");
                    if (source.Count<DataRow>() > 0)
                    {
                        return source[0]["PrintFormatNo"].ToString().Trim();
                    }
                    DataRow[] rowArray2 = dtPrintFormat.Select(" ClientNo is null ", " Sort asc,Id desc ");
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
            string[] strArray = itemno.Split(new char[] { ',' });
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + " SpecialtyItemNo = " + strArray[i] + " or";
            }
            str = "(" + str.Substring(0, str.LastIndexOf("or")) + ")";
            if (dtPrintFormat.Rows.Count > 0)
            {
                DataRow[] dra = dtPrintFormat.Select(" " + str, " Sort asc,Id desc ");
                string str2 = this.PrintFormatFilter_Weblis(dra, strArray.Length);
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

        public string GetFormatPrint(int clientno, string itemno, DataTable dtPrintFormat)
        {
            string[] strArray = itemno.Split(new char[] { ',' });
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + " SpecialtyItemNo = " + strArray[i] + " or";
            }
            str = "(" + str.Substring(0, str.LastIndexOf("or")) + ")";
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
        private string PrintFormatFilter_Weblis(DataRow[] dra, int itemcount)
        {
            Func<DataRow, bool> predicate = null;
            IOrderedEnumerable<DataRow> source = (from u in dra
                                                  where (((u["ItemMinNumber"] != DBNull.Value) && (u["ItemMaxNumber"] != DBNull.Value)) && (Valid.ToInt(u["ItemMinNumber"].ToString().Trim()) <= itemcount)) && (Valid.ToInt(u["ItemMaxNumber"].ToString().Trim()) >= itemcount)
                                                  orderby Convert.ToInt32(u["Sort"].ToString().Trim())
                                                  select u).ThenByDescending<DataRow, int>(u => Convert.ToInt32(u["Id"].ToString().Trim()));
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
                return source.ElementAt<DataRow>(0)["PrintFormatNo"].ToString().Trim();
            }
            return "-1";
        }

    }
}

