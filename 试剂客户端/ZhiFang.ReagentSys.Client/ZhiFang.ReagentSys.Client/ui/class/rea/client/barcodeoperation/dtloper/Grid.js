/**
 * 某一移库/出库明细的货品条码扫码信息
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.barcodeoperation.dtloper.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '货品扫码记录',
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
	/**获取条码信息的业务明细ID*/
	PK: null,
	/**批条码信息的具体业务表:入库明细表:ReaBmsInDtl;供货明细表:ReaBmsCenSaleDtl*/
	lotType: "ReaBmsOutDtl",
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',

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
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
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
		},{
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
		},  {
			dataIndex: 'ReaGoodsBarcodeOperation_BDocID',
			sortable: false,
			text: '业务主单ID',
			width: 145,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_BDtlID',
			sortable: false,
			text: '业务明细ID',
			width: 145,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsBarcodeOperation_QtyDtlID',
			sortable: false,
			text: '库存ID',
			width: 145,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//me.store.removeAll();
		me.getView().update();
		if(!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "获取条码的参数值(业务ID)为空!");
			me.getView().update(error);
			return false;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function (data) {
		var me = this;
		if (!data || data.list.length <= 0) return data;
		
		var arr=[];
		var list=[];
		for (var i = 0; i < data.list.length; i++) {
			var operTypeID = data.list[i]["ReaGoodsBarcodeOperation_OperTypeID"];
			var usePackSerial = data.list[i]["ReaGoodsBarcodeOperation_UsePackSerial"];
			var usePackQRCode = data.list[i]["ReaGoodsBarcodeOperation_UsePackQRCode"];
			var bdtlId = data.list[i]["ReaGoodsBarcodeOperation_BDtlID"];
			var str=""+bdtlId+operTypeID+usePackSerial+usePackQRCode;
			//过滤重复扫码的条码信息
			if (arr.indexOf(str)<0) {
				arr.push(str);
				list.push(data.list[i]);
			}
		}
		data.list=list;
		return data;
	}
});