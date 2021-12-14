/**
 * 借款单申请后的流程
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.basic.EditTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '借款单审核',
	border: false,
	closable: true,
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBillByField',
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
	basicFormIsLoad: false,
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**是否隐藏通过按钮*/
	hiddenPass: false,
	/**是否隐藏区域内审核按钮*/
	hiddenSpecially: true,
	/**是否隐藏退回按钮*/
	hiddenRetract: false,
	/**通过按钮显示文字*/
	btnPassText: "审核通过",
	isSave: false,
	/**是否隐藏审核信息*/
	isHiddenReview: true,
	/**是否隐藏借款核对信息*/
	isHiddenTwoReview: true,
	/**是否隐藏特殊审批信息*/
	isHiddenThreeReview: true,
	/**是否隐藏借款复核信息*/
	isHiddenFourReview: true,
	/**是否隐藏打款信息*/
	isHiddenPay: true,
	/**是否隐藏领款信息*/
	isHiddenReceive: true,
	/**申请人ID*/
	ApplyManID: null,
	/*附件信息是否已经加载*/
	isattachmentLoad: false,
	/*是否隐藏打款日期*/
	hiddenPayDate: true,
	/*审核等的录入意见的弹出窗体*/
	ViewInfoWin: null,
	FormAppClass: 'Shell.class.oa.ploanbill.show.FormApp',
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
		me.basicFormTitle = "借款单信息";
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
		}
	},
	loadPreviewApp: function() {
		var me = this;
		if(me.formtype != "add")
			me.PreviewApp.showPdf();
	},
	createItems: function() {
		var me = this;

		me.FormApp = Ext.create(me.FormAppClass, {
			itemId: 'FormApp',
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

		if(me.formtype == 'edit') {
			me.hasOperation = true;
		}
		var items = [me.FormApp];
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			//region: 'center',
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
			formtype: "edit"
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
			itemId: 'btnSpecially',
			iconCls: 'button-save',
			hidden: me.hiddenSpecially,
			text: "特殊审批",
			tooltip: '特殊审批',
			handler: function() {
				me.onSpeciallyClick();
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
		var btnSpecially = buttonsToolbar.getComponent('btnSpecially');

		btnPass.setDisabled(enable);
		btnRetract.setDisabled(enable);
		btnReset.setDisabled(enable);
		btnColse.setDisabled(enable);
		btnSpecially.setDisabled(enable);
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
		var Form = me.getComponent("FormApp").getComponent('basicForm');
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
	/**@overwrite 区域内审核按钮点击处理方法*/
	onSpeciallyClick: function() {
		var me = this;
	},
	/**@overwrite 撤回按钮点击处理方法*/
	onRetractClick: function() {

	},

	updateStatus: function(params) {
		var me = this;
		var memo = params.entity.OperationMemo;
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
			hiddenPayDate: me.hiddenPayDate,
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
		JShell.Win.open('Shell.class.oa.ploanbill.basic.ViewInfoForm', config).show();
	}
});