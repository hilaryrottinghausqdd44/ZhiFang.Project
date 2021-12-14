/**
 *预览退款申请二审
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.two.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '退款审批',
	/**通过按钮显示文字*/
	btnPassText: "退款审批通过",
	hiddenSpecially: true,
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: false,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: true,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: true,
	/**是否隐藏打款信息*/
	isHiddenPay: true,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
		/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormTwoReview',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "退款审批";
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
				var params = me.getSaveParams(1, "退款审批通过");
//				params.fields = params.fields;
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("退款审批通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams(0, "退款审批退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("退款审批退回意见", callback);
	}
});