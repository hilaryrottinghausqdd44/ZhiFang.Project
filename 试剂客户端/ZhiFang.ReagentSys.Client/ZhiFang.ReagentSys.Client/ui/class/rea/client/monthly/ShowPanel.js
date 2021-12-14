/**
 * 库存结转报表
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.monthly.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '库存结转报表信息',
	header: false,
	border: false,
	//width:680,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

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
		me.TabPanel.on({
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
		me.addEvents('save', 'onLaunchFullScreen', 'onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocForm = Ext.create('Shell.class.rea.client.monthly.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 190,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.TabPanel = Ext.create('Shell.class.rea.client.monthly.TabPanel', {
			header: false,
			itemId: 'TabPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.TabPanel, me.DocForm];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.isShow();
		me.DocForm.getForm().reset();

		me.TabPanel.PK = null;
		me.TabPanel.clearData();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
		me.DocForm.PK = null;
		me.DocForm.isAdd();

		me.TabPanel.PK = null;
		me.TabPanel.clearData();
	},
	isEdit: function(record) {
		var me = this;
		var id = record.get("ReaBmsQtyMonthBalanceDoc_Id");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = id;
		me.DocForm.isEdit(id);

		me.TabPanel.PK = id;
		me.TabPanel.loadData(record);
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsQtyMonthBalanceDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.isShow(id);

		me.TabPanel.PK = id;
		me.TabPanel.loadData(record);
	}
});