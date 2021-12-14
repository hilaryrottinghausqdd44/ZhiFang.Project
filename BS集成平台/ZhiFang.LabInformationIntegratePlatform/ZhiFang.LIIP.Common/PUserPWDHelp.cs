using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.LIIP.Common
{
    public class PUserPWDHelp
    {
        private static readonly string Strcoust = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
        private static void Get64Str(int il, string pwd, ref int iLength, ref string Result)
        {
            char achar;
            int iCount64, iChar, imod64, i;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                achar = pwd[iLength - il + i];
                iChar = (int)achar;
                iCount64 = iCount64 * 256 + iChar;
            }
            for (i = 0; i < 4; i++)
            {
                if (iCount64 == 0)
                {
                    Result = '=' + Result;
                }
                else
                {
                    imod64 = (iCount64 % 64) + 1;
                    iCount64 = iCount64 / 64;
                    achar = Strcoust[imod64 - 1];
                    Result = achar + Result;
                }
            }
        }

        private static void Get256Str(int il, ref string pwd, ref int iLength, ref string Result)
        {
            int i, ichar, iCount64, imod256;
            iCount64 = 0;
            for (i = 0; i < il; i++)
            {
                ichar = Strcoust.IndexOf(pwd[iLength + i - il]);
                iCount64 = iCount64 * 64 + ichar;
            }
            for (i = 0; i < 3; i++)
            {
                if (iCount64 != 0)
                {
                    imod256 = iCount64 % 256;
                    Result = ((char)imod256) + Result;
                    iCount64 = iCount64 / 256;
                }
            }
        }
        //加密
        public static string CovertPassWord(string astr)
        {
            int iLength, imod3, idv3;
            iLength = astr.Length;
            string Result = "";
            if (iLength == 0)
            {
                return "=";
            }
            idv3 = iLength / 3;
            imod3 = iLength % 3;
            while (idv3 > 0)
            {
                Get64Str(3, astr, ref iLength, ref Result);
                iLength = iLength - 3;
                idv3--;
            }
            switch (imod3)
            {
                case 1:
                    Get64Str(1, astr, ref iLength, ref Result);
                    break;
                case 2:
                    Get64Str(2, astr, ref iLength, ref Result);
                    break;
            }
            return Result;
        }
        //解密
        public static string UnCovertPassWord(string pwd)
        {
            int iLength, imod4, idv4;
            iLength = pwd.Length;
            string Result = "";
            if (iLength == 0)
            {
                return "";
            }

            idv4 = iLength / 4;
            imod4 = iLength % 4;
            while (idv4 > 0)
            {
                Get256Str(4, ref pwd, ref iLength, ref Result);
                iLength = iLength - 4;
                idv4--;
            }
            switch (imod4)
            {
                case 1:
                    Get256Str(1, ref pwd, ref iLength, ref Result);
                    break;
                case 2:
                    Get256Str(2, ref pwd, ref iLength, ref Result);
                    break;
                case 3:
                    Get256Str(3, ref pwd, ref iLength, ref Result);
                    break;
            }

            for (var i = 0; i < Result.Length; i++)
            {
                if ((int)Result[i] < 32 || (int)Result[i] > 127)
                {
                    Result = pwd;
                }
            }
            return Result;
        }
    }
}
