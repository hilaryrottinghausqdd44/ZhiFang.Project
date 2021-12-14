/**
 * 发票申请列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.invoice.apply.Grid', {
	extend: 'Shell.class.wfm.business.invoice.basic.Grid',
	title: '发票申请列表',
	features: [{
		ftype: 'summary'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	PayOrgID: '',
	PayOrgName: '',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	ExportType: '0',
	ContractID: null,
	ContractName: null,
	/**已开票金额*/
	InvoiceMoney: 0,
	/**合同金额*/
	ContractInvoiceMoney: 0,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);	
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var Status = record.get('PInvoice_Status');
				var id = record.get('PInvoice_Id');
				var ContractID = record.get('PInvoice_ContractID');
				//当前选中行开票金额
				var InvoiceAmount = record.get('PInvoice_InvoiceAmount');

				if(Status == '4' || Status == '1') {
					me.openEditForm(id, ContractID,InvoiceAmount);
				} else {
					me.openShowForm(id, ContractID);
				}
			}
		});
	},
		/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PInvoiceStatus',function(){
			if(!JShell.System.ClassDict.PInvoiceStatus){
    			JShell.Msg.error('未获取到发票状态，请刷新列表');
    			return;
    		}
           
    	});
    	var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere ="pinvoice.ApplyManID=" + userId;
    	me.load(null, true, autoSelect);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.push({
			text: '状态',
			dataIndex: 'PInvoice_Status',
			width: 60,
			sortable: false,
			menuDisabled: false,
		    renderer:function(value,meta){
            	var v = value || '';
            	if(v){
            		var info = JShell.System.ClassDict.getClassInfoById('PInvoiceStatus',v);
            		if(info){
            			v = info.Name;
            			meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
            		}
            	}
            	return v;
           },
			summaryRenderer: function(value) {
				return '<strong>  合计 ' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '开票金额',
			dataIndex: 'PInvoice_InvoiceAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			},
			summaryRenderer: function(value) {
				me.InvoiceMoney=value;
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		});
		return columns;
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me._resouce_columns || [],
			length = columns.length,
			fields = [];

		for(var i = 0; i < length; i++) {
			if(columns[i].dataIndex) {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? 'float' : 'string'
				};
				fields.push(obj);
			}
		}

		return fields;
	},

	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push('refresh', '-', {
			text: '发票申请',
			tooltip: '发票申请',
			iconCls: 'button-add',
			itemId: 'Add',
			name: 'Add',
			xtype: 'button',
			disabled: true,
			handler: function() {
				me.onAddClick();
			}
		});
		return buttonToolbarItems;
	},

	/**发票申请*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.apply.AddPanel', {
			resizable: true,
			formtype: 'add',
			PayOrgID: me.PayOrgID,
			PayOrgName: me.PayOrgName,
			ContractID: me.ContractID,
			ContractName: me.ContractName,
			InvoiceMoney: me.InvoiceMoney,
			VAT: me.VAT,
			ContractInvoiceMoney: me.ContractInvoiceMoney,
			title: '发票申请',
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**发票修改*/
	openEditForm: function(id, ContractID,InvoiceAmount) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.apply.AddPanel', {
			resizable: true,
			PK: id,
			formtype: 'edit',
			VAT: me.VAT,
			hastempSave: false,
			PayOrgID: me.PayOrgID,
			PayOrgName: me.PayOrgName,
			ContractID: me.ContractID,
			ContractName: me.ContractName,
			InvoiceMoney: me.InvoiceMoney,
			InvoiceAmount:InvoiceAmount,
			ContractInvoiceMoney: me.ContractInvoiceMoney,
			Status: 2,
			hastempSave: true,
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**发票查看*/
	openShowForm: function(id, ContractID) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.basic.ShowTabPanel', {
			resizable: true,
			PK: id,
			formtype: 'show',
			hasButtontoolbar: false,
			hasSave: false,
			hasDisSave: false,
			VAT: me.VAT,
			ContractID: ContractID,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];
		//合同
		if(me.ContractID) {
			params.push("pinvoice.ContractID='" + me.ContractID + "'");
		}
		params.push("pinvoice.IsUse=1");
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
});