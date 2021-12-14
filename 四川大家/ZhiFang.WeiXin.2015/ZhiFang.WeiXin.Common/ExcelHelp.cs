using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Aspose.Cells;

namespace ZhiFang.WeiXin.Common
{
    public class ExcelHelp
    {
        /// <summary>
        /// Excel导出PDF
        /// </summary>
        /// <param name="excelfilefulldir">excel文件物理路径</param>
        /// <param name="pdffilefulldir">pdf文件物理路径</param>
        /// <returns>bool</returns>
        public static bool ExcelToPDF(string excelfilefulldir, string pdffilefulldir)
        {
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(pdffilefulldir)))
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ProjectProgressMonitorManage.Common.ExcelHelp.ExcelToPDF.pdffilefulldir.文件夹路径不存在！");
                try
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(pdffilefulldir));
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.ProjectProgressMonitorManage.Common.ExcelHelp.ExcelToPDF.pdffilefulldir.文件夹路径:" + System.IO.Path.GetDirectoryName(pdffilefulldir) + "建立异常：" + e.ToString() + "！");
                    return false;
                }
            }
            try
            {
                Aspose.Cells.Workbook a = new Aspose.Cells.Workbook(excelfilefulldir);
                a.Save(pdffilefulldir, SaveFormat.Pdf);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ProjectProgressMonitorManage.Common.ExcelHelp.ExcelToPDF.pdffilefulldir.EXCEL文件" + excelfilefulldir + "导出PDF文件" + pdffilefulldir + "异常：" + e.ToString() + "！");
                return false;
            }
        }
    }
}