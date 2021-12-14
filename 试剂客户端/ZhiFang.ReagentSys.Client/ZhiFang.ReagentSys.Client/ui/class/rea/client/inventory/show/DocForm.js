/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.show.DocForm', {
	extend: 'Shell.class.rea.client.inventory.basic.DocForm',

	title: '盘库信息',
	formtype: 'show',
	width: 420,
	height: 225,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsCheckDocAndDtlList',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/RS_UDTO_UpdateReaBmsCheckDocAndDtl',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	buttonDock: "top",
	/**盘库单Id*/
	PK: null,
	/**盘库审核是否需要确认*/
	ReaBmsCheckDocIsCheck: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents("save", "onEditClick", "onConfirmClick");
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 165) me.defaults.width = 165;
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			if(me.hasSave) items.push("save");
			if(me.hasReset) items.push('reset');
		}
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnCheck",
			text: '打印盘库单',
			tooltip: '打印选择的盘库单',
			handler: function() {
				me.onPrintClick();
			}
		});
		items.push("-", {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnConfirm",
			text: '确认盘库',
			tooltip: '确认盘库',
			handler: function() {
				me.onConfirmClick();
			}
		});
		if(items.length == 0) return null;

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}
		if(me.formtype == 'edit')
			me.fireEvent('onEditClick', me, params);	
	},
	/**@description 打印盘库*/
	onPrintClick: function() {
		var me = this;
		if(!me.PK) {
			JShell.Msg.error("请先保存盘库单后,再打印盘库单!");
			return;
		}
		if(me.Status != "1") {
			var statusName = "";
			if(me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能打印盘库清单!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf");
		url += '?operateType=1&id=' + me.PK;
		window.open(url);
	},
	/**@description 确认盘库*/
	onConfirmClick: function() {
		var me = this;
		if(me.formtype == "add") {
			JShell.Msg.error("请先进行新增盘库保存和盘库单打印后再操作!");
			return;
		}
		if(me.Status != "1") {
			var statusName = "";
			if(me.StatusEnum != null)
				statusName = me.StatusEnum[status];
			JShell.Msg.error("当前状态为【" + statusName + "】,不能执行确认盘库操作!");
			return;
		}
		var params = me.getEditParams();
		me.fireEvent('onConfirmClick', me, params);
	}
});