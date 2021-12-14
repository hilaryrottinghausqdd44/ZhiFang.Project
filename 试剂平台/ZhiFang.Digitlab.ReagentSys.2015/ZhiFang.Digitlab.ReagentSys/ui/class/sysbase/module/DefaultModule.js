/**
 * 默认模块
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.module.DefaultModule',{
    extend:'Ext.panel.Panel',
    title:'默认模块',
    
    autoScroll:true,
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	
    	me.initButtonListeners();
    },
    /**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.bodyStyle = 'background:#FFFFF0;';
		me.initData();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部按钮*/
	createItems:function(){
		var me = this,
			items = [];
		
		for(var i in me.ItemsConfig){
			items.push(me.createButton(me.ItemsConfig[i]));
		}
		
		return items;
	},
	/**创建按钮*/
	createButton:function(config){
		var me = this;
		var text = config.text || '';
		text = text.length > 5 ? text.slice(0,4) + '...' : text;
		var item = {
			xtype:'button',
			margin:10,
			tid:config.tid,
			tooltip: '<b>' + config.text + '</b>',
			className:config.url,
			text: '<div style="background:url(' +
				JShell.System.Path.MODULE_ICON_ROOT_64 + '/' + config.icon +
				') no-repeat; width:64px;height:64px;line-height:64px;"></div>' +
				'<div style="width:64px;margin-bottom:4px;"><b>' + text + '</b></div>'
		};
		return item;
	},
	/**按钮点击监听*/
	initButtonListeners:function(){
		var me = this,
			items = me.items.items,
			len = items.length;
			
		for(var i=0;i<len;i++){
			items[i].on({
				click:function(btn){
					me.onBtnClick(btn);
				}
			});
		}
	},
	/**按钮点击处理*/
	onBtnClick:function(btn){
		var me = this,
			tid = btn.tid,
			config = Ext.clone(me.ItemsConfig[tid]);
			
		config.icon = JShell.System.Path.MODULE_ICON_ROOT_16 + '/' + config.icon;
		Ext.getCmp('SystemViewport').ContentTab.insertTab(config);
	},
	initData:function(){
		var me = this;
		
		me.ItemsConfig = {
			'5482148218078933465':{
				tid:'5482148218078933465',
				icon:'dictionary.PNG',
				text:'机构类型维护',
				url:'#Shell.class.rea.cenorgtype.App'
			},
			'5592722171828410595':{
				tid:'5592722171828410595',
				icon:'configuration.PNG',
				text:'机构维护',
				url:'#Shell.class.rea.cenorg.App'
			},
			'5129644397110195291':{
				tid:'5129644397110195291',
				icon:'configuration.PNG',
				text:'上级机构维护',
				url:'#Shell.class.rea.cenorgcondition.ParentApp'
			},
			'5090050225588115603':{
				tid:'5090050225588115603',
				icon:'configuration.PNG',
				text:'下级机构维护',
				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
			},
			'4967004674776952047':{
				tid:'4967004674776952047',
				icon:'configuration.PNG',
				text:'试剂管理',
				url:'#Shell.class.rea.goods.Grid'
			}
			
			
//			,'1':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'2':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'3':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'4':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'5':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'6':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'7':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'8':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'9':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'10':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'11':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'12':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'13':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'14':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'15':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'16':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'17':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'18':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'19':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'20':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'21':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'22':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'23':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'24':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'25':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'26':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'27':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'28':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'30':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'31':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'32':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'33':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'34':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			},
//			'35':{
//				tid:'4967004674776952047',
//				icon:'configuration.PNG',
//				text:'试剂管理',
//				url:'#Shell.class.rea.goods.Grid'
//			},
//			'36':{
//				tid:'5482148218078933465',
//				icon:'dictionary.PNG',
//				text:'机构类型维护',
//				url:'#Shell.class.rea.cenorgtype.App'
//			},
//			'37':{
//				tid:'5592722171828410595',
//				icon:'configuration.PNG',
//				text:'机构维护',
//				url:'#Shell.class.rea.cenorg.App'
//			},
//			'38':{
//				tid:'5129644397110195291',
//				icon:'configuration.PNG',
//				text:'上级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ParentApp'
//			},
//			'39':{
//				tid:'5090050225588115603',
//				icon:'configuration.PNG',
//				text:'下级机构维护',
//				url:'#Shell.class.rea.cenorgcondition.ChildrenApp'
//			}
		};
	}
});
	