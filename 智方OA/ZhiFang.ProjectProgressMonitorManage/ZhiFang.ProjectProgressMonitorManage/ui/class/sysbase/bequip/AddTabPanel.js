/**
 * 新增仪器信息
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.sysbase.bequip.AddTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '新增仪器信息',
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
	/**字典类型里的仪器类型ID*/
	EquipTypeId: '',
	EquipTypeCName: '',
	/**字典类型里的仪器厂商品牌ID*/
	EquipFactoryBrandId: "",
	EquipFactoryBrandCName: '',

	/*仪器厂商品牌ID**/
	EBRADID: '4777630349498328266',
	/*仪器分类**/
	ETYPEID: '5724611581318422977',
	PK: '',
	formtype: "add",

	isLoadbasicForm:true,
	isContentLoad: false,
	isattachmentLoad: false,

	basicFormApp: '',
	initComponent: function() {
		var me = this;
		me.width = me.width;
		me.basicFormApp = me.basicFormApp || "Shell.class.sysbase.bequip.Form";
		me.bodyPadding = 1;
		me.formtype = me.formtype || "add",
			me.title = me.title || "";
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		//文档基本信息处理
		var Form = me.getComponent('basicForm');
		var Attachment = me.getComponent('Attachment');

		Form.on({
			save: function() {
				me.Attachment.save();
			}
		});
		Attachment.on({
			//附件上所有操作处理完
			save: function(win, e) {
				me.fireEvent('save', me);
				if(e.success) {
					me.close();
				}else{
					me.enableControl(false); //启用所有的操作功能
				}
			},
			uploadcomplete: function(win, e) {
				me.fireEvent('save', me);
				JShell.Msg.alert(Attachment.progressMsg);
			}
		});
		//页签切换处理
		me.ontabchange();
		me.activeTab = 0;
		if(me.formtype == "edit") {
			me.loadContentForm();
			me.loadAttachment();
		}
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "仪器基本信息";
		me.contentFormTitle = "仪器详细说明";
	},
	/*仪器基本信息**/
	loadbasicForm: function() {
		var me = this;
		if(me.isLoadbasicForm == false && me.basicForm.formtype == 'edit') {
			me.basicForm.load(me.PK);
		}
		me.isLoadbasicForm = true;
	},
	/**加载仪器内容信息*/
	loadContentForm: function() {
		var me = this;
		if(me.isContentLoad == false && me.ContentForm.formtype == 'edit') {
			me.ContentForm.load(me.PK);
		}
		me.isContentLoad = true;
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
	loadDafultData: function() {
		var me = this;
		me.contentIsLoad = false;
		if(me.formtype == 'edit') {
			me.basicForm.isEdit(me.PK);
			JShell.Action.delay(function() {
				me.loadContentForm();
			}, null, 200);
		}
	},
	createItems: function() {
		var me = this;
		me.basicForm = Ext.create(me.basicFormApp, {
			itemId: 'basicForm',
			formtype: me.formtype,
			hasSave: false,
			hasReset: me.hasReset,
			border: false,
			title: me.basicFormTitle || '仪器基本信息',
			/**字典类型里的仪器类型ID*/
			EquipTypeId: me.EquipTypeId,
			EquipTypeCName: me.EquipTypeCName,
			/**字典类型里的仪器厂商品牌ID*/
			EquipFactoryBrandId: me.EquipFactoryBrandId,
			EquipFactoryBrandCName: me.EquipFactoryBrandCName,
			/*仪器厂商品牌ID**/
			ETYPEID: me.ETYPEID,
			/*仪器分类**/
			EBRADID: me.EBRADID,
			height: me.height,
			width: me.width,
			PK: me.PK,
		});
		me.ContentForm = Ext.create('Shell.class.sysbase.bequip.ContentForm', {
			title: me.contentFormTitle || '仪器详细说明',
			header: false,
			height: me.height,
			width: me.width,
			itemId: 'ContentForm',
			border: false,
			PK: me.PK,
			formtype: me.formtype
		});

		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			region: 'center',
			header: false,
			title: me.uploadTitle || '附件信息',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory:"BEquip",
			BusinessModuleCode:"BEquip",
			formtype: me.formtype
		});
		return [me.basicForm, me.ContentForm, me.Attachment];
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
	/**监听*/
	doNextExecutorListeners: function() {},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push("->");
		items.push({
			xtype: 'button',
			itemId: 'btnRelease',
			iconCls: 'button-save',
			text: "确认提交",
			tooltip: "确认提交",
			handler: function() {
				me.onSaveClick();
			}
		});
		items.push({
			xtype: 'button',
			itemId: 'btnReset',
			iconCls: 'button-reset',
			text: "重置",
			tooltip: '重置',
			handler: function() {
				me.onResetClick();
			}
		},{
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
					case 'ContentForm':
						me.loadContentForm();
						break;
					case 'basicForm':
						if(me.isLoadbasicForm == false) {
							me.loadbasicForm();
						}
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

	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		//基本表单重置
		if(!me.basicForm.PK) {
			me.basicForm.getForm().reset();
		} else {
			me.basicForm.getForm().setValues(me.basicForm.lastData);
		}
		//详细内容表单重置
		if(!me.ContentForm.PK) {
			me.ContentForm.getForm().reset();
		} else {
			me.ContentForm.getForm().setValues(me.ContentForm.lastData);
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},

	/**提交点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var isExec = true,
			itemId = "",
			msg = "";
		var form = me.getComponent('basicForm');
		me.saveBEquip();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');

		var values = Form.getForm().getValues();
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var IsUse = 1; //隐藏,全是在用 values.FFile_IsUse ? 1 : 0;

		var entity = Form.getAddParams();
		//内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var content = contentvalues.BEquip_Content;
		if(content && content != 'undefined') {
			entity.entity.Content = content.replace(/\\/g, '&#92');
		}
		return entity;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var entity = Form.getEditParams();
		//内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var content = contentvalues.BEquip_Content;
		if(content && content != 'undefined') {
			entity.entity.Content = content.replace(/\\/g, '&#92');
		}
		return entity;
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var btnRelease = buttonsToolbar.getComponent('btnRelease');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');
		
		btnRelease.setDisabled(enable);
		btnReset.setDisabled(enable);
		btnColse.setDisabled(enable);
	},
	/**禁用所有的操作功能*/
	disableControl:function(){
		this.enableControl(true);
	},
	/*仪器新增及编辑保存*/
	saveBEquip: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		var isValid = Form.getForm().isValid();
		if(!values.BEquip_CName || values.BEquip_CName == "") {
			JShell.Msg.alert("仪器名称不能为空", null, 1000);
			return;
		}
		if(!isValid) return;

		var url = Form.formtype == 'add' ? Form.addUrl : Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = Form.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		if(Form.formtype == "edit" && Form.PK == "") return;

		params = Ext.JSON.encode(params);
		me.disableControl(); //禁用所有的操作功能
		JShell.Server.post(url, params, function(data) {
			//Form.hideMask(); //隐藏遮罩层
			if(data.success) {
				if(Form.formtype == "add") {
					me.PK = data.value.id;
					me.Attachment.PK = data.value.id;
					me.Attachment.fkObjectId = data.value.id;
				}
				Form.fireEvent('save');

				if(Form.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				me.enableControl(false); //启用所有的操作功能
				JShell.Msg.error(msg);
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