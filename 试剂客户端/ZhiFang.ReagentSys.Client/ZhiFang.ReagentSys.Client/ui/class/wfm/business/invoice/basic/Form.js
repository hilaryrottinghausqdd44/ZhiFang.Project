/**
 * 基础发票表单
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.invoice.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '基础发票表单',
	width: 550,
	height: 450,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPInvoice',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePInvoiceByField',
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
	Compname: 'OurCorName',
	/*发票类型*/
	InvoiceType: 'InvoiceType',
	/**项目进度*/
	ProjectPace: 'ProjectPace',
	/**项目类别*/
	ProjectType: 'ItemType',
	/*付款单位Id*/
	PayOrgID: '',
	/*付款单位Id*/
	PayOrgName: '',
	/*开票内容*/
	InvoiceContentType: 'InfoiceContent',
	/*发票类型标记*/
	InvoiceTypeShortcode: '',
	/*发票类型为增值税发票时，注册地址和电话信息*/
	VAT:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
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
		//创建字典选择组件
		me.createDictItems();
		//创建其他组件
		me.createOtherItems();
	},

	//创建其他组件
	createOtherItems: function() {
		var me = this;
		//申请人
		me.PInvoice_ApplyMan = {
		fieldLabel: '申请人',
			name: 'PInvoice_ApplyMan',
			itemId: 'PInvoice_ApplyMan',
			readOnly: true,
			locked: true,
			hidden: true,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1
		};
		//付款单位
		me.PInvoice_PayOrgName = {
			fieldLabel: '付款单位',
			name: 'PInvoice_PayOrgName',
			value: me.PayOrgName,
			readOnly: true,
			locked: true,
			emptyText: '必填项',
			allowBlank: false
		};
		//开票金额
		me.PInvoice_InvoiceAmount = {
			fieldLabel: '开票金额',
			name: 'PInvoice_InvoiceAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		};
		//预计回款时间
		me.PInvoice_ExpectReceiveDate = {
			fieldLabel: '预计回款时间',
			name: 'PInvoice_ExpectReceiveDate',
			itemId: 'PInvoice_ExpectReceiveDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		//收票人姓名
		me.PInvoice_ReceiveInvoiceName = {
				fieldLabel: '收票人姓名',
			name: 'PInvoice_ReceiveInvoiceName',
			itemId: 'PInvoice_ReceiveInvoiceName',
			emptyText: '必填项',
			allowBlank: false,
		};
	//收票人电话
		me.PInvoice_ReceiveInvoicePhoneNumbers = {
			fieldLabel: '收票人电话',
			name: 'PInvoice_ReceiveInvoicePhoneNumbers',
			itemId: 'PInvoice_ReceiveInvoicePhoneNumbers',
			emptyText: '必填项',
			allowBlank: false
		};
		//注册地址
		me.PInvoice_Address = {
			fieldLabel: '注册地址',
			name: 'PInvoice_Address',
			itemId: 'PInvoice_Address',
			hidden: true
		};
			//收票人地址
		me.PInvoice_PhoneNumbers = {
			fieldLabel: '电话',
			name: 'PInvoice_PhoneNumbers',
			itemId: 'PInvoice_PhoneNumbers',
			hidden: true
		};
	},
	/**创建字典选择组件*/
	createDictItems: function() {
		var me = this;
		//用户名称
		me.PInvoice_ClientName = {
		fieldLabel: '用户名称',
			name: 'PInvoice_ClientName',
			itemId: 'PInvoice_ClientName',
			value: me.PayOrgName,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid',
			emptyText: '必填项',
			allowBlank: false
		};

		//项目类别
		me.PInvoice_ProjectTypeName = {
			fieldLabel: '项目类别',
			name: 'PInvoice_ProjectTypeName',
			xtype: 'uxCheckTrigger',
			itemId: 'PInvoice_ProjectTypeName',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '项目类别选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectType + "'"
			}
		};

		//项目进度
		me.PInvoice_ProjectPaceName = {
			fieldLabel: '项目进度',
			name: 'PInvoice_ProjectPaceName',
			itemId: 'PInvoice_ProjectPaceName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '项目进度选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectPace + "'"
			}
		};

		//发票类型选择
		me.PInvoice_InvoiceTypeName = {
			fieldLabel: '发票类型',
			name: 'PInvoice_InvoiceTypeName',
			itemId: 'PInvoice_InvoiceTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.business.invoice.basic.DictCheckGrid',
			classConfig: {
				title: '发票类型选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.InvoiceType + "'"
			}
		};

		//执行公司
		me.PInvoice_ComponeName = {
			fieldLabel: '执行公司',
			name: 'PInvoice_ComponeName',
			itemId: 'PInvoice_ComponeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '本公司名称选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.Compname + "'"
			}
		};
		//开票内容
		me.PInvoice_InvoiceContentTypeName = {
			fieldLabel: '开票内容',
			name: 'PInvoice_InvoiceContentTypeName',
			itemId: 'PInvoice_InvoiceContentTypeName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.business.invoice.apply.CheckTypeGrid',
			classConfig: {
				width: 530,
				title: '开票内容类型选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.InvoiceContentType + "'"
			},
		};
		
	},

	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'PInvoice_Id'
		});
		items.push({
			fieldLabel: '执行公司',
			name: 'PInvoice_ComponeID',
			itemId: 'PInvoice_ComponeID',
			hidden: true
		});
		items.push({
			fieldLabel: '申请时间',
			name: 'PInvoice_ApplyDate',
			itemId: 'PInvoice_ApplyDate',
			xtype: 'datefield',
			hidden: true,
			format: 'Y-m-d'
		});
		items.push({
			fieldLabel: '发票类型',
			name: 'PInvoice_InvoiceTypeID',
			itemId: 'PInvoice_InvoiceTypeID',
			hidden: true
		});
		items.push({
			fieldLabel: '开票内容类型',
			name: 'PInvoice_InvoiceContentTypeID',
			itemId: 'PInvoice_InvoiceContentTypeID',
			hidden: true
		});
		items.push({
			fieldLabel: '申请人',
			name: 'PInvoice_ApplyManID',
			itemId: 'PInvoice_ApplyManID',
			hidden: true
		});
		items.push({
			fieldLabel: '项目进度',
			itemId: 'PInvoice_ProjectPaceID',
			name: 'PInvoice_ProjectPaceID',
			hidden: true
		});
		items.push({
			fieldLabel: '用户名称Id',
			name: 'PInvoice_ClientID',
			itemId: 'PInvoice_ClientID',
			value: me.PayOrgID,
			hidden: true
		});
		items.push({
			fieldLabel: '项目类别',
			name: 'PInvoice_ProjectTypeID',
			itemId: 'PInvoice_ProjectTypeID',
			hidden: true
		});
		items.push({
			fieldLabel: '付款单位Id',
			name: 'PInvoice_PayOrgID',
			hidden: true,
			value: me.PayOrgID
		});
		
		return items;
	},
/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//付款单位
		var PayOrg = me.getComponent('PInvoice_PayOrg'),
			PInvoice_PayOrg_Id = me.getComponent('PInvoice_PayOrg_Id');
		if(PayOrg) {
			PayOrg.on({
				check: function(p, record) {
					PayOrg.setValue(record ? record.get('PClient_Name') : '');
					PInvoice_PayOrg_Id.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			});
		}
		//用户名称
		var Client = me.getComponent('PInvoice_ClientName'),
			ClientID = me.getComponent('PInvoice_ClientID');
		if(Client) {
			Client.on({
				check: function(p, record) {
					Client.setValue(record ? record.get('PClient_Name') : '');
					ClientID.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			});
		}
		//申请人
		var ApplyMan = me.getComponent('PInvoice_ApplyMan'),
			ApplyMan_Id = me.getComponent('PInvoice_ApplyManID');
		if(ApplyMan) {
			ApplyMan.on({
				check: function(p, record) {
					ApplyMan.setValue(record ? record.get('HREmployee_CName') : '');
					ApplyMan_Id.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			});
		}
		//发票类型
		var InvoiceTypeName = me.getComponent('PInvoice_InvoiceTypeName'),
			InvoiceTypeID = me.getComponent('PInvoice_InvoiceTypeID');
		var InvoiceTypeShortcode = '';
		//电话
		var PhoneNumbers = me.getComponent('PInvoice_PhoneNumbers');
		//地址
		var Address = me.getComponent('PInvoice_Address');
		if(InvoiceTypeName) {
			InvoiceTypeName.on({
				check: function(p, record) {
					InvoiceTypeName.setValue(record ? record.get('BDict_CName') : '');
					InvoiceTypeID.setValue(record ? record.get('BDict_Id') : '');
					InvoiceTypeShortcode = record ? record.get('BDict_Shortcode') : '';
					if(InvoiceTypeShortcode == '1' || InvoiceTypeShortcode == '2') {
						PhoneNumbers.setVisible(true);
						Address.setVisible(true);
					} else {
						PhoneNumbers.setVisible(false);
						Address.setVisible(false);
					}
					p.close();
				}
			});
		}
		//字典监听
		var dictList = ['InvoiceType', 'Compone', 'ProjectType', 'ProjectPace', 'InvoiceContentType'];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		var CName = me.getComponent('PInvoice_' + name + 'Name');
		var Id = me.getComponent('PInvoice_' + name + 'ID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				p.close();
			}
		});
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