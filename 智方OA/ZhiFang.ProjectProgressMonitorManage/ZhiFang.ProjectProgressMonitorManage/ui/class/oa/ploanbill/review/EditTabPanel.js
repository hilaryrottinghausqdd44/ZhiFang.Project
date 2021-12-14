/**
 * 借款单借款审核的流程
 * @author longfc
 * @version 2016-11-11
 */
Ext.define('Shell.class.oa.ploanbill.review.EditTabPanel', {
	extend: 'Shell.class.oa.ploanbill.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '借款审核',
	/**是否隐藏区域内审核按钮*/
	hiddenSpecially: true,
	/**通过按钮显示文字*/
	btnPassText: "审核通过",
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: true,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: true,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: true,
	/**是否隐藏打款信息*/
	isHiddenPay: true,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	FormAppClass: 'Shell.class.oa.ploanbill.review.FormApp',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "借款审核";
	},
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var formApp = me.getComponent("FormApp");
		var entity = formApp.getEditParams();
		entity.entity.Id = me.PK;
		entity.entity.Status = status;
		entity.entity.ReviewInfo = me.entityValues.ViewInfo;
		entity.entity.OperationMemo = operationMemo;
		var params = {
			entity: entity.entity,
			fields: entity.fields
		};
		//		var entity = {
		//			Id: me.PK,
		//			Status: status,
		//			ReviewInfo: me.entityValues.ViewInfo,
		//			OperationMemo: operationMemo
		//		};
		//		var params = {
		//			entity: entityentity,
		//			fields: "Id,Status"
		//		};
		return params;
	},
	/**通过处理方法*/
	onPassClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams("3", "借款审核通过");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("借款审核通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams("4", "借款审核退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("借款审核退回意见", callback);
	}
});