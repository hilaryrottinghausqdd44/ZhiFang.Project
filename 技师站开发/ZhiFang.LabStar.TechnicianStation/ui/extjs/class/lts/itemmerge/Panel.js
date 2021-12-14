/**
 *图片与样本结果列表面板
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.itemmerge.Panel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'图片与样本结果列表面板',
    //样本单ID
    OldTestFormID:null,
    //组合项目
    GroupItemObj:{},
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.ItemGrid.on({
			itemclick:function(v, record) {
				me.onSearch2(record);
			},
			select:function(RowModel, record){
				me.onSearch2(record);
			},
			nodata:function(){
				
			},
			cellValChange:function(grid){
				me.onSearchChart();
			},
			changeResult : function(grid,data){
				me.onSearchChart();
			},
			ItemGridAfterLoad: function (p) {
				me.fireEvent("ItemGridAfterLoad",p);
			}
		});
		me.Echart.on({
			cbChangeClick : function(){
				me.onSearchChart();
			},
			save:function(){
				me.fireEvent('save');
			}
		});
		
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('save');
		//创建挂靠功能栏
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.ItemGrid = Ext.create('Shell.class.lts.itemmerge.ItemGrid', {
			region:'north',
			height:180,
			itemId:'ItemGrid',
			header:false,
			split:true,
			collapsible:false
		});
		me.ResultGrid = Ext.create('Shell.class.lts.itemmerge.ResultGrid', {
			region:'east',
			width:230,
			itemId:'ResultGrid',
			header:true,
			split:true,
			collapsible:false
		});
		me.Echart = Ext.create('Shell.class.lts.itemmerge.LineChart', {
			region:'center',
			itemId:'Echart',
			header:false
		});
		return [me.ItemGrid,me.ResultGrid,me.Echart];
	},
	onSearch : function(obj,GroupItemObj){
		var me = this;
		me.GroupItemObj=GroupItemObj;
		me.ItemGrid.onSearch(obj);
	},
	//样本结果详细列表
	onSearch2 : function(record){
		var me = this;
		if(me.OldTestFormID && record.get('LBMergeItemVO_LisTestItem_LisTestForm_Id') == me.OldTestFormID){
			
		}else{
			me.OldTestFormID = record.get('LBMergeItemVO_LisTestItem_LisTestForm_Id');
            me.ResultGrid.onSearch(record.get('LBMergeItemVO_LisTestItem_LisTestForm_Id'),true);
		}
	},
	//图形生成
	onSearchChart : function(){
		var me = this;
		//获取合并标识为是的行数据toFormID
		var toFormID ="",listLisTestItem =[];
		
		var items = me.ItemGrid.store.data.items,
		    len = items.length;
		    
		for(var i = 0 ;i<len;i++){
			if(items[i].data.LBMergeItemVO_IsMerge==true){
				toFormID = items[i].data.LBMergeItemVO_LisTestItem_LisTestForm_Id;
			}
			var itemObj = {
				Id:items[i].data.LBMergeItemVO_LisTestItem_Id,
				EquipID:items[i].data.LBMergeItemVO_LisTestItem_EquipID ? items[i].data.LBMergeItemVO_LisTestItem_EquipID : null,
				LBItem: { Id: items[i].data.LBMergeItemVO_ChangeItemID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				//LBItem:{Id:items[i].data.LBMergeItemVO_LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]},
				ReportValue:items[i].data.LBMergeItemVO_LisTestItem_ReportValue
			};
			listLisTestItem.push(itemObj);
		}
		me.Echart.onSearch({toFormID:toFormID,listLisTestItem:listLisTestItem},items);
	},
	onUploadImg:function(){
		var me = this;
		var items = me.ItemGrid.store.data.items,
		    len = items.length;
		me.Echart.onUploadImg(me.GroupItemObj);
	}
});