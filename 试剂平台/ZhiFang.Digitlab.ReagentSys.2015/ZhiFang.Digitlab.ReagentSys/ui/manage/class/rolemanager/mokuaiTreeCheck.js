
/***
 * 角色管理--角色权限分配
 * 中间模块选择树
 * 1.选中左角色选择列表，已有访问权限模块勾选的还原
 * getAllChecked:获取模块树的所有已选中项
 * setTreeChecked():还原模块树的已选中项
 */
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.Loader.setPath('Ext.manage.rolemanager', getRootPath() + '/ui/manage/class/rolemanager/mokuaiyucaozuoCheck.js');

Ext.ns('Ext.manage');
Ext.define('Ext.manage.rolemanager.mokuaiTreeCheck', {
    extend:'Ext.tree.Panel',
    alias:'widget.mokuaiTreeCheck',
    title:'',
    /***
     * 左角色选择列表选中的角色Id,外部传入
     * @type 
     */
    roleId:'',
    /***
     * 已经选中的节点,从后台获取
     * 以区分哪些节点是新增,哪些节点是删除
     * @type 
     */
    selectdChecked:[],
    /***
     * 是否刚刚打开
     * @type Boolean
     */
    isJustOpen:false,
    /***
     * 当前选中的节点
     * @type 
     */
    selectRecord:null,
    ClassCode:'BTDAppComponents_ClassCode',
    linkageType:true,
    width:260,
    lines:false,
    useArrows:true,
    linkageType:true,//是否真假树
    isTure:true,//是否级联
    rootVisible:false,
    checked:true,
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
    isMenu:false,
    isDelMenuBtn:false,
    hideNodeId:null,
    columnsStr:[ {
        xtype:'treecolumn',
        text:'中文名称',
        width:180,
        sortable:true,
        dataIndex:'text',
        triStateSort:false
    }, {
        text:'描述',
        dataIndex:'Comment',
        width:150,
        sortable:false,
        hidden:false,
        hideable:true,
        align:null
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
        text:'实验室ID',
        dataIndex:'LabID',
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
    } ],
    getAppInfoServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    getAppListServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL',
    updateFileServerUrl:getRootPath() + '/' + 'ConstructionService.svc/ReceiveModuleIconService',
    selectServerUrl:getRootPath() + '/' + 'RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree',
    editDataServerUrl:getRootPath() + '/' + 'RBACService.svc/RBAC_UDTO_UpdateRBACModuleByField',
    whereFields:'RBACModule_Comment,RBACModule_Id,RBACModule_DataTimeStamp,RBACModule_LabID,RBACModule_ParentID',
    autoScroll:true,
    filterfield:'text',
    childrenField:'Tree',
    /***
     * 子节点勾选后,其所有父节点都勾选
     * @param {} node
     */
    setParentnode:function(node) {
        var me = this;
        if(node!=null){
            if(node.data.tid.toString()=="0"){
                node.set("checked", false);
                node.updateInfo({
                    checked:false
                });
            }else{
                if(node.data.tid.toString()!="0"){
                    node.updateInfo({
                        checked:true
                    });
                }
                 //根节点
                if(node.parentNode==null&&node.data.tid.toString()!="0"){
                   node.updateInfo({
                        checked:true
                    });
                }else{
                    var parentNode=node.parentNode;
                    if(parentNode!=null){
                        node.parentNode.set("checked", true);
                        this.setParentnode(parentNode);
                    }
                }
            }
        }
    },
    parentnode:function(node) {
        var me = this;
        if (node.parentNode != null) {
            if (me.nodep(node.parentNode)) {
                node.parentNode.set("checked", true);
            } else {
                node.parentNode.set("checked", false);
            }
            this.parentnode(node.parentNode);
        }
    },
    chd:function(node, check) {
        node.set("checked", check);
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

    internalWhere:'',
    externalWhere:'',
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },

    createStore:function(where) {
        var me = this;
        var w = '?fields=RBACModule_Comment,RBACModule_Id,RBACModule_DataTimeStamp,RBACModule_LabID,RBACModule_ParentID';
        var myUrl = me.selectServerUrl + w;
        var store = Ext.create('Ext.data.TreeStore', {
            fields:['hasBeenDeleted','Comment','LabID','checked','value','text','expanded','leaf','icon','url','tid','Id','ParentID','DataTimeStamp'],
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
            timeout:2000,
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
            fieldLabel:'',
            tooltip:'',
            itemId:'filterText',
            labelAlign:'right',
            labelWidth:6,
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
        me.store = me.createStore();
        me.load = function(whereStr) {
            var w = '?fields=' + me.whereFields;
            var myUrl = me.selectServerUrl + w;
            me.store.proxy.url = myUrl;
            me.store.load();
        };
        me.listeners = me.listeners || [];
        me.listeners.checkchange = function(node, checked) {
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
                     //子节点处理
                    if (checked) {
                        me.setParentnode(node);
                    }else{
                        //当前子节点不勾选时
                        var parentNode=node.parentNode;
                        var checked=false;
                        parentNode.eachChild(function(n) {
                            if(n.data.checked){
                                checked=n.data.checked;
                            }
                        });
                        
                        node.parentNode.data.checked = checked;
                        node.parentNode.updateInfo({
                            checked:checked
                        });
                    }
                }
            }
            //me.fireEvent("OnChanged");
        };
        this.callParent(arguments);
    },
    /**
     * 还原模块树的已选中项
     * @private
     */
    setTreeChecked:function(arrParamsValue){
        var me = this;
        var rootNode = me.getRootNode();
        //先将所有节点设置为不选中状态
        me.setNodefalse(rootNode);
        //展开需要展开的所有父节点
        var expandParentNode = function(value,callback){
            var arr = value.split("_");
            if(arr.length >1){
                var v = arr[0];
                var num = 1;
                var open = function(){
                    if(num < arr.length-1){
                        v = v + "_" + arr[num];
                        var n = rootNode.findChild("Id",v,true);
                        if(!n.isExpanded()){//节点没有展开
                            num++;
                            n.expand(false,open);
                        }else{
                            num++;
                            open();
                        }
                    }else{
                        callback();//完成
                    }
                };
                open();
            }else{
                callback();
            }
        }
        
        //选中节点
        var checkedNode = function(value){
            var node = rootNode.findChild("Id",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                //treeNodeCheckedChange(node,true);
            }
        }
        
        var nodeArr = [];//没展开的节点数组
        for(var i in arrParamsValue){
            var value = arrParamsValue[i].moduleId;
            var node = rootNode.findChild("Id",value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                //treeNodeCheckedChange(node,true);
            }else{//节点不存在
                nodeArr.push(value);
            }
        }
        //勾选展开后的节点
        var openNodes = function(nodes){
            for(var i in nodes){
                checkedNode(nodes[i]);
            }
        }
        if(nodeArr.length == 0){
            me.isJustOpen = false;
        }else{
            var count = 0;
            var changeNodes = function(num){
                var callback =function(){
                    if(num == nodeArr.length-1){
                        if(me.appId != -1 && me.isJustOpen){
                            openNodes(nodeArr);
                            me.isJustOpen = false;
                        }
                    }else{
                        changeNodes(++num);
                    }
                }
                expandParentNode(nodeArr[num],callback);
            }
            //延时500毫秒处理
            setTimeout(function(){changeNodes(0);},500);
        }
    },
    /**
     * 用于获取所有勾选的节点数据，返回records
     * @private
     * @return {}
     */
    getAllChecked:function() {
        var me = this;
        var arrChecked = [];
        arrChecked = me.getChecked();
        return arrChecked;
    },
    /**
     * 用户获取所有改变的数据，返回records；
     * @private
     * @return {}
     */
    getAllChanged:function() {
        var me = this;
        var allChangedValue = [];
        me.store.each(function(record) {
            allChangedValue.push(record);
        });
        return allChangedValue;
    }
});