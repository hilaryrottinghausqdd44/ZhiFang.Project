/**
 * 服务管理
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.service.accept.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '服务管理',
	width: 840,
	height: 500,
	autoScroll: false,
	PK: '',
	formtype: '',
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			isValid: function(p) {
				me.setActiveTab(me.Form);
			},
//			beforesave: function(p) {
//				me.showMask(me.saveText);
//			},
			aftersave: function(p, id) {
//				me.hideMask();
				me.PK = id;
				me.onSaveAttachment(id);
			}
		});
		me.Attachment.on({
			save: function(win, data) {
				if(data.success) {
					me.fireEvent('save', me, me.PK);
				} else {
					JShell.Msg.error(data.msg);
				}
			}
		});
		//		me.ontabchange();
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.wfm.service.accept.Form', {
			region: 'center',
			PK: me.PK,
			title: '服务受理',
			formtype: me.formtype,
			itemId: 'Form'
		});
		me.Attachment = Ext.create('Shell.class.wfm.service.accept.Attachment', {
			region: 'center',
			header: false,
			title: '附件',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory: "PCustomerServiceAttachment",
			formtype: me.formtype
		});
		return [me.Form, me.Attachment];
	},
	/**创建功能按钮栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push({
			xtype: 'button',
			itemId: 'btnSave',
			iconCls: 'button-save',
			text: "保存",
			tooltip: "保存",
			handler: function(btn) {
				me.Status = 2;
				btn.disable();
				me.onSaveClick();
//				btn.enable();
			}
		}, 'reset', {
			xtype: 'button',
			itemId: 'btnColse',
			iconCls: 'button-del',
			text: "关闭",
			tooltip: '关闭',
			handler: function() {
				me.fireEvent('onCloseClick', me);
				me.close();
			}
		});
		if(items.length > 0) items.unshift('->');

		if(items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	onSaveAttachment: function(id) {
		var me = this;
		me.Attachment.setValues({
			fkObjectId: id
		});
		me.Attachment.save();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(btn) {
		var me = this;
		if(!me.Form.getForm().isValid()) return;
		var url = me.Form.formtype == 'add' ? me.Form.addUrl : me.Form.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params = me.Form.formtype == 'add' ? me.Form.getAddParams() : me.Form.getEditParams();
		if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		var values = me.Form.getForm().getValues();
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave', me);
		JShell.Server.post(url, params, function(data) {
			me.hideMask();//隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				me.fireEvent('aftersave', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			me.Form.getForm().reset();
		} else {
			me.Form.getForm().setValues(me.Form.lastData);
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	}
	
});