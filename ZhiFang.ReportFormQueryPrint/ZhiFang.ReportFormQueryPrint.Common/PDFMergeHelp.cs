using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public class PDFMergeHelp
    {
        public static bool Blend(string[] fileList,string[] pageType, string outMergeFile)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();
                document.SetPageSize(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                bool flag = true;
                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        if (pageType[i].Equals("A4"))
                        {
                            document.NewPage();
                            PdfImportedPage newPage0;
                            newPage0 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage0, 0, 0);
                        }
                        else {
                            if (flag)
                            {
                                document.NewPage();
                                PdfImportedPage newPage1;
                                newPage1 = writer.GetImportedPage(reader, j);
                                cb.AddTemplate(newPage1, 0, PageSize.A4.Height / 2);
                                flag = false;
                            }
                            else
                            {
                                PdfImportedPage newPage2;
                                newPage2 = writer.GetImportedPage(reader, j);
                                cb.AddTemplate(newPage2, 0, 0);
                                flag = true;
                            }
                        }
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 合成pdf文件
        /// </summary>
        /// <param name="fileList">要合并的pdf文件集合</param>
        /// <param name="outMergeFile">合成后的文件存放路径</param>
        /// <param name="isDeleteFileLIst">是否删除原文件</param>
        public static bool mergePdfFiles(string[] fileList, string outMergeFile,bool isDeleteFileLIst)
        {
            try
            {
                PdfReader reader;
                //此处将内容从文本提取至文件流中的目的是避免文件被占用,无法删除
                FileStream fs1 = new FileStream(fileList[0], FileMode.Open);
                byte[] bytes1 = new byte[(int)fs1.Length];
                fs1.Read(bytes1, 0, bytes1.Length);
                fs1.Close();
                reader = new PdfReader(bytes1);
                
                // iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(1000,800);//设置样式
                iTextSharp.text.Rectangle rec = reader.GetPageSize(1);
                
                //创建一个文档变量
                iTextSharp.text.Document document = new iTextSharp.text.Document(rec, 50, 50, 50, 50);
                //创建该文档
                PdfWriter pdfWrite = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                //打开文档
                document.Open();
                //添加内容
                PdfContentByte contentByte = pdfWrite.DirectContent;
                PdfImportedPage newPage;
                for (int i = 0; i < fileList.Length; i++)
                {
                    FileStream fs = new FileStream(fileList[i], FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    reader = new PdfReader(bytes);
                    int pageNum = reader.NumberOfPages;//获取文档页数
                    for (int j = 1; j <= pageNum; j++)
                    {
                        document.NewPage();
                        newPage = pdfWrite.GetImportedPage(reader, j);
                        contentByte.AddTemplate(newPage, 0, 0);
                    }
                    if (isDeleteFileLIst)
                    {
                        File.Delete(fileList[i]);
                    }
                
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.Common.PDFMergeHelp.mergePdfFiles:" + e.ToString());
                return false;
            }
        }
        /// <summary>
        /// 双A5合并成A4
        /// </summary>
        /// <param name="fileList">A5pdf路径列表</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>

        public static bool DobuleA5MergeA4PDFFiles(string[] fileList, string outMergeFile)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();
                document.SetPageSize(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                bool flag = true;
                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        if (flag)
                        {
                            document.NewPage();
                            PdfImportedPage newPage1;
                            newPage1 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage1, 0, PageSize.A4.Height / 2);
                            flag = false;
                        }
                        else
                        {
                            PdfImportedPage newPage2;
                            newPage2 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage2, 0, 0);
                            flag = true;
                        }
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 双32K合并成16K
        /// </summary>
        /// <param name="fileList">A5pdf路径列表</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>

        public static bool Dobule32KMerge16KPDFFiles(string[] fileList, string outMergeFile)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();

                document.SetPageSize(new Rectangle(527, 745));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                bool flag = true;
                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        if (flag)
                        {
                            document.NewPage();
                            PdfImportedPage newPage1;
                            newPage1 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage1, 0, document.PageSize.Height / 2);
                            flag = false;
                        }
                        else
                        {
                            PdfImportedPage newPage2;
                            newPage2 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage2, 0, 0);
                            flag = true;
                        }
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

       
        public static bool Merge16KPDFFiles(string[] fileList, string outMergeFile)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();

                document.SetPageSize(new Rectangle(527, 745));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;

                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        PdfImportedPage newPage1;
                        newPage1 = writer.GetImportedPage(reader, j);
                        cb.AddTemplate(newPage1, 0, 0);
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 双半张合并成单整张
        /// </summary>
        /// <param name="fileList">A5pdf路径列表</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        /// <param name="width">整张纸宽像素</param>
        /// <param name="height">整张纸高像素</param>

        public static bool DobuleRFMergePDFFiles(string[] fileList, string outMergeFile, float widthx, float heightx)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();

                document.SetPageSize(new Rectangle(widthx, heightx));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                bool flag = true;
                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        if (flag)
                        {
                            document.NewPage();
                            PdfImportedPage newPage1;
                            newPage1 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage1, 0, document.PageSize.Height / 2);
                            flag = false;
                        }
                        else
                        {
                            PdfImportedPage newPage2;
                            newPage2 = writer.GetImportedPage(reader, j);
                            cb.AddTemplate(newPage2, 0, 0);
                            flag = true;
                        }
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

        public static bool MergeA4PDFFiles(string[] fileList, string outMergeFile)
        {
            try
            {
                //outMergeFile = @"pdfout\A4test_" + Guid.NewGuid().ToString() + ".pdf";
                PdfReader reader;
                Document document = new Document();
                document.SetPageSize(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                for (int i = 0; i < fileList.Length; i++)
                {
                    reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        PdfImportedPage newPage;
                        newPage = writer.GetImportedPage(reader, j);
                        cb.AddTemplate(newPage, 0, 0);
                    }
                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DobuleA5MergeA4PDFFiles:" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 合成pdf文件到A4纸，并根据比例缩放
        /// </summary>
        /// <param name="fileList">要合并的pdf文件集合</param>
        /// <param name="outMergeFile">合成后的文件存放路径</param>
        /// <param name="isDeleteFileLIst">是否删除原文件</param>
        public static bool mergePdfFilesZoomToA4(string[] fileList, string outMergeFile, bool isDeleteFileLIst)
        {
            try
            {
                PdfReader reader;
                
                //创建一个文档变量
                Document document = new Document();
                document.SetPageSize(PageSize.A4);
                
                //创建该文档
                PdfWriter pdfWrite = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
                //打开文档
                document.Open();
                //添加内容
                PdfContentByte contentByte = pdfWrite.DirectContent;
                PdfImportedPage newPage;
                for (int i = 0; i < fileList.Length; i++)
                {
                    //此处将内容从文本提取至文件流中的目的是避免文件被占用,无法删除
                    FileStream fs = new FileStream(fileList[i], FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    reader = new PdfReader(bytes);
                    int pageNum = reader.NumberOfPages;//获取文档页数
                    for (int j = 1; j <= pageNum; j++)
                    {
                        document.NewPage();
                        newPage = pdfWrite.GetImportedPage(reader, j);
                        Rectangle rec = reader.GetPageSize(pageNum);
                        //缩放比例
                        float widthScale = PageSize.A4.Width / rec.Width;
                        float heightScale = PageSize.A4.Height / rec.Height;
                        contentByte.AddTemplate(newPage, widthScale, 0, 0, heightScale, 0, 0);
                    }
                    if (isDeleteFileLIst)
                    {
                        File.Delete(fileList[i]);
                    }

                }
                document.Close();
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.Common.PDFMergeHelp.mergePdfFilesZoomToA4:" + e.ToString());
                return false;
            }
        }

    }
}
