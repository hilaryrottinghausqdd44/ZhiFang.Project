using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IPGroup ��ժҪ˵����
	/// </summary>
    public interface IDPGroup : IDAL.IDataBase<Model.PGroup>
	{
		#region  ��Ա����		
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int SectionNo,int Visible);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		int Delete(int SectionNo,int Visible);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.PGroup GetModel(int SectionNo,int Visible);
        Model.PGroup GetModel(string ClientNo,int SectionNo, int Visible);
		#endregion  ��Ա����
	}
}
