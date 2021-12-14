/**
 * 发票申请信息查看
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.invoice.basic.ShowTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '发票信息',
	height: 490,
	width: 715,
	autoScroll: false,
	/**发票ID*/
	PK: null,
	contentIsLoad: false,
	PInvoiceMsg: '发票信息',
	hasButtontoolbar: true,
	hasSave: true,
	hasDisSave: true,
	DigSaveText: '不通过',
	SaveText: '通过',
	/**合同ID*/
	ContractID: '',
	VAT: {
		/**增值税税号*/
		VATNumber: '',
		/**增值税开户行*/
		VATBank: '',
		/**增值税账号*/
		VATAccount: '',
		/**电话*/
		PhoneNum: '',
		/**地址*/
		Address: ''
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ontabchange();
		me.ContentPanel.on({
			load: function(p, data) {
				me.setTitle(me.PInvoiceMsg);
			}
		});
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
					case 'MemoPanel':
						me.loadMemoPanelForm();
						break;
					default:
						break
				}
			}
		});
	},
	/**加载说明信息*/
	loadMemoPanelForm: function() {
		var me = this;
		if(me.contentIsLoad == false) {
			me.MemoPanel.load(me.PK);
			me.contentIsLoad = true;
		}
	},
	
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();

		if(dockedItems.length > 0) {
			me.dockedItems = dockedItems;
		}
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.business.invoice.basic.ContentPanel', {
			title: '发票详情',
			itemId: 'ContentPanel',
			formtype: 'show',
			VAT:me.VAT,
			hasLoadMask: false, //开启加载数据遮罩层
			PK: me.PK
		});
		me.ContractPanel = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			title: '合同详情',
			formtype: 'show',
			itemId: 'ContractPanel',
			PK: me.ContractID,
			hasLoadMask: false
		});
		me.OperatePanel = Ext.create('Shell.class.wfm.business.invoice.basic.OperatePanel', {
			title: '操作记录',
			formtype: 'show',
			itemId: 'OperatePanel',
			hasLoadMask: false,
			PK: me.PK
		});
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			region: 'center',
			header: false,
			title: '附件',
			itemId: 'Attachment',
			border: false,
			defaultLoad: true,
			PK: me.PK,
			SaveCategory: "PInvoice",
			formtype: me.formtype
		});
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App', {
			title: '交流',
			FormPosition: 'e',
			itemId: 'Interaction',
			PK: me.PK
		});

	    me.PdfApp = Ext.create('Shell.class.wfm.business.invoice.basic.PdfApp', {
			title: '预览PDF',
			itemId: 'PdfApp',
			border: false,
			height: me.height,
			width: me.width,
			hasBtntoolbar:false,
			defaultLoad:true,
			PK: me.PK
		});
		return [me.ContentPanel, me.ContractPanel, me.Attachment, me.OperatePanel, me.Interaction, me.PdfApp];
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if(items.length == 0) {
			if(me.hasSave) {
				items.push({
					xtype: 'button',
					itemId: 'btnSave',
					iconCls: 'button-save',
					text: me.SaveText,
					tooltip: me.SaveText,
					handler: function() {
						me.fireEvent('onSaveClick', me);
					}
				});
			}
			if(me.hasDisSave) {
				items.push({
					xtype: 'button',
					itemId: 'btnDisSave',
					iconCls: 'button-save',
					text: me.DigSaveText,
					tooltip: me.DigSaveText,
					handler: function() {
						me.fireEvent('onDigSaveClick', me);
					}
				});
			}
			 items.push({
				text: '关闭',
				iconCls: 'button-del',
				tooltip: '关闭',
				handler: function() {
					me.close();
				}
			});
			if(items.length > 0) items.unshift('->');
		}
		if(items.length == 0) return null;
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items,
			hidden: hidden
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		return items;
	}
});