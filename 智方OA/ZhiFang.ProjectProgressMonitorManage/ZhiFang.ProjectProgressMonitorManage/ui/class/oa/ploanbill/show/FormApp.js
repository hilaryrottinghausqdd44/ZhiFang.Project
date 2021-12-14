/**
 * 借款单信息
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.show.FormApp', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '借款单信息',
	Status: 1,
	OperationMemo: "",
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
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
	isHiddenPay: false,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	/**申请人ID*/
	ApplyManID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		//var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		me.pempfinanceaccountForm = Ext.create('Shell.class.sysbase.user.pempfinanceaccount.show.Form', {
			region: 'west',
			header: false,
			split: true,
			itemId: 'pempfinanceaccountForm',
			formtype: 'show',
			border: false,
			title: "财务账户信息",
			height: me.height,
			width: 165,
			defaultLoad: true,
			EmpID: me.ApplyManID,
		});
		me.basicForm = Ext.create('Shell.class.oa.ploanbill.show.Form', {
			region: 'center',
			header: false,
			split: true,
			itemId: 'basicForm',
			formtype: me.formtype,
			border: false,
			title: me.basicFormTitle,
			width: me.width - me.pempfinanceaccountForm.width,
			title: me.basicFormTitle,
			height: me.height,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			/**是否隐藏审核信息*/
			isHiddenReview: me.isHiddenReview,
			/**是否隐藏借款核对信息*/
			isHiddenTwoReview: me.isHiddenTwoReview,
			/**是否隐藏特殊审批信息*/
			isHiddenThreeReview: me.isHiddenThreeReview,
			/**是否隐藏借款复核信息*/
			isHiddenFourReview: me.isHiddenFourReview,
			/**是否隐藏打款信息*/
			isHiddenPay: me.isHiddenPay,
			/**是否隐藏领款信息*/
			isHiddenReceive: me.isHiddenReceive,
			PK: me.PK
		});
		return [me.pempfinanceaccountForm, me.basicForm];
	}
});