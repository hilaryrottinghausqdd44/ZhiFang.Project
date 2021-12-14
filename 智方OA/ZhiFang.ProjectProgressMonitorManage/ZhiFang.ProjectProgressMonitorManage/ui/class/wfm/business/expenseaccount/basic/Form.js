/**
 * 基础报销表单
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.expenseaccount.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '基础报销表单',
	width: 550,
	height: 350,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPExpenseAccount',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePExpenseAccountByField',
	bodyPadding: '20px 20px 10px 20px',
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	/**启用表单状态初始化*/
	openFormType: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**操作记录-处理意见*/
	OperMsg: '',
	/*本公司名称*/
	ComponeName: 'OurCorName',
	/*项目类别*/
	ProjectTypeName: 'ItemType',
	/**报销单类型*/
	PExpenseAccounTypeName: 'PExpenseAccounTypeName',
	/**报销单内容类型*/
	PExpenseAccounContentTypeName: 'PExpenseAccounContentTypeName',
	/**一级科目*/
	OneLevelItemName: 'OneLevelItemName',
	/**二级科目*/
	TwoLevelItemName: 'TwoLevelItemName',
	/**处理意见的id和name*/
	ReviewInfo: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		//		me.items=me.createItems();
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.InfoLabel = {
			xtype: 'displayfield',
			name: 'InfoLabel',
			margin: '0 0 10px 0'
		};
		//创建可见组件
		me.createShowItems();
		//创建隐形组件
		items = items.concat(me.createHideItems());
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		return items;
	},
	/**创建可见组件*/
	createShowItems: function() {
		var me = this;
		//创建报销单其他选择组件
		me.createOtherItems();
		//创建字典选择组件
		me.createDictItems();
		//创建合同/部门选择组件
		me.createEvalItems();
	},

	//创建合同/部门/项目选择组件
	createEvalItems: function() {
		var me = this;
		//合同
		me.PExpenseAccount_ContractName = {
			fieldLabel: '合同',
			name: 'PExpenseAccount_ContractName',
			itemId: 'PExpenseAccount_ContractName',
			xtype: 'uxCheckTrigger',
			classConfig: {
				title: '合同选择',
				width: 480
			},
			className: 'Shell.class.wfm.business.expenseaccount.apply.ContractCheckGrid'
		};
		//项目（客户）
		me.PExpenseAccount_ClientName = {
			fieldLabel: '项目名称',
			name: 'PExpenseAccount_ClientName',
			itemId: 'PExpenseAccount_ClientName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid'
		};
		//部门
		me.PExpenseAccount_DeptName = {
			fieldLabel: '所属部门',
			name: 'PExpenseAccount_DeptName',
			itemId: 'PExpenseAccount_DeptName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.org.CheckTree',
			classConfig: {
				title: '部门选择'
			}
		};

	},
	/**创建字典选择组件*/
	createDictItems: function() {
		var me = this;
		//执行公司
		me.PExpenseAccount_ComponeName = {
			fieldLabel: '所属公司',
			name: 'PExpenseAccount_ComponeName',
			itemId: 'PExpenseAccount_ComponeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '本公司名称选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ComponeName + "'"
			}
		};

		//项目类别
		me.PExpenseAccount_ProjectTypeName = {
			fieldLabel: '费用类型',
			name: 'PExpenseAccount_ProjectTypeName',
			itemId: 'PExpenseAccount_ProjectTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			emptyText: '必填项',
			allowBlank: false,
			classConfig: {
				title: '费用类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ProjectTypeName + "'"
			}
		};

		//报销单类型
		me.PExpenseAccount_PExpenseAccounTypeName = {
			fieldLabel: '报销单类型',
			name: 'PExpenseAccount_PExpenseAccounTypeName',
			itemId: 'PExpenseAccount_PExpenseAccounTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '报销单类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.PExpenseAccounTypeName + "'"
			}
		};

		//报销单内容类型
		me.PExpenseAccount_PExpenseAccounContentTypeName = {
			fieldLabel: '内容类型',
			name: 'PExpenseAccount_PExpenseAccounContentTypeName',
			itemId: 'PExpenseAccount_PExpenseAccounContentTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '内容类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.PExpenseAccounContentTypeName + "'"
			}
		};

		//一级科目
		me.PExpenseAccount_OneLevelItemName = {
			fieldLabel: '一级科目',
			name: 'PExpenseAccount_OneLevelItemName',
			itemId: 'PExpenseAccount_OneLevelItemName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '一级科目选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.OneLevelItemName + "'"
			}
		};
		//二级科目
		me.PExpenseAccount_TwoLevelItemName = {
			fieldLabel: '二级科目',
			name: 'PExpenseAccount_TwoLevelItemName',
			itemId: 'PExpenseAccount_TwoLevelItemName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '二级科目选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.TwoLevelItemName + "'"
			}
		};
		//核算单位
		me.PExpenseAccount_AccountingDeptName = {
			fieldLabel: '核算单位',
			name: 'PExpenseAccount_AccountingDeptName',
			itemId: 'PExpenseAccount_AccountingDeptName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.org.CheckTree',
			classConfig: {
				title: '核算单位选择'
			}
		};
	},

	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'PExpenseAccount_Id'
		});
		items.push({
			fieldLabel: '本公司ID',
			hidden: true,
			name: 'PExpenseAccount_ComponeID',
			itemId: 'PExpenseAccount_ComponeID'
		});
		items.push({
			fieldLabel: '合同ID',
			hidden: true,
			name: 'PExpenseAccount_ContractID',
			itemId: 'PExpenseAccount_ContractID'
		});
		items.push({
			fieldLabel: '项目类别ID',
			hidden: true,
			name: 'PExpenseAccount_ProjectTypeID',
			itemId: 'PExpenseAccount_ProjectTypeID'
		});
		items.push({
			fieldLabel: '报销单内容类型ID',
			hidden: true,
			name: 'PExpenseAccount_PExpenseAccounContentTypeID',
			itemId: 'PExpenseAccount_PExpenseAccounContentTypeID'
		});
		items.push({
			fieldLabel: '项目ID',
			hidden: true,
			name: 'PExpenseAccount_ClientID',
			itemId: 'PExpenseAccount_ClientID'
		});
		items.push({
			fieldLabel: '部门ID',
			hidden: true,
			name: 'PExpenseAccount_DeptID',
			itemId: 'PExpenseAccount_DeptID'
		});
		items.push({
			fieldLabel: '报销单类型ID',
			hidden: true,
			name: 'PExpenseAccount_PExpenseAccounTypeID',
			itemId: 'PExpenseAccount_PExpenseAccounTypeID'
		});
		items.push({
			fieldLabel: '一级科目ID',
			hidden: true,
			name: 'PExpenseAccount_OneLevelItemID',
			itemId: 'PExpenseAccount_OneLevelItemID'
		});
		items.push({
			fieldLabel: '二级科目ID',
			hidden: true,
			name: 'PExpenseAccount_TwoLevelItemID',
			itemId: 'PExpenseAccount_TwoLevelItemID'
		});
		items.push({
			fieldLabel: '核算单位ID',
			hidden: true,
			name: 'PExpenseAccount_AccountingDeptID',
			itemId: 'PExpenseAccount_AccountingDeptID'
		});
		items.push({
			fieldLabel: '申请人',
			hidden: true,
			name: 'PExpenseAccount_ApplyMan',
			itemId: 'PExpenseAccount_ApplyMan'
		});
		items.push({
			fieldLabel: '一审人',
			hidden: true,
			name: 'PExpenseAccount_ReviewMan',
			itemId: 'PExpenseAccount_ReviewMan'
		});
		items.push({
			fieldLabel: '二审人',
			hidden: true,
			name: 'PExpenseAccount_TwoReviewMan',
			itemId: 'PExpenseAccount_TwoReviewMan'
		});
		items.push({
			fieldLabel: '三审人',
			hidden: true,
			name: 'PExpenseAccount_ThreeReviewMan',
			itemId: 'PExpenseAccount_ThreeReviewMan'
		});
		items.push({
			fieldLabel: '四审人',
			hidden: true,
			name: 'PExpenseAccount_FourReviewMan',
			itemId: 'PExpenseAccount_FourReviewMan'
		});
		items.push({
			fieldLabel: '打款人',
			hidden: true,
			name: 'PExpenseAccount_PayManName',
			itemId: 'PExpenseAccount_PayManName'
		});
		return items;
	},

	/**创建其他选择组件*/
	createOtherItems: function() {
		var me = this;
		//核算年月
		me.PExpenseAccount_AccountingDate = {
			xtype: 'uxYearComboBox',
			itemId: 'Year',
			fieldLabel: '核算年份',
			name: 'PExpenseAccount_AccountingDate',
			//			minValue: 2016,
			value: me.YearValue
		};
		me.PExpenseAccount_PExpenseAccountNo = {
			fieldLabel: '报销单号',
			name: 'PExpenseAccount_PExpenseAccountNo'
		};
		me.PExpenseAccount_PExpenseAccounAmount = {
			fieldLabel: '报销金额',
			name: 'PExpenseAccount_PExpenseAccounAmount',
			itemId: 'PExpenseAccount_PExpenseAccounAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//出差天数
		me.PExpenseAccount_DayCount = {
			fieldLabel: '出差天数',
			name: 'PExpenseAccount_DayCount',
			itemId: 'PExpenseAccount_DayCount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//交通费
		me.PExpenseAccount_Transport = {
			fieldLabel: '交通费',
			name: 'PExpenseAccount_Transport',
			itemId: 'PExpenseAccount_Transport',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//交通费
		me.PExpenseAccount_CityTransport = {
			fieldLabel: '市内车费',
			name: 'PExpenseAccount_CityTransport',
			itemId: 'PExpenseAccount_CityTransport',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//住宿费
		me.PExpenseAccount_HotelRates = {
			fieldLabel: '住宿费',
			name: 'PExpenseAccount_HotelRates',
			itemId: 'PExpenseAccount_HotelRates',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//餐费补贴
		me.PExpenseAccount_Meals = {
			fieldLabel: '餐费补贴',
			name: 'PExpenseAccount_Meals',
			itemId: 'PExpenseAccount_Meals',

			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//招待费
		me.PExpenseAccount_EntertainsCosts = {
			fieldLabel: '招待费',
			name: 'PExpenseAccount_EntertainsCosts',
			itemId: 'PExpenseAccount_EntertainsCosts',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//通讯费
		me.PExpenseAccount_CommunicationCosts = {
			fieldLabel: '通讯费',
			name: 'PExpenseAccount_CommunicationCosts',
			itemId: 'PExpenseAccount_CommunicationCosts',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//其他费
		me.PExpenseAccount_OtherCosts = {
			fieldLabel: '其他费',
			name: 'PExpenseAccount_OtherCosts',
			itemId: 'PExpenseAccount_OtherCosts',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//报销单号
		me.PExpenseAccount_PExpenseAccountNo = {
			fieldLabel: '报销单号',
			name: 'PExpenseAccount_PExpenseAccountNo',
			itemId: 'PExpenseAccount_PExpenseAccountNo'
		};
		//报销单金额
		me.PExpenseAccount_PExpenseAccounAmount = {
			fieldLabel: '报销单金额',
			name: 'PExpenseAccount_PExpenseAccounAmount',
			itemId: 'PExpenseAccount_PExpenseAccounAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//财务凭证单号
		me.PExpenseAccount_VoucherNo = {
			fieldLabel: '财务凭证单号',
			name: 'PExpenseAccount_VoucherNo',
			itemId: 'PExpenseAccount_VoucherNo'
		};
		//现金金额
		me.PExpenseAccount_CashAmount = {
			fieldLabel: '现金金额',
			name: 'PExpenseAccount_CashAmount',
			itemId: 'PExpenseAccount_CashAmount',
			minValue: 0,
			xtype: 'numberfield'
		};

		//转账金额
		me.PExpenseAccount_TransferAmount = {
			fieldLabel: '转账金额',
			name: 'PExpenseAccount_TransferAmount',
			itemId: 'PExpenseAccount_TransferAmount',
			minValue: 0,
			xtype: 'numberfield'
		};
		//借款相抵金额
		me.PExpenseAccount_LoadAmount = {
			fieldLabel: '借款相抵金额',
			name: 'PExpenseAccount_LoadAmount',
			itemId: 'PExpenseAccount_LoadAmount',
			minValue: 0,
			xtype: 'numberfield'
		};
		//报销单说明
		me.PExpenseAccount_PExpenseAccounMemo = {
			fieldLabel: '报销单说明',
			name: 'PExpenseAccount_PExpenseAccounMemo',
			minHeight: 40,
			style: {
				marginBottom: '10px'
			},
			xtype: 'textarea'
		};
		//报销单号
		me.PExpenseAccount_PExpenseAccountNo = {
			fieldLabel: '报销单号',
			name: 'PExpenseAccount_PExpenseAccountNo',
			itemId: 'PExpenseAccount_PExpenseAccountNo'
		};
		//处理意见
		me.ReviewInfo = {
			fieldLabel: '处理意见',
			minHeight: 60,
			height: 80,
			name: me.ReviewInfo,
			itemId: me.ReviewInfo,
			xtype: 'textarea'
		};
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//部门
		var DeptName = me.getComponent('PExpenseAccount_DeptName'),
			DeptID = me.getComponent('PExpenseAccount_DeptID');
		if(DeptName) {
			DeptName.on({
				check: function(p, record) {
					DeptName.setValue(record ? record.get('text') : '');
					DeptID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
		//核算单位
		var AccountingDeptName = me.getComponent('PExpenseAccount_AccountingDeptName'),
			AccountingDeptID = me.getComponent('PExpenseAccount_AccountingDeptID');
		if(AccountingDeptName) {
			AccountingDeptName.on({
				check: function(p, record) {
					AccountingDeptName.setValue(record ? record.get('text') : '');
					AccountingDeptID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
		//合同
		var ContractName = me.getComponent('PExpenseAccount_ContractName'),
			ContractID = me.getComponent('PExpenseAccount_ContractID');
		if(ContractName) {
			ContractName.on({
				check: function(p, record) {
					ContractName.setValue(record ? record.get('PContract_Name') : '');
					ContractID.setValue(record ? record.get('PContract_Id') : '');
					p.close();
				}
			});
		}
		//项目
		var ClientName = me.getComponent('PExpenseAccount_ClientName'),
			ClientID = me.getComponent('PExpenseAccount_ClientID');
		if(ClientName) {
			ClientName.on({
				check: function(p, record) {
					ClientName.setValue(record ? record.get('PClient_Name') : '');
					ClientID.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			});
		}
		//字典监听
		var dictList = [
			'Compone', 'ProjectType', 'PExpenseAccounType', 'PExpenseAccounContentType', 'TwoLevelItem', 'OneLevelItem'
		];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		var CName = me.getComponent('PExpenseAccount_' + name + 'Name');
		var Id = me.getComponent('PExpenseAccount_' + name + 'ID');
		//		var DataTimeStamp = me.getComponent('PExpenseAccount_' + name + 'DataTimeStamp');

		if(!CName) return;

		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				//				DataTimeStamp.setValue(record ? record.get('PDict_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {

		var me = this;
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];

		return items;
	}
});