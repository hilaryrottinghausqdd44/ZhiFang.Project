/***
 * 部门管理--定制组织机构管理
 */
Ext.define('bumenApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.bumenApp',
    title:'组织机构管理',
    width:756,
    height:320,
    /**
     * 是否刚刚开启页面
     * @type Boolean
     */
    isJustOpen:true,
    bmTreeLoadStore:false,//更新树数据的状态标志:false为可以再次更新,true表示已经更新过一次数据
    layout:{
        type:'border',
        regionWeights:{
            east:1
        }
    },
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        x:254,
        width:500,
        height:283,
        appId:'5619730273024616351',
        header:true,
        itemId:'bumenForm',
        title:'部门信息',
        region:'east',
        split:true,
        collapsible:true,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        width:249,
        height:283,
        appId:'5699842133138790529',
        header:true,
        itemId:'bumenTree',
        title:'部门列表',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    } ],
    comNum:0,
    /**
     * 更改中文名称时处理--拼音字头
     * 快捷码和拼音字头自动生成
     * interactionFields:交互字段
     * @private
     */
    setPinYinZiTouValue:function(newValue) {
        var me = this;
        var changePinYinZiTou = function(newValue) {
            var bmForm = me.getComponent('bumenForm');
            var hRDeptPinYinZiTou = bmForm.getComponent('HRDept_PinYinZiTou');
            if (newValue != '' && newValue != null) {
                //替换返回值的空格
                newValue = newValue.replace(/ /g, '');
            }
            if (hRDeptPinYinZiTou) {
                hRDeptPinYinZiTou.setValue(newValue);
            }
            var hRDeptShortcode = bmForm.getComponent('HRDept_Shortcode');
            if (hRDeptShortcode) {
                //快捷码
                hRDeptShortcode.setValue(newValue);
            }
        };
        if (newValue != '') {
            getPinYinZiTouFromServer(newValue, changePinYinZiTou);
        } else {

            var bmForm = me.getComponent('bumenForm');
            var hRDeptPinYinZiTou = bmForm.getComponent('HRDept_PinYinZiTou');
            if (hRDeptPinYinZiTou) {
                hRDeptPinYinZiTou.setValue('');
            }
            var hRDeptShortcode = bmForm.getComponent('HRDept_Shortcode');
            if (hRDeptShortcode) {
                //快捷码
                hRDeptShortcode.setValue('');
            }
        }
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.createItems();   
    },
    createItems:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        for (var i in appInfos) {
            var id = appInfos[i].appId;
            var callback = me.getCallback(appInfos[i]);
            me.getAppInfoFromServer(id, callback);
        }
    },
    getCallback:function(appInfo) {
        var me = this;
        var callback = function(obj) {
            if (obj.success && obj.appInfo != '') {
                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
                var cl = eval(ClassCode);
                var callback2 = function(panel) {
                    me.initLink();
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
            } else {
                appInfo.html = obj.ErrorInfo;
                var panel = Ext.create('Ext.panel.Panel', appInfo);
                me.add(panel);
            }
        };
        return callback;
    },
    getAppInfos:function() {
        var me = this;
        var appInfos = me.appInfos;
        for (var i in appInfos) {
            if (appInfos[i].title == '') {
                delete appInfos[i].title;
            } else if (appInfos[i].title == '_') {
                appInfos[i].title = '';
            }
        }
        return Ext.clone(appInfos);
    },
    setParentIDComValue:function(record, type) {
        var me = this;
        var bmTree = me.getComponent('bumenTree');
        var bmForm = me.getComponent('bumenForm');
        //部门管理部门树和部门信息联动后树节点还原问题bumenApp
        var hRDeptParentID = bmForm.getComponent('HRDept_ParentID');
       if (type == 'add') {
            var hRDeptID = bmForm.getComponent('HRDept_Id');
            if (hRDeptID&&hRDeptID!=undefined) {
                hRDeptID.setValue('-1');
            }
            //部门是否使用，新增时默认为勾选
            bmForm.Id=-1;
            var hRDeptIsUse = bmForm.getComponent('HRDept_IsUse');
            if(hRDeptIsUse&&hRDeptIsUse!=undefined){
                hRDeptIsUse.setValue(true);
            }
                        
         }
        var records = bmTree.getSelectionModel().getSelection();
        var node = null;
        if (records && records.length > 0) {
            node = records[0];
        } else {
            node = null;
        }
        if (node && node != undefined && node != null) {
            if (hRDeptParentID) {
                var parentNode = null;
                if (type == 'add') {
                    parentNode = node;
                } else {
                    if(node.data.Id.toString()=='0'){
                        parentNode = node;//根节点
                    }else{
                       parentNode = node.parentNode;
                    }
                }
                var value = '';
                var text = '';
                if (parentNode && parentNode != null) {
                    var parentID = parentNode.data.Id;
                    if (parentID == '' || parentID == null) {
                        value =parentNode.data.Id;//根节点
                        text = parentNode.data.text;
                    } else {
                        value = parentID;
                        text = parentNode.data.text;
                    }
                } else {
                    value = '0';
                    text = '根节点';
                }
                var arrTemp = [ [ value, text ] ];
                hRDeptParentID.store = Ext.create('Ext.data.SimpleStore', {
                    fields:[ 'value', 'text' ],
                    data:arrTemp,
                    autoLoad:true
                });
                hRDeptParentID.setValue(value);
            }
        }
    },

    initLink:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        if (me.comNum == length) {
            var me = this;
            var appInfos = me.getAppInfos();
            var length = appInfos.length;
            var bmTree = me.getComponent('bumenTree');
            var bmForm = me.getComponent('bumenForm');
            //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
            bmForm.setdataTimeStampValue=false;
            //表单保存操作后更新树
            bmForm.on({
                saveClick:function(but) {
                    if(bmForm.type=='edit'||bmForm.type=='add'){
                        //更新树数据的状态标志
                        if(me.bmTreeLoadStore==false){
                            bmTree.store.load();
                            me.bmTreeLoadStore=true;
                        }else{
                            me.bmTreeLoadStore=true;
                        }
                    }
                }
            });
            var hRDeptCName = bmForm.getComponent('HRDept_CName');
            if (hRDeptCName && hRDeptCName != undefined) {
                hRDeptCName.on({
                    change:function(field, newValue, oldValue, eOpts) {
                        if(newValue!=''&&bmForm.isLoadingComplete==true&&(bmForm.type=='edit'||bmForm.type=='add')){
                            me.setPinYinZiTouValue(newValue);
                        }else{
                            bmForm.isLoadingComplete=true;
                        }
                    }
                });
            }; 
            bmTree.on({
                load:function(treeStore, node,records,successful,eOpts){
                    //更新树数据的状态标志
                    
                    
                    me.bmTreeLoadStore=true;
                },
                addClick:function(grid, rowIndex, colIndex, item, e, record) {
                    var record = null;
                    //更新树数据的状态标志
                    me.bmTreeLoadStore=false;
                    bmForm.type = 'add';
                    bmForm.isAdd();
                    //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
                    bmForm.setdataTimeStampValue=false;
                    me.setParentIDComValue(record, 'add');
                },
                editClick:function(grid, rowIndex, colIndex, item, e, record) {
                    //更新树数据的状态标志
                    me.bmTreeLoadStore=false;
                    var records = bmTree.getSelectionModel().getSelection();
                    var record = null;
                    if (records && records.length > 0) {
                        record = records[0];
                    } else {
                        record = null;
                    }
                    var Id = '';
                    if (record != null) {
                        Id = record.get('Id');
                        bmForm.type = 'edit';
                        bmForm.isEdit(Id);
                        //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
                        bmForm.setdataTimeStampValue=false;
                        me.setParentIDComValue(record, 'edit');
                    }
                    
                },
                showClick:function(view, record) {
                    me.bmTreeLoadStore=false;
                    var records = bmTree.getSelectionModel().getSelection();
                    var record = null;
                    if (records && records.length > 0) {
                        record = records[0];
                    } else {
                        record = null;
                    }
                    var Id = '';
                    if (record != null) {
                        Id = record.get('Id');
                        bmForm.type = 'show';
                        bmForm.isShow(Id);
                        //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
                        bmForm.setdataTimeStampValue=false;
                        me.setParentIDComValue(record, 'show');
                    }
                },
                select:function(view, record) {
                    me.bmTreeLoadStore = false;
                    bmForm.getForm().reset();
                    var records = bmTree.getSelectionModel().getSelection();
                    var record = null;
                    if (records && records.length > 0) {
                        record = records[0];
                    } else {
                        record = null;
                    }
                    bmForm.type = 'edit';
                    var Id = '';
                    if (record != null) {
                        Id = record.get('Id');
                        if(Id!='0'){
                            bmForm.isEdit(Id);
                            //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
                            bmForm.setdataTimeStampValue=false;
                            me.setParentIDComValue(record, 'edit');
                        }else{
                            bmForm.isAdd();
                            //功能按钮弹出选择树后,双击选择节点后是否更新dataTimeStamp的值,默认更新
                            bmForm.setdataTimeStampValue=false;
                            me.setParentIDComValue(record, 'add');
                        }
                    }
                    else if(record == '' || record == null || record == undefined) {
                        Ext.Msg.alert('提示', '请在部门树选择部门后再操作');
                    }
                }
            });
        if(Ext.typeOf(me.callback) == 'function') {
          me.callback(me);
        }
       }
       
    },
    getAppInfoFromServer:function(id, callback) {
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
                        var obj = {
                            success:false,
                            ErrorInfo:'没有获取到应用组件信息!'
                        };
                        if (appInfo != '') {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == 'function') {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息' + result.ErrorInfo + ''
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == 'function') {
                    var obj = {
                        success:false,
                        ErrorInfo:'获取应用组件信息请求失败！'
                    };
                    callback(obj);
                }
            }
        });
    }
});