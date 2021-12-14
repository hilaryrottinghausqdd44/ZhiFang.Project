/**
 * 套餐项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.comitem.GroupPanel',{
    extend:'Shell.ux.panel.AppPanel',
    padding:'0 0 1 0',
    title:'套餐项目维护',
    hasBtntoolbar:true,
    width:950,
    height:470,
    /**项目编号*/
	ItemNo:null,
	formtype:'edit',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.IsGroupItemGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var ItemNo=record.get('ItemAllItem_Id');
					me.ItemGrid.ItemNo=ItemNo;
					me.ItemGrid.onSearch();
                },null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var ItemNo=record.get('ItemAllItem_Id');
					me.ItemGrid.ItemNo=ItemNo;
					me.ItemGrid.onSearch();
				},null,500);
			},
			itemdblclick:function(com,record,item,index,e,eOpts ){
	    		me.fireEvent('itemdblclick',me,record);
	    	},
	    	nodata:function(p){
				me.ItemGrid.clearData();
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.addEvents('itemdblclick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.IsGroupItemGrid = Ext.create('Shell.class.weixin.dict.core.itemallitem.comitem.IsGroupItemGrid', {
			region: 'north',
			header: false,
			split: false,
			collapsible: false,
			height:240,
		    ItemNo:me.ItemNo,
		    border:false,formtype:me.formtype,
			itemId: 'IsGroupItemGrid'
		});
		me.ItemGrid = Ext.create('Shell.class.weixin.dict.core.itemallitem.comitem.ItemGrid', {
			region: 'center',
			split: false,
			header:false,
			  /**项目编号*/
			ItemNo:me.ItemNo,
		    border:false,
			itemId: 'ItemGrid'
		});
		return [me.IsGroupItemGrid,me.ItemGrid];
	},
	/**获取选中行*/
	getRecs:function(){
		var me=this;
		var recs = me.IsGroupItemGrid.getSelectionModel().getSelection();
        if(!recs){
        	JShell.Msg.error('请选择行');
        }
        return recs;
	}
});