/**
 * 打卡清单查询条件
 * @author liangyl	
 * @version 2017-01-23
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.punchlist.SearchToolbar', {
	extend: 'Shell.class.oa.at.attendance.statistical.empdetail.basic.SearchToolbar',
	height: 60,
	toolbarHeight: 60,
	/**导出数据服务路径*/
	downLoadExcelUrl: '/WeiXinAppService.svc/SC_UDTO_DownLoadGetExportExcelOfATEmpSignInfoDetail',
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		//部门
		me.DeptName.x = 5;
		me.DeptName.y = 34;
		me.DeptName.width = 160;
		items.push(me.DeptName);
		//员工
		me.UserName.x = 185;
		me.UserName.y = 34;
		me.UserName.width = 160;
		items.push(me.UserName);
		//选择
		me.CheckGroup.x = 365;
		me.CheckGroup.y = 34;
		me.CheckGroup.width = 300;
		items.push(me.CheckGroup);

		return items;
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('down').hide();
		me.getComponent('up').show();
		me.OrderHide(true);
		//me.setHeight(30);
	},
	/**隐藏其他组件*/
	OrderHide: function(bo) {
		var me = this;
		var DeptName = me.getComponent('DeptName');
		var UserName = me.getComponent('UserName');
		var CheckGroup = me.getComponent('CheckGroup');
		if(bo) {
			DeptName.show();
			UserName.show();
			CheckGroup.show();
		} else {
			DeptName.hide();
			UserName.hide();
			CheckGroup.hide();
		}
	},
	/**获取参数*/
	getParams: function() {
		var me = this,
			DeptID = '',
			UserID = '',
			ApproveStatusID = -1,
			SDate = '',
			EDate = '',
			applySDate = '',
			applyEDate = '',
			ATEmpApplyAllLog = '',
			filterType = '',
			params = {};

		params.applySDate = applySDate;
		params.applyEDate = applyEDate;
		params.DeptID = DeptID;
		params.UserID = UserID;
		params.ApproveStatusID = ApproveStatusID;
		params.ATEmpApplyAllLog = ATEmpApplyAllLog;
		params.filterType = filterType;
		if(me.getComponent('BeginDate').getValue()) {
			params.applySDate = JcallShell.Date.toString(me.getComponent('BeginDate').getValue(), true);
		}
		if(me.getComponent('EndDate').getValue()) {
			params.applyEDate = JcallShell.Date.toString(me.getComponent('EndDate').getValue(), true);
		}
		if(me.getComponent('DeptID').getValue()) {
			params.DeptID = me.getComponent('DeptID').getValue();
		}
		if(me.getComponent('UserID').getValue()) {
			params.UserID = me.getComponent('UserID').getValue();
		}
		if(me.getComponent('ApproveStatusID').getValue()) {
			params.ApproveStatusID = me.getComponent('ApproveStatusID').getValue();
		}
		if(me.getComponent('ATEmpApplyAllLogID').getValue()) {
			params.ATEmpApplyAllLog = me.getComponent('ATEmpApplyAllLogID').getValue();
		}
		var CheckboxGroup = me.getComponent('CheckGroup').getChecked();
		for(var i = 0; i < CheckboxGroup.length; i++) {
			if(i > 0) {
				filterType += ",";
			}
			filterType += CheckboxGroup[i].inputValue;
			params.filterType = filterType;
		}
		params.isCheckTree = me.isCheckTree;
		return params;
	},
	/**导出EXCEL文件*/
	DownLoadExcel: function(pams) {
		var me = this,
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		var params = [];
		params.push("operateType=" + operateType);
		params.push("filterType=" + pams.filterType);
		params.push("deptId=" + pams.DeptID);
		params.push("isGetSubDept=" + pams.isCheckTree);
		params.push("empId=" + pams.UserID);
		params.push("startDate=" + pams.applySDate);
		params.push("endDate=" + pams.applyEDate);
		url += "?" + params.join("&");
		window.open(url);
	}
});