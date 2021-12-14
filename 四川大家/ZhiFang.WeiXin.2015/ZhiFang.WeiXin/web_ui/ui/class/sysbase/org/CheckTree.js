/**
 * 选择组织机构树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.org.CheckTree',{
    extend:'Shell.class.sysbase.org.Tree',
	
	title:'选择组织机构',
	
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有组织机构',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.topToolbar = me.topToolbar || ['-','->',{
			xtype:'button',
			iconCls:'button-accept',
			text:'确定',
			tooltip:'确定',
			handler:function(){
				me.onAcceptClick();
			}
		}];
		
		me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('accept',me,records[0]);
	}
});
	