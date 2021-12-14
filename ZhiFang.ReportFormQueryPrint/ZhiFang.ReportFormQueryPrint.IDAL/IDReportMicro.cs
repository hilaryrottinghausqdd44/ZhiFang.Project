using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IReportMicro ��ժҪ˵����
	/// </summary>
    public interface IDReportMicro : IDataBase<Model.ReportMicro>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int ResultNo,int ItemNo,string FormNo);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        int Delete(int ResultNo, int ItemNo, string FormNo);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo);
        /// <summary>
        /// ����FormNo����ReportForm������ReportItem�б�
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo);
        /// <summary>
        /// ����FormNo����ReportForm������ReportItem�б�
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo,string ItemNo);
        /// <summary>
        /// ����FormNo����ReportForm������ReportItem�б�
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo,string ItemNo,string MicroNo);
         /// <summary>
        /// ����FormNo����ReportForm������ReportItem�б�(xsltģ��ʹ��)
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroGroupList(string FormNo);
        DataTable GetReportMicroGroupListForSTestType(string FormNo);

        DataSet GetReportMicroFullByReportFormId(string reportformid);

        int UpdateReportMicroFull(ReportMicroFull model);
        #endregion  ��Ա����
    }
}
