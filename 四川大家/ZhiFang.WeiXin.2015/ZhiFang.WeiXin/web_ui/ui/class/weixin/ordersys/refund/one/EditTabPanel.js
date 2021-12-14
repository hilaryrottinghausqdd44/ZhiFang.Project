/**
 *预览退款申请一审
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.one.EditTabPanel', {
	extend: 'Shell.class.weixin.ordersys.refund.basic.EditTabPanel',
	header: true,
	activeTab: 0,
	title: '退款处理',
	/**通过按钮显示文字*/
	btnPassText: "退款处理通过",
	RefundFormCode:null,
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormOneReview',
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "退款处理";
	},
	getSaveParams: function(Result, operationMemo) {
		var me = this;
		var entity = {
			Id: me.PK,
			Result: Result,
			RefundFormCode:me.RefundFormCode,
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
				var params = me.getSaveParams(1, "退款处理通过");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("退款处理通过意见", callback);
	},
	/**@overwrite 退回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var callback = function() {
			if(me.isSave == true) {
				var params = me.getSaveParams(0, "退款处理退回");
				me.updateStatus(params);
			}
		};
		me.openViewInfoForm("退款处理退回意见", callback);
	}
});