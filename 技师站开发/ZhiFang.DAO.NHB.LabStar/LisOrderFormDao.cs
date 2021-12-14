using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.DAO.NHB.LabStar
{
    public class LisOrderFormDao : BaseDaoNHB<LisOrderForm, long>, IDLisOrderFormDao
    {
        public List<LisOrderFormVo> GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere)
        {
            string sql = "select lisorderform.OrderFormID,lisorderform.OrderFormNo,lispatient.CName,lisorderform.OrderTime," +
                             "lispatient.DeptName,lispatient.Bed,lisorderform.IsAffirm,lispatient.PatNo,lisorderform.ParItemCName as ItemName from Lis_OrderForm lisorderform " +
                            "left join Lis_Patient lispatient on lisorderform.PatID = lispatient.PatID";
            string hqlwhere = "lispatient.PatNo='" + patno + "' and lispatient.SickTypeID = '" + sickTypeNo + "'";
            if (!string.IsNullOrEmpty(hisDeptNo))
            {
                hqlwhere += " and lisorderform.HisDeptNo = '" + hisDeptNo + "'";
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                hqlwhere += " and " + strWhere;
            }
            sql += " where " + hqlwhere;
            var lisOrderFormVos = this.Session.CreateSQLQuery(sql).List();
            List<LisOrderFormVo> lisOrderForms = new List<LisOrderFormVo>();
            foreach (var item in lisOrderFormVos)
            {
                Array dataarr = (Array)item;
                LisOrderFormVo lisOrderFormVo = new LisOrderFormVo();
                for (int i = 0; i < dataarr.Length; i++)
                {
                    var aa = dataarr.GetValue(i);
                    #region 判断赋值
                    switch (i)
                    {
                        case 0:
                            lisOrderFormVo.OrderFormID = long.Parse(aa.ToString());
                            break;
                        case 1:
                            if (aa != null) {
                                lisOrderFormVo.OrderFormNo = aa.ToString();
                            }
                            break;
                        case 2:
                            if (aa != null)
                            {
                                lisOrderFormVo.CName = aa.ToString();
                            }
                            break;
                        case 3:
                            if (aa != null)
                            {
                                lisOrderFormVo.OrderTime = DateTime.Parse(aa.ToString());
                            }
                            break;
                        case 4:
                            if (aa != null)
                            {
                                lisOrderFormVo.DeptName = aa.ToString();
                            }
                            break;
                        case 5:
                            if (aa != null)
                            {
                                lisOrderFormVo.Bed = aa.ToString();
                            }
                            break;
                        case 6:
                            if (aa != null)
                            {
                                lisOrderFormVo.IsAffirm = (int)aa;
                            }
                            break;
                        case 7:
                            if (aa != null)
                            {
                                lisOrderFormVo.PatNo = aa.ToString();
                            }
                            break;
                        case 8:
                            if (aa != null)
                            {
                                lisOrderFormVo.ItemName = aa.ToString();
                            }
                            break;
                    }
                    #endregion
                }
                lisOrderForms.Add(lisOrderFormVo);
            }           
            if (lisOrderForms.Count > 0)
            {
                return lisOrderForms;
            }
            else
            {
                return null;
            }
        }

        public System.Collections.IList GetOrderFormList(string fields,string where,out string sqlwhere) {
            
            string sql = "select distinct " + fields + " from Lis_OrderForm lisorderform " +
                         "left join Lis_Patient lispatient on lisorderform.PatID = lispatient.PatID";
            string hqlwhere = " 1=1 ";
            if (!string.IsNullOrEmpty(where))
            {
                hqlwhere += " and " + where;
            }
            sql += " where " + hqlwhere;
            sqlwhere = hqlwhere;
            var lisOrderFormVos = this.Session.CreateSQLQuery(sql).List();
            if (lisOrderFormVos.Count > 0)
            {
                return lisOrderFormVos;
            }
            else
            {
                return null;
            }
        }
    }
}