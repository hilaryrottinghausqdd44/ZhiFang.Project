/***
 * 左模块树
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.moduleTree', {
    extend:'Ext.tree.Panel',
    alias:'widget.moduleTree',
    title:'',
    selectRecord:null,
    selectId:null,
    hideNodeId:null,
    columnsStr:[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:190,
        sortable:true,
        dataIndex:'text',
        triStateSort:false
    } ],

    selectServerUrl:getRootPath() + '/' + 'RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree',
    
    whereFields:'RBACModule_DataTimeStamp',
    autoScroll:true,
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
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    ClassCode:'BTDAppComponents_ClassCode',
    width:205,
    lines:false,
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
    createStore:function(where) {
        var me = this;
        var w = '?fields=RBACModule_Id,RBACModule_DataTimeStamp,RBACModule_ModuleType';
        var myUrl = me.selectServerUrl + w;
        var store = Ext.create('Ext.data.TreeStore', {
            fields:['CName','value','ParentID','Id','tid','url','icon','leaf','DataTimeStamp','text','expanded','ModuleType'],
            proxy:{
                type:'ajax',
                url:myUrl,
                extractResponseData:function(response) {
                    return me.changeStoreData(response);
                }
            },
            defaultRootProperty:me.childrenField,
            root:{
                text:'所有模块',
                leaf:false,
                ParentID:0,
                id:0,
                Id:0,
                tid:0,
                expanded:true
            },
            listeners:{
                load:function(treeStore, node, records, successful, eOpts) {}
            }
        });
        return store;
    },
    changeStoreData:function(response) {
        var me = this;
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = [];
        if (data.ResultDataValue && data.ResultDataValue != '') {
            ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        }
        data[me.childrenField] = ResultDataValue.Tree;
        var changeNode = function(node) {
            var value = node['value'];
            
            if (value && value != null && value != '') {
                var ModuleType=value.ModuleType;//模块类型
                if (value.Id == me.hideNodeId) {
                    return true;
                }
                //当叶子节点时,并且模块类型为非构建,该节点过滤掉(需要不需要过滤2014-06-06)
                if (node.leaf==true&&ModuleType== '1') {
                    return true;
                }
            }
            for (var i in value) {
                node[i] = value[i];
            }
            node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);
            node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);
            if (node['icon'] && node['icon'] != '') {
                node['icon'] = getIconRootPathBySize(16) + '/' + node['icon'];
            }
            var children = node[me.childrenField];
            if (children) {
                changeChildren(children);
            }
            return false;
        };
        var changeChildren = function(children) {
            for (var i = 0; i < children.length; i++) {
                var bo = changeNode(children[i]);
                if (bo) {
                    children.splice(i, 1);
                    i--;
                }
            }
        };
        var children = data[me.childrenField];
        changeChildren(children);
        response.responseText = Ext.JSON.encode(data);
        return response;
    },
    createTreeColumns:function() {
        var me = this;
        return me.columnsStr;
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
    load : function(whereStr) {
        var me = this;
        var w = '?fields=' + me.whereFields;
        var myUrl = me.selectServerUrl + w;
        me.store.proxy.url = myUrl;
        me.store.load();
     },
    initComponent:function() {
        var me = this;
        //me.columns = me.createTreeColumns();
        me.addEvents('okClick');
        me.addEvents('cancelClick');
        me.dockedItems = me.ctreatetoolsBar();
        me.listeners = me.listeners || [];
        me.store = me.createStore();
        
        this.callParent(arguments);
    }
});