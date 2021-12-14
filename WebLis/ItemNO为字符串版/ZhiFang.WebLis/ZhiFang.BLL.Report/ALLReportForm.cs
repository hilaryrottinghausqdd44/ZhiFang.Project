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
    public class ALLReportForm : ZhiFang.IBLL.Report.IBALLReportForm
    {
        private readonly IDALLReportForm dal = DalFactory<IDALLReportForm>.GetDalByClassName("ALLReportForm");
        #region IBALLReportForm 成员
        /// <summary>
        /// 骨髓项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetMarrowItemList(string FormNo)
        {
            return dal.GetMarrowItemList(FormNo);
        }
        /// <summary>
        /// 生化表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromInfo(string FormNo)
        {
            return dal.GetFromInfo(FormNo);
        }
        /// <summary>
        /// 生化表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public Model.ReportForm GetFromInfoModel(string FormNo)
        {
            return dal.GetFromInfoModel(FormNo);
        }
        /// <summary>
        /// 微生物表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroInfo(string FormNo)
        {
            return dal.GetFromInfo(FormNo);
        }
        /// <summary>
        /// 生化表单项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromItemList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroItemList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单微生物列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromMicroList(string FormNo,string ItemNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo, string ItemNo)
        {
            return dal.GetFromItemList(FormNo);
        }
        /// <summary>
        /// 微生物表单抗生素列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetFromAntiList(string FormNo, string ItemNo, string MicroNo)
        {
            return dal.GetFromItemList(FormNo);
        }

        public DataTable GetFromPGroupInfo(int SectionNo)
        {
            return dal.GetFromPGroupInfo(SectionNo);
        }

        public DataTable GetFromGraphList(string FormNo)
        {
            return dal.GetFromGraphList(FormNo);
        }
        public DataTable GetFromUserImage(string UserName)
        {
            IDPUser dalPUser = DalFactory<IDPUser>.GetDalByClassName("PUser");
            Model.PUser pm=new Model.PUser();
            pm.CName=UserName;
            return dalPUser.GetList(pm).Tables[0];
        }
        #endregion

    }
}
