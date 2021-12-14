using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ZhiFang.BLL.Common
{
    public class ConvertType
    {
        /// <summary>
        /// 类型转换公共方法
        /// </summary>
        /// <param name="sourceInfo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ParseEntityToType(object sourceInfo, Type type)
        {

            //源实体类型
            Type sourceType = sourceInfo.GetType();

            PropertyInfo[] sourceFileds = sourceType.GetProperties();
            object result = Activator.CreateInstance(type);

            foreach (PropertyInfo sourceFiled in sourceFileds)
            {
                PropertyInfo info = result.GetType().GetProperty(sourceFiled.Name);
                
                if (info != null)
                {
                    info.SetValue(result, sourceFiled.GetValue(sourceInfo, null), null);
                }             
             
            }
            return result;
        }

   
    }
}
