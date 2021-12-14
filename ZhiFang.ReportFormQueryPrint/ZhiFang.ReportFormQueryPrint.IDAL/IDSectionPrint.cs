namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    using ZhiFang.ReportFormQueryPrint.Model;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public interface IDSectionPrint : IDataBase<SectionPrint>
    {
        int Add(SectionPrint model);
        int Delete(int SPID);
        int Delete(int SectionNo, int SPID);
        int DeleteList(string SPIDlist);
        bool Exists(int SectionNo, int SPID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        SectionPrint GetModel(int SPID);
        int Update(SectionPrint model);
        List<SectionPrint> GetModelList(SectionPrint sectionprint);
        DataSet GetSectionPgroupList(string SectionNo);
        Model.SectionPrint GetSectionPrintStrPageNameBySectionNo(string SectionNo);

    }
}

