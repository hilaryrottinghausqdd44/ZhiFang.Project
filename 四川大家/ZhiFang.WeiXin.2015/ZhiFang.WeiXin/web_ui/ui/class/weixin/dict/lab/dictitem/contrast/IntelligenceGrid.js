/**
 * 智能对照
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.contrast.IntelligenceGrid', {
    extend: 'Shell.ux.grid.Panel',
	title: '确认 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByField',
	/**删除数据服务路径*/
    delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBTestItemControl',  
    /**新增对照服务地址*/
	addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddBTestItemControl',
		/**默认加载*/
	defaultLoad: false,
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	/**默认每页数量*/
	defaultPageSize: 100000,
	//实验室项目
	dictList:[],
	//中心项目
	ItemList:[],
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**实验室Id*/
	ClienteleId:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().update('');
		me.loadDatas();
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
	    me.addEvents('save');
		me.callParent(arguments);
	},
	/**数据匹配
	 * 项目名称或者项目编号一样对照
	 * */
	loadDatas:function(){
		var me = this;
		var dictLen=me.dictList.length;
		var itemlen=me.ItemList.length;
        if(itemlen==0) return;
        if(dictLen==0) return;
        var list=[],obj={},arr=[];
		for(var i=0;i<dictLen;i++){
			var dictId=me.dictList[i].BTestItemControlVO_BLabTestItem_ItemNo;
			var dictCName=me.dictList[i].BTestItemControlVO_BLabTestItem_CName;
			var TestItemId=me.dictList[i].BTestItemControlVO_TestItem_Id;
			var TestItemCName=me.dictList[i].BTestItemControlVO_TestItem_CName;
			var BTestItemControlVOId=me.dictList[i].BTestItemControlVO_Id;
            var tag='1';
           
			//0 新增，1先删除后新增
			if(!TestItemId && !TestItemCName){
				Id='';
				tag='0';
			}
			for(var j=0;j<itemlen;j++){
				if(me.ItemList[j]){
					var Id=me.ItemList[j].ItemAllItem_Id;
				    var CName=me.ItemList[j].ItemAllItem_CName;
				    if(Id==dictId || CName==dictCName ){
				    	obj={
				    		BTestItemControlVO_BLabTestItem_ItemNo:dictId,
				    		BTestItemControlVO_BLabTestItem_CName:dictCName,
				    		ItemAllItem_Id:Id,
				    		ItemAllItem_CName:CName,
				    		Id:BTestItemControlVOId,
				    		Tag:tag
				    	};
//				    	dictLen=dictLen-1;
//				    	me.dictList.splice(j,1);
				    	list.push(obj);
				    	break;//跳出整个循环；
				    }
				}
	    	}
		}
        me.store.add(list);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'实验室项目编号',dataIndex:'BTestItemControlVO_BLabTestItem_ItemNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室项目名称',dataIndex:'BTestItemControlVO_BLabTestItem_CName',minWidth:200,
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'中心项目编号',dataIndex:'ItemAllItem_Id',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'中心项目名称',dataIndex:'ItemAllItem_CName',minWidth:200,
			flex:1,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'Id',dataIndex:'Id',width:100,isKey:true,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'标签新增和更新Tag',dataIndex:'Tag',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =[];
		//查询框信息
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '智能对照成功列表',
	        itemId:'labMarketPrice',
	        style: "font-weight:bold;color:#0000EE;",
	        margin: '0 0 10 10'
		});
		return buttonToolbarItems;
	},
	/**创建分页栏*/
	createPagingtoolbar: function() {
	    var me = this,
			items =  [];
        items.push('->','save','cancel');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	onCancelClick:function(){
		var me =this;
		me.close();
	},
	onSaveClick:function(){
		var me =this,
		    records = me.store.data.items;
		me.showMask();
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
	    
		for(var i=0;i<len;i++){
			if(changedRecords[i].get('Tag')=='1'){
				me.UpdateOne(changedRecords[i]);
			}else{
				//新增
			    me.oneAdd(changedRecords[i]);
			}
			
		}
	},
	/**行新增信息*/
	oneAdd:function(record){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
        var ItemNo=record.get('ItemAllItem_Id');
		var ControlItemNo=record.get('BTestItemControlVO_BLabTestItem_ItemNo');
        var ItemControlNo= me.ClienteleId+"_"+ItemNo+"_"+ControlItemNo;
		var entity={
			ItemControlNo:ItemControlNo,
			ItemNo:ItemNo,
			ControlLabNo:me.ClienteleId,		
			ControlItemNo:ControlItemNo,
			UseFlag:1
		};
		var params = Ext.JSON.encode({
			entity:entity
		});
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				me.fireEvent('save', me);
				
			}
		});
	},
	  /**更新一条数据*/
	UpdateOne: function(record ) {
		var me = this;
		var id= record.get('Id');
		if(!id)return;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
                me.oneAdd(record);
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
	}
});