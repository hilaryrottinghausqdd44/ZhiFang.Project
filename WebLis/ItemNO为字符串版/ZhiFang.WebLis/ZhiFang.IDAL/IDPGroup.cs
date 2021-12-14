using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�IPGroup ��ժҪ˵����
	/// </summary>
    public interface IDPGroup : IDAL.IDataBase<ZhiFang.Model.PGroup>,IDataPage<ZhiFang.Model.PGroup>
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
		#endregion  ��Ա����

        bool Exists(int SectionNo);

        int Delete(int SectionNo);

        int DeleteList(string SectionIDlist);

        PGroup GetModel(int SectionNo);

        int Add(List<ZhiFang.Model.PGroup> modelList);
        int Update(List<ZhiFang.Model.PGroup> modelList);
    }
}
