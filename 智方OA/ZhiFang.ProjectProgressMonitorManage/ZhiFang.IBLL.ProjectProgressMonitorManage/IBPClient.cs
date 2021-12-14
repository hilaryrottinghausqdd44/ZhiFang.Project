using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPClient : IBGenericManager<PClient>
    {
        EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, string sort, int page, int limit, long salesManId, bool isOwn);
        EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, int page, int limit, long salesManId, bool isOwn);
        EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, int page, int limit, long salesManId, bool isOwn, long OwnId);
        EntityList<PClient> SearchPClientByHQLAndSalesManId(string where, string sort, int page, int limit, long salesManId, bool isOwn, long OwnId);
        FileStream GetPClientExportExcel(string where, ref string fileName, string type, int page, int limit, string fields, string sort, bool isPlanish, long SalesManId, bool IsOwn);
    }
}