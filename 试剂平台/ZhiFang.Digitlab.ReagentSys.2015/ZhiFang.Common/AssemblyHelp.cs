using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ZhiFang.Common.Public
{
    public class AssemblyHelp
    {
        public static string GetAssemblyVersion(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        public static object GetAssemblyCustomAttributes(Assembly assembly, Type t)
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(t, false);
            if (attributes.Length == 0)
            {
                
                return "";
            }
            else
            {
                return attributes[0];
            } 
        }
    }
}
