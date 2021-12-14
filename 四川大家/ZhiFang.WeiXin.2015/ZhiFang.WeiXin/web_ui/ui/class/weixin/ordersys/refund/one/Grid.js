/**
 * 退款申请一审(一审)
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.weixin.ordersys.refund.one.Grid', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.Grid',
	title: '退款申请一审',
	/**默认员工赋值*/
	hasDefaultUser: false,
	/**是否显示打回操作列*/
	hiddenRetract: true,
	initComponent: function() {
		var me = this;
		me.defaultWhere = "(Status=1 and RefundOneReviewManID is null) or ( RefundOneReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + ")";
		me.callParent(arguments);
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "1": //申请
				me.openEditTabPanel(record, false);
				break;
			case "4": //一审退回
				me.openEditTabPanel(record, false);
				break;
			case "7": //二审退回
				me.openEditTabPanel(record, false);
				break;
			default:
				me.openShowTabPanel(record);
				break;
		}
	},
	onShowClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.openShowTabPanel(records[0]);
	},
	/**打开退款申请单审核应用*/
	openEditTabPanel: function(record, hiddenPass) {
		var me = this;
		var ApplyManID = "";
		var id = "";
		if(record != null) {
			id = record.get('Id');
			ApplyManID = record.get('ApplyManID');
		}
		var maxWidth = document.body.clientWidth * 0.62;
		var height = document.body.clientHeight * 0.86;
		if(hiddenPass == null) {
			hiddenPass = false;
		}
		var minWidth = 620;
		maxWidth = maxWidth <= minWidth ? minWidth : maxWidth;
		var config = {
			showSuccessInfo: false,
			PK: id,
			hasOperation: true,
			SUB_WIN_NO: '1',
			height: height,
			width: maxWidth,
			RefundFormCode:record.get('MRefundFormCode'),
			resizable: true,
			title: "退款申请一审",
			formtype: 'edit',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			hiddenPass: hiddenPass,
			ApplyManID: ApplyManID,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.refund.one.EditTabPanel', config).show();
	}
});