using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�ICLIENTELE ��ժҪ˵����
	/// </summary>
    public interface IDCLIENTELE : IDataBase<Model.CLIENTELE>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int ClIENTNO);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		int Delete(int ClIENTNO);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.CLIENTELE GetModel(int ClIENTNO);

        DataSet GetList(string where,string fields);
		#endregion  ��Ա����
	}
}
