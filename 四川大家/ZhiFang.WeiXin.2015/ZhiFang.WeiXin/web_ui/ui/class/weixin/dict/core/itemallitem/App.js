/**
 * 中心项目字典维护
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'中心项目字典维护',
    hasBtntoolbar:true,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				me.onSelect();
			},
			select:function(RowModel, record){
				me.onSelect();
			},
			deselect:function(RowModel, record){
				me.onSelect(record);
			},
			onAddClick:function(id){
				me.AddPanel.onAddClick(id);
			},
			onEditClick:function(p,record){
				var records = me.Grid.getSelectionModel().getSelection();
				if(!records || records.length != 1){
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid.PKField);
				me.AddPanel.onEditClick(id);
			},
			nodata:function(p){
				me.AddPanel.clearData();
			}
		});
		me.AddPanel.on({
			save:function(p,id){
				me.Grid.onSearch(id);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();

		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.weixin.dict.core.itemallitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.AddPanel = Ext.create('Shell.class.weixin.dict.core.itemallitem.AddPanel', {
			region: 'east',
			header: false,
			width:685,
			itemId: 'AddPanel',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.AddPanel];
	},
	//选中处理
	onSelect:function(record){
		var me = this;
		
		JShell.Action.delay(function(){
			var records = me.Grid.getSelectionModel().getSelection(),
				sLen = records.length;
			if(sLen == 0){//不存在选中数据
				//清空表单信息+清空明细列表
				me.AddPanel.clearData();
			}else{
				var Range = me.Grid.store.getRange(),
					rLen = Range.length,
					firstRaw = null;
				for(var i=0;i<rLen;i++){
					if(firstRaw) break;
					for(var j=0;j<sLen;j++){
						if(Range[i].get('TestItem_Id') == 
							records[j].get('TestItem_Id')){
							firstRaw = records[j];
							break;
						}
					}
				}
				var ItemNo=firstRaw.get('TestItem_Id');
				me.AddPanel.loadData(ItemNo);
			}
		},null,200);
	}
});