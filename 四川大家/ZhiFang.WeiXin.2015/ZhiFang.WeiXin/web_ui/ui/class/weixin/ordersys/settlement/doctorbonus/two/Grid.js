/**
 * 奖金结算二审
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.two.Grid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid',
	title: '奖金结算二审',

	EditTabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.two.EditTabPanel',
	initComponent: function() {
		var me = this;
		me.addEvents('onPassClick');
		me.addEvents('onRetractClick');
		me.initDefaultWhere();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createDockedItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		items.push(me.createToolbarItems5());
		return items;
	},
	/**审核通过或不通过的按钮工具栏*/
	createToolbarItems5: function() {
		var me = this,
			items = [{
				xtype: 'button',
				itemId: 'btnPass',
				iconCls: 'button-check',				
				text: "二审通过",
				tooltip: '二审通过',
				handler: function() {
					var records = me.getSelectionModel().getSelection();
					if(!records || records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					me.fireEvent('onPassClick', me, records[0]);
				}
			}, {
				xtype: 'button',
				itemId: 'btnRetract',
				iconCls: 'button-save',
				style:{
					marginLeft:'10px'
				},
				text: "二审退回",
				tooltip: '二审退回',
				handler: function() {
					var records = me.getSelectionModel().getSelection();
					if(!records || records.length != 1) {
						JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
						return;
					}
					me.fireEvent('onRetractClick', me, records[0]);
				}
			}];
		var toolbarItems5 = {
			xtype: 'toolbar',
			dock: 'top',
			//border:false,
			itemId: 'toolbarItems5',
			items: items
		};
		return toolbarItems5;
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = "(Status=4 and BonusTwoReviewManID = null) or ( BonusTwoReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + ")";
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "4": //一审通过
				me.openEditTabPanel(record, false);
				break;
//			case "8": //二审退回
//				me.openEditTabPanel(record, false);
//				break;
			case "10": //检查并打款退回
				me.openEditTabPanel(record, false);
				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
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