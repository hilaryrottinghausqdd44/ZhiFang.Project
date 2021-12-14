/**
 * 退款申请(商务助理)
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.apply.Grid', {
	extend: 'Shell.class.weixin.ordersys.userorder.basic.Grid',
	title: '退款申请',
	hasAdd: false,
	hasShow: false,
	hasEdit: false,
	hasDel: false,
	hasRefresh: true,
	hiddenRetract: true,
	removeApply: false,
	/*日期范围类型默认值**/
	defaultDateTypeValue: 'RefundApplyTime',
	/*是否显示启/禁用列**/
	isShowIsUseColumn: false,
	isRemoveApplyManID: true,

	SearchTypeList: [
		["", "不过滤"],
		["RefundApplyManID", "申请人"],
		["RefundOneReviewManID", "退款处理人"],
		["RefundTwoReviewManID", "退款审批人"],
		["RefundThreeReviewManID", "退款发放人"]
	],
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.checkSatusApply(records[0]);
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
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		me.checkSatusApply(record);
	},
	checkSatusApply: function(record) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "2": //已交费
				me.openApplyTabPanel(record);
				break;
			case "3": //部分使用
				me.openApplyTabPanel(record);
				break;
			case "4": //部分使用
				me.openApplyTabPanel(record);
				break;
			default:
				//me.openShowTabPanel(record);
				break;
		}
	},
	/**打开退款单申请应用*/
	openApplyTabPanel: function(record) {
		var me = this;
		var UOFID = "",
			id = '',
			Status = 1;
		var hiddenButtonRetract = true;
		if(record != null) {
			UOFID = record.get('Id');
			Status = record.get('Status').toString();
		}
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '1',
			/**用户订单ID*/
			UOFID: UOFID,
			Status: Status,
			resizable: true,
			title: "退款申请",
			formtype: 'add',
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id && id != null && id != "") {
			config.formtype = 'edit';
			config.PK = id;
			config.hasOperation = true;
			title: "编辑退款申请";
		}
		JShell.Win.open('Shell.class.weixin.ordersys.refund.apply.TabPanel', config).show();
	},
	//退款申请
	onRefundClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.checkSatusApply(records[0]);
	}
});