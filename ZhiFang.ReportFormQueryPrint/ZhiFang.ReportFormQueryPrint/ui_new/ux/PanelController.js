/**
 * 面板控制器
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.PanelController',{
	/**@override 渲染完毕执行*/
	afterRender:function(){
		this.overrideInfo('afterRender');
	},
	/**@override 初始化面板属性*/
	initComponent:function(){
		this.overrideInfo('initComponent');
	},
	/**@override 初始化参数*/
	initParams:function(){
		this.overrideInfo('initParams');
	},
	/**@override boxready时触发*/
	boxIsReady:function(){},
	/**启用所有的操作功能*/
	enableControl:function(bo){
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];
		
		for(var i=0;i<length;i++){
			if(toolbars[i].xtype == 'header') continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}
		
		var iLength = items.length;
		for(var i=0;i<iLength;i++){
			items[i][enable ? 'enable' : 'disable']();
		}
		if(bo){me.defaultLoad = true;}
	},
	/**禁用所有的操作功能*/
	disableControl:function(){
		this.enableControl(false);
	},
	/**右键快捷菜单*/
	onContextMenu:function(){
		var me = this;
		me.on({
//			contextmenu:{
//				element:'el',
//				fn:function(e,t,eOpts){
//					if(me.hasContextMenu){
//						me.fireEvent('contextmenu',me,e,t,eOpts);
//					}else{
//						//禁用浏览器的右键相应事件 
//	        			e.preventDefault();e.stopEvent();
//					}
//				}
//			},
			dblclick:{
				element:'el',
				fn:function(e,t,eOpts){
					me.fireEvent('dblclick',me,e,t);
				}
			}
		});
	},
	/**提示信息*/
	showInfo:function(value){
		this.showMsg({
			title:'提示信息',
			icon:Ext.Msg.INFO,
			msg:value,
			buttons:Ext.Msg.OK
		});
	},
	/**提示警告*/
	showWarning:function(value){
		this.showMsg({
			title:'警告信息',
			icon:Ext.Msg.WARNING,
			msg:value,
			buttons:Ext.Msg.OK
		});
	},
	/**提示错误*/
	showError:function(value){
		this.showMsg({
			title:'错误信息',
			icon:Ext.Msg.ERROR,
			msg:value,
			buttons:Ext.Msg.OK
		});
	},
	/**删除数据确认框*/
	confirmDel:function(fn,title,msg){
		this.showMsg({
            title:title || '删除确认',
            msg:msg || '确定要删除吗？',
            icon:Ext.Msg.WARNING,
            buttons:Ext.Msg.OKCANCEL,
            callback:fn
		});
	},
	/**弹出提示框*/
	showMsg:function(config){
		Shell.util.Msg.showMsg(config);
	},
	
	/**执行按钮点击*/
	onButtonClick:function(but,type){
		this.onClick(but,type);
	},
	/**功能栏按钮事件处理*/
	onClick:function(but,type){
		if(!type) return;
		
		var act = 'on' + type.slice(0,1).toUpperCase() + type.slice(1) + 'Click';
		if(Ext.typeOf(this[act]) == 'function'){
			this[act](but);
		}else{
			this.overrideInfo(act);
		}
	},
	
	/**点击刷新按钮*/
	onRefreshClick:function(){
		this.onSearch();
	},
	/**点击新增按钮*/
	onAddClick:function(but){
		this.overrideInfo('onAddClick');
	},
	/**点击修改按钮*/
	onEditClick:function(but){
		this.overrideInfo('onEditClick');
	},
	/**点击查看按钮*/
	onShowClick:function(but){
		this.overrideInfo('onShowClick');
	},
	/**点击删除按钮*/
	onDelClick:function(but){
		this.overrideInfo('onDelClick');
	},
	/**点击打印按钮*/
	onPrintClick:function(but){
		this.overrideInfo('onPrintClick');
	},
	/**点击导出按钮*/
	onExpClick:function(but){
		this.overrideInfo('onExpClick');
	},
	/**点击设置按钮*/
	onConfigClick:function(but){
		this.overrideInfo('onConfigClick');
	},
	/**执行查询功能*/
	onSearch:function(value){
		this.overrideInfo('onSearch');
	},
	/**点击高级查询*/
	onSearchClick:function(but){
		this.overrideInfo('onSearchClick');
	},
	/**点击帮助按钮*/
	onHelpClick:function(but){
		var me = this,
			url = but.iframeUrl,
			className = but.className,
			classConfig = but.classConfig || {},
			width = document.body.clientWidth * 0.6,
			height = document.body.clientHeight * 0.6;
			
		if(url){
			var config = {
				width:width,
				height:height,
				html:"<html><body><iframe src='" + url + "' height='100%' width='100%' frameborder='0' " +
					"style='overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;" +
					"top:0px;left:0px;right:0px;bottom:0px'></iframe></body></html>"
			};
			Shell.util.Win.open('Ext.panel.Panel',config);
		}else if(className && className != ''){
			Shell.util.Win.open(className,classConfig);
		}
	},
	
	/**点击保存按钮*/
	onSaveClick:function(but){
		this.overrideInfo('onSaveClick');
	},
	/**点击另存按钮*/
	onSaveasClick:function(but){
		this.overrideInfo('onSaveasClick');
	},
	/**点击重置按钮*/
	onResetClick:function(but){
		this.overrideInfo('onResetClick');
	},
	/**点击确定按钮*/
	onAcceptClick:function(but){
		this.overrideInfo('onAcceptClick');
	},
	/**点击取消按钮*/
	onCancelClick:function(but){
		this.overrideInfo('onCancelClick');
	},
	/**点击返回按钮*/
	onBackClick:function(but){
		this.overrideInfo('onBackClick');
	},
	
	/**重写的方法提示*/
	overrideInfo:function(name){
		Shell.util.Msg.showOverrideInfo(name);
	}
});