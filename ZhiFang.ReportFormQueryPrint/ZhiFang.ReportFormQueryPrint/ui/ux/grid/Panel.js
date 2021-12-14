/**
 * 列表面板类
 * @author Jcall
 * @version 2014-08-04
 * 
 * toolbars 功能栏数组
 * pagingtoolbar 分页栏,支持5种:不分页(basic);数字分页(number);进度条分页(sliding);滚动分页(progressbar),简单分页(simple)
 * 
 */
Ext.define('Shell.ux.grid.Panel',{
	extend:'Ext.grid.Panel',
	
	requires:['Shell.ux.ButtonsToolbar'],
	mixins:[
		'Shell.ux.server.Ajax',
		'Shell.ux.PanelController'
	],
	
	/**开启右键菜单*/
	hasContextMenu:false,
	/**默认选中第一行*/
	autoSelect:true,
	/**默认加载数据*/
	defaultLoad:false,
	/**开启单元格内容提示*/
	tooltip:false,
	/**是否显示错误信息*/
	showErrorInfo:true,
	/**分页栏类型*/
	pagingtoolbar:null,
	/**数据集属性*/
	storeConfig:{},
	
	/**启用数据列排序功能*/
	columnSortable:false,
	/**启用数据列移动功能*/
	columnDraggable:false,
	/**启用数据列隐藏功能*/
	columnHideable:false,
	
	/**主键列*/
	PKColumn:'Id',
	/**获取列表数据服务*/
	selectUrl:'',
	/**删除数据服务*/
	delUrl:'',
	
	/**默认数据条件*/
	defaultWhere:'',
	/**内部数据条件*/
	internalWhere:'',
	/**外部数据条件*/
	externalWhere:'',
	
	/**每页最大数量*/
	infinityPageSize:999999,
	/**默认每页数量*/
	defaultPageSize:100,
	/**视图面板属性*/
	_viewConfig:{
		emptyText:'没有数据！',
		loadingText:'获取数据中,请等待...',
		loadMask:true,
		enableTextSelection:true
	},
	/**分页栏下拉框数据*/
	pageSizeList:[[10,10],[20,20],[50,50],[100,100],[200,200],[300,300],[400,400],[500,500]],
	
	/**查询组件内部编号*/
	searchTextItemId:'searchtext',
	/**弹出窗口关闭时处理:关闭destroy、隐藏hidden*/
	openWinCloseAction:'destroy',
	/**是否远程排序*/
	remoteSort:true,
	/**本地数据*/
	data:null,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		//视图准备完毕
		me.on({
			boxready:function(){
				if(me.selectUrl){
					me.defaultLoad ? me.load(null,true) : me.disableControl();
				}else{
					if(!me.data) me.disableControl();
				}
				me.boxIsReady();
			},
			expand:function(p,d){
				if(me.isCollapsed && me.selectUrl){me.load(null,true);}
				me.isCollapsed = false;
			}
		});
	},
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('contextmenu','afterload');
		
		me.viewConfig = Ext.apply(me._viewConfig,(me.viewConfig || {}));
		
		me.store = me.createStore();
		me.columns = me.createColumns();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建列表数据集*/
	createStore:function(){
		var me = this,
			url = me.selectUrl,
			type = me.pagingtoolbar,
			data = me.data,
			config = {};
		config.fields = me.getStoreFields();
		
		if(data && data.length > 0){
			config.data = data;
		}else if(url){
			config.proxy = {
				type:'ajax',
				url:'',
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						success = result.success;
					if (!success && me.showErrorInfo) { me.showError(result.ErrorInfo); }
					return me.responseToList(response);
				}
			};
			config.listeners = {//数据集监听
			    beforeload:function(){return me.onBeforeLoad();},
			    load:function(store,records,successful){
			    	me.fireEvent('afterload',me,records || [],successful);
			    	me.onAfterLoad(records,successful);
			    }
			};
		}
		
		if(type == 'basic'){
			config.pageSize = me.infinityPageSize;
		}else if(type == 'number' || type == 'sliding' || type == 'progressbar' || type == 'simple'){
			config.pageSize = me.defaultPageSize;
			config.remoteSort = me.remoteSort;
		}
			
		return Ext.create('Ext.data.Store',Ext.apply(config,me.storeConfig || {}));
	},
	/**创建数据列*/
	createColumns:function(){
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			type;
		
		//数据列基础属性默认值
		for(var i=0;i<length;i++){
			if(columns[i].xtype == 'rownumberer') continue;
			type = columns[i].type;
			if(type == 'key'){me.PKColumn = columns[i].dataIndex;}
			
			if(type == 'datetime'){
				columns[i] = me.applyDatetimeColumn(columns[i]);
			}else if(type == 'isuse'){
				columns[i] = me.applyIsuseColumn(columns[i]);
			}else{
				columns[i] = me.applyColumn(columns[i]);
			}
			
			columns[i] = Ext.applyIf(columns[i],{
				sortable:me.columnSortable,
				draggable:me.columnDraggable,
				hideable:me.columnHideable
			});
		}
		
		return columns;
	},
	/**转化一般数据列*/
	applyColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
                    return value;
                }
			});
		}
		
		return column;
	},
	/**转化日期时间数据列*/
	applyDatetimeColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value == null ? '' : Shell.util.Date.toString(value);
                    if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
                    return value;
                }
			});
		}else{
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value == null ? '' : Shell.util.Date.toString(value);
                    return value;
                }
			});
		}
		
		return column;
	},
	/**转化是否使用数据列*/
	applyIsuseColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value.toString() == 'true' ? "<b style='color:green'>是</b>" : "<b style='color:red'>否</b>";
                    if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
                    return value;
                }
			});
		}else{
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value.toString() == 'true' ? "<b style='color:green'>是</b>" : "<b style='color:red'>否</b>";
                    return value;
                }
			});
		}
		
		return column;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			toolbars = me.toolbars || [],
			length = toolbars.length,
			dockedItems = [];
		
		//分页栏
		var pagingtoolbar = me.createPagingToolbar();
		pagingtoolbar && dockedItems.push(pagingtoolbar);
			
		for(var i=0;i<length;i++){
			dockedItems.push(Ext.apply({
				autoScroll:true,
				dock:'top',
				xtype:'uxbuttonstoolbar',
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					},
					search:function(toolbar,search,value){
						me.searchValue = value;
						me.onSearch();
					}
				}
			},toolbars[i]));
		}
		return dockedItems;
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			fields = [];
			
		for(var i=0;i<length;i++){
			if(columns[i].dataIndex){
				fields.push(columns[i].dataIndex);
			}
		}
		
		return fields;
	},
	/**创建分页栏*/
	createPagingToolbar:function(){
		var me = this,
			type = me.pagingtoolbar,
			itemId = 'pagingtoolbar',
			dock =  'bottom',
			pagingtoolbar = null;
			
		if(type == 'basic'){
			pagingtoolbar = {
				xtype:'toolbar',itemId:itemId,dock:dock,autoScroll:true,
				items:[{xtype:'label',itemId:'count',text:' 共 0 条 ',margin:'0 4 0 4',width:'100%',style:'textAlign:right;'}]
			};
			me.setCount = function(count){
				me.getComponent(itemId).getComponent('count').setText(' 共 ' + (count || 0) + ' 条 ');
			};
			me.store.on({
				load:function(store,records,successful){
					var count = successful ? records.length : 0;
					me.setCount(count);
				}
			});
		}else{
			var combo = {
				xtype:'combo',mode:'local',editable:false,
	            displayField:'text',valueField:'value',
	            width:50,value:me.defaultPageSize,
				store:new Ext.data.SimpleStore({
					fields:['text','value'],
					data:me.pageSizeList
				}),
				listeners:{
					change:function(com,newValue){
						me.store.pageSize = newValue;
						me.onSearch();
					}
				}
			};
			var con = {
				xtype:'pagingtoolbar',itemId:itemId,autoScroll:true,
				dock:dock,displayInfo:true,store:me.store,
				items:['-','每页',combo,'条','-']
			};
				
			if(type == 'sliding'){
				con.plugins = Ext.create('Ext.ux.SlidingPager',{});
			}else if(type == 'progressbar'){
				con.plugins = Ext.create('Ext.ux.ProgressBarPager',{});
			}else if(type == 'simple'){
				con.xtype = 'uxsimplepagingtoolbar';
				con.items = ['-',combo,'-'];
				con = Ext.create('Shell.ux.grid.SimplePagingToolbar',con);
			}
			
			if(type == 'number' || type == 'sliding' || type == 'progressbar' || type == 'simple'){
				pagingtoolbar = con;
			}
		}
		
		return pagingtoolbar;
	},
	/**加载数据前*/
	onBeforeLoad:function(){
		var me = this;
		
  		me.disableControl();//禁用 所有的操作功能
  		if(!me.defaultLoad) return false;
  		me.store.proxy.url = me.getLoadUrl();//查询条件
	},
	/**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		
		me.enableControl();//启用所有的操作功能
		
		if(!successful || records.length == 0){
			me.store.removeAll();
			return;
		}
		
		var autoSelect = me.autoSelect,
			type = Ext.typeOf(autoSelect),
			num = autoSelect === true ? 0 : -1;
  		
  		if(type === 'string'){//需要选中的行主键
			num = me.store.find(me.PKColumn,autoSelect);
		}
		//选中行号为num的数据行
		if(num >= 0){me.getSelectionModel().select(num);}
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			arr = [];
		
		//默认条件
		if(me.defaultWhere && me.defaultWhere != ''){
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != ''){
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != ''){
			arr.push(me.externalWhere);
		}
		
		var where = arr.join(" and ");
		
		if(where){
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + Shell.util.String.encode(where);
		}
		
		return url;
	},
	/**重写查询功能*/
	onSearch:function(){
		this.internalWhere = this.searchValue;
		this.load(null,true);
	},
	/**@public 根据where条件加载数据*/
	load:function(where,isPrivate){
		var me = this,
			collapsed = me.getCollapsed();
			
		me.defaultLoad = true;
		me.externalWhere = isPrivate ? me.externalWhere : where;
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.store.currentPage = 1;
		me.store.load();
	},
	/**清空数据,禁用功能按钮*/
	clearData:function(){
		var me = this;
		me.disableControl();//禁用 所有的操作功能
		me.store.removeAll();//清空数据
	}
});