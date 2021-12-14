/**
 *预览退款申请二审
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.two.Grid', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.Grid',
	title: '退款审批',

	hasShow: false,
	hiddenRetract: true,
	/**默认员工赋值*/
	hasDefaultUser: false,
	
	initComponent: function() {
		var me = this;
		//二审默认条件(二审人=登录者) or (二审人=null and 状态=一审通过)
		me.defaultWhere = "(RefundTwoReviewManID=null and Status=3) or (RefundTwoReviewManID=" + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + ")";
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		if(!tempStatus) return [];
		var itemArr = [];
		itemArr.push(tempStatus[0]);
		if(me.removeApply)
			itemArr.push(tempStatus[1]);
		//一审中
		itemArr.push(tempStatus[2]);
		//一审退回
		itemArr.push(tempStatus[4]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempStatus, itemArr[index]);
		});
		return tempStatus;
	},
	onItemDblClick: function(grid, record, item, index, e, eOpts) {
		var me = this;
		var status = record.get('Status').toString();
		switch(status) {
			case "3": //一审通过
				me.openEditTabPanel(record, false);
				break;
			case "8": //财务退回
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
	/**打开借款单审核应用*/
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
			SUB_WIN_NO: '2',
			hasOperation: true,
			height: height,
			width: maxWidth,
			resizable: true,
			title: "退款申请二审",
			RefundFormCode:record.get('MRefundFormCode'),
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
		JShell.Win.open('Shell.class.weixin.ordersys.refund.two.EditTabPanel', config).show();
	}
});