/**
 * @description 部门采购审核
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.check.ApplyGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyGrid',

	title: '采购审核',
	width: 800,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "check",
	/**下拉状态默认值*/
	defaultStatusValue: "2",

	/**审核服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck',
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
		if(me.defaultWhere) me.defaultWhere = '(' + me.defaultWhere + ') and ';
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		//申请审核应用:只审核自己部门的数据,状态应该过滤掉暂时
		me.defaultWhere += ' (reabmsreqdoc.DeptID=' + deptId + ') and (reabmsreqdoc.Status!=1)';
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);

		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});