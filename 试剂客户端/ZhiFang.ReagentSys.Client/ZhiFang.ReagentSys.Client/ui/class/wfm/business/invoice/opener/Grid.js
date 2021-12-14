/**
 * 发票开具
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.opener.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.BasicGrid',
	title: '发票开具',
	PInvoiceMsg: '发票开具',
	/**状态默认为一审*/
	defaultStatusValue: '5',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	ExportType: 3,
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
				//发票状态为二审通过是可编辑状态
				if(Status =='5' ) {
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
			SaveText: '发票开具',
			PInvoiceMsg: '发票开具',
			title:'发票开具',
				/**合同ID*/
	        ContractID:ContractID,
	        	SUB_WIN_NO: '5',
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
		JShell.Win.open('Shell.class.wfm.business.invoice.opener.Form', {
			resizable: true,
			formtype: 'edit',
			width: 500,
			height: 200,
			title: '发票开具',
			PK: id,
			SUB_WIN_NO: '6',
			listeners: {
				save: function(p, id) {
					p.close();
					me.fireEvent('save', win);
				}
			}
		}).show();
	}
});