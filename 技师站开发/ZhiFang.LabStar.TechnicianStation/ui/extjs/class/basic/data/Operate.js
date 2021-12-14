/**
 * 当前登陆者操作存储（浏览器LocalStorage存储）
 * @author Jcall
 * @version 2020-07-13
 */
Ext.define('Shell.class.basic.data.Operate',{
	
	//当前确认人信息包含：人员ID,姓名,授权方式(登录者本人/预授权),开始时间,结束时间,操作时是否提示
	//例如:{"Id":"1","Name":"张三","OperaterType":"0","BeginTime":"2010-01-01 10:10:10","EndTime":"2010-01-02 10:10:10","isCheckTip":true}
	//保存当前确认人信息
	setHandlerInfo:function(info){
		var me = this,
			AccountInfo = me._getAccountInfo();
			
		AccountInfo['HandlerInfo'] = info;
		
		me._setAccountInfo(AccountInfo);
	},
	//获取当前确认人信息
	getHandlerInfo:function(){
		var me = this,
			AccountInfo = me._getAccountInfo(),
			HandlerInfo = AccountInfo['HandlerInfo'] || {};
			
		return HandlerInfo;
	},
	//当前审核人信息包含：人员ID,姓名,授权方式(登录者本人/预授权),开始时间,结束时间,操作时是否提示
	//例如:{"Id":"1","Name":"张三","OperaterType":"0","BeginTime":"2010-01-01 10:10:10","EndTime":"2010-01-02 10:10:10","isCheckTip":true}
	//保存当前审核人信息
	setCheckerInfo:function(info){
		var me = this,
			AccountInfo = me._getAccountInfo();
			
		AccountInfo['CheckerInfo'] = info;
		
		me._setAccountInfo(AccountInfo);
	},
	//获取当前审核人信息
	getCheckerInfo:function(){
		var me = this,
			AccountInfo = me._getAccountInfo(),
			CheckerInfo = AccountInfo['CheckerInfo'] || {};
			
		return CheckerInfo;
	},
	
	//保存登陆者存储信息
	_setAccountInfo:function(info){
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			LabStar_TS = JShell.LocalStorage.get('LabStar_TS',true);
			
		if(LabStar_TS){
			LabStar_TS[userId] = info;
			JShell.LocalStorage.set('LabStar_TS',JSON.stringify(LabStar_TS));
		}else{
			var data = {};
			data[userId] = info;
			JShell.LocalStorage.set('LabStar_TS',JSON.stringify(data));
		}
	},
	//获取登陆者存储信息
	_getAccountInfo:function(){
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			LabStar_TS = JShell.LocalStorage.get('LabStar_TS',true) || {},
			AccountInfo = LabStar_TS[userId] || {};
			
		return AccountInfo;
	}
});