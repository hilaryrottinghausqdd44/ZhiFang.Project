/**
 * OA参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};

/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = '管理系统';

JcallShell.WFM = JcallShell.WFM || {};

/**字典类型编码*/
JcallShell.WFM.DictTypeCode = {
	/**公共_紧急程度*/
	Urgency:'100001',
	
	/**项目类型字典编码*/
	ProjectType:'101001',
	/**项目状态字典编码*/
	ProjectStatus:'101003',
	/**项目进度字典编码*/
	ProjectPace:'101004',
	
	/**任务类型字典编码*/
	TaskType:'102001',
	/**任务执行方式字典编码*/
	TaskExecutType:'102002',
	/**任务状态字典编码*/
	TaskStatus:'102003',
	/**任务进度字典编码*/
	TaskPace:'102004',
	/**任务评估字典编码*/
	TaskEval:'102005'
},

/**枚举*/
JcallShell.WFM.Enum = {
	/**单位类型*/
	UnitType: {
		'E0': '无',
		'E1': '送检单位',
		'E2': '经销商',
		'E3': '个人'
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