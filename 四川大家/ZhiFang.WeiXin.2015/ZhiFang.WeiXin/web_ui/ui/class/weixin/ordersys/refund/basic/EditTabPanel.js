/**
 * 退款申请后的流程
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.basic.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '退款申请',
	border: false,
	closable: true,
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormByField',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	hasLoadMask: true,
	width: 760,
	height: 560,
	PK: '',
	formtype: "edit",
	/**显示操作记录页签	,false不显示*/
	hasOperation: true,
	formLoaded: false,
	DetailPanelIsLoad: false,
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**是否隐藏通过按钮*/
	hiddenPass: false,
	/**是否隐藏退回按钮*/
	hiddenRetract: false,
	/**通过按钮显示文字*/
	btnPassText: "审核通过",
	isSave: false,
	RefundFormCode:'',
	/*附件信息是否已经加载*/
	isattachmentLoad: false,
	/*审核等的录入意见的弹出窗体*/
	ViewInfoWin: null,

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
		me.on({
			save: function(p, id) {
				me.Attachment.save();
			}
		});
		me.Attachment.on({
			//附件上所有操作处理完
			save: function(win, e) {
				me.fireEvent('save', me);
				if(e.success) {
					me.close();
				} else {
					me.enableControl(false); //启用所有的操作功能
				}
			},
			uploadcomplete: function(win, e) {
				me.fireEvent('save', me);
				JShell.Msg.alert(me.Attachment.progressMsg);
			}
		});
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.DetailPanelTitle = "退款申请单信息";
	},
	/**加载附件信息*/
	loadAttachment: function() {
		var me = this;
		if(me.isattachmentLoad == false && me.formtype == 'edit') {
			me.Attachment.PK = me.PK;
			me.Attachment.load();
		}
		me.isattachmentLoad = true;
	},
	loadDetailPanel: function() {
		var me = this;
		var Form = me.getComponent("FormApp").getComponent('DetailPanel');
		if(me.DetailPanelIsLoad == false && me.formtype == 'edit') {
			Form.load(me.PK);
		}
		me.DetailPanelIsLoad = true;
	},
	loadDafultData: function() {
		var me = this;
		var id = me.PK;
		var Form = me.getComponent("FormApp").getComponent('DetailPanel');
		if(me.formtype == 'edit') {
			Form.isEdit(me.PK);
		}
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
			formtype: 'show',
			border: false,
			height: me.height,
			width: me.width,
			StatusList: me.StatusList,
			StatusEnum: me.StatusEnum,
			StatusFColorEnum: me.StatusFColorEnum,
			StatusBGColorEnum: me.StatusBGColorEnum,
			PK: me.PK
		});

		if(me.formtype == 'edit') {
			me.hasOperation = true;
		}
		var items = [me.DetailPanel];
		me.Attachment = Ext.create('Shell.class.weixin.ordersys.refund.attachment.Attachment', {
			//region: 'center',
			header: false,
			height: me.height,
			width: me.width,
			title: '附件信息',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			formtype: "edit"
		});
		items.push(me.Attachment);
		if(me.formtype != "add") {
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
				title: '预览PDF',
				itemId: 'PreviewApp',
				hasBtntoolbar: false,
				defaultLoad: true,
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
		items.push("->");

		items.push("-");

		items.push({
			xtype: 'button',
			itemId: 'btnPass',
			iconCls: 'button-save',
			hidden: me.hiddenPass,
			text: me.btnPassText,
			tooltip: me.btnPassText,
			handler: function() {
				me.onPassClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnRetract',
			iconCls: 'button-save',
			text: "退回",
			tooltip: '退回',
			hidden: me.hiddenRetract,
			handler: function() {
				me.onRetractClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnReset',
			iconCls: 'button-reset',
			text: "重置",
			tooltip: '重置',
			hidden: true,
			handler: function() {
				me.onResetClick();
			}
		});
		items.push({
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
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(false); //启用所有的操作功能
	},

	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var btnPass = buttonsToolbar.getComponent('btnPass');
		var btnRetract = buttonsToolbar.getComponent('btnRetract');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');

		btnPass.setDisabled(enable);
		btnRetract.setDisabled(enable);
		btnReset.setDisabled(enable);
		btnColse.setDisabled(enable);
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**关闭*/
	onCloseClick: function() {
		var me = this;
		me.fireEvent('onCloseClick', me);
		me.close();
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		var Form = getComponent('DetailPanel');
		//基本表单重置
		if(!Form.PK) {
			Form.getForm().reset();
		} else {
			Form.getForm().setValues(Form.lastData);
		}
	},
	/**通过处理方法*/
	onPassClick: function() {
		var me = this;
	},
	/**@overwrite 撤回按钮点击处理方法*/
	onRetractClick: function() {

	},
	updateStatus: function(params) {
		var me = this;
		var memo ="";
		var params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.hideMask(); //隐藏遮罩层
				JShell.Msg.alert(memo + "操作成功", null, 1000);
				me.fireEvent('save', me);
				me.ViewInfoWin.close();
			} else {
				me.hideMask();
				JShell.Msg.error(memo + "操作失败!<br />" + data.msg);
			}
		});
	},
	entityValues: {
		ViewInfo: "",
		PayDate: ''
	},
	openViewInfoForm: function(title, callback) {
		var me = this;
		me.isSave = false;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: '3',
			resizable: false,
			title: title,
			formtype: 'add',
			zindex: 10,
			listeners: {
				save: function(win, values) {
					me.entityValues = values;
					me.isSave = true;
					me.ViewInfoWin = win;
					if(callback != null) {
						callback();
					}
					//win.close();
				}
			}
		};
		JShell.Win.open('Shell.class.weixin.ordersys.refund.basic.ViewInfoForm', config).show();
	}
});