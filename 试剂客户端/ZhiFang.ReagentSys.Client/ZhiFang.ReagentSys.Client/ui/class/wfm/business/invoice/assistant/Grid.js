/**
 * 发票邮寄
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.assistant.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.BasicGrid',
	title: '发票审核列表',
	PayOrgID: '',
	PayOrgName: '',
	PInvoiceMsg: '发票邮寄',
	/**状态默认为一审*/
	defaultStatusValue: '7',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	ExportType: 4,
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
				//可编辑状态为发票开具
				if(Status == '7' ) {
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
	/**邮寄信息*/
	openEditForm: function(id,ContractID) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.basic.ShowTabPanel', {
			resizable: true,
			PK: id,
			formtype: 'show',
			hasDisSave: false,
			hasOpener: true,
			SaveText: '发票邮寄',
			PInvoiceMsg: '发票邮寄',
			title:'发票邮寄',
				/**合同ID*/
	        ContractID:ContractID,
	        SUB_WIN_NO: '7',
			listeners: {
				onSaveClick: function(win) {
					me.onAddClick(id, win);
				}
			}
		}).show();
	},
	/**发票开具信息填写*/
	onAddClick: function(id, win) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.assistant.Form', {
			resizable: true,
			formtype: 'edit',
			width: 500,
			height: 200,
			title: '发票邮寄',
			PK: id,
			SUB_WIN_NO: '8',
			listeners: {
				save: function(p, id) {
					p.close();
					me.fireEvent('save', win);
				}
			}
		}).show();
	}
});