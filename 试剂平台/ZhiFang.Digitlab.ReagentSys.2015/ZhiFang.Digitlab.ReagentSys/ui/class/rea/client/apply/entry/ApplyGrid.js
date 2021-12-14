/**
 * @description 部门采购申请录入申请主列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.entry.ApplyGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyGrid',

	title: '申请信息',
	width: 800,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",

	/**下拉状态默认值*/
	defaultStatusValue: "1",
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 285,
			isLike: true,
			itemId: 'Search',
			emptyText: '申请人/申请单号',
			fields: ['reabmsreqdoc.ApplyName', 'reabmsreqdoc.ReqDocNo']
		};
		me.defaultWhere = me.defaultWhere || '';
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(me.defaultWhere) me.defaultWhere = '(' + me.defaultWhere + ') and ';
		me.defaultWhere += 'reabmsreqdoc.ApplyID=' + userId;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
	}
});