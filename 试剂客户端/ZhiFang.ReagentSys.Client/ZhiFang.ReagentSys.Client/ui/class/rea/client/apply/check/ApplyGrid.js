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

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "check",
	/**下拉状态默认值*/
	defaultStatusValue: "2",
	/**审核服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck',
	
	/**用户UI配置Key*/
	userUIKey: 'apply.check.ApplyGrid',
	/**用户UI配置Name*/
	userUIName: "采购审核列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 255,
			isLike: true,
			itemId: 'search',
			emptyText: '申请人/申请单号',
			fields: ['reabmsreqdoc.ApplyName', 'reabmsreqdoc.ReqDocNo']
		};
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) me.defaultWhere = '(' + me.defaultWhere + ') and ';
		me.defaultWhere += ' (reabmsreqdoc.Status!=1)';
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);

		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});