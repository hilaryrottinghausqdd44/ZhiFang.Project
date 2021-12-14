using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZhiFang.Common.Public
{
    public class StringPlus
    {
        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        public static SortedList GetStrSortedList(string str, char speater1, char speater2)
        {
            SortedList sl = new SortedList();
            string[] ss = str.Split(speater1);
            foreach (string s in ss)
            {
                if (s.Split(speater2).Length > 1)
                {
                    sl.Add(s.Split(speater2)[0], s.Split(speater2)[1]);
                }
                else
                {
                    sl.Add(s.Split(speater2)[0], "");
                }
            }
            return sl;
        }
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }

        #region 将字符串样式转换为纯字符串
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 将字符串转换为新样式
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //如果输入空值，返回空，并给出错误提示
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "请输入需要划分格式的字符串";
            }
            else
            {
                //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                }
                else
                {
                    //检查新样式中分隔符的位置
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //将分隔符放在新样式中的位置
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //给出最后的结果
                    ReturnValue = StrList;
                    //因为是正常的输出，没有错误
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion

        #region 提取字符串拼音字头
        /// <summary>
        /// 汉字转拼音静态类,包括功能全拼和缩写，方法全部是静态的
        /// </summary>
        public class Chinese2Spell
        {
            #region 编码定义

            private static int[] pyvalue = new int[] 
        { 
        -20319, -20317, -20304, -20295, -20292, -20283, -20265, -20257, -20242, -20230, -20051, -20036, -20032, 
        -20026, 
        -20002, -19990, -19986, -19982, -19976, -19805, -19784, -19775, -19774, -19763, -19756, -19751, -19746, 
        -19741, -19739, -19728, 
        -19725, -19715, -19540, -19531, -19525, -19515, -19500, -19484, -19479, -19467, -19289, -19288, -19281, 
        -19275, -19270, -19263, 
        -19261, -19249, -19243, -19242, -19238, -19235, -19227, -19224, -19218, -19212, -19038, -19023, -19018, 
        -19006, -19003, -18996, 
        -18977, -18961, -18952, -18783, -18774, -18773, -18763, -18756, -18741, -18735, -18731, -18722, -18710, 
        -18697, -18696, -18526, 
        -18518, -18501, -18490, -18478, -18463, -18448, -18447, -18446, -18239, -18237, -18231, -18220, -18211, 
        -18201, -18184, -18183, 
        -18181, -18012, -17997, -17988, -17970, -17964, -17961, -17950, -17947, -17931, -17928, -17922, -17759, 
        -17752, -17733, -17730, 
        -17721, -17703, -17701, -17697, -17692, -17683, -17676, -17496, -17487, -17482, -17468, -17454, -17433, 
        -17427, -17417, -17202, 
        -17185, -16983, -16970, -16942, -16915, -16733, -16708, -16706, -16689, -16664, -16657, -16647, -16474, 
        -16470, -16465, -16459, 
        -16452, -16448, -16433, -16429, -16427, -16423, -16419, -16412, -16407, -16403, -16401, -16393, -16220, 
        -16216, -16212, -16205, 
        -16202, -16187, -16180, -16171, -16169, -16158, -16155, -15959, -15958, -15944, -15933, -15920, -15915, 
        -15903, -15889, -15878, 
        -15707, -15701, -15681, -15667, -15661, -15659, -15652, -15640, -15631, -15625, -15454, -15448, -15436, 
        -15435, -15419, -15416, 
        -15408, -15394, -15385, -15377, -15375, -15369, -15363, -15362, -15183, -15180, -15165, -15158, -15153, 
        -15150, -15149, -15144, 
        -15143, -15141, -15140, -15139, -15128, -15121, -15119, -15117, -15110, -15109, -14941, -14937, -14933, 
        -14930, -14929, -14928, 
        -14926, -14922, -14921, -14914, -14908, -14902, -14894, -14889, -14882, -14873, -14871, -14857, -14678, 
        -14674, -14670, -14668, 
        -14663, -14654, -14645, -14630, -14594, -14429, -14407, -14399, -14384, -14379, -14368, -14355, -14353, 
        -14345, -14170, -14159, 
        -14151, -14149, -14145, -14140, -14137, -14135, -14125, -14123, -14122, -14112, -14109, -14099, -14097, 
        -14094, -14092, -14090, 
        -14087, -14083, -13917, -13914, -13910, -13907, -13906, -13905, -13896, -13894, -13878, -13870, -13859, 
        -13847, -13831, -13658, 
        -13611, -13601, -13406, -13404, -13400, -13398, -13395, -13391, -13387, -13383, -13367, -13359, -13356, 
        -13343, -13340, -13329, 
        -13326, -13318, -13147, -13138, -13120, -13107, -13096, -13095, -13091, -13076, -13068, -13063, -13060, 
        -12888, -12875, -12871, 
        -12860, -12858, -12852, -12849, -12838, -12831, -12829, -12812, -12802, -12607, -12597, -12594, -12585, 
        -12556, -12359, -12346, 
        -12320, -12300, -12120, -12099, -12089, -12074, -12067, -12058, -12039, -11867, -11861, -11847, -11831, 
        -11798, -11781, -11604, 
        -11589, -11536, -11358, -11340, -11339, -11324, -11303, -11097, -11077, -11067, -11055, -11052, -11045, 
        -11041, -11038, -11024, 
        -11020, -11019, -11018, -11014, -10838, -10832, -10815, -10800, -10790, -10780, -10764, -10587, -10544, 
        -10533, -10519, -10331, 
        -10329, -10328, -10322, -10315, -10309, -10307, -10296, -10281, -10274, -10270, -10262, -10260, -10256, 
        -10254 
        };

            private static string[] pystr = new string[] 
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
            "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
            "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
            "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
            "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
            "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
            "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
            "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
            "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
            "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
            "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
            "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
            "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
            "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
            "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
            "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
            "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
            "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
            "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
            "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
            "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
            "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
            "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
            "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
            "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
            "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
            "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
            "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
            "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
            "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
            "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
            "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

            #endregion

            //然后就是拼音的处理方法了

            #region 拼音处理

            /// <summary> 
            /// 将一串中文转化为拼音
            /// 如果给定的字符为非中文汉字将不执行转化，直接返回原字符；
            /// </summary> 
            /// <param name="chsstr">指定汉字</param> 
            /// <returns>拼音码</returns> 
            public static string ChsString2Spell(string chsstr)
            {
                string strRet = string.Empty;

                char[] ArrChar = chsstr.ToCharArray();

                foreach (char c in ArrChar)
                {
                    strRet += SingleChs2Spell(c.ToString());
                }

                return strRet;
            }
            /// <summary> 
            /// 将一串中文转化为拼音
            /// </summary> 
            /// <param name="chsstr">指定汉字</param> 
            /// <returns>拼音首字母</returns> 
            public static string GetHeadOfChs(string chsstr)
            {
                string strRet = string.Empty;

                char[] ArrChar = chsstr.ToCharArray();

                foreach (char c in ArrChar)
                {
                    strRet += GetHeadOfSingleChs(c.ToString());
                }

                return strRet;
            }

            /// <summary> 
            /// 单个汉字转化为拼音
            /// </summary> 
            /// <param name="SingleChs">单个汉字</param> 
            /// <returns>拼音</returns> 
            public static string SingleChs2Spell(string SingleChs)
            {
                byte[] array;
                int iAsc;
                string strRtn = string.Empty;

                array = Encoding.Default.GetBytes(SingleChs);

                try
                {
                    iAsc = (short)(array[0]) * 256 + (short)(array[1]) - 65536;
                }
                catch
                {
                    iAsc = 1;
                }

                if (iAsc > 0 && iAsc < 160)
                    return SingleChs;

                for (int i = (pyvalue.Length - 1); i >= 0; i--)
                {
                    if (pyvalue[i] <= iAsc)
                    {
                        strRtn = pystr[i];
                        break;
                    }
                }

                //将首字母转为大写
                if (strRtn.Length > 1)
                {
                    strRtn = strRtn.Substring(0, 1).ToUpper() + strRtn.Substring(1);
                }

                return strRtn;
            }

            /// <summary> 
            /// 得到单个汉字拼音的首字母
            /// </summary> 
            /// <returns> </returns> 
            public static string GetHeadOfSingleChs(string SingleChs)
            {
                return SingleChs2Spell(SingleChs).Substring(0, 1);
            }
            #endregion

        }
        #endregion

        #region 转义特殊字符
        /// <summary>
        /// 转换特殊字符
        /// </summary>
        /// <param name="SourceStr"></param>
        /// <returns></returns>
        public static string ConvertSpecialCharacter(string SourceStr)
        {
            string tempStr = SourceStr;
            tempStr = tempStr.Replace("\\", "\\\\");
            tempStr = tempStr.Replace("\"", "\\\"");
            tempStr = tempStr.Replace("'", "''");
            return tempStr;
        }
        #endregion

        #region SQL语句中特殊字符转义
        /// <summary>
        /// SQL语句中特殊字符转义
        /// </summary>
        /// <param name="esc"></param>
        /// <returns></returns>
        public static string SQLConvertSpecialCharacter(string SourceStr)
        {
            string tempStr = SourceStr;
            tempStr = tempStr.Replace("'", "''");
            return tempStr;
        }
        #endregion

        #region 实数字符串去掉末尾的零
        /// <summary>
        /// 实数字符串去掉末尾的零
        /// </summary>
        /// <param name="esc"></param>
        /// <returns></returns>
        public static string ConvertRealNumberStr(string RealNumberStr)
        {
            if (RealNumberStr.IndexOf(".")>=0)
              RealNumberStr.TrimEnd('.','0');
            return RealNumberStr;
        }
        #endregion

        #region 判断是否是数字
        public static bool CheckIsNumber(string str)
        {
            Regex r = new Regex("^[-,+][0-9]*$");
            return r.IsMatch(str);
        }
        #endregion

        #region 字符串转数据库字符串
        /// <summary>
        ///  字符串转数据库字符串 例如: "ABC" 转换为 'ABC'
        ///  主要应用HQL语句
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="isUseNull">如果源字符串为空时,此参数为false,则返回'';否则,返回null</param>
        /// <returns>字符串</returns>
        public static string StrConvertDataBaseStr(string str, bool isUseNull)
        {
            string resultStr = "\'" + str + "\'";
            if (string.IsNullOrEmpty(str.Trim()) && isUseNull)
                resultStr = "null";
            return resultStr;
        }
        #endregion

        #region Json字符串转Dictionary
        public static Dictionary<string, string> JsonStrToDictionary(string strJson)
        {
            Dictionary<string, string> judgeCondition = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(strJson))
            {
                JObject tempJObject = JObject.Parse(strJson);
                string[] names = tempJObject.Properties().Select(item => item.Name.ToString()).ToArray();
                foreach (string name in names)
                {
                    judgeCondition.Add(name, tempJObject[name].ToString());
                }
            }
            return judgeCondition;
        }
        #endregion 

        #region 根据属性名从Json字符串取值
        public static string GetPropertyValueByJsonStr(string strJson, string strPropertyName)
        {
            JObject tempJObject = JObject.Parse(strJson);
            return tempJObject[strPropertyName].ToString();

        }
        #endregion 
        //取出字符串表达式中{}中的内容,内容包括{}，例如：表达式"aa{3001}bb", 则取出的内容为{3001}
        public static string PatternBrace = @"{.*?}";

        //取出字符串表达式中{}中的内容,内容不包括{}，例如：表达式"aa{3001}bb", 则取出的内容为3001
        public static string PatternNoBrace = "(?<={)[^{}]+(?=})";

        //取出字符串表达式中[]中的内容,内容包括[]，例如：表达式"aa[1001]bb", 则取出的内容为[1001]
        public static string PatternSquareBrackets = @"\[.*?\]";

        //取出字符串表达式中[]中的内容,内容不包括[]，例如：表达式"aa[1001]bb", 则取出的内容为1001
        public static string PatternNoSquareBrackets = @"(?<=\[)[^\[\]]+(?=\])";

        #region 根据正则表达式取出相应的字符串内容
        public static IList<string> GetNeedContentByExpression(string strSource, string pattern)
        {
            string value = "";
            IList<string> listStr = new List<string>();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(strSource);
            foreach (Match match in matches)
            {
                value = match.Value;
                if (!string.IsNullOrEmpty(value))
                    listStr.Add(value);
            }
            return listStr;
        }
        #endregion 
    }           
}
