/**
 * 基础面板类
 * @author Jcall
 * @version 2014-08-12
 */
Ext.define('Shell.ux.panel.Panel',{
	extend:'Ext.panel.Panel',
	
	requires:['Shell.ux.HeaderTool','Shell.ux.ButtonsToolbar'],
	mixins:['Shell.ux.server.Ajax','Shell.ux.PanelController'],
	
	width:2400,
	height:1200,
	
	/**帮助功能*/
	help:false,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		//视图准备完毕
		me.on({boxready:function(){me.boxIsReady();}});
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.tools = me.createTools();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建标题功能栏*/
	createTools:function(){
		var me = this,
			tools = me.tools || [];
			
		if(me.help){
			tools.push(Ext.apply({
				xtype:'uxtool',type:'help',tooltip:'<b>帮助</b>',
				handler:function(){me.onHelpClick(this);}
			},me.help));
		}
		
		return tools;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			tool = [];//[{dock:'bottom',buttons:['->']}];
			
		//if(me.floating){tool[0].buttons.push('cancel');};
			
		var toolbars = me.toolbars || tool,
			length = toolbars.length,
			dockedItems = [];
		
		for(var i=0;i<length;i++){
			dockedItems.push(Ext.apply({
				autoScroll:true,
				dock:toolbars[i].dock || 'top',
				xtype:'uxbuttonstoolbar',
				buttons:toolbars[i].buttons,
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					}
				}
			},toolbars[i]));
		}
			
		return dockedItems;
	},
	
	/**帮助按钮处理*/
	onHelpClick:function(){
		this.overrideInfo('onHelpClick');
	}
});