using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.BLL.Base;
using System.IO;
using ZhiFang.WeiXin.Common;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.IBLL;
using System.Reflection;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSDoctorBonus : BaseBLL<OSDoctorBonus>, ZhiFang.WeiXin.IBLL.IBOSDoctorBonus
    {
        IBBParameter IBBParameter { get; set; }

        #region Excel导出
        /// <summary>
        /// 咨询费打款明细报表Excel导出
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public FileStream GetExportExcelOSDoctorBonusDetail(string where, ref string fileName)
        {
            FileStream fileStream = null;
            IList<OSDoctorBonus> atemCount = new List<OSDoctorBonus>();
            EntityList<OSDoctorBonus> tempEntityList = new EntityList<OSDoctorBonus>();

            tempEntityList = DBDao.GetListByHQL(where, " DataAddTime ", 0, 0);
            if (tempEntityList != null)
            {
                atemCount = tempEntityList.list;
            }
            if (atemCount != null && atemCount.Count > 0)
            {
                DataTable dtSource = null;
                dtSource = this.ExportExcelOSDoctorBonusToDataTable<OSDoctorBonus>(atemCount);
                string strHeaderText = "咨询费打款明细报表信息";
                fileName = "咨询费打款明细报表信息.xlsx";

                string filePath = "", basePath = "";
                //一级保存路径
                basePath = (string)IBBParameter.GetCache(BParameterParaNoClass.ExcelExportSavePath.Key.ToString());
                if (String.IsNullOrEmpty(basePath))
                {
                    basePath = "ExcelExport";
                }
                //ATEmpSignInfoDetail为二级保存路径,作分类用
                basePath = basePath + "\\" + "ExportExcelOSDoctorBonus\\";
                filePath = basePath + DateTime.Now.ToString("yyMMddhhmmss") + fileName;
                try
                {
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    //单元格字体颜色的处理
                    Dictionary<string, short> cellFontStyleList = new Dictionary<string, short>();
                    //cellFontStyleList.Add("", NPOI.HSSF.Util.HSSFColor.Red.Index);

                    fileStream = ExportDTtoExcelHelp.ExportDTtoExcellHelp(dtSource, strHeaderText, filePath, cellFontStyleList);
                    if (fileStream != null)
                    {
                        fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    }
                }
                catch (Exception ee)
                {
                    ZhiFang.Common.Log.Log.Error("导出咨询费打款明细报表信息失败:" + ee.Message);
                    throw ee;
                }
            }
            return fileStream;
        }

        private DataTable ExportExcelOSDoctorBonusToDataTable<T>(IList<T> list)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> removeList = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                Type t = ExportDTtoExcelHelp.GetCoreType(prop.PropertyType);
                string columnName = prop.Name;
                #region DataTable的列转换为导出的中文显示名称
                switch (prop.Name)
                {
                    case "DoctorName":
                        columnName = "医生姓名";
                        break;
                    case "IDNumber":
                        columnName = "身份证号";
                        break;
                    case "BankAddress":
                        columnName = "开户行详细信息";
                        break;
                    case "BankAccount":
                        columnName = "卡号";
                        break;
                    case "Amount":
                        columnName = "结算金额（元）";
                        break;
                    case "MobileCode":
                        columnName = "联系电话";
                        break;
                    case "Memo":
                        columnName = "备注";
                        break;
                    default:
                        //columnName = "";
                        removeList.Add(columnName);
                        break;
                }
                #endregion
                if (!String.IsNullOrEmpty(columnName))
                    tb.Columns.Add(columnName, t);
            }
            foreach (T item in list)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            foreach (var columnName in removeList)
            {
                if (tb.Columns.Contains(columnName))
                {
                    tb.Columns.Remove(columnName);
                }
            }
            if (tb != null)
            {
                tb.Columns["医生姓名"].SetOrdinal(0);
                tb.Columns["身份证号"].SetOrdinal(1);

                tb.Columns["开户行详细信息"].SetOrdinal(2);
                tb.Columns["卡号"].SetOrdinal(3);
                tb.Columns["结算金额（元）"].SetOrdinal(4);
                tb.Columns["联系电话"].SetOrdinal(5);

                tb.Columns["备注"].SetOrdinal(6);
                //排序
                tb.DefaultView.Sort = "医生姓名 asc";
                tb = tb.DefaultView.ToTable();

            }
            return tb;
        }
        #endregion

        public void GetDoctorBonusInfo(OSDoctorChargeInfoVO doctorChargeVO, long DoctorAccountID)
        {
            IList<OSDoctorBonusVO> OSDoctorBonusList = new List<OSDoctorBonusVO>();
            IList<OSDoctorBonus> list = this.SearchListByHQL(" osdoctorbonus.DoctorAccountID=" + DoctorAccountID.ToString());
            if (list != null && list.Count > 0)
            {
                foreach (OSDoctorBonus db in list)
                {
                    OSDoctorBonusVO dbVO = new OSDoctorBonusVO();
                    dbVO.Id = db.Id;
                    dbVO.BonusCode = db.BonusFormCode;
                    dbVO.Price = db.Amount;
                    dbVO.DT = db.DataAddTime;
                    OSDoctorBonusList.Add(dbVO);
                }
            }
            doctorChargeVO.OSDoctorBonusList = OSDoctorBonusList;
        }
    }
}