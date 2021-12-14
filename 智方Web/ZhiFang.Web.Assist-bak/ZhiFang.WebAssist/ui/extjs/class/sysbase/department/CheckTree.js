/**
 * 病区科室树信息
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.CheckTree',{
    extend:'Shell.class.sysbase.department.Tree',
	
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
		me.topToolbar = me.topToolbar || ['-',{
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: 75,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			onTriggerClick: function() {
				this.setValue('');
				me.clearFilter();
			},
			listeners: {
				keyup: {
					fn: function(field, e) {
						var bo = Ext.EventObject.ESC == e.getKey();
						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
					}
				}
			}
		},'->',{
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
	