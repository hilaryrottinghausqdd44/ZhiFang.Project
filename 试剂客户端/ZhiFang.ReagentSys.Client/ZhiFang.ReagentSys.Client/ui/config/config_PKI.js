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
	/**锁定状态*/
	IsLocked: {
		'E0': '待对账',
		'E1': '对账中',
		'E2': '财务锁定',
		'E3': '对账错误'
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
	MigrationFlag:{
		'E0': '未上传',
		'E1': '部分上传',
		'E2': '已上传'
	},
	/**颜色*/
	Color: {
		'E0': '#FFCC00',
		'E1': '#FF99CC',
		'E2': '#99CC33',
		'E3': '#CC0033',
		'E4': '#663366',
		'E5': '#999966',
		'E6': '#663300',
		'E7': '#6699CC'
	},
	/**
	 * @param {Boolean} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getList: function(name, hasAll, hasColor,hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if (!obj) return [];

		if (hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for (var i in obj) {
			if(!hasNull){
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i]];
			if (hasColor) {
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
	if(params.LANG){
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();