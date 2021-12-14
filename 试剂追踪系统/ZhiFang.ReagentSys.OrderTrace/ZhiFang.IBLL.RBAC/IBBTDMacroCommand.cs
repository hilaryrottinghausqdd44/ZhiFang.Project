using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.RBAC
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBTDMacroCommand 
	{
        BTDMacroCommand Entity { get; set; }

        bool Add(string key);

        bool Edit(string key);

        bool Remove(string key);

        Dictionary<string, BTDMacroCommand> Search();

        BTDMacroCommand Get(string key);

        BTDMacroCommand Load(string key);

        int GetTotalCount();
	}
}