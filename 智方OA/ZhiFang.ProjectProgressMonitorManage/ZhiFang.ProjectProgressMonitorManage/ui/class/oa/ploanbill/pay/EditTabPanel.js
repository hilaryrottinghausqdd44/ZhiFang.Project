/**
 * 借款单借款检查并打款
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.pay.EditTabPanel', {
	extend: 'Shell.class.oa.ploanbill.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '出纳检查并打款',
	hiddenRetract: true,
	hiddenSpecially: true,
	/**通过按钮显示文字*/
	btnPassText: "出纳检查并打款",
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: false,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: false,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: false,
	/**是否隐藏打款信息*/
	isHiddenPay: false,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	hiddenPayDate: false,
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Status: status,
			PayDateInfo: me.entityValues.ViewInfo,
			OperationMemo: operationMemo
		};
		var params = {
			entity: entity,
			fields: "Id,Status"
		};
		return params;
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "出纳检查并打款";
	},
	/**打款确认处理方法*/
	onPassClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('11', "出纳检查并打款");
				var serverTime = JcallShell.System.Date.getDate();
				var payDate = JShell.Date.toServerDate(serverTime);
				if(me.entityValues && me.entityValues.PayDate)
					payDate = me.entityValues.PayDate
				params.entity.PayDate = payDate;
				params.fields = params.fields + ",PayDate";
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("检查并打款确认意见", callback);

	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('10', "出纳检查并打款退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("检查并打款退回意见", callback);
	}
});