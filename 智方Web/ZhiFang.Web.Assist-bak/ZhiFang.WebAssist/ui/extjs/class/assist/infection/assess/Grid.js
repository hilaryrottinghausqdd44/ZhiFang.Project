/**
 * 环境监测送检样本登记
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.assess.Grid', {
	extend: 'Shell.class.assist.infection.basic.Grid',
	
	/**感控评估查询项*/
	IsHaveDept:true,
	
	/**用户UI配置Key*/
	userUIKey: 'assist.infection.assess.Grid',
	/**用户UI配置Name*/
	userUIName: "环境监测送检样本登记列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	}
});
