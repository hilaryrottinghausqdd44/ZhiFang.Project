/**
 * 商务助理审核（一审)
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.oneaudit.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.BasicGrid',
	title: '发票一审',
	/**默认员工类型*/
	defaultUserType: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get('PInvoice_Id');
				var Status = record.get('PInvoice_Status');
				var ContractID = record.get('PInvoice_ContractID');
				//发票状态为申请或者二审退回时可编辑
				if(Status == '2'  || Status == '6') {
					me.openEditForm(id, ContractID);
				} else {
					me.openShowForm(id, ContractID);
				}
			},
			save: function(p) {
				p.close();
				me.onSearch();
				me.OperMsg = '';
			}
		});
	},
	/**发票修改*/
	openEditForm: function(id, ContractID) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.oneaudit.ShowTabPanel', {
			resizable: true,
			PK: id,
			PInvoiceMsg: me.PInvoiceMsg,
			DigSaveText: me.DigSaveText,
			SaveText: me.SaveText,
			hasSave: me.hasSave,
			hasDisSave: me.hasDisSave,
			formtype: 'show',
			VAT:me.VAT,
			ContractID: ContractID,
			SUB_WIN_NO: '2',
			title:'商务助理审核',
			listeners: {
				onSaveClick: function(p) {
					//发票信息
				    if(!p.Form.getForm().isValid()) return;
					me.onAddClick(id, p);
				},
				onDigSaveClick: function(p) {
//					if(!p.Form.getForm().isValid()) return;
					me.onSave(id, me.noadoptStatus, p);
				},
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	
	/**发票一审信息填写*/
	onAddClick: function(id, win) {
		var me = this;
		var  params = win.Form.getForm().getValues();
		JShell.Win.open('Shell.class.wfm.business.invoice.oneaudit.Form', {
			resizable: true,
			formtype: 'edit',
			width: 500,
			height: 200,
			title: '发票一审',
			PK: id,
			SUB_WIN_NO: '3',
			Params:params,
			listeners: {
				save: function(p, id) {
					p.close();
					me.fireEvent('save', win);
				}
			}
		}).show();
	},
	//退回
	onSaveClick: function(id, Status, text, p) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var entity = {
			Status: Status,
			OperationMemo: text,
			ReviewInfo: text,
			Id: id
		};
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.ReviewDate = ReviewDateStr;
		}
		var values = p.Form.getForm().getValues();
		if(values){
			if(values.PInvoice_ClientID) {
				entity.ClientID = values.PInvoice_ClientID;
				entity.ClientName = values.PInvoice_ClientName;
			}
			if(values.PInvoice_InvoiceTypeID) {
				entity.InvoiceTypeID = values.PInvoice_InvoiceTypeID;
			    entity.InvoiceTypeName = values.PInvoice_InvoiceTypeName;
			}
			if(values.PInvoice_ComponeID) {
				entity.ComponeID = values.PInvoice_ComponeID;
				entity.ComponeName = values.PInvoice_ComponeName;
			}
			if(values.PInvoice_ProjectTypeID) {
				entity.ProjectTypeID = values.PInvoice_ProjectTypeID;
				entity.ProjectTypeName = values.PInvoice_ProjectTypeName;
			}
	
			if(values.PInvoice_ProjectPaceID) {
				entity.ProjectPaceID = values.PInvoice_ProjectPaceID;
				entity.ProjectPaceName = values.PInvoice_ProjectPaceName;
			}
			if(values.PInvoice_InvoiceContentTypeID) {
				entity.InvoiceContentTypeID = values.PInvoice_InvoiceContentTypeID;
				entity.InvoiceContentTypeName = values.PInvoice_InvoiceContentTypeName;
			}
			if(values.PInvoice_ExpectReceiveDate) {
				entity.ExpectReceiveDate = JShell.Date.toServerDate(values.PInvoice_ExpectReceiveDate);
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
		}
		
		var fields = ['Id', ' Status', 'InvoiceDate','IncomeTypeName','IsReceiveMoney','InvoiceNo',
		    'ClientID', 'ClientName', 'InvoiceTypeID', 'InvoiceTypeName',
			'ComponeID', 'ComponeName',
			'ProjectTypeID', 'ProjectTypeName', 'ProjectPaceID', 'ProjectPaceName',
			'InvoiceContentTypeID', 'InvoiceContentTypeName', 'ExpectReceiveDate',  'VATNumber',
			'VATAccount', 'VATBank', 'Comment','ReceiveInvoiceName','ReceiveInvoicePhoneNumbers',
			'ReceiveInvoiceAddress', 'ClientAddress','ClientPhone','HardwareAmount','SoftwareAmount',
			'ServerAmount','SoftwareCount','HardwareCount'
		];
		fields = fields.join(',');
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', p);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	}

});