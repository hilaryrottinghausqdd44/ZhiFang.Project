/**
 * 借款单特殊审批的流程
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.threereview.EditTabPanel', {
	extend: 'Shell.class.oa.ploanbill.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '特殊审批',
	/**通过按钮显示文字*/
	btnPassText: "特殊审批通过",
	hiddenSpecially: true,
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: false,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: false,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: true,
	/**是否隐藏打款信息*/
	isHiddenPay: true,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "特殊审批";
	},
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Status: status,
			ThreeReviewInfo:me.entityValues.ViewInfo,
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
				var params = me.getSaveParams('7', "特殊审批通过");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("特殊审批通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams('8', "特殊审批退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("特殊审批退回意见", callback);
	}
});