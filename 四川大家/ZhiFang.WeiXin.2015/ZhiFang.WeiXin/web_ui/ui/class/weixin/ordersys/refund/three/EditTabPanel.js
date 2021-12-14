/**
 *预览退款申请二审
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.three.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '退款发放',
	/**通过按钮显示文字*/
	btnPassText: "退款发放通过",
	RefundFormThreeReviewVO:null,
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormThreeReview',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "退款发放";
	},
	getSaveParams: function(Result, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Result: Result,
			RefundFormCode: me.RefundFormCode,
			Reason: me.entityValues.ViewInfo
		};
		var params = {
			entity: entity
		};
		return params;
	},
	/**通过处理方法*/
	onPassClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				me.RefundFormThreeReviewVO.Id = me.PK;
				me.RefundFormThreeReviewVO.Result = true;
				me.RefundFormThreeReviewVO.RefundFormCode = me.RefundFormCode;
				var params = {
					entity: me.RefundFormThreeReviewVO
				};
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("财务打款通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams(true, "财务退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("财务退回意见", callback);
	},
	openViewInfoForm: function(title, callback) {
		var me = this;
		me.isSave = false;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '3',
			resizable: false,
			title: title,
			formtype: 'edit',
			PK: me.PK,
			width: 630,
			height: 340,
			zindex: 10,
			/**带功能按钮栏*/
			hasButtontoolbar: true,
			/**是否启用保存按钮*/
			hasSave: true,
			/**是否重置按钮*/
			hasReset: true,
			listeners: {
				save: function(win,params) {
					me.RefundFormThreeReviewVO =params.entity;
					me.isSave = true;
					me.ViewInfoWin = win;
					if(callback != null) {
						callback();
					}
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.refund.three.Form', config).show();
	}
});