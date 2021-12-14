using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�ITestItem ��ժҪ˵����
	/// </summary>
    public interface IDS_RequestVItem : IDataBase<Model.S_RequestVItem>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int ItemNo);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		int Delete(int ItemNo);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.TestItem GetModel(int ItemNo);
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetListLike(Model.TestItem model);

        DataSet GetList(string strWhere, string fields);

        DataSet GetListByReportPublicationID(string ReportPublicationID);
        #endregion  ��Ա����
    }
}
