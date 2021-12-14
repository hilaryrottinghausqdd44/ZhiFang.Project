/**
 * 出库查询
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.out.search.DtlPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库查询',
	header: false,
	border: false,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			},
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.loadBarCode(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.loadBarCode(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.out.basic.ShowDtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.BarCodeGrid = Ext.create('Shell.class.rea.client.barcodeoperation.dtloper.Grid', {
			header: true,
			itemId: 'BarCodeGrid',
			region: 'east',
			width: 305,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false
		});
		var appInfos = [me.DtlGrid, me.BarCodeGrid];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.BarCodeGrid.PK = null;
		me.BarCodeGrid.defaultWhere = "";
		me.BarCodeGrid.store.removeAll();
		me.BarCodeGrid.disableControl();

		me.DtlGrid.PK = null;
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		var barCodeType = me.DtlGrid.getComponent('buttonsToolbar').getComponent('DocBarCodeType');
		if(barCodeType) barCodeType.setValue("");
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsOutDoc_Id");
		me.PK = id;
		me.BarCodeGrid.clearData();

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.loadDtl(me.PK);
	},
	loadDtl: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmsoutdtl.OutDocID=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	},
	loadBarCode: function(record) {
		var me = this;
		var id = record.get("ReaBmsOutDtl_Id");
		me.BarCodeGrid.PK = id;
		me.BarCodeGrid.defaultWhere = "reagoodsbarcodeoperation.BDtlID=" + id;
		me.BarCodeGrid.onSearch();
	}
});