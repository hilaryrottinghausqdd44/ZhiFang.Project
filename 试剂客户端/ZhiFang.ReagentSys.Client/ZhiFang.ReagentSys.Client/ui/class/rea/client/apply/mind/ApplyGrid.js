/**
 * @description 部门智能采购申请录入申请主列表
 * @author liuyj
 * @version 2020-12-15
 */
Ext.define('Shell.class.rea.client.apply.mind.ApplyGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyGrid',

	title: '智能采购申请列表',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**下拉状态默认值*/
	defaultStatusValue: "",
	
	/**用户UI配置Key*/
	userUIKey: 'apply.mind.ApplyGrid',
	/**用户UI配置Name*/
	userUIName: "智能采购申请列表",
	
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
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		if(userId && userId != -1) {
			if(me.defaultWhere) me.defaultWhere = '(' + me.defaultWhere + ') and ';
			me.defaultWhere += 'reabmsreqdoc.ApplyID=' + userId;
		}
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	}
});