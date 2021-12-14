using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.LabStar;
using System.Data;

namespace ZhiFang.IBLL.LabStar
{
	/// <summary>
	///
	/// </summary>
	public interface IBBModuleGridList : IBGenericManager<BModuleGridList>
	{
		DataTable SearchModuleAggregateList(string gridCodes, string formCodes, string cheartCodes);
		bool GetModuleGridConfigDefault();
		void AddSetModuleGridConfigDefault();
		void EditSetModuleGridConfigDefault();
	}
}