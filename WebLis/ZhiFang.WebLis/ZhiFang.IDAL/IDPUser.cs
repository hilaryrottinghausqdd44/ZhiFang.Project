using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�IPUser ��ժҪ˵����
	/// </summary>
    public interface IDPUser : IDataBase<ZhiFang.Model.PUser>, IDataPage<ZhiFang.Model.PUser>
	{		
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int UserNo,string ShortCode);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		int Delete(int UserNo,string ShortCode);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Model.PUser GetModel(int UserNo,string ShortCode);

        bool Exists(int UserNo);

        int Delete(int UserNo);

        int DeleteList(string UserIDlist);

        PUser GetModel(int UserNo);
    }
}
