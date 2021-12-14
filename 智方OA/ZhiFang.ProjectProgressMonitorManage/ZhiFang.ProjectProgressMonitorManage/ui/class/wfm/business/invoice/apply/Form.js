/**
 * 发票申请表单
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.invoice.apply.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '新增发票申请',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 240,
	height: 500,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	hasButtontoolbar: false,
	Status:null,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPInvoice',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByField',
	/**添加发票申请日志服务地址*/
	addPInvoiceOperLogUrl: '/SystemCommonService.svc/SC_UDTO_AddSCOperation',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 90,
		width: 220,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
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
	/*新公司开票内容*/
	InvoiceContent: 'InfoiceContent',
	/*旧公司开票内容*/
	ZFKFInfoiceContent: 'ZFKFInfoiceContent',
	/*发票类型标记*/
	InvoiceTypeShortcode: '',
	VAT: {
		/**增值税税号*/
		VATNumber: '',
		/**增值税开户行*/
		VATBank: '',
		/**增值税账号*/
		VATAccount: '',
		/**电话*/
		PhoneNum: '',
		/**地址*/
		Address: ''
	},
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
				fieldLabel: '付款单位Id',
				name: 'PInvoice_PayOrgID',
				itemId: 'PInvoice_PayOrgID',
				hidden: true,
				colspan: 1,
				width: me.defaults.width * 1,
				value: me.PayOrgID
			}, {
				fieldLabel: '付款单位',
				name: 'PInvoice_PayOrgName',
				itemId: 'PInvoice_PayOrgName',
				colspan: 2,
				width: me.defaults.width * 2 ,
				value: me.PayOrgName,
				readOnly: true,
				locked: true,
				emptyText: '必填项',
				allowBlank: false
			},{
				fieldLabel: '项目类别',
				name: 'PInvoice_ProjectTypeName',
				xtype: 'uxCheckTrigger',
				itemId: 'PInvoice_ProjectTypeName',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '项目类别选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ProjectType + "'"
				},
				colspan: 1,
				width: me.defaults.width * 1
			},  {
				fieldLabel: '用户名称',
				name: 'PInvoice_ClientName',
				itemId: 'PInvoice_ClientName',
				value: me.PayOrgName,
				colspan: 2,
				width: me.defaults.width * 2,
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.client.CheckGrid'
			}, {
				fieldLabel: '项目进度',
				name: 'PInvoice_ProjectPaceName',
				itemId: 'PInvoice_ProjectPaceName',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '项目进度选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ProjectPace + "'"
				},
				colspan: 1,
				width: me.defaults.width * 1
			},  {
				fieldLabel: '执行公司',
				name: 'PInvoice_ComponeName',
				itemId: 'PInvoice_ComponeName',
				colspan: 2,
				width: me.defaults.width * 2,
				emptyText: '必填项',
				allowBlank: false,
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '本公司名称选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.Compname + "'"
				}
			}, {
				fieldLabel: '预计回款时间',
				width: me.defaults.width * 1,
				name: 'PInvoice_ExpectReceiveDate',
				itemId: 'PInvoice_ExpectReceiveDate',
				xtype: 'datefield',
				format: 'Y-m-d'
			},{
				fieldLabel: '发票类型',
				name: 'PInvoice_InvoiceTypeName',
				itemId: 'PInvoice_InvoiceTypeName',
				xtype: 'uxCheckTrigger',
				emptyText: '必填项',
				allowBlank: false,
				className: 'Shell.class.wfm.business.invoice.basic.DictCheckGrid',
				classConfig: {
					title: '发票类型选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.InvoiceType + "'"
				},
				colspan: 2,
				width: me.defaults.width * 2
			},{
				fieldLabel: '客户注册电话',
				name: 'PInvoice_ClientPhone',
				itemId: 'PInvoice_ClientPhone',
				colspan: 1,
				width: me.defaults.width * 1
			}, {
				fieldLabel: '客户注册地址',
				name: 'PInvoice_ClientAddress',
				itemId: 'PInvoice_ClientAddress',
				colspan: 2,
				width: me.defaults.width * 2
			},{
				fieldLabel: '增值税税号',
				name: 'PInvoice_VATNumber',
				itemId: 'PInvoice_VATNumber',

				colspan: 1,
				emptyText: '必填项',
				allowBlank: false,
				width: me.defaults.width * 1
			},{
				fieldLabel: '增值税开户行',
				name: 'PInvoice_VATBank',
				itemId: 'PInvoice_VATBank',

				emptyText: '必填项',
				allowBlank: false,
				colspan: 2,
				width: me.defaults.width * 2
			},  {
				fieldLabel: '增值税账号',
				name: 'PInvoice_VATAccount',
				itemId: 'PInvoice_VATAccount',

				colspan: 1,
				emptyText: '必填项',
				allowBlank: false,
				width: me.defaults.width * 1
			},  {
				fieldLabel: '开票内容类型',
				name: 'PInvoice_InvoiceContentTypeName',
				itemId: 'PInvoice_InvoiceContentTypeName',
				colspan: 2,
				emptyText: '必填项',
				allowBlank: false,
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
//					width: 530,
					title: '开票内容类型选择',
					defaultWhere: "pdict.PDictType.Id=5106560944309337578"
				},
				width: me.defaults.width * 2
			}, {
				fieldLabel: '收票人姓名',
				name: 'PInvoice_ReceiveInvoiceName',
				itemId: 'PInvoice_ReceiveInvoiceName',
				emptyText: '必填项',
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},{
				fieldLabel: '收票人地址',
				name: 'PInvoice_ReceiveInvoiceAddress',
				itemId: 'PInvoice_ReceiveInvoiceAddress',
				emptyText: '必填项',
				allowBlank: false,
				colspan: 2,
				width: me.defaults.width * 2
			},
			  {
				fieldLabel: '收票人电话',
				name: 'PInvoice_ReceiveInvoicePhoneNumbers',
				itemId: 'PInvoice_ReceiveInvoicePhoneNumbers',
				emptyText: '必填项',
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},{
				fieldLabel: '开票内容',
				name: 'PInvoice_InvoiceContentName',
				itemId: 'PInvoice_InvoiceContentName',
				colspan: 2,
				emptyText: '必填项',
				allowBlank: false,
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.business.invoice.apply.CheckTypeGrid',
				classConfig: {
					width: 530,
					title: '开票内容选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.InvoiceContent + "'"
				},
				width: me.defaults.width * 2
			}, {
				fieldLabel: '开票金额',
				name: 'PInvoice_InvoiceAmount',
				itemId: 'PInvoice_InvoiceAmount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 1,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			},{
				fieldLabel: '硬件金额',
				name: 'PInvoice_HardwareAmount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 1,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			}, {
				fieldLabel: '软件金额',
				name: 'PInvoice_SoftwareAmount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 1,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			}, {
				fieldLabel: '服务金额',
				name: 'PInvoice_ServerAmount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 1,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			}, {
				fieldLabel: '软件套数',
				name: 'PInvoice_SoftwareCount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 1,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			}, {
				fieldLabel: '硬件数量',
				name: 'PInvoice_HardwareCount',
				minValue: 0,
				xtype: 'numberfield',
				value: 0,
				colspan: 2,
				width: me.defaults.width * 1,
				emptyText: '必填项',
				allowBlank: false
			}, {
				fieldLabel: '用户名称Id',
				name: 'PInvoice_ClientID',
				itemId: 'PInvoice_ClientID',
				value: me.PayOrgID,
				hidden: true
			}, {
				fieldLabel: '申请人',
				name: 'PInvoice_ApplyManID',
				itemId: 'PInvoice_ApplyManID',
				hidden: true,
				colspan: 1,
				width: me.defaults.width * 1
			}, {
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
			}, {
				fieldLabel: '开票内容',
				name: 'PInvoice_InvoiceContentID',
				itemId: 'PInvoice_InvoiceContentID',
				colspan: 1,
				hidden: true,
				width: me.defaults.width * 1
			}, {
				fieldLabel: '开票内容类型',
				name: 'PInvoice_InvoiceContentTypeID',
				itemId: 'PInvoice_InvoiceContentTypeID',
				colspan: 1,
				hidden: true,
				width: me.defaults.width * 1
			}, {
				fieldLabel: '发票类型',
				name: 'PInvoice_InvoiceTypeID',
				itemId: 'PInvoice_InvoiceTypeID',
				hidden: true,
				colspan: 1,
				width: me.defaults.width * 1
			},
            {
				fieldLabel: '申请时间',
				width: me.defaults.width * 1,
				name: 'PInvoice_ApplyDate',
				itemId: 'PInvoice_ApplyDate',
				xtype: 'datefield',
				hidden: true,
				format: 'Y-m-d'
			}, {
				fieldLabel: '执行公司',
				name: 'PInvoice_ComponeID',
				itemId: 'PInvoice_ComponeID',
				colspan: 2,
				hidden: true,
				width: me.defaults.width * 2
			},{
				height: 60,
				colspan: 3,
				width: me.defaults.width * 3,
				emptyText: '备注',
				fieldLabel: '备注',
				name: 'PInvoice_Comment',
				itemId: 'PInvoice_Comment',
				xtype: 'textarea'
			}, {
				fieldLabel: '主键ID',
				name: 'PInvoice_Id',
				hidden: true
			}, {
				fieldLabel: '项目类别',
				name: 'PInvoice_ProjectTypeID',
				itemId: 'PInvoice_ProjectTypeID',
				hidden: true,
				colspan: 1,
				width: me.defaults.width * 1
			}, {
				fieldLabel: '项目进度',
				itemId: 'PInvoice_ProjectPaceID',
				name: 'PInvoice_ProjectPaceID',
				hidden: true,
				colspan: 1,
				width: me.defaults.width * 1
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

		if(InvoiceTypeName) {
			InvoiceTypeName.on({
				check: function(p, record) {
					InvoiceTypeName.setValue(record ? record.get('PDict_CName') : '');
					InvoiceTypeID.setValue(record ? record.get('PDict_Id') : '');
					InvoiceTypeShortcode = record ? record.get('PDict_Shortcode') : '';
					if(InvoiceTypeShortcode == 1) {
						me.showAddressANDPhoneNumbers(true);
					} else {
						me.showAddressANDPhoneNumbers(false);
					}
					p.close();
				}
			});
		}
		//执行公司
		var ComponeName = me.getComponent('PInvoice_ComponeName'),
			ComponeID = me.getComponent('PInvoice_ComponeID');
		var InvoiceContentName=me.getComponent('PInvoice_InvoiceContentName');
		var InvoiceContentID=me.getComponent('PInvoice_InvoiceContentID');

		if(ComponeName) {
			ComponeName.on({
				check: function(p, record) {
					var Id=record ? record.get('PDict_Id') : ''	;
					ComponeName.setValue(record ? record.get('PDict_CName') : '');
					ComponeID.setValue(Id);
					InvoiceContentName.setValue('');
					InvoiceContentID.setValue('');
					var ContentType=me.InvoiceContent;
				    //新公司
					if(Id=='5446797576966742329'){
						ContentType=me.InvoiceContent;
					}else{
						ContentType=me.ZFKFInfoiceContent;
					}
					InvoiceContentName.classConfig={
						defaultWhere: "pdict.PDictType.DictTypeCode='" + ContentType + "'"
					};
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
					var id =record ? record.get('PClient_Id') : '';
					me.clientSetDefault(id);
					Client.setValue(record ? record.get('PClient_Name') : '');
					ClientID.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				}
			});
		}
		//字典监听
		var dictList = ['InvoiceType', 'ProjectType', 'ProjectPace', 'InvoiceContentType','InvoiceContent'];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	},
	/**客户改变，赋默认值 */
    clientSetDefault:function(id){
    	var me=this;
//  	var PInvoice_PayOrgID= me.getComponent('PInvoice_PayOrgID'),
//			PInvoice_PayOrgName= me.getComponent('PInvoice_PayOrgName'),
		var	PInvoice_ComponeID= me.getComponent('PInvoice_ComponeID'),
			PInvoice_ComponeName= me.getComponent('PInvoice_ComponeName'),
			PInvoice_InvoiceTypeName= me.getComponent('PInvoice_InvoiceTypeName'),
			PInvoice_InvoiceTypeID= me.getComponent('PInvoice_InvoiceTypeID'),
			PInvoice_ClientAddress= me.getComponent('PInvoice_ClientAddress'),
			PInvoice_VATBank= me.getComponent('PInvoice_VATBank'),
			PInvoice_InvoiceContentTypeName= me.getComponent('PInvoice_InvoiceContentTypeName'),
			PInvoice_InvoiceContentTypeID= me.getComponent('PInvoice_InvoiceContentTypeID'),
			PInvoice_ReceiveInvoiceAddress= me.getComponent('PInvoice_ReceiveInvoiceAddress'),
			PInvoice_InvoiceContentName= me.getComponent('PInvoice_InvoiceContentName'),
			PInvoice_InvoiceContentID= me.getComponent('PInvoice_InvoiceContentID'),
			PInvoice_ClientPhone= me.getComponent('PInvoice_ClientPhone'),
			PInvoice_VATNumber= me.getComponent('PInvoice_VATNumber'),
			PInvoice_VATAccount= me.getComponent('PInvoice_VATAccount'),
			PInvoice_ReceiveInvoiceName= me.getComponent('PInvoice_ReceiveInvoiceName'),
			PInvoice_ReceiveInvoicePhoneNumbers= me.getComponent('PInvoice_ReceiveInvoicePhoneNumbers');
           
//          PInvoice_PayOrgID.setValue('');
//			PInvoice_PayOrgName.setValue('');
			PInvoice_ComponeID.setValue('');
			PInvoice_ComponeName.setValue('');
			PInvoice_InvoiceTypeName.setValue('');
			PInvoice_InvoiceTypeID.setValue('');
			PInvoice_ClientAddress.setValue('');
			PInvoice_VATBank.setValue('');
			PInvoice_InvoiceContentTypeName.setValue('');
			PInvoice_InvoiceContentTypeID.setValue('');
			PInvoice_ReceiveInvoiceAddress.setValue('');
			PInvoice_InvoiceContentName.setValue('');
			PInvoice_InvoiceContentID.setValue('');
			PInvoice_ClientPhone.setValue('');
			PInvoice_VATNumber.setValue('');
			PInvoice_VATAccount.setValue('');
			PInvoice_ReceiveInvoiceName.setValue('');
			PInvoice_ReceiveInvoicePhoneNumbers.setValue('');
        if(id){
			me.getInfo(id,function(data){
				if(data && data.value.list){
//					var PayOrgID=data.value.list[0].PInvoice_PayOrgID;
//					var PayOrgName=data.value.list[0].PInvoice_PayOrgName;
					var ComponeID=data.value.list[0].PInvoice_ComponeID;
					var ComponeName=data.value.list[0].PInvoice_ComponeName;
					var InvoiceTypeName=data.value.list[0].PInvoice_InvoiceTypeName;
					var InvoiceTypeID=data.value.list[0].PInvoice_InvoiceTypeID;
					var ClientAddress=data.value.list[0].PInvoice_ClientAddress;
					var VATBank=data.value.list[0].PInvoice_VATBank;
					var InvoiceContentTypeName=data.value.list[0].PInvoice_InvoiceContentTypeName;
					var InvoiceContentTypeID=data.value.list[0].PInvoice_InvoiceContentTypeID;
					var ReceiveInvoiceAddress=data.value.list[0].PInvoice_ReceiveInvoiceAddress;
					var InvoiceContentName=data.value.list[0].PInvoice_InvoiceContentName;
					var InvoiceContentID=data.value.list[0].PInvoice_InvoiceContentID;
					var ClientPhone=data.value.list[0].PInvoice_ClientPhone;
					var VATNumber=data.value.list[0].PInvoice_VATNumber;
					var VATAccount=data.value.list[0].PInvoice_VATAccount;
					var ReceiveInvoiceName=data.value.list[0].PInvoice_ReceiveInvoiceName;
					var ReceiveInvoicePhoneNumbers=data.value.list[0].PInvoice_ReceiveInvoicePhoneNumbers;	

//		            PInvoice_PayOrgID.setValue(PayOrgID);
//					PInvoice_PayOrgName.setValue(PayOrgName);
					PInvoice_ComponeID.setValue(ComponeID);
					PInvoice_ComponeName.setValue(ComponeName);
					PInvoice_InvoiceTypeName.setValue(InvoiceTypeName);
					PInvoice_InvoiceTypeID.setValue(InvoiceTypeID);
					PInvoice_ClientAddress.setValue(ClientAddress);
					PInvoice_VATBank.setValue(VATBank);
					PInvoice_InvoiceContentTypeName.setValue(InvoiceContentTypeName);
					PInvoice_InvoiceContentTypeID.setValue(InvoiceContentTypeID);
					PInvoice_ReceiveInvoiceAddress.setValue(ReceiveInvoiceAddress);
					PInvoice_InvoiceContentName.setValue(InvoiceContentName);
					PInvoice_InvoiceContentID.setValue(InvoiceContentID);
					PInvoice_ClientPhone.setValue(ClientPhone);
					PInvoice_VATNumber.setValue(VATNumber);
					PInvoice_VATAccount.setValue(VATAccount);
					PInvoice_ReceiveInvoiceName.setValue(ReceiveInvoiceName);
					PInvoice_ReceiveInvoicePhoneNumbers.setValue(ReceiveInvoicePhoneNumbers);
		    	}
			});
		}
    },
	/**显示注册地址和电话*/
	showAddressANDPhoneNumbers: function(bo) {
		var me = this;
		//电话
		var PhoneNumbers = me.getComponent('PInvoice_ClientPhone');
		//地址
		var Address = me.getComponent('PInvoice_ClientAddress');
		if(!PhoneNumbers) return;
		if(!Address) return;
		if(bo === false) {
			Address.hide();
			PhoneNumbers.hide();
			Address.emptyText = '';
			Address.allowBlank = true;
			PhoneNumbers.emptyText = '';
			PhoneNumbers.allowBlank = true;
			PhoneNumbers.setValue('');
			Address.setValue('');
		} else {
			Address.show();
			PhoneNumbers.show();
			Address.emptyText = '必填项';
			Address.allowBlank = false;
			PhoneNumbers.emptyText = '必填项';
			PhoneNumbers.allowBlank = false;
			Address.setValue(me.VAT.Address);
			PhoneNumbers.setValue(me.VAT.PhoneNum);
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
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PInvoice_ApplyDate = JShell.Date.getDate(data.PInvoice_ApplyDate);
		data.PInvoice_ExpectReceiveDate = JShell.Date.getDate(data.PInvoice_ExpectReceiveDate);
		if(data.PInvoice_ClientPhone == '' && data.PInvoice_ClientAddress == '') {
			this.showAddressANDPhoneNumbers(false);
		}
		var reg = new RegExp("<br/>", "g");
		data.PInvoice_Comment = data.PInvoice_Comment.replace(reg, "\r\n");
		return data;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			//设置申请时间默认值和申请人默认值
			var EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			var username = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || -1;
			var PInvoice_ApplyManID = me.getComponent('PInvoice_ApplyManID');
			var PInvoice_ApplyMan = me.getComponent('PInvoice_ApplyMan');
			PInvoice_ApplyManID.setValue(EmpID);
			PInvoice_ApplyMan.setValue(username);
			me.showAddressANDPhoneNumbers(false);
			var VATBank = me.getComponent('PInvoice_VATBank');
			VATBank.setValue(me.VAT.VATBank);
			var VATNumber = me.getComponent('PInvoice_VATNumber');
			VATNumber.setValue(me.VAT.VATNumber);
			var VATAccount = me.getComponent('PInvoice_VATAccount');
			VATAccount.setValue(me.VAT.VATAccount);
			if(me.PayOrgID){
				me.clientSetDefault(me.PayOrgID);
			}
		} else {
			me.getForm().setValues(me.lastData);
		}
	},
	/**更改标题
	 * */
	changeTitle: function() {
		var me = this;
	},
    /**获取上一次的提交的用户发票信息
     * 付款单位、用户名称、执行公司、发票类型、客户注册地址、
     * 增值税开户行、开票内容类型、收票人地址、开票内容、
     * 客户注册电话、增值税税号、增值税账号、收票人姓名、收票人电话
     * @author liangyl @version 2017-07-27*/
	 getInfo:function(Id,callback){
		var me = this,
			url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByHQL?isPlanish=true';
		
		url = JShell.System.Path.getRootUrl(url);
		url +='&fields=PInvoice_PayOrgID,PInvoice_PayOrgName,PInvoice_ClientName,PInvoice_ClientID,'+
			'PInvoice_ComponeID,PInvoice_ComponeName,PInvoice_InvoiceTypeName,PInvoice_InvoiceTypeID,'+
			'PInvoice_ClientAddress,PInvoice_VATBank,PInvoice_InvoiceContentTypeName,PInvoice_InvoiceContentTypeID,'+
			'PInvoice_ReceiveInvoiceAddress,PInvoice_InvoiceContentName,PInvoice_InvoiceContentID,'+
			'PInvoice_ClientPhone,PInvoice_VATNumber,'+
			'PInvoice_VATAccount,PInvoice_ReceiveInvoiceName,PInvoice_ReceiveInvoicePhoneNumbers';
		
		var where = 'pinvoice.IsUse=1 and pinvoice.ClientID='+Id;
		where+='&sort=[{"property":"PInvoice_ApplyDate","direction":"DESC"}]';
		url += '&where=' + where;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});