/**
 * 申请部门列表
 * @author longfc
 * @version 2016-10-27
 */
Ext.define('Shell.class.rea.client.apply.basic.HRDeptCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '申请部门列表',
	width: 400,
	height: 480,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchApplyHRDeptByByHRDeptId?isPlanish=true',

	/**是否单选*/
	checkOne: false,
	/**是否带确认按钮*/
	//hasAcceptButton: false,
	initComponent: function() {
		var me = this;
		//var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;		
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || 0;
		me.selectUrl = me.selectUrl + "&deptId=" + deptId;
		//查询框信息
		me.searchInfo = {
			width: 260,
			isLike: true,
			itemId: 'Search',
			emptyText: '中文名',
			fields: ['CName']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['->'];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'HRDept_CName',
			text: '中文名',
			width: 175,
			defaultRenderer: true
		}, {
			dataIndex: 'HRDept_EName',
			text: '英文名',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'HRDept_SName',
			text: '简称',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'HRDept_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		return columns;
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = 'CName',
			where = "";

		if(isLike) {
			where = fields + " like " + value + "";
		} else {
			where = fields + "=" + value + "";
		}
		return where;
	}
});