using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�ITestItem ��ժҪ˵����
	/// </summary>
	public interface IDStation_PrinterList: IDataBase<ZhiFang.Model.Station_PrinterList>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int id);		
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        int Delete(int id);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.Station_PrinterList GetModel(int id);		
		#endregion  ��Ա����
	}
}
