/**
 * 报销单信息新增页面
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.apply.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '报销申请',
	requires: ['Shell.ux.toolbar.Button'],
	width: 750,
	height: 350,
	autoScroll: false,
	FormClassConfig: {
		formtype: 'add'
	},
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**内容表单名称*/
	FormClassName: 'Shell.class.wfm.business.expenseaccount.apply.Form',
	formtype: '',
	/**报销单ID*/
	PK: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
			isValid: function(p) {
				me.setActiveTab(me.Form);
			},
			beforesave: function(p) {
				me.showMask(me.saveText);
			},
			save: function(p, id) {
				me.hideMask();
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
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;

		me.Form = Ext.create(me.FormClassName, Ext.apply(me.FormConfig, {
			formtype: 'add',
			hasLoadMask: false, //开启加载数据遮罩层
			title: '单据信息',
			hasButtontoolbar: false //带功能按钮栏
		}));
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			title: '附件',
			PK: me.PK,
			defaultLoad: true,
			SaveCategory: "PExpenseAccount",
			formtype: me.formtype
		});
		return [me.Form, me.Attachment];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: ['->', {
				text: '暂存',
				iconCls: 'button-save',
				tooltip: '暂存',
				handler: function() {
					me.onSave(false);
				}
			}, {
				text: '提交',
				iconCls: 'button-save',
				tooltip: '提交',
				handler: function() {
					me.onSave(true);
				}
			}, 'reset',
			{
			text: '关闭',
			iconCls: 'button-del',
			tooltip: '关闭',
			handler: function() {
				me.close();
			}
		}]};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		this.Form.onSave(isSubmit);
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
	}
});