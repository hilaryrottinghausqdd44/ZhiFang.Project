using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.BLL.Common
{
    public static
        class BCompression
    {
        /// <summary>
        /// 解压缩文件
        /// 所有的路径为绝对路径
        /// </summary>
        /// <param name="FilePath">压缩包路径</param>
        /// <param name="UnzipPath">解压后的路径</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <returns></returns>
        public static bool ExeAllDeComp(string FilePath,string UnzipPath,bool overWrite)
        {
            bool flag = true;
            try
            {
                ReadOptions options = new ReadOptions();
                options.Encoding = Encoding.Default;
                if (!File.Exists(FilePath))
                {
                    return false;
                }
                ZipFile zip = ZipFile.Read(FilePath, options);
                // zip.Password = "123456";//密码解压
                foreach (ZipEntry entry in zip)
                {
                    //Extract解压zip文件包的方法，参数是保存解压后文件的路径
                    if (overWrite)
                    {
                        entry.Extract(UnzipPath, ExtractExistingFileAction.OverwriteSilently);//解压文件，如果已存在就覆盖
                    }
                    else
                    {
                        entry.Extract(UnzipPath, ExtractExistingFileAction.DoNotOverwrite);//解压文件，如果已存在不覆盖
                    }
                }
            }
            catch (Exception ex) {
                flag = false;
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
            }
            return flag;
        }
    }
}
