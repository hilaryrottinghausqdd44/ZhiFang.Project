/**
 * 
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.TabPanel', {
	extend: 'Ext.tab.Panel',
	title: '',
	header: false,
	border: false,
//	bodyPadding: 1,
	EquipID:null,
	isGoodsLoad:false,
	isEquipLoad:false,
	oldEquipID:null,
    oldEquipGoodsID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'AddPanel':
					    me.loadDatas(me.EquipID);
						break;
					case 'EquipGoodsApp':
					    me.loadDatas(me.EquipID);
						break;
					default:
						break
				}
			}
		});
	},
	loadDatas:function(id){
		var me =  this;
		var comtab = me.getActiveTab(me.items.items[0]);
		switch(comtab.itemId) {
			case 'AddPanel':
				if(!me.oldEquipID ||id!=me.oldEquipID  ){
					me.isEquipLoad=false;
					if(!me.isEquipLoad)me.AddPanel.loadEquipData(id);
					me.oldEquipID=id;
					me.isEquipLoad=true;
				}
				break;
			case 'EquipGoodsApp':
                if(!me.oldEquipGoodsID ||id!=me.oldEquipGoodsID  ){
                	me.isGoodsLoad=false;
					if(!me.isGoodsLoad)me.EquipGoodsApp.loadGoodsData(id);
					me.oldEquipGoodsID=id;
					me.isGoodsLoad=true;
				}
				break;
			default:
				break
		}
	},
	clearData:function(){
		var me =  this;
		var comtab = me.getActiveTab(me.items.items[0]);
		switch(comtab.itemId) {
			case 'AddPanel':
			    me.AddPanel.clearData();
				break;
			case 'EquipGoodsApp':
                me.EquipGoodsApp.clearData();
				break;
			default:
				break
		}
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		//me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.AddPanel = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.AddPanel', {
			title: '按仪器项目',
			header: true,
			border: false,
			itemId: 'AddPanel'
		});
		me.EquipGoodsApp = Ext.create('Shell.class.rea.client.testitem.equipitemgoodlink.EquipGoodsApp', {
			title: '按仪器试剂',
			header: true,
			border: false,
			itemId: 'EquipGoodsApp'
		});
		var appInfos = [me.AddPanel, me.EquipGoodsApp];
		return appInfos;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});