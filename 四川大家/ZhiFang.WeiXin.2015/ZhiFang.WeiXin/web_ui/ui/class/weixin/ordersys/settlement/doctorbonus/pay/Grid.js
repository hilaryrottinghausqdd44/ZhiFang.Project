/**
 * 检查并打款
 * @author longfc
 * @version 2017-03-02
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.pay.Grid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid',
	title: '检查并打款',

	EditTabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.pay.EditTabPanel',
	initComponent: function() {
		var me = this;
		me.initDefaultWhere();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(1, 0, {
			xtype: 'actioncolumn',
			text: '发放',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-check hand',
				tooltip: '发放',
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					me.openTabPanel(record);
				}
			}]
		});
		return columns
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		me.openTabPanel(record);
	},
	openTabPanel: function(record) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "7": //二审通过
				me.openEditTabPanel(record, false);
				break;
			case "9": //检验并打款
				me.openEditTabPanel(record, false);
				break;
			case "11": //打款异常
				me.openEditTabPanel(record, false);
				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = "((Status=7 or Status=9 or Status=11) and BonusThreeReviewManID = null) or ( BonusThreeReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + ")";
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);
		//申请
		if(tempList[2]) itemArr.push(tempList[2]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	}
});