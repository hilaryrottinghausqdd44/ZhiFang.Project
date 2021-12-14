using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�IPrintFormat ��ժҪ˵����
	/// </summary>
	public interface IDPrintFormat:IDAL.IDataBase<ZhiFang.Model.PrintFormat>
	{
		#region  ��Ա����	
	   
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
        bool Exists(string Id);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        int Delete(string Id);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        Model.PrintFormat GetModel(string Id);
		#endregion  ��Ա����

        DataSet GetListByPage(PrintFormat model, int nowPageNum, int nowPageSize);
    }
}
