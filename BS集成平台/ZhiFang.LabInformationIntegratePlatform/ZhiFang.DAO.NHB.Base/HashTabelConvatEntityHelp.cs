using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// hashTabe转换实体类
/// jing  2018-10-11
/// </summary>
namespace ZhiFang.DAO.NHB.Base
{
    public class HashTabelConvatEntityHelp<T> where T : new()
    {
        /// <summary>
        /// 输入IList<HashTable> 返回List实体类
        /// </summary>
        /// <param name="ilist"></param>
        /// <returns></returns>
       public List<T> HashTabelConvatEntityList(IList ilist) {
            List<T> listT = new List<T>();
            bool flag = false;
            foreach (Hashtable item in ilist)
            {
                T t = new T();

                foreach (System.Reflection.PropertyInfo pi in t.GetType().GetProperties())
                {
                    if (item.ContainsKey(pi.Name))
                    {
                        foreach (DictionaryEntry de in item)
                        {
                            if (de.Key.Equals(pi.Name))
                            {
                                pi.SetValue(t, de.Value, null);
                                flag = true;
                            }
                        }
                    }
                }
                if (flag)
                {
                    listT.Add(t);
                    flag = false;
                }
            }
            return listT;
        }
    }
}
