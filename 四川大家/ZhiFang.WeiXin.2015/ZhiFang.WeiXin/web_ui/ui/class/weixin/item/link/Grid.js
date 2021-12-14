/**
 * 检测项目与产品分类树关系
 * @author liangyl	
 * @version 2017-01-18
 */
Ext.define('Shell.class.weixin.item.link.Grid', {
    extend: 'Shell.ux.grid.IsUseGrid',
	title: '检测项目与产品分类树关系',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByTreeId?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeLinkByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelOSItemProductClassTreeLink',
	/**新增数据服务路径*/
	addUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_AddOSItemProductClassTreeLink',
    /**查询项目服务路径*/
    testItemUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?isPlanish=true',
	/**查询产品分类树服务路径*/
	productClassTreeUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectUrl2: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?isPlanish=true',
	
	/**默认加载*/
	defaultLoad: false,
	
	/**是否启用修改按钮*/
	hasEdit:false,
	
	/**检测项目产品分类树Id*/
	ItemProductClassTreeID:0,
	/**项目名称*/
	TestItemEnum:null,
	TestItemList:[],
	/**产品分类树名称*/
	ClassTreeEnum:null,
	ClassTreeList:[],
	/**区域代码*/
    AreaID:null,
    /**区域ID*/
    AreaNO:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
	    //创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
		/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			
			//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,itemId:'search',
			itemId: 'search',fields: ['ositemproductclasstreelink.CName']};
		
		buttonToolbarItems.push('refresh','-','add',
		{text:'删除',tooltip:'删除',iconCls:'button-del',itemId: 'del',
		handler: function() {
			me.onDelClick();
		   }
		});
		buttonToolbarItems.push('-',{
			boxLabel: '本节点',itemId: 'checkBDictTreeId',checked: false,value: false,inputValue: false,xtype: 'checkbox',
				style: {
					marginRight: '8px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.onSearch();
					}
				}
		});
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'项目分类',dataIndex:'OSItemProductClassTreeLink_ItemProductClassTreeCName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目编码',dataIndex:'OSItemProductClassTreeLink_ItemNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'OSItemProductClassTreeLink_ItemCName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'创建者',dataIndex:'OSItemProductClassTreeLink_CreatorName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'创建时间',dataIndex:'OSItemProductClassTreeLink_DataAddTime',width:135,
			hasTime: true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'OSItemProductClassTree_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,hidden:true,
			stopSelection:false,type:'boolean'
		},{
			text:'主键ID',dataIndex:'OSItemProductClassTreeLink_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'项目编号',dataIndex:'OSItemProductClassTreeLink_ItemNo',hidden:true,hideable:false
		},{
			text:'项目分类ID',dataIndex:'OSItemProductClassTreeLink_ItemProductClassTreeID',hidden:true,hideable:false
		}];
		
		return columns;
	},
	
	/**根据分类树ID加载数据*/
	loadByItemProductClassTreeID:function(id){
		var me = this;
		me.ItemProductClassTreeID = id;
		me.defaultWhere = 'ositemproductclasstreelink.ItemProductClassTreeID=' + id;
		me.onSearch();
	},
	
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		var ClassTreeID=me.ItemProductClassTreeID+'';
	    if(ClassTreeID=='0'){
	    	JShell.Msg.error('请选择树节点');
	    	return;
	    }
		JShell.Win.open('Shell.class.weixin.item.link.item.CheckGrid', {
//      	SUB_WIN_NO:'1',//内部窗口编号
        	//resizable:false,
            title:'检测项目产品选择',
            AreaID:me.AreaID,
            width:605,
            height:350,
            listeners:{
                accept:function(p,records){
                    me.onSave(p,records);
                },
            }
        }).show();
	},
	/**保存关系数据*/
	onSave:function(p,records){
		var me = this,
			ids = [],
		    nos = [],
			addIds = [],addNos=[];
			
		if(records.length == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
			
		for(var i in records){
			ids.push(records[i].get('BLabTestItem_Id'));
			nos.push(records[i].get('BLabTestItem_ItemNo'));
		}
		//获取现有关系用于验证过滤已经存在的关系
		me.getLinkByIds(ids,nos,function(list){
			addIds=[];
			for(var i in records){
				var ItemNo = records[i].get('BLabTestItem_ItemNo');
				var TestItemId = records[i].get('BLabTestItem_Id');
				var hasLink = false;
				for(var j in list){
					if(TestItemId == list[j].ItemID && ItemNo == list[j].ItemNo){
						hasLink = true;
						break;
					}
				}
				if(!hasLink){
					addIds.push(TestItemId);
					addNos.push(ItemNo);
				}
				if(hasLink){
					me.hideMask();//隐藏遮罩层
					p.close();
				}
			}
			
			//循环保存数据
			for(var i in addIds){
				me.saveLength = addIds.length;
				me.onAddOneLink(addIds[i],addNos[i],function(){
					p.close();
					me.onSearch();
				});
			}
		});
	},
	/**新增关系数据*/
	onAddOneLink:function(ItemID,ItemNo,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		var params = {
			entity:{
				IsUse:1,
				ItemNo:ItemNo,
				ItemID:ItemID,
				ItemProductClassTreeID:me.ItemProductClassTreeID
			}
		};
		if(me.AreaNO){
			params.entity.AreaID = me.AreaNO;
		}
		
		//创建者信息
		var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) ;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			params.entity.CreatorID = userId;
		}
		if(userName){
			params.entity.CreatorName = userName;
		}
		
		//提交数据到后台
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){callback();}
			}
		});
	},
	
	/**根据IDS获取关系数据，用于验证勾选的项目是否已经存在于关系中*/
	getLinkByIds:function(ids,nos,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl2.split('?')[0] + 
				'?fields=OSItemProductClassTreeLink_ItemID,OSItemProductClassTreeLink_ItemNo' +
				'&where=ositemproductclasstreelink.ItemID in(' + ids.join(',') + ') and ositemproductclasstreelink.ItemNo in(' + nos.join(',') + ') and ositemproductclasstreelink.ItemProductClassTreeID='+me.ItemProductClassTreeID ;
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.data || {}).list || [];
				callback(data.value.list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
		    buttonsToolbar = me.getComponent('buttonsToolbar'),
			url = me.callParent(arguments);
        var checkBDictTreeId= buttonsToolbar.getComponent('checkBDictTreeId').getValue();
		var ischeckBDictTree=checkBDictTreeId == false ? true : false;
		url += '&isSearchChild=' + ischeckBDictTree;
		if(me.AreaID){
			url += '&areaId='+ me.AreaNO;
		}
		url += '&treeId=' + me.ItemProductClassTreeID;
		return url;
	}
});