using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IPrintFormat ��ժҪ˵����
	/// </summary>
	public interface IDPrintFormat:IDAL.IDataBase<Model.PrintFormat>
	{
		#region  ��Ա����	
	   
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int Id);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		int Delete(int Id);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.PrintFormat GetModel(int Id);
		#endregion  ��Ա����
	}
}
