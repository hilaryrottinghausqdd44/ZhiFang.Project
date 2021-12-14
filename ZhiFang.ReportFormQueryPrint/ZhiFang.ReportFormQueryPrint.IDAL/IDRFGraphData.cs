using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IRFGraphData ��ժҪ˵����
	/// </summary>
	public interface IDRFGraphData:IDAL.IDataBase<Model.RFGraphData>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
        bool Exists(string GraphName, int GraphNo, string FormNo);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        int Delete(string GraphName, int GraphNo, string FormNo);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo);
		#endregion  ��Ա����

        DataSet GetListByReportFormId(string ReportFormId);

		/// <summary>
		/// ����isNeedPDF�Ƿ����pointTypeΪ8�����ݣ�trueΪ�����ˣ�falseΪ����
		/// </summary>
		DataSet GetListByReportPublicationID(string ReportPublicationID);

		/// <summary>
		/// ����PointTypeΪҪ����ָ�����͵����ݣ���ʽ1,2,3
		/// </summary>
		DataSet GetListByReportPublicationIDAndPointType(string ReportPublicationID, string PointType);
		
	}
}
