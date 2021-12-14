
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BSampleType : BaseBLL<SampleType, int>, ZhiFang.IBLL.WebAssist.IBSampleType
    {
        public string GetSampleTypeCName(string code1)
        {
            string testItemId = "", testItemCName = "";
            if (code1.Contains("^"))
            {
                testItemId = code1.Split('^')[0];
            }
            else
            {
                testItemId = code1.Trim();
            }

            if (string.IsNullOrEmpty(testItemId)) return testItemCName;

            int id2 = 0;
            int.TryParse(testItemId, out id2);
            SampleType testItem = this.Get(id2);
            if (testItem != null)
            {
                testItemCName = testItem.CName;
            }

            return testItemCName;
        }
    }
}