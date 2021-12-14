using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;

namespace ZhiFang.ReagentSys.Client.Common
{
    public class ExcelToPdfReportHelp
    {
        /// <summary>
        /// Excel转换PDF文件
        /// </summary>
        /// <param name="excelFileFullDir">excel文件物理路径</param>
        /// <param name="reportSubDir">PDF分类子文件夹</param>
        /// <param name="labId"></param>
        /// <param name="pdfName"></param>
        /// <param name="pdfFullDir">存放生成PDF文件的全路径</param>
        /// <returns></returns>
        public static bool ExcelToPDF(string excelFileFullDir, string reportSubDir, long labId, string pdfName, ref string pdfFullDir)
        {
            //string publicTemplateDir, 
            string savePath = PdfReportHelp.GetSavePdfFullDir(labId, reportSubDir);
            pdfFullDir = savePath + "\\" + pdfName;
            Aspose.Cells.Workbook a = new Aspose.Cells.Workbook(excelFileFullDir);
            a.Save(pdfFullDir, SaveFormat.Pdf);
            //ZhiFang.Common.Log.Log.Debug("pdfFullDir:" + pdfFullDir);
            return true;
        }
    }
}
