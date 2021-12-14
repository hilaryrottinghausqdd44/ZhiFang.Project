/**
 * 环境监测送检样本登记--感染科登记
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.assess.Form', {
	extend: 'Shell.class.assist.infection.basic.Form',
	
	/**是否启用合格/不合格按钮*/
	hasJudgment: true,
	/**1:感控监测;2:科室监测;*/
	MonitorType:"1",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	}
	
});
