/**
 * 已外送清单明细列表
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliveryModule.sendDeliveryDetaileList', {
    extend:'Ext.grid.Panel',
    alias : 'widget.sendDeliveryDetaileList',
    title : '',
    width : 318,
    objectName : 'MEPTSampleDeliveryConditon',
    openFormId : '',
    defaultLoad : false,
    border:false,
    defaultWhere : '',//'meptsampledeliveryconditon.MEPTSampleDelivery.DeliveryDept.CName=1',
    sortableColumns : true,
    /***
     * 获取已外送清单明细数据
     */
    url:getRootPath()+ getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleDeliveryConditonByHQL?isPlanish=true&fields=MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo,MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryDate,MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryMan,MEPTSampleDeliveryConditon_MEPTSampleDelivery_PrintCount,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_CName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_EName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_ShortCode,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_LinkMan,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_PhoneNum1,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_CName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleDelivery_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCode,MEPTSampleDeliveryConditon_MEPTSampleForm_IsSpiltItem,MEPTSampleDeliveryConditon_MEPTSampleForm_IsSendOut,MEPTSampleDeliveryConditon_MEPTSampleForm_SendOutLabID,MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCap,MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCount,MEPTSampleDeliveryConditon_MEPTSampleForm_CollectPart,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeOpTime,MEPTSampleDeliveryConditon_MEPTSampleForm_SerialScanTime,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeSource,MEPTSampleDeliveryConditon_MEPTSampleForm_PrintCount,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_SerialNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_OldSerialNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatCardNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_InpatientNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatientID,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_CName,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Birthday,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Age,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Bed,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DoctorID,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Doctor,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Diag,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_IsFree,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Charge,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_FormMemo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_TestAim,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Name,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_SName,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Shortcode,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_PinYinZiTou,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Comment,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_DataTimeStamp,MEPTSampleDeliveryConditon_Id,MEPTSampleDeliveryConditon_DataTimeStamp',
    //                                "/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleDeliveryConditonByHQL?isPlanish=true&fields=MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo,MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryDate,MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryMan,MEPTSampleDeliveryConditon_MEPTSampleDelivery_PrintCount,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_CName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_EName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_ShortCode,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_LinkMan,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_PhoneNum1,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_CName,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleDelivery_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCode,MEPTSampleDeliveryConditon_MEPTSampleForm_IsSpiltItem,MEPTSampleDeliveryConditon_MEPTSampleForm_IsSendOut,MEPTSampleDeliveryConditon_MEPTSampleForm_SendOutLabID,MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCap,MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCount,MEPTSampleDeliveryConditon_MEPTSampleForm_CollectPart,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeOpTime,MEPTSampleDeliveryConditon_MEPTSampleForm_SerialScanTime,MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeSource,MEPTSampleDeliveryConditon_MEPTSampleForm_PrintCount,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_SerialNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_OldSerialNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatCardNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_InpatientNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatientID,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatNo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_CName,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Birthday,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Age,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Bed,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DoctorID,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Doctor,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Diag,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_IsFree,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Charge,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_FormMemo,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_TestAim,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Name,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_SName,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Shortcode,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_PinYinZiTou,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Comment,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleForm_Id,MEPTSampleDeliveryConditon_MEPTSampleForm_DataTimeStamp,MEPTSampleDeliveryConditon_Id,MEPTSampleDeliveryConditon_DataTimeStamp";

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
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
        var me = this;
        me.callParent(arguments);
        if(Ext.typeOf(me.callback)==='function'){me.callback(me);}
        me.defaultLoad && me.load(true);
    },
   /**默认hql*/
    defaultWhere:'',
    /**内部hql*/
    internalWhere:'',
    /**外部hql*/
    externalWhere:'',
    /***
     * 外部动态field
     * @type String
     */
    fieldsExternal:[],
    /***
     * 外部动态传入的列
     * @type String
     */
    columnsExternal:[],
    /***
     * 是否执行代码
     * @type Boolean
     */
    hasExecute:true,
    /**
     * 根据where条件加载数据
     * @public
     * @param {} where
     */
    load:function(where){
        var me = this;
        if(where !== true){
            me.externalWhere = where;
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
            fields:me.fields,
            remoteSort:config.remoteSort,
            sorters:config.sorters,
            pageSize:config.PageSize,
            proxy:{
                type:'ajax',
                url:me.url,
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
            }
       };
        var store = Ext.create('Ext.data.Store',cfg);
        return store;
    },
    /**
     * 数据转化
     * @private
     * @param {} response
     * @return {}
     */
    changeData:function(response,bo){
        var me = this;
        
        var data = Ext.JSON.decode(response.responseText);
        var success = (data.success + "" == "true" ? true : false);
        if(!success){
            me.showError(data.ErrorInfo);
        }
        if(data.ResultDataValue && data.ResultDataValue != ''){
            data.ResultDataValue =data.ResultDataValue.replace(/[\r\n]+/g,"");
            var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
            var arrLists=[];
            arrLists=ResultDataValue.list;
            var count=ResultDataValue.count;
            var date='',qCDataLotNo='';index=0;//上一行的日期,批次
            var dateCurrent='',qCDataLotNoCurrent='';
            var deleteArr=[];
            //当日期相同并批次相同时需要合并成一行
            for(var i=0;i<arrLists.length;i++){
                var qcmatCName=arrLists[i]["QCDValue_QCItem_QCMat_CName"];
                var reportValue=arrLists[i]["QCDValue_ReportValue"];
                dateCurrent=Ext.util.Format.date(arrLists[i]["QCDValue_ReceiveTime"],'Y-m-d').toString();
                qCDataLotNoCurrent=arrLists[i]["QCDValue_QCDataLotNo"];
                if(dateCurrent==date&&qCDataLotNo==qCDataLotNoCurrent){
                    arrLists[index][qcmatCName]=reportValue;
                    deleteArr.push(i);
                    count=count-1;
                }else{
                    for(var j=0;j<me.fieldsExternal.length;j++){
                        if(me.fieldsExternal[j]==qcmatCName){
                            index=i;
                            arrLists[i][qcmatCName]=reportValue;
                            date=dateCurrent;
                            qCDataLotNo=qCDataLotNoCurrent;
                        }
                    }
                }
            }
            //删除
            var counts2=0;
            for(var k=0;k<deleteArr.length;k++){
                if(k>0){
                    arrLists.splice(deleteArr[k]-counts2,1);
                }else{
                    arrLists.splice(deleteArr[k],1);
                }
                counts2=counts2+1;
            }
            data.list =arrLists;
            data.count = count;
        }else{
            data.list = [];
            data.count = 0;
        }
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
    /***
     * 内部fields
     * @type 
     */
    fields:[
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryDate',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryMan',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_PrintCount',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_CName',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_EName',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_ShortCode',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_LinkMan',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_PhoneNum1',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_DataTimeStamp',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_CName',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_DataTimeStamp',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DataTimeStamp',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCode',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_IsSpiltItem',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_IsSendOut',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_SendOutLabID',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCap',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCount',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_CollectPart',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeOpTime',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_SerialScanTime',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeSource',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_PrintCount',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_SerialNo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_OldSerialNo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatCardNo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_InpatientNo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatientID',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatNo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_CName',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Birthday',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Age',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Bed',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DoctorID',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Doctor',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Diag',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_IsFree',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Charge',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_FormMemo',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_TestAim',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Name',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_SName',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Shortcode',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_PinYinZiTou',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Comment',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_DataTimeStamp',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DataTimeStamp',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_Id',
         'MEPTSampleDeliveryConditon_MEPTSampleForm_DataTimeStamp',
         'MEPTSampleDeliveryConditon_Id', 'MEPTSampleDeliveryConditon_DataTimeStamp'],
    columns:[{
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        }, {
            text:'外送清单编号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'外送日期',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryDate',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'外送人或交接人',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryMan',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'打印次数',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_PrintCount',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'名称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_CName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'英文名称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_EName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'简称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_ShortCode',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'联系人',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_LinkMan',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'联系电话1',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_PhoneNum1',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_Id',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryLab_DataTimeStamp',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'部门名称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_CName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleDelivery_DataTimeStamp',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'条码号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCode',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'是否拆分项目',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_IsSpiltItem',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'是否外送样本',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_IsSendOut',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'外送实验室',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_SendOutLabID',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'采样量',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCap',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'采样次数',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_SampleCount',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'采样部位',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_CollectPart',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'条码生成时间',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeOpTime',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'条码扫描时间',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_SerialScanTime',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'条码来源',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_BarCodeSource',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'打印次数',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_PrintCount',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'申请单号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_SerialNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'原始申请单号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_OldSerialNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'就诊卡号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatCardNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'住院号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_InpatientNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'病人ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatientID',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'病历号',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_PatNo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'姓名',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_CName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'生日',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Birthday',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'年龄',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Age',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'病床',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Bed',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'医生ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DoctorID',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'医生',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Doctor',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'临床诊断',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Diag',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'是否免费',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_IsFree',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'费用',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Charge',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'备注',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_FormMemo',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'送检目的',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_TestAim',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'名称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Name',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'简称',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_SName',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'快捷码',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Shortcode',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'汉字拼音字头',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_PinYinZiTou',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'备注',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Comment',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_BSpecialty_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_MEPTOrderForm_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_MEPTSampleForm_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'MEPTSampleDeliveryConditon_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'MEPTSampleDeliveryConditon_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }],
   viewConfig:{
        //行背景色显示
        enableTextSelection: 'true',
        getRowClass : function(record, rowIndex, rowParams, store){
            var qcMatCName=record.get('MEPTSampleDeliveryConditon_MEPTSampleDelivery_DeliveryDept_CName');
            var qcdValueTotal=rowIndex;
            var cssCalss='';
            var cssCalssArr=[
               "x-grid-record-lightSkyBlue",
               "x-grid-record-lightGreen",
               "x-grid-record-lightBLue",
               "x-grid-record-beige",
               "x-grid-record-dimGray",
               "x-grid-record-lavender",
               "x-grid-record-lightYellow",
               "x-grid-record-plum"
           ];
            cssCalss=record.get("cssCalssRow");
            if(cssCalss==""){
                cssCalss=cssCalssArr[rowIndex];
            }
            return cssCalss;
          }
    },
    createFields:function() {
        var me=this;
        if(me.fieldsExternal.length>0){
            me.fields.concat(me.fieldsExternal);
        }
    },
    initComponent : function() {
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        Ext.Loader.setPath('Ext.zhifangux.DateIntervals', getRootPath() + '/ui/zhifangux/DateIntervals.js');
        Ext.Loader.setPath('Ext.zhifangux.DateField', getRootPath() + '/ui/zhifangux/DateField.js');
        me.searchArray = [];
        if(me.hasExecute==true){
            me.store=me.createStore({
                remoteSort : true,
                sorters : ['MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo'],
                PageSize : 3000,
                hasCountToolbar : true,
                buffered : false,
                leadingBufferZone : null
            });
        }
        var dateintervals=Ext.create('Ext.zhifangux.DateIntervals',
            {
                editable : true,
                name : 'dtDateintervals',
                itemId : 'dtDateintervals',
                fieldLabel : '查询日期',
                fieldLabelTwo : '至 ',
                labelSeparator:'',
                width : 210,
                layoutType : 'hbox',
                value : new Date(),
                valueTwo : new Date(),
                bodyCls:'',
                cls:'',
                hidden : false,
                dateFormat : 'Y-m-d'
        });
        var topTtems=[
            dateintervals,'　',{
                type : 'refresh',
                itemId:'refresh',
                text:'刷新',
                iconCls:'build-button-refresh'
            }
        ];
        me.dockedItems = [/*{
                    xtype : 'toolbar',
                    dock : 'top',
                    itemId : 'toolbar-top',
                    items :topTtems
                },*/{
            xtype : 'toolbar',
            dock : 'bottom',
            itemId : 'toolbar-bottom',
            items : [{
                        xtype : 'label',
                        itemId : 'count',
                        text : '共0条'
                    }]
        }];
         
        this.callParent(arguments);
    }
});