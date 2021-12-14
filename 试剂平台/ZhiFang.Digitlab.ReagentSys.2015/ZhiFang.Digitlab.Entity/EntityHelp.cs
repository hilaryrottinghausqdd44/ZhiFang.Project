using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.Entity
{
    public class EntityHelper
    {
        public static string GetEntityNameByType(Type type)
        {
            string typeName = "";
            if (type != null)
            {
                try
                {
                    object[] array = type.GetCustomAttributes(false);
                    if (array != null && array.Length > 0)
                    {
                        foreach (object o in array)
                        {
                            if (o is ZhiFang.Digitlab.Entity.DataDescAttribute)
                            {
                                typeName = ((ZhiFang.Digitlab.Entity.DataDescAttribute)o).CName;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return typeName;
        }
    }
}
