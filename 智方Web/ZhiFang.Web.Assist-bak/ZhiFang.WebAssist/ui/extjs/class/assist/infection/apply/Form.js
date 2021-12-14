/**
 * 环境监测送检样本登记--科室监测
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.apply.Form', {
	extend: 'Shell.class.assist.infection.basic.Form',
	/**是否登录科室*/
	ISCURDEPT:true,
	/**1:感控监测;2:科室监测;*/
	MonitorType:"2",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	}
	
});
