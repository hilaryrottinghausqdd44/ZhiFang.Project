/**
 * 退款申请单查看
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.show.TabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '退款申请单信息',
	border: false,
	closable: true,

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	hasLoadMask: true,

	PK: '',
	formtype: "show",
	/**显示操作记录页签	,false不显示*/
	hasOperation: true,
	formLoaded: false,
	DetailPanelIsLoad: false,
	Status: 1,
	OperationMemo: "",
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	initComponent: function() {
		var me = this;
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.DetailPanelTitle = "退款单信息";
	},

	loadDetailPanel: function() {
		var me = this;
		var Form = getComponent('DetailPanel');
		Form.load(me.PK);
		me.DetailPanelIsLoad = true;
	},
	/**加载附件信息*/
	loadAttachment: function() {
		var me = this;
		if(me.isattachmentLoad == false) {
			me.Attachment.PK = me.PK;
			me.Attachment.load();
		}
		me.isattachmentLoad = true;
	},
	loadPreviewApp: function() {
		var me = this;
		if(me.formtype != "add")
			me.PreviewApp.showPdf();
	},
	createItems: function() {
		var me = this;
		me.DetailPanel = Ext.create('Shell.class.weixin.ordersys.refund.show.DetailPanel', {
			itemId: 'DetailPanel',
			formtype: me.formtype,
			border: false,
			title: me.DetailPanelTitle,
			height: me.height,
			width: me.width,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			PK: me.PK
		});

		var items = [me.DetailPanel];
		me.Attachment = Ext.create('Shell.class.weixin.ordersys.refund.attachment.Attachment', {
			header: false,
			height: me.height,
			width: me.width,
			title: '附件信息',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory: "RefundFormAttachment",
			BusinessModuleCode: "RefundFormAttachment",
			formtype: "show"
		});
		items.push(me.Attachment);
		me.hasOperation = true;
		me.OperationPanel = Ext.create('Shell.class.weixin.ordersys.refund.operation.Panel', {
			title: '退款申请操作记录',
			header: false,
			hasButtontoolbar: false,
			hasPagingtoolbar: false,
			defaultPageSize: 500,
			hidden: !me.hasOperation,
			itemId: 'OperationPanel',
			PK: me.PK,
			border: false,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			isShowForm: false
		});
		items.push(me.OperationPanel);
		me.PreviewApp = Ext.create('Shell.class.weixin.ordersys.refund.show.PreviewPDF', {
			title: '预览退款单PDF',
			itemId: 'PreviewApp',
			hasBtntoolbar: false,
			defaultLoad: true,
			border: false,
			height: me.height,
			width: me.width,
			PK: me.PK
		});
		items.push(me.PreviewApp);
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push('->', {
			xtype: 'button',
			itemId: 'btnColse',
			iconCls: 'button-del',
			text: "关闭",
			tooltip: '关闭',
			handler: function() {
				me.onCloseClick();
			}
		});
		return items;
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'DetailPanel':
						me.loadDetailPanel();
						break;
					case 'PreviewApp':
						me.loadPreviewApp();
						break;
					default:
						break
				}
			}
		});
	},
	/**关闭*/
	onCloseClick: function() {
		var me = this;
		me.fireEvent('onCloseClick', me);
		me.close();
	}
});