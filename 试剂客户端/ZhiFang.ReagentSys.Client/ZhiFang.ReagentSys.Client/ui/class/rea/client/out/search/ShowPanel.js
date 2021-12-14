/**
 * 出库查询
 * @author longfc
 * @version 2019-03-12
 */
Ext.define('Shell.class.rea.client.out.search.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库查询',
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
		me.DtlPanel.on({
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
		me.addEvents('onLaunchFullScreen', 'onExitFullScreen');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.DocForm = Ext.create('Shell.class.rea.client.out.search.Form', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 185,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.DtlPanel = Ext.create('Shell.class.rea.client.out.search.DtlPanel', {
			header: false,
			itemId: 'DtlPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlPanel, me.DocForm];
		return appInfos;
	},
	nodata: function() {
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DtlPanel.clearData(); //清空数据
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsOutDoc_Id");
		me.PK = id;
		me.formtype = "show";
		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		me.DocForm.isShow(id);
		me.DtlPanel.isShow(record);
	}
});