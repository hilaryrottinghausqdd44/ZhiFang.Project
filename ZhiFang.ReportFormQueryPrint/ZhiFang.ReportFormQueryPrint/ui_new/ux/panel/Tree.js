/**
 * 树面板类
 * @author Jcall
 * @version 2014-09-02
 */
Ext.define('Shell.ux.panel.Tree',{
	extend:'Ext.tree.Panel',
	alias:'widget.uxtree',
	
	requires:['Shell.ux.ButtonsToolbar'],
	
	mixins:[
		'Shell.ux.server.Ajax',
		'Shell.ux.panel.Panel'
	],
	
	/**是否复选*/
	multiSelect:false,
	/**显示根节点*/
	rootVisible:true,
	/**子节点的属性名*/
	defaultRootProperty:'Tree',
	/**根节点*/
	root:{
		text:'根节点',
		leaf:false,
		expanded:true
	},
	
	/**叶子节点图标css*/
    leafIconCls:null,
    /**非叶子节点图标css*/
    nLeafIconCls:null,
    
    /**叶子节点图标路径*/
    leafIcon:null,
    /**非叶子节点图标路径*/
    nLeafIcon:null,
	
    /**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**开启右键菜单*/
	hasContextMenu:false,
	/**默认加载数据*/
	defaultLoad:false,
	/**是否显示错误信息*/
	showErrorInfo:true,
	/**数据集属性*/
	storeConfig:{},
	
	/**需要的数据字段*/
	fields:[],
	/**过滤数据列*/
	filterFields:['text'],
	/**获取列表数据服务*/
	selectUrl:'',
	
	/**默认数据条件*/
	defaultWhere:'',
	/**内部数据条件*/
	internalWhere:'',
	/**外部数据条件*/
	externalWhere:'',
	
	/**功能栏*/
	toolbars:null,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//开启右键快捷菜单设置
		me.onContextMenu();
		
		me.on({
			boxready:function(){
				me.defaultLoad ? me.firstLoad() : me.disableControl();
				me.boxIsReady();
			},
			expand:function(p,d){
				if(me.isCollapsed){me.firstLoad();}
				me.isCollapsed = false;
			},
			checkchange:function(node,checked){
				me.onNodeCheckChange(node,checked);
			}
		});
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('contextmenu','afterload');
		me.toolbars = me.createToolbars();
		me.store = me.createStore();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建功能栏*/
	createToolbars:function(){
		var me = this,
			toolbars = me.toolbars || [{dock:'top',buttons:[
				'refresh','-',
				{xtype:'uxbutton',iconCls:'button-arrow-in',tooltip:'全部收缩',itemId:'collapseAll'},
				{xtype:'uxbutton',iconCls:'button-arrow-out',tooltip:'全部展开',itemId:'expandAll'},
				'-',
				{btype:'searchtext',width:100,emptyText:'快速检索',
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
			    }
			]
		}];
			
		return toolbars;
	},
	/**创建数据集*/
	createStore:function(){
		var me = this,
			config = {
				defaultRootProperty:me.defaultRootProperty,
				fields:me.getStoreFields(),
				autoLoad:false
			};
			
		if(me.selectUrl){
			config.proxy = {
				type:'ajax',
				url:Shell.util.Path.rootPath + me.selectUrl,
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						success = result.success;
					if(!success && me.showErrorInfo){me.showError(result.ErrorInfo);}
					
					response = me.responseToTree(response);
					
					var res = Ext.JSON.decode(response.responseText);
					res[me.defaultRootProperty] = me.changeTreeData(res[me.defaultRootProperty]);
						
					response.responseText = Ext.JSON.encode(res);
					
					return response;
				}
			};
			config.listeners = {//数据集监听
			    beforeload:function(){return me.onBeforeLoad();},
			    load:function(store,node,records,successful){
			    	me.onAfterLoad(store,node,records,successful);
			    	me.fireEvent('afterload',me,node,records,successful);
			    }
			};
		}else{
			config.root = Ext.clone(me.root);
			delete me.root;
		}
		
		return Ext.create('Ext.data.TreeStore',Ext.apply(config,me.storeConfig || {}));
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			fields = me.fields;
			
		return fields;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			toolbars = me.toolbars || [],
			length = toolbars.length,
			dockedItems = [];
		
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
	/**加载数据前*/
	onBeforeLoad:function(){
		var me = this;
  		me.disableControl();//禁用 所有的操作功能
  		if(!me.defaultLoad) return false;
  		if(me.hasLoadMask && me.body){me.body.mask('数据加载中...');}//显示遮罩层
	},
	/**加载数据后*/
	onAfterLoad:function(store,node,records,successful){
		var me = this;
		
		me.enableControl();//启用所有的操作功能
		if(me.hasLoadMask && me.body){me.body.unmask();}//隐藏遮罩层
		
		if(!successful || records.length == 0){
			node.removeAll();
			return;
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			where = '';
		
		//默认条件
		if(me.defaultWhere && me.defaultWhere != ''){
			where += '(' + me.defaultWhere +') and ';
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != ''){
			where += '(' + me.internalWhere + ') and ';
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != ''){
			where += '(' + me.externalWhere + ') and ';
		}
		
		where = where.slice(-5) == ' and ' ? where.slice(0,-5) : where;
		
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + Shell.util.String.encode(where);
		
		return url;
	},
	
	/**全部收缩处理*/
	onCollapseAllClick:function(){
		this.collapseAll();
	},
	/**全部展开处理*/
	onExpandAllClick:function(){
		this.expandAll();
	},
	
	/**根据显示文字过滤*/
  	filterByText:function(text){
  		var me = this,
  			arr = me.filterFields || ['text'];
  			
        me.filterBy(text,arr);
    },
    /**
     * 根据值和字段过滤
     * @private
     * @param {} text 过滤的值
     * @param {} by 过滤的字段
     */
    filterBy:function(text,by){
    	var me = this,
    		byArr = Ext.typeOf(by) == 'array' ? by : Ext.typeOf(by) == 'string' ? [by] : [],
    		len = byArr.length,
	       	view = this.getView(),
            nodesAndParents = [];
            
        me.clearFilter();
        me.getRootNode().cascadeBy(function(tree,view){
            var currNode = this;
            if(currNode){
            	for(var i=0;i<len;i++){
	            	var v = currNode.data[byArr[i]];
	            	if(v){
	            		//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
		            	if(v.toString().toLowerCase().indexOf(text.toLowerCase()) > -1){
		            		me.expandPath(currNode.getPath());
			                while(currNode.parentNode){
			                    nodesAndParents.push(currNode.id);
			                    currNode = currNode.parentNode;
			                }
		            	}
	            	}
	            }
            }
        },null,[me,view]);
        
        me.getRootNode().cascadeBy(function(tree,view){
            var uiNode = view.getNodeByRecord(this);
            if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)){
                Ext.get(uiNode).setDisplayed('none');
            }
        },null,[me,view]);
    },
	/**清空过滤*/
    clearFilter:function(){
        var view = this.getView();
        this.getRootNode().cascadeBy(function(tree,view){
            var uiNode = view.getNodeByRecord(this);
            if(uiNode){
                Ext.get(uiNode).setDisplayed('table-row');
            }
        },null,[this,view]);
    },
	
    /**数据集的值转化*/
  	changeTreeData:function(obj){
  		var me = this;
  		var tree = Ext.clone(obj);
  		
  		var changeArray = function(arr){
  			for(var i in arr){
  				arr[i] = changeObj(arr[i]);
  			}
  			return arr;
  		};
  		
  		var changeObj = function(o){
  			if(me.multiSelect){o.checked = o.checked ? true : false;}
  			if(o.leaf){
				if(me.leafIconCls){o.iconCls = me.leafIconCls};
				if(me.leafIcon){o.icon = me.leafIcon};
			}else{
				if(me.nLeafIconCls){o.iconCls = me.nLeafIconCls};
				if(me.nLeafIcon){o.icon = me.nLeafIcon};
			}
			if(o[me.defaultRootProperty] && o[me.defaultRootProperty].length > 0){
				o[me.defaultRootProperty] = changeArray(o[me.defaultRootProperty]);
			}
			return o;
  		};
  		tree = changeArray(tree);
  		
  		return tree;
  	},
    /**启动加载*/
  	firstLoad:function(){
  		var me = this,
  			collapsed = me.getCollapsed();
  		
  		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		me.store.proxy.url = me.getLoadUrl();//查询条件
  		
  		me.getRootNode().expand();
  	},
  	
  	/**节点选择变化处理*/
  	onNodeCheckChange:function(node,checked){
  		var me = this;
  		//向上遍历父结点
		var nodep = function(nod){
			var bnode = true;
			nod.eachChild(function(child){
				if(!child.data.checked){bnode = false;return;}
			});
			return bnode;
		};
		//父级选中操作
		var parentnode=function(nod){
			if(!nod.parentNode) return;
			nod.parentNode.set('checked',nodep(nod.parentNode));
			parentnode(nod.parentNode);
		};
		
		//遍历子结点 选中与取消选中操作
		var chd=function(nod,check){
			nod.set('checked',check);
			nod.eachChild(function(child){
				chd(child,check);
			});
		};
		//选中与取消所有子结点
		chd(node,checked);
		//进行父级选中操作 
		parentnode(node);
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
		me.store.proxy.url = me.getLoadUrl();//查询条件
		me.store.load();
	},
	/**@public 清空数据,禁用功能按钮*/
	clearData:function(){
		var me = this;
		me.disableControl();//禁用 所有的操作功能
		me.store.removeAll();//清空数据
	}
});