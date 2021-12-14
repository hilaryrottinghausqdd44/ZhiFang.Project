/**
 * 套餐项目选择
 * @author longfc	
 * @version 2017-04-17
 */
Ext.define('Shell.class.weixin.item.check.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'套餐项目选择',
    
    width:820,
    height:480,
      /**项目id*/
    ItemID:null,
     /**区域id*/
    AreaID:null,
    /**区域下的送检单位*/
    ClientNo:null,
      /**区域id*/
    ItemName:'',
    /**是否单选*/
	checkOne:true,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.PItemGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.GroupItemGrid.PItemNo=record.get('BLabTestItem_ItemNo');
					me.GroupItemGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.GroupItemGrid.PItemNo=record.get('BLabTestItem_ItemNo');
					me.GroupItemGrid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.GroupItemGrid.clearData();
			}
		});
		me.PItemGrid.on({
			accept: function(p, record) {
//				me.autoSelect=record ? record.get(p.PKField) : '';
//				me.PItemGrid.autoSelect=me.autoSelect;
				me.fireEvent('accept',me,record);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.PItemGrid = Ext.create('Shell.class.weixin.item.check.PItemGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId: 'PItemGrid',
			ClientNo:me.ClientNo,
			ItemName:me.ItemName,
			ItemID:me.ItemID,
			width:320
		});
		me.GroupItemGrid = Ext.create('Shell.class.weixin.item.check.GroupItemGrid', {
			region: 'center',
			header: false,
			itemId: 'GroupItemGrid',
			checkOne:me.checkOne,
			AreaID:me.AreaID,
			/**带功能按钮栏*/
	        hasButtontoolbar: false,
			/**默认加载*/
			defaultLoad:false
		});
		return [me.PItemGrid,me.GroupItemGrid];
	}
});