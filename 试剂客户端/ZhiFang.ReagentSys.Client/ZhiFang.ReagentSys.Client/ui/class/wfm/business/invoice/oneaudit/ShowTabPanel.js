/**
 * 发票一审
 * @author liangyl
 * @version 2017-03-09
 */
Ext.define('Shell.class.wfm.business.invoice.oneaudit.ShowTabPanel', {
	extend: 'Shell.class.wfm.business.invoice.basic.ShowTabPanel',
	PK:null,
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		me.Form = Ext.create('Shell.class.wfm.business.invoice.apply.Form', {
			title: '发票信息',
			formtype:'edit',
			PK: me.PK,
			itemId: 'Form',
			border: true
		});
		me.Form.changeResult= function(data) {
			data.PInvoice_ApplyDate = JShell.Date.getDate(data.PInvoice_ApplyDate);
			data.PInvoice_ExpectReceiveDate = JShell.Date.getDate(data.PInvoice_ExpectReceiveDate);
			if(data.PInvoice_ClientPhone == '' && data.PInvoice_ClientAddress == '') {
				me.Form.showAddressANDPhoneNumbers(false);
			}
			var reg = new RegExp("<br/>", "g");
			data.PInvoice_Comment = data.PInvoice_Comment.replace(reg, "\r\n");
			var InvoiceAmount=me.Form.getComponent('PInvoice_InvoiceAmount');
			InvoiceAmount.setReadOnly(true);
			//用户名称
			var ClientName = me.Form.getComponent('PInvoice_ClientName');
            ClientName.emptyText = '必填项';
			ClientName.allowBlank = false;
			return data;
		};

		items.splice(0, 0,me.Form);
	    return items;
	}
});