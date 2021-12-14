using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Common.Public
{
    public class GUIDHelp
    {
        public static long GetGUIDLong()
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
        public static string GetGUIDString()
        {
            return Guid.NewGuid().ToString();
        }
        public static Byte[] GetGUIDByte()
        {
            return Guid.NewGuid().ToByteArray();
        }
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }

        public static int GetGUIDInt()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}
