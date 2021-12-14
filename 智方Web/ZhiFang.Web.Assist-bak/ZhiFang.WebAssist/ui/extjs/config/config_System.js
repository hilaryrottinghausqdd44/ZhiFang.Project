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
JcallShell.System.Name = '平台系统';
//同域系统映射
JcallShell.System.Map = {
	'REA.CLIENT': 'zf.rea.client',
	'QMS': 'ZhiFang.QMS',
	'GENE': 'ZhiFang.Gene'
};
/**系统管理员账号*/
JcallShell.System.ADMINNAME = 'admin';
/**admin是否可登陆*/
JcallShell.System.ADMIN_CAN_LOGIN = true;
/**点击系统图标跳转页面*/
JcallShell.System.LOGO_TO_PAGE = 'http://demo.zhifang.com.cn';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/login/LoginTop.png';

//JS文件加载完毕时处理
JcallShell.System.afertJSLoading = function() {
	var loading = document.getElementById("loading-div");
	if (loading) {
		loading.parentNode.removeChild(loading);
		//loading.remove();
	}
};

/**服务器时间*/
JcallShell.System.Date = {
	/**每隔一段时间向服务器校准时间，单位：秒*/
	seconds: 300,
	/**失败时尝试请求的次数*/
	tryTimes: 10,
	/**当前的尝试次数*/
	_tryCount: 0,
	_sysTime: null,
	_url: '/ServerWCF/ConstructionService.svc/CS_UDTO_GetServerInformation', ///ServerWCF
	_leftSeconds: null,
	_milliseconds: 1000,
	_isError: null,
	/**服务器错误*/
	isError: function() {
		//从集成平台获取服务器时间
		if (top.layui && top.layui.system) {
			return top.layui.system.date.isError();
		}
		return this._isError;
	},
	/**获取服务器时间*/
	getDate: function() {
		//从集成平台获取服务器时间
		if (top.layui && top.layui.system) {
			return top.layui.system.date.getDate();
		}
		return this._sysTime;
	},
	next: function() {
		var me = this;
		me._leftSeconds--;

		if (me._leftSeconds == 0) {
			me.init();
		} else {
			me._sysTime = new Date(me._sysTime.getTime() + me._milliseconds);
			setTimeout(function() {
				me.next();
			}, me._milliseconds);
		}
	},
	init: function(callback) {
		var me = this;
		//从集成平台获取服务器时间
		if (top.layui && top.layui.system) {
			return top.layui.system.date.init(callback);
		}
		me._leftSeconds = me.seconds;
		var url = (this._url.slice(0, 4) == 'http' ? '' :
			JcallShell.System.Path.ROOT) + this._url; //JShell.System.Path.RBAC_ROOT

		JcallShell.Server.get(url, function(data) {
			if (data.success) {
				me._isError = false;
				var d = data.value.ServerCurrentTime;
				me._sysTime = JcallShell.Date.getDate(d);

				//LOG输出
				JcallShell.Msg.log(JcallShell.Date.toString(new Date(), false, true) +
					'【校准成功】服务器时间:' + JcallShell.Date.toString(me._sysTime, false, true));

				setTimeout(function() {
					me.next();
				}, me._milliseconds);
				if (callback) {
					callback();
				}
			} else {
				if (me._tryCount < me.tryTimes) {
					me._tryCount++;
					setTimeout(function() {
						JcallShell.Msg.log('尝试第' + me._tryCount + '/' + me.tryTimes +
							'次获取服务器时间失败 ' + JcallShell.Date.toString(new Date(), false, true));
						JcallShell.System.Date.init(callback);
					}, 1000);
				} else {
					me._isError = true;
				}
			}
		});
	}
};

JcallShell.System.getPinYinZiTou = function(value, callback) {
	var url = JcallShell.System.Path.ROOT + '/ServerWCF/ConstructionService.svc/GetPinYin?chinese=' + value; ///ServerWCF

	url = JcallShell.String.encode(url, true);

	if (!value || value == "") {
		callback("");
		return;
	}
	var result = "";
	JShell.Server.get(url, function(text) {
		var data = Ext.JSON.decode(text);
		if (data.success) {
			result = data.ResultDataValue;
		} else {
			JcallShell.Msg.error(data.msg, null, 500);
		}
		callback(result);
	}, null, null, true);
};
/**枚举*/
JcallShell.System.Enum = {
	/**模块类型*/
	ModuleType: {
		'E0': '内部模块',
		'E1': '内部链接',
		'E2': '外部链接',
		'E3': '功能包'
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
	getList: function(name, hasAll, hasColor, hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if (!obj) return [];

		if (hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for (var i in obj) {
			if (!hasNull) {
				if (obj[i] == '无') continue;
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
/**系统类字典*/
JcallShell.System.ClassDict = {
	//获取类字典服务/ServerWCF
	_classDicUrl: '/ServerWCF/CommonService.svc/GetClassDic',
	//获取类字典列表服务/ServerWCF
	_classDicListUrl: '/ServerWCF/CommonService.svc/GetClassDicList',
	/** @public
	 * 初始化字典信息，支持单个字典，也支持多个字典
	 * @param {Object} classNameSpace 类域
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.init(
	 * 		'ZhiFang.Entity.ProjectProgressMonitorManage',
	 * 		'PContractStatus',
	 * 		function(){
	 * 			//回调函数处理
	 * 		}
	 * 	);
	 * 	JcallShell.System.ClassDict.init([
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PContractStatus'},
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PTaskStatus'}
	 * 	],function(){
	 * 		//回调函数处理
	 * 	});
	 */
	init: function(classNameSpace, className, callback) {
		var me = this;
		var type = Ext.typeOf(classNameSpace);

		if (type == 'string') {
			//单个字典
			if (me[className]) {
				if (Ext.typeOf(callback) == 'function') {
					callback();
				}
			} else {
				me.loadClassInfo(classNameSpace, className, callback);
			}
		} else if (type == 'array') {
			var classParamList = classNameSpace,
				callback = className,
				hasData = true;

			for (var i in classParamList) {
				if (!me[classParamList[i].classname]) {
					hasData = false;
					break;
				}
			}

			if (hasData) {
				if (Ext.typeOf(callback) == 'function') {
					callback();
				}
			} else {
				me.loadClassInfoList(classParamList, callback);
			}
		}
	},
	/**
	 * 加载单个类字典信息
	 * @param {Object} classNameSpace 类域
	 * @param {Object} className 类名
	 * @param {Object} callback 回调函数
	 */
	loadClassInfo: function(classNameSpace, className, callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me._classDicUrl);
		url += '?classnamespace=' + classNameSpace + '&classname=' + className;

		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.initClassInfo(className, data.value);
			} else {
				me.initClassInfo(className, null);
			}
			if (Ext.typeOf(callback) == 'function') {
				callback();
			}
		});
	},
	/**
	 * 加载多个类字典信息
	 * @param {Object} classParamList 类字典参数
	 * @param {Object} callback 回调函数
	 * @example
	 * 	JcallShell.System.ClassDict.loadClassInfoList([
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PContractStatus'},
	 * 		{classnamespace:'ZhiFang.Entity.ProjectProgressMonitorManage',classname:'PTaskStatus'}
	 * 	],function(){
	 * 		//回调函数处理
	 * 	});
	 */
	loadClassInfoList: function(classParamList, callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me._classDicListUrl);

		var params = {
			jsonpara: classParamList
		};
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if (data.success) {
				for (var i in classParamList) {
					me.initClassInfo(classParamList[i].classname, data.value[i][classParamList[i].classname]);
				}
			} else {
				for (var i in classParamList) {
					me.initClassInfo(classParamList[i].classname, null);
				}
			}
			if (Ext.typeOf(callback) == 'function') {
				callback();
			}
		});
	},
	initClassInfo: function(className, data) {
		this[className] = data;
	},
	/** @public
	 * 根据字典内容ID获取字典内容
	 * @param {Object} className
	 * @param {Object} id
	 */
	getClassInfoById: function(className, id) {
		var classInfo = this[className],
			data = null;

		for (var i in classInfo) {
			if (classInfo[i].Id == id) {
				data = classInfo[i];
				break;
			}
		}

		return data;
	},
	/** @public
	 * 根据字典内容Name获取字典内容
	 * @param {Object} className
	 * @param {Object} name
	 */
	getClassInfoByName: function(className, name) {
		var classInfo = this[className],
			data = null;

		for (var i in classInfo) {
			if (classInfo[i].Name == name) {
				data = classInfo[i];
				break;
			}
		}

		return data;
	}
};
/***
 * 获取某一window的最顶级的父窗体对象
 * @param {Object} curWin:当前window对象
 */
JcallShell.System.getTop = function(curWin) {
	var curWin = curWin || window;
	var win = curWin.top == curWin ? curWin : JcallShell.System.getTop(curWin.top);
	return win;
};
(function() {
	window.JcallShell =window.JShell = JcallShell;
	//JcallShell.System.getTop(window).JShell = window.JShell = JcallShell;
})();
