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
    selectServerUrl:getRootPath() + '/RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID',
    /***
     * 模块操作id
     * @type 
     */
    moduleOperId:0,
    whereFields:'',
    autoScroll:true,
    filterfield:'text',
    childrenField:'Tree',
    linkageType:false,
    height:495,
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
    autoScroll:true,
    useArrows:true,
    isTure:false,
    rootVisible:false,
    checked:false,
    filterBar:'top',
    functionBar:'top',
    positionBar:'top',
    isShowAction:false,
    isShowCeneral:true,
    isShowDeleteAlag:false,
    isFiltration:true,
    isFunction:false,
    isbuttonBar:false,
    isMinusBtn:false,
    isPlusBtn:false,
    isreFreshBtn:false,
    istoolsDelBtn:false,
    istoolsEditBtn:false,
    istoolsShowBtn:false,
    istoolsAddBtn:false,
    isShowBtn:false,
    isAddBtn:false,
    isEditBtn:false,
    isDelBtn:false,
    isConfirmBtn:false,
    isCancelBtn:false,
    isMenu:false,
    isDelMenuBtn:false,
    fields:[ {
	        name:'text',
	        type:'auto'
	    }, {
	        name:'expanded',
	        type:'auto'
	    }, {
	        name:'leaf',
	        type:'auto'
	    }, {
	        name:'icon',
	        type:'auto'
	    }, {
	        name:'url',
	        type:'auto'
	    }, {
	        name:'tid',
	        type:'auto'
	    }, {
	        name:'id',
	        type:'auto'
	    }, {
            name:'Id',
            type:'auto'
        }, {
	        name:'value',
	        type:'auto'
	    }, {
            name:'objectType',
            type:'auto'
        },{
            name:'pid',
            type:'auto'
        }],
    createStore:function(moduleOperId) {
        var me = this;
        
        var store =null;
        if(moduleOperId&&moduleOperId!=undefined&&moduleOperId!=''){
            me.moduleOperId=moduleOperId;
         }else{
           me.moduleOperId=0;
        }
	        var myUrl = me.selectServerUrl;
	        store = Ext.create('Ext.data.TreeStore', {
	            fields:me.fields,
	            proxy:{
	                type:'ajax',
	                url:myUrl,
	                extractResponseData:function(response) {
	                    return me.changeStoreData(response);
	                }
	            },
	            defaultRootProperty:me.childrenField,
	            root:{
                    text:'',
                    objectType:'string',
                    leaf:false,
                    Id:0,
                    tid:me.moduleOperId
                    ,id:me.moduleOperId
                    ,expanded:true
                },
                //autoLoad:true,
	            listeners:{
	                load:function(treeStore, node, records, successful, eOpts) {
	                    var treeToolbar = me.getComponent('treeToolbar');
	                    if (treeToolbar == undefined || treeToolbar == '') {
	                        treeToolbar = me.getComponent('treeToolbarTwo');
	                    }
	                    if (treeToolbar && treeToolbar != undefined) {
	                        var refresh = treeToolbar.getComponent('refresh');
	                        if (refresh && refresh != undefined) {
	                            refresh.disabled = false;
	                        }
	                    }
	                }
	            }
	        });

        //return store;
        me.store=store;
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
                }
                //图片地址处理
                if(node['icon'] && node['icon'] != ""){
                    node['icon'] = getIconRootPathBySize(16) + "/" + node['icon'];
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
            Ext.Msg.alert('提示','错误信息【<b style="color:red">'+ result.ErrorInfo +'</b>】');
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
    createTreeColumns:function() {
        var me = this;
        var columns=[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:220,
        sortable:true,
        dataIndex:'text',
        triStateSort:false
    }, {
        text:'主键ID',
        dataIndex:'id',
        width:10,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'主键ID',
        dataIndex:'tid',
        width:10,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        xtype:"actioncolumn",
        text:"操作列",
        width:120,
        align:"center",
        itemId:"Action",
        items:[{
            iconCls:"build-button-edit hand",
            tooltip:"修改信息",
            listeners:{
                click:function(grid,rowIndex,colIndex,item,e,record){
                    me.fireEvent('editClick');
                    //点击某个行过滤条件的节点下的某一个角色时,该怎样操作?
                }
            }
        }, {
            iconCls:"build-button-delete hand",
            tooltip:"删除",
            listeners:{
                click:function(grid,rowIndex,colIndex,item,e,record){
                    me.fireEvent('deleteClick');
                    //删除某角色权限下的行过滤条件存在的关系
                }
            }
        } ]
    } ];
        return columns;
    },

    ctreatetoolsBar:function() {
        var me = this;
        var filterBarArr = [];
        var tt = '';
        var filtrationArr = [];
        var buttonBarArr = [];
        if (me.isFiltration == true) {
            filtrationArr.push(me.createtoolsAddFilter());
        }
        if (me.isFunction == true) {
            if (me.isreFreshBtn == true) {
                filterBarArr.push(me.createtoolsreFreshBtn());
            }
            if (me.isPlusBtn == true) {
                filterBarArr.push(me.createtoolsPlusBtn());
            }
            if (me.isMinusBtn == true) {
                filterBarArr.push(me.createtoolsMinusBtn());
            }
            if (me.istoolsAddBtn == true) {
                filterBarArr.push(me.createtoolsAddBtn());
            }
            if (me.istoolsShowBtn == true) {
                filterBarArr.push(me.createtoolsShowBtn());
            }
            if (me.istoolsEditBtn == true) {
                filterBarArr.push(me.createtoolsEditBtn());
            }
            if (me.istoolsDelBtn == true) {
                filterBarArr.push(me.createtoolsDelBtn());
            }
        }
        if (me.isbuttonBar == true) {
            if (me.isConfirmBtn == true) {
                buttonBarArr.push(me.createisConfirmBtn);
            }
            if (me.isCancelBtn == true) {
                buttonBarArr.push(me.createisCancelBtn());
            }
        }
        if (me.filterBar === me.functionBar && me.filterBar === me.positionBar) {
            var tempArr = filtrationArr.concat(buttonBarArr);
            tempArr = tempArr.concat(filterBarArr);
            tt = [ {
                xtype:'toolbar',
                itemId:'treeToolbar',
                dock:me.filterBar,
                items:tempArr
            } ];
        } else {
            var tempArrOne = [];
            var positionOne = '';
            var tempArrTwo = [];
            var positionTwo = '';
            if (me.filterBar === me.positionBar) {
                tempArrOne = buttonBarArr.concat(filterBarArr);
                positionOne = me.filterBar;
                tempArrTwo = filtrationArr;
                positionTwo = me.functionBar;
            } else if (me.filterBar === me.functionBar) {
                tempArrOne = filterBarArr.concat(filtrationArr);
                positionOne = me.filterBar;
                tempArrTwo = buttonBarArr;
                positionTwo = me.positionBar;
            } else if (me.functionBar === me.positionBar) {
                tempArrOne = buttonBarArr.concat(filtrationArr);
                positionOne = me.functionBar;
                tempArrTwo = filterBarArr;
                positionTwo = me.filterBar;
            }
            if (tempArrOne.length > 0 && tempArrTwo.length > 0) {
                tt = [ {
                    xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:positionOne,
                    items:tempArrOne
                }, {
                    xtype:'toolbar',
                    itemId:'treeToolbarTwo',
                    dock:positionTwo,
                    items:tempArrTwo
                } ];
            } else if (tempArrOne.length > 0 && tempArrTwo.length == 0) {
                tt = [ {
                    xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:positionOne,
                    items:tempArrOne
                } ];
            } else if (tempArrOne.length == 0 && tempArrTwo.length > 0) {
                tt = [ {
                    xtype:'toolbar',
                    itemId:'treeToolbar',
                    dock:positionTwo,
                    items:tempArrTwo
                } ];
            } else {
                tt = '';
            }
        }
        if (me.isFiltration == false && me.isFunction == false && me.isbuttonBar == false) {
            tt = '';
        }
        return tt;
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
    load:function(moduleOperId) {
        var me=this;
        if(moduleOperId&&moduleOperId!=undefined&&moduleOperId!=''){
            me.moduleOperId=moduleOperId;
         }else{
           me.moduleOperId=0;
        }
        me.store.proxy.url = me.selectServerUrl;
        me.store.load();
    },
    initComponent:function() {
        var me = this;
        me.columns = me.createTreeColumns();
        me.addEvents('okClick');
        me.addEvents('cancelClick');
        me.dockedItems = me.ctreatetoolsBar();
        
        this.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.createStore(me.moduleOperId);
        me.callParent(arguments);
        
    }
});