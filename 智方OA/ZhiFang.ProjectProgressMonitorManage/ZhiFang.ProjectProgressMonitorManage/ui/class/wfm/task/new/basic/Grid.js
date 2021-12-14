/**
 *任务列表
 * @author liangyl	
 * @version 2017-06-09
 */
Ext.define('Shell.class.wfm.task.new.basic.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    title:'任务列表',
    /**默认加载数据*/
	defaultLoad:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
	    buttonToolbarItems.push('-', {
	    	boxLabel: '本节点',itemId: 'checkBDictTreeId',checked: false,value: false,
			inputValue: false,xtype: 'checkbox',style: {marginRight: '8px'},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.fireEvent('change', com, newValue);
				}
			}
		});
		return buttonToolbarItems;
	}
});