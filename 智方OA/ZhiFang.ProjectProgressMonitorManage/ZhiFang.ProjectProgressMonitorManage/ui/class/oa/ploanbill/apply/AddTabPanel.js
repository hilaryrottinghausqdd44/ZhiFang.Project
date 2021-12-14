/**
 * 借款单申请
 * @author longfc
 * @version 2016-11-09
 */
Ext.define('Shell.class.oa.ploanbill.apply.AddTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '借款申请',
	border: false,
	closable: true,
	/**是否重置按钮*/
	hasReset: false,
	/**是否启用取消按钮*/
	hasCancel: false,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	hasLoadMask: true,
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBillByField',
	PK: '',
	formtype: "",
	/**显示操作记录页签	,false不显示*/
	hasOperation: false,
	formLoaded: false,
	basicFormIsLoad: false,
	Status: 1,
	OperationMemo: "",
	StatusList: [],
	StatusEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	ApplyManID: null,
	isattachmentLoad: false,
	/*是否隐藏撤回按钮*/
	hiddenButtonRetract:true,
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "add";
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
		me.basicFormTitle = "借款申请";
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
	/**加载仪器附件信息*/
	loadAttachment: function() {
		var me = this;
		if(me.isattachmentLoad == false && me.formtype == 'edit') {
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
		me.FormApp = Ext.create('Shell.class.oa.ploanbill.apply.FormApp', {
			itemId: 'FormApp',
			formtype: me.formtype,
			border: false,
			title: me.basicFormTitle,
			height: me.height,
			width: me.width,
			ApplyManID: me.ApplyManID,
			PK: me.PK
		});
		var items = [me.FormApp];
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
				border: false,
				height: me.height,
				width: me.width,
				hasBtntoolbar:false,
				defaultLoad:true,
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
		items.push({
			xtype: 'button',
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "暂存",
			tooltip: "临时暂存",
			handler: function() {
				me.onTempSaveClick();
			}
		});
		items.push({
			xtype: 'button',
			itemId: 'btnApply',
			iconCls: 'button-save',
			text: "提交申请",
			tooltip: "提交申请",
			handler: function() {
				me.onApplyClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnRetract',
			iconCls: 'button-save',//back
			hidden:me.hiddenButtonRetract,
			text: "撤回",
			tooltip: '撤回',
			handler: function() {
				me.onRetractClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnReset',
			iconCls: 'button-reset',
			text: "重置",
			tooltip: '重置',
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
		var btnTempSave = buttonsToolbar.getComponent('btnTempSave');
		var btnRetract = buttonsToolbar.getComponent('btnRetract');
		var btnApply = buttonsToolbar.getComponent('btnApply');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');

		btnTempSave.setDisabled(enable);
		btnRetract.setDisabled(enable);
		btnApply.setDisabled(enable);
		btnReset.setDisabled(enable);
		btnColse.setDisabled(enable);
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent("FormApp").getComponent('basicForm');
		var entity=Form.getAddParams();
		entity.entity.Status=me.Status;
		entity.entity.OperationMemo=me.OperationMemo;
		return entity;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		var Form = me.getComponent("FormApp").getComponent('basicForm');
		var entity=Form.getEditParams();
		entity.entity.Status=me.Status;
		entity.entity.OperationMemo=me.OperationMemo;
		return entity;
	},
	/*文档新增及编辑保存*/
	saveffile: function() {
		var me = this;
		var Form = me.getComponent("FormApp").getComponent('basicForm');
		var values = Form.getForm().getValues();
		var isValid = Form.getForm().isValid();

		if(!isValid) {
			JShell.Msg.alert("表单验证不通过!", null, 1000);
			return;
		};
		var com = Form.getComponent('ReceiveTypeID');
		var checkbox = com.getChecked();
		if(checkbox == null || checkbox.length < 1) {
			JShell.Msg.alert("请选择领款方式!", null, 1000);
			return;
		}
		if(!values.ApplyMan || values.ApplyMan == "") {
			JShell.Msg.alert("借款人不能为空!", null, 1000);
			return;
		}
		var url = Form.formtype == 'add' ? Form.addUrl : Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = Form.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		
		if(Form.formtype == "edit" && Form.PK == "") {
			return;
		}
		params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.hideMask(); //隐藏遮罩层
				var id = Form.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				me.PK = id;
				me.Attachment.PK = id;
				me.Attachment.fkObjectId = id;
				Form.PK = id;
				//Form.formtype = 'edit';
				me.fireEvent('save', me, id);
				//if(Form.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
			} else {
				me.hideMask(); //隐藏遮罩层
				JShell.Msg.alert("保存操作失败", null, 1000);
			}
		});
	},

	/**临时保存*/
	onTempSaveClick: function() {
		var me = this;
		me.fireEvent('onTempSaveClick', me);
		me.OperationMemo = "借款单暂存";
		me.Status = 1;
		me.onSaveClick();
	},
	/**提交点击处理方法*/
	onSaveClick: function() {
		var me = this;
		me.saveffile();
	},
	/**提交申请*/
	onApplyClick: function() {
		var me = this;
		me.fireEvent('onApplyClick', me);
		me.OperationMemo = "借款单确认提交申请";
		me.Status = 2;
		me.onSaveClick();
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
	/**@overwrite 撤回按钮点击处理方法*/
	onRetractClick: function() {
		var me = this;
		var entity = {
			Id: me.PK,
			Status: 1,
			OperationMemo: "借款单撤回"
		};
		var fields = ['Id', 'Status'];
		var params = {
			entity: entity,
			fields: fields.join(',')
		};
		var params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.hideMask(); //隐藏遮罩层
				me.fireEvent('save', me, me.PK);
				JShell.Msg.alert("撤回操作成功", null, 1000);
			} else {
				me.hideMask();
				JShell.Msg.alert("撤回操作失败", null, 1000);
			}
		});
	}
});