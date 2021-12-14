/**
 * 借款单申请表单
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.apply.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	labelStyle: 'color:black', //fontWeight:bold;#00F;
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 160,
		labelAlign: 'right'
	},
	border: false,
	formtype: "add",
	title: '借款申请',
	width: 980,
	height: 465,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**获取数据服务路径(编辑时不需要更新总阅读数)*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillById?isPlanish=false',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPLoanBill',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBillByField',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,

	autoScroll: true,
	PK: '',
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	dafultReceiveTypeCName: '',
	/**是否隐藏员工财务账户信息*/
	hiddenEmpFinanceAccount: true,
	/*合同选择的默认条件(依选择的客户进行过滤)**/
	contractExternalWhere: '',
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
		me.createTitleName("借款单");

		me.createLoanBillTypeName("借款类型");
		me.createLoanBillContentTypeName("内容类型");
		me.createClientName("客户名称");
		me.createContractName("合同");

		me.createComponeName("借款单位");
		me.createDeptName("借款部门");
		me.createApplyMan('借款人');
		me.createApplyDate("借款时间");

		me.createPEmpFinanceAccount_OneceLoanUpperAmount("单笔借款上限");
		me.createLoanUpperAmount("借款上限");
		me.createPEmpFinanceAccount_LoanAmount("借款总额");
		me.createPEmpFinanceAccount_UnRepaymentAmount("待还额度");

		me.createLoanBillAmountUpperCase("借款金额");
		me.createLoanBillAmount("￥");
		me.createReceiveTypeID("领款方式");

		me.createLoanBillMemo("借款事由");

		me.createReceiveBankInfo("备注说明");
	},
	//显示用
	createTitleName: function(fieldLabel) {
		var me = this;
		me.TitleName = {
			xtype: 'label',
			fieldLabel: '', //
			style: 'position:relative;left:420px;fontSize:26px;fontWeight:bold;color:#00F;',
			hideLabel: true,
			name: 'TitleName',
			text: fieldLabel,
			itemId: 'TitleName'
		};
	},
	createClientName: function(fieldLabel) {
		var me = this;
		me.ClientName = {
			fieldLabel: fieldLabel,
			name: 'ClientName',
			itemId: 'ClientName',
			allowBlank: true,
			emptyText: "客户名称",
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.oa.pgm.basic.PClientCheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 420,
				defaultLoad: true
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('ClientName');
					var Id = me.getComponent('ClientID');
					CName.setValue(record ? record.get('PClient_Name') : '');
					Id.setValue(record ? record.get('PClient_Id') : '');

					//客户选择改变后,合同信息先清空
					me.contractExternalWhere = (record ? "pcontract.PClientID=" + record.get('PClient_Id') : '');
					var ContractID = me.getComponent('ContractID');
					var ContractName = me.getComponent('ContractName');
					ContractID.setValue("");
					ContractName.setValue("");

					ContractName.classConfig.defaultWhere = me.contractExternalWhere;
					ContractName.changeClassConfig(ContractName.classConfig);
					p.close();
				}
			}
		};
	},

	createContractName: function(fieldLabel) {
		var me = this;
		me.ContractName = {
			fieldLabel: fieldLabel,
			name: 'ContractName',
			itemId: 'ContractName',
			allowBlank: true,
			emptyText: "合同名称",
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.wfm.business.contract.CheckGrid',
			classConfig: {
				title: fieldLabel,
				defaultLoad: true,
				externalWhere: me.contractExternalWhere
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('ContractName');
					var Id = me.getComponent('ContractID');
					CName.setValue(record ? record.get('PContract_Name') : '');
					Id.setValue(record ? record.get('PContract_Id') : '');
					p.close();
				}
			}
		};
	},
	createLoanBillTypeName: function(fieldLabel) {
		var me = this;
		//字典类型为借款类型
		me.LoanBillTypeName = {
			fieldLabel: fieldLabel,
			name: 'LoanBillTypeName',
			itemId: 'LoanBillTypeName',
			allowBlank: false,
			emptyText: "借款类型",
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 320,
				defaultLoad: true,
				defaultWhere: "pdict.PDictType.Id=4641331093809308015"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('LoanBillTypeName');
					var Id = me.getComponent('LoanBillTypeID');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			}
		};
	},
	createLoanBillContentTypeName: function(fieldLabel) {
		var me = this;
		//字典类型为借款内容类型
		me.LoanBillContentTypeName = {
			fieldLabel: fieldLabel,
			name: 'LoanBillContentTypeName',
			itemId: 'LoanBillContentTypeName',
			allowBlank: true,
			emptyText: "借款内容类型",
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 320,
				defaultLoad: true,
				defaultWhere: "pdict.PDictType.Id=4808477805327937637"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('LoanBillContentTypeName');
					var Id = me.getComponent('LoanBillContentTypeID');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');

					p.close();
				}
			}
		};
	},

	createComponeName: function(fieldLabel) {
		var me = this;
		//字典类型为本公司名称
		me.ComponeName = {
			fieldLabel: fieldLabel,
			name: 'ComponeName',
			itemId: 'ComponeName',
			allowBlank: false,
			emptyText: "请选择员工所属的公司",
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.sysbase.pdict.CheckGrid',
			classConfig: {
				title: fieldLabel,
				height: 320,
				defaultLoad: true,
				defaultWhere: "pdict.PDictType.Id=4991808299988371973"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('ComponeName');
					var Id = me.getComponent('ComponeID');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');

					p.close();
				}
			}
		};
	},

	createDeptName: function(fieldLabel) {
		var me = this;
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		me.DeptName = {
			fieldLabel: fieldLabel,
			name: 'DeptName',
			itemId: 'DeptName',
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			className: 'Shell.class.sysbase.org.CheckTree',
			value: (me.formtype == "add" ? DEPTNAME : ""),
			classConfig: {
				title: fieldLabel,
				height: 420,
				defaultLoad: true
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('DeptName');
					var Id = me.getComponent('DeptID');
					CName.setValue(record ? record.get('text') : '');
					Id.setValue(record ? record.get('tid') : '');
					p.close();
				}
			}
		};
	},
	createApplyMan: function(fieldLabel) {
		var me = this;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		me.ApplyMan = {
			fieldLabel: fieldLabel,
			name: 'ApplyMan',
			itemId: 'ApplyMan',
			value: (me.formtype == "add" ? userName : ""),
			xtype: 'uxCheckTrigger',
			labelStyle: me.labelStyle,
			labelWidth: 120,
			className: 'Shell.class.sysbase.user.CheckApp',
			classConfig: {
				title: fieldLabel,
				height: 420,
				defaultLoad: true,
				defaultWhere: ""
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('ApplyMan');
					var Id = me.getComponent('ApplyManID');
					CName.setValue(record ? record.get('HREmployee_CName') : '');
					Id.setValue(record ? record.get('HREmployee_Id') : '');

					var DeptCName = me.getComponent('DeptName');
					var DeptId = me.getComponent('DeptID');

					DeptCName.setValue(record ? record.get('HREmployee_HRDept_CName') : '');
					DeptId.setValue(record ? record.get('HREmployee_HRDept_Id') : '');
					p.close();
				}
			}
		};
	},
	/**申请时间*/
	createApplyDate: function(fieldLabel) {
		var me = this;
		var serverTime = JcallShell.System.Date.getDate();
		me.ApplyDate = {
			fieldLabel: fieldLabel,
			labelStyle: me.labelStyle,
			labelWidth: 110,
			name: 'ApplyDate',
			itemId: 'ApplyDate',
			xtype: 'datefield',
			value: (me.formtype == "add" ? serverTime : ""),
			format: 'Y-m-d'
		};
	},
	/**借款说明*/
	createLoanBillMemo: function(fieldLabel) {
		var me = this;
		me.LoanBillMemo = {
			fieldLabel: fieldLabel,
			xtype: 'textarea',
			border: false,
			labelStyle: me.labelStyle,
			name: 'LoanBillMemo',
			minHeight: 20,
			height: 50,
			maxLengthText: "借款说明",
			style: {
				marginBottom: '2px'
			}
		};
	},
	//借款金额大写
	createLoanBillAmountUpperCase: function(fieldLabel) {
		var me = this;
		me.LoanBillAmountUpperCase = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			labelStyle: me.labelStyle,
			name: 'LoanBillAmountUpperCase',
			itemId: 'LoanBillAmountUpperCase'
		};
	},
	createLoanBillAmount: function(fieldLabel) {
		var me = this;
		me.LoanBillAmount = {
			xtype: 'numberfield',
			minValue: 0,
			minLengthText: '最小值只能输入0',
			maxValue: 999999999999,
			maxLengthText: '最大值只能输入999999999999',
			allowBlank: false,
			emptyText: "借款金额",
			allowDecimals: true,
			decimalPrecision: 2,
			itemId: 'LoanBillAmount',
			labelStyle: me.labelStyle,
			fieldLabel: fieldLabel,
			labelWidth: 130,
			name: 'LoanBillAmount',
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
	//单笔借款上限
	createPEmpFinanceAccount_OneceLoanUpperAmount: function(fieldLabel) {
		var me = this;
		me.PEmpFinanceAccount_OneceLoanUpperAmount = {
			labelWidth: 90,
			style: 'color:#00F;',
			fieldStyle: 'background:none;border:0;border-bottom:0px;color:#00F;',
			hidden: me.hiddenEmpFinanceAccount,
			name: 'PEmpFinanceAccount_OneceLoanUpperAmount',
			itemId: 'PEmpFinanceAccount_OneceLoanUpperAmount',
			fieldLabel: fieldLabel,
			tooltip: fieldLabel
		};
	},
	//借款上限
	createLoanUpperAmount: function(fieldLabel) {
		var me = this;
		me.PEmpFinanceAccount_LoanUpperAmount = {
			style: 'color:#00F;',
			fieldStyle: 'background:none;border:0;border-bottom:0px;color:#00F;',
			hidden: me.hiddenEmpFinanceAccount,
			name: 'PEmpFinanceAccount_LoanUpperAmount',
			itemId: 'PEmpFinanceAccount_LoanUpperAmount',
			fieldLabel: fieldLabel,
			tooltip: fieldLabel
		};
	},
	//借款总额
	createPEmpFinanceAccount_LoanAmount: function(fieldLabel) {
		var me = this;
		me.PEmpFinanceAccount_LoanAmount = {
			style: 'color:#00F;',
			fieldStyle: 'background:none;border:0;border-bottom:0px;color:#00F;',
			hidden: me.hiddenEmpFinanceAccount,
			name: 'PEmpFinanceAccount_LoanAmount',
			itemId: 'PEmpFinanceAccount_LoanAmount',
			fieldLabel: fieldLabel,
			tooltip: fieldLabel
		};
	},
	//待还额度
	createPEmpFinanceAccount_UnRepaymentAmount: function(fieldLabel) {
		var me = this;
		me.PEmpFinanceAccount_UnRepaymentAmount = {
			style: 'color:#00F;',
			fieldStyle: 'background:none;border:0;border-bottom:0px;color:#00F;',
			fieldLabel: fieldLabel,
			tooltip: fieldLabel,
			hidden: me.hiddenEmpFinanceAccount,
			name: 'PEmpFinanceAccount_UnRepaymentAmount',
			itemId: 'PEmpFinanceAccount_UnRepaymentAmount'
		};
	},

	createReceiveTypeID: function(fieldLabel) {
		var me = this;
		var items = [];
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true&fields=PDict_Id,PDict_CName';
		var defaultWhere = "pdict.PDictType.Id=4646976940682874430";
		url = url + "&where=" + defaultWhere;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var count = data.value['count'];
				var list = data.value['list'];
				for(var i = 0; i < count; i++) {
					var inputValue = list[i]["PDict_Id"];
					var boxLabel = list[i]["PDict_CName"];
					var tempItem = {
						checked: (i == count - 1 ? true : false),
						name: "rgpReceiveTypeID",
						boxLabel: boxLabel,
						inputValue: inputValue
					};
					items.push(tempItem);
					if(i == count - 1)
						me.dafultReceiveTypeCName = boxLabel;
				}
			}
		}, false);
		me.ReceiveTypeID = {
			xtype: 'radiogroup',
			labelStyle: me.labelStyle,
			fieldLabel: fieldLabel,
			tooltip: fieldLabel,
			emptyText: fieldLabel,
			itemId: 'ReceiveTypeID',
			name: 'ReceiveTypeID',
			allowBlank: false,
			columnWidth: 60,
			columns: 2,
			vertical: false,
			labelWidth: 90,
			items: items,
			//重置复选组items
			resetItems: function(array) {
				me.ReceiveTypeID.removeAll();
				me.ReceiveTypeID.add(array);
			},
			addItems: function(array) {
				me.ReceiveTypeID.add(array);
			},
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					var checkbox = com.getChecked();
					var Name = me.getComponent('ReceiveTypeName');
					var inputValue = "";
					if(checkbox != null) {
						inputValue = checkbox[0].boxLabel;
					}
					Name.setValue(inputValue);
				}
			}
		};

	},
	/**领款人银行备注说明*/
	createReceiveBankInfo: function(fieldLabel) {
		var me = this;
		me.ReceiveBankInfo = {
			fieldLabel: fieldLabel,
			xtype: 'textarea',
			border: false,
			labelStyle: me.labelStyle,
			name: 'ReceiveBankInfo',
			minHeight: 20,
			height: 50,
			maxLengthText: "领款人银行备注说明",
			style: {
				marginBottom: '2px'
			}

		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		var width = 85;
		var colspanWidth = parseInt(me.width / 4) - 5;

		me.TitleName.colspan = 4;
		me.TitleName.width = 120;
		items.push(me.TitleName);

		me.LoanBillTypeName.colspan = 1;
		me.LoanBillTypeName.width = colspanWidth * me.LoanBillTypeName.colspan;
		items.push(me.LoanBillTypeName);

		me.LoanBillContentTypeName.colspan = 1;
		me.LoanBillContentTypeName.width = colspanWidth * me.LoanBillContentTypeName.colspan;
		items.push(me.LoanBillContentTypeName);

		me.ClientName.colspan = 1;
		me.ClientName.width = colspanWidth * me.ClientName.colspan;
		items.push(me.ClientName);

		me.ContractName.colspan = 1;
		me.ContractName.width = colspanWidth * me.ContractName.colspan;
		items.push(me.ContractName);

		//第三行
		me.ComponeName.colspan = 1;
		me.ComponeName.width = colspanWidth * me.ComponeName.colspan;
		items.push(me.ComponeName);

		me.ApplyMan.colspan = 1;
		me.ApplyMan.width = colspanWidth;
		items.push(me.ApplyMan);

		me.DeptName.colspan = 1;
		me.DeptName.width = colspanWidth;
		items.push(me.DeptName);

		me.ApplyDate.colspan = 1;
		me.ApplyDate.width = colspanWidth;
		items.push(me.ApplyDate);

		//
		me.LoanBillMemo.colspan = 4;
		me.LoanBillMemo.width = me.width - 20;
		items.push(me.LoanBillMemo);

		if(!me.hiddenEmpFinanceAccount) {
			//单笔借款上限
			me.PEmpFinanceAccount_OneceLoanUpperAmount.colspan = 1;
			me.PEmpFinanceAccount_OneceLoanUpperAmount.width = colspanWidth;
			items.push(me.PEmpFinanceAccount_OneceLoanUpperAmount);
			//借款上限
			me.PEmpFinanceAccount_LoanUpperAmount.colspan = 1;
			me.PEmpFinanceAccount_LoanUpperAmount.width = colspanWidth;
			items.push(me.PEmpFinanceAccount_LoanUpperAmount);
			//借款总额
			me.PEmpFinanceAccount_LoanAmount.colspan = 1;
			me.PEmpFinanceAccount_LoanAmount.width = colspanWidth;
			items.push(me.PEmpFinanceAccount_LoanAmount);
			//待还额度
			me.PEmpFinanceAccount_UnRepaymentAmount.colspan = 1;
			me.PEmpFinanceAccount_UnRepaymentAmount.width = colspanWidth;
			items.push(me.PEmpFinanceAccount_UnRepaymentAmount);
		}

		me.LoanBillAmountUpperCase.colspan = 2;
		me.LoanBillAmountUpperCase.width = colspanWidth * me.LoanBillAmountUpperCase.colspan;
		items.push(me.LoanBillAmountUpperCase);

		me.LoanBillAmount.colspan = 1;
		me.LoanBillAmount.width = colspanWidth;
		items.push(me.LoanBillAmount);

		//支付方式
		me.ReceiveTypeID.colspan = 1;
		me.ReceiveTypeID.width = colspanWidth;
		items.push(me.ReceiveTypeID);

		me.ReceiveBankInfo.colspan = 4;
		me.ReceiveBankInfo.width = me.width - 20;
		items.push(me.ReceiveBankInfo);
		return items;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ApplyDate = JShell.Date.getDate(data.ApplyDate);
		if(me.formtype == "edit" && data.LoanBillAmount != null) {
			var money = JcallShell.Number.getMoney(parseFloat(data.LoanBillAmount));
			me.LoanBillAmountUpperCase.value = money;
			me.contractExternalWhere = "";
			if(data.PClient_Id != null && data.PClient_Id != "")
				me.contractExternalWhere = "pcontract.PClientID=" + data.PClient_Id;
		}
		var reg = new RegExp("<br />", "g");
		if(data.LoanBillMemo != null)
			data.LoanBillMemo = data.LoanBillMemo.replace(reg, "\r\n");
		if(data.ReceiveBankInfo != null)
			data.ReceiveBankInfo = data.ReceiveBankInfo.replace(reg, "\r\n");

		var ReceiveTypeID = me.getComponent('ReceiveTypeID');
		if(ReceiveTypeID && data.ReceiveTypeID) {
			var tempArr = [];
			tempArr.push(data.ReceiveTypeID);
			var arrJson = {
				rgpReceiveTypeID: [tempArr]
			};
			ReceiveTypeID.setValue(arrJson);
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
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var DEPTID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'Id'
		}, {
			fieldLabel: '客户ID',
			hidden: true,
			itemId: 'ClientID',
			name: 'ClientID'
		}, {
			fieldLabel: '本公司ID',
			hidden: true,
			itemId: 'ComponeID',
			name: 'ComponeID'
		}, {
			fieldLabel: '合同ID',
			hidden: true,
			itemId: 'ContractID',
			name: 'ContractID'
		}, {
			fieldLabel: '借款类型ID',
			hidden: true,
			itemId: 'LoanBillTypeID',
			name: 'LoanBillTypeID'
		}, {
			fieldLabel: '借款内容类型ID',
			hidden: true,
			itemId: 'LoanBillContentTypeID',
			name: 'LoanBillContentTypeID'
		}, {
			fieldLabel: '所属部门ID',
			hidden: true,
			itemId: 'DeptID',
			value: (me.formtype == "add" ? DEPTID : ""),
			name: 'DeptID'
		}, {
			fieldLabel: '申请人ID',
			hidden: true,
			value: (me.formtype == "add" ? userId : ""),
			name: 'ApplyManID',
			itemId: 'ApplyManID'
		}, {
			fieldLabel: '领款方式',
			hidden: true,
			itemId: 'ReceiveTypeName',
			name: 'ReceiveTypeName',
			value: (me.formtype == "add" ? me.dafultReceiveTypeCName : "")
		});

		return items;
	},

	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.hiddenEmpFinanceAccount) {
			me.setEmpFinanceAccountValues();
		}
		var DEPTID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";;
		var DEPTNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";

		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.getForm().setValues({
			ComponeID: '',
			ComponeName: '',
			ApplyManID: userId,
			ApplyMan: userName,
			DeptID: DEPTID,
			DeptName: DEPTNAME
		});
	},
	isEdit: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.hiddenEmpFinanceAccount) {
			me.setEmpFinanceAccountValues();
		}
		if(me.contractExternalWhere != "") {
			var ContractName = me.getComponent('ContractName');
			if(ContractName) {
				ContractName.classConfig.externalWhere = me.contractExternalWhere;
				ContractName.changeClassConfig(ContractName.classConfig);
			}
		}
	},
	setEmpFinanceAccountValues: function() {
		var me = this;
		var objValues = {
			PEmpFinanceAccount_OneceLoanUpperAmount: '',
			PEmpFinanceAccount_LoanUpperAmount: '',
			PEmpFinanceAccount_LoanAmount: '',
			PEmpFinanceAccount_UnRepaymentAmount: ''
		};
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";
		if(empID != null && empID != "") {
			var url = JShell.System.Path.getRootUrl('/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountByHQL?isPlanish=true');
			url = url + '&fields=PEmpFinanceAccount_OneceLoanUpperAmount,PEmpFinanceAccount_LoanUpperAmount,PEmpFinanceAccount_LoanAmount,PEmpFinanceAccount_UnRepaymentAmount,PEmpFinanceAccount_RepaymentAmount&where=(pempfinanceaccount.IsUse=1 and pempfinanceaccount.EmpID=' + empID + ")";
			JShell.Server.get(url, function(data) {
				if(data.success) {
					var list = (data.value || {}).list || [];
					if(list && list.length > 0)
						objValues = list[0];
					me.getForm().setValues(objValues);
				}
			}, false);
		} else {
			me.getForm().setValues(objValues);
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var values = me.getForm().getValues();
		var IsUse = 1; //

		var entity = {
			LoanBillTypeName: values.LoanBillTypeName,
			LoanBillContentTypeName: values.LoanBillContentTypeName,
			ClientName: values.ClientName,
			ContractName: values.ContractName,

			ComponeName: values.ComponeName,
			DeptID: values.DeptID,
			DeptName: values.DeptName,

			Status: me.Status,
			ApplyManID: values.ApplyManID,
			ApplyMan: values.ApplyMan,
			LoanBillAmount: values.LoanBillAmount,
			//领款方式
			ReceiveTypeID: values.ReceiveTypeID,
			ReceiveTypeName: values.ReceiveTypeName,
			IsUse: IsUse,
			OperationMemo: me.OperationMemo
		};
		if(values.LoanBillTypeID) {
			entity.LoanBillTypeID = values.LoanBillTypeID;
		}
		if(values.LoanBillContentTypeID) {
			entity.LoanBillContentTypeID = values.LoanBillContentTypeID;
		}
		if(values.ClientID) {
			entity.ClientID = values.ClientID;
		}
		if(values.ContractID) {
			entity.ContractID = values.ContractID;
		}
		if(values.ComponeID) {
			entity.ComponeID = values.ComponeID;
		}
		//领款方式
		if(entity.ReceiveTypeID == null || entity.ReceiveTypeID == "") {
			var com = me.getComponent('ReceiveTypeID');
			var checkbox = com.getChecked();
			if(checkbox != null) {
				entity.ReceiveTypeID = checkbox[0].inputValue;
			}
		}

		if(values.LoanBillMemo) {
			entity.LoanBillMemo = values.LoanBillMemo.replace(/\\/g, '&#92');
			entity.LoanBillMemo = entity.LoanBillMemo.replace(/[\r\n]/g, '<br />');
		}
		if(values.ApplyDate) {
			entity.ApplyDate = JShell.Date.toServerDate(values.ApplyDate);
		}
		if(values.ReceiveBankInfo) {
			entity.ReceiveBankInfo = values.ReceiveBankInfo.replace(/\\/g, '&#92');
			entity.ReceiveBankInfo = entity.ReceiveBankInfo.replace(/[\r\n]/g, '<br />');
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		var values = me.getForm().getValues();
		entity = me.getAddParams();

		var fields = [];
		var fields = ['Id', 'ClientID', 'ClientName', 'ContractID', 'ContractName', 'LoanBillTypeID', 'LoanBillTypeName', 'LoanBillAmount', 'LoanBillContentTypeID', 'LoanBillContentTypeName', 'LoanBillMemo', 'ComponeID', 'ComponeName', 'DeptID', 'DeptName', 'Status', 'ReceiveTypeID', 'ReceiveTypeName', 'ReceiveBankInfo']; // 'ApplyManID', 'ApplyMan', 'ApplyDate',
		entity.fields = fields.join(',');
		entity.entity.Id = values.Id;
		return entity;
	}
});