/***
 * 未外送样本清单Tabpanel页
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliverModule.unSendSampleList', {
    extend:'Ext.grid.Panel',
    alias:'widget.unSendSampleList',
    title:'未外送样本清单列表',
    width:850,
    height:200,
    objectName:'MEPTSampleForm',
    openFormId:'',
    defaultLoad:true,
    defaultWhere:'meptsampleform.IsSendOut=1',//  meptsampleform.IsSendOut
    //selType:'checkboxmodel',
    multiSelect:true,
    sortableColumns:false,

    /***
     * 获取样本单数据
     */
    url:getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleFormByHQL?isPlanish=true&fields=checkSelect,MEPTSampleForm_BarCode,MEPTSampleForm_IsSendOut,' + 
                'MEPTSampleForm_MEPTOrderForm_Client_CName,MEPTSampleForm_MEPTOrderForm_Client_Id,MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp,'+
                'MEPTSampleForm_MEPTOrderForm_HRDept_CName,MEPTSampleForm_MEPTOrderForm_HRDept_Id,MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp,'+
                'MEPTSampleForm_MEPTOrderForm_BSickType_Name,MEPTSampleForm_MEPTOrderForm_BSickType_Id,MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp,'+
                'MEPTSampleForm_BarCodeOpTime,MEPTSampleForm_SerialScanTime,' +
                'MEPTSampleForm_BSampleStatus_Id,MEPTSampleForm_BSampleStatus_DataTimeStamp,' +
                'MEPTSampleForm_BSampleType_Name,MEPTSampleForm_BSampleType_Id,MEPTSampleForm_BSampleType_DataTimeStamp,MEPTSampleForm_MEPTSamplingGroup_CName,' +
                'MEPTSampleForm_MEPTSamplingGroup_Destination,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_CName,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum,' +
                ',MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_Id,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_DataTimeStamp,MEPTSampleForm_MEPTOrderForm_SerialNo,MEPTSampleForm_MEPTOrderForm_OldSerialNo,' +
                'MEPTSampleForm_MEPTOrderForm_PatCardNo,MEPTSampleForm_MEPTOrderForm_InpatientNo,MEPTSampleForm_MEPTOrderForm_PatientID,MEPTSampleForm_MEPTOrderForm_PatNo,MEPTSampleForm_MEPTOrderForm_CName,MEPTSampleForm_MEPTOrderForm_Birthday,MEPTSampleForm_MEPTOrderForm_Age,' +
                'MEPTSampleForm_MEPTOrderForm_Bed,MEPTSampleForm_MEPTOrderForm_DoctorID,MEPTSampleForm_MEPTOrderForm_Doctor,MEPTSampleForm_MEPTOrderForm_Diag,MEPTSampleForm_MEPTOrderForm_Charge,' +
                'MEPTSampleForm_MEPTOrderForm_Id,MEPTSampleForm_MEPTOrderForm_DataTimeStamp,MEPTSampleForm_Id,MEPTSampleForm_DataTimeStamp',
    /***
     *获取指定样本单项目明细数据 
     * @param {} val
     * @return {}
     */
    myurl:getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleItemByHQL?isPlanish=true&fields=' +
            'MEPTSampleItem_ItemAllItem_CName,MEPTSampleItem_ItemAllItem_Id,MEPTSampleItem_ItemAllItem_DataTimeStamp,' +
            'MEPTSampleItem_SampleFrom_Id,MEPTSampleItem_SampleFrom_DataTimeStamp,' +
            'MEPTSampleItem_SampleFrom_MEPTOrderForm_Id,MEPTSampleItem_SampleFrom_MEPTOrderForm_DataTimeStamp,' +
            'MEPTSampleItem_Id,MEPTSampleItem_DataTimeStamp',
    /**
     * 用于列渲染器的自定义功能
     * renderer : change,
     * @param {Object} val
     */
    change:function(val) {
        if (val > 0) {
            return '<span style="color:green;">' + val + '</span>';
        } else if (val < 0) {
            return '<span style="color:red;">' + val + '</span>';
        }
        return val;
    },
    /**
     * 设置cookie中的操作ID
     * @private
     * @param {} value
     */
    setModuleOperCookie:function(value){
        var v = value || "";
        Ext.util.Cookies.set('000660',v);//模块操作ID
    },
    /**
     * 根据where条件加载数据
     * @public
     * @param {} where
     */
    load:function(where){
        var me = this;
        if(me.loaddata){
            me.setModuleOperCookie(me.loaddata);//设置cookie中的操作ID
        }
        if(where !== true){
            me.externalWhere = where;
        }
        var w = '';
        if(me.externalWhere && me.externalWhere != ''){
            if(me.externalWhere.slice(-1) == '^'){
                w += me.externalWhere;
            }else{
                w += me.externalWhere+' and ';
            }
        }
        if(me.defaultWhere && me.defaultWhere != ''){
            w += me.defaultWhere.replace(/\%25/g,"%").replace(/\%27/g,"'") +' and ';
        }
        
        if(me.internalWhere && me.internalWhere != ''){
            w += me.internalWhere + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
        me.store.currentPage = 1;
        me.store.proxy.url = encodeString(me.url + '&where=' + w);
        me.store.load();
    },
    /**
     * 创建数据集
     * @private
     * @param {} config
     * @return {}
     */
    createStore:function(config){
        var me = this;
        var cfg = {
            fields:config.fields,
            remoteSort:config.remoteSort,
            sorters:config.sorters,
            pageSize:config.PageSize,
            proxy:{
                type:'ajax',
                url:config.url,   //getRootPath() + '/' +
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                //内部数据匹配方法
                extractResponseData:function(response){
                    return me.changeData(response,config.hasCountToolbar); 
                }
            },
            onBeforeSort:function(){
                if(me.loaddata){
                    me.setModuleOperCookie(me.loaddata);//设置cookie中的操作ID
                }
                var groupers = this.groupers;
                if (groupers.getCount() > 0) {
                    this.sort(groupers.items, 'prepend', false);
                }
            },
            loadPage:function(page,options){
                //条件处理
                this.proxy.url = me.getDefaultUrl();
                this.proxy.url = me.getLoadUrl();
                //原组件的代码
                this.currentPage = page;
                // Copy options into a new object so as not to mutate passed in objects
                options = Ext.apply({
                    page: page,
                    start: (page - 1) * this.pageSize,
                    limit: this.pageSize,
                    addRecords: !this.clearOnPageLoad
                }, options);
        
                if (this.buffered) {
                    return this.loadToPrefetch(options);
                }
                this.read(options);
            }
        };
        if(config.buffered){
            cfg.buffered = config.buffered;
            cfg.leadingBufferZone = config.leadingBufferZone || config.PageSize;
        }
        
        var store = Ext.create('Ext.data.Store',cfg);
        return store;
    },
        /**
     * 获取带查询参数的URL
     * @private
     * @return {}
     */
    getLoadUrl:function(){
        var me = this;
        var w = '';
        if(me.externalWhere && me.externalWhere != ''){
            if(me.externalWhere.slice(-1) == '^'){
                w += me.externalWhere;
            }else{
                w += me.externalWhere+' and ';
            }
        }
        if(me.defaultWhere && me.defaultWhere != ''){
            w += me.defaultWhere.replace(/\%25/g,"%").replace(/\%27/g,"'") +' and ';
        }
        
        if(me.internalWhere && me.internalWhere != ''){
            w += me.internalWhere + ' and ';
        }
        w = w.slice(-5) == ' and ' ? w.slice(0,-5) : w;
        
        return encodeString(me.url + '&where=' + w);
    },
    /**
     * 数据转化
     * @private
     * @param {} response
     * @return {}
     */
    changeData:function(response,bo){
        var me = this;
        
        //var where=' (qcmattime.BeginDate is null or qcmattime.BeginDate<='+"'"+dateTody+"')" +' and (qcmattime.EndDate is null or qcmattime.EndDate>='+"'"+dateTody+"') and qcmattime.IsUse=true";
        //var where='MEPTSampleItem_SampleFrom_Id';
        var dataurl=me.myurl;//+'&where'+where;..
        var data = Ext.JSON.decode(response.responseText);
        var success = (data.success + "" == "true" ? true : false);
        
        //质控物与时效数据合并处理
        var c = function(data){
            response.responseText = Ext.JSON.encode(data);
            return response;
        };
        var callback = function(text){
            //对象化返回的数据；
            //将质控物列表与有效性数据列表合并
            //alert(data.);
            var datavalue=text;
            //response.responseText = Ext.JSON.encode(text);
            var data1=Ext.JSON.decode(text);
            var ResultDataValue1=Ext.JSON.decode(data1.ResultDataValue);
            var list=ResultDataValue1.list;
            
            //质控物数组
            var arr1=data.list
            for(var i=0;i<arr1.length;i++)
            {
                var matertalID=arr1[i]['MEPTSampleForm_Id'];
                for(var j=0;j<list.length;j++)
                {
                    var qcmaterialtimeId=list[j]['MEPTSampleItem_SampleFrom_Id'];
                    arr1[i]['MEPTSampleItem_ItemAllItem_CName']=(arr1[i]['MEPTSampleItem_ItemAllItem_CName']==''||arr1[i]['MEPTSampleItem_ItemAllItem_CName']=='undefined')?'':arr1[i]['MEPTSampleItem_ItemAllItem_CName'];
                    if(matertalID==qcmaterialtimeId)
                    {
                        arr1[i]['MEPTSampleItem_ItemAllItem_CName']=arr1[i]['MEPTSampleItem_ItemAllItem_CName']+list[j]['MEPTSampleItem_ItemAllItem_CName']+',';
                        //arr1[i]['QCMatTime_EndDate']=list[j]['QCMatTime_EndDate'];
                        //arr1[i]['QCMatTime_CanUseBeginDate']=list[j]['QCMatTime_CanUseBeginDate'];
                        //arr1[i]['QCMatTime_CanUseEndDate']=list[j]['QCMatTime_CanUseEndDate'];
                    }
                }
                //arr1[i]['MEPTSampleItem_ItemAllItem_CName']=arr1[i]['MEPTSampleItem_ItemAllItem_CName'].split();
                
            }
            
            c(arr1);
        };
        if(!success){
            me.showError(data.ErrorInfo);
        }
        if(data.ResultDataValue && data.ResultDataValue != ''){
            data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,"");
            var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
            data.list = ResultDataValue.list;
            
            data.count = ResultDataValue.count;
        }else{
            data.list = [];
            data.count = 0;
        }
        
        //获取未外送样本项目明细数据
        getToServer(dataurl,callback,false);

        response.responseText = Ext.JSON.encode(data);;
        bo && me.setCount(data.count);;//总条数数值赋值
        return response;
    },
    /**
     * 显示总条数
     * @private
     * @param {} count
     */
    setCount:function(count){
        var me = this;
        var bottomtoolbar = me.getComponent('toolbar-bottom');
        if(bottomtoolbar){
            var com = bottomtoolbar.getComponent('count');
            if(com){
                var str = '共'+count+'条';
                com.setText(str,false);
            }
        }
    },

    /**
     * 删除质控物
     * @param {} id
     * @param {} callback
     */
    deleteInfo:function(id, callback) {
        
        var url = getRootPath() + '/QCService.svc/QC_UDTO_DelQCMat?id=' + id;
        var c = function(text) {
            var result = Ext.JSON.decode(text);
            if (result.success) {
                if (Ext.typeOf(callback) == 'function') {
                    callback();
                }
            } else {
                alertError(result.ErrorInfo);
            }
        };
        getToServer(url, c);
    },
     /**
     * 创建下拉框数据集
     * @private
     * @param {} config
     * @return {}
     */
    createComboStore:function(config){
        var me = this,
            fields = config.fields,
            url = config.url,
            InteractionField = config.InteractionField,
            DataTimeStampField = config.DataTimeStampField,
            valueField = config.valueField;
        var store = Ext.create('Ext.data.Store',{
            autoLoad:true,
            fields:fields.split(","),
            pageSize:5000,
            proxy:{
                type:'ajax',
                url:getRootPath() + "/" + url + "&fields=" + fields,
                reader:{type:'json',totalProperty:'count',root:'list'},
                extractResponseData:function(response){
                    return me.changeStoreData(response);
                }
            },
            listeners:{
                load:function(s,records,successful){
                    var combo = me.getComponent(InteractionField);
                    if(combo){
                        combo.setValue(combo.defaultValue || '');
                    }
                    var com = me.getComponent(DataTimeStampField);
                    if(com){
                        var record = s.findRecord(valueField,combo.getValue());
                        if(record != null && record != ""){
                            var value=record.get(DataTimeStampField.split("_").slice(-2).join("_"));
                            com.setValue(value);
                        }
                    }
                }
            }
        });
        return store;
    },
    /**
     * 列表格式数据匹配方法
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData:function(response,callback){
        var result = Ext.JSON.decode(response.responseText);
        result.count = 0;result.list = [];
        
        if(result.ResultDataValue && result.ResultDataValue !=''){
            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
            result.count = ResultDataValue.count;
            result.list = ResultDataValue.list;
            result.ResultDataValue = "";
        }
        if(Ext.typeOf(callback) === 'function'){
            callback(result);
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    },
        /**
     * 创建下拉框监听
     * @private
     * @return {}
     */
    createComboListeners:function(){
        var me = this;
        var listeners = {};
        //选中时监听
        listeners.select = function(combo,records){
            var com = combo.ownerCt.getComponent(combo.DataTimeStampField);
            if(com){
                //时间戳匹配处理
                var data = records[0].data;
                for(var i in data){
                    var arr = i.split('_');
                    var lastWord = arr.slice(-1);
                    //var lastWord = i.split('_').slice(-1);
                    if(lastWord == 'DataTimeStamp'){
                        var value = data[i]
                        com.setValue(value);
                     }
                 }
            }
        };
        //模糊匹配
        listeners.beforequery = function(e){
            var combo = e.combo;
            if(!e.forceAll){
                var value = e.query;
                combo.store.filterBy(function(record,id){
                    var text = record.get(combo.displayField);
                    return (text.indexOf(value) != -1);
                });
                combo.expand();
                return false;
            }
        };
        //追加基础监听
        var basicListeners = me.createBasicListeners();
        for(var i in basicListeners){
            listeners[i] = basicListeners[i];
        }
        
        return listeners;
    },
    /**
     * 创建基础组件监听
     * @private
     * @return {}
     */
    createBasicListeners:function(){
        var me = this;
        var listeners = {};
        return listeners;
    },
    //======================================
        /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
        var me = this;
        me.callParent(arguments);
        me.defaultLoad && me.load(true);

        if(Ext.typeOf(me.callback)==='function'){me.callback(me);}
    },
    /**
     * 初始化
     */
    initComponent : function() {
        var me = this;
         Ext.Loader.setConfig({enabled:true});
         //Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux/GridPanel.js');

        me.searchArray = [ ];
        me.store = me.createStore({
            fields : ['checkSelect','MEPTSampleForm_BarCode', 'MEPTSampleForm_IsSendOut','MEPTSampleItem_ItemAllItem_CName',
                   'MEPTSampleForm_MEPTOrderForm_Client_CName','MEPTSampleForm_MEPTOrderForm_Client_Id','MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                   'MEPTSampleForm_MEPTOrderForm_HRDept_CName','MEPTSampleForm_MEPTOrderForm_HRDept_Id','MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp',
                   'MEPTSampleForm_MEPTOrderForm_BSickType_Name','MEPTSampleForm_MEPTOrderForm_BSickType_Id','MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp',
                   'MEPTSampleForm_BarCodeOpTime', 'MEPTSampleForm_SerialScanTime',                   
                   'MEPTSampleForm_BSampleStatus_Id', 'MEPTSampleForm_BSampleStatus_DataTimeStamp','MEPTSampleForm_BSampleType_Name', 
                   'MEPTSampleForm_BSampleType_Id', 'MEPTSampleForm_BSampleType_DataTimeStamp', 'MEPTSampleForm_MEPTSamplingGroup_CName',
                   'MEPTSampleForm_MEPTSamplingGroup_Destination','MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_CName',
                   'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum',
                   'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_Id', 'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_DataTimeStamp',
                   'MEPTSampleForm_MEPTOrderForm_SerialNo', 'MEPTSampleForm_MEPTOrderForm_OldSerialNo',
                   'MEPTSampleForm_MEPTOrderForm_PatCardNo', 'MEPTSampleForm_MEPTOrderForm_InpatientNo',
                   'MEPTSampleForm_MEPTOrderForm_PatientID', 'MEPTSampleForm_MEPTOrderForm_PatNo',
                   'MEPTSampleForm_MEPTOrderForm_CName', 'MEPTSampleForm_MEPTOrderForm_Birthday',
                   'MEPTSampleForm_MEPTOrderForm_Age', 'MEPTSampleForm_MEPTOrderForm_Bed',
                   'MEPTSampleForm_MEPTOrderForm_DoctorID', 'MEPTSampleForm_MEPTOrderForm_Doctor',
                   'MEPTSampleForm_MEPTOrderForm_Diag','MEPTSampleForm_MEPTOrderForm_Charge',
                   'MEPTSampleForm_MEPTOrderForm_Id','MEPTSampleForm_MEPTOrderForm_DataTimeStamp', 'MEPTSampleForm_Id', 'MEPTSampleForm_DataTimeStamp' ],
             url :me.url,
            remoteSort : true,
            sorters : [],
            PageSize : 25,
            hasCountToolbar : false,
            buffered : false,
            leadingBufferZone : null
        });
        me.columns = [ {
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        },{
            text : '选中',
            dataIndex : 'checkSelect',
            width : 35,
            editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },
            align:'left',
            xtype:'checkcolumn'
         },{
            text:'条码号',
            dataIndex:'MEPTSampleForm_BarCode',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'是否外送样本',
            dataIndex:'MEPTSampleForm_IsSendOut',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'申请单号',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_SerialNo',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'样本单号',
            dataIndex:'MEPTSampleForm_Id',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        },{
            text:'原始申请单号',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_OldSerialNo',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'就诊卡号',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_PatCardNo',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'住院号',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_InpatientNo',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'病人ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_PatientID',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'病历号',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_PatNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'姓名',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_CName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'生日',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Birthday',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'年龄',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Age',
            width:35,
            sortable:false,
            hideable:true,
            align:'left'
        },{
            text:'科室',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_HRDept_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
            
        },{
            text:'送检目的地',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_Destination',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        },{
            text:'委托单位',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Client_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
            
        },{
            text:'就诊类别',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_BSickType_Name',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
            
        },{
            text:'样本类型',
            dataIndex:'MEPTSampleForm_BSampleType_Name',
            width:70,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'病床',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Bed',
            width:40,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'医生ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_DoctorID',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'医生',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Doctor',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'临床诊断',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Diag',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'外送项目',
            dataIndex:'MEPTSampleItem_ItemAllItem_CName',
            width:200,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'费用',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Charge',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, /*{
            text:'采样部位',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_CollectPart',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'送检目的',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_TestAim',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        },*/ {
            text:'主键ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'委托单位ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Client_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'委托单位时间戳',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'就诊类别ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_BSickType_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'就诊类别时间戳',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },
        {
            text:'委托单位ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_HRDept_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'委托单位时间戳',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'条码生成时间',
            dataIndex:'MEPTSampleForm_BarCodeOpTime',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'条码扫描时间',
            dataIndex:'MEPTSampleForm_SerialScanTime',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        },{
            text:'主键ID',
            dataIndex:'MEPTSampleForm_BSampleStatus_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleForm_BSampleStatus_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleForm_BSampleType_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleForm_BSampleType_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'采样组',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_CName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        },{
            text:'名称',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_CName',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'条码打印份数',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'主键ID',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },  {
            text:'时间戳',
            dataIndex:'MEPTSampleForm_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } ];
        
        //===================================
        var dateintervals=Ext.create('Ext.zhifangux.DateIntervals',
            {
                editable : true,
                name : 'dtDateintervals',
                itemId : 'dtDateintervals',
                fieldLabel : '查询日期',
                fieldLabelTwo : '至 ',
                labelSeparator:'',
                width : 180,
                layoutType : 'hbox',
                value : new Date(),
                valueTwo : new Date(),
                bodyCls:'',
                cls:'',
                hidden : false,
                dateFormat : 'Y-m-d'
        });
        var dateField=Ext.create('Ext.zhifangux.DateField',
            {
                name : 'dtStarEnd',
                itemId : 'dtStarEnd',
                fieldLabel : '',
                labelWidth : 5,
                width : 100,
                editable : true,
                readOnly : false,
                hidden : false,
                format : 'Y-m'
        });
        
        //===================================
        me.dockedItems = [
            {
            xtype:'toolbar',
            itemId:'toolbar-top',
            dock:'top',
            items:[/*{
                    xtype : 'label',
                    name : 'lblsearch',
                    itemId : 'lblsearch',
                    fieldLabel : '',
                    width : 60,                    
                    hidden : false,
                    align:'right',
                    text : '查询日期'
                },*/' ',{
                xtype:'button',
                itemId:'btnCheckAll',
                text:'全选',
                iconCls:'build-button-unchecked'　　　//build-button-checked
            },{
                xtype:'button',
                itemId:'btnUnCheckAll',
                text:'全否',
                iconCls:'build-button-unchecked'    //build-button-unchecked
            },'　　　',dateintervals,'　',                
                {
                   xtype:'combobox',
                    editable:true,
                    typeAhead:true,
                    queryMode:'local',
                    defaultValue:'',
                    displayField:'BLaboratory_CName',
                    valueField:'BLaboratory_Id',
                    DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                    store:me.createComboStore({
                        fields:'BLaboratory_CName,BLaboratory_Id,BLaboratory_DataTimeStamp',
                        url:'SingleTableService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true',
                        InteractionField:'MEPTSampleForm_MEPTOrderForm_Client_CName',
                        DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                        valueField:'BLaboratory_Id'
                    }),
                    type:'combobox',
                    itemId:'BLaboratory_Id',
                    name:'BLaboratory_Id',
                    labelWidth:60,
                    height:22,
                    readOnly:false,
                    labelStyle:'font-style:normal',
                    fieldLabel:'委托单位',
                    labelAlign:'left',
                    sortNum:15,
                    hasReadOnly:false,
                    hidden:false,
                    width:210,
                    listeners://me.createComboListeners()
                    {                            
                        change:function(com, newValue, oldValue, eOpts )
                        {
                            //me.search();
                        }
                    }   
                },'  ',{
                    xtype:'combobox',
                    editable:true,
                    typeAhead:true,
                    queryMode:'local',
                    defaultValue:'',
                    displayField:'HRDept_CName',
                    valueField:'HRDept_Id',
                    DataTimeStampField:'QCMat_EPBEquip_DataTimeStamp',
                    store:me.createComboStore({
                        fields:'HRDept_CName,HRDept_Id,HRDept_DataTimeStamp',
                        url:'RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
                        InteractionField:'MEPTSampleForm_MEPTOrderForm_HRDept_CName',
                        DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp',
                        valueField:'HRDept_Id'
                    }),
                    type:'combobox',
                    itemId:'HRDept_Id',
                    name:'HRDept_Id',//QCMat_EPBEquip_CName
                    labelWidth:60,
                    height:22,
                    readOnly:false,
                    labelStyle:'font-style:normal',
                    fieldLabel:'送检单位',
                    labelAlign:'left',
                    sortNum:15,
                    hasReadOnly:false,
                    hidden:false,
                    width:210,
                    listeners://me.createComboListeners()
                    {                            
                        change:function(com, newValue, oldValue, eOpts )
                        {
                            //me.search();
                        }
                    }
                },'　',{
                type : 'refresh',
                itemId:'btnrefresh',
                text:'查询',
                iconCls:'build-button-refresh'
            }]
        },{xtype:'toolbar',
            itemId:'toolbar-bottom',
            dock:'bottom',
            items:['　　',{
                   xtype:'combobox',
                    editable:true,
                    typeAhead:true,
                    queryMode:'local',
                    defaultValue:'',
                    displayField:'BLaboratory_CName',
                    valueField:'BLaboratory_Id',
                    DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                    store:me.createComboStore({
                        fields:'BLaboratory_CName,BLaboratory_Id,BLaboratory_DataTimeStamp',
                        url:'SingleTableService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true',
                        InteractionField:'MEPTSampleForm_MEPTOrderForm_Client_CName',
                        DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                        valueField:'BLaboratory_Id'
                    }),
                    type:'combobox',
                    itemId:'BLaboratoryId',
                    name:'BLaboratoryId',
                    labelWidth:120,
                    height:22,
                    readOnly:false,
                    labelStyle:'font-style:normal',
                    fieldLabel:'指定到新的委托单位',
                    labelAlign:'left',
                    sortNum:15,
                    hasReadOnly:false,
                    hidden:false,
                    width:240,
                    listeners://me.createComboListeners()
                    {                            
                        change:function(com, newValue, oldValue, eOpts )
                        {
                            //me.search();
                        }
                    }   
                },{
                type : 'refresh',
                itemId:'btnset',
                text:'设置',
                iconCls:'build-button-refresh'
            },'　　',{
                    xtype:'combobox',
                    editable:true,
                    typeAhead:true,
                    queryMode:'local',
                    defaultValue:'',
                    displayField:'HRDept_CName',
                    valueField:'HRDept_Id',
                    DataTimeStampField:'QCMat_EPBEquip_DataTimeStamp',
                    store:me.createComboStore({
                        fields:'HRDept_CName,HRDept_Id,HRDept_DataTimeStamp',
                        url:'RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
                        InteractionField:'MEPTSampleForm_MEPTOrderForm_HRDept_CName',
                        DataTimeStampField:'MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp',
                        valueField:'HRDept_Id'
                    }),
                    type:'combobox',
                    itemId:'HRDept_IdNew',
                    name:'HRDept_IdNew',//QCMat_EPBEquip_CName
                    labelWidth:120,
                    height:22,
                    readOnly:false,
                    labelStyle:'font-style:normal',
                    fieldLabel:'指定送出单位名称',
                    labelAlign:'left',
                    sortNum:15,
                    hasReadOnly:false,
                    hidden:false,
                    width:240,
                    listeners://me.createComboListeners()
                    {                            
                        change:function(com, newValue, oldValue, eOpts )
                        {
                            //me.search();
                        }
                    }
                }
           ]
        }
        ];
         //me.plugins=Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:2});
        this.callParent(arguments);
    }
});