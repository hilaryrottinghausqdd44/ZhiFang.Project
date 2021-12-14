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
	/// 业务逻辑类PUser 的摘要说明。
	/// </summary>
    public class BPUser 
	{
        private readonly string Strcoust = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";

        private readonly IDPUser dal = DalFactory<IDPUser>.GetDal("PUser");
        public BPUser()
		{}

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
		public bool Exists(int UserNo,string ShortCode)
		{
			return dal.Exists(UserNo,ShortCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.PUser model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.PUser model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int UserNo,string ShortCode)
		{			
			return dal.Delete(UserNo,ShortCode);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.PUser GetModel(int UserNo,string ShortCode)
		{
			
			return dal.GetModel(UserNo,ShortCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Model.PUser GetModelByCache(int UserNo,string ShortCode)
		{
			
			string CacheKey = "PUserModel-" + UserNo+ShortCode;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UserNo,ShortCode);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache").Value;
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.PUser)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.PUser> DataTableToList(DataTable dt)
		{
			List<Model.PUser> modelList = new List<Model.PUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.PUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Model.PUser();
					if(dt.Columns.Contains("UserNo") &&dt.Rows[n]["UserNo"].ToString()!="")
					{
						model.UserNo=int.Parse(dt.Rows[n]["UserNo"].ToString());
					}
					model.CName=dt.Rows[n]["CName"].ToString();
					model.Password=dt.Rows[n]["Password"].ToString();
					model.ShortCode=dt.Rows[n]["ShortCode"].ToString();
					if(dt.Columns.Contains("Gender") && dt.Rows[n]["Gender"].ToString()!="")
					{
						model.Gender=int.Parse(dt.Rows[n]["Gender"].ToString());
					}
					if(dt.Columns.Contains("Birthday")&&dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					model.Role=dt.Rows[n]["Role"].ToString();
					model.Resume=dt.Rows[n]["Resume"].ToString();
					if(dt.Columns.Contains("Birthday")&&dt.Rows[n]["Visible"].ToString()!="")
					{
						model.Visible=int.Parse(dt.Rows[n]["Visible"].ToString());
					}
					if(dt.Columns.Contains("Birthday")&&dt.Rows[n]["DispOrder"].ToString()!="")
					{
						model.DispOrder=int.Parse(dt.Rows[n]["DispOrder"].ToString());
					}
                    if (dt.Columns.Contains("HisOrderCode") && dt.Rows[n]["HisOrderCode"].ToString() != "")
                    {
                        model.HisOrderCode = dt.Rows[n]["HisOrderCode"].ToString();
                    }                   
					if(dt.Columns.Contains("userimage") &&dt.Rows[n]["userimage"].ToString()!="")
					{
						model.userimage=(byte[])dt.Rows[n]["userimage"];
					}
					model.usertype=dt.Rows[n]["usertype"].ToString();
					if(dt.Columns.Contains("DeptNo") && dt.Rows[n]["DeptNo"].ToString()!="")
					{
						model.DeptNo=int.Parse(dt.Rows[n]["DeptNo"].ToString());
					}
					if(dt.Columns.Contains("SectorTypeNo") && dt.Rows[n]["SectorTypeNo"].ToString()!="")
					{
						model.SectorTypeNo=int.Parse(dt.Rows[n]["SectorTypeNo"].ToString());
					}
					model.UserImeName=dt.Rows[n]["UserImeName"].ToString();
					if(dt.Columns.Contains("IsManager") && dt.Rows[n]["IsManager"].ToString()!="")
					{
						model.IsManager=int.Parse(dt.Rows[n]["IsManager"].ToString());
					}
					
					if(dt.Columns.Contains("tstamp") && dt.Rows[n]["tstamp"].ToString()!="")
					{
						model.tstamp=DateTime.Parse(dt.Rows[n]["tstamp"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PUser model)
        {
            return dal.GetList(model);
        }
        public List<Model.PUser> GetModelList(Model.PUser model)
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



        public DataSet GetListByPUserIdList(string puseridlist)
        {
            return dal.GetListByPUserIdList(puseridlist);
        }

        public DataSet GetListByPUserIdList(string[] puseridlist)
        {
            return dal.GetListByPUserIdList(puseridlist);
        }

        public DataSet GetListByPUserNameList(List<string> puseridlist)
        {
            return dal.GetListByPUserNameList(puseridlist);
        }

        public void Get64Str(int il, string pwd, ref int iLength, ref string Result)
        {
            char achar;
            int iCount64, iChar, imod64, i;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                achar = pwd[iLength - il + i];
                iChar = (int)achar;
                iCount64 = iCount64 * 256 + iChar;
            }
            for (i = 0; i < 4; i++)
            {
                if (iCount64 == 0)
                {
                    Result = '=' + Result;
                }
                else
                {
                    imod64 = (iCount64 % 64) + 1;
                    iCount64 = iCount64 / 64;
                    achar = Strcoust[imod64 - 1];
                    Result = achar + Result;
                }
            }
        }

        public void Get256Str(int il, ref string pwd, ref int iLength, ref string Result)
        {
            int i, ichar, iCount64, imod256;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                ichar = Strcoust.IndexOf(pwd[iLength + i - il]);
                iCount64 = iCount64 * 64 + ichar;
            }
            for (i = 0; i < 3; i++)
            {
                if (iCount64 != 0)
                {
                    imod256 = iCount64 % 256;
                    Result = ((char)imod256) + Result;
                    iCount64 = iCount64 / 256;
                }
            }
        }
        //加密
        public string CovertPassWord(string astr)
        {
            int iLength, imod3, idv3;
            iLength = astr.Length;
            string Result = "";
            if (iLength == 0)
            {
                return "=";
            }
            idv3 = iLength / 3;
            imod3 = iLength % 3;
            while (idv3 > 0)
            {
                Get64Str(3, astr, ref iLength, ref Result);
                iLength = iLength - 3;
                idv3--;
            }
            switch (imod3)
            {
                case 1:
                    Get64Str(1, astr, ref iLength, ref Result);
                    break;
                case 2:
                    Get64Str(2, astr, ref iLength, ref Result);
                    break;
            }
            return Result;
        }
        //解密
        public string UnCovertPassWord(string pwd)
        {
            int iLength, imod4, idv4;
            iLength = pwd.Length;
            string Result = "";
            if (iLength == 0)
            {
                return "";
            }

            idv4 = iLength / 4;
            imod4 = iLength % 4;
            while (idv4 > 0)
            {
                Get256Str(4, ref pwd, ref iLength, ref Result);
                iLength = iLength - 4;
                idv4--;
            }
            switch (imod4)
            {
                case 1:
                    Get256Str(1, ref pwd, ref iLength, ref Result);
                    break;
                case 2:
                    Get256Str(2, ref pwd, ref iLength, ref Result);
                    break;
                case 3:
                    Get256Str(3, ref pwd, ref iLength, ref Result);
                    break;
            }

            for (var i = 0; i < Result.Length; i++)
            {
                if ((int)Result[i] < 32 || (int)Result[i] > 127)
                {
                    Result = pwd;
                }
            }
            return Result;
        }
        //查询所有用户
        public DataSet GetOperatorChecker(string Where) {

            return dal.GetOperatorChecker(Where);
        }

        //查询要添加的用户是否存在
        public int GetIsPUser(long UserNo)
        {
            int isok = dal.GetIsPUser(UserNo);
            return isok;
        }

        public int GetCreatePUserESignature(string SqlWhere) {

            int aa =  dal.GetCreatePUserESignature(SqlWhere);
            return aa;
        }
    }
}

