/**
 * 打卡清单查询条件
 * @author liangyl	
 * @version 2017-01-23
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.leavelist.SearchToolbar', {
	extend: 'Shell.class.oa.at.attendance.statistical.empdetail.basic.SearchToolbar',
	height: 60,
	toolbarHeight:60,
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		//部门
		me.DeptName.x=5;
		me.DeptName.y=32;
		me.DeptName.width=160;
        items.push(me.DeptName);
        //员工
        me.UserName.x=185;
		me.UserName.y=32;
		me.UserName.width=160;
        items.push(me.UserName);
        
        //请假类型
        me.ATEmpApplyAllLog.x=365;
		me.ATEmpApplyAllLog.y=32;
		me.ATEmpApplyAllLog.width=180;
        items.push(me.ATEmpApplyAllLog);
        
        //审批状态
		me.ApproveStatusName.x=555;
		me.ApproveStatusName.y=32;
		me.ApproveStatusName.width=180;
        items.push(me.ApproveStatusName);
		return items;
	},
    afterRender: function () {
        var me = this;
        me.callParent(arguments);

		me.getComponent('down').hide();
		me.getComponent('up').show();
		me.OrderHide(true);
    },
    	/**隐藏其他组件*/
	OrderHide: function(bo) {
		var me = this;
		var DeptName = me.getComponent('DeptName');
		var UserName = me.getComponent('UserName');
		var ATEmpApplyAllLog = me.getComponent('ATEmpApplyAllLog');
		var ApproveStatusName = me.getComponent('ApproveStatusName');

		if(bo) {
			DeptName.show();
			UserName.show();
			ATEmpApplyAllLog.show();
			ApproveStatusName.show();
		} else {
			DeptName.hide();
			UserName.hide();
			ATEmpApplyAllLog.hide();
			ApproveStatusName.hide();
		}
	}
});