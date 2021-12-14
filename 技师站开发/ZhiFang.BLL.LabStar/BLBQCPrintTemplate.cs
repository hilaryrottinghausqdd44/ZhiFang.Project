using System.Data;
using System.IO;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBQCPrintTemplate : BaseBLL<LBQCPrintTemplate>, IBLBQCPrintTemplate
    {
        public DataTable GetQCTempleModuleList(string name)
        {
            string path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("QCPrintTempleUrl");
            DirectoryInfo folder = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/" + path);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Path");
            foreach (FileInfo file in folder.GetFiles("*.frx"))
            {
                var nr = dataTable.NewRow();
                nr["Name"] = file.Name;
                //判断是否存在压缩文件 不存在压缩一个
                string zippath = System.AppDomain.CurrentDomain.BaseDirectory + path + file.Name;
                ZhiFang.LabStar.Common.DotNetZipHelp.Zip(zippath,
                    zippath.Replace(".frx", ".zip"));
                nr["Path"] = path + file.Name;
                if (name != null && name != "")
                {
                    if (file.Name.IndexOf(name) != -1)
                    {
                        dataTable.Rows.Add(nr);
                    }
                }
                else
                {
                    dataTable.Rows.Add(nr);
                }
            }
            return dataTable;
        }
    }
}