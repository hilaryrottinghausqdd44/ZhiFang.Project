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

/**系统管理员账号*/
JcallShell.System.ADMINNAME = 'admin';
/**点击系统图标跳转页面*/
JcallShell.System.LOGO_TO_PAGE = 'http://demo.zhifang.com.cn';

/**服务器时间*/
JcallShell.System.Date = {
	/**每隔一段时间向服务器校准时间，单位：秒*/
	seconds:300,
	/**失败时尝试请求的次数*/
	tryTimes:10,
	/**当前的尝试次数*/
	_tryCount:0,
	_sysTime:null,
	_url:'/ConstructionService.svc/CS_UDTO_GetServerInformation',
	_leftSeconds:null,
	_milliseconds:1000,
	getDate:function(){
		return this._sysTime;
	},
	next:function(){
		var me = this;
		me._leftSeconds--;
		
		if(me._leftSeconds == 0){
			me.init();
		}else{
			me._sysTime = new Date(me._sysTime.getTime() + me._milliseconds);
			setTimeout(function(){me.next();},me._milliseconds);
		}
	},
	init:function(callback){
		var me = this;
		me._leftSeconds = me.seconds;
		var url = (this._url.slice(0, 4) == 'http' ? '' :
			JcallShell.System.Path.ROOT) + this._url;
			
		JcallShell.Server.get(url,function(data){
			if(data.success){
				var d = data.value.ServerCurrentTime;
				me._sysTime = JcallShell.Date.getDate(d);
				
				//LOG输出
				JcallShell.Msg.log(JcallShell.Date.toString(new Date(),false,true) + 
					'【校准成功】服务器时间:' + JcallShell.Date.toString(me._sysTime,false,true));
					
				setTimeout(function(){me.next();},me._milliseconds);
				if(callback){callback();}
			}else{
				if(me._tryCount < me.tryTimes){
					me._tryCount++;
					setTimeout(function(){
						JcallShell.Msg.log('尝试第' + me._tryCount + '/' + me.tryTimes + 
							'次获取服务器时间失败 ' + JcallShell.Date.toString(new Date(),false,true));
						JcallShell.System.Date.init(callback);
					},1000);
				}
			}
		});
	}
};

JcallShell.System.getPinYinZiTou = function(value,callback){
	var url = JcallShell.System.Path.ROOT + 
		'/ConstructionService.svc/GetPinYin?chinese=' + value;
	
	url = JcallShell.String.decode(url,true);
	
	if(!value || value == ""){
		callback("");
		return;
	}
	var result = "";
	JShell.Server.get(url,function(text){
		var data = Ext.JSON.decode(text);
		if(data.success){
			result = data.ResultDataValue;
		}else{
			JcallShell.Msg.error(data.msg,null,500);
		}
		callback(result);
	},null,null,true);
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
})();