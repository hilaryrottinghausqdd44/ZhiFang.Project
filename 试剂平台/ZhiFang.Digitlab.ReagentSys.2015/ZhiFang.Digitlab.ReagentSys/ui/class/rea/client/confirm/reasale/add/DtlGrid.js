/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.reasale.add.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.add.DtlGrid',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**是否隐藏复制列*/
	hiddenCopy: true,
	OTYPE: "reasale",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];
		items.push({
			iconCls: 'button-add',
			name: "btnChoseOrder",
			itemId: "btnChoseOrder",
			text: '供货单选择',
			tooltip: '供货单选择',
			handler: function() {
				me.onChooseSale();
			}
		});
		items.push('-', {
			xtype: 'radiogroup',
			fieldLabel: '扫码方式',
			itemId: "rboScanCode",
			width: 160,
			vertical: false,
			labelWidth: '65px',
			items: [{
					boxLabel: '接收',
					name: 'ScanCode',
					inputValue: 1,
					checked: true
				},
				{
					boxLabel: '<b style="color:red;">拒收</b>',
					name: 'ScanCode',
					inputValue: 2
				}
			]
		}, {
			xtype: 'textfield',
			width: 280,
			style: {
				marginLeft: "15px"
			},
			emptyText: '货品扫码',
			fieldLabel: '',
			name: 'txtScanCode',
			itemId: 'txtScanCode',
			handler: function(textfield, e) {
				me.onAddScanCode(e);
			}
		}, {
			xtype: 'checkboxfield',
			boxLabel: '是否显示浮动窗',
			checked:true,			
			inputValue: 1,
			name: 'cboIShowDtlInfo',
			itemId: 'cboIShowDtlInfo',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					var selected = me.getSelectionModel().getSelection();
					if(selected && selected.length > 0)
						me.onShowDtlInfo(selected[0]);
				}
			}
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**供货单导入*/
	onChooseSale: function() {
		var me = this;
		//var maxWidth = document.body.clientWidth * 0.82;
		var height = document.body.clientHeight * 0.72;
		var config = {
			resizable: true,
			SUB_WIN_NO: '1',
			width: 860,
			height: height,
			listeners: {
				accept: function(p, record) {
					me.BmsCenSaleDoc = record;
					if(record) {
						me.PK = record.get("BmsCenSaleDoc_Id");
					} else {
						me.PK = null;
						me.BmsCenSaleDoc = null;
						me.formtype = "show";
						me.defaultWhere = "";
						me.Status = null;
						me.store.removeAll();
						me.fireEvent('nodata', me);
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.confirm.choose.sale.SaleDocCheck', config);
		win.show();
	},
	/**货品扫码*/
	onAddScanCode: function(textfield, e) {
		var me = this;
	},
	onFullScreenClick: function() {
		var me = this;
		me.fireEvent('onFullScreenClick', me);
	}
});