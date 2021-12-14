/**
 * 中心项目列表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.contrast.TestItemGrid', {
	extend: 'Shell.ux.panel.AppPanel',
	title:"中心项目列表",
	requires:[
	    'Shell.class.weixin.dict.lab.dictitem.contrast.PagingMemoryProxy'
	],
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
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
	rowNumbererWidth:50,
	border:false,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	autoSelect: false,
	 afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.myGridPanel.on({
			deselect:function(RowModel, record){
				record.set('addClss','0');
			}
		});
		
	},
    initComponent:function(){
		var me = this;
		me.addEvents('changeData');
		me.iniDataListeners();
		me.myGridPanel = me.createGrid();
		me.items = me.myGridPanel;
		me.callParent(arguments);
	},
    /*创建gridPanel */
    createGrid:function(){
    	var me = this;
    	/*创建数据列 */
        var gridColumn = [{
			xtype: 'rownumberer',text: '序号',width: me.rowNumbererWidth,align: 'center'
		},{
			text:'',dataIndex:'',width:10,
			sortable:true,menuDisabled:true,align: 'center',
			renderer: function(value, meta, record) {
				var v = "",Color='';
				var UseFlag =record.get('ItemAllItem_Visible');
				//禁用
				if(UseFlag=='0'){
					Color = '<span style="padding:0px;color:gray; border:solid 3px gray"></span>&nbsp;&nbsp;'
				}
				//启用 并且对照状态=已对照
				if(UseFlag=='1'){
                    Color = '<span style="padding:0px;color:white;"></span>&nbsp;&nbsp;'
				}
				v = Color;
				var addClss=record.get('addClss')
            	if(addClss=='1'){
            	    meta.style = 'background-color:#8EE5EE' ;
            	}
				return v;
			}
		},{
			text:'中心项目编号',dataIndex:'ItemAllItem_Id',width:100,
			isKey:true,sortable:false,menuDisabled:true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var addClss=record.get('addClss')
            	var v = value || '';
            	if(addClss=='1'){
            	    meta.style = 'background-color:#8EE5EE' ;
            	}
            	return v;
           }
		},{
			text:'中心项目简码',dataIndex:'ItemAllItem_ShortCode',width:150,
			sortable:false,menuDisabled:true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var addClss=record.get('addClss')
            	var v = value || '';
            	if(addClss=='1'){
            	    meta.style = 'background-color:#8EE5EE' ;
            	}
            	return v;
           }
//			defaultRenderer:true
		},{
			text:'中心项目名称',dataIndex:'ItemAllItem_CName',minWidth:150,
			flex:1,sortable:false,menuDisabled:true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var addClss=record.get('addClss')
            	var v = value || '';
            	if(addClss=='1'){
            	    meta.style = 'background-color:#8EE5EE' ;
            	}
            	return v;
           }
//			defaultRenderer:true
		},{
			text:'是否使用',dataIndex:'ItemAllItem_Visible',width:150,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'addClss',dataIndex:'addClss',width:150,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
        for (var i = 0; i < gridColumn.length; i++) {
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
//          viewConfig:{getRowClass:me.changeRowClass()},
            dockedItems:[me.createPagingtoolbar(), me.createButtontoolbar()]
        });
        return picker; 
    },
    changeRowClass:function(){
    	var me =this;
    	
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
//          listeners: {
//				load: function(store, records, successful) {
//					me.doAutoSelect(records, true);
//				}
//			}
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
                me.fireEvent('changeData', me.myGridPanel);
            },
            listeners: {
                change:function(){
                	me.fireEvent('changeData', me.myGridPanel);
                }
            }
		};
		return Ext.create('Ext.toolbar.Paging', config);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items =  [];
        items.push({
	        xtype: 'label', text: '中心项目列表',itemId:'labMarketPrice',
	        style: "font-weight:bold;color:#0000EE;",
	        margin: '0 0 10 10'
		},'->',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
		    name: 'fastKey',
		    emptyText: '中心项目名称/简码',
			listeners:{
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER){
						me.onSearch();
						me.fireEvent('changeData', me.myGridPanel);
					}
				}
            }
        });
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**刷新数据*/
    refreshData:function(){
        //清空筛选输入框的数据,
        var fastKey = this.myGridPanel.query("[name='fastKey']")[0];
        fastKey.setValue("");
        /*重新加载数据*/
        this.onSearch(); 
    },
    /*
     * 查询数据
     * 本地过滤
     */
    onSearch: function () {
        var me = this;
         me.showMask();
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
         me.ItemList= newData;
        
        /*重新加载数据*/
        me.myGridPanel.store.loadData(newData);
        me.myGridPanel.store.getProxy().data = newData; //更新在缓存的数据
        me.myGridPanel.store.loadPage(1); //重新刷新
//      me.fireEvent('changeData', me.myGridPanel);
        me.hideMask();
    },
   
	/**数据初始化*/
	iniDataListeners:function(){
        var me = this;
        var response={};
		me.getItemAll(function(data){
			if(data.value){
				me.gridData=data.value.list;
				me.ItemList=me.gridData;
			}
		});
	},
	//获取数据
	getItemAll:function(callback){
		var me=this;
		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += "&fields=ItemAllItem_Id,ItemAllItem_ShortCode,ItemAllItem_CName,ItemAllItem_Visible";
		JShell.Server.get(url,function(data){
			if(data.success){
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
	/**获取选中行*/
	getSelection:function(){
		var me =this;
		var Selection=me.myGridPanel.getSelectionModel().getSelection();
		return Selection;
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
	},
		/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.myGridPanel.store.removeAll(); //清空数据
	}
});