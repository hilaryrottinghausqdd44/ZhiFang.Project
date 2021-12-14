/**
 * 某一入库明细的货品条码信息
 * @author longfc
 * @version 2019-12-13
 */
Ext.define('Shell.class.rea.client.transfer.basic.BarCodeGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '货品条码信息',
	/**获取数据服务路径*/
	selectUrl: '',
	/**获取条码信息的业务明细ID*/
	PK: null,
	/**当前扫码信息*/
	CurScanCodeList:[],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**编辑单元格pluginId*/
	cellpluginId: 'cellpluginId',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if (me.CurScanCodeList.length>0) {
			me.store.loadData(me.CurScanCodeList);
		}
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: me.cellpluginId
		});
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		//me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'DispSerial',
			sortable: false,
			width: 120,
			hidden: true,
			text: '显示条码',
			renderer: function(value, meta, record) {
				var v = value;
				var usePackSerial = record.get("UsePackSerial");
				var usePackQRCode = record.get("UsePackQRCode");
				return v;
			}
		},{
			dataIndex: 'UsePackSerial',
			sortable: false,
			width: 140,
			text: '一维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		},{
			dataIndex: 'UsePackQRCode',
			sortable: false,
			flex:1,
			width: 480,
			text: '二维条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		},{
			dataIndex: 'SysPackSerial',
			sortable: false,
			width: 120,
			hidden: true,
			text: '系统条码',
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		},{
			dataIndex: 'GoodsCName',
			sortable: false,
			hidden: true,
			width: 120,
			text: '货品名称',
			defaultRenderer: true
		},{
			dataIndex: 'GoodsUnit',
			sortable: false,
			width: 55,
			text: '单位',
			defaultRenderer: true
		},{
			dataIndex: 'MinBarCodeQty',
			sortable: false,
			width: 85,
			text: '最小单位数',
			defaultRenderer: true
		},{
			dataIndex: 'ScanCodeQty',
			sortable: false,
			width: 75,
			hidden: true,
			text: '扫码数',
			defaultRenderer: true
		},{
			dataIndex: 'OverageQty',
			sortable: false,
			width: 75,
			hidden: true,
			text: '剩余数',
			defaultRenderer: true
		},{
			dataIndex: 'QtyDtlID',
			sortable: false,
			width: 75,
			hidden: true,
			text: '库存Id',
			defaultRenderer: true
		},{
			dataIndex: 'GoodsID',
			sortable: false,
			width: 75,
			hidden: true,
			text: '货品Id',
			defaultRenderer: true
		},{
			dataIndex: 'ScanCodeGoodsID',
			sortable: false,
			width: 120,
			hidden: true,
			text: '扫码货品Id',
			defaultRenderer: true
		},{
			dataIndex: 'BarCodeType',
			sortable: false,
			width: 120,
			hidden: true,
			text: '条码类型Id',
			defaultRenderer: true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = me.callParent(arguments);
		//if(me.PK) url = url + "&inDtlId=" + me.PK;
		return url;
	}
});