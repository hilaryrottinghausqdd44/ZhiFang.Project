using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Web;
using Ionic.Zip;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public class ZipHelp
    {
        /// <summary>
        /// 将整个文件夹压缩成zip(Add a folder along with its subfolders to a Package)
        /// </summary>
        /// <param name="folderName">The folder to add</param>
        /// <param name="compressedFileName">The package to create</param>
        /// <param name="overrideExisting">Override exsisitng files</param>
        /// <returns></returns>
        public static bool PackageFolder(string folderName, string compressedFileName, bool overrideExisting)
        {
            if (folderName.EndsWith(@"\"))
                folderName = folderName.Remove(folderName.Length - 1);
            bool result = false;
            if (!Directory.Exists(folderName))
            {
                return result;
            }

            if (!overrideExisting && File.Exists(compressedFileName))
            {
                return result;
            }
            try
            {
                using (Package package = Package.Open(compressedFileName, FileMode.Create))
                {
                    var fileList = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
                    foreach (string fileName in fileList)
                    {

                        //The path in the package is all of the subfolders after folderName
                        string pathInPackage;
                        pathInPackage = Path.GetDirectoryName(fileName).Replace(folderName, string.Empty) + "/" + Path.GetFileName(fileName);

                        Uri partUriDocument = PackUriHelper.CreatePartUri(new Uri(pathInPackage, UriKind.Relative));
                        PackagePart packagePartDocument = package.CreatePart(partUriDocument, "", CompressionOption.Maximum);
                        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            fileStream.CopyTo(packagePartDocument.GetStream());
                        }
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error zipping folder " + folderName, e);
            }

            return result;
        }
        /// <summary>
        /// 将单个文件添加到zip文件中(Compress a file into a ZIP archive as the container store)
        /// </summary>
        /// <param name="fileName">The file to compress</param>
        /// <param name="compressedFileName">The archive file</param>
        /// <param name="overrideExisting">override existing file</param>
        /// <returns></returns>
        public static bool PackageFile(string fileName, string compressedFileName, bool overrideExisting)
        {
            bool result = false;

            if (!File.Exists(fileName))
            {
                return result;
            }

            if (!overrideExisting && File.Exists(compressedFileName))
            {
                return result;
            }

            try
            {
                Uri partUriDocument = PackUriHelper.CreatePartUri(new Uri(Path.GetFileName(fileName), UriKind.Relative));

                using (Package package = Package.Open(compressedFileName, FileMode.OpenOrCreate))
                {
                    if (package.PartExists(partUriDocument))
                    {
                        package.DeletePart(partUriDocument);
                    }

                    PackagePart packagePartDocument = package.CreatePart(partUriDocument, "", CompressionOption.Maximum);
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.CopyTo(packagePartDocument.GetStream());
                    }
                }
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error zipping file " + fileName, e);
            }

            return result;
        }
        /// <summary>
        /// zip文件解压(Extract a container Zip. NOTE: container must be created as Open Packaging Conventions (OPC) specification)
        /// </summary>
        /// <param name="folderName">The folder to extract the package to</param>
        /// <param name="compressedFileName">The package file</param>
        /// <param name="overrideExisting">override existing files</param>
        /// <returns></returns>
        public static bool UncompressFile(string folderName, string compressedFileName, bool overrideExisting)
        {
            bool result = false;
            try
            {
                if (!File.Exists(compressedFileName))
                {
                    return result;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                using (Package package = Package.Open(compressedFileName, FileMode.Open, FileAccess.Read))
                {
                    foreach (PackagePart packagePart in package.GetParts())
                    {
                        ExtractPart(packagePart, folderName, overrideExisting);
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error unzipping file " + compressedFileName, e);
            }

            return result;
        }

        public static void ExtractPart(PackagePart packagePart, string targetDirectory, bool overrideExisting)
        {
            string stringPart = targetDirectory + HttpUtility.UrlDecode(packagePart.Uri.ToString()).Replace('\\', '/');

            if (!Directory.Exists(Path.GetDirectoryName(stringPart)))
                Directory.CreateDirectory(Path.GetDirectoryName(stringPart));

            if (!overrideExisting && File.Exists(stringPart))
                return;
            using (FileStream fileStream = new FileStream(stringPart, FileMode.Create))
            {
                packagePart.GetStream().CopyTo(fileStream);
            }
        }
        

        public static bool CreateZipFile(string ZipFileToCreate, string DirectoryToZip, bool overrideExisting)
        {
            try
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(System.Text.Encoding.Default))
                {
                    // note: this does not recurse directories! 
                    String[] filenames = Directory.GetFiles(ZipFileToCreate);

                    // This is just a sample, provided to illustrate the DotNetZip interface.  
                    // This logic does not recurse through sub-directories.
                    // If you are zipping up a directory, you may want to see the AddDirectory() method, 
                    // which operates recursively. 
                    foreach (String filename in filenames)
                    {
                        ZhiFang.Common.Log.Log.Debug("CreateZipFile.Adding " + filename + "...");
                        Ionic.Zip.ZipEntry e = zip.AddFile(filename,"");
                        //e.Comment = "Added by Cheeso's CreateZip utility.";
                    }

                    //zip.Comment = String.Format("This zip archive was created by the CreateZip example application on machine '{0}'",                       System.Net.Dns.GetHostName());

                    zip.Save(DirectoryToZip);
                    return true;
                }

            }
            catch (System.Exception ex1)
            {
                ZhiFang.Common.Log.Log.Debug("CreateZipDic.ZipFileToCreate:" + ZipFileToCreate + ".DirectoryToZip:" + DirectoryToZip + "exception: " + ex1);
                throw new Exception("exception: " + ex1);
            }

        }
        public static bool CreateZipDic(string ZipFileToCreate, string DirectoryToZip)
        {
            try
            {
                using (ZipFile zip = new Ionic.Zip.ZipFile(System.Text.Encoding.Default))
                {
                    zip.StatusMessageTextWriter = System.Console.Out;
                    zip.AddDirectory(ZipFileToCreate); // recurses subdirectories
                    zip.Save(DirectoryToZip);
                }
                return true;
            }
            catch (System.Exception ex1)
            {
                ZhiFang.Common.Log.Log.Debug("CreateZipDic.ZipFileToCreate:" + ZipFileToCreate + ".DirectoryToZip:" + DirectoryToZip + "exception: " + ex1);
                throw  new Exception("exception: " + ex1);
            }
        }
    }
    public class DotNetZipHelp
    {
        /// <summary>
        ///  单个文件压缩
        /// </summary>
        /// <param name="FileToZip">文件物理路径</param>
        /// <param name="DstFile">解压缩路径</param>
        public static void Zip(string FileToZip, string DstFile)
        {
            try
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(System.Text.Encoding.Default))
                {
                    //String[] filenames = GetFiles(DirectoryToZip);
                    //foreach (String filename in filenames)
                    //{
                        //Console.WriteLine("Adding {0}...", filename);
                        Ionic.Zip.ZipEntry e = zip.AddFile(FileToZip);
                        e.Comment = "Added by Cheeso's CreateZip utility.";
                    //}

                    zip.Comment = String.Format("This zip archive was created by the CreateZip example application on machine '{0}'",
                       System.Net.Dns.GetHostName());

                    zip.Save(DstFile);
                }

            }
            catch (System.Exception ex1)
            {
                System.Console.Error.WriteLine("exception: " + ex1);
            }

        }

        private static string[] GetFiles(string DirectoryToZip)
        {
            string[] files = System.IO.Directory.GetFiles(DirectoryToZip, "*.*", System.IO.SearchOption.AllDirectories);
            //string[] dics=System.IO.Directory.GetDirectories(DirectoryToZip);
            //if (dic.Length > 0)
            //{
            //    foreach (var d in dic)
            //    {
            //        GetFiles(d);
            //    }
            //}
            return files;
        }

        public static void UnZip(string ZipFileDirectory, string UnFileDirectory)
        {
            var options = new ReadOptions { StatusMessageWriter = System.Console.Out, Encoding = System.Text.Encoding.Default };
            using (ZipFile zip = ZipFile.Read(ZipFileDirectory, options))
            {
                // This call to ExtractAll() assumes:
                //   - none of the entries are password-protected.
                //   - want to extract all entries to current working directory
                //   - none of the files in the zip already exist in the directory;
                //     if they do, the method will throw.
                zip.ExtractAll(UnFileDirectory);
            }
        }

        /// <summary>
        /// 得到指定的输入流的ZIP压缩流对象【原有流对象不会改变】
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static Stream ZipCompress(Stream sourceStream, string entryName = "zip")
        {
            MemoryStream compressedStream = new MemoryStream();
            if (sourceStream != null)
            {
                long sourceOldPosition = 0;
                try
                {
                    sourceOldPosition = sourceStream.Position;
                    sourceStream.Position = 0;
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddEntry(entryName, sourceStream);
                        zip.Save(compressedStream);
                        compressedStream.Position = 0;
                    }
                }
                catch
                {
                }
                finally
                {
                    try
                    {
                        sourceStream.Position = sourceOldPosition;
                    }
                    catch
                    {
                    }
                }
            }
            return compressedStream;
        }


        /// <summary>
        /// 得到指定的字节数组的ZIP解压流对象
        /// 当前方法仅适用于只有一个压缩文件的压缩包，即方法内只取压缩包中的第一个压缩文件
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static Stream ZipDecompress(byte[] data)
        {
            Stream decompressedStream = new MemoryStream();
            if (data != null)
            {
                try
                {
                    MemoryStream dataStream = new MemoryStream(data);
                    using (ZipFile zip = ZipFile.Read(dataStream))
                    {
                        if (zip.Entries.Count > 0)
                        {
                            zip.Entries.First().Extract(decompressedStream);
                            // Extract方法中会操作ms，后续使用时必须先将Stream位置归零，否则会导致后续读取不到任何数据
                            // 返回该Stream对象之前进行一次位置归零动作
                            decompressedStream.Position = 0;
                        }
                    }
                }
                catch
                {
                }
            }
            return decompressedStream;
        }

        /// <summary>
        /// 压缩ZIP文件
        /// 支持多文件和多目录，或是多文件和多目录一起压缩
        /// </summary>
        /// <param name="list">待压缩的文件或目录集合</param>
        /// <param name="strZipName">压缩后的文件名</param>
        /// <param name="IsDirStruct">是否按目录结构压缩</param>
        /// <returns>成功：true/失败：false</returns>
        public static bool CompressMulti(List<string> list, string strZipName, bool IsDirStruct)
        {
            try
            {
                using (ZipFile zip = new ZipFile(Encoding.Default))//设置编码，解决压缩文件时中文乱码
                {
                    foreach (string path in list)
                    {
                        string fileName = Path.GetFileName(path);//取目录名称
                        //如果是目录
                        if (Directory.Exists(path))
                        {
                            if (IsDirStruct)//按目录结构压缩
                            {
                                zip.AddDirectory(path, fileName);
                            }
                            else//目录下的文件都压缩到Zip的根目录
                            {
                                zip.AddDirectory(path);
                            }
                        }
                        if (File.Exists(path))//如果是文件
                        {
                            zip.AddFile(path);
                        }
                    }
                    zip.Save(strZipName);//压缩
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 解压ZIP文件
        /// </summary>
        /// <param name="strZipPath">待解压的ZIP文件</param>
        /// <param name="strUnZipPath">解压的目录</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <returns>成功：true/失败：false</returns>
        public static bool Decompression(string strZipPath, string strUnZipPath, bool overWrite)
        {
            try
            {
                ReadOptions options = new ReadOptions();
                options.Encoding = Encoding.Default;//设置编码，解决解压文件时中文乱码
                using (ZipFile zip = ZipFile.Read(strZipPath, options))
                {
                    foreach (ZipEntry entry in zip)
                    {
                        if (string.IsNullOrEmpty(strUnZipPath))
                        {
                            strUnZipPath = strZipPath.Split('.').First();
                        }
                        if (overWrite)
                        {
                            entry.Extract(strUnZipPath, ExtractExistingFileAction.OverwriteSilently);//解压文件，如果已存在就覆盖
                        }
                        else
                        {
                            entry.Extract(strUnZipPath, ExtractExistingFileAction.DoNotOverwrite);//解压文件，如果已存在不覆盖
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
