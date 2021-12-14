/**
 * @description 采购申请合并报表
 * @author longfc
 * @version 2018-11-27
 */
Ext.define('Shell.class.rea.client.apply.mergereport.ApplyGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyGrid',

	title: '合并报表',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "mergereport",
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**是否多选行*/
	checkOne: false,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.mergereport.ApplyGrid',
	/**用户UI配置Name*/
	userUIName: "申请合并报表列表",
	
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
		var removeArr = [];
		//暂存,已申请,审核退回
		if(tempList[1]) removeArr.push(tempList[1]);
		if(tempList[2]) removeArr.push(tempList[2]);
		if(tempList[4]) removeArr.push(tempList[4]);

		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		return tempList;
	}
});