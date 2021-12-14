/**
 * 客户端库存初始化(手工入库)
 * @author longfc
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.stock.manualinput.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '入库信息',
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

		me.DocForm = Ext.create('Shell.class.rea.client.stock.manualinput.basic.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 165,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.DtlPanel = Ext.create('Shell.class.rea.client.stock.manualinput.DtlPanel', {
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
		me.DocForm.StatusName = "";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlPanel.clearData(); //清空数据
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";
		me.DocForm.formtype = "add";
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.DtlPanel.formtype = "add";

		me.DtlPanel.isAdd();
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		var status = record.get("ReaBmsInDoc_Status");
		me.PK = id;
		me.formtype = "edit";

		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.setReaCompNameReadOnly(true);

		me.DtlPanel.isEdit(record);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		var status = record.get("ReaBmsInDoc_Status");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		me.DocForm.isShow(id);

		me.DtlPanel.isShow(record);
	}
});