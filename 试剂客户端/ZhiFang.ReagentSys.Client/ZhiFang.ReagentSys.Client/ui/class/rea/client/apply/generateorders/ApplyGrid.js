/**
 * @description 部门采购生成订单
 * @author longfc
 * @version 2018-02-26
 */
Ext.define('Shell.class.rea.client.apply.generateorders.ApplyGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyGrid',

	title: '生成订单',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "create",
	/**下拉状态默认值*/
	defaultStatusValue: "3",
	/**是否多选行*/
	checkOne: false,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.generateorders.ApplyGrid',
	/**用户UI配置Name*/
	userUIName: "生成订单列表",
	
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
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var removeArr = [];
		//暂存,已申请,审核退回
		if(tempList[1]) removeArr.push(tempList[1]);
		if(tempList[2]) removeArr.push(tempList[2]);
		if(tempList[4]) removeArr.push(tempList[4]);

		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		return tempList;
	},
	/**获取状态查询条件*/
	getStatusWhere: function() {
		var me = this;
		var statusWhere = me.callParent(arguments);
		if(!statusWhere)statusWhere='(reabmsreqdoc.Status=3 or reabmsreqdoc.Status=5)';
		return statusWhere;
	}
});