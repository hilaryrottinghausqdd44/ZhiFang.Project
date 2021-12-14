using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace ZhiFang.Tools
{
    public class PagePaging
    {
        public static int GetCountMaxPage(DataTable dt, int pagesize)
        {
            if (pagesize > 0)
            {
                return dt.Rows.Count / pagesize;
            }
            else
            {
                return 0;
            }
        }
        public static DataTable PresentPage(DataTable dt, int i, int pagesize)
        {
            DataTable tmpdt = dt.Clone();
            for (int index = (i) * pagesize; index < (i + 1) * pagesize; index++)
            {
                if (index < dt.Rows.Count && dt.Rows[index] != null)
                {
                    tmpdt.ImportRow(dt.Rows[index]);
                }
                else
                {
                    break;
                }
            }
            return tmpdt;
        }
        public static int GetCountMaxPage(DataView dv, int pagesize)
        {
            if (pagesize > 0)
            {
                return dv.Count / pagesize;
            }
            else
            {
                return 0;
            }
        }
        public static DataTable PresentPage(DataView dv, int i, int pagesize)
        {
            DataTable tmpdt = dv.Table.Clone();
            for (int index = (i) * pagesize; index < (i + 1) * pagesize; index++)
            {
                if (index < dv.Count && dv[index] != null)
                {
                    tmpdt.ImportRow(dv[index].Row);
                }
                else
                {
                    break;
                }
            }
            return tmpdt;
        }
        public static DataTable NextPage(DataTable dt, int i, int pagesize)
        {
            DataTable tmpdt = dt.Clone();
            for (int index = (i + 1) * pagesize; index < (i + 2) * pagesize; index++)
            {
                if (index<dt.Rows.Count && dt.Rows[index] != null)
                {
                    tmpdt.ImportRow(dt.Rows[index]);
                }
                else
                {
                    break;
                }
            }
            return tmpdt;
        }
        public static DataTable PrevPage(DataTable dt, int i, int pagesize)
        {
            DataTable tmpdt = dt.Clone();
            for (int index = (i - 1) * pagesize; index < (i) * pagesize; index++)
            {
                if (dt.Rows[index] != null)
                {
                    tmpdt.ImportRow(dt.Rows[index]);
                }
                else
                {
                    break;
                }
            }
            return tmpdt;
        }
        public static DataTable HomePage(DataTable dt, int pagesize)
        {
            return NextPage(dt, -1, pagesize);
        }
        public static DataTable EndPage(DataTable dt, int pagesize)
        {
            return NextPage(dt, GetCountMaxPage(dt, pagesize)-1, pagesize);
        }
        /*
         protected void Home_Click(object sender, EventArgs e)
        {
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel != null)
            {
                DataTable dt = (DataTable)Common.DataCache.GetCache(CacheKey);
                this.pageIndex.Text = "1";
                DataList1.DataSource = Tools.PagePaging.HomePage(dt, PageSize);
                DataList1.DataBind();
            }
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel != null)
            {
                DataTable dt = (DataTable)Common.DataCache.GetCache(CacheKey);
                try
                {
                    
                    this.pageIndex.Text = (Convert.ToInt32(this.pageIndex.Text) - 1).ToString();
                    if (Convert.ToInt32(this.pageIndex.Text) <= 0)
                    {
                        this.pageIndex.Text = "1";
                    }
                
                DataList1.DataSource = Tools.PagePaging.PrevPage(dt,Convert.ToInt32(this.pageIndex.Text), PageSize);
                DataList1.DataBind();
                }
                catch
                {
                    this.pageIndex.Text = this.pageIndex.Text;
                }                
            }
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel != null)
            {
                DataTable dt = (DataTable)Common.DataCache.GetCache(CacheKey);
                try
                {
                    this.pageIndex.Text = (Convert.ToInt32(this.pageIndex.Text) + 1).ToString();
                    if (Convert.ToInt32(this.pageIndex.Text) >= Convert.ToInt32(this.pageCount.Text))
                    {
                        this.pageIndex.Text = this.pageCount.Text;
                    }
                    DataList1.DataSource = Tools.PagePaging.NextPage(dt, Convert.ToInt32(this.pageIndex.Text), PageSize);
                    DataList1.DataBind();
                }
                catch
                {
                    this.pageIndex.Text = this.pageIndex.Text;
                }
            }
        }

        protected void End_Click(object sender, EventArgs e)
        {
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel != null)
            {
                DataTable dt = (DataTable)Common.DataCache.GetCache(CacheKey);
                this.pageIndex.Text = this.pageCount.Text;
                DataList1.DataSource = Tools.PagePaging.EndPage(dt,PageSize);
                DataList1.DataBind();
            }
        }

         */
        #region by spf
        /// <summary>
        /// 显示分页
        /// </summary>
        /// <param name="PageSize">每页记录</param>
        /// <param name="Count">总记录</param>
        /// <param name="CurrentPage">当前页</param>
        /// <returns></returns>
        public static string PageTurn(int PageSize, int Count, int CurrentPage, Page P)
        {
            StringBuilder sbstr = new StringBuilder();

            if (Count == 0)
            {
                sbstr.Append("<div style='color:red;text-align:center;'>无记录！</div>");
            }
            else
            {

                sbstr.Append("<div id='page-num-style'>");
                if (PageSize == 0)
                {
                    sbstr.Append("参数不正确！");
                }
                else
                {
                    int pagecount = 0;
                    pagecount = Count / PageSize;
                    if (Count % PageSize > 0) { pagecount++; }

                    int st, et;
                    st = 1;
                    et = pagecount;
                    string url = P.Request.Url.ToString();
                    if (url.IndexOf("&page=") > 0)
                    {
                        url = url.Substring(0, url.IndexOf("&page="));
                    }

                    if (url.IndexOf("?page=") > 0)
                    {
                        url = url.Substring(0, url.IndexOf("?page="));
                    }

                    sbstr.Append("共" + Count.ToString() + "条&nbsp;&nbsp;");
                    sbstr.Append("第" + CurrentPage.ToString() + "/" + pagecount.ToString() + "页&nbsp;&nbsp;&nbsp;&nbsp;");
                    string str = "&nbsp;<a href='{0}&page={1}'>{1}</a>&nbsp;";
                    if (url.IndexOf("?") < 0)
                    {
                        str = str.Replace("&page", "?page");
                    }

                    for (int i = st; i <= et; i++)
                    {
                        if (i == CurrentPage)
                        {
                            sbstr.Append("&nbsp;<a href='" + url + "&page=" + i.ToString() + "'><span class='current'>" + i.ToString() + "</span></a>&nbsp;");
                        }
                        else
                        {
                            sbstr.Append(string.Format(str, url, i));
                        }
                    }
                }
                sbstr.Append("</div>");

            }
            return sbstr.ToString();
        }

        public static string PageRich(int PageSize, int Count, int CurrentPage, Page P)
        {
            StringBuilder sbstr = new StringBuilder();



            sbstr.Append("<div id='page-num-style'>");
            if (PageSize == 0)
            {
                sbstr.Append("参数不正确！");
            }
            else
            {
                int pagecount = 0;
                pagecount = Count / PageSize;
                if (Count % PageSize > 0) { pagecount++; }

                int st, et;
                st = 1;
                et = pagecount;


                string str = "&nbsp;<a href='index_{0}.aspx'>{0}</a>&nbsp;";


                sbstr.Append("&nbsp;<a href='index.aspx'>首页</a>&nbsp;");

                for (int i = st; i <= et; i++)
                {
                    if (i == CurrentPage)
                    {
                        sbstr.Append("&nbsp;<a href='index_" + i.ToString() + ".aspx'><span class='current'>" + i.ToString() + "</span></a>&nbsp;");
                    }
                    else
                    {
                        sbstr.Append(string.Format(str, i));
                    }
                }
            }
            sbstr.Append("</div>");

            return sbstr.ToString();
        }
        /// <summary>
        /// 申川运营平台分页
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="Count"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static string PageRich2(int PageSize, int Count, int CurrentPage, Page P)
        {
            StringBuilder sbstr = new StringBuilder();

            if (PageSize == 0)
            {
                sbstr.Append("参数不正确！");
            }
            else
            {
                int pagecount = 0;
                pagecount = Count / PageSize;
                if (Count % PageSize > 0) { pagecount++; }

                if (CurrentPage == 1)
                {
                    sbstr.Append("&nbsp;<span>首页</span>&nbsp;");
                    sbstr.Append("&nbsp;<span>上一页</span>&nbsp;");
                }
                else
                {
                    sbstr.Append("&nbsp;<a href='?page=1'>首页</a>&nbsp;");
                    int p = CurrentPage - 1;
                    sbstr.Append("&nbsp;<a href='?page=" + p.ToString() + "'>上一页</a>&nbsp;");
                }

                if (CurrentPage < pagecount)
                {
                    int n = CurrentPage + 1;
                    sbstr.Append("&nbsp;<a href='?page=" + n.ToString() + "'>下一页</a>&nbsp;");
                    sbstr.Append("&nbsp;<a href='?page=" + pagecount.ToString() + "'>末页</a>&nbsp;");
                }
                else
                {
                    sbstr.Append("&nbsp;<span>下一页</span>&nbsp;");
                    sbstr.Append("&nbsp;<span>末页</span>&nbsp;");
                }
                sbstr.Append("&nbsp;<span>共有<em>" + pagecount.ToString() + "</em>页</span>&nbsp;");
                sbstr.Append("&nbsp;<span>共有<em>" + Count.ToString() + "</em>条纪录</span>&nbsp;");


            }

            return sbstr.ToString();
        }
		/// <summary>
		/// 根据参数结构生成网页的QueryString
		/// </summary>
		/// <param name="Para">参数结构</param>
		/// <param name="Key">被忽略的主键</param>
		/// <returns>返回生成的QueryString</returns>
		public static string UrlQuery(object Para, string Key)
		{
			if(Para==null) return String.Empty;
			Type type=Para.GetType();
			FieldInfo[] info = type.GetFields(BindingFlags.Instance| BindingFlags.Public);
			StringBuilder sb = new StringBuilder();
			foreach (FieldInfo fld in info )
			{
				if (fld.Name==Key) continue;
				object tmp=fld.GetValue(Para);
				switch(fld.FieldType.ToString())
				{
					case "System.String":
						if (tmp!=null)
						{
							if (sb.Length>0) sb.Append("&");
							sb.Append(fld.Name+"="+HttpContext.Current.Server.UrlEncode(tmp.ToString()));
						}
						break;
					case "System.Int32":
					case "System.Int16":
					case "System.Int64":
					case "System.Byte":
						if (!tmp.Equals(0))
						{
							if (sb.Length>0) sb.Append("&");
							sb.Append(fld.Name+"="+tmp.ToString());
						}
						break;
					case "System.Float":
					case "System.Double":
						if (!tmp.Equals(0))
						{
							if (sb.Length>0) sb.Append("&");
							sb.Append(fld.Name+"="+tmp.ToString());
						}
						break;
				}
			}
			return sb.ToString();
		}
		/// <summary>
		/// 生成翻页的Html代码
		/// </summary>
		/// <param name="Page">当前页数</param>
		/// <param name="MaxPage">总页数</param>
		/// <param name="Scale">显示前后页数范围</param>
		/// <returns>返回生成的Html代码</returns>
		public static string PageTurn(int Page, int MaxPage, int Scale)
		{
			return PageTurn(Page, MaxPage, Scale, null);
		}

		/// <summary>
		/// 生成翻页的Html代码
		/// </summary>
		/// <param name="Page">当前页数</param>
		/// <param name="MaxPage">总页数</param>
		/// <param name="Scale">显示前后页数范围</param>
		/// <param name="para">附加QueryString的参数结构</param>
		/// <returns>返回生成的Html代码</returns>
		public static string PageTurn(int Page, int MaxPage, int Scale, object para)
		{

			if (MaxPage<=0) return "没有找到相关记录";
			if (MaxPage==1) return "<font color=#ff0000>共 1 页</font>";

			string url="";
			if (para!=null) url=UrlQuery(para,"page");
			if (url!="") url+="&";
			
			if (Page<=0) Page=1;
			else if (Page>MaxPage) Page=MaxPage;

			int prev=Page-Scale<1?1:Page-Scale;
			int next=Page+Scale>MaxPage?MaxPage:Page+Scale;

			string addr=HttpContext.Current.Request.ServerVariables["url"]+"?";
			StringBuilder sb = new StringBuilder();

			if(Page>1) sb.Append("<a href=\""+ addr + url + "page=" + (Page-1).ToString() +"\">上一页</a> ");
			for(int i=prev;i<Page;i++)
			{
				sb.Append("<a href=\""+ addr + url + "page=" + i.ToString() +"\">["+i.ToString()+"]</a> ");
			}
			sb.Append("<span style='color:red;font-weight:bold'>"+Page.ToString()+"</span> ");
			for(int i=Page+1;i<=next;i++)
			{
				sb.Append("<a href=\""+ addr + url +"page=" + i.ToString() +"\">["+i.ToString()+"]</a> ");
			}
			if(Page<MaxPage) sb.Append("<a href=\""+ addr + url +"page=" + (Page+1).ToString() +"\">下一页</a> ");


			return sb.ToString();
		}

		/// <summary>
		/// 生成翻页的Html代码,简约无参数，直接对应转发前的.htm命名
		/// </summary>
		/// <param name="pname">转向前的文件名，如nl,资讯列表第三页：nl3.htm</param>
		/// <param name="Page">当前页数</param>
		/// <param name="MaxPage">总页数</param>
		/// <param name="Scale">显示前后页数范围</param>
		/// <param name="para">附加QueryString的参数结构</param>
		/// <returns>返回生成的Html代码</returns>
		public static string PageTurn(string pname,int Page, int MaxPage, int Scale, object para)
		{

			if (MaxPage<=0) return "没有找到相关记录";
			if (MaxPage==1) return "<font color=#ff0000>共 1 页</font>";

			string url="";
			if (para!=null) url=UrlQuery(para,"page");
			if (url!="") url+="&";
			
			if (Page<=0) Page=1;
			else if (Page>MaxPage) Page=MaxPage;

			int prev=Page-Scale<1?1:Page-Scale;
			int next=Page+Scale>MaxPage?MaxPage:Page+Scale;

			string addr=pname;
			StringBuilder sb = new StringBuilder();

			if(Page>1) sb.Append("<a href=\""+ addr + (Page-1).ToString() +".htm\">上一页</a> ");
			for(int i=prev;i<Page;i++)
			{
				sb.Append("<a href=\""+ addr + i.ToString() +".htm\">["+i.ToString()+"]</a> ");
			}
			sb.Append("<span style='color:red;font-weight:bold'>"+Page.ToString()+"</span> ");
			for(int i=Page+1;i<=next;i++)
			{
				sb.Append("<a href=\""+ addr + i.ToString() +".htm\">["+i.ToString()+"]</a> ");
			}
			if(Page<MaxPage) sb.Append("<a href=\""+ addr + (Page+1).ToString() +".htm\">下一页</a> ");


			return sb.ToString();
		}

		/// <summary>
		/// 生成翻页的Html代码,带参数
		/// </summary>
		/// <param name="Page">当前页数</param>
		/// <param name="MaxPage">总页数</param>
		/// <param name="Scale">显示前后页数范围</param>
		/// <param name="para">附加QueryString的参数结构</param>
		/// <returns>返回生成的Html代码</returns>
		public static string PageTurn(string pname,int Page, int MaxPage, int Scale, object para,int flag)
		{

			if (MaxPage<=0) return "没有找到相关记录";
			if (MaxPage==1) return "<font color=#ff0000>共 1 页</font>";

			string url="?";
			if (para!=null) url+=UrlQuery(para,"page");
			if (url!="") url+="&";
			
			if (Page<=0) Page=1;
			else if (Page>MaxPage) Page=MaxPage;

			int prev=Page-Scale<1?1:Page-Scale;
			int next=Page+Scale>MaxPage?MaxPage:Page+Scale;

			StringBuilder sb = new StringBuilder();

			if(Page>1) sb.Append("<a href=\""+ pname + url + "page=" + (Page-1).ToString() +"\">上一页</a> ");
			for(int i=prev;i<Page;i++)
			{
				sb.Append("<a href=\""+ pname + url + "page=" + i.ToString() + "\">["+i.ToString()+"]</a> ");
			}
			sb.Append("<span style='color:red;font-weight:bold'>"+Page.ToString()+"</span> ");
			for(int i=Page+1;i<=next;i++)
			{
				sb.Append("<a href=\""+ pname + url + "page=" + i.ToString() + "\">["+i.ToString()+"]</a> ");
			}
			if(Page<MaxPage) sb.Append("<a href=\""+ pname + url + "page=" + (Page+1).ToString() + "\">下一页</a> ");


			return sb.ToString();
		}

		/// <summary>
		/// 生成翻页的Html代码
		/// </summary>
		/// <param name="Page">当前页数</param>
		/// <param name="PageSize">每页显示数量</param>
		/// <param name="MaxCount">总记录数</param>
		/// <param name="Scale">显示前后页数范围</param>
		/// <param name="para">附加QueryString的参数结构</param>
		/// <returns>返回生成的Html代码</returns>
		public static string PageTurn(int Page, int PageSize, int MaxCount, int Scale, object para)
		{
			int MaxPage=0;
			if (MaxCount>0)
				MaxPage=(MaxCount-1)/PageSize+1;
			return PageTurn(Page,MaxPage,Scale,para);
		}

		/// <summary>
		/// 获取几天前的日期
		/// </summary>
		/// <param name="day">指定天数</param>
		/// <returns>返回当前日期减去指定天数的日期</returns>
		public static string DateBefore(int day)
		{
			DateTime date = DateTime.Now.Date;
			return date.AddDays(1-day).ToShortDateString();
		}

		/// <summary>
		/// 返回某范围里的日期
		/// </summary>
		/// <param name="flag">标志数　1、当日　2、本周　3、本月</param>
		/// <returns></returns>
		public static string DateScale(int flag)
		{
			DateTime date=DateTime.Now.Date;
			switch(flag)
			{
				case 1: //当天
					break;
				case 2: //本周
				switch(date.DayOfWeek)
				{
					case DayOfWeek.Monday:
						return date.AddDays(-1).ToShortDateString();
					case DayOfWeek.Tuesday:
						return date.AddDays(-2).ToShortDateString();
					case DayOfWeek.Wednesday:
						return date.AddDays(-3).ToShortDateString();
					case DayOfWeek.Thursday:
						return date.AddDays(-4).ToShortDateString();
					case DayOfWeek.Friday:
						return date.AddDays(-5).ToShortDateString();
					case DayOfWeek.Saturday:
						return date.AddDays(-6).ToShortDateString();
				}
					break;
				case 3: //本月
					date=new DateTime(date.Year,date.Month,1);
					break;
			}
			return date.ToShortDateString();
		}

		public static string SplitByDate(DateTime date)
		{
			return date.Year.ToString("0000")+date.Month.ToString("00")+date.Day.ToString("00");
		}
        public static string Trim(string str, int len)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] > 255 || str[i] < 0)
                    count += 2;
                else
                    count += 1;
                if (count <= len * 2) sb.Append(str[i]);
            }
            return sb.ToString();
        }
        #endregion
    }
}
