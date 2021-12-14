/**
 * 部门选择
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.basic.CheckTree', {
	extend: 'Shell.class.sysbase.org.CheckTree',
	title: '部门选择',
	width: 270,
	height: 300,
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.topToolbar = me.topToolbar || [ '-',{
			xtype: 'checkbox',
			boxLabel: '本节点',
			checked: true,
			value: false,
			inputValue: false,
			itemId: 'onlyShowDept'
		},'-','->',{
			xtype:'button',
			iconCls:'button-accept',
			text:'确定',
			tooltip:'确定',
			handler:function(){
				me.onAcceptClick();
			}
		}];
		
		me.callParent(arguments);
	}
});