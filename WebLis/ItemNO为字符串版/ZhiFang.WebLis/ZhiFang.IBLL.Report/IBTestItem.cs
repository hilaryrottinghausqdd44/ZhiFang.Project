using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Report
{	/// <summary>
	/// ҵ���߼���TestItem ��ժҪ˵����
	/// </summary>
    public interface IBTestItem : IBLLBase<Model.TestItem>
	{        
		/// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        bool Exists(string ItemNo);
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        int Delete(string ItemNo);

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        Model.TestItem GetModel(string ItemNo);

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        Model.TestItem GetModelByCache(string ItemNo);
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetListLike(Model.TestItem model);
	}
}

