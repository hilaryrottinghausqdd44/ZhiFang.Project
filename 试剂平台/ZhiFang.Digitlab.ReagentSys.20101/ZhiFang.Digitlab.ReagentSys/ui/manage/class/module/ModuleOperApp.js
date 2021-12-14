
/***
 * 模块管理--模块操作列表管理
 * 
 * 外部传入的参数
 * appId:应用ID值
 * moduleId:模块Id值
 * moduleCName:模块名称值
 * moduleDataTimeStamp:模块时间
 * 公开事件
 * addClick:新增按钮事件
 * delClick:删除事件
 * savechangesClick:修改保存后事件
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.module.ModuleOperApp',{
    extend:'Ext.grid.Panel',
    alias: 'widget.moduleoperapp',
    /***
     * 查询应用操作列表的HQLwhere串
     * @type 
     */
    btdAppComponentsHQL:[],
    autoScroll :true,
    itemId:'moduleOper',
    internalWhere:'',
    externalWhere:'',
    border:false,
    objectName:'RBACModuleOper',
    defaultWhere:'',
    /**
     * 外面传入的模块应用Id
     * 在openAppComponentsTreeWin里有测试数据
     */
    appId:-1,
    /**
     * 外面传入模块应用时间戳
     * 在openAppComponentsTreeWin里有测试数据
     */
    appDataTimeStamp:'',
    queryFields:
    'RBACModuleOper_RowFilterBase,RBACModuleOper_Id,RBACModuleOper_UseCode,RBACModuleOper_CName,RBACModuleOper_Comment,RBACModuleOper_IsUse,' +
    'RBACModuleOper_DispOrder,RBACModuleOper_DefaultChecked,RBACModuleOper_RBACModule_CName,' +
    'RBACModuleOper_RBACModule_IsUse,RBACModuleOper_RBACModule_DispOrder,RBACModuleOper_RBACModule_Id,' +
    'RBACModuleOper_RBACModule_DataTimeStamp,RBACModuleOper_DataTimeStamp,RBACModuleOper_LabID,' +
    'RBACModuleOper_BTDAppComponentsOperate_Id,RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp',
    
    /***
     * 接收外部的传入的模块Id值
     * @type String
     */
    moduleId:'-1',
    /***
     * 接收外部的传入的模块名称值
     * @type String
     */
    moduleCName:'',
    /***
     * 接收外部的传入的模块时间
     * @type String
     */
    moduleDataTimeStamp:'',
    /**
     * 需要过滤的数据字段
     * @type 
     */
    filterFields:['DataTimeStamp','DataAddTime','LabID','dataStatus'],
    /***
     * 是否打开应用操作树
     * @type Boolean
     */
    isOpenAppComponentsTree:true,
    /***
     * 应用操作树返回的选中节点数组
     * @type 
     */
    arrnodesChecked:'',
    columns:[ {
            text:'模块操作主键ID',
            dataIndex:'RBACModuleOper_Id',
            width:10,
            sortable:false,
            hidden:true,
            align:'left'
        },{
            text:'模块操作代码',
            dataIndex:'RBACModuleOper_UseCode',
            width:120,
            sortable:false,
            hidden:false,
            hideable:true,
            editor:false,
            align:'left'
        }, {
            text:'模块操作名称',
            dataIndex:'RBACModuleOper_CName',
            width:212,
            sortable:false,
            hidden:false,
            editor:{
                allowBlank:true
            },
            align:'left'
        }, {
            text:'模块操作描述',
            dataIndex:'RBACModuleOper_Comment',
            width:150,
            hidden:false,
            editor:{
                allowBlank:true
            },
            align:'left'
        }, 
        {text:'是否使用',dataIndex:'RBACModuleOper_IsUse',width:80,align:'left',
            sortable:false,
            xtype:'checkcolumn',
            editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },
            renderer:function(value,metaData,record){

                var cssPrefix = Ext.baseCSSPrefix,
                    cls = [cssPrefix + 'grid-checkheader'];
                if(value=='true'||value==true){
	                cls.push(cssPrefix + 'grid-checkheader-checked');
	                value=true;
                }else{
                    value=false;
                }
                return '<div class="' + cls.join(' ') + '" style="background-position:0px;text-indent:14px;">&#160;' + '</div>';
            },
            listeners:{
             }
         },
        {
            text:'显示次序',
            dataIndex:'RBACModuleOper_DispOrder',
            width:69,
            sortable:false,
            hidden:false,
            align:'left',
            xtype:'numbercolumn',
            format:'第0行',
            editor:{
                xtype:'numberfield',
                allowBlank:false,
                minValue:0,
                maxValue:9999
            }
        }, {
            text:'数据对象',
            dataIndex:'RBACModuleOper_RowFilterBase',
            width:80,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'默认状态',
            dataIndex:'RBACModuleOper_DefaultChecked',
            width:10,
            sortable:false,
            hidden:true,
            align:'left'
        }, {
            text:'模块名称',
            dataIndex:'RBACModuleOper_RBACModule_CName',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'模块主键ID',
            dataIndex:'RBACModuleOper_RBACModule_Id',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'模块时间戳',
            dataIndex:'RBACModuleOper_RBACModule_DataTimeStamp',
            width:10,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },  {
            text:'模块操作时间戳',
            dataIndex:'RBACModuleOper_DataTimeStamp',
            width:10,
            sortable:false,
            hidden:true,
            align:'left'
        }, {
            text:'模块操作实验室ID',
            dataIndex:'RBACModuleOper_LabID',
            width:19,
            sortable:false,
            hidden:true,
            align:'left'
        }, {
            text:'应用操作ID',
            dataIndex:'RBACModuleOper_BTDAppComponentsOperate_Id',
            width:19,
            sortable:false,
            hidden:true,
            align:'left'
        } , {
            text:'应用操作时间戳',
            dataIndex:'RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp',
            width:19,
            sortable:false,
            hidden:true,
            align:'left'
        },{
            text:'提示信息',
            dataIndex:'dataStatus',
            width:69,
            sortable:false,
            hidden:false,
            align:'left'
        }  ],

    fields:
    [ 
    'dataStatus','RBACModuleOper_UseCode','RBACModuleOper_CName','RBACModuleOper_Comment', 'RBACModuleOper_IsUse',
    'RBACModuleOper_DispOrder','RBACModuleOper_RowFilterBase','RBACModuleOper_DefaultChecked','RBACModuleOper_RBACModule_CName', 'RBACModuleOper_RBACModule_IsUse',
    'RBACModuleOper_RBACModule_DispOrder', 'RBACModuleOper_RBACModule_Id','RBACModuleOper_RBACModule_DataTimeStamp','RBACModuleOper_Id','RBACModuleOper_DataTimeStamp', 'RBACModuleOper_LabID' ,
    'RBACModuleOper_BTDAppComponentsOperate_Id','RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp'
    ],
  
    /***
     * 编辑字段
     * Id,DataTimeStamp
     */
    editfields:'Id,CName,UseCode,IsUse,Comment,DispOrder,RowFilterBase',//BTDAppComponentsOperate_Id
    saveServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_AddRBACModuleOper',
    editServerUrl:getRootPath() + '/'+'RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
    getServerUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
    /***
     * 查询应用操作组件的URL
     */
    getBTDAppComponentsOperateUrl:getRootPath() +'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsOperateByHQL?isPlanish=true'+
    '&fields=BTDAppComponentsOperate_RowFilterBase,BTDAppComponentsOperate_DataTimeStamp,BTDAppComponentsOperate_Id,RBACModuleOper_RowFilterBase,BTDAppComponentsOperate_BTDAppComponents_Id,BTDAppComponentsOperate_BTDAppComponents_CName,BTDAppComponentsOperate_BTDAppComponents_ModuleOperCode',                

    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:false,
    /**
     * 该字段是否需要过滤
     * @private
     * @param {} field
     * @return {Boolean}
     */
    isFilterField:function(field){
        var me = this;
        var filterFields = me.filterFields || [];
        for(var i in filterFields){
            if(filterFields[i] == field.split('_').slice(-1)){
                return true;
            }
        }
        return false;
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /**
     * 弹出应用模块树页面
     * @private
     * @param {} type
     * @param {} id
     */
    openAppComponentsTreeWin:function(type,obj){
        var me = this;
        var title = '';
        var Id = -1;
         if(type == "show"){
            title = "应用操作树选择";
            Id = -1;
        }
        if(me.appId==""||me.appId==-1||me.appId==0){
	        alertError("当前模块应用操作信息获取不到,请重新对模块进行修改保存后再操作");
	        return ;
        }else{
	        var appComponentsTree = Ext.create('Ext.manage.module.appComponentsTree',{
	            modal:true,//模态
	            floating:true,//漂浮
	            closable:true,//有关闭按钮
	            draggable:true,//可移动
	            
	            isWindow:true,//窗口打开
	            
	            getAppListServerUrl:me.getAppListServerUrl,
	            updateFileServerUrl:me.updateFileServerUrl,
	            
	            title:title,
	            type:type,
	            Id:Id,
	            appId:me.appId,//外面传入的应用组件Id值
	            arrChecked:[],//
	            treeDataConfig:0,//为过滤没有操作的应用,其他值时不过滤
	            externalApp:me//外部调用的应用组件,如表单或者列表
	        }).show();
	        
	        //外部传入参数默认勾选子节点
	        //appComponentsTree.defaultChecked(arrChecked);
	        appComponentsTree.on({
	            cancelClick:function(){
	                appComponentsTree.close();
	            },
	            okClick:function(){
	                me.arrnodesChecked=appComponentsTree.getnodesChecked();
	                appComponentsTree.close();
	                //拼成查询应用操作组件信息的HQL的where串,以方便获取应用操作组件Id及应用操作组件时间戳
	                me.btdAppComponentsHQL='';
	                if(me.arrnodesChecked.length>0){
	                    //列表中显示被勾选中的对象
	                    me.btdAppComponentsHQL='';
	                    var store = me.store;
	                    Ext.Array.each(me.arrnodesChecked,function(record){
	                        
	                        var moduleOperCode=record.moduleOperCode;          
	                        //var result=store.findRecord('RBACModuleOper_UseCode',moduleOperCode);//不生效
	                        var result=false;
	                        var booluseCode="'"+record.moduleOperCode+"'"; 
	                        store.each(function(record){
					            var useCode = "'"+record.get('RBACModuleOper_UseCode')+"'";
					            if(useCode ==moduleOperCode){//
					                 result=true;//新增的数据模块操作代码重复
					             }
					        });
	                        
	                        if(result){
	                            alertError('模块操作代码重复,添加失败');
	                        }else{
			                    var btdAppComponentsHQL='&where= btdappcomponentsoperate.Id='+record.tid;
			                    var obj=me.getBTDAppComponentsDatasById(btdAppComponentsHQL);
	                            if(moduleOperCode.toString().length>1){
	                                moduleOperCode=moduleOperCode.substring(1,moduleOperCode.length-1);
	                            }
			                     //往前台操作列表新增数据
			                    var obj={ 
			                           'RBACModuleOper_Id':'-1',//新增数据
			                           'RBACModuleOper_UseCode':''+moduleOperCode,
			                           'RBACModuleOper_CName':''+record.text,
			                           'RBACModuleOper_IsUse':'true',
			                           'RBACModuleOper_Comment':null,
			                           'RBACModuleOper_DispOrder':'0',
	                                   "RBACModuleOper_RowFilterBase":''+obj['RowFilterBase'],
			                           'RBACModuleOper_BTDAppComponentsOperate_Id':''+record.tid,
			                           'RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp':''+obj['DataTimeStamp'],//应用操作时间戳
			                           'RBACModuleOper_RBACModule_Id':''+me.moduleId,
			                           'RBACModuleOper_RBACModule_CName':''+me.moduleCName,
			                           'RBACModuleOper_RBACModule_DataTimeStamp':''+me.moduleDataTimeStamp
			                        };
			                     var arr=[];
			                     arr.push(obj);
			                     me.setRecordByArray(arr);
	                             //新增的行设置成脏数据,以方便新增保存成功
	                        }
	                      });
	 
	                 }
	            }
	        });
      }
    },
    /**
     * 保存,修改按钮事件处理
     * @private
     */
    saveContents:function(){
        var me = this;
        var store = me.store;
        
        var addArr = [];//需要新增的数据
        var editArr = [];//需要修改的数据
        store.each(function(record){
            var dirty = record.dirty;//是否是脏数据
            var id = record.get('RBACModuleOper_Id');
            if(id == '-1'){//新增的数据为-1
                 addArr.push(record);
             }
            else if(dirty){
                editArr.push(record);
            }
        });
        
        //需要新增的数据
        for(var i in addArr){
            addArr[i].dataStatus = ''//状态置空
            var obj = me.getEntityByRecord(addArr[i]);
            var callback =null;
            callback = function(responseText){
                var result = Ext.JSON.decode(responseText);
                var record = addArr[i];
                if(result.success){
                    var data = Ext.JSON.decode(result.ResultDataValue);
                    record.set('RBACModuleOper_Id',data.id);
                    record.set('dataStatus','<b style="color:green">新增成功</b>');
                    record.commit();
                }else{
                    record.set('dataStatus','<b style="color:red">新增失败</b>');
                    //record.dirty=true;
                    //Ext.alert('提示','新增保存失败');
                }
            };
            var params = Ext.JSON.encode(obj);
            //util-POST方式与后台交互
            postToServer(me.saveServerUrl,params,callback);
        }
        //需要修改的数据
        for(var i in editArr){
            var callback =null;
            editArr[i].dataStatus = '';//状态置空
            var obj = me.geteditEntityByRecord(editArr[i]);
            var callback = function(responseText){
                var result = Ext.JSON.decode(responseText);
                var record = editArr[i];
                if(result.success){
                    record.set('dataStatus','<b style="color:green">修改成功</b>');
                    record.commit();
                }else{
                    record.set('dataStatus','<b style="color:red">修改失败</b>');
                    record.commit();
                }
            };
            var params = Ext.JSON.encode(obj);
            //util-POST方式与后台交互
            postToServer(me.editServerUrl,params,callback);
        }
    },
    /**
     * 根据record获取需要的数据对象--新增
     * @private
     * @param {} record
     * @return {}
     */
    getEntityByRecord:function(record){
        var me = this;
        var fields='Id,LabID,CName,RowFilterBase,UseCode,Comment,IsUse,DispOrder,DefaultChecked,RBACModule_Id,RBACModule_DataTimeStamp,BTDAppComponentsOperate_Id,BTDAppComponentsOperate_DataTimeStamp';
        var data = record;
        var CName2 = ''+data.get('RBACModuleOper_CName');
        var Comment2 = ''+data.get('RBACModuleOper_Comment');
        var IsUse = data.get('RBACModuleOper_IsUse');
        var DispOrder2 = data.get('RBACModuleOper_DispOrder');
        var UseCode2 = ''+data.get('RBACModuleOper_UseCode');
        var RowFilterBase = ''+data.get('RBACModuleOper_RowFilterBase');
        if (UseCode2==""||UseCode2 == undefined) {
           	alertError('不能保存数据,模块操作代码不能为空');
            return;
        }
        //模块子数据对象RBACModuleOper_RowFilterBase
        var entityRBACModule={};
        var RBACModuleId = ''+data.get('RBACModuleOper_RBACModule_Id');
        var RBACModuleDataTimeStamp = ''+data.get('RBACModuleOper_RBACModule_DataTimeStamp');
        var RBACModuleDataTimeStampArr=[];
        if (RBACModuleDataTimeStamp && RBACModuleDataTimeStamp != undefined) {
            RBACModuleDataTimeStampArr = RBACModuleDataTimeStamp.split(',');
        }else {
            alertError('不能保存数据,模块数据对象数据对象的时间戳值获取不到');
            return;
        }
        var RBACModule={Id:RBACModuleId,DataTimeStamp:RBACModuleDataTimeStampArr};
        
        //应用操作子数据对象
        var BTDAppComponentsOperateId = ''+data.get('RBACModuleOper_BTDAppComponentsOperate_Id');
        var BTDAppComponentsOperateDataTimeStamp = ''+data.get('RBACModuleOper_BTDAppComponentsOperate_DataTimeStamp');
        var BTDAppComponentsOperateDataTimeStampArr=[];
        if (BTDAppComponentsOperateDataTimeStamp && BTDAppComponentsOperateDataTimeStamp != undefined) {
            BTDAppComponentsOperateDataTimeStampArr = BTDAppComponentsOperateDataTimeStamp.split(',');
        }else {
            alertError('不能保存数据,应用操作数据对象的时间戳值获取不到');
            return;
        }
        var BTDAppComponentsOperate={Id:BTDAppComponentsOperateId,DataTimeStamp:BTDAppComponentsOperateDataTimeStampArr};
        var  newAdd= {
                Id:-1,
                LabId:1,
                RowFilterBase:""+RowFilterBase,
                UseCode:''+UseCode2.toString(),
                CName:''+CName2.toString(),
                IsUse:IsUse,
                DispOrder:''+DispOrder2.toString(),
                RBACModule:RBACModule,
                BTDAppComponentsOperate:BTDAppComponentsOperate
            };
        if(Comment2!=""&&Comment2!=null&&Comment2!=undefined){
             newAdd.Comment=''+Comment2.toString(); 
        }          
       var obj={'entity':newAdd};//,'fields':fields
        
        return obj;
    },
    /**
     * 根据record获取需要的数据对象--修改
     * @private
     * @param {} record
     * @return {}
     */
    geteditEntityByRecord:function(record){
        var me = this;
        var data = record;
        var Id = ''+data.get('RBACModuleOper_Id');
        //var LabId = ''+data.get('RBACModuleOper_LabId');
        var CName = ''+data.get('RBACModuleOper_CName');
        var Comment = ''+data.get('RBACModuleOper_Comment');
        var UseCode2 = ''+data.get('RBACModuleOper_UseCode');
        
        var IsUse= data.get('RBACModuleOper_IsUse');
        var DispOrder = ''+data.get('RBACModuleOper_DispOrder');
        var RowFilterBase = ''+data.get('RBACModuleOper_RowFilterBase');
        if (UseCode2==""||UseCode2 == undefined) {
           	alertError('不能保存数据,模块操作代码不能为空');
            return;
        }
        var  newedit= {
                'Id':Id,
                'UseCode':''+UseCode2.toString(),
                'CName':''+CName.toString(),
                'Comment':''+Comment.toString(),
                'IsUse':IsUse,
                "RowFilterBase":""+RowFilterBase,
                'DispOrder':''+DispOrder.toString() 
            };
                    
        var obj={'entity':newedit,'fields':me.editfields};
        return obj;
    },
    /***
     * 获取应用操作组件Id及应用操作时间戳
     */
    getBTDAppComponentsDatasById:function(hql) {
        var me=this;
        var DataTimeStamp='';
        var obj=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.getBTDAppComponentsOperateUrl+hql+'',
            method:'GET',
            timeout:5000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                    var count = ResultDataValue['count'];
                    if(count>0){
	                    var list=ResultDataValue.list[count-1];
	                    var DataTimeStamp=''+list['BTDAppComponentsOperate_DataTimeStamp'];
	                    var RowFilterBase=''+list['BTDAppComponentsOperate_RowFilterBase'];//行过滤依据对象
	                    obj={"RowFilterBase":RowFilterBase,"DataTimeStamp":DataTimeStamp};
                    }else{
                        obj={"RowFilterBase":"","DataTimeStamp":""};
                    }
                }
                }else{
                    obj={"RowFilterBase":"","DataTimeStamp":""};
                    alertError('获取信息失败！');
                }
            },
            failure : function(response,options){ 
                obj={"RowFilterBase":"","DataTimeStamp":""};
                alertError('获取信息请求失败！');
            }
        });
        return obj;
    },
 
    /**
     * 给记录列表赋值
     * @private
     * @param {} array
     */
    setRecordByArray:function(array){
        var me = this;
        Ext.Array.each(array,function(obj){
            var rec = ('Ext.data.Model',obj);
            me.addRecordByRecord(rec);//添加组件记录
        });
    },
    /**
     * 新添加模块操作记录
     * @private
     * @param {} record
     */
    addRecordByRecord:function(record){
        var me = this;
        var list = me;//列属性列表
        var store = list.store;
        store.add(record);
        //store.sync();
    },
    createStore:function(){
        var me=this;
        var w = '';
        //外部传入的模块Id
        if(me.moduleId!=''){
            w +='rbacmoduleoper.RBACModule.Id='+me.moduleId +'';
        }
	    me.store = Ext.create('Ext.data.Store', {
	            fields:me.fields,
	            remoteSort:true,
	            autoLoad:false,
	            sorters:[],
	            //pageSize:10,
	            proxy:{
	                type:'ajax',
	                url:me.getServerUrl+'&fields='+me.queryFields + '&where=' + w,
	                reader:{
	                    type:'json',
	                    root:'list',
	                    totalProperty:'count'
	                },
	                extractResponseData:function(response) {
	                    var data = Ext.JSON.decode(response.responseText);
	                    if (data.ResultDataValue && data.ResultDataValue != '') {
	                        data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g,'<br/>');
		                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
		                    data.count = ResultDataValue.count;
		                    data.list = ResultDataValue.list;
	                    } else {
	                        data.list = [];
	                        data.count = 0;
	                    }
	                    response.responseText = Ext.JSON.encode(data);
	                    return response;
	                }
	            }
	        });
    },
    load:function(where) {
        var me = this;
        me.externalWhere = where;
        var w = '';
        if (me.externalWhere && me.externalWhere != '') {
            if (me.externalWhere.slice(-1) == '^') {
                w += me.externalWhere;
            } else {
                w += me.externalWhere + ' and ';
            }
        }
        if (me.defaultWhere && me.defaultWhere != '') {
            w += me.defaultWhere + ' and ';
        }
        if (me.internalWhere && me.internalWhere != '') {
            w += me.internalWhere + ' and ';
        }
        //外部传入的模块Id
        if(me.moduleId!=''){
            w += 'rbacmoduleoper.RBACModule.Id='+me.moduleId + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0, -5) :w;
        me.store.currentPage = 1;
        me.store.proxy.url = me.getServerUrl+'&fields='+me.queryFields + '&where=' + w;
        me.store.load();
     },
     deleteInfo:function(id, callback) {
            var url = getRootPath() + '/RBACService.svc/RBAC_UDTO_DelRBACModuleOper?id=' + id;
            Ext.Ajax.defaultPostHeader = 'application/x-www-form-urlencoded';
            Ext.Ajax.request({
                async:false,
                url:url,
                method:'GET',
                timeout:2000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (Ext.typeOf(callback) == 'function') {
                            callback();
                        }
                    } else {
                        alertError('删除信息失败！错误信息:' + result.ErrorInfo);
                    }
                },
                failure:function(response, options) {
                    alertError('删除信息请求失败！');
                }
            });
        },
    initComponent:function() {
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.manage',getRootPath()+'/ui/manage/class');
        Ext.Loader.setPath('Ext.manage.module.appComponentsTree', getRootPath() + '/ui/manage/class/module/appComponentsTree.js');
        me.createStore();
        me.dockedItems = [ {
            xtype:'toolbar',
            itemId:'buttonstoolbar',
            dock:'top',
            items:[ {
                type:'refresh',
                itemId:'refresh',
                text:'更新',
                iconCls:'build-button-refresh',
                handler:function(but, e) {
                    var com = but.ownerCt.ownerCt;
                    com.store.load(com.externalWhere);
                }
            }, {
                type:'add',
                itemId:'add',
                text:'新增',
                iconCls:'build-button-add',
                handler:function(but, e) {
                    me.fireEvent('addClick');
                    if(me.isOpenAppComponentsTree==true){
                        me.openAppComponentsTreeWin();
                    }
                }
            }, {
                type:'del',
                itemId:'del',
                text:'删除',
                iconCls:'build-button-delete',
                handler:function(but, e) {
                    me.fireEvent('delClick');
                    var list = but.ownerCt.ownerCt;
                    var records = list.getSelectionModel().getSelection();
                    if (records.length > 0) {
                        Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
                            if (button == 'yes') {
                                var records = me.getSelectionModel().getSelection();
                                for (var i in records) {
                                    var id = records[i].get('RBACModuleOper_Id');
                                    var callback = function() {
                                        var rowIndex = me.store.find('RBACModuleOper_Id', id);
                                        me.deleteIndex = rowIndex; 
                                    };
                                    me.deleteInfo(id, callback);
                                }
                                me.load();
                            }
                        });
                    } else {
                        alertError('请选择数据进行操作！');
                    }
                }
            }, {
                xtype:'button',
                text:'修改保存',
                iconCls:'build-button-save',
                margin:'0 0 0 2',
                itemId:'save-button',
                name:'save-button',
                listeners:{
                    click:function(but, e, eOpts) {
                        Ext.Msg.confirm('警告', '确定要执行修改保存吗？', function(button) {
                            if (button == 'yes') {
                                me.saveContents();
                                //me.store.load();
                                me.fireEvent('savechangesClick');
                            } else {
                                //me.store.load();
                            }
                        });
                    }
                }
            } ]
        } ];
       
        var rowEditing = Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit:1
        });
        me.plugins = [ rowEditing ];
        
        me.openFormWin = function(type, id, record) {};
        me.addEvents('addClick');//新增按钮事件
        me.addEvents('delClick');//删除事件
        me.addEvents('savechangesClick');//修改保存后事件
        this.callParent(arguments);
    }
});