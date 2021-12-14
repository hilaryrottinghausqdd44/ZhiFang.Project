/**
 * 环境卫生学及消毒灭菌效果监测--只按当前登录科室使用
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.statistics.infection.ofcurdept.OfDateTimeGrid', {
	extend: 'Shell.class.assist.statistics.infection.basic.OfDateTimeGrid',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//如果科室信息为空,提示帐号绑定科室后再使用
		var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(deptId){
			if(!me.defaultWhere){
				me.defaultWhere=" gksamplerequestform.DeptId="+deptId;
			}else{
				me.defaultWhere=me.defaultWhere+" gksamplerequestform.DeptId="+deptId;
			}
		}
		
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	}
});
