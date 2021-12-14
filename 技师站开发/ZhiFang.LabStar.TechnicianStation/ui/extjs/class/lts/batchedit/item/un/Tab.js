/**
 * 未选项目TAB
 * @author liangyl
 * @version 2019-12-23
 */
Ext.define('Shell.class.lts.batchedit.item.un.Tab', {
	extend:'Ext.tab.Panel',
	title:'未选项目TAB',
//	activedTab:0,
	//小组ID
	SectionID:null,
	
	defaults:{border:false},
	
	//当前激活页签
	activePanel:null,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.activePanel = me.OrderItem;
	    me.on({
			/**页签切换事件处理*/
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
				switch(newCard.itemId) {
					case 'OrderItem':
					    me.activePanel = me.OrderItem;
						break;
					case 'GroupItem':
					    me.activePanel = me.GroupItem;
						break;
					case 'Item':
					    me.activePanel = me.Item;
						break;
					default:
						break
				}
			}
		});
		me.OrderItem.on({
			itemclickAddLeft: function (v, records) {
				me.fireEvent("itemclickAddLeft", v, records);
			}
		});
		me.GroupItem.on({
			itemclickAddLeft: function (v, records) {
				me.fireEvent("itemclickAddLeft", v, records);
			}
		});
		me.Item.on({
			itemclickAddLeft: function (v, records) {
				me.fireEvent("itemclickAddLeft", v, records);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.activeTab = 0;//初始页签
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.OrderItem = Ext.create('Shell.class.lts.batchedit.item.un.App',{
			closable:false,
			border:false,
			itemId:'OrderItem',
			type:'1',
			SectionID:me.SectionID,
			title:'医嘱项目'
		});
		
		me.GroupItem = Ext.create('Shell.class.lts.batchedit.item.un.App',{
			closable:false,
			border:false,
			type:'2',
			itemId:'GroupItem',
			SectionID:me.SectionID,
			title:'组合项目'
		});
		
		me.Item = Ext.create('Shell.class.lts.batchedit.item.un.App',{
			closable:false,
			border:false,
			itemId:me.ALL_ITEMID,
			SectionID:me.SectionID,
			type:'0',
			itemId:'Item',
			title:'全部项目'
		});
		
		return [me.OrderItem,me.GroupItem,me.Item];
	},
	getSelect : function(){
		var me = this;
		return me.activePanel.getSelect();
	}
});