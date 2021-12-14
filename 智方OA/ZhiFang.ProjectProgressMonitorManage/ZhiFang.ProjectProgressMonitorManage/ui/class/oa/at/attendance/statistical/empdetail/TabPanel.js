/**
 * 员工考勤明细
 * @author longfc
 * @version 2017-01-23
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.TabPanel', {
	extend: 'Ext.tab.Panel',
	title: '员工考勤明细',

	width: 600,
	height: 400,
	bodyPadding:1,
	border:false,
	margin:'1px 0px 0px 0px',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.PunchGrid= Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.punchlist.Grid', {
			itemId: 'PunchGrid',
			title:'打卡清单'
		});
		me.LeaveGrid= Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.leavelist.Grid', {
			itemId: 'LeaveGrid',
			title:'请假清单'
		});
		me.TravelGrid= Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.travellist.Grid', {
			itemId: 'TravelGrid',
			title:'出差清单'
		});
		me.OutGrid= Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.outlist.Grid', {
			itemId: 'OutGrid',
			title:'外出清单'	
		});
		me.OvertimeGrid= Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.overtimelist.Grid', {
			itemId: 'OvertimeGrid',
			title:'加班清单'
		});
		return [me.PunchGrid,me.LeaveGrid,me.TravelGrid,me.OutGrid, me.OvertimeGrid];

	},
	/**
	 * 隐藏 tab
	 * @param tabPanel
	 * @param tab
	 * @returns {boolean}
	 */
	hideTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.hide();
		tab.tab.hide();
	},
	/**
	 * 显示 tab
	 * @param tabPanel
	 * @param tab
	 */
	showTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.show();
		tab.tab.show();
		me.setActiveTab(tab);
	}
});