/***
 * 模块管理---应用操作树
 * 　　功能要求：
 *　   1：页面显示时，获取应用和操作的树的关系，并显示树
 *　　　　应用显示名称，只显示有操作的应用，没有操作的应用不用显示
 *　　    操作显示名称和内部代码
 *　　 2：应用不能勾选，操作可以勾选
 *　　 3：还原已经勾选的操作
 *　　 4：确认选择 直接保存,保存完后,load外部调用的组件
 * 公开外部属性,方法和事件
 * arrChecked:还原已经勾选的应用操作,外部传入的数组,封装格式[{tid:值,内部编码:内部编码值},{tid:值,内部编码:内部编码值}]
 * treeDataConfig:0为过滤没有操作的应用,其他值时不过滤
 * externalApp:外部调用的应用组件,如表单或者列表
 * externalAppHQL:外部调用的应用组件的更新数据的HQL串
 * externalModulId:外部调用的应用组件传入的模块Id,以供保存时调用作父节点
 * 方法
 * getnodesChecked():确认选择--获取需要保存选中的子节点
 * defaultChecked(arrChecked):外部传入参数默认勾选子节点
 * 事件
 * cancelClick:取消按钮事件
 * okClick:确定按钮事件
 * OnChanged:树的change选择事件
 * 
 * 外部调用
 * 给externalApp,externalAppHQL赋值,方便保存后更新外部组件数据
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.appComponentsTree', {

    extend:'Ext.tree.Panel',
    alias:'widget.appcomponentstree',
    title:'应用操作树选择',
    selectServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameTree',
    /***
     * 模块操作
     * @type 
     */
    saveServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_AddRBACModuleOper' ,
    autoScroll:true,
    hideNodeId:null,
    externalApp:null,//外部调用的应用组件,如表单或者列表
    externalAppHQL:'',//外部调用的应用组件的更新数据的HQL串
    externalModulId:'',//外部调用的应用组件传入的模块Id,以供保存时调用作父节点
    /**
     * 外面传入的应用组件ID
     */
    appId:-1,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    //0为过滤没有操作的应用,其他值时不过滤
    treeDataConfig:0,

    //还原已经勾选的应用操作,外部传入的数组,封装格式[{Id:值},{Id:值}]
    arrChecked:[],
    width:356,
    height:380,
    lines:false,
    isTure:false,
    useArrows:true,
    rootVisible:true,
    checked:true,
    
    filterfield:'text',
    childrenField:'Tree',
    /***
     * 
     */
    chd:function(node, check) {
        if(node.data.leaf == false){//父节点不选择
            node.set('checked', false);
        }else{
            node.set('checked', true);
        }
        if (node.isNode) {
            node.eachChild(function(child) {
                if (node.isLeaf) {} else {
                    chd(child, check);
                }
            });
        }
    },
    /***
     * 全否
     */
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
    /***
     * 全选
     */
    setNode:function(n) {
        var me = this;
        n.expand();
        if(n.data.leaf == false){//父节点不选择
            n.data.checked = false;
            n.updateInfo({
                checked:false
            });
        }else{
            n.data.checked = true;
            n.updateInfo({
                checked:true
            });
        }
        
        var childs = n.childNodes;
        for (var i = 0; i < childs.length; i++) {
            childs[i].data.checked = true;
            childs[i].updateInfo({
                checked:true
            });
            if (childs[i].data.leaf == false) {//父节点
                me.setNode(childs[i]);
            }
        }
    },
    viewConfig:{
        plugins:{
            ptype:'treeviewdragdrop',
            allowLeafInserts:true
        },
        listeners:{
            beforedrop:function(node, data, overModel, dropPosition, dropFunction, eOpts) {}
        }
    },
    /***
     * 父节点不选择
     * @param {} node
     */
    parentnode:function(node) {
        var me = this;
        if (node.parentNode != null) {
            if (me.nodep(node.parentNode)) {
                node.parentNode.set('checked', false);
            } else {
                node.parentNode.set('checked', false);
            }
            this.parentnode(node.parentNode);
        }
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
    nodep:function(node) {
        var me = this;
        var bnode = true;
        var num=0;
        Ext.Array.each(node.childNodes, function(v) {
            if (!v.data.checked) {
                bnode = false;
                return;
            }
        });
        return bnode;
    },
    /***
     * 创建stroe
     * @param {} where
     * @return {}
     */
    createStore:function(where) {
        var me = this;
        
        var w = '?isPlanish=true&where=';
        if(me.treeDataConfig==0){
            w=w+'treeDataConfig=0';
        }else{//不过滤
            w=w+'treeDataConfig=1';
        }
        //应用的Id
        if(me.appId!=-1){
            w=w+' and id='+me.appId;
        }
        var myUrl = me.selectServerUrl + w;
        var store = Ext.create('Ext.data.TreeStore', {
            fields:[ {
                name:'ParentID',
                type:'auto'
            }, {
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
                name:'value',
                type:'auto'
            }, {
                name:'checked',
                type:'bool'
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
                text:'应用操作树',
                leaf:false,
                Id:0,
                ParentID:0,
                tid:0,
                expanded:true
            },
            listeners:{
                load:function(treeStore, node, records, successful, eOpts) {
                    var treeToolbar = me.getComponent('treeToolbar');
                    if (treeToolbar == undefined || treeToolbar == '') {
                        treeToolbar = tree.getComponent('treeToolbarTwo');
                    }
                    treeToolbar = treeToolbar.getComponent('treeToolbar');
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
    /***
     * 获取数据
     * @param {} response
     * @return {}
     */
    changeStoreData:function(response) {
        var me = this;
        var data = Ext.JSON.decode(response.responseText);
        if(data.ResultDataValue && data.ResultDataValue !=''){ 
            var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
            data.ResultDataValue = ResultDataValue;
            data.Tree = ResultDataValue.Tree; 
        }else{
            data.Tree=[];
        }
        data[me.childrenField] = data.Tree;
        
        var changeNode = function(node){
            if(node.tid == me.hideNodeId){//需要剔除的节点
                return true;
            }
            //时间处理
            //node['DataAddTime'] = getMillisecondsFromStr(node['DataAddTime']);
            //node['DataUpdateTime'] = getMillisecondsFromStr(node['DataUpdateTime']);
            //图片地址处理
            if(node['icon'] && node['icon'] != ''){
                node['icon'] = getIconRootPathBySize(16) + '/' + node['icon'];
            }
            
            var children = node[me.childrenField];
            if(children){
                changeChildren(children);
            }
            return false
        };
        
        var changeChildren = function(children){
            for(var i=0;i<children.length;i++){
                var bo = changeNode(children[i]);
                if(bo){
                    children.splice(i,1);
                    i--;
                }
            }
        };
        var children = data[me.childrenField];
        changeChildren(children);
        
        response.responseText = Ext.JSON.encode(data);
        return response;
    },

    getValue:function() {
        var myTree = this;
        var arrTemp = [];
        arrTemp = myTree.getSelectionModel().getSelection();
        return arrTemp;
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
        if(me.arrChecked!=null){
            //外部还原已经勾选的操作
            me.defaultChecked();
        }
    },
    initComponent:function() {
        var me = this;
        me.dockedItems = [ {
            xtype:'toolbar',
            itemId:'treeToolbar',
            dock:'top',
            items:[ {
                xtype:'toolbar',
                border:0,
                dock:'top',
                items:[ {
                    type:'filter',
                    text:'按过滤',
                    iconCls:'build-button-refresh',
                    xtype:'textfield',
                    fieldLabel:'检索过滤',
                    emptyText :'请输入应用或者操作名称',
                    labelAlign:'right',
                    labelWidth:60,
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
                } ]
            },{
                xtype:'toolbar',
                itemId:'treeToolbarTwo',
                border:0,
                dock:'top',
                items:[ {
                    type:'refresh',
                    itemId:'refresh',
                    text:'',
                    iconCls:'build-button-refresh',
                    tooltip:'刷新数据',
                    handler:function(but, e) {
                        var treeToolbar = me.getComponent('treeToolbar');
                        if (treeToolbar == undefined || treeToolbar == null) {
                            treeToolbar = me.getComponent('treeToolbarTwo');
                        }
                        treeToolbar = treeToolbar.getComponent('treeToolbar');
                        if (treeToolbar && treeToolbar != undefined) {
                            var refresh = treeToolbar.getComponent('refresh');
                            if (refresh && refresh != undefined) {
                                refresh.disabled = true;
                            }
                        }
                        me.load();
                    }
                }, {
                    type:'minus',
                    itemId:'minus',
                    text:'',
                    iconCls:'build-button-arrow-in',
                    tooltip:'收缩数据',
                    handler:function(but, e) {
                        me.collapseAll();
                        me.getRootNode().expand();
                    }
                }, {
                    type:'plus',
                    itemId:'plus',
                    text:'',
                    iconCls:'build-button-arrow-out',
                    tooltip:'展开数据',
                    handler:function(but, e) {
                        me.expandAll();
                    }
                } ]
            } ]
        },{
            xtype:'toolbar',
            itemId:'treeToolbarOK',
            border:0,
            dock:'bottom',
            items:[ {
                type:'confirm',
                text:'确定选择',
                iconCls:'build-button-save',
                tooltip:'确定选择',
                handler:function(but, e) {
                    me.fireEvent('okClick');
//                   var callback =null;
//                   var arr=me.getnodesChecked();
//                   
//                   //批量增加(需要保存服务)
//
//                   var moduleId=me.externalModulId;//模块Id
//                   //测试用
//                   //moduleId='4810495935196084053';
//                   
//                   var RBACModule='{Id:'+moduleId+''+'}';
//                   
//                   if(arr.length>0){
//                    //列表中显示被勾选中的对象
//                    Ext.Array.each(arr,function(record){
//                        var bTDAppComponentsOperate='{Id:'+record.tid+''+'}';
//                        var entity= '{'+
//                            'LabID:0,'+
//                            'Id:-1,'+
//                            'InvisibleOrDisable:1,'+
//                            'RBACModule:'+RBACModule+','+
//                            'BTDAppComponentsOperate:'+bTDAppComponentsOperate+
//                             '}';
//                        var obj={'entity':Ext.decode(entity)};
//                        postToServer(me.saveServerUrl,obj,callback);
//                    });
//                    
//                   }
//                   
//                   //更新外部组件数据
//                   if(me.externalApp&&(me.externalApp!=undefined||me.externalApp!=null)){
//                     var w='';
//                     if(me.externalAppHQL!=''&&me.externalAppHQL!=undefined){
//                        w=''+me.externalAppHQL;
//                     }else{
//                        w='';
//                     }
//                     me.externalApp.load(w);
//                   }
                }
            }, {
                type:'cancel',
                text:'取消',
                iconCls:'build-button-delete',
                tooltip:'取消',
                handler:function(but, e) {
                    me.fireEvent('cancelClick');
                }
            } ]
            } ];
        me.listeners = me.listeners || [];
        /***
         * 勾选项改变后
         */
        me.listeners.checkchange = function(node, checked) {
            var me = this;
            if (node.data.leaf == false) {//父节点不勾选
                node.data.checked = false;
                node.updateInfo({
                checked:false
                });
            }   
            if (me.isTure == false) {
                if (checked) {
                    node.expand();
                    node.eachChild(function(child) {
                        child.data.checked = true;
                        child.updateInfo({
                            checked:true
                        });
                        if (child.data.leaf == false) {//父节点
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
            }
            me.fireEvent('OnChanged');
        };
        me.listeners.contextmenu = {
            element:'el',
            fn:function(e, t, eOpts) {
                e.preventDefault();
                e.stopEvent();
                new Ext.menu.Menu({
                    items:[ {} ]
                }).showAt(e.getXY());
            }
        };

        me.addEvents('cancelClick');
        me.addEvents('okClick');
        me.addEvents('OnChanged');
        me.store = me.createStore();
        me.load = function(whereStr) {
        var w = '?isPlanish=true&where=';
        if(me.treeDataConfig==0){
            w=w+'treeDataConfig=0';
        }else{//不过滤
            w=w+'treeDataConfig=1';
        }
        //应用的Id
        if(me.appId!=-1){
            w=w+' and id='+me.appId;
        }
        var myUrl = me.selectServerUrl + w;
        me.store.proxy.url = myUrl;
        me.store.load();
        };
        this.callParent(arguments);
    },
    /**
     * 外部传入参数默认勾选子节点
     * @private
     */
    defaultChecked:function(arrChecked){
        var me = this;
        if(arrChecked&&arrChecked!=''){
            //me.arrChecked=arrChecked;
        }
        var rootNode = me.getRootNode();
        //展开需要展开的所有父节点
        var expandParentNode = function(value,callback){
            var arr = value.split('_');
            if(arr.length >1){
                var v = arr[0];
                var num = 1;
                var open = function(){
                    if(num < arr.length-1){
                        v = v + '_' + arr[num];
                        var n = rootNode.findChild('tid',v,true);
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
        };
        
        //选中节点
        var checkedNode = function(value){
            var node = rootNode.findChild('tid',value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }
        };
        var nodeArr = [];//没展开的节点数组
        for(var i in arrChecked){
            var value = arrChecked[i].tid;
            var node = rootNode.findChild('tid',value,true);
            if(node != null){//节点存在
                node.set('checked',true);
                treeNodeCheckedChange(node,true);
            }else{//节点不存在
                nodeArr.push(value);
            }
        }
        //勾选展开后的节点
        var openNodes = function(nodes){
            for(var i in nodes){
                checkedNode(nodes[i]);
            }
        };
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
                };
                expandParentNode(nodeArr[num],callback);
            };
            //延时500毫秒处理
            setTimeout(function(){changeNodes(0);},500);
        }
    },
    /***
     * 确认选择--获取需要保存选中的节点
     * 应用不能勾选，操作可以勾选
     */
    getnodesChecked:function(){
        var me = this;
        var data = me.getChecked();
        //勾选节点数组
        var dataArray = [];
        //列表中显示被勾选中的对象
        Ext.Array.each(data,function(record){
            if(record.get('leaf')){//是不是只有子节点才保存
                var item= {
                        //text:record.get('text'),
                        //Id:record.get('Id'),//应用的id
                        tid:record.get('tid')//应用操作Id
                        //moduleOperCode:record.get('ParentID')//内部编码
                    };
                dataArray.push(item);
            }
        });
        return dataArray;
    }

});