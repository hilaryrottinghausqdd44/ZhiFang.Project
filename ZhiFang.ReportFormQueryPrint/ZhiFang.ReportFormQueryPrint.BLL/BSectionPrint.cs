using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;


namespace ZhiFang.ReportFormQueryPrint.BLL
{
    /// <summary>
	/// 业务逻辑类BSectionPrint
	/// </summary>
    public class BSectionPrint
    {
        private readonly IDSectionPrint dal = DalFactory<IDSectionPrint>.GetDal("SectionPrint");
        public BSectionPrint()
        { }
        #region  成员方法

        /// <summary>
        /// 获取数据表
        /// </summary>
        public List<SectionPrint> GetModelList(SectionPrint sectionprint) {
            return dal.GetModelList(sectionprint);
        }

        public DataSet GetList(string SectionNo) {
            
            return dal.GetList(SectionNo);
        }

        public DataSet GetSectionPgroupList(string SectionNo)
        {
            return dal.GetSectionPgroupList(SectionNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.SectionPrint model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.SectionPrint model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteSectionPrint(int SPID) {
            int i = 0;
            i = dal.Delete(SPID);
            return i;
        }

        public SectionPrint GetSectionPrintBySectionOne(string SectionNo) {

            return dal.GetSectionPrintStrPageNameBySectionNo(SectionNo);
        }
        #endregion  成员方法
    }
}