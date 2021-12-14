/**
 * 树类型选择(定制)表单
 * 【必配参数】
 * 
 * 【可选参数】
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.treeNodeChoose', {
    extend:'Ext.form.Panel',
    alias:'widget.treeNodeChoose',
    title:'树类型选择表单',
    /***
     * 外部传入的选中树节点
     * @type 
     */
    treeNode:null,
    /***
     * 绑定打开的表单应用id
     * @type String
     */
    appComID:'',
    /***
     * 绑定打开的表单应用名称
     * @type String
     */
    appComText:'',
    width:335,
    height:162,
    header:true,
    isSuccessMsg:true,
    getAppInfoServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',

    classCode:'BTDAppComponents_ClassCode',
    autoScroll:true,
    type:'add',
    bodyCls:'',
    layout:'absolute',
    /***
     * 设置外部传入的选中树节点
     * @param {} treeNode
     * @return {}
     */
    settreeNode:function(treeNode) {
        var me = this;
        me.treeNode=treeNode;
        return me.treeNode;
    },
    getInfoByIdFormServer:function(id, callback) {
        var me = this;
        var myUrl = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:myUrl,
            method:'GET',
            timeout:2000,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        result.ResultDataValue = result.ResultDataValue.replace(/\\n/g, '\\\\u000a');
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        if (appInfo == '') {
                            Ext.Msg.alert('提示', '没有获取到应用组件信息！');
                        } else {
                            callback(appInfo);
                        }
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
    onOKCilck:function(com, e, optes) {
        var me = this;
        
        var appComID = me.appComID;
        if (appComID != null || appComID != undefined) {
            var callback = function(appInfo) {
                var title = me.appComText;
                
                var ClassCode = '';
                if (appInfo && appInfo != '') {
                    ClassCode = appInfo[me.classCode];
                }
                if (ClassCode && ClassCode != '') {
                    var treeNode=[];
                    var id='';
                    var chooseValue=me.gettxtTreeNodeValue();
                    var record=me.treeNode;
                    if(chooseValue=='lateral'){//平级部门
                        id=''+record.get('ParentID');//(当前选中节点的父节点Id值为新增节点的父节点
                    }else if(chooseValue=='root'){//顶级部门
                        id=''+0//树的父节点为0
                    }else if(chooseValue=='下级部门'){//下级部门
                        id=''+record.get('Id')//当前选中节点的节点Id值为新增节点的父节点
                    }else{
                        id=''+0//树的父节点为0
                    }
                    //treeNode={Id:id,Text:record.get('text')};
                    treeNode=[[id,record.get('text')]];
                    me.openAppShowWin(title, ClassCode, treeNode);
                } else {
                    Ext.Msg.alert('提示', '没有类代码！');
                }
            };
            me.getInfoByIdFormServer(appComID, callback);
        }
    },
    openAppShowWin:function(title, classCode, treeNode) {
        var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight * .98;
        var maxWidth = document.body.clientWidth * .98;
        var appList = Ext.create(panel, {
            maxWidth:maxWidth,
            maxHeight:maxHeight,
            treeNode:treeNode,//自定义属性,以方便给表单某个定值下拉框绑定数据
            autoScroll:true,
            model:true,
            floating:true,
            closable:true,
            draggable:true
        }).show();
        appList.on({
        });
    },
    GetGroupItems:function(url2, valueField, displayField, groupName, defaultValue) {
        var myUrl = url2;
        if (myUrl == '' || myUrl == null) {
            Ext.Msg.alert('提示', myUrl);
            return null;
        } else {
            myUrl = getRootPath() + '/' + myUrl;
        }
        var localData = [];
        Ext.Ajax.request({
            async:false,
            timeout:6e3,
            url:myUrl,
            method:'GET',
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var ResultDataValue = {
                        count:0,
                        list:[]
                    };
                    if (result['ResultDataValue'] && result['ResultDataValue'] != '') {
                        ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                    }
                    var count = ResultDataValue['count'];
                    var mychecked = false;
                    var arrStr = [];
                    if (defaultValue != '') {
                        arrStr = defaultValue.split(',');
                    }
                    for (var i = 0; i < count; i++) {
                        var DeptID = ResultDataValue.list[i][valueField];
                        var CName = ResultDataValue.list[i][displayField];
                        if (arrStr.length > 0) {
                            mychecked = Ext.Array.contains(arrStr, DeptID);
                        }
                        var tempItem = {
                            checked:mychecked,
                            name:groupName,
                            boxLabel:CName,
                            inputValue:DeptID
                        };
                        localData.push(tempItem);
                    }
                } else {
                    Ext.Msg.alert('提示', '获取信息失败！');
                }
            }
        });
        return localData;
    },
    beforeRender:function() {
        var me = this;
        me.callParent(arguments);
        if (!(me.header === false)) {
            me.updateHeader();
        }
    },
    initComponent:function() {
        var me = this;
        me.addEvents('onOKCilck');
        me.load = function(id) {};
        me.submit = function() {
        
        };

        me.changeStoreData = function(response) {
            var data = Ext.JSON.decode(response.responseText);
            if (data.ResultDataValue && data.ResultDataValue != '') {
                var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                data.ResultDataValue = ResultDataValue;
                data.list = ResultDataValue.list;
            } else {
                data.list = [];
            }
            response.responseText = Ext.JSON.encode(data);
            return response;
        };
        me.items = [ {
            xtype:'radiogroup',
            name:'txtTreeNode',
            fieldLabel:'类型选择',
            labelWidth:100,
            labelStyle:'font-style:normal;',
            width:277,
            height:22,
            columnWidth:100,
            columns:1,
            itemId:'txtTreeNode',
            x:15,
            y:9,
            readOnly:false,
            vertical:true,
            padding:2,
            autoScroll:true,
            hidden:false,
            isdataValue:true,
            items:[ {
                checked:true,
                name:'txtTreeNode',
                inputValue:'lateral',
                boxLabel:'平级部门'
            }, {
                checked:false,
                name:'txtTreeNode',
                inputValue:'root',
                boxLabel:'顶级部门'
            }, {
                checked:false,
                name:'txtTreeNode',
                inputValue:'childNode',
                boxLabel:'下级部门'
            } ],
            tempGroupName:'txtTreeNode',
            sortNum:0,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 0;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:'left'
        }, {
            xtype:'button',
            text:'确定',
            itemId:'btnOK',
            x:125,
            y:92,
            width:65,
            height:22,
            listeners:{
                scope:this,
                click:function(com, e) {
                    me.fireEvent('onOKCilck');
                    me.onOKCilck(com, e);
                }
           }
        } ];
        me.callParent(arguments);
    },
    gettxtTreeNodeValue:function(){
        var me=this;
	    var value = this.getComponent('txtTreeNode').value;
	    return value;
    },
    gettxtTreeNode:function(){
        var me=this;
        var com = this.getComponent('txtTreeNode');
        return com;
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }
});