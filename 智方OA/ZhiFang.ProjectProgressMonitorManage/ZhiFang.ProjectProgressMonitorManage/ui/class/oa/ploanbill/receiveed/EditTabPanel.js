/**
 * 领款人确认
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.receiveed.EditTabPanel', {
	extend: 'Shell.class.oa.ploanbill.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '领款确认',
	hiddenRetract: true,
	hiddenSpecially: true,
	/**通过按钮显示文字*/
	btnPassText: "审核通过",
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
	isHiddenReceive: false,
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "领款确认";
	},
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Status: status,
			ReceiveManInfo: me.entityValues.ViewInfo,
			OperationMemo: operationMemo
		};
		var params = {
			entity: entity,
			fields: "Id,Status"
		};
		return params;
	},
	/**领款人确认处理方法*/
	onPassClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('12', "领款确认");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("领款确认意见",callback);
	}
});