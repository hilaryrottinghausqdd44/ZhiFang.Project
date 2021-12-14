/**
 * 医生奖金结算申请
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.ApplyTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '医生奖金结算申请',
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
	PK: '',
	formtype: "add",
	/**显示操作记录页签	,false不显示*/
	hasOperation: true,
	formLoaded: false,
	isSave: false,
	/*附件信息是否已经加载*/
	isattachmentLoad: false,
	/**医生奖金结算申请信息*/
	applyInfo: null,
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**新增医生奖金结算申请服务*/
	addUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonusFormAndDetails',
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
		me.title = "医生奖金结算申请信息";
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
		var items = [];
		me.BonusGrid = Ext.create('Shell.class.weixin.ordersys.settlement.doctorbonus.apply.BonusGrid', {
			itemId: 'BonusGrid',
			header: false,
			border: false
		});
		if(me.applyInfo != null && me.applyInfo.OSDoctorBonusList)
			me.BonusGrid.OSDoctorBonusList = me.applyInfo.OSDoctorBonusList;
		items.push(me.BonusGrid);

		me.Attachment = Ext.create('Shell.class.weixin.ordersys.settlement.doctorbonus.attachment.Attachment', {
			header: false,
			title: '附件信息',
			itemId: 'Attachment',
			//border: false,
			defaultLoad: false,
			formtype: "add"
		});
		items.push(me.Attachment);

		if(me.formtype != "add") {
			me.hasOperation = true;
			me.OperationPanel = Ext.create('Shell.class.weixin.ordersys.settlement.doctorbonus.operation.Panel', {
				title: '医生奖金结算操作记录',
				header: false,
				hidden: !me.hasOperation,
				itemId: 'OperationPanel',
				//border: false,
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
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: '暂存',
			tooltip: '暂存提交',
			handler: function() {
				me.onTempSaveClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: '提交',
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
		var btnTempSave = buttonsToolbar.getComponent('btnTempSave');
		var btnSave = buttonsToolbar.getComponent('btnSave');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');
		btnTempSave.setDisabled(enable);
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
	},
	onTempSaveClick: function() {
		var me = this;
		me.Status = 1;
		me.OperationMemo = "暂存";
		me.saveData();
	},
	/**提交处理方法*/
	onSaveClick: function() {
		var me = this;
		me.Status = 2;
		me.OperationMemo = "提交申请";
		me.saveData();
	},
	/*新增申请保存*/
	saveData: function() {
		var me = this;
		var isSave = true;
		var msg = "";
		if(me.applyInfo == null) {
			isSave = false;
			msg = msg + "结算数据为空!<br />";
		}
		if(isSave == true && me.applyInfo.OSDoctorBonusForm == null) {
			isSave = false;
			msg = msg + "结算数据为空!<br />";
		}
		var count1 = me.BonusGrid.store.getCount();
		if(isSave == true && count1 < 1) {
			isSave = false;
			msg = msg + "结算数据的医生奖金记录为空!<br />";
		}
		if(isSave == true) {
			var url = me.addUrl; // me.formtype == 'add' ? me.addUrl : me.editUrl;
			url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
			var bonusList = [];
			me.BonusGrid.store.each(function(record) {
				if(record.data.OSDoctorBonusFormID == "") record.data.OSDoctorBonusFormID = null;
				if(record.data.DoctorAccountID == "") record.data.DoctorAccountID = null;
				if(record.data.WeiXinUserID == "") record.data.WeiXinUserID = null;
				if(record.data.BankAccount == "") record.data.BankAccount = null;
				if(record.data.BankID == "") record.data.BankID = null;
				if(record.data.PaymentMethod == "") record.data.PaymentMethod = null;
				if(me.Status != null) record.data.Status = me.Status;
				bonusList.push(record.data);
			});

			delete me.applyInfo.OSDoctorBonusForm.DataTimeStamp;
			delete me.applyInfo.OSDoctorBonusForm.DataAddTime;
			delete me.applyInfo.OSDoctorBonusForm.DataUpdateTime;
			delete me.applyInfo.OSDoctorBonusForm.DispOrder;
			delete me.applyInfo.OSDoctorBonusForm.StatusName;
			delete me.applyInfo.OSDoctorBonusForm.BonusApplytTime;
			
			delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewFinishTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewStartTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewManID;
			
			delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewFinishTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewStartTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewManID;
			
			delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewFinishTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewStartTime;
			delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewManID;
			
			me.applyInfo.OSDoctorBonusForm.Status = me.Status;
			me.applyInfo.OperationMemo = me.OperationMemo;
			me.applyInfo.OSDoctorBonusList =bonusList;
			var params = Ext.JSON.encode({
				entity: me.applyInfo
			});
			me.showMask("数据提交保存中...");
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					me.hideMask(); //隐藏遮罩层
					var id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object') {
						id = id.id;
					}
					me.PK = id;
					me.Attachment.PK = id;
					me.Attachment.fkObjectId = id;
					me.fireEvent('save', me, id);
				} else {
					me.hideMask(); //隐藏遮罩层
					msg = me.errorFormat.replace(/{msg}/, data.msg);
					JShell.Msg.error(msg);
				}
			});
		} else {
			JShell.Msg.alert(msg, null, 2000);
		}
	}
});