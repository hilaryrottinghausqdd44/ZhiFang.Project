/**
 * 树面板
 * 【功能】
 * 带有刷新、全部展开、全部收缩、快速检索功能；
 * 快速检索功能支持按钮清空，也支持ESC键清空，文字改变时当即检索；
 * 加载数据时自动开启保护层，禁用刷新按钮，加载完毕保护层消失，刷新重新可用；
 * 叶子节点和非叶子节点的图标可配，默认采用系统整体样式；
 * 
 * 【可配参数】
 * leafIconCls：叶子节点图标css，默认采用系统样式；
 * nLeafIconCls：非叶子节点图标css，默认采用系统样式；
 * searchWidth：检索框的宽度，默认120；
 * rootVisible：是否显示根节点，默认false；
 * defaultLoad：是否默认加载数据，默认false；
 * defaultRootProperty：子节点对象名，默认Tree；
 * 
 * 【必配参数】
 * url：服务的路径；
 * 
 * 【公开方法】
 * load([parmas])，可以接收参数对象，例如{pId=123,where="name like '%张%'"}，可以不传
 * 
 * 【示例】
 * {
 *		xtype:'zhifanguxtreepanel',itemId:'items',title:'仪器-项目',
 *		url:getRootPath()+'/QCService.svc/QC_RJ_GetJudgeEquipItemTree',
 *		leafIconCls:'userImg16',nLeafIconCls:'usersImg16'
 *	}
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.BasicTreePanel',{
	extend:'Ext.tree.Panel',
	type:'treepanel',
	alias:'widget.zhifanguxbasictreepanel',
	title:'',
	width:800,
	hieght:600,
	useArrows:true,
	containerScroll:true,//是否支持滚动条 
	autoScroll:false,//内容溢出的时候是否产生滚动条
	
	/**是否已经加载过数据*/
	hasLoad:false,
	/**条件*/
	params:null,
	
    //可配参数
	/**叶子节点图标css*/
    leafIconCls:null,
    /**非叶子节点图标css*/
    nLeafIconCls:null,
    /**检索框的宽度*/
    searchWidth:120,
    /**是否显示根节点*/
    rootVisible:false,
    /**默认加载数据*/
    defaultLoad:false,
    /**子节点对象名*/
    defaultRootProperty:'Tree',
    
    //必配参数
    /**服务的路径*/
    url:null,
   
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化监听
		me.initListeners();
		if(me.defaultLoad){
			me.load();
		}else{
			var toptoolbar = me.getComponent('toptoolbar');
			toptoolbar && toptoolbar.disable();
		}
	},
	/**
	 * 初始化组件信息
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.initParams();//初始化参数
		me.initView();//初始化视图
		me.callParent(arguments);
	},
	/**
	 * 初始化参数
	 * @private
	 */
	initParams:function(){
		var me = this;
		//加载数据时的保护罩
		me.viewConfig = me.viewConfig || {};
		me.viewConfig.loadMask = me.viewConfig.loadMask || true;
		me.viewConfig.loadingText = me.viewConfig.loadingText || '数据加载中...';
		me.viewConfig.emptyText = me.viewConfig.emptyText || '没有找到数据';
		//根节点
		me.root = me.root || {text:'ROOT',iconCls:'main-lefttree-root-img-16',expanded:false};
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		me.dockedItems = me.dockedItems || me.createDockedItems();//创建挂靠功能
		me.store = me.store || me.createStore();//创建数据集
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',
			dock:'top',
			itemId:'toptoolbar',
			items:[{
				tooltip:'刷新数据',itemId:'refresh',iconCls:'build-button-refresh',
				handler:function(but){me.load(me.params);}
			},{
				tooltip:'全部展开',iconCls:'build-button-arrow-out',
				handler:function(but){me.expandAll();}
			},{
				tooltip:'全部收缩',iconCls:'build-button-arrow-in',
				handler:function(but){me.collapseAll();}
			},'-',{
				xtype:'trigger',itemId:'searchText',emptyText:'快速检索',width:me.searchWidth,
				triggerCls:'x-form-clear-trigger',enableKeyEvents:true,
		        onTriggerClick:function(){this.setValue('');me.clearFilter();},
		        listeners:{
		            keyup:{
		                fn:function(field,e){
		                    var bo = Ext.EventObject.ESC == e.getKey();
		                    bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
		                }
		            }
		        }
			}]
		}];
		return dockedItems;
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.TreeStore',{
			fields:['text','expanded','leaf','icon','url','tid'],
			defaultRootProperty:me.defaultRootProperty,
			autoLoad:false,
			proxy:{
				type:'ajax',
				url:me.url,
				extractResponseData:function(response){return me.changeStoreData(response);}
			}
		});
		return store;
	},
	/**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData:function(response){
		var me = this;
    	var data = Ext.JSON.decode(response.responseText);
		data.Tree = [];
		if(data.success){
			if(data.ResultDataValue && data.ResultDataValue != ""){
				var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	    		data.Tree = ResultDataValue ? ResultDataValue.Tree : [];
	    		data.Tree = me.changeTree(data.Tree);
			}
		}else{
			var html = "<center><b style='color:red;font-size:x-large'>" + data.ErrorInfo + "</b></center>";
			me.getView().update(html);
		}
    	response.responseText = Ext.JSON.encode(data);
    	return response;
  	},
  	/**
  	 * 数据集的值转化
  	 * @private
  	 * @param {} obj
  	 * @return {}
  	 */
  	changeTree:function(obj){
  		var me = this;
  		var tree = Ext.clone(obj);
  		
  		var changeArray = function(arr){
  			for(var i in arr){
  				arr[i] = changeObj(arr[i]);
  			}
  			return arr;
  		};
  		
  		var changeObj = function(o){
  			if(o.leaf){
				me.leafIconCls && (o.iconCls = me.leafIconCls);
			}else{
				me.nLeafIconCls && (o.iconCls = me.nLeafIconCls);
			}
			if(o[me.defaultRootProperty] && o[me.defaultRootProperty].length > 0){
				o[me.defaultRootProperty] = changeArray(o[me.defaultRootProperty]);
			}
			return o;
  		};
  		tree = changeArray(tree);
  		
  		return tree;
  	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		//数据集监听
		me.store.on({
		    beforeload:function(store,operation){me.beforeLoad(store,operation);},
		    load:function(store,node,records,successful,eOpts){me.afterLoad(store,node,records,successful,eOpts);}
		});
	},
	/**
  	 * 加载数据前
  	 * @private
  	 * @param {} store
  	 * @param {} operation
  	 */
  	beforeLoad:function(store,operation){
  		var me = this;
  		var refresh = me.getComponent('toptoolbar').getComponent('refresh');
  		refresh && refresh.disable();
  	},
  	/**
  	 * 加载数据后
  	 * @private
  	 * @param {} store
  	 * @param {} node
  	 * @param {} records
  	 * @param {} successful
  	 * @param {} eOpts
  	 */
  	afterLoad:function(store,node,records,successful,eOpts){
  		var me = this;
  		var refresh = me.getComponent('toptoolbar').getComponent('refresh');
  		refresh && refresh.enable();
  		me.hasLoad = true;
  	},
  	/**
  	 * 根据显示文字过滤
  	 * @private
  	 * @param {} text
  	 */
  	filterByText:function(text){
        this.filterBy(text,'text');
    },
    /**
     * 根据值和字段过滤
     * @private
     * @param {} text 过滤的值
     * @param {} by 过滤的字段
     */
    filterBy:function(text,by){
        this.clearFilter();
        var view = this.getView(),
            me = this,
            nodesAndParents = [];
            
        this.getRootNode().cascadeBy(function(tree,view){
            var currNode = this;
            if(currNode && currNode.data[by]){
            	//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
            	if(currNode.data[by].toString().toLowerCase().indexOf(text.toLowerCase()) > -1){
            		me.expandPath(currNode.getPath());
	                while(currNode.parentNode){
	                    nodesAndParents.push(currNode.id);
	                    currNode = currNode.parentNode;
	                }
            	}
            }
        },null,[me,view]);
        
        this.getRootNode().cascadeBy(function(tree,view){
            var uiNode = view.getNodeByRecord(this);
            if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)){
                Ext.get(uiNode).setDisplayed('none');
            }
        },null,[me,view]);
    },
    /**
     * 清空过滤
     * @private
     */
    clearFilter:function(){
        var view = this.getView();
        this.getRootNode().cascadeBy(function(tree,view){
            var uiNode = view.getNodeByRecord(this);
            if(uiNode){
                Ext.get(uiNode).setDisplayed('table-row');
            }
        },null,[this,view]);
    },
	/**
  	 * 刷新树数据
  	 * @public
  	 * @param {} params 参数对象，例如{pId=123,where="name like '%张%'"}
  	 * @param {} isPrivate 内部调用
  	 */
  	load:function(params){
  		var me = this;
		var arr = [];
  		//拼接带条件的url
  		if(Ext.typeOf(params) === 'object'){
  			for(var i in params){
  				var value = i + '=' + (i == 'where') ? encodeString(params[i]) : params[i];
  				arr.push(value);
  			}
  			me.params = params;
  		}
  		var url = me.url;
  		if(arr.length > 0){
  			url = me.url + '?' + arr.join('&');
  		}
  		me.store.proxy.url = url;
  		
  		if(!me.hasLoad){
  			var toptoolbar = me.getComponent('toptoolbar');
			toptoolbar && toptoolbar.enable();
  			me.getRootNode().expand();
  			me.hasLoad = true;
  		}else{
  			me.store.load();
  		}
  	}
});