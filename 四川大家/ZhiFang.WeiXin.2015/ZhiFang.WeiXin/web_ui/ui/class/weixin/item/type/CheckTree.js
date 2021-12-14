/**
 * 检测项目产品分类树选择列表
 * @author liangyl
 * @version 2017-01-18
 */
Ext.define('Shell.class.weixin.item.type.CheckTree',{
    extend:'Shell.class.weixin.item.type.Tree',
	
	title:'选择项目分类',
	width:250,
	height:330,
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有项目分类',
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
	