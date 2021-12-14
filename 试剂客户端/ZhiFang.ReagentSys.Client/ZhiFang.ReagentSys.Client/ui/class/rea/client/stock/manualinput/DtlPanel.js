/**
 * 入库明细列表+条码信息列表
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.DtlPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '入库信息',
	header: false,
	border: false,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.InDtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			},
			select: function(RowModel, record) {
				me.loadBarCode(record);
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
		me.InDtlGrid = Ext.create('Shell.class.rea.client.stock.manualinput.basic.DtlGrid', {
			header: false,
			itemId: 'InDtlGrid',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.BarCodeGrid = Ext.create('Shell.class.rea.client.printbarcode.indtl.Grid', {
			header: false,
			itemId: 'BarCodeGrid',
			region: 'east',
			width: 305,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		var appInfos = [me.InDtlGrid, me.BarCodeGrid];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.BarCodeGrid.PK = null;
		me.BarCodeGrid.store.removeAll();
		me.BarCodeGrid.disableControl();

		me.InDtlGrid.PK = null;
		me.InDtlGrid.defaultWhere = "";
		me.InDtlGrid.store.removeAll();
		me.InDtlGrid.getComponent('buttonsToolbar').getComponent('DocBarCodeType').setValue("");
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
		me.clearData();
	},
	isEdit: function(record) {
		var me = this;
		me.isShow(record);
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		var status = record.get("ReaBmsInDoc_Status");
		me.PK = id;
		me.BarCodeGrid.clearData();

		me.InDtlGrid.PK = id;
		me.InDtlGrid.formtype = "show";
		me.InDtlGrid.Status = status;
		//me.InDtlGrid.buttonsDisabled = true;
		me.loadInDtl(me.PK);
	},
	loadInDtl: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmsindtl.InDocID=" + id;
		me.InDtlGrid.defaultWhere = defaultWhere;
		me.InDtlGrid.getComponent('buttonsToolbar').getComponent('DocBarCodeType').setValue("");
		me.InDtlGrid.onSearch();
	},
	loadBarCode: function(record) {
		var me = this;
		var id = record.get("ReaBmsInDtl_Id");
		me.BarCodeGrid.PK = id;
		me.BarCodeGrid.onSearch();
	}
});