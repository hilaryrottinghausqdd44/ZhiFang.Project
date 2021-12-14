/***
 * 右区域:数据过滤条件角色树
 * 
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.roleDataFiltersTree', {
    extend:'Ext.tree.Panel',
    alias:'widget.roleDataFiltersTree',
    title:'',
    selectId:null,
    hideNodeId:null,
    whereFields:'',
    filterfield:'text',
    childrenField:'Tree',
    linkageType:false,
    viewConfig:'',
    
    getValue:function() {
        var myTree = this;
        var arrTemp = myTree.getSelectionModel().getSelection();
        return arrTemp;
    },

    internalWhere:'',
    externalWhere:'',
    afterRender:function() {
        var me = this;
        me.addEvents('deleteClick');//删除按钮点击
        me.addEvents('editClick');//
        me.addEvents('showClick');//
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    ClassCode:'BTDAppComponents_ClassCode',
    lines:false,
    useArrows:true,
    isTure:false,
    rootVisible:false,
    checked:false,
    filterBar:'top',
    functionBar:'top',
    positionBar:'top',
    autoScroll:true,
    isLoaded:false,
    //width:420,
    //height:380,
    isFiltration:true,
    isFunction:false,
    isbuttonBar:false,
    selectServerUrl:getRootPath() + '/RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID',
    fields:['pid','value','ParentID','Id','tid','url','icon','leaf','objectType','text','expanded'],
    createStore:function() {
        var me = this;
        var store =null;
	        store = Ext.create('Ext.data.TreeStore', {
	            fields:me.fields,
                remoteSort:false,
                defaultLoad : false,
                sorters:[],
	            proxy:{
	                type:'ajax',
	                url:me.selectServerUrl,
	                extractResponseData:function(response) {
	                    return me.changeStoreData(response);
	                }
	            },
	            defaultRootProperty:me.childrenField,
	            listeners:{
	                load:function(treeStore, node, records, successful, eOpts) {
                        if(successful){
                        }
                    }
	            }
	        });
        return store;
    },
    changeStoreData:function(response) {
        var me = this;
        var result = Ext.JSON.decode(response.responseText);
        if(result.success){
            result[me.childrenField] = [];
            if(result.ResultDataValue && result.ResultDataValue != ''){
                var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                result[me.childrenField] = ResultDataValue.Tree;
            }
            
            var changeNode = function(node){
                var value = node['value'];
                for(var i in value){
                    node[i] = value[i];
                    //图片地址处理
	                if(node[i]['objectType'] && node[i]['objectType'] == "RBACRowFilter"){
	                    node[i]['icon'] =getRootPath() + "/ui/css/images/icons/list.PNG";
	                }else if(node[i]['objectType']== "RBACRole"){
	                    node[i]['icon'] = getRootPath() + "/ui/css/images/icons/default.png";
	                }
                }
                //默认所有都不展开
                node['expanded'] =false;
                //图片地址处理
                if(node['objectType'] && node['objectType'] == "RBACRowFilter"){
                    node['icon'] = getRootPath() + "/ui/css/images/icons/list.PNG";
                }else if(node['objectType']== "RBACRole"){
	                node['icon'] = getRootPath() + "/ui/css/images/icons/default.png";
                }
	            var children = node[me.childrenField];
	            if(children){
	                changeChildren(children);
	            }
            };
            var changeChildren = function(children){
                Ext.Array.each(children,changeNode);
            };
            
            var children = result[me.childrenField];
            changeChildren(children);
        }else{
            Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ '获取数据失败' +'</b>】');
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    createTreeColumns:function() {
        var me = this;
        var columns=[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:300,
        sortable:true,
        dataIndex:'text'
    }, {
        text:'主键ID',
        dataIndex:'id',
        width:10,
        sortable:false,
        hidden:true,
        hideable:false
    }, {
        text:'主键ID',
        dataIndex:'tid',
        width:10,
        sortable:false,
        hidden:true,
        hideable:false
    }, {
        xtype:"actioncolumn",
        text:"操作列",
        width:80,
        align:"center",
        itemId:"Action",
        items:[{
            iconCls:"build-button-edit hand",
            tooltip:"修改数据过滤条件信息",
            handler:function(grid,rowIndex,colIndex,item,e,record){
                me.fireEvent('editClick',grid,rowIndex,colIndex,item,e,record);
            }
        },
        {
            iconCls:"blank16",
            tooltip:"",
            handler:function(grid,rowIndex,colIndex,item,e,record){
                //me.fireEvent('showClick',grid,rowIndex,colIndex,item,e,record);
            }
        },
         {
	        iconCls:"build-button-delete hand",
	        tooltip:"删除数据过滤条件/角色信息",
	        handler:function(grid,rowIndex,colIndex,item,e,record){
	            me.fireEvent('deleteClick',grid,rowIndex,colIndex,item,e,record);
	        }  
          } ]
    } ];
        return columns;
    },

    ctreatetoolsBar:function() {
        var me = this;
        var filterBarArr = [];
        var tt = '';
        if (me.isFiltration == true) {
            filterBarArr.push(me.createtoolsAddFilter());
            filterBarArr.push(me.createminusBtn());
            filterBarArr.push(me.createplusBtn());
        }
        var bottom={
	        xtype:'toolbar',
	        itemId:'toolbar-bottom',
	        dock:'bottom',
	        items:[{
	            xtype:'label',
	            text:'',
	            itemId:'count'
	        }]};
        tt = [ {
                    xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:'top',
                    items:filterBarArr
            }];
        return tt;
    },
   /**
     * 显示总条数
     * @private
     * @param {} count
     */
    setCount:function(count){
        var me = this;
        var bottomtoolbar = me.getComponent('toolbar-bottom');
        if(bottomtoolbar){
            var com = bottomtoolbar.getComponent('count');
            if(com&&count>0){
                var str = '共'+count+'条';
                com.setText(str,false);
            }else{
                com.setText('',false);
            }
        }
    },
    createtoolsAddFilter:function() {
        var me = this;
        var filter = {
            xtype:'textfield',
            fieldLabel:'检索过滤',
            itemId:'filterText',
            labelAlign:'right',
            labelWidth:65,
            enableKeyEvents:true,
            listeners:{
                keyup:{
                    fn:function(field, e) {
                        if (Ext.EventObject.ESC == e.getKey()) {
                            this.setValue('');
                            me.clearFilter();
                        } else {
                            me.filterByText(this.getRawValue());
                        }
                    }
                }
            }
        };
        return filter;
    },
    //面板功能展开全部节点
   createminusBtn:function(){
       var  minusBtn='';
       var me=this;
       var minusBtn={
       iconCls:'build-button-arrow-out',type:'plus',
       itemId:'plus',tooltip:'全部展开',text:'',
       handler:function(event,toolEl,owner,tool){
            me.expandAll();
        }
       };
       return minusBtn;
   },
   //面板功能收缩全部节点
   createplusBtn:function(){
       var plusBtn='';
       var me=this;
       var plusBtn={
	       text:'',iconCls:'build-button-arrow-in',
	       type:'minus',itemId:'minus',
	       tooltip:'全部收缩',
           handler:function(event,toolEl,owner,tool){
	            me.collapseAll();
	            me.getRootNode().expand();
           }
       };
       return plusBtn;
   },

    filterByText:function(text) {
        this.filterBy(text, this.filterfield);
    },
    filterBy:function(text, by) {
        var me = this;
        this.clearFilter();
        var view = this.getView();
        var tempValue = '';
        nodesAndParents = [];
        this.getRootNode().cascadeBy(function(tree, view) {
            var textValue = text.toLowerCase();
            var byValue = by.toString().toLowerCase().split(',');
            if (isNaN(parseInt(text, 10))) {
                textValue = String(text.toLowerCase()).trim();
            } else {
                textValue = String(text).trim();
            }
            for (var i = 0; i < byValue.length; i++) {
                var currNode = this;
                if (currNode && currNode.data[byValue[i]] && currNode.data[byValue[i]].indexOf(textValue) > -1) {
                    me.expandPath(currNode.getPath());
                    while (currNode.parentNode) {
                        nodesAndParents.push(currNode.id);
                        currNode = currNode.parentNode;
                    }
                }
            }
        }, null, [ this, view ]);
        this.getRootNode().cascadeBy(function(tree, view) {
            var uiNode = view.getNodeByRecord(this);
            if (uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
                Ext.get(uiNode).setDisplayed('none');
            }
        }, null, [ me, view ]);
    },
    clearFilter:function() {
        var me = this;
        var view = this.getView();
        this.getRootNode().cascadeBy(function(tree, view) {
            var uiNode = view.getNodeByRecord(this);
            if (uiNode) {
                Ext.get(uiNode).setDisplayed('table-row');
            }
        }, null, [ this, view ]);
    },
    load:function(id) {
        var me=this;
        id= (id && id != '') ? id : 0;
        //第一种方法load传参数{params:{id:id}}
        me.store.load({params:{id:id}});

        //第二种方法load传参数{node:root}
//        var root = me.getRootNode();
//        root.setId(id);
//        me.store.load({node:root});
    },
    root:{
        text:'',
        objectType:'string',
        leaf:false,
        id:0,
        expanded:false
    },
    initComponent:function() {
        var me = this;
        me.columns = me.createTreeColumns();
        me.addEvents('okClick');
        me.addEvents('cancelClick');
        me.dockedItems = me.ctreatetoolsBar();
        me.store=me.createStore();
        this.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
         if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }
});