using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ZhiFang.ReportFormQueryPrint.Common
{
  
    public  class glob
    {
    private static Hashtable myTable;
    static glob()
       {
           myTable = Hashtable.Synchronized(new Hashtable ());
       }
       public static object getValue(object myItem)
       {
           if (myTable.ContainsKey(myItem))
           {
               return myTable [myItem];
           }
           else
               return "";
       }

       public static void setValue(object myItem, object myVlaue)
       {
           if (myTable.ContainsKey(myItem))
           {
               myTable[myItem] = myVlaue;
           }
           else
           {
               myTable.Add(myItem, myVlaue);
           }
       }
    
    }
}
