//部门树
Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.bumenTreeQuery', {
    extend:'Ext.tree.Panel',
    alias:'widget.bumenTreeQuery',
    title:'部门列表',
    selectId:null,
    hideNodeId:null,
    columnsStr:[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:200,
        sortable:true,
        dataIndex:'text',
        triStateSort:false
    }, {
        text:'主键ID',
        dataIndex:'Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'时间戳',
        dataIndex:'DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'树形结构父级ID',
        dataIndex:'ParentID',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'代码',
        dataIndex:'UseCode',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'部门名称',
        dataIndex:'CName',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'电话',
        dataIndex:'Tel',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    }, {
        text:'联系人',
        dataIndex:'Contact',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:null
    } ],
    getAppInfoServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    getAppListServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
    updateFileServerUrl:getRootPath() + '/' + 'ConstructionService.svc/ReceiveModuleIconService',
    selectServerUrl:getRootPath() + '/' + 'RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree',
    editDataServerUrl:getRootPath() + '/' + 'RBACService.svc/RBAC_UDTO_UpdateHRDeptByField',
    whereFields:'HRDept_Id,HRDept_DataTimeStamp,HRDept_ParentID,HRDept_UseCode,HRDept_CName,HRDept_Tel,HRDept_Contact',
    autoScroll:true,
    filterfield:'text',
    childrenField:'Tree',
    linkageType:false,
    chd:function(node, check) {
        node.set('checked', check);
        if (node.isNode) {
            node.eachChild(function(child) {
                if (node.isLeaf) {} else {
                    chd(child, check);
                }
            });
        }
    },
    setNodefalse:function(n) {
        var me = this;
        n.data.checked = false;
        n.updateInfo({
            checked:false
        });
        var childs = n.childNodes;
        for (var i = 0; i < childs.length; i++) {
            childs[i].data.checked = false;
            childs[i].updateInfo({
                checked:false
            });
            if (childs[i].data.leaf == false) {
                me.setNodefalse(childs[i]);
            }
        }
    },
    nodep:function(node) {
        var me = this;
        var bnode = true;
        Ext.Array.each(node.childNodes, function(v) {
            if (!v.data.checked) {
                bnode = false;
                return;
            }
        });
        return bnode;
    },
    setNode:function(n) {
        var me = this;
        n.expand();
        n.data.checked = true;
        n.updateInfo({
            checked:true
        });
        var childs = n.childNodes;
        for (var i = 0; i < childs.length; i++) {
            childs[i].data.checked = true;
            childs[i].updateInfo({
                checked:true
            });
            if (childs[i].data.leaf == false) {
                me.setNode(childs[i]);
            }
        }
    },
    viewConfig:'',
    getValue:function() {
        var myTree = this;
        var arrTemp = myTree.getSelectionModel().getSelection();
        return arrTemp;
    },
    parentnode:function(node) {
        var me = this;
        if (node.parentNode != null) {
            if (me.nodep(node.parentNode)) {
                node.parentNode.set('checked', true);
            } else {
                node.parentNode.set('checked', false);
            }
            this.parentnode(node.parentNode);
        }
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
    width:292,
    height:280,
    lines:true,
    useArrows:false,
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
    isFunction:true,
    isbuttonBar:false,
    isMinusBtn:true,
    isPlusBtn:true,
    isreFreshBtn:true,
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
    isMenu:true,
    isDelMenuBtn:true,
    createStore:function(where) {
        var me = this;
        var w = '?fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_ParentID,HRDept_UseCode,HRDept_CName,HRDept_Tel,HRDept_Contact';
        var myUrl = me.selectServerUrl + w;
        var store = Ext.create('Ext.data.TreeStore', {
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
                name:'Id',
                type:'auto'
            }, {
                name:'ParentID',
                type:'auto'
            }, {
                name:'hasBeenDeleted',
                type:'auto'
            }, {
                name:'value',
                type:'auto'
            }, {
                name:'Id',
                type:'auto'
            }, {
                name:'DataTimeStamp',
                type:'auto'
            }, {
                name:'ParentID',
                type:'auto'
            }, {
                name:'UseCode',
                type:'auto'
            }, {
                name:'CName',
                type:'auto'
            }, {
                name:'Tel',
                type:'auto'
            }, {
                name:'Contact',
                type:'auto'
            }, {
                name:'Id',
                type:'auto'
            }, {
                name:'DataTimeStamp',
                type:'auto'
            }, {
                name:'ParentID',
                type:'auto'
            }, {
                name:'UseCode',
                type:'auto'
            }, {
                name:'CName',
                type:'auto'
            }, {
                name:'Tel',
                type:'auto'
            }, {
                name:'Contact',
                type:'auto'
            }, {
                name:'Id',
                type:'auto'
            }, {
                name:'DataTimeStamp',
                type:'auto'
            }, {
                name:'ParentID',
                type:'auto'
            }, {
                name:'UseCode',
                type:'auto'
            }, {
                name:'CName',
                type:'auto'
            }, {
                name:'Tel',
                type:'auto'
            }, {
                name:'Contact',
                type:'auto'
            }, {
                name:'Id',
                type:'auto'
            }, {
                name:'DataTimeStamp',
                type:'auto'
            }, {
                name:'ParentID',
                type:'auto'
            }, {
                name:'UseCode',
                type:'auto'
            }, {
                name:'CName',
                type:'auto'
            }, {
                name:'Tel',
                type:'auto'
            }, {
                name:'Contact',
                type:'auto'
            } ],
            proxy:{
                type:'ajax',
                url:myUrl,
                extractResponseData:function(response) {
                    return me.changeStoreData(response);
                }
            },
            defaultRootProperty:me.childrenField,
            root:{
                text:'所有部门',
                leaf:false,
                ParentID:0,
                Id:0,
                tid:0,
                expanded:true
            },
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
            if (value.Id == me.hideNodeId) {
                return true;
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
    getInfoByIdFormServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:url,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        callback(appInfo);
                    }
                } else {
                    Ext.Msg.alert('提示', '获取应用信息失败！');
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取应用信息请求失败！');
            }
        });
    },
    openAppShowWin:function(title, classCode, node, type) {
        var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight * .98;
        var maxWidth = document.body.clientWidth * .98;
        var win = Ext.create(panel, {
            maxWidth:maxWidth,
            type:type,
            maxHeight:maxHeight,
            autoScroll:true,
            model:true,
            floating:true,
            closable:true,
            draggable:true
        }).show();
        if (win && node && node != undefined && node != null) {
            var id = node.data.Id;
            win.load(id);
            var objectName = win.objectName;
            var ParentID = win.getComponent('' + objectName + '_ParentID');
            if (ParentID) {
                var parentNode = node.parentNode;
                var value = '0';
                var text = '';
                if (parentNode) {
                    var parentID = parentNode.data.Id;
                    if (parentID == '' || parentID == null) {
                        value = 0;
                        text = parentNode.data.text;
                    } else {
                        value = parentID;
                        text = parentNode.data.text;
                    }
                } else {
                    value == '0';
                    text = parentNode.data.text;
                }
                var arrTemp = [ [ value, text ] ];
                ParentID.store = Ext.create('Ext.data.SimpleStore', {
                    fields:[ 'value', 'text' ],
                    data:arrTemp,
                    autoLoad:true
                });
                ParentID.setValue(value);
            }
        }
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
    createContextmenu:function() {
        var me = this;
        var menuItems = [];
        if (me.isMenu == true) {
            if (me.isDelMenuBtn == true) {
                menuItems.push(me.createContextmenuDeletebtn());
            }
        } else {
            menuItems = [];
        }
        return menuItems;
    },
    createContextmenuDeletebtn:function() {
        var me = this;
        var com = '';
        com = {
            text:'删除',
            iconCls:'delete',
            handler:function(btn, e, optes) {
                me.fireEvent('delClick');
                me.deleteModule();
            }
        };
        return com;
    },
    deleteServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_DelHRDept',
    deleteModuleServer:function(id, record) {
        var me = this;
        var url = me.deleteServerUrl + '?id=' + id;
        Ext.Ajax.request({
            async:false,
            url:url,
            method:'GET',
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    record.set('hasBeenDeleted', 'true');
                    record.commit();
                } else {
                    record.set('hasBeenDeleted', 'false');
                    record.commit();
                    Ext.Msg.alert('提示', '删除信息失败！');
                }
            },
            failure:function(response, options) {
                record.set('hasBeenDeleted', 'false');
                record.commit();
                Ext.Msg.alert('提示', '连接删除服务出错！');
            }
        });
    },
    deleteModule:function() {
        var me = this;
        var records = me.getSelectionModel().getSelection();
        if (records.length > 0) {
            Ext.Msg.confirm('警告', '确定要删除吗？', function(button) {
                if (button == 'yes') {
                    Ext.Array.each(records, function(record) {
                        var id = record.get('Id');
                        if (record.get('me.hasBeenDeleted') != 'true') {
                            me.deleteModuleServer(id, record);
                        }
                    });
                    me.load('');
                    me.getRootNode().expand();
                    me.fireEvent('delafterClick');
                }
            });
        } else {
            Ext.Msg.alert('提示', '请选择需要删除的行记录！');
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
    createtoolsreFreshBtn:function() {
        var me = this;
        var refreshBtn = {
            iconCls:'build-button-refresh',
            type:'refresh',
            itemId:'refresh',
            tooltip:'刷新数据',
            text:'',
            handler:function(event, toolEl, owner, tool) {
                var treeToolbar = me.getComponent('treeToolbar');
                if (treeToolbar == undefined || treeToolbar == '') {
                    treeToolbar = me.getComponent('treeToolbarTwo');
                }
                if (treeToolbar && treeToolbar != undefined) {
                    var refresh = treeToolbar.getComponent('refresh');
                    if (refresh && refresh != undefined) {
                        refresh.disabled = true;
                    }
                }
                me.load('');
            }
        };
        return refreshBtn;
    },
    createtoolsPlusBtn:function() {
        var me = this;
        var plusBtn = {
            text:'',
            iconCls:'build-button-arrow-in',
            type:'minus',
            itemId:'minus',
            tooltip:'全部收缩',
            handler:function(event, toolEl, owner, tool) {
                me.collapseAll();
                me.getRootNode().expand();
            }
        };
        return plusBtn;
    },
    createtoolsMinusBtn:function() {
        var me = this;
        var minusBtn = {
            iconCls:'build-button-arrow-out',
            type:'plus',
            itemId:'plus',
            tooltip:'全部展开',
            text:'',
            handler:function(event, toolEl, owner, tool) {
                me.expandAll();
            }
        };
        return minusBtn;
    },
    initComponent:function() {
        var me = this;
        me.columns = me.createTreeColumns();
        me.addEvents('okClick');
        me.addEvents('cancelClick');
        me.dockedItems = me.ctreatetoolsBar();
        me.addEvents('delClick');
        me.addEvents('delafterClick');
        me.listeners = me.listeners || [];
        me.listeners.checkchange = function(node, checked) {
            var me = this;
            if (me.linkageType == false) {
                if (checked) {
                    node.expand();
                    node.eachChild(function(child) {
                        child.data.checked = true;
                        child.updateInfo({
                            checked:true
                        });
                        if (child.data.leaf == false) {
                            me.setNode(child);
                        }
                        me.chd(child, true);
                    });
                } else {
                    node.expand();
                    node.eachChild(function(child) {
                        child.data.checked = false;
                        child.updateInfo({
                            checked:false
                        });
                        if (child.data.leaf == false) {
                            me.setNodefalse(child);
                        }
                        me.chd(child, false);
                    });
                }
                me.parentnode(node);
            } else {
                if (node.data.leaf == false) {
                    if (checked) {
                        node.expand();
                        node.eachChild(function(n) {
                            n.data.checked = true;
                            n.updateInfo({
                                checked:true
                            });
                            if (n.data.leaf == false) {
                                me.setNode(n);
                            }
                        });
                    } else {
                        node.expand();
                        node.eachChild(function(n) {
                            n.data.checked = false;
                            n.updateInfo({
                                checked:false
                            });
                            if (n.data.leaf == false) {
                                me.setNodefalse(n);
                            }
                        });
                    }
                } else {
                    node.parentNode.data.checked = false;
                    node.parentNode.updateInfo({
                        checked:false
                    });
                }
            }
            me.fireEvent('OnChanged');
        };
        me.listeners.contextmenu = {
            element:'el',
            fn:function(e, t, eOpts) {
                e.preventDefault();
                e.stopEvent();
                new Ext.menu.Menu({
                    items:me.createContextmenu()
                }).showAt(e.getXY());
            }
        };
        me.store = me.createStore();
        me.load = function(whereStr) {
            var w = '?fields=' + me.whereFields;
            var myUrl = me.selectServerUrl + w;
            me.store.proxy.url = myUrl;
            me.store.load();
        };
        this.callParent(arguments);
    }
});