/***
 * 部门管理--定制部门相关员工查询选择添加
 */
Ext.define('yuangongCheckApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.yuangongCheckApp',
    title:'员工选择',
    width:631,
    appPanel:null,//外部打开当前表单时传入的应用组件,比如可能是列表或者树
    bmxgyghql:'',//外部传入HQL
    height:455,
    //员工所属部门Id(外部传入)
    hRDeptId:'',
    //员工所属部门时间戳(外部传入)
    hRDeptDataTimeStamp:'',
    layout:{
        type:'border',
        regionWeights:{
            north:4,
            south:3
        }
    },
    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    appInfos:[ {
        width:629,
        height:121,
        appId:'4894446352234370339',
        header:true,
        itemId:'yuangongQuery',
        title:'员工查询',
        region:'north',
        split:true,
        collapsible:true,
        collapsed:false,
        border:true
    }, {
        y:393,
        width:629,
        height:40,
        appId:'5764169395081335526',
        header:false,
        itemId:'querenguanbiForm',
        title:'',
        region:'south',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true
    }, {
        y:126,
        width:629,
        height:267,
        appId:'4717838825572700705',
        header:true,
        itemId:'yuangongListCheck',
        title:'选择员工',
        region:'center',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true
    } ],
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.createItems();
        me.on({
        close:function( panel ,eOpts ){
            if(me.appPanel&&me.appPanel!=null){
	            //更新部门相关员工列表数据
	            me.appPanel.load(me.bmxgyghql);
             }
        }
        });
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
    initLink:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        var querenForm = me.getComponent('querenguanbiForm');
        var ygList=me.getComponent('yuangongListCheck');
        ygList.on({
            afterOpenShowWin:function(formPanel){
                //还原部门信息
                var record = formPanel.selectionRecord;//构建列表时传入
                var records=record;
                if(record==null){
                     records=ygList.getSelectionModel().getSelection();
                     record=records[0];
                }
                if(record&&record!=null){ 
                var value=record.get('HREmployee_HRDept_Id');
                var text=record.get('HREmployee_HRDept_CName');
                var hrDeptId=formPanel.getComponent('HREmployee_HRDept_Id');
                if(hrDeptId.xtype=='combobox'){
                        var arrTemp=[[value,text]];
                        hrDeptId.store=new Ext.data.SimpleStore({ 
                            fields:['value','text'], 
                            data:arrTemp 
                            ,autoLoad:true
                        });
                        hrDeptId.setValue(value);
                    }
              }else{
                    Ext.Msg.alert('提示','请选择一行记录！');
                }
            }
        });
        var ygQuery = me.getComponent('yuangongQuery');
        if (me.comNum == length) {
            ygQuery.on({
                selectClick:function(but) {
                    var com = ygQuery;
                    var where = com.getValue();
                    ygList.load(where);
                }
            });
            var btnOK = querenForm.getComponent('btnOK');
            var btnClose = querenForm.getComponent('btnClose');
            btnClose.on({
                click:function(com, e, opets) {
                    if(me.appPanel&&me.appPanel!=null){
                        //更新部门相关员工列表数据
                        me.appPanel.load(me.bmxgyghql);
                    }
                    me.close();
                }
            });
            //确定关闭表单应用的确实按钮时间
            
             btnOK.on({
                click:function(com, e, opets) {
                    var callback =null;
					var url=''+getRootPath()+'/RBACService.svc/RBAC_UDTO_AddHRDeptEmp';
					var hRDeptId =me.hRDeptId;//员工所属部门是传入进来的
					var hRDeptDataTimeStamp =me.hRDeptDataTimeStamp;//员工所属部门时间戳是传入进来的
					var leftArr=[];
					if(hRDeptId==''||hRDeptId==null||hRDeptId==undefined){
					    Ext.Msg.alert('提示','没有获取到员工的所属部门ID信息！');
					}
					if(hRDeptDataTimeStamp&&hRDeptDataTimeStamp!=undefined){
					        leftArr=hRDeptDataTimeStamp.split(',');
					}
					var items = ygList.getSelectionModel().getSelection();
					if(items.length>0){
					Ext.Array.each(items,function(record){
					    
					    var HREmployeeId = record.get('HREmployee_Id');
					    var HREmployeeDataTimeStamp = record.get('HREmployee_DataTimeStamp');
					    var rightArr=[];
					    if(HREmployeeDataTimeStamp&&HREmployeeDataTimeStamp!=undefined){
					            rightArr=HREmployeeDataTimeStamp.split(',');
					    } 
					    var HRDept='{Id:'+hRDeptId+','+'DataTimeStamp:['+leftArr+']}';
					    var HREmployee='{Id:'+HREmployeeId+','+'DataTimeStamp:['+rightArr+']}';
					    var  newAdd= '{'+
					            'LabID:0,'+
					            'Id:-1,'+
					            'HRDept:'+HRDept+','+
					            'HREmployee:'+HREmployee+''+
					            ',DataTimeStamp:['+rightArr+']'+
					        '}';
					    var obj={'entity':Ext.decode(newAdd)};
					    var params = Ext.JSON.encode(obj);
						//util-POST方式与后台交互
						postToServer(url,params,callback);
					});
					}
                }
            });
            if (Ext.typeOf(me.callback) == 'function') {
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
            timeout:2e3,
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