/**
 * 员工身份TAB
 * @author Jcall
 * @version 2019-11-26
 */
Ext.define('Shell.class.sysbase.user.lis.Tab', {
	extend:'Ext.tab.Panel',
	title:'员工身份',
	width: 600,
	height: 300,
	
	//获取系统列表服务
	getSystemListUrl:'/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL',
	//错误信息样式
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	
	defaults:{border:false},
	tabPosition:'top',
	
	//员工ID
	EmpId:'',
	//员工名字
	EmpName:'',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initSystemTab();
	},
    
	initComponent:function(){
		var me = this;
		me.title += ' - ' + me.EmpName;
		me.callParent(arguments);
	},
	//初始化系统Tab
	initSystemTab:function(){
		var me = this;
		me.loadSystemList(function(list){
			me.initSystemConfig(list);
		});
	},
	//加载系统列表
	loadSystemList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getSystemListUrl;
			
		url += '?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemCode';
			
		JShell.Server.get(url,function(data){
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				var error = me.errorFormat.replace(/{msg}/,data.msg);
				me.update(error);
			}
		});
	},
	//初始化系统设置
	initSystemConfig:function(list){
		var me = this,
			tabs = [];
			
		for(var i in list){
			tabs.push(Ext.create('Shell.class.sysbase.user.lis.TypeGrid',{
				itemId:list[i].Id,
				title:list[i].SystemName,
				SYSTEM_CODE:list[i].SystemCode,//系统编码
				EmpId:me.EmpId,//员工ID
				EmpName:me.EmpName//员工名字
			}));
		}
		
		me.add(tabs);
	}
});