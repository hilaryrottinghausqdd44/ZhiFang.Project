using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BSC_Operation
    {
       
            public BSC_Operation()
            { }
           

            /// <summary>
            /// 增加一条数据
            /// </summary>
            public bool Add(ZhiFang.ReportFormQueryPrint.Model.SC_Operation model)
            {
            SC_Operation sco = new SC_Operation();
                return sco.Add(model);
            }

            
        }
    }

