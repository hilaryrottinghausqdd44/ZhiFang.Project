/**
 * 项目维护字典
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目维护',
     requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    hasBtntoolbar:true,
     /**实验室*/
    ClienteleID:null,
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
			delSelectClick:function(){
				me.AddPanel.clearData();
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
				var UseFlag = records[0].get('BLabTestItem_UseFlag');
				var ItemNo = records[0].get('BLabTestItem_ItemNo');
				var UseFlag = records[0].get('BLabTestItem_UseFlag');

				me.AddPanel.onEditClick(id,UseFlag,ItemNo,me.ClienteleID);
			},
			nodata:function(p){
				me.AddPanel.clearData();
			},
			ClienteleClick:function(id){ //实验室改变时
				me.ClienteleID=id;
				me.AddPanel.Grid.ClienteleID=id;
				me.AddPanel.clearData();
			}
		});
		me.AddPanel.on({
			save:function(){
				me.Grid.onSearch();
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
		me.Grid = Ext.create('Shell.class.weixin.dict.lab.dictitem.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.AddPanel = Ext.create('Shell.class.weixin.dict.lab.dictitem.AddPanel', {
			region: 'east',
			header: false,
			width:690,
			itemId: 'AddPanel',
			split: true,
			collapsible: true,
			collapseMode:'mini'
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
						if(Range[i].get('BLabTestItem_ItemNo') == 
							records[j].get('BLabTestItem_ItemNo')){
							firstRaw = records[j];
							break;
						}
					}
				}
		
				var ItemNo=firstRaw.get('BLabTestItem_ItemNo');
				var id=firstRaw.get(me.Grid.PKField);	
				me.AddPanel.loadData(id,ItemNo,me.ClienteleID);
			}
		},null,500);
	}
});