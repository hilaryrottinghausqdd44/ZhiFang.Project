/**
 * 借款单申请基本信息查看表单
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.show.Form', {
	extend: 'Shell.ux.form.Panel',
	bodyPadding: '2px 2px 2px 2px',
	//labelStyle: 'color:black',//color:#00F;
	layout: {
		type: 'table',
		columns: 4
	},
	//行DIV框样式float:left;width:100%;
	rDivStyle: 'padding:2px;margin:5px 1;border:1px solid #5cb85c;border-radius:2px;',
	/**每个组件的默认属性*/
	defaults: {
		xtype: 'textfield',
		labelWidth: 78,
		width: 195,
		locked: true,
		border: 'border:none;',
		readOnly: true,
		fieldStyle: 'background:none;border:0;border-bottom:0px', // solid #036;
		labelAlign: 'right'
	},
	colspanWidth: 200,
	/**每个组件的默认属性*/
	defaultsFieldSet: {
		xtype: 'textfield',
		padding: '1px',
		labelWidth: 78,
		border: 'border:none;',
		readOnly: true,
		fieldStyle: 'background:none;border:0;border-bottom:0px', // solid #036;
		labelAlign: 'right'
	},
	border: false,
	formtype: "show",
	title: '借款单信息',
	width: 780,
	height: 465,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**获取数据服务路径(编辑时不需要更新总阅读数)*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillById?isPlanish=false',
	/**显示成功信息*/
	showSuccessInfo: false,
	autoScroll: true,
	PK: '',
	/**是否隐藏审核信息*/
	isHiddenReview: true,
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
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.colspanWidth = parseInt(me.width / 4) - 2;
		me.createTitleName("借款单");
		me.createLoanBillTypeName("借款类型");
		me.createLoanBillContentTypeName("内容类型");

		me.createClientName("客户名称");
		me.createContractName("合同名称");

		me.createComponeName("借款单位");
		me.createDeptName("借款部门");
		me.createApplyMan('借款人');
		me.createApplyDate("借款时间");

		me.createLoanBillAmountUpperCase("借款金额");
		me.createLoanBillAmount("￥");
		me.createReceiveTypeName("支付方式");

		me.createLoanBillMemo("借款事由");

		me.createReceiveBankInfo("备注说明");

		me.createReviewMan("上级领导");
		me.createReviewDate("审核时间");
		me.createReviewInfo("审核意见");
		me.createReviewFieldSet();

		me.createTwoReviewMan("行政助理");
		me.createTwoReviewDate("行政核对时间");
		me.createTwoReviewInfo("行政核对意见");
		me.createTwoReviewFieldSet();

		me.createThreeReviewMan("总经理");
		me.createThreeReviewDate("特殊审批时间");
		me.createThreeReviewInfo("特殊审批意见");
		me.createThreeReviewFieldSet();

		me.createFourReviewMan("账务人员");
		me.createFourReviewDate("账务复核时间");
		me.createFourReviewInfo("账务复核意见");
		me.createFourReviewFieldSet();

		me.createPayManName("出纳打款人");
		me.createPayDate("出纳打款时间");
		me.createPayDateInfo("出纳打款意见");
		me.createPayFieldSet();

		me.createReceiveManName("领款人");
		me.createReceiveDate("领款时间");
		me.createReceiveManInfo("领款意见");
		me.createReceiveFieldSet();
	},
	//显示用
	createTitleName: function(fieldLabel) {
		var me = this;
		me.TitleName = {
			xtype: 'label',
			fieldLabel: '', //style:'margin:auto;width:70%;',
			style: 'position:relative;left:450px;fontSize:26px;fontWeight:bold;color:#00F;',
			hideLabel: true,
			name: 'TitleName',
			text: fieldLabel,
			itemId: 'TitleName'
		};
	},

	createReviewFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[3];
		me.ReviewFieldSet = {
			xtype: 'fieldset',
			labelAlign: 'right',

			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'ReviewFieldSet',
			hidden: me.isHiddenReview,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.ReviewMan, me.ReviewDate, me.ReviewInfo]
		};
	},
	createReviewMan: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[3];
		me.ReviewMan = {
			fieldLabel: fieldLabel,
			name: 'ReviewMan',
			itemId: 'ReviewMan',
			hidden: me.isHiddenReview
		};
		me.ReviewMan.colspan = 1;
		me.ReviewMan.width = me.colspanWidth;
	},
	createReviewDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[3];
		me.ReviewDate = {
			fieldLabel: fieldLabel,
			name: 'ReviewDate',
			itemId: 'ReviewDate',
			hidden: me.isHiddenReview
		};
		me.ReviewDate.colspan = 1;
		me.ReviewDate.width = me.colspanWidth;
	},
	createReviewInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[3];
		me.ReviewInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'ReviewInfo',
			hidden: me.isHiddenReview
		};
		me.ReviewInfo.colspan = 2;
		me.ReviewInfo.width = me.colspanWidth * 2 - 10;
	},
	createTwoReviewFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[5];
		me.TwoReviewFieldSet = {
			xtype: 'fieldset',
			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'TwoReviewFieldSet',
			hidden: me.isHiddenTwoReview,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.TwoReviewMan, me.TwoReviewDate, me.TwoReviewInfo]
		};
	},
	createTwoReviewMan: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[5];
		me.TwoReviewMan = {
			fieldLabel: fieldLabel,
			name: 'TwoReviewMan',
			itemId: 'TwoReviewMan',
			hidden: me.isHiddenTwoReview
		};
		me.TwoReviewMan.colspan = 1;
		me.TwoReviewMan.width = me.colspanWidth;
	},
	createTwoReviewDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[5];
		me.TwoReviewDate = {
			fieldLabel: fieldLabel,
			name: 'TwoReviewDate',
			itemId: 'TwoReviewDate',
			hidden: me.isHiddenTwoReview
		};
		me.TwoReviewDate.colspan = 1;
		me.TwoReviewDate.width = me.colspanWidth;
	},
	createTwoReviewInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[5];
		me.TwoReviewInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'TwoReviewInfo',
			hidden: me.isHiddenTwoReview
		};
		me.ReviewInfo.colspan = 1;
		me.ReviewInfo.width = me.colspanWidth * 2 - 10;
	},

	createThreeReviewFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[7];
		me.ThreeReviewFieldSet = {
			xtype: 'fieldset',
			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'ThreeReviewFieldSet',
			hidden: me.isHiddenThreeReview,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.ThreeReviewMan, me.ThreeReviewDate, me.ThreeReviewInfo]
		};
	},
	createThreeReviewMan: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[7];
		me.ThreeReviewMan = {
			fieldLabel: fieldLabel,
			name: 'ThreeReviewMan',
			itemId: 'ThreeReviewMan',
			hidden: me.isHiddenThreeReview
		};
		me.ThreeReviewMan.colspan = 1;
		me.ThreeReviewMan.width = me.colspanWidth;
	},
	createThreeReviewDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[7];
		me.ThreeReviewDate = {
			fieldLabel: fieldLabel,
			name: 'ThreeReviewDate',
			hidden: me.isHiddenThreeReview
		};
		me.ThreeReviewDate.colspan = 1;
		me.ThreeReviewDate.width = me.colspanWidth;
	},
	createThreeReviewInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[7];
		me.ThreeReviewInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'ThreeReviewInfo',
			hidden: me.isHiddenThreeReview
		};
		me.ThreeReviewInfo.colspan = 2;
		me.ThreeReviewInfo.width = me.colspanWidth * 2 - 10;
	},

	createFourReviewFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[9];
		me.FourReviewFieldSet = {
			xtype: 'fieldset',
			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'FourReviewFieldSet',
			hidden: me.isHiddenFourReview,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.FourReviewMan, me.FourReviewDate, me.FourReviewInfo]
		};
	},
	createFourReviewMan: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[9];
		me.FourReviewMan = {
			fieldLabel: fieldLabel,
			name: 'FourReviewMan',
			hidden: me.isHiddenFourReview
		};
		me.FourReviewMan.colspan = 1;
		me.FourReviewMan.width = me.colspanWidth;
	},
	createFourReviewDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[9];
		me.FourReviewDate = {
			fieldLabel: fieldLabel,
			name: 'FourReviewDate',
			hidden: me.isHiddenFourReview
		};
		me.FourReviewDate.colspan = 1;
		me.FourReviewDate.width = me.colspanWidth;
	},
	createFourReviewInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[9];
		me.FourReviewInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'FourReviewInfo',
			hidden: me.isHiddenFourReview
		};
		me.FourReviewInfo.colspan = 2;
		me.FourReviewInfo.width = me.colspanWidth * 2 - 10;
	},

	createPayFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[11];
		me.PayFieldSet = {
			xtype: 'fieldset',
			labelAlign: 'right',
			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'PayFieldSet',
			hidden: me.isHiddenPay,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.PayManName, me.PayDate, me.PayDateInfo]
		};
	},
	createPayManName: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[11];
		me.PayManName = {
			fieldLabel: fieldLabel,
			name: 'PayManName',
			hidden: me.isHiddenPay
		};
		me.PayManName.colspan = 1;
		me.PayManName.width = me.colspanWidth;
	},
	createPayDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[11];
		me.PayDate = {
			fieldLabel: fieldLabel,
			name: 'PayDate',
			hidden: me.isHiddenPay
		};
		me.PayDate.colspan = 1;
		me.PayDate.width = me.colspanWidth;
	},
	createPayDateInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[11];
		me.PayDateInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'PayDateInfo',
			hidden: me.isHiddenPay
		};
		me.PayDateInfo.colspan = 2;
		me.PayDateInfo.width = me.colspanWidth * 2 - 10;
	},

	createReceiveFieldSet: function() {
		var me = this;
		var bcolor = me.StatusBGColorEnum[12];
		me.ReceiveFieldSet = {
			xtype: 'fieldset',
			layout: {
				type: 'table',
				columns: 4
			},
			style: me.rDivStyle,
			name: 'ReceiveFieldSet',
			hidden: me.isHiddenReceive,
			defaultType: 'textfield',
			defaults: me.defaultsFieldSet,
			items: [me.ReceiveManName, me.ReceiveDate, me.ReceiveManInfo]
		};
	},
	createReceiveManName: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[12];
		me.ReceiveManName = {
			fieldLabel: fieldLabel,
			name: 'ReceiveManName',
			hidden: me.isHiddenReceive
		};
		me.ReceiveManName.colspan = 1;
		me.ReceiveManName.width = me.colspanWidth;
	},
	createReceiveDate: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[12];
		me.ReceiveDate = {
			fieldLabel: fieldLabel,
			name: 'ReceiveDate',
			hidden: me.isHiddenReceive
		};
		me.ReceiveDate.colspan = 1;
		me.ReceiveDate.width = me.colspanWidth;
	},
	createReceiveManInfo: function(fieldLabel) {
		var me = this;
		var bcolor = me.StatusBGColorEnum[12];
		me.ReceiveManInfo = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'ReceiveManInfo',
			hidden: me.isHiddenReceive
		};
		me.ReceiveManInfo.colspan = 2;
		me.ReceiveManInfo.width = me.colspanWidth * 2 - 10;
	},
	createLoanBillTypeName: function(fieldLabel) {
		var me = this;
		me.LoanBillTypeName = {
			labelWidth: 60,
			fieldLabel: fieldLabel,
			name: 'LoanBillTypeName',
			itemId: 'LoanBillTypeName'
		};
	},
	createLoanBillContentTypeName: function(fieldLabel) {
		var me = this;
		me.LoanBillContentTypeName = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			name: 'LoanBillContentTypeName',
			itemId: 'LoanBillContentTypeName'

		};
	},
	createContractName: function(fieldLabel) {
		var me = this;
		me.ContractName = {
			fieldLabel: fieldLabel,
			name: 'ContractName',
			itemId: 'ContractName'

		};
	},
	createClientName: function(fieldLabel) {
		var me = this;
		me.ClientName = {
			fieldLabel: fieldLabel,
			name: 'ClientName',
			itemId: 'ClientName'

		};
	},
	createComponeName: function(fieldLabel) {
		var me = this;
		//字典类型为本公司名称
		me.ComponeName = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			name: 'ComponeName',
			itemId: 'ComponeName'
		};
	},

	createDeptName: function(fieldLabel) {
		var me = this;
		me.DeptName = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			name: 'DeptName',
			itemId: 'DeptName'
		};
	},
	createApplyMan: function(fieldLabel) {
		var me = this;
		me.ApplyMan = {
			fieldLabel: fieldLabel,
			name: 'ApplyMan',
			itemId: 'ApplyMan'
		};
	},
	/**申请时间*/
	createApplyDate: function(fieldLabel) {
		var me = this;
		var serverTime = JcallShell.System.Date.getDate();
		me.ApplyDate = {
			fieldLabel: fieldLabel,
			name: 'ApplyDate',
			itemId: 'ApplyDate',
			format: 'Y-m-d'
		};
	},
	/**借款说明*/
	createLoanBillMemo: function(fieldLabel) {
		var me = this;
		me.LoanBillMemo = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			xtype: 'displayfield',
			name: 'LoanBillMemo'
		};
	},
	//借款金额大写
	createLoanBillAmountUpperCase: function(fieldLabel) {
		var me = this;
		me.LoanBillAmountUpperCase = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			name: 'LoanBillAmountUpperCase',
			itemId: 'LoanBillAmountUpperCase'
		};
	},
	createLoanBillAmount: function(fieldLabel) {
		var me = this;
		me.LoanBillAmount = {
			fieldLabel: fieldLabel,
			name: 'LoanBillAmount',
			itemId: 'LoanBillAmount',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue != null && newValue != "") {
						me.loanBillAmountUpperCaseSetValue(com, newValue, oldValue, eOpts);
					} else {
						var com = me.getComponent('LoanBillAmountUpperCase');
						com.setValue("");
					}
				}
			}
		};
	},
	loanBillAmountUpperCaseSetValue: function(com, newValue, oldValue, eOpts) {
		var me = this;
		var money = JcallShell.Number.getMoney(newValue);
		var com = me.getComponent('LoanBillAmountUpperCase');
		com.setValue(money);
	},
	createReceiveTypeName: function(fieldLabel) {
		var me = this;
		me.ReceiveTypeName = {
			fieldLabel: fieldLabel,
			name: 'ReceiveTypeName',
			itemId: 'ReceiveTypeName'
		};
	},
	createReceiveBankInfo: function(fieldLabel) {
		var me = this;
		me.ReceiveBankInfo = {
			fieldLabel: fieldLabel,
			labelWidth: 60,
			xtype: 'displayfield',
			name: 'ReceiveBankInfo',
			itemId: 'ReceiveBankInfo'
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		me.colspanWidth = parseInt(me.width / 4) - 5;

		me.TitleName.colspan = 4;
		me.TitleName.width = 120;
		items.push(me.TitleName);

		me.LoanBillTypeName.colspan = 1;
		me.LoanBillTypeName.width = me.colspanWidth * me.LoanBillTypeName.colspan;
		items.push(me.LoanBillTypeName);

		me.LoanBillContentTypeName.colspan = 1;
		me.LoanBillContentTypeName.width = me.colspanWidth * me.LoanBillContentTypeName.colspan;
		items.push(me.LoanBillContentTypeName);
		
		me.ClientName.colspan = 1;
		me.ClientName.width = me.colspanWidth * me.ClientName.colspan;
		items.push(me.ClientName);
		
		me.ContractName.colspan = 1;
		me.ContractName.width = me.colspanWidth * me.ContractName.colspan;
		items.push(me.ContractName);

		//第三行
		me.ComponeName.colspan = 1;
		me.ComponeName.width = 240; // me.colspanWidth * me.ComponeName.colspan;
		items.push(me.ComponeName);

		me.DeptName.colspan = 1;
		me.DeptName.width = me.colspanWidth;
		items.push(me.DeptName);

		me.ApplyMan.colspan = 1;
		me.ApplyMan.width = me.colspanWidth;
		items.push(me.ApplyMan);

		me.ApplyDate.colspan = 1;
		me.ApplyDate.width = me.colspanWidth;
		items.push(me.ApplyDate);

		me.LoanBillAmountUpperCase.colspan = 2;
		me.LoanBillAmountUpperCase.width = me.colspanWidth * me.LoanBillAmountUpperCase.colspan;
		items.push(me.LoanBillAmountUpperCase);

		me.LoanBillAmount.colspan = 1;
		me.LoanBillAmount.width = me.colspanWidth;
		items.push(me.LoanBillAmount);

		//支付方式
		me.ReceiveTypeName.colspan = 1;
		me.ReceiveTypeName.width = me.colspanWidth;
		items.push(me.ReceiveTypeName);

		//
		me.LoanBillMemo.colspan = 4;
		me.LoanBillMemo.width = me.width - 20;
		items.push(me.LoanBillMemo);

		me.ReceiveBankInfo.colspan = 4;
		me.ReceiveBankInfo.width = me.width - 20;
		items.push(me.ReceiveBankInfo);

		if(!me.isHiddenReview) {
			me.ReviewFieldSet.colspan = 4;
			me.ReviewFieldSet.width = me.width - 20;
			items.push(me.ReviewFieldSet);
		}
		if(!me.isHiddenTwoReview) {
			me.TwoReviewFieldSet.colspan = 4;
			me.TwoReviewFieldSet.width = me.width - 20;
			items.push(me.TwoReviewFieldSet);
		}
		if(!me.isHiddenThreeReview) {
			me.ThreeReviewFieldSet.colspan = 4;
			me.ThreeReviewFieldSet.width = me.width - 20;
			items.push(me.ThreeReviewFieldSet);
		}

		if(!me.isHiddenFourReview) {
			me.FourReviewFieldSet.colspan = 4;
			me.FourReviewFieldSet.width = me.width - 20;
			items.push(me.FourReviewFieldSet);
		}

		if(!me.isHiddenPay) {
			me.PayFieldSet.colspan = 4;
			me.PayFieldSet.width = me.width - 20;
			items.push(me.PayFieldSet);
		}

		if(!me.isHiddenReceive) {
			me.ReceiveFieldSet.colspan = 4;
			me.ReceiveFieldSet.width = me.width - 20;
			items.push(me.ReceiveFieldSet);
		}
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		//data.ApplyDate = JShell.Date.getDate(data.ApplyDate);
		if(me.formtype == "edit" && data.LoanBillAmount != null) {
			var money = JcallShell.Number.getMoney(parseFloat(data.LoanBillAmount));
			me.LoanBillAmountUpperCase.value = money;
		}
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		return items;
	}
});