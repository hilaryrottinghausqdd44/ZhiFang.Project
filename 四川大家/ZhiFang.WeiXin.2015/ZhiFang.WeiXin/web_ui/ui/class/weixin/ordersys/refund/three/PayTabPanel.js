/**
 * 退款发放
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.three.PayTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '退款发放',
	border: false,
	closable: true,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	hasLoadMask: true,
	width: 630,
	height: 340,
	/**用户订单ID*/
	UOFID:'',
	PK: '',
	formtype: "add",
	/**显示操作记录页签	,false不显示*/
	hasOperation: true,
	formLoaded: false,
	isSave: false,
	/*附件信息是否已经加载*/
	isattachmentLoad: false,
	RefundFormCode:'',
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormThreeReview',
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
		me.FormTitle = "退款申请单信息";
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

	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.weixin.ordersys.refund.three.Form', {
			itemId: 'Form',
			formtype: me.formtype,
			border: false,
			height: me.height,
			width: me.width,
			UOFID:me.UOFID,
			PK: me.PK
		});

		if(me.formtype == 'edit') {
			me.hasOperation = true;
		}
		var items = [me.Form];
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
				title: '退款操作记录',
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
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: '发放',
			tooltip: '提交保存',
			handler: function() {
				me.onSaveClick();
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
					case 'Form':
						me.loadForm();
						break;
					case 'Attachment':
						me.loadAttachment();
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
		var btnSave = buttonsToolbar.getComponent('btnSave');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');

		btnSave.setDisabled(enable);
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
		var Form = me.getComponent('Form');
		//基本表单重置
		if(!Form.PK) {
			Form.getForm().reset();
		} else {
			Form.getForm().setValues(Form.lastData);
		}
	},
	/**通过处理方法*/
	onSaveClick: function() {
		var me = this;
		me.saveData();
	},
	/*新增及编辑保存*/
	saveData: function() {
		var me = this;
		var Form = me.getComponent('Form');
		var isValid = Form.getForm().isValid();
		if(!isValid) {
			JShell.Msg.alert("表单验证不通过!", null, 2000);
			return;
		};
		var values = Form.getForm().getValues();
		var com = Form.getComponent('RefundType');
		var checkbox = com.getChecked();
		if(checkbox == null || checkbox.length < 1) {
			JShell.Msg.alert("退款方式不能为空!", null, 2000);
			return;
		}
		var url =me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params =Form.getAddParams();
		params.entity.Result=1;
		params.entity.RefundFormCode=me.RefundFormCode;
		if(!params) return;
		
		params = Ext.JSON.encode(params);
		me.showMask("数据提交保存中...");
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.hideMask(); //隐藏遮罩层
				me.fireEvent('save', me, me.PK);
			} else {
				me.hideMask(); //隐藏遮罩层
				JShell.Msg.alert("保存失败", null, 1000);
			}
		});
	}
});