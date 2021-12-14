/**
 * 一审
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.invoice.oneaudit.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
	],
	title: '发票一审',
	width: 240,
	height: 430,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	hasButtontoolbar: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPInvoice',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByField',
	/**布局方式*/
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
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: '',
	/**发票信息*/
	Params:null,
	OperMsg: '发票开具',
	/**收入分类字典*/
	IncomeTypeName: 'IncomeTypeName',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键',
			name: 'PInvoice_Id',
			hidden: true,
			itemId: 'PInvoice_Id'
		}, {
			fieldLabel: '收入分类',
			allowBlank: false,
			emptyText: '必填项',
			name: 'PInvoice_IncomeTypeName',
			itemId: 'PInvoice_IncomeTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '收入分类选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.IncomeTypeName + "'"
			},
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '收入分类选择ID',
			colspan: 1,
			width: me.defaults.width * 1,
			height: 80,
			hidden: true,
			name: 'PInvoice_IncomeTypeID',
			itemId: 'PInvoice_IncomeTypeID'
		}, {
			fieldLabel: '申请表编号',
			colspan: 1,
			width: me.defaults.width * 1,
			name: 'PInvoice_InvoiceNo',
			itemId: 'PInvoice_InvoiceNo'
		}, {
			fieldLabel: '处理意见',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 80,
			xtype: 'textarea',
			name: 'PInvoice_ReviewInfo',
			itemId: 'PInvoice_ReviewInfo'
		});
		return items;
	},

	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Status: 3
		};
		entity.InvoiceNo = values.PInvoice_InvoiceNo;
		if(values.PInvoice_IncomeTypeID) {
			entity.IncomeTypeName = values.PInvoice_IncomeTypeName;
		}
		if(values.PInvoice_ReviewInfo) {
			entity.ReviewInfo = values.PInvoice_ReviewInfo.replace(/\\/g, '&#92');
			entity.OperationMemo = values.PInvoice_ReviewInfo.replace(/\\/g, '&#92');
		}
		if(me.PK) {
			entity.Id = me.PK;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.ReviewDate = ReviewDateStr;
		}
		if(me.Params){

			if(me.Params.PInvoice_ClientID) {
				entity.ClientID = me.Params.PInvoice_ClientID;
				entity.ClientName = me.Params.PInvoice_ClientName;
			}
			if(me.Params.PInvoice_InvoiceTypeID) {
				entity.InvoiceTypeID = me.Params.PInvoice_InvoiceTypeID;
			    entity.InvoiceTypeName = me.Params.PInvoice_InvoiceTypeName;
			}
			if(me.Params.PInvoice_ComponeID) {
				entity.ComponeID = me.Params.PInvoice_ComponeID;
				entity.ComponeName = me.Params.PInvoice_ComponeName;
			}
			if(me.Params.PInvoice_ProjectTypeID) {
				entity.ProjectTypeID = me.Params.PInvoice_ProjectTypeID;
				entity.ProjectTypeName = me.Params.PInvoice_ProjectTypeName;
			}
	
			if(me.Params.PInvoice_ProjectPaceID) {
				entity.ProjectPaceID = me.Params.PInvoice_ProjectPaceID;
				entity.ProjectPaceName = me.Params.PInvoice_ProjectPaceName;
			}
			if(me.Params.PInvoice_InvoiceContentTypeID) {
				entity.InvoiceContentTypeID = me.Params.PInvoice_InvoiceContentTypeID;
				entity.InvoiceContentTypeName = me.Params.PInvoice_InvoiceContentTypeName;
			}
			if(me.Params.PInvoice_ExpectReceiveDate) {
				entity.ExpectReceiveDate = JShell.Date.toServerDate(me.Params.PInvoice_ExpectReceiveDate);
			}
	
	      	if(me.Params.PInvoice_VATNumber) {
				entity.VATNumber = me.Params.PInvoice_VATNumber;
			}
	      	if(me.Params.PInvoice_VATAccount) {
				entity.VATAccount = me.Params.PInvoice_VATAccount;
			}
	      	if(me.Params.PInvoice_VATBank) {
				entity.VATBank =  me.Params.PInvoice_VATBank;
			}
			if(me.Params.PInvoice_Comment) {
				entity.Comment = me.Params.PInvoice_Comment.replace(/\\/g, '&#92');
			}
			if(me.Params.PInvoice_ReceiveInvoiceName) {
				entity.ReceiveInvoiceName = me.Params.PInvoice_ReceiveInvoiceName;
			}
			if(me.Params.PInvoice_ReceiveInvoicePhoneNumbers) {
				entity.ReceiveInvoicePhoneNumbers = me.Params.PInvoice_ReceiveInvoicePhoneNumbers;
			}
			if(me.Params.PInvoice_ReceiveInvoiceAddress) {
				entity.ReceiveInvoiceAddress = me.Params.PInvoice_ReceiveInvoiceAddress;
			}
			if(me.Params.PInvoice_ClientAddress) {
				entity.ClientAddress = me.Params.PInvoice_ClientAddress;
			}
			if(me.Params.PInvoice_ClientPhone) {
				entity.ClientPhone = me.Params.PInvoice_ClientPhone;
			}
			if(me.Params.PInvoice_HardwareAmount) {
				entity.HardwareAmount = me.Params.PInvoice_HardwareAmount;
			}
			if(me.Params.PInvoice_SoftwareAmount) {
				entity.SoftwareAmount = me.Params.PInvoice_SoftwareAmount;
			}
			if(me.Params.PInvoice_ServerAmount) {
				entity.ServerAmount = me.Params.PInvoice_ServerAmount;
			}		
			if(me.Params.PInvoice_SoftwareCount) {
				entity.SoftwareCount = me.Params.PInvoice_SoftwareCount;
			}	
			if(me.Params.PInvoice_HardwareCount) {
				entity.HardwareCount = me.Params.PInvoice_HardwareCount;
			}		
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id', ' Status', 'InvoiceDate','IncomeTypeName','IsReceiveMoney','InvoiceNo',
		    'ClientID', 'ClientName', 'InvoiceTypeID', 'InvoiceTypeName',
			'ComponeID', 'ComponeName',
			'ProjectTypeID', 'ProjectTypeName', 'ProjectPaceID', 'ProjectPaceName',
			'InvoiceContentTypeID', 'InvoiceContentTypeName', 'ExpectReceiveDate',  'VATNumber',
			'VATAccount', 'VATBank', 'Comment','ReceiveInvoiceName','ReceiveInvoicePhoneNumbers',
			'ReceiveInvoiceAddress', 'ClientAddress','ClientPhone','HardwareAmount','SoftwareAmount',
			'ServerAmount','SoftwareCount','HardwareCount'
		];
		entity.fields = fields.join(',');
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//收入分类
		var IncomeTypeName = me.getComponent('PInvoice_IncomeTypeName'),
			IncomeTypeID = me.getComponent('PInvoice_IncomeTypeID');

		if(IncomeTypeName) {
			IncomeTypeName.on({
				check: function(p, record) {
					IncomeTypeName.setValue(record ? record.get('PDict_CName') : '');
					IncomeTypeID.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			});
		}
	}
});