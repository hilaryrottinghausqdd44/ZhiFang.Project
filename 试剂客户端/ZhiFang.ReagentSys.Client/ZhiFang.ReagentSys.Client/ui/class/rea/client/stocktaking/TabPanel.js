/**
 * 盘库管理
 * @author longfc
 * @version 2018-03-20
 */
Ext.define('Shell.class.rea.client.stocktaking.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '盘库管理',
	header: false,
	border: false,
	bodyPadding: 1,
	/**盘库单Id*/
	PK: null,
	/**当前盘库单选择行*/
	checkRecord:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'OveragePanel':
						me.OveragePanel.loadData(me.checkRecord);
						break;
					case 'ShortagePanel':
						me.ShortagePanel.loadData(me.checkRecord);
						break;
					default:
						me.StocktakingPanel.loadData(me.checkRecord);
						break
				}
			}
		});
		me.activeTab = 0;
		me.StocktakingPanel.on({
			save: function(p, id) {
				me.PK=id;
				me.fireEvent('save', p, me.PK);
			}
		});
		me.OveragePanel.on({
			save: function(p, id) {
				me.fireEvent('save', p, me.PK);
			}
		});
		me.ShortagePanel.on({
			save: function(p, id) {
				me.fireEvent('save', p, me.PK);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.StocktakingPanel = Ext.create('Shell.class.rea.client.stocktaking.add.Panel', {
			title: '盘库',
			header: true,
			border: false,
			itemId: 'StocktakingPanel'
		});
		me.OveragePanel = Ext.create('Shell.class.rea.client.stocktaking.overage.Panel', {
			title: '盘盈入库',
			header: true,
			hidden: true,
			border: false,
			itemId: 'OveragePanel'
		});
		me.ShortagePanel = Ext.create('Shell.class.rea.client.stocktaking.shortage.Panel', {
			title: '盘亏出库',
			header: true,
			hidden: true,
			border: false,
			itemId: 'ShortagePanel'
		});
		var appInfos = [me.StocktakingPanel, me.OveragePanel, me.ShortagePanel];
		return appInfos;
	},
	loadData: function(record) {
		var me = this;
		me.checkRecord=record;
		me.PK=record.get("ReaBmsCheckDoc_Id");
		var status = "" + record.get("ReaBmsCheckDoc_Status");
		//盘库锁定
		if(status == "1")
			me.isEdit(record);
		else
			me.isShow(record);
		me.setActiveTab(me.child('#StocktakingPanel'));
	},
	/**@description 控制页签显示*/
	setTabShow: function(isShow) {
		var me = this;
		if(isShow == false) {
			me.child('#OveragePanel').tab.hide();
			me.child('#ShortagePanel').tab.hide();
		} else {
			me.child('#OveragePanel').tab.show();
			me.child('#ShortagePanel').tab.show();
		}
	},
	isAdd: function() {
		var me = this;
		me.nodata();
		me.setFormType("add");
		me.StocktakingPanel.isAdd();
		me.setTabShow(false);
		me.setActiveTab(me.child('#StocktakingPanel'));
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.StocktakingPanel.isEdit(record);
		me.setTabShow(false);
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.StocktakingPanel.isShow(record);
		me.setTabShow(true);
	},
	clearData: function() {
		var me = this;
	},
	nodata: function() {
		var me = this;
		me.PK=null;
		me.checkRecord=null;
		me.setFormType("show");
		me.StocktakingPanel.clearData();
		me.OveragePanel.clearData();
		me.ShortagePanel.clearData();
		me.clearData();
		me.setTabShow(false);
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.StocktakingPanel.formtype = formtype;
		me.StocktakingPanel.DocForm.formtype = formtype;
		me.StocktakingPanel.DtlGrid.formtype = formtype;
	}
});