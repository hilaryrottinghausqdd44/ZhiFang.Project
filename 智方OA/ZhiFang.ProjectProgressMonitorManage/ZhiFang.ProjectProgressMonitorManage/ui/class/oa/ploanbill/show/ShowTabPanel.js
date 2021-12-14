/**
 * 借款单查看
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.show.ShowTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '借款单信息',
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
	basicFormIsLoad: false,
	Status: 1,
	OperationMemo: "",
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	hiddenSpecially: true,
	/**是否隐藏审核信息*/
	isHiddenReview: false,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: false,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: false,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: false,
	/**是否隐藏打款信息*/
	isHiddenPay: false,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	/**申请人ID*/
	ApplyManID: null,
	isattachmentLoad: false,
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
		me.basicFormTitle = "借款单信息";
	},

	loadbasicForm: function() {
		var me = this;
		var Form = me.getComponent("FormApp").getComponent('basicForm');
		if(me.basicFormIsLoad == false && me.formtype == 'edit') {
			Form.load(me.PK);
		}
		me.basicFormIsLoad = true;
	},
	loadDafultData: function() {
		var me = this;
		var id = me.PK;
		var Form = me.getComponent("FormApp").getComponent('basicForm');
		if(me.formtype == 'edit') {
			Form.isEdit(me.PK);
			JShell.Action.delay(function() {

			}, null, 200);
		}
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

		me.basicForm = Ext.create('Shell.class.oa.ploanbill.show.FormApp', {
			itemId: 'basicForm',
			formtype: me.formtype,
			border: false,
			title: me.basicFormTitle,
			height: me.height,
			width: me.width,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			/**是否隐藏审核信息*/
			isHiddenReview: me.isHiddenReview,
			/**是否隐藏借款核对信息*/
			isHiddenTwoReview: me.isHiddenTwoReview,
			/**是否隐藏特殊审批信息*/
			isHiddenThreeReview: me.isHiddenThreeReview,
			/**是否隐藏借款复核信息*/
			isHiddenFourReview: me.isHiddenFourReview,
			/**是否隐藏打款信息*/
			isHiddenPay: me.isHiddenPay,
			/**是否隐藏领款信息*/
			isHiddenReceive: me.isHiddenReceive,
			ApplyManID: me.ApplyManID,
			PK: me.PK
		});

		var items = [me.basicForm];
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			header: false,
			height: me.height,
			width: me.width,
			title: '附件信息',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory: "PLoanBill",
			BusinessModuleCode: "PLoanBill",
			formtype: "show"
		});
		items.push(me.Attachment);
		if(me.formtype != "add") {
			me.hasOperation = true;
			me.OperationForm = Ext.create('Shell.class.oa.sc.operation.Grid', {
				title: '操作记录',
				header: false,
				hasButtontoolbar: false,
				hasPagingtoolbar: false,
				defaultPageSize: 500,
				hidden: !me.hasOperation,
				itemId: 'OperationForm',
				PK: me.PK,
				border: false,
				StatusList: me.StatusList,
				StatusEnum: me.StatusEnum,
				StatusFColorEnum: me.StatusFColorEnum,
				StatusBGColorEnum: me.StatusBGColorEnum,
				isShowForm: false
			});
			items.push(me.OperationForm);
			me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
				title: '交流信息',
				FormPosition: 'e',
				PK: me.PK
			});
			items.push(me.Interaction);
			me.PreviewApp = Ext.create('Shell.class.oa.ploanbill.basic.PreviewApp', {
				title: '预览PDF',
				itemId: 'PreviewApp',
				hasBtntoolbar:false,
				defaultLoad:true,
				border: false,
				height: me.height,
				width: me.width,
				PK: me.PK
			});
			items.push(me.PreviewApp);
		}

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
					case 'basicForm':
						me.loadbasicForm();
						break;
					case 'Attachment':
						me.loadAttachment();
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