/***
 * 部门管理--定制联动维护应用
 * 对构建的部门管理应用(4838889549948904416:bumenApp)的类代码进行定制联动
 * 添加了属性defaultactiveTab:默认选中标签页
 * 修改afterRender
 * 
 */
Ext.define('bumenApp', {
    extend:'Ext.panel.Panel',
    alias:'widget.bumenApp',
    title:'部门管理',
    //默认选中标签页
    defaultactiveTab:0,
    width:813,
    height:337,
    layout:{
        type:'border',
        regionWeights:{
            west:2
        }
    },
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        x:305,
        width:506,
        height:310,
        appId:'5124476146721711511',
        header:false,
        itemId:'bumenTab',
        title:'部门信息',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true
    }, {
        width:300,
        height:310,
        appId:'5699842133138790529',
        header:false,
        itemId:'bumenTree',
        title:'部门列表',
        region:'west',
        split:true,
        collapsible:true,
        collapsed:false,
        border:true
    } ],
    comNum:0,
    /**
     * 更改中文名称时处理--拼音字头
     * 快捷码和拼音字头自动生成
     * interactionFields:交互字段
     * @private
     */
    setPinYinZiTouValue:function(newValue){
        var me = this;
        var changePinYinZiTou = function(newValue){
            var bmTab4=me.getComponent('bumenTab');
            var bmForm = bmTab4.getComponent('bumenForm');
            var hRDeptPinYinZiTou= bmForm.getComponent('HRDept_PinYinZiTou');
            if(newValue!=''&&newValue!=null){
                //替换返回值的空格
                newValue=newValue.replace(/ /g,'');//
            }
            if(hRDeptPinYinZiTou){ 
                hRDeptPinYinZiTou.setValue(newValue);
            }
            var hRDeptShortcode= bmForm.getComponent('HRDept_Shortcode');
            if(hRDeptShortcode){
                //快捷码
                hRDeptShortcode.setValue(newValue);
            }
        };
        if(newValue != ''){
            getPinYinZiTouFromServer(newValue,changePinYinZiTou);
        }else{
            var bmTab4=me.getComponent('bumenTab');
            var bmForm = bmTab4.getComponent('bumenForm');
            var hRDeptPinYinZiTou= bmForm.getComponent('HRDept_PinYinZiTou');
            if(hRDeptPinYinZiTou){
                hRDeptPinYinZiTou.setValue('');
            }
            var hRDeptShortcode= bmForm.getComponent('HRDept_Shortcode');
            if(hRDeptShortcode){
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
    setParentIDComValue:function(record,type){
    var me=this;
    var bmTree = me.getComponent('bumenTree');
    var bmTab2=me.getComponent('bumenTab');
    var bmForm = bmTab2.getComponent('bumenForm');
    //部门管理部门树和部门信息联动后树节点还原问题bumenApp
    var hRDeptParentID=bmForm.getComponent('HRDept_ParentID');
    var DataTimeStampValue = '';
    if(record!=null){
        DataTimeStampValue=record.get('DataTimeStamp');
    }
    var records=bmTree.getSelectionModel().getSelection();
    var node=null;
    if(records&&records.length>0){
        node=records[0];
    }else{
        node=null;
    }
    if(node&&node!=undefined&&node!=null){
        if(hRDeptParentID){
            var parentNode=null;
            if(type=='add'){
                parentNode=node;
                var hRDeptID=bmForm.getComponent('HRDept_ID');
                if(hRDeptID){
                    hRDeptID.value='-1';
                }
            }else{//if(type=='edit')
                parentNode=node.parentNode;
            }
            var value='';
            var text='';
            if(parentNode&&parentNode!=null){
                var parentID=parentNode.data.Id;
                if(parentID==''||parentID==null){
                    value='';
                    text='';
                }else{
                    value=parentID;
                    text=parentNode.data.text;
                }
            }else{
                value=='';
                text='';
            }
            var arrTemp=[[value,text]];
            hRDeptParentID.store=Ext.create('Ext.data.SimpleStore',{  
                fields:['value','text'], 
                data:arrTemp,
                autoLoad:true
            });
            hRDeptParentID.setValue(value);
        }
     }
     //部门管理部门树和部门信息联动后树节点还原问题bumenApp(结束)
    },
    initLink:function() {
        var me = this;
        if(Ext.typeOf(me.callback)=='function'){me.callback(me);}
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        var bmTree = me.getComponent('bumenTree');
        var bmTab2=me.getComponent('bumenTab');
        
        if (me.comNum == length) {
            
            bmTree.on({
                addClick:function(grid,rowIndex,colIndex,item,e,record) {
                    bmTab2.activeTab=0;
                    var record=null;
                    var bmForm = bmTab2.getComponent('bumenForm');
                            bmForm.getForm().reset();
                            bmForm.type='add';
                            me.setParentIDComValue(record,'add');
                },
                editClick:function(grid,rowIndex,colIndex,item,e,record) {
                    bmTab2.activeTab=0;
                    var records=bmTree.getSelectionModel().getSelection();
                    var record=null;
                    if(records&&records.length>0){
                        record=records[0];
                    }else{
                        record=null;
                    }
                    var Id ='';
                    if(record!=null){
                        Id = record.get('Id');
                    }
                    var bmForm = bmTab2.getComponent('bumenForm');
                            bmForm.type='edit';
                            bmForm.load(Id);
                            me.setParentIDComValue(record,'edit');
                },
                showClick:function(view, record) {
                    bmTab2.activeTab=0;
                    var records=bmTree.getSelectionModel().getSelection();
                    var record=null;
                    if(records&&records.length>0){
                        record=records[0];
                    }else{
                        record=null;
                    }
                    var Id ='';
                    if(record!=null){
                        Id = record.get('Id');
                    }
                    var bmForm = bmTab2.getComponent('bumenForm');
                            bmForm.type='show';
                            bmForm.load(Id);
                            me.setParentIDComValue(record,'show');
                },
                select:function(view, record) {
                     //部门的下属部门tab:部门列表只呈现一下级
                    bmTab2.activeTab=1;
                    var Id ='';
                    if(record!=null){
                        Id = record.get('Id');
                    }
                    if(Id==''||Id==null||Id==undefined){
                        Ext.Msg.alert('提示','请在部门树选择部门后再操作');
                    }
                    var bmxsbmhql = 'hrdept.ParentID=' + Id;
                    var bmxsList = bmTab2.getComponent('bumenList');
                    bmxsList.load(bmxsbmhql);
                    
                     //部门直属员工列表tab
                    var bmxshql = 'hremployee.HRDept.Id=' + Id;
                    var bmxsygList = bmTab2.getComponent('bumenyuangongList');
                    bmxsygList.load(bmxshql);

                   //部门相关员工列表(部门的所有员工列表，包括子部门的员工)
                    var bmxgyghql = 'hrdeptemp.HRDept.Id=' + Id;
                    var bmxgygList = bmTab2.getComponent('bumenyuangonggxList');
                    bmxgygList.load(bmxgyghql);
                    var bmForm = bmTab2.getComponent('bumenForm');
                    if(record!=null){
                        Id = record.get('Id');
                        bmForm.type='show';
                        bmForm.load(Id);
                        me.setParentIDComValue(record,'show');
                    }
                }
            });
        }  
         //部门直属员工列表
         var bmxsygList=bmTab2.getComponent('bumenyuangongList');
         //给打开的员工选择应用(yuangongCheckApp)属性赋值
         bmxsygList.on({
            afterOpenAddWin:function(formPanel){
                //取部门Id
                var node2=bmTree.getValue();
                var records=bmTree.getSelectionModel().getSelection();
                var node=null;
                if(records&&records.length>0){
                    node=records[0];
                }else{
                    node=null;
                }
                var hRDeptId=node.data.Id;
                var hRDeptCName=node.data.text;
                var hRDeptDataTimeStamp=''+node.data.DataTimeStamp;
                //给部门员工查询选择添加(yuangongCheckApp)的员工所属部门hRDeptId赋值,hRDeptDataTimeStamp时间戳
                if(hRDeptId==''||hRDeptId==null||hRDeptId==undefined){
                   Ext.Msg.alert('提示',',获取不到部门树的部门Id值信息,请在部门树选择后再操作！');
                }else{
                    formPanel.hRDeptId=hRDeptId;
                }
                if(hRDeptDataTimeStamp==''||hRDeptDataTimeStamp==null||hRDeptDataTimeStamp==undefined){
                   Ext.Msg.alert('提示',',获取不到部门树的时间戳信息,部门树需要配置时间戳列信息！');
                }else{
                    formPanel.hRDeptDataTimeStamp=hRDeptDataTimeStamp;
                }
                
            }
         });
         //部门相关员工列表
         var bmxgygxxList=bmTab2.getComponent('bumenyuangonggxList');
         //给打开的员工选择应用(yuangongCheckApp)属性赋值
         bmxgygxxList.on({
            afterOpenAddWin:function(formPanel){
                //取部门Id
                var node2=bmTree.getValue();
                var records=bmTree.getSelectionModel().getSelection();
                var node=null;
                if(records&&records.length>0){
                    node=records[0];
                }else{
                    node=null;
                }
                var hRDeptId=node.data.Id;
                var hRDeptCName=node.data.text;
                var hRDeptDataTimeStamp=''+node.data.DataTimeStamp;
                //给部门员工查询选择添加(yuangongCheckApp)的员工所属部门hRDeptId赋值,hRDeptDataTimeStamp时间戳
                if(hRDeptId==''||hRDeptId==null||hRDeptId==undefined){
                   Ext.Msg.alert('提示',',获取不到部门树的部门Id值信息,请在部门树选择后再操作！');
                }else{
                    formPanel.hRDeptId=hRDeptId;
                }
                if(hRDeptDataTimeStamp==''||hRDeptDataTimeStamp==null||hRDeptDataTimeStamp==undefined){
                   Ext.Msg.alert('提示',',获取不到部门树的时间戳信息,部门树需要配置时间戳列信息！');
                }else{
                    formPanel.hRDeptDataTimeStamp=hRDeptDataTimeStamp;
                }
            }
         });
         var bmForm5 = bmTab2.getComponent('bumenForm');
         var hRDeptCName= bmForm5.getComponent('HRDept_CName');
         if(hRDeptCName&&hRDeptCName!=undefined){
            hRDeptCName.on({
                change:function(field,newValue,oldValue,eOpts){
                    if(newValue!=null&&newValue!=''){
                        me.setPinYinZiTouValue(newValue);//拼音字头
                    }
               }
            });
         }
          //更新树,但会被调用四次
//        var bmTab4=me.getComponent('bumenTab');
//        var bmForm = bmTab4.getComponent('bumenForm');
//            bmForm.on({
//                saveClick:function(but) {
//                    var _bumenTree = me.getComponent('bumenTree');
//                    setTimeout(function(){_bumenTree.load('');},2000);
//                }
//       });   
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