/**
 * 发票申请页签
 * @author liangyl
 * @version 2016-10-18
 */
Ext.define('Shell.class.wfm.business.invoice.apply.AddPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '发票申请',
	height: 490,
	width: 715,
	border: false,
	hasButtontoolbar: true,
	hasSave: true,
	hasReset: true,
	hasSaveas: false,
	hasCancel: true,
	/*发票管理Id**/
	PK: '',
	/*付款单位Id**/
	PayOrgID: '',
	/*付款单位**/
	PayOrgName: '',
	/*合同Id**/
	ContractID: null,
	/*合同**/
	ContractName: null,
	formtype: 'edit',
	hastempSave: true,
	contentIsLoad: false,
	uploadIsLoad: false,
	Status: '',
	/**当前状态操作记录*/
	STATUS_ID: null,
	/**操作记录-处理意见*/
	OperMsg: '',
	/**已开票金额*/
	InvoiceMoney: 0,
	/**合同金额*/
	ContractInvoiceMoney: 0,
	/**选择行开票金额*/
	InvoiceAmount:0,
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
	initComponent: function() {
		var me = this;
		me.items = [];
		me.bodyPadding = 1;
		me.items = me.createItems();
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0) {
			me.dockedItems = dockedItems;
		}
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			isValid: function(p) {
				me.setActiveTab(me.Form);
			},
			beforesave: function(p) {
				me.showMask(me.saveText);
			},
			aftersave: function(p, id) {
				me.hideMask();
				me.PK = id;
				me.onSaveAttachment(id);
			}
		});
		me.Attachment.on({
			save: function(win, data) {
				if(data.success) {
					me.fireEvent('save', me, me.PK);
				} else {
					JShell.Msg.error(data.msg);
				}
			}
		});
		me.ontabchange();
	},
	onSaveAttachment: function(id) {
		var me = this;
		me.Attachment.setValues({
			fkObjectId: id
		});
		me.Attachment.save();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'Interaction':
						me.Interaction.PK = me.PK;
						break;
					case 'ContentForm':
						me.loadContentForm();
						break;
					case 'Attachment':
						me.loadAttachment();
						break;
					default:
						break
				}
			}
		});
	},
	createItems: function() {
		var me = this;
		var items = [];
		me.Form = Ext.create('Shell.class.wfm.business.invoice.apply.Form', {
			title: '发票信息',
			PayOrgID: me.PayOrgID,
			PayOrgName: me.PayOrgName,
			formtype: me.formtype,
			VAT: me.VAT,
			PK: me.PK,
			itemId: 'Form',
			border: true
		});
		me.ContentForm = Ext.create('Shell.class.wfm.business.invoice.apply.ContentForm', {
			title: '说明',
			header: false,
			formtype: me.formtype,
			layout: 'fit',
			itemId: 'ContentForm',
			border: false
		});
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			region: 'center',
			header: false,
			title: '附件',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory: "PInvoice",
			formtype: me.formtype
		});
		me.OperatePanel = Ext.create('Shell.class.wfm.business.invoice.basic.OperatePanel', {
			title: '操作记录',
			formtype: 'show',
			itemId: 'OperatePanel',
			hasLoadMask: false,
			PK: me.PK
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流',
			header: false,
			PK: me.PK,
			FormPosition: 'e',
			itemId: 'Interaction',
			border: false
		});
	    me.PdfApp = Ext.create('Shell.class.wfm.business.invoice.basic.PdfApp', {
			title: '预览PDF',
			itemId: 'PdfApp',
			border: false,
			height: me.height,
			width: me.width,
			hasBtntoolbar:false,
			defaultLoad:true,
			PK: me.PK
		});
		items.push(me.Form, me.ContentForm, me.Attachment);
		if(me.formtype == 'edit') {
			items.push(me.OperatePanel, me.Interaction,me.PdfApp);
		}
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if(items.length == 0) {
			if(me.hastempSave) {
				items.push({
					xtype: 'button',
					itemId: 'btnTempSaveSave',
					iconCls: 'button-save',
					text: "暂存",
					tooltip: "发票申请临时暂存",
					handler: function() {
						me.Status = 1;
						me.onSaveClick(false);
					}
				});
			}

			if(me.hasSave) {
				items.push({
					xtype: 'button',
					itemId: 'btnSave',
					iconCls: 'button-save',
					text: "保存",
					tooltip: "发票申请保存",
					handler: function() {
						me.Status = 2;
						me.onSaveClick(true);
					}
				});
			}
			if(me.hasCancel) items.push({
				xtype: 'button',
				itemId: 'btnColse',
				iconCls: 'button-del',
				text: "关闭",
				tooltip: '关闭',
				handler: function() {
					me.fireEvent('onCloseClick', me);
					me.close();
				}
			});
			if(items.length > 0) items.unshift('->');
		}
		if(items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	/**加载文档附件信息*/
	loadAttachment: function() {
		var me = this;
		if(me.uploadIsLoad == false && me.formtype == "edit") {
			me.uploadIsLoad = true;
			me.Attachment.PK = me.PK;
			me.Attachment.load();
		}
	},
	/**加载文档内容信息*/
	loadContentForm: function() {
		var me = this;
		if(me.contentIsLoad == false) {
			me.ContentForm.load(me.PK);
			me.contentIsLoad = true;
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function(isSubmit) {
		var me = this,
			values = me.Form.getForm().getValues();
		//设置申请时间默认值和申请人默认值
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyManDate = JcallShell.Date.toString(Sysdate);
		var entity = {
			ApplyDate: JShell.Date.toServerDate(ApplyManDate)
		};
		if(values.PInvoice_InvoiceAmount) {
			var InvoiceMoney= parseFloat(me.InvoiceMoney) + parseFloat(values.PInvoice_InvoiceAmount);
			var ContractInvoiceMoney= parseFloat(me.ContractInvoiceMoney);
            
            if(me.formtype=='edit'){
            	InvoiceMoney=InvoiceMoney-me.InvoiceAmount;
            }
			if(Number(InvoiceMoney.toFixed(2)) > Number(ContractInvoiceMoney.toFixed(2))) {
				JShell.Msg.error('开票金额不能大于合同金额!');
				return;
			}
			var nn = me.InvoiceMoney + values.PInvoice_InvoiceAmount;
			entity.InvoiceAmount = values.PInvoice_InvoiceAmount;
			
			//本次开票金额占合同额百分比
			var PercentAmount =values.PInvoice_InvoiceAmount/me.ContractInvoiceMoney;
			if(PercentAmount){
				entity.PercentAmount = parseFloat(PercentAmount).toFixed(2)*100+'%';
			}
		}
		if(me.PayOrgID) {
			entity.PayOrgID = me.Form.PayOrgID;
		}
		if(me.PayOrgName) {
			entity.PayOrgName = me.Form.PayOrgName;
		}
		//合同
		if(me.ContractID) {
			entity.ContractID = me.ContractID;
		}
		if(me.ContractName) {
			entity.ContractName = me.ContractName;
		}
		if(values.PInvoice_ClientID) {
			entity.ClientID = values.PInvoice_ClientID;
		}
		if(values.PInvoice_ClientName) {
			entity.ClientName = values.PInvoice_ClientName;
		}
		if(values.PInvoice_InvoiceTypeID) {
			entity.InvoiceTypeID = values.PInvoice_InvoiceTypeID;
		}
		if(values.PInvoice_InvoiceTypeName) {
			entity.InvoiceTypeName = values.PInvoice_InvoiceTypeName;
		}
		if(values.PInvoice_ComponeID) {
			entity.ComponeID = values.PInvoice_ComponeID;
		}
		if(values.PInvoice_ComponeName) {
			entity.ComponeName = values.PInvoice_ComponeName;
		}
		if(values.PInvoice_ProjectTypeID) {
			entity.ProjectTypeID = values.PInvoice_ProjectTypeID;
		}
		if(values.PInvoice_ProjectTypeName) {
			entity.ProjectTypeName = values.PInvoice_ProjectTypeName;
		}
		if(values.PInvoice_ProjectPaceID) {
			entity.ProjectPaceID = values.PInvoice_ProjectPaceID;
		}
		if(values.PInvoice_ProjectPaceName) {
			entity.ProjectPaceName = values.PInvoice_ProjectPaceName;
		}
		if(values.PInvoice_ApplyManID) {
			entity.ApplyManID = values.PInvoice_ApplyManID;
		}
		if(values.PInvoice_ApplyMan) {
			entity.ApplyMan = values.PInvoice_ApplyMan;
		}
		if(values.PInvoice_InvoiceContentTypeID) {
			entity.InvoiceContentTypeID = values.PInvoice_InvoiceContentTypeID;
		}
		if(values.PInvoice_InvoiceContentTypeName) {
			entity.InvoiceContentTypeName = values.PInvoice_InvoiceContentTypeName;
		}
		if(values.PInvoice_InvoiceContentID) {
			entity.InvoiceContentID = values.PInvoice_InvoiceContentID;
		}
		if(values.PInvoice_InvoiceContentName) {
			entity.InvoiceContentName = values.PInvoice_InvoiceContentName;
		}
		if(values.PInvoice_InvoiceInfo) {
			entity.InvoiceInfo = values.PInvoice_InvoiceInfo;
		}
		if(values.PInvoice_ExpectReceiveDate) {
			entity.ExpectReceiveDate = JShell.Date.toServerDate(values.PInvoice_ExpectReceiveDate);
		}
		//文档内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var InvoiceMemo = contentvalues.PInvoice_InvoiceMemo;
		if(InvoiceMemo && InvoiceMemo != 'undefined') {
			entity.InvoiceMemo = contentvalues.PInvoice_InvoiceMemo.replace(/\\/g, '&#92');
		}
      	if(values.PInvoice_VATNumber) {
			entity.VATNumber = values.PInvoice_VATNumber;
		}
      	if(values.PInvoice_VATAccount) {
			entity.VATAccount = values.PInvoice_VATAccount;
		}
      	if(values.PInvoice_VATBank) {
			entity.VATBank =  values.PInvoice_VATBank;
		}
		if(values.PInvoice_Comment) {
			entity.Comment = values.PInvoice_Comment.replace(/\\/g, '&#92');
		}
		if(values.PInvoice_ReceiveInvoiceName) {
			entity.ReceiveInvoiceName = values.PInvoice_ReceiveInvoiceName;
		}
		if(values.PInvoice_ReceiveInvoicePhoneNumbers) {
			entity.ReceiveInvoicePhoneNumbers = values.PInvoice_ReceiveInvoicePhoneNumbers;
		}
		if(values.PInvoice_ReceiveInvoiceAddress) {
			entity.ReceiveInvoiceAddress = values.PInvoice_ReceiveInvoiceAddress;
		}
		if(values.PInvoice_ClientAddress) {
			entity.ClientAddress = values.PInvoice_ClientAddress;
		}
		if(values.PInvoice_ClientPhone) {
			entity.ClientPhone = values.PInvoice_ClientPhone;
		}
		if(values.PInvoice_HardwareAmount) {
			entity.HardwareAmount = values.PInvoice_HardwareAmount;
		}
		if(values.PInvoice_SoftwareAmount) {
			entity.SoftwareAmount = values.PInvoice_SoftwareAmount;
		}
		if(values.PInvoice_ServerAmount) {
			entity.ServerAmount = values.PInvoice_ServerAmount;
		}		
		if(values.PInvoice_SoftwareCount) {
			entity.SoftwareCount = values.PInvoice_SoftwareCount;
		}	
		if(values.PInvoice_HardwareCount) {
			entity.HardwareCount = values.PInvoice_HardwareCount;
		}		
		//所属部门
		var DeptID=JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
	    var DeptName=JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		if(DeptID){
			entity.DeptID=DeptID;
		}
		if(DeptName){
			entity.DeptName=DeptName;
		}
		
		entity.Status = me.Status;
		entity.IsUse = 1;
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function(isSubmit) {
		var me = this,
			values = me.Form.getForm().getValues(),
			entity = me.getAddParams(isSubmit);
		var fields = ['Id', 'ReceiveInvoiceName', 'ReceiveInvoicePhoneNumbers', 'ReceiveInvoiceAddress',
			'ClientID', 'ClientName', 'ComponeID', 'ComponeName',
			'ProjectTypeID', 'ProjectTypeName', 'ProjectPaceID', 'ProjectPaceName',
			'InvoiceTypeID', 'InvoiceTypeName', 'InvoiceAmount', 'Status', 'Comment',
			'InvoiceContentTypeID', 'InvoiceContentTypeName', 'ExpectReceiveDate',
			'ClientPhone', 'ClientAddress','VATBank','VATAccount','VATNumber',
			'HardwareCount','SoftwareCount','ServerAmount','SoftwareAmount','HardwareAmount',
			'PercentAmount','InvoiceContentID','InvoiceContentName'
		];
        if(entity && entity!='undefined') {
        	entity.fields = fields.join(',');
        	if(values.PInvoice_Id != '') {
				entity.entity.Id = values.PInvoice_Id;
			}
        }
		
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var url = me.Form.formtype == 'add' ? me.Form.addUrl : me.Form.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params = me.Form.formtype == 'add' ? me.getAddParams(isSubmit) : me.getEditParams(isSubmit);
		if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		var values = me.Form.getForm().getValues();
		me.fireEvent('beforesave', me);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				me.fireEvent('aftersave', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});