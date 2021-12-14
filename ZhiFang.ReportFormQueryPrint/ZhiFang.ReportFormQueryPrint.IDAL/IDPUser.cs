using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IPUser ��ժҪ˵����
	/// </summary>
    public interface IDPUser : IDataBase<Model.PUser>
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

        DataSet GetListByPUserIdList(string puseridlist);

        DataSet GetListByPUserIdList(string[] puseridlist);
        DataSet GetListByPUserNameList(List<string> puseridlist);

        DataSet GetOperatorChecker(string where);

        int GetIsPUser(long UserNo);

        int GetCreatePUserESignature(string SqlWhere);
    }
}
