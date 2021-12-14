/**
 * 套餐项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.comitem.ItemPanel',{
    extend:'Shell.ux.panel.AppPanel',
    padding:'0 0 1 0',
    title:'套餐项目维护',
    hasBtntoolbar:true,
    width:960,
    height:430,
    /**项目编号*/
	ItemNo:null,
    /**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchGroupItemSubItemByPItemNo?isPlanish=true',
    RecDatas:[],
    formtype:'edit',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Btn.on({
			click:function(){
			     me.checkedRecords();
			}
		});
		me.GroupItemGrid.on({
			save:function(){
			     me.fireEvent('save', me);
			},
			onAcceptClick:function(p,recs){
				me.fireEvent('onAcceptClick',me,recs);
			}
		});
		me.GroupPanel.on({
			itemdblclick:function(com,record,item,index,e,eOpts ){
	    	    me.checkedRecords();
			}
		});
		
	},
   
	initComponent:function(){
		var me = this;
		me.addEvents('save','onAcceptClick');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.GroupPanel = Ext.create('Shell.class.weixin.dict.core.itemallitem.comitem.GroupPanel', {
			region: 'west',
			header: false,
			split: false,
			collapsible: false,
			width:430,
			/**项目编号*/
			ItemNo:me.ItemNo,
			formtype:me.formtype,
			itemId: 'GroupPanel'
		});
		
		me.Btn = Ext.create('Shell.class.weixin.dict.core.itemallitem.comitem.BtnPanel', {
		    region: 'west',
			width:75,
			split: false,
			border:false,
			collapsible: false,
			itemId: 'Btn'
		});
		
		me.GroupItemGrid = Ext.create('Shell.class.weixin.dict.core.itemallitem.comitem.GroupItemGrid', {
			region: 'center',
			split: false,
			header:false,
			/**项目编号*/
			ItemNo:me.ItemNo,
		    RecDatas:me.RecDatas,
			itemId: 'GroupItemGrid'
		});
		
		return [me.GroupPanel,me.Btn,me.GroupItemGrid];
	},
	 /**
	 * 选中数据处理
	 * @private
	 * @param {} callback 数据处理
	 */
	checkedRecords:function(){
		var me=this;
		var recs = me.GroupPanel.getRecs();
		if(!recs) return;
        var len=recs.length;
        for(var i =0 ;i<len;i++){
        	var ItemNo=recs[i].get('ItemAllItem_Id');
        	me.onSetGroupItemData(ItemNo,recs);
        }
	},
	/**调用服务拆分到已选列表*/
	onSetGroupItemData:function(ItemNo,recs){
		var me=this;
		var msg='';
		me.getLabTestItem(ItemNo,function(data){
			if(data.value){
				msg='';
				var list = data.value.list;
				for(var i=0;i<list.length;i++){
					var Id=list[i].GroupItemVO_TestItem_Id;
					var index = me.GroupItemGrid.store.findExact('GroupItemVO_TestItem_Id',Id);
				    if(me.ItemNo==list[i].GroupItemVO_TestItem_Id){
				    	msg+='主项目编码【'+me.ItemNo+'】,主项编码不能存在细项中,不能选择<br>';
				    }
				    if(index>-1){
				    	msg+='存在重复项【'+list[i].GroupItemVO_TestItem_Id+'】,不能选择 <br>';
				    }
				    if(index==-1 && me.ItemNo!=list[i].GroupItemVO_TestItem_Id){
						var obj={
				        	GroupItemVO_TestItem_Id:list[i].GroupItemVO_TestItem_Id,
				        	GroupItemVO_TestItem_CName:list[i].GroupItemVO_TestItem_CName,
				            GroupItemVO_TestItem_MarketPrice:list[i].GroupItemVO_TestItem_MarketPrice,
				            GroupItemVO_TestItem_GreatMasterPrice:list[i].GroupItemVO_TestItem_GreatMasterPrice,
				            GroupItemVO_TestItem_Price:list[i].GroupItemVO_TestItem_Price,
				            GroupItemVO_TestItem_Tag:'1'
				        };
			        	me.GroupItemGrid.store.insert(me.GroupItemGrid.getStore().getCount(),obj);            
			        }
				}
			}else{
				 for(var i =0;i<recs.length;i++) {
				 	var Id=recs[i].data.ItemAllItem_Id;
					var index = me.GroupItemGrid.store.findExact('GroupItemVO_TestItem_Id',Id);
			        if(index>-1){
				    	msg+='存在重复项【'+Id+'】,不能选择 <br>';
				    }
			        if(index==-1){
			        	var obj={
				        	GroupItemVO_TestItem_Id:recs[i].data.ItemAllItem_Id,
				        	GroupItemVO_TestItem_CName:recs[i].data.ItemAllItem_CName,
				            GroupItemVO_TestItem_MarketPrice:recs[i].data.ItemAllItem_MarketPrice,
				            GroupItemVO_TestItem_GreatMasterPrice:recs[i].data.ItemAllItem_GreatMasterPrice,
				            GroupItemVO_TestItem_Price:recs[i].data.ItemAllItem_Price,
				            GroupItemVO_TestItem_Tag:'1'
				        };
				        me.GroupItemGrid.store.insert(me.GroupItemGrid.getStore().getCount(),obj);            
			        }
		       }
			}
			if(msg){
				JShell.Msg.alert(msg);
			}
		});
	},
	/**获取已selectUrl*/
	getLabTestItem:function(itemNo,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl;
		var fields='GroupItemVO_TestItem_Id,GroupItemVO_TestItem_CName,GroupItemVO_TestItem_MarketPrice,GroupItemVO_TestItem_GreatMasterPrice,GroupItemVO_TestItem_Price';
		url += '&fields='+ fields +'&pitemNo='+itemNo;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
    /**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
    onSaveClick:function(){
    	var me = this;
    	//删除
    	me.GroupItemGrid.onDelSave();
	    var recs = me.GroupPanel.getRecs();
		if(!recs) return;
		//新增
		me.GroupItemGrid.onSaveClick();
    },
    onAcceptClick:function(){
    	var me =this;
    	var recs=me.GroupItemGrid.getAllRecDatas();
    	me.fireEvent('onAcceptClick',me,recs);
    }
});