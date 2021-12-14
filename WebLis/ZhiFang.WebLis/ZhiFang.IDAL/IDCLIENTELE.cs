using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// �ӿڲ�ICLIENTELE ��ժҪ˵����
	/// </summary>
    public interface IDCLIENTELE : IDataBase<ZhiFang.Model.CLIENTELE>,IDataPage<ZhiFang.Model.CLIENTELE>
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
        bool Exists(long ClIENTNO);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// ����ͬ��ʱ��������
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// ����ͬ��ʱ�޸�����
        /// </summary>
        int UpdateByDataRow(DataRow dr);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
        int Delete(long ClIENTNO);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        Model.CLIENTELE GetModel(long ClIENTNO);
        DataSet GetClientNo(string CLIENTIDlist, string CName);

        DataSet GetListLike(ZhiFang.Model.CLIENTELE model);  
		#endregion  ��Ա����

        int DeleteList(string CLIENTIDlist);

        int Add(List<ZhiFang.Model.CLIENTELE> modelList);
        int Update(List<ZhiFang.Model.CLIENTELE> modelList);
    }
}
