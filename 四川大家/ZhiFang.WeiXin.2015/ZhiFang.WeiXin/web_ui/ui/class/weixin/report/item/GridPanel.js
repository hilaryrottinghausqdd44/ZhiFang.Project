/**
 * 列表
 * @author liangyl	
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.report.item.GridPanel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'列表',
    layout:'fit',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.class.weixin.report.finance.SearchToolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = [];

		items.push(me.createButtontoolbar());
		return items;
	},
	createItems:function(){
		var me = this;
        me.Grid = Ext.create('Shell.class.weixin.report.item.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
         
		return [me.Grid];
	}
});