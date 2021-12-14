using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Common.AppConfig;

namespace ZhiFang.BLL.Common
{
    public class GetLabList : IBLL.Common.IBGetLabList
    {
        IDAL.IDDepartment dalDept = DALFactory.DalFactory<IDAL.IDDepartment>.GetDal("B_Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDAL.IDCLIENTELE dalClient = DALFactory.DalFactory<IDAL.IDCLIENTELE>.GetDal("B_CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
        public GetLabList()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").IndexOf("Digitlab8") < 0)
            {
                dalDept = DALFactory.DalFactory<IDAL.IDDepartment>.GetDal("Department", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalClient = DALFactory.DalFactory<IDAL.IDCLIENTELE>.GetDal("CLIENTELE", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
        #region IBGetLabList 成员

        public DataSet GetLabLst_Dept(Model.Department model, out string LabFrom)
        {
            LabFrom = "(来源于Department表)";
            return dalDept.GetListLike(model);
        }

        public DataSet GetLabLst_Client(Model.CLIENTELE model, out string LabFrom)
        {
            LabFrom = "(来源于CLIENTELE表)";
            return dalClient.GetListLike(model);
        }

        public DataSet GetLabLst_RBAC(string str, out string LabFrom)
        {
            LabFrom = "(来源于权限系统)";
            return new DataSet();
        }

        #endregion


		public List<Model.CLIENTELE> DataTableToList_Client(DataTable dt)
		{
			List<ZhiFang.Model.CLIENTELE> modelList = new List<ZhiFang.Model.CLIENTELE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZhiFang.Model.CLIENTELE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZhiFang.Model.CLIENTELE();
					//if (dt.Rows[n]["CLIENTID"].ToString() != "")
					//{
					//    model.CLIENTID = int.Parse(dt.Rows[n]["CLIENTID"].ToString());
					//}
					if (dt.Rows[n]["ClIENTNO"].ToString() != "")
					{
						model.ClIENTNO = dt.Rows[n]["ClIENTNO"].ToString();
					}
					model.CNAME = dt.Rows[n]["CNAME"].ToString();
					model.ENAME = dt.Rows[n]["ENAME"].ToString();
					model.SHORTCODE = dt.Rows[n]["SHORTCODE"].ToString();
					if (dt.Rows[n]["ISUSE"].ToString() != "")
					{
						model.ISUSE = int.Parse(dt.Rows[n]["ISUSE"].ToString());
					}
					model.LINKMAN = dt.Rows[n]["LINKMAN"].ToString();
					model.PHONENUM1 = dt.Rows[n]["PHONENUM1"].ToString();
					model.ADDRESS = dt.Rows[n]["ADDRESS"].ToString();
					model.MAILNO = dt.Rows[n]["MAILNO"].ToString();
					model.EMAIL = dt.Rows[n]["EMAIL"].ToString();
					model.PRINCIPAL = dt.Rows[n]["PRINCIPAL"].ToString();
					model.PHONENUM2 = dt.Rows[n]["PHONENUM2"].ToString();
					if (dt.Rows[n]["CLIENTTYPE"].ToString() != "")
					{
						model.CLIENTTYPE = int.Parse(dt.Rows[n]["CLIENTTYPE"].ToString());
					}
					if (dt.Rows[n]["bmanno"].ToString() != "")
					{
						model.bmanno = int.Parse(dt.Rows[n]["bmanno"].ToString());
					}
					model.romark = dt.Rows[n]["romark"].ToString();
					if (dt.Rows[n]["titletype"].ToString() != "")
					{
						model.titletype = int.Parse(dt.Rows[n]["titletype"].ToString());
					}
					if (dt.Rows[n]["uploadtype"].ToString() != "")
					{
						model.uploadtype = int.Parse(dt.Rows[n]["uploadtype"].ToString());
					}
					if (dt.Rows[n]["printtype"].ToString() != "")
					{
						model.printtype = int.Parse(dt.Rows[n]["printtype"].ToString());
					}
					if (dt.Rows[n]["InputDataType"].ToString() != "")
					{
						model.InputDataType = int.Parse(dt.Rows[n]["InputDataType"].ToString());
					}
					if (dt.Rows[n]["reportpagetype"].ToString() != "")
					{
						model.reportpagetype = int.Parse(dt.Rows[n]["reportpagetype"].ToString());
					}
					model.clientarea = dt.Rows[n]["clientarea"].ToString();
					model.clientstyle = dt.Rows[n]["clientstyle"].ToString();
					model.FaxNo = dt.Rows[n]["FaxNo"].ToString();
					if (dt.Rows[n]["AutoFax"].ToString() != "")
					{
						model.AutoFax = int.Parse(dt.Rows[n]["AutoFax"].ToString());
					}
					model.ClientReportTitle = dt.Rows[n]["ClientReportTitle"].ToString();
					if (dt.Rows[n]["IsPrintItem"].ToString() != "")
					{
						model.IsPrintItem = int.Parse(dt.Rows[n]["IsPrintItem"].ToString());
					}
					model.CZDY1 = dt.Rows[n]["CZDY1"].ToString();
					model.CZDY2 = dt.Rows[n]["CZDY2"].ToString();
					model.CZDY3 = dt.Rows[n]["CZDY3"].ToString();
					model.CZDY4 = dt.Rows[n]["CZDY4"].ToString();
					model.CZDY5 = dt.Rows[n]["CZDY5"].ToString();
					model.CZDY6 = dt.Rows[n]["CZDY6"].ToString();
					model.LinkManPosition = dt.Rows[n]["LinkManPosition"].ToString();
					model.WebLisSourceOrgId = dt.Rows[n]["WebLisSourceOrgId"].ToString();
					

					modelList.Add(model);
				}
			}
			return modelList;
		}
	}
}
