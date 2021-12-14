/**
 * PKI参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};

/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = 'PKI系统';

JcallShell.PKI = JcallShell.PKI || {};

/**财务信息*/
JcallShell.PKI.Balance = {
	/**财务处理中的延后时间*/
	Dates: -10
};
/**枚举*/
JcallShell.PKI.Enum = {
	/**单位类型*/
	UnitType: {
		'E0': '无',
		'E1': '送检单位',
		'E2': '经销商',
		'E3': '个人'
	},
	/**合作级别*/
	CoopLevel: {
		'E0': '无',
		'E1': '送检单位',
		'E2': '单位科室'
	},
	/**对帐状态:列表列显示用*/
	IsLocked: {
		//'E0': '全部',
		'E1': '待对账',
		'E2': '销售锁定',
		'E3': '财务锁定',
		'E4': '待对账+销售锁定',
		'E5': '销售+财务锁定'
	},
	/**颜色*/
	IsLockedColor: {
		'E0': '#FFCC00',
		'E1': '#FFCC00',
		'E2': '#99CC33',
		'E3': '#6699CC',
		'E4': '#FF7F00',
		'E5': '#0000EE'
	},
	/**财务锁定标志*/
	IsFinanceLocked: {
		'E1': '财务锁定'
	},
	/**匹配状态*/
	IsGetPriceList: {
		'0': '未匹配',
		'1': '已匹配',
		'-1': '匹配错误无合同',
		'-2': '匹配错误无销售'
	},
	/**价格类型*/
	ItemPriceType: {
		'E0': '无',
		'E1': '合同价',
		'E2': '阶梯价',
		'E3': '免单价',
		'E4': '终端价'
	},
	/**上传状态*/
	MigrationFlag: {
		'E0': '未上传',
		'E1': '部分上传',
		'E2': '已上传'
	},
	/**样本项目状态*/
	SampleStateList: {
		0: '全部',
		'Accept': '已接收',
		'Accept_DA': '已接收',
		'Delete': '已删除',
		'Grouping': '已拆分',
		'HandOver': '已移交',
		'Receive': '正在检验',
		'Receive_Part': '部分检验',
		'Refuse': '已退回',
		'Report': '已完成',
		'Report_Part': '部分完成',
		'Report_Second': '已完成',
		'Review': '已复核',
		'SecondReview': '已复核',
		'Report': '一审',
		'Report_Second': '二审',
		'Refuse': '已退回',
		'Delete': '已删除'
	},
	/**财务锁定状态*/
	IsFinanceLockedColor: {
		'E0': '#FFCC00',
		'E1': '#99CC33'
	},
	/**样本项目状态*/
	SampleStateColor: {
		0: '#FFFFFF',
		'Report': '#CC0033',
		'Report_Second': '#FFCC00',
		'Refuse': '#99CC33',
		'Delete': '#FF3030'
	},
	/**颜色*/
	Color: {
		'E0': '#FFCC00',
		'E1': '#FF99CC',
		'E2': '#99CC33',
		'E3': '#CC0033',
		'E4': '#FF7F00',
		'E5': '#0000EE',
		'E6': '#663300',
		'E7': '#6699CC',
		'E10': '#99CC33',
		'E11': '#FFCC00',
		'E-1': '#CC0033'
	},

	DealerType: {
		'E0': '普通经销商',
		'E1': '直销经销商'
	},
	/**合同类型:0-无;1-送检单位;2-经销商 */
	ContractType: {
		'E0': '0',
		'E1': '1',
		'E2': '2'
	},
	ContractType2: {
		'E0': '无',
		'E1': '送检单位',
		'E2': '经销商'
	},
	/**
	 * @param {Boolean} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getList: function(name, hasAll, hasColor, hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if(!obj) return [];

		if(hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for(var i in obj) {
			if(!hasNull) {
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i]];
			if(hasColor) {
				li.push('font-weight:bold;color:' + me.Color[i] + ';');
			}
			list.push(li);
		}

		return list;
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG) {
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();