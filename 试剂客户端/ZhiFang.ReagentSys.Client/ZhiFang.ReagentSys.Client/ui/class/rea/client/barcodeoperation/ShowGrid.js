/**
 * 货品盒条码操作记录
 * @author longfc
 * @version 2019-01-08
 */
Ext.define('Shell.class.rea.client.barcodeoperation.ShowGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	
	title: '货品盒条码操作记录',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsBarcodeOperationByHQL?isPlanish=true',
	
	defaultOrderBy: [{
		property: 'ReaGoodsBarcodeOperation_BarcodeCreatType',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_DataAddTime',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_OperTypeID',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsBarcodeOperation_DispOrder',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',
	/**操作记录操作类型*/
	OperTypeKey: "ReaGoodsBarcodeOperType",
	
	/**用户UI配置Key*/
	userUIKey: 'barcodeoperation.ShowGrid',
	/**用户UI配置Name*/
	userUIName: "盒条码操作记录列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: me.cellpluginId
		});
		JShell.REA.StatusList.getStatusList(me.OperTypeKey, false, true, null);
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaGoodsBarcodeOperation_ReaGoodsNo',
			text: '货品编码',
			width: 110,
			renderer: function(value, meta,record ) {
				var v = record.get("ReaGoodsBarcodeOperation_OverageQty");
				if(v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsCName',
			text: '货品名称',
			width: 150,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsBarcodeOperation_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsUnit',
			text: '包装单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BarcodeCreatType',
			text: '生成类型',
			width: 70,
			renderer: function(value, meta, record) {
				var v = '否';
				var style = 'font-weight:bold;';
				meta.style = style;
				if(value && parseInt(value) == 1) {
					style = style + "background-color:#ffffff;color:#1c8f36;";
					v = '条码生成';
				} else {
					style = style + "background-color:#ffffff;color:#dd6572;";
					v = '货品扫码';
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_OperTypeID',
			text: '操作类型',
			width: 75,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.OperTypeKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.OperTypeKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.OperTypeKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.OperTypeKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.OperTypeKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.OperTypeKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_PrintCount',
			text: '已打印',
			width: 50,
			align: 'center',
			renderer: function(value, meta, record) {
				var v = '否';
				var style = 'font-weight:bold;';
				meta.style = style;
				if(value && parseInt(value) > 0) {
					style = style + "background-color:#ffffff;color:#1c8f36;";
					v = '是';
				} else {
					style = style + "background-color:#ffffff;color:red;";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_PUsePackSerial',
			sortable: false,
			width: 145,
			text: '父条码',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UsePackSerial',
			sortable: false,
			width: 110,
			//hidden: true,
			text: '一维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_UsePackQRCode',
			sortable: false,
			width: 505,
			text: '二维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_DispOrder',
			text: '顺序号',
			width: 55,
			type: 'int',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_MinBarCodeQty',
			text: '最小单位条码数',
			width: 95,
			type: 'float',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_ScanCodeQty',
			text: '当次扫码数',
			width: 75,
			type: 'float',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_ScanCodeGoodsUnit',
			text: '扫码单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_OverageQty',
			text: '剩余条码数',
			width: 80,
			type: 'float',
			renderer: function(value, meta) {
				var v = value;
				if(v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsQty',
			text: '条码数量',
			hidden: true,
			width: 70,
			type: 'float',
			align: 'right',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			isKey: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_DataAddTime',
			text: '操作时间',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_CreaterName',
			sortable: false,
			text: '操作人',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDocNo',
			sortable: false,
			text: '业务单据号',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDocID',
			sortable: false,
			text: '业务主单ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDtlID',
			sortable: false,
			text: '业务明细ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_QtyDtlID',
			sortable: false,
			text: '库存ID',
			width: 145,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_GoodsID',
			sortable: false,
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		return items;
	}
});