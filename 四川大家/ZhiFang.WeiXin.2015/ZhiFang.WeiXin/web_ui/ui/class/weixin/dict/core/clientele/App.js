/**
 * 中心医疗机构字典表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.clientele.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'中心医疗机构字典表维护',
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
			onAddClick:function(AreaID,AreaName){
				me.Form.isAdd(AreaID,AreaName);
			},
			onEditClick:function(p){
				var records = me.Grid.getSelectionModel().getSelection();
				if(!records || records.length != 1){
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.Grid.PKField);
				me.Form.isEdit(id);
				me.Form.AreaEnum=me.Grid.AreaEnum;
			},
			nodata:function(p){
				me.onSelect();
			}
				
		});
		me.Form.on({
			
			save:function(p,id){
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
		me.Grid = Ext.create('Shell.class.weixin.dict.core.clientele.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.weixin.dict.core.clientele.Form', {
			region: 'east',
			header: false,
			width:485,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.Grid,me.Form];
	},
	//选中处理
	onSelect:function(record){
		var me = this;
		
		JShell.Action.delay(function(){
			var records = me.Grid.getSelectionModel().getSelection(),
				sLen = records.length;
			if(sLen == 0){//不存在选中数据
				//清空表单信息+清空明细列表
				me.Form.clearData();
			}else{
				var Range = me.Grid.store.getRange(),
					rLen = Range.length,
					firstRaw = null;
				for(var i=0;i<rLen;i++){
					if(firstRaw) break;
					for(var j=0;j<sLen;j++){
						if(Range[i].get('CLIENTELE_Id') == 
							records[j].get('CLIENTELE_Id')){
							firstRaw = records[j];
							break;
						}
					}
				}
				var Id=firstRaw.get('CLIENTELE_Id');
				me.Form.isShow(Id);
				me.Form.AreaEnum=me.Grid.AreaEnum;
			}
		},null,200);
	}
});