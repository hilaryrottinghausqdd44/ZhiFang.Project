using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层IPGroup 的摘要说明。
	/// </summary>
    public interface IDPGroup : IDAL.IDataBase<Model.PGroup>
	{
		#region  成员方法		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int SectionNo,int Visible);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int SectionNo,int Visible);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.PGroup GetModel(int SectionNo,int Visible);
        Model.PGroup GetModel(string ClientNo,int SectionNo, int Visible);
		#endregion  成员方法
	}
}
