/**
 * 医生奖金结算单信息
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '医生奖金结算单信息',

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	hasLoadMask: true,
	PK: '',
	formLoaded: false,
	isSave: false,
	isBonusGridLoad: false,
	/*附件信息是否已经加载*/
	isattachmentLoad: false,
	isOperationLoad: false,
	BonusGridCalss:'',
	initComponent: function() {
		var me = this;
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.BonusGridCalss=me.BonusGridCalss||'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var items = [];
		me.BonusGrid = Ext.create(me.BonusGridCalss, {
			itemId: 'BonusGrid',
			title: '医生奖金结算记录信息',
			//border: false,
			isAllowEditing: false,
			defaultLoad:false,
			PK: me.PK
		});
		items.push(me.BonusGrid);

		me.Attachment = Ext.create('Shell.class.weixin.ordersys.settlement.doctorbonus.attachment.Attachment', {
			header: false,
			title: '附件信息',
			itemId: 'Attachment',
			//border: false,
			defaultLoad: true,
			PK: me.PK,
			formtype: "show"
		});
		items.push(me.Attachment);

		me.OperationPanel = Ext.create('Shell.class.weixin.ordersys.settlement.doctorbonus.operation.Panel', {
			title: '操作记录信息',
			header: false,
			itemId: 'OperationPanel',
			PK: me.PK,
			border: false,
			isShowForm: false
		});
		items.push(me.OperationPanel);
		return items;
	},
		/**加载附件信息*/
	loadAttachment: function() {
		var me = this;
		me.Attachment.PK = me.PK;
		if(me.isattachmentLoad == false) {
			me.Attachment.load();
		}
		me.isattachmentLoad = true;
	},
	/**加载明细信息*/
	loadBonusGrid: function() {
		var me = this;
		me.BonusGrid.PK = me.PK;
		if(me.isBonusGridLoad == false) {
			me.BonusGrid.defaultWhere="OSDoctorBonusFormID="+me.PK;
			me.BonusGrid.load();
		}
		me.isBonusGridLoad = true;
	},
	loadOperation: function() {
		var me = this;
		me.OperationPanel.PK = me.PK;
		if(me.isOperationLoad == false) {
			me.OperationPanel.onLoadData();
		}
		me.isOperationLoad = true;
	},
	/**加载明细信息*/
	loadTabPanel: function() {
		var me = this;
		me.BonusGrid.PK = me.PK;
		me.Attachment.PK = me.PK;
		me.OperationPanel.PK = me.PK;
		me.isattachmentLoad = false;
		me.isOperationLoad = false;
		me.isBonusGridLoad = false;
		if(me.PK == "" || me.PK == null) {
			me.clearData();
		} else {
			me.setActiveTab(me.BonusGrid);
			me.loadBonusGrid();
		}
	},
	clearData: function() {
		var me = this;
		me.PK=null;
		me.BonusGrid.PK = null;
		me.Attachment.PK =null;
		me.OperationPanel.PK =null;
		me.isattachmentLoad = false;
		me.isOperationLoad = false;
		me.isBonusGridLoad = false;
		me.BonusGrid.clearData();
		//me.Attachment.clearData();
		me.OperationPanel.clearData();
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
					case 'BonusGrid':
						me.loadBonusGrid();
						break;
					case 'Attachment':
						me.loadAttachment();
						break;
					case 'OperationPanel':
						me.loadOperation();
						break;
					default:
						break
				}
			}
		});
	}
});