/**
 * 应用面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.panel.AppPanel',{
    extend:'Ext.panel.Panel',
    mixins:['Shell.ux.Langage'],
    layout:'border',
    bodyPadding:1,
    
    /**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText:JShell.Server.SAVE_TEXT,
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
    	me.disableControl();//禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
    	me.enableControl();//启用所有的操作功能
	},
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
	},
	/**禁用所有的操作功能*/
	disableControl:function(){
		this.enableControl(false);
	}
});
	