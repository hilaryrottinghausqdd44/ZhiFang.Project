/**
 * 功能菜单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.FunctionTree',{
    extend:'Shell.class.sysbase.module.Tree',
	
	title:'功能菜单',
	
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'功能菜单',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	initComponent:function(){
		var me = this;
		me.addEvents('configclick');
		me.topToolbar = me.topToolbar || ['-',{
			xtype:'button',
			itemId:'module',
			iconCls:'button-config',
			tooltip:'系统模块功能',
			hidden:true,
			handler:function(){
				me.fireEvent('configclick',me);
			}
		},'->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		me.callParent(arguments);
	}
});
	