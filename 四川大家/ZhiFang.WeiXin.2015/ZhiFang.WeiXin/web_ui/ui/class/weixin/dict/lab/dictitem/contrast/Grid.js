/**
 * 中心项目列表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.contrast.Grid', {
	extend: 'Shell.ux.panel.AppPanel',
	title:"中心项目列表",
	requires:[
	    'Shell.class.weixin.dict.lab.dictitem.contrast.PagingMemoryProxy'
	],
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/SearchBLabTestItemByLabCodeAndType',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBLabTestItem',
    layout:'fit',
    /*静态数据，可以从后台获取*/
    gridData:[], //保存数据
    totalCount:0, //数据总条数
    pageSize:50,  //每页显示的条数
    myStore:null, // 创建的store数据对象
    myGridPanel: null , //GridPanel对象
    getStoreFields:[], //数据列模型
    /**获取列表数据*/
	ItemList: [],
		/**实验室*/
	ClienteleID:null,
	border:false,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	autoSelect: false,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.ClienteleID){
			me.enableControl(true);
		}else{
			me.enableControl(false);
		}
		me.myGridPanel.on({
			itemclick:function(v, record) {
				me.fireEvent('itemclick', v, record);
			},
			select:function(RowModel, record){
				me.fireEvent('select', RowModel, record);
			}
		});
		
	},
    createStore:function(){
    	var me = this;
    	me.myStore = Ext.create("Ext.data.Store", {
            fields: me.getStoreFields,
            pageSize: me.pageSize, // 指定每页显示2条记录
            autoLoad: true,
            data: me.gridData,
            proxy: {
                type: 'pagingmemory',
                reader: {
                    type: 'json',
                    totalProperty:'total'
                }
            }
        });
        return me.myStore;
    },
    
    /**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;
		var config = {
			store: me.myStore,
            dock: 'bottom',
            cls: "smallPagingToolBar",
            inputItemWidth: 50, 
            displayInfo: true,
            dorefreshData: function () { 
                me.refreshData();
            }
		};
		return Ext.create('Ext.toolbar.Paging', config);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items =  [];
        items.push({
	        xtype: 'label',
	        text: '项目字典表对照关系表',
	        itemId:'labMarketPrice',
	        style: "font-weight:bold;color:#0000EE;"
		},'-',{
	        xtype: 'radiogroup',
	        fieldLabel: '',
	        columns: 3,
	        vertical: true,
	        width:200,
	        itemId:'radioType',
	        height:25,
	        items: [
	            { boxLabel: '全部', name: 'rb', inputValue: '0'},
	            { boxLabel: '已对照', name: 'rb', inputValue: '1'},
	            { boxLabel: '未对照', name: 'rb', inputValue: '2' , checked: true}
	        ],
	        listeners:{
	        	change : function(com,newValue,oldValue,eOpts ){
					if(!me.ClienteleID) {
				    	JShell.Msg.error('实验室编号为空!');
				    	return;
				    }
					if(!newValue) {
				    	JShell.Msg.error('查询类型为空!');
				    	return;
				    }
	        		me.loadDatas();
	        	}
	        }
	    },'->',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
		    name: 'fastKey',
		    emptyText: '实验室项目名称/简码',
			listeners:{
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER){
						me.onSearch();
					}
				}
            }
        });
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	
    /*创建gridPanel */
    createGridColumns:function(){
    	var me = this;
    	/*创建数据列 */
        var gridColumn = [{
			xtype: 'rownumberer',text: '序号',width: 50,align: 'center'
		},{
			text:'',dataIndex:'',width:10,
			sortable:true,menuDisabled:true,align: 'left',
			renderer: function(value, meta, record) {
				var v = "",Color='';
				var TestItemId=record.get('BTestItemControlVO_TestItem_Id');
				var UseFlag =record.get('BTestItemControlVO_TestItem_UseFlag');
				
				//禁用
				if(UseFlag=='0'){
					Color = '<span style="padding:0px;color:gray; border:solid 3px gray"></span>'
				}
				//启用 并且对照状态=未对照
				if(UseFlag=='1' && !TestItemId) {
                    Color = '<span style="padding:0px;color:Violet; border:solid 3px Violet"></span>&nbsp;'
				}
				//启用 并且对照状态=已对照
				if(UseFlag=='1' && TestItemId){
                    Color = '<span style="padding:0px;color:white;"></span>'
				}
				v = Color;
//				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		},{
			text:'ID',dataIndex:'BTestItemControlVO_Id',width:200,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室项目编号',dataIndex:'BTestItemControlVO_BLabTestItem_ItemNo',width:100,
			isKey:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'实验室项目名称',dataIndex:'BTestItemControlVO_BLabTestItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'中心项目编号',dataIndex:'BTestItemControlVO_TestItem_Id',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'中心项目名称',dataIndex:'BTestItemControlVO_TestItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否使用',dataIndex:'BTestItemControlVO_TestItem_UseFlag',width:100,
			hidden:false,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
        for (var i = 0; i < gridColumn.length; i++) {
//      	me.getStoreFields=['BLabTestItem_ItemNo','BLabTestItem_CName','BLabTestItem_Id','BLabTestItem_ShortName'];
        	if(gridColumn[i].dataIndex && gridColumn[i].dataIndex !='undefined'){
        		me.getStoreFields.push(gridColumn[i].dataIndex);
        	}
        }
        me.totalCount = me.gridData.length;
        var picker = Ext.create("Ext.grid.Panel", { 
            store: me.createStore(),
            columns: gridColumn,
            layout:"fit", 
            height:350,
            sortableColumns: false, 
            autoScroll: true,
//          columnLines: true,
            dockedItems:[me.createPagingtoolbar(), me.createButtontoolbar()]
        });
        return picker; 
    },
    refreshData:function(){
        //清空筛选输入框的数据,
        var fastKey = this.myGridPanel.query("[name='fastKey']")[0];
        fastKey.setValue("");
        me.showMask();
        /*重新加载数据*/
        this.onSearch(); 
    },
    /*
     * 查询数据
     * 本地过滤
     */
    onSearch: function () {
        var me = this;
//       me.showMask();
        var fastKey = me.myGridPanel.query("[name='fastKey']")[0];
        var searchValue = fastKey.getValue().toString().toLowerCase(),
            newData = [];  //newData保存筛选出来的数据
            me.ItemList=[];
        if (searchValue == "") { 
            newData = me.gridData;
            
        } else {
            for (var i = 0, len = me.gridData.length; i < len; i++) {
                for (var j = 1, jlen = me.getStoreFields.length; j < jlen; j++) {
                    if (me.gridData[i][me.getStoreFields[j]] && me.gridData[i][me.getStoreFields[j]].toString().toLowerCase().indexOf(searchValue) >= 0) {
                        newData.push(me.gridData[i]);
                        break;
                    }
                }
            }
        } 
        /*重新加载数据*/
        me.myGridPanel.store.loadData(newData);
        me.myGridPanel.store.getProxy().data = newData; //更新在缓存的数据
        me.ItemList= newData;
        me.myGridPanel.store.loadPage(1); //重新刷新
        me.hideMask();
    },
     /*重新加载数据*/
    loadDatas:function(){
    	var me =this;
    	me.iniDataListeners();
    	me.onSearch();
    },
    initComponent:function(){
		var me = this;
		me.addEvents('itemclick', 'select');
		me.myGridPanel = me.createGridColumns();
		me.items = me.myGridPanel;
		me.callParent(arguments);
	},
	/**数据初始化*/
	iniDataListeners:function(){
        var me = this;

        var buttonsToolbar=me.myGridPanel.getComponent('buttonsToolbar');
        var radioType = buttonsToolbar.getComponent('radioType');
	    
        if(!me.ClienteleID) {
	    	JShell.Msg.error('实验室编号为空!');
	    	return;
	    }
	    if(!radioType.getValue().rb) {
	    	JShell.Msg.error('查询类型为空!');
	    	return;
	    }
	    var Type=radioType.getValue().rb;
		me.getItemAll(Type,me.ClienteleID,function(data){
			if(data.value){
				me.gridData=data.value.list;
				me.ItemList=me.gridData;
			}
		});
	},
	//获取数据
	getItemAll:function(Type,LabCode,callback){
		var me=this;
		me.showMask();
		var url = JShell.System.Path.ROOT + me.selectUrl;
//		url += "&fields=BTestItemControlVO_BLabTestItem_ItemNo,BTestItemControlVO_BLabTestItem_CName,BTestItemControlVO_TestItem_Id,BTestItemControlVO_TestItem_CName";
		url+='?Type='+Type+'&LabCode='+LabCode;
		var Str=JcallShell.String.encode('1=1');      
		url+="&where=(1=1)";		
		JShell.Server.get(url,function(data){
			if(data.success){
				data = me.changeResult(data);
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**获取数据*/
	getItemList:function(){
		var me =this;
		return me.ItemList;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],len=0,
			obj = {};
	    
		if(data && data.value){
			list=data.value;
			
			len=list.length;
			for(var i=0;i<len;i++){
				obj={
					BTestItemControlVO_BLabTestItem_ItemNo:list[i].ControlItemNo,
					BTestItemControlVO_BLabTestItem_CName:list[i].BLabTestItemCName,
					BTestItemControlVO_TestItem_Id:list[i].ItemNo,
					BTestItemControlVO_TestItem_CName:list[i].TestItemCName,
					BTestItemControlVO_TestItem_UseFlag:list[i].UseFlag ? list[i].UseFlag :'0'
				};
				obj.BTestItemControlVO_Id=list[i].Id;
				arr.push(obj);
			}
		}else{
			arr=[];
		}		
		var obj2={list:arr,count:arr.length};
		result.value=obj2;
		result.success=true;
		return result;
	},
		/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.myGridPanel.store.removeAll(); //清空数据
	},
	/**获取选中行*/
	getSelection:function(){
		var me =this;
		var Selection=me.myGridPanel.getSelectionModel().getSelection();
		return Selection;
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.myGridPanel.dockedItems.items || [],
			len = toolbars.length,
			items = [];

		for (var i = 0; i < len; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
		if (bo) {
			me.defaultLoad = true;
		}
	},
	/**显示遮罩*/
	showMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(me.loadingText);
		} //显示遮罩层
		me.enableControl(false); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(true); //启用所有的操作功能
	}
});