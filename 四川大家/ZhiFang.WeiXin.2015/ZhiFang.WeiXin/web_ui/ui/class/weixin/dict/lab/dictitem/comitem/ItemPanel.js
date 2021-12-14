/**
 * 套餐项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.comitem.ItemPanel',{
    extend:'Shell.ux.panel.AppPanel',
    padding:'0 0 1 0',
    title:'套餐项目维护',
    hasBtntoolbar:true,
    width:1100,
    height:470,
    /**项目编号*/
	ItemNo:null,
	/**实验室id*/
    ClienteleID:null,
    /**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
    selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?isPlanish=true',
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
//		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.GroupPanel = Ext.create('Shell.class.weixin.dict.lab.dictitem.comitem.GroupPanel', {
			region: 'west',
			header: false,
			split: false,
			collapsible: false,
			width:530,
			/**项目编号*/
			ItemNo:me.ItemNo,
			/**实验室id*/
		    ClienteleID:me.ClienteleID,
		    formtype:me.formtype,
			itemId: 'GroupPanel'
		});
		
		me.Btn = Ext.create('Shell.class.weixin.dict.lab.dictitem.comitem.BtnPanel', {
		    region: 'west',
			width:75,
			split: false,
			border:false,
			collapsible: false,
			itemId: 'Btn'
		});
		
		me.GroupItemGrid = Ext.create('Shell.class.weixin.dict.lab.dictitem.comitem.GroupItemGrid', {
			region: 'center',
			split: false,
			header:false,
			/**项目编号*/
			ItemNo:me.ItemNo,
			/**实验室id*/
		    ClienteleID:me.ClienteleID,
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
		me.showMask();
		var recs = me.GroupPanel.getRecs();
		if(!recs) return;
        var len=recs.length;
        for(var i =0 ;i<len;i++){
        	var ItemNo=recs[i].get('ItemNo');
        	me.onSetGroupItemData(ItemNo,recs);
        }
        me.hideMask();
	},
	
	/**调用服务拆分到已选列表*/
	onSetGroupItemData:function(ItemNo,recs){
		var me=this;
		me.getLabTestItem(ItemNo,function(data){
			var msg='';
			if(data.success){
				if(data.value){
					msg='';
					var list = data.value.list;
					for(var i=0;i<list.length;i++){
						var Id=list[i].BLabGroupItemVO_BLabTestItemVO_ItemNo;
						var index = me.GroupItemGrid.store.findExact('BLabGroupItemVO_BLabTestItemVO_ItemNo',Id);
				        if(me.ItemNo==Id){
					    	msg+='主项目编码【'+me.ItemNo+'】,主项编码不能存在细项中,不能选择<br>';
					    }
					    if(index>-1){
					    	msg+='存在重复项【'+Id+'】,不能选择 <br>';
					    }
				        if(index==-1 && me.ItemNo!=list[i].BLabGroupItemVO_BLabTestItemVO_ItemNo){
							var obj={
					        	BLabGroupItemVO_BLabTestItemVO_ItemNo:list[i].BLabGroupItemVO_BLabTestItemVO_ItemNo,
					        	BLabGroupItemVO_BLabTestItemVO_CName:list[i].BLabGroupItemVO_BLabTestItemVO_CName,
					            BLabGroupItemVO_BLabTestItemVO_MarketPrice:list[i].BLabGroupItemVO_BLabTestItemVO_MarketPrice,
					            BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice:list[i].BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice,
					            BLabGroupItemVO_Price:list[i].BLabGroupItemVO_BLabTestItemVO_Price,
					            BLabGroupItemVO_BLabTestItemVO_Tag:'1'
					        };
				        	me.GroupItemGrid.store.insert(me.GroupItemGrid.getStore().getCount(),obj);            
				        }
					}
				}
			}else{
				for(var i =0;i<recs.length;i++) {
				 	var Id=recs[i].data.ItemNo;
					var index = me.GroupItemGrid.store.findExact('BLabGroupItemVO_BLabTestItemVO_ItemNo',Id);
			        if(index>-1){
				    	msg+='存在重复项【'+Id+'】,不能选择 <br>';
				    }
			        if(index==-1){
			        	var obj={
				        	BLabGroupItemVO_BLabTestItemVO_ItemNo:recs[i].data.ItemNo,
				        	BLabGroupItemVO_BLabTestItemVO_CName:recs[i].data.CName,
				            BLabGroupItemVO_BLabTestItemVO_MarketPrice:recs[i].data.MarketPrice,
				            BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice:recs[i].data.GreatMasterPrice,
				            BLabGroupItemVO_Price:recs[i].data.Price,
				            BLabGroupItemVO_BLabTestItemVO_Tag:'1'
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
		var fields='BLabGroupItemVO_BLabTestItemVO_ItemNo,BLabGroupItemVO_BLabTestItemVO_PItemNo,BLabGroupItemVO_BLabTestItemVO_CName,BLabGroupItemVO_BLabTestItemVO_MarketPrice,BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice,BLabGroupItemVO_BLabTestItemVO_Price';
		url += '&fields='+ fields +'&LabCode='+me.ClienteleID+'&pitemNo='+itemNo;
		JShell.Server.get(url,function(data){
			callback(data);
		},false);
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