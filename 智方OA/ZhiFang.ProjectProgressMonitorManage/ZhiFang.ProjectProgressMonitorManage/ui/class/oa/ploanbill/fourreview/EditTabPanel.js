/**
 * 借款单借款复核的流程
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.fourreview.EditTabPanel', {
	extend: 'Shell.class.oa.ploanbill.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '账务复核',
	/**通过按钮显示文字*/
	btnPassText: "账务复核通过",
	/**是否隐藏退回按钮*/
	hiddenSpecially: true,
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: false,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: false,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: false,
	/**是否隐藏打款信息*/
	isHiddenPay: true,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "账务复核";
	},
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Status: status,
			FourReviewInfo: me.entityValues.ViewInfo,
			OperationMemo: operationMemo
		};
		var params = {
			entity: entity,
			fields: "Id,Status"
		};
		return params;
	},
	/**通过处理方法*/
	onPassClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('9', "账务复核通过");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("账务复核通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('10', "账务复核退回");
				me.updateStatus(params);
			}
		}
		me.openViewInfoForm("账务复核退回意见", callback);

	}
});