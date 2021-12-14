using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层IPUser 的摘要说明。
	/// </summary>
    public interface IDPUser : IDataBase<ZhiFang.Model.PUser>, IDataPage<ZhiFang.Model.PUser>
	{		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int UserNo,string ShortCode);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int UserNo,string ShortCode);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.PUser GetModel(int UserNo,string ShortCode);

        bool Exists(int UserNo);

        int Delete(int UserNo);

        int DeleteList(string UserIDlist);

        PUser GetModel(int UserNo);
    }
}
