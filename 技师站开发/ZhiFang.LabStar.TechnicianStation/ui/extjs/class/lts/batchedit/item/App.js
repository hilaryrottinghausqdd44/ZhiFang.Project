/**
 * 选择项目
 * @author liangyl		
 * @version 2019-12-23
 */
Ext.define('Shell.class.lts.batchedit.item.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'选择项目',
     //小组ID
	SectionID: null,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.UnTab.on({
			itemclickAddLeft: function (v, records) {
				me.addLeft(records);
			}
		});
		me.Btn.on({
			left : function(){//向左移
				me.addLeft();
			},
			right : function(){//删除已选择项目行
				me.Grid.removeRec();
			},
			accept : function(){//确定
			    var items = me.Grid.store.data.items,
			        len = items.length;
			    me.fireEvent('save', me, items);    
			},
			cancel:function(){
				me.close();
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('save');

		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.lts.batchedit.item.Grid', {
			region:'west',
			width:300,
			itemId:'Grid',
			header:false,
			split:true,
			collapsible:false
		});
		me.Btn = Ext.create('Shell.class.lts.batchedit.item.Btn', {
			region:'west',
			width:60,
			itemId:'Btn',
			header:false,
			split:true,
			collapsible:false
		});
		
		me.UnTab = Ext.create('Shell.class.lts.batchedit.item.un.Tab', {
			region:'center',
			itemId:'UnApp',
			SectionID:me.SectionID,
			split:true,
			collapsible:false,
			header:false
		});
		
		return [me.Grid,me.Btn,me.UnTab];
	},
	//选择项目添加到已选项目（往左列表添加数据)
	addLeft: function (records){
		var me = this;
		//获取选择行
		var records = records || me.UnTab.getSelect();
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			me.Grid.store.each(function(rec) {
				if(rec.get('LBSectionItem_LBItem_Id') == record.LBSectionItem_LBItem_Id) {
					//如果列表存在非组合项目，需要替换为组合项目
					if(!rec.get('LBSectionItem_LBItem_GroupItemID')){
						rec.set('LBSectionItem_LBItem_GroupItemCName',record.LBSectionItem_LBItem_GroupItemCName);
						rec.set('LBSectionItem_LBItem_GroupItemID',record.LBSectionItem_LBItem_GroupItemID);
						rec.set('LBSectionItem_LBItem_GroupItemUseCode',record.LBSectionItem_LBItem_GroupItemUseCode);
						rec.set('LBSectionItem_LBItem_GroupType',record.LBSectionItem_LBItem_GroupType);
					}
					isAdd = false;
					return false;
				}
			});
			if(isAdd)me.Grid.store.add(record);
		});
	}
});