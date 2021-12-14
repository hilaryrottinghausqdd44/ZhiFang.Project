using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDDepartment : IDataBase<ZhiFang.Model.Department>, IDataPage<ZhiFang.Model.Department>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DeptNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DeptNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.Department GetModel(int DeptNo);

        DataSet GetListLike(ZhiFang.Model.Department model);  

        int DeleteList(string DeptIDlist);
        int Add(List<ZhiFang.Model.Department> modelList);

        int Update(List<ZhiFang.Model.Department> modelList);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
