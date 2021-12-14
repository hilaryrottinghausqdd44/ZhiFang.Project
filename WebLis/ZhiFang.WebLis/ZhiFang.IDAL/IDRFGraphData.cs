using System;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�IRFGraphData ��ժҪ˵����
	/// </summary>
    public interface IDRFGraphData : IDAL.IDataBase<ZhiFang.Model.RFGraphData>, IDataPage<ZhiFang.Model.RFGraphData>
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

        int DeleteList(string GraphIDlist);
    }
}
