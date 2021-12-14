/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '库存结转',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,

	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.DocForm.on({
			save: function(p, id) {
				me.fireEvent('save', me, id);
			}
		});
		me.DtlGrid.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save','onLaunchFullScreen','onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.qtybalance.basic.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.qtybalance.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 175,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.getForm().reset();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
		me.DocForm.isAdd();;

		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.store.removeAll();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsQtyBalanceDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = id;
		me.DocForm.isEdit(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = status;
		me.loadDtlGrid(me.PK);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsQtyBalanceDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.loadDtlGrid(me.PK);
	},
	loadDtlGrid: function(id) {
		var me = this;
		if(!id)
			var defaultWhere = "";
		if(id) defaultWhere = "reabmsqtybalancedtl.QtyBalanceDocID=" + id;
		me.DtlGrid.defaultWhere = defaultWhere;
		me.DtlGrid.onSearch();
	}
});