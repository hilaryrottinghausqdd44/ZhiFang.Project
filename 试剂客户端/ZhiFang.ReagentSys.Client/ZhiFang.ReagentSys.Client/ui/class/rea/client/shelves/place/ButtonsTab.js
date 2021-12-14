/**
 * 根据选择的库房加载货架按钮
 * @author liangyl
 * @version 2018-03-09
 */
Ext.define('Shell.class.rea.client.shelves.place.ButtonsTab', {
    extend:'Shell.ux.panel.AppPanel',
    layout:'fit',
    requires: [
	    'Shell.ux.toolbar.Button'
	],
	margins: '0 0 1 0', 
	bodyPadding: '0 0 0 0',
	border:false,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		me.loadData('4876672975928512457','库房1');
	},
	initComponent: function() {
		var me = this;
		me.addEvents('change');
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
        me.Button = Ext.create('Shell.ux.toolbar.Button', {
			region: 'center',
			header: false,border:true,
			itemId: 'Button'
		});
		return [me.Button];
	},
	/**选中库房加载货架*/
    loadData:function(StorageID,StorageCName){
    	var me =this;
    	me.hideMask();
    	var buttonsToolbar=me.Button;
    	buttonsToolbar.removeAll();
    	if(!StorageID){
    		me.NOPlaceTip(buttonsToolbar);
    		buttonsToolbar.hide();
    	}else{
    		var arr=[];
    		me.getPlaceById(StorageID,function(data){
    			if(data && data.value){
    			   if(data.value.list.length==0) {
	    			   	me.NOPlaceTip(buttonsToolbar);
    			   }
				   for(var i=0 ;i<data.value.list.length;i++){
				        var PlaceCName=data.value.list[i].ReaPlace_CName;
				        var PlaceID=data.value.list[i].ReaPlace_Id;
						var btn={
							xtype:'button',
							itemId:'btn'+i,
							text:PlaceCName,
							tooltip:PlaceCName,
							margins: '0 0 5 0' ,
							enableToggle:false,
							StorageCName:StorageCName,
							StorageID:StorageID,
							PlaceID:PlaceID,
							PlaceCName:PlaceCName
				        };
						buttonsToolbar.add(btn,'-');
				   }
				}else{
					me.NOPlaceTip(buttonsToolbar);
				}
    		});
    		buttonsToolbar.show();
    		for(var i = 0; i < buttonsToolbar.items.length; i++) {
		    //'-' 不处理
				if(buttonsToolbar.items.items[i].itemId){
					buttonsToolbar.items.items[i].on({
						click:function(com, e,eOpts ){
							me.cleartogglebuttonsToolbar(buttonsToolbar,com);
							com.toggle(true);
							me.fireEvent('change',com);
//							me.setRecStoragePlace(com);
//							me.getSelectionModel().deselectAll();
						}
					});
				}
			}
    	}
    },
     /**
     *不选中的按钮清空选中状态     */
	cleartogglebuttonsToolbar:function(buttonsToolbar,com){
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			if(buttonsToolbar.items.items[i].itemId){
				if(com.itemId != buttonsToolbar.items.items[i].itemId){    
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
	  /**没有货架提示*/
	NOPlaceTip:function(buttonsToolbar){
		var  me=this;
		var label={
			xtype:'label',
			text:'没有货架',
			style:'color: #FF0000',
			margins: '0 0 8 10'  
        };
		buttonsToolbar.add(label);
	},
    /**根据库房id获取货架*/
	getPlaceById:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true';
		url += '&fields=ReaPlace_Id,ReaPlace_CName&where=reaplace.Visible=1 and reaplace.ReaStorage.Id='+id;
		url +='&sort=[{"property":"ReaPlace_DispOrder","direction":"ASC"}]'
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} 
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} 
	}
});