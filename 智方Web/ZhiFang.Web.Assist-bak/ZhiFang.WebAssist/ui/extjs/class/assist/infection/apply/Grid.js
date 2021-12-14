/**
 * @description 环境监测送检样本登记--只按当前登录科室使用
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.apply.Grid', {
	extend: 'Shell.class.assist.infection.basic.Grid',
	
	/**用户UI配置Key*/
	userUIKey: 'assist.infection.apply.Grid',
	/**用户UI配置Name*/
	userUIName: "环境监测送检样本登记列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//如果科室信息为空,提示帐号绑定科室后再使用
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(deptId){
			me.defaultWhere="gksamplerequestform.DeptId="+deptId;
		}
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.callParent(arguments);
		
		//如果科室信息为空,清空数据,提示帐号绑定科室后再使用
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(!deptId){
			var error = me.errorFormat.replace(/{msg}/, "未获取到当前登录账号的所属科室信息!");
			me.getView().update(error);
			return false;
		}
	}
	
});
