/***
 * 行过滤条件---预定义可选属性树
 * 　　功能要求：

 * 方法
 * getPredefinedField():确认选择--获取需要保存选中的子节点
 * defaultChecked(arrChecked):外部传入参数默认勾选子节点
 * 事件
 * cancelClick:取消按钮事件
 * okClick:确定按钮事件
 * OnChanged:树的change选择事件
 * 
 * 外部调用
 * 给externalApp,externalAppHQL赋值,方便保存后更新外部组件数据
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.predefinedAttributesTree',{

    extend:'Ext.tree.Panel',
    alias:'widget.predefinedAttributesTree',
    title:'预定义可选属性选择',
    objectPropertyUrl:getRootPath() + '/ConstructionService.svc/CS_BA_GetEntityFrameTree',
    /**
     * 获取数据对象内容时后台接收的参数名称
     * @type String
     */
    objectPropertyParam:'EntityName',
    /***
     * 树的所需的数据对象名,外部传入
     * @type String
     */
    objectName:'',
    moduleOperId:'',
    /***
     * 所需的数据对象中文名
     * @type String
     */
    objectCName:'数据对象',
    /***
     * 模块操作预定义属性更新
     * @type 
     */
    updateServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField' ,
    updateFields:"PredefinedField,Id",
    selectServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true&fields=RBACModuleOper_PredefinedField,RBACModuleOper_Id',
     
    autoScroll:true,

    /**
     * 外面传入的应用组件ID
     */
    appId:-1,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    //还原已经勾选的应用操作,外部传入的数组,封装格式[{Id:值},{Id:值}]
    arrChecked:[],
    width:486,
    height:380,
    lines:false,
    useArrows:true,
    linkageType:true,//是否真假树
    isTure:true,//是否级联
    rootVisible:true,
    checked:true,
    expanded:true,
    filterfield:'text',
    childrenField:'Tree',
    /***
     * 预定义可选属性的模块操作树数据
     * @type 
     */
    arrTreeJson:[],
    /**
     * 外部传入参数默认勾选子节点
     * @private
     */
    defaultChecked:function(){
        var me=this;
        var lists=[],hqlWhere='',url="",PredefinedField=[];
        if(me.moduleOperId!=""){
            hqlWhere='rbacmoduleoper.Id='+me.moduleOperId;
            url=me.selectServerUrl;
            lists=me.getServerLists(url,hqlWhere,false);
            if(lists!=undefined&&lists.length>0){
                var str=lists[0]["RBACModuleOper_PredefinedField"];
                if(str&&str!=null&&str.length>0){
                    //所有Tree替换为children
                    str=str.replace(/Tree/g,"children");
                    PredefinedField=Ext.decode(str);
                    if(Ext.isArray(PredefinedField)){
                        var treeStore = Ext.create('Ext.data.TreeStore', {
                            fields:['id','text','parentId','expanded','leaf','FieldClass','tid','value','InteractionField','ParentCName','ParentEName'],
						    root:PredefinedField[0] 
						});
                        var tree=Ext.create('Ext.tree.Panel', {
						    //renderTo: Ext.getBody(),
						    title: '还原选择树',
						    width: 150,
						    height: 150,
						    store:treeStore
						});
                        var rootNode = tree.getRootNode();
                        
                        me.nodeEachChild(rootNode);

                    }
                }
            }
        }
    },
    /***
     * 遍历还原选择树的所有节点
     * @param {} node
     */
    nodeEachChild:function(node) { 
        var me=this;
        var rootNode=me.getRootNode();
        node.eachChild(function(childNode){  
	        var leaf=childNode.data.leaf;
	        if(leaf==false){//父节点不用勾选(子节点勾选时父节点会同时勾选)
	            me.nodeEachChild(childNode);
	        }else{
	            //根据当前子节点查询匹配到选择树的节点并设置勾选
	            var value=childNode.data.InteractionField;
	            me.nodefindChild(rootNode,value);
	        }
	        
	    });
    },
    /***
     * 遍历选择树
     * @param {} node
     */
    nodefindChild:function(node,value) { 
        var me=this;
        node.eachChild(function(childNode){  
	        var leaf=childNode.data.leaf;
            var InteractionField=childNode.data["InteractionField"];
            if(leaf==true&&InteractionField==value){
                //子节点并且已经匹配上
                childNode.set('checked', true);
                var parentNode=childNode.parentNode;
                if(parentNode!=""){
                    parentNode.set('checked', true);
                }
            }else if(leaf==false){
                //如果当前节点是父节点
                var findChildNode=childNode.findChild('InteractionField',value);
	            if(findChildNode==null){
	                //继续查询
	                me.nodefindChild(childNode,value);
	            }else{
                    findChildNode.set('checked', true);
                    var parentNode=findChildNode.parentNode;
	                if(parentNode!=""){
	                    parentNode.set('checked', true);
	                }
                }
            }
        });
    },
    /***
     * 
     */
    chd:function(node, check) {
        if(node.data.leaf == false){
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
            n.data.checked = true;
            n.updateInfo({
                checked:true
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
                node.parentNode.set('checked', true);
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
    createStore:function() {
        var me = this;
        var store = Ext.create('Ext.data.TreeStore', {
            fields:['checked','id','text','expanded','leaf','FieldClass','tid','value','InteractionField','ParentCName','ParentEName'],
            proxy:{
                type:'ajax',
                url:me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.objectName,
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    if(data.ResultDataValue && data.ResultDataValue != ""){
                        var arr = Ext.JSON.decode(data.ResultDataValue);
                        //过滤时间戳
                        var children =[];
                        var treeCom=me;
                        if(arr){
                            for (var i in arr) {
                                var obj=arr[i];
                                arr[i]['expanded'] = true;
                                arr[i].checked=false;
                                arr[i].ParentCName=treeCom.ParentCName;
                                arr[i].ParentEName=treeCom.ParentEName;
                                var arrTemp=obj['InteractionField'].split('_');
                                if(arrTemp[arrTemp.length-1]!='DataTimeStamp'){
                                    children.push(arr[i]);
                                }
                            }
                        }else{
                            children =arr;
                        }
                        if(treeCom.nodeClassName != ""){
                            data['expanded'] = true;
                            data['Tree'] = children;
                        }
                        else{
                            data['Tree'] = [{
                                text:''+me.objectCName,
                                InteractionField:''+me.objectName,
                                id:0,
                                parentId:0,
                                leaf:false,
                                expanded:true,
                                Tree:children
                            }];
                        }
                    }
                    response.responseText = Ext.JSON.encode(data);
                    return response;
                }
            },
            defaultRootProperty:'Tree',
            root:{
                text:''+me.objectCName,
                InteractionField:''+me.objectName,
                id:0,
                parentId:0,
                leaf:false,
                expanded:true
            },
            autoLoad:false,
            listeners:{}
        });
        return store;
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
        
        if(me.store!=null&&me.moduleOperId!=null&&me.moduleOperId!=""){
            //外部还原已经勾选的操作
           setTimeout(function(){me.defaultChecked()},2800); 
        }
    },
    /**
     * 数据对象内容的值字段
     * @type String
     */
    objectPropertyValueField:'InteractionField',
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
                    emptyText :'请输入名称',
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
                    if(me.moduleOperId&&me.moduleOperId!=""&&me.moduleOperId!=null){
                        var PredefinedField=me.getPredefinedField();
                        
                        if(PredefinedField.length==0){
                            PredefinedField=null;
                        }else if(PredefinedField.length>0){
                            PredefinedField=Ext.JSON.encode(PredefinedField);
                            PredefinedField=PredefinedField.replace(/'/g,"");
                            PredefinedField=PredefinedField.replace(/"/g,"'");
                        }
                        var entity={PredefinedField:PredefinedField,Id:me.moduleOperId};
                        var params = Ext.JSON.encode({
                            entity : entity,
                            fields : me.updateFields
                        });
                        var c = function(text) {
                            me.fireEvent('okClick',but,e,text,me.arrTreeJson);
                        };
                        postToServer(me.updateServerUrl, params, c, false);
                    }
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
        me.listeners.beforeitemexpand=function(node){
            me.nodeClassName = node.data[me.objectPropertyValueField];
        },
        me.listeners.beforeload=function(store){
            if(this.nodeClassName != ""){
                store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.nodeClassName;
            }
        },
        /***
         * 勾选项改变后
         */
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
            me.fireEvent("OnChanged");
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
        
        me.load = function() {
	        me.store.proxy.url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.objectName;
	        me.store.load();
        };
        
        this.callParent(arguments);
    },
    /***
     * 确认选择--获取需要保存选中的节点
     * 拼成预定义属性的数据对象树(EntityFrameTree)的Json字符串
     */
    getPredefinedField:function(){
       var me = this;
        var field = 'InteractionField';//唯一字段,属性名称
        var children = 'Tree';//子节点字段
        
        //需要生成的内容
        var gatNodeInfo = function(node){
            var parentNode=node.parentNode,ParentCName="",ParentEName="";
            if(parentNode&&parentNode!=null){
                ParentEName=parentNode.get('InteractionField');
                ParentCName=parentNode.get('text');
            }
            var info = {
                expanded:true,
                text:node.get('text'),
                leaf:node.get('leaf'),
                ParentCName:ParentCName,
                ParentEName:ParentEName,
                InteractionField:node.get('InteractionField'),
                FieldClass:node.get('FieldClass')
            };
            info[children] = [];
            return info;
        };
        
        var maxLayersNumber = 0;//最大层数
        var layers = {};//层次对象
        
        //找出所有勾选的节点对象列表
        var checkedNodes = me.getChecked();
        //循环处理每个节点
        for(var i in checkedNodes){
            //获取的结果'/Root/节点1',转化为数组:['','Root','节点1'],前面两个字符串去掉
            var path = checkedNodes[i].getPath(field);//获取节点全路径
            path = path.split('/').slice(2);
            var length = path.length;
            
            if(length == 0) continue;
            
            var node = {
                path:path,
                pathLen:length,
                node:checkedNodes[i]
            };
            if(length > maxLayersNumber){
                maxLayersNumber = length;
            }
            
            if(!layers['L'+length]){
                layers['L'+length] = [];
            }
            
            layers['L'+length].push(node);
        }
        
        //结果对象
        var nodes = {};
        nodes[children] = [];
        
        //在结果对象中获取上一级节点
        var getParentNode = function(nodeInfo){
            var node = nodeInfo.node,
                length = nodeInfo.pathLen,
                path = nodeInfo.path;
                
            var pNode = nodes;
            
            for(var i=1;i<length;i++){
                var list = pNode[children];
                for(var j in list){
                    if(list[j][field] == path[i-1]){
                        pNode = list[j];
                        break;
                    }
                }
            }
            
            return pNode;
        };
        
        //整体处理
        for(var i=1;i<=maxLayersNumber;i++){
            var list = layers['L'+i];
            for(var j in list){
                var info  = gatNodeInfo(list[j].node);
                var pNode = getParentNode(list[j]);
                pNode[children].push(info);
            }
        }
        
        //新增的处理
        var arrTreeJson=[];
        var str=Ext.JSON.encode(nodes);
        str=str.replace(/'/g,"");
        str=str.replace(/"/g,"'");
        var nodeStr="{'Tree':[]}";
        if(str!=null&&str.length>0){
            if(str==nodeStr||str==''){
                arrTreeJson=[];
            }else{
	            arrTreeJson=[{ 
	                        Tree:[],expanded: true,text:me.objectCName,leaf: false, 
	                        ParentEName:me.objectName,ParentCName:"",
	                        InteractionField: me.objectName,FieldClass:me.objectName
	                    }];
	            arrTreeJson[0].Tree=nodes["Tree"];
            }
        }
        return arrTreeJson;
    
    },

	/***
	 * 获取数据集
	 */
	 getServerLists: function(url,hqlWhere,async){
	    var arrLists=[];
	    var myUrl="";
	    if(hqlWhere&&hqlWhere!=null){
	        myUrl=url+'&where='+encodeString(hqlWhere);;
	    }else{
	        myUrl=url;
	    }
	    //查询数据过滤条件行记录
	    Ext.Ajax.defaultPostHeader = 'application/json';
	    Ext.Ajax.request({
	        async:async,//非异步
	        url:myUrl,
	        method:'GET',
	        success:function(response,opts){
	            var data = Ext.JSON.decode(response.responseText);
	            var success = (data.success + "" == "true" ? true : false);
	            if(!success){
	                alert(data.ErrorInfo);
	            }
	            if(success){
	                if(data.ResultDataValue && data.ResultDataValue != ''){
	                    data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,"");
	                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
	                    arrLists= ResultDataValue.list;
	                }else{
	                    arrLists= [];
	                }
	            }
	        },
	        failure : function(response,options){
	             arrLists=[];
	        }
	    });
	    return arrLists;
	}
});