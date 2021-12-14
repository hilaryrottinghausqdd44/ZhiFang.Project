/**
 * 发票一审
 * @author liangyl
 * @version 2017-03-09
 */
Ext.define('Shell.class.wfm.business.invoice.show.ShowTabPanel', {
	extend: 'Shell.class.wfm.business.invoice.basic.ShowTabPanel',
	PK:null,
	Status:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
        me.Form.on({
        	save: function(p) {
        		me.fireEvent('save', me);
        	}
        });
	},
	
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		me.Form = Ext.create('Shell.class.wfm.business.invoice.apply.Form', {
			title: '发票信息',
			formtype:'edit',
			PK: me.PK,
			itemId: 'Form',
			hasButtontoolbar: true,
			Status:me.Status,
			hasSave: true,
			hasReset:true,
			border: true
		});
		me.Form.changeResult= function(data) {
			var me =this;
			data.PInvoice_ApplyDate = JShell.Date.getDate(data.PInvoice_ApplyDate);
			data.PInvoice_ExpectReceiveDate = JShell.Date.getDate(data.PInvoice_ExpectReceiveDate);
			if(data.PInvoice_ClientPhone == '' && data.PInvoice_ClientAddress == '') {
				me.showAddressANDPhoneNumbers(false);
			}
			var reg = new RegExp("<br/>", "g");
			data.PInvoice_Comment = data.PInvoice_Comment.replace(reg, "\r\n");
			var InvoiceAmount=me.getComponent('PInvoice_InvoiceAmount');
			InvoiceAmount.setReadOnly(true);
			//用户名称
			var ClientName = me.getComponent('PInvoice_ClientName');
            ClientName.emptyText = '必填项';
			ClientName.allowBlank = false;
			return data;
		};
		
		//退回
	me.Form.onSaveClick=function() {
		var me = this;
		if(!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		var url='/SingleTableService.svc/ST_UDTO_UpdatePInvoiceByFieldManager';
		var url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
	   		var entity = {
				Status: me.Status
			};
			if(me.PK) {
				entity.Id = me.PK;
			}
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
					me.fireEvent('save');
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
				} else {
					var msg = data.msg;
					if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
						msg = '有重复';
					}
					JShell.Msg.error(msg);
				}
			}, false);
		};
		items.splice(0, 0,me.Form);
	    return items;
	}
});