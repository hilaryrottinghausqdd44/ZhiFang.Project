/***
 * 未外送样本清单Tabpanel页
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliverModule.unSendSampleList', {
    extend:'Ext.zhifangux.GridPanel',
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
    initComponent:function() {
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        Ext.Loader.setPath('Ext.zhifangux.DateIntervals', getRootPath() + '/ui/zhifangux/DateIntervals.js');
        Ext.Loader.setPath('Ext.zhifangux.DateField', getRootPath() + '/ui/zhifangux/DateField.js');
        me.url = getRootPath() + '/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleFormByHQL?isPlanish=true&fields=checkSelect,MEPTSampleForm_BarCode,MEPTSampleForm_IsSendOut,' +   //MEPTSampleForm_SendOutLabID,MEPTSampleForm_IsSpiltItem,
                'MEPTSampleForm_MEPTOrderForm_Client_CName,MEPTSampleForm_MEPTOrderForm_Client_Id,MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp,'+
                'MEPTSampleForm_MEPTOrderForm_HRDept_CName,MEPTSampleForm_MEPTOrderForm_HRDept_Id,MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp,'+
                'MEPTSampleForm_MEPTOrderForm_BSickType_Name,MEPTSampleForm_MEPTOrderForm_BSickType_Id,MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp,'+
                'MEPTSampleForm_BarCodeOpTime,MEPTSampleForm_SerialScanTime,' +
                //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Name,MEPTSampleForm_BSampleStatus_BSampleStatusType_Id,MEPTSampleForm_BSampleStatus_BSampleStatusType_DataTimeStamp,' +
                //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Color,MEPTSampleForm_BSampleStatus_BSampleStatusType_SName,MEPTSampleForm_BSampleStatus_BSampleStatusType_IsUse,MEPTSampleForm_MEPTOrderForm_IsFree,' +
                //MEPTSampleForm_BSampleType_SName,MEPTSampleForm_BSampleType_IsUse,
                'MEPTSampleForm_BSampleStatus_Id,MEPTSampleForm_BSampleStatus_DataTimeStamp,' +
                'MEPTSampleForm_BSampleType_Name,MEPTSampleForm_BSampleType_Id,MEPTSampleForm_BSampleType_DataTimeStamp,MEPTSampleForm_MEPTSamplingGroup_CName,' +
                'MEPTSampleForm_MEPTSamplingGroup_Destination,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_CName,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum,' +
                ',MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_Id,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_DataTimeStamp,MEPTSampleForm_MEPTOrderForm_SerialNo,MEPTSampleForm_MEPTOrderForm_OldSerialNo,' +
                'MEPTSampleForm_MEPTOrderForm_PatCardNo,MEPTSampleForm_MEPTOrderForm_InpatientNo,MEPTSampleForm_MEPTOrderForm_PatientID,MEPTSampleForm_MEPTOrderForm_PatNo,MEPTSampleForm_MEPTOrderForm_CName,MEPTSampleForm_MEPTOrderForm_Birthday,MEPTSampleForm_MEPTOrderForm_Age,' +
                'MEPTSampleForm_MEPTOrderForm_Bed,MEPTSampleForm_MEPTOrderForm_DoctorID,MEPTSampleForm_MEPTOrderForm_Doctor,MEPTSampleForm_MEPTOrderForm_Diag,MEPTSampleForm_MEPTOrderForm_Charge,' +
                //'MEPTSampleForm_MEPTOrderForm_CollectPart,MEPTSampleForm_MEPTOrderForm_TestAim,' +
                'MEPTSampleForm_MEPTOrderForm_Id,MEPTSampleForm_MEPTOrderForm_DataTimeStamp,MEPTSampleForm_Id,MEPTSampleForm_DataTimeStamp';
        me.searchArray = [];
        me.store = me.createStore({
            fields:['checkSelect','MEPTSampleForm_BarCode', 'MEPTSampleForm_IsSendOut', //'MEPTSampleForm_SendOutLabID','MEPTSampleForm_IsSpiltItem',
                   'MEPTSampleForm_MEPTOrderForm_Client_CName','MEPTSampleForm_MEPTOrderForm_Client_Id','MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',
                   'MEPTSampleForm_MEPTOrderForm_HRDept_CName','MEPTSampleForm_MEPTOrderForm_HRDept_Id','MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp',
                   'MEPTSampleForm_MEPTOrderForm_BSickType_Name','MEPTSampleForm_MEPTOrderForm_BSickType_Id','MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp',
                   'MEPTSampleForm_BarCodeOpTime', 'MEPTSampleForm_SerialScanTime',
                   //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Name','MEPTSampleForm_BSampleStatus_BSampleStatusType_Id', 'MEPTSampleForm_BSampleStatus_BSampleStatusType_DataTimeStamp', 
                   //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Color,MEPTSampleForm_BSampleStatus_BSampleStatusType_SName', 'MEPTSampleForm_BSampleStatus_BSampleStatusType_IsUse','MEPTSampleForm_MEPTOrderForm_IsFree',
                   'MEPTSampleForm_BSampleStatus_Id', 'MEPTSampleForm_BSampleStatus_DataTimeStamp','MEPTSampleForm_BSampleType_Name', 
                   //'MEPTSampleForm_BSampleType_SName', 'MEPTSampleForm_BSampleType_IsUse',
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
                   //'MEPTSampleForm_MEPTOrderForm_CollectPart','MEPTSampleForm_MEPTOrderForm_TestAim',
                   'MEPTSampleForm_MEPTOrderForm_Id','MEPTSampleForm_MEPTOrderForm_DataTimeStamp', 'MEPTSampleForm_Id', 'MEPTSampleForm_DataTimeStamp' ],
            url:'MEPTService.svc/MEPT_UDTO_SearchMEPTSampleFormByHQL?isPlanish=true&fields=checkSelect,MEPTSampleForm_BarCode,MEPTSampleForm_IsSendOut,' +  //MEPTSampleForm_IsSpiltItem,MEPTSampleForm_SendOutLabID,
                'MEPTSampleForm_MEPTOrderForm_Client_CName,MEPTSampleForm_MEPTOrderForm_Client_Id,MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp,' +
                'MEPTSampleForm_MEPTOrderForm_HRDept_CName,MEPTSampleForm_MEPTOrderForm_HRDept_Id,MEPTSampleForm_MEPTOrderForm_HRDept_DataTimeStamp,'+
                'MEPTSampleForm_MEPTOrderForm_BSickType_Name,MEPTSampleForm_MEPTOrderForm_BSickType_Id,MEPTSampleForm_MEPTOrderForm_BSickType_DataTimeStamp,'+
                'MEPTSampleForm_BarCodeOpTime,MEPTSampleForm_SerialScanTime,' +
                //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Color,MEPTSampleForm_BSampleStatus_BSampleStatusType_SName,MEPTSampleForm_BSampleStatus_BSampleStatusType_IsUse,MEPTSampleForm_MEPTOrderForm_IsFree,' +
                //'MEPTSampleForm_BSampleStatus_BSampleStatusType_Name,MEPTSampleForm_BSampleStatus_BSampleStatusType_Id,MEPTSampleForm_BSampleStatus_BSampleStatusType_DataTimeStamp,' +
                'MEPTSampleForm_BSampleStatus_Id,MEPTSampleForm_BSampleStatus_DataTimeStamp,' +
                'MEPTSampleForm_BSampleType_Name,MEPTSampleForm_BSampleType_Id,' +//MEPTSampleForm_BSampleType_SName,MEPTSampleForm_BSampleType_IsUse,
                'MEPTSampleForm_BSampleType_DataTimeStamp,MEPTSampleForm_MEPTSamplingGroup_CName,' +
                'MEPTSampleForm_MEPTSamplingGroup_Destination,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_CName,' +
                'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum,MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_Id,' +
                'MEPTSampleForm_MEPTSamplingGroup_MEPTSamplingGroupPrint_DataTimeStamp,MEPTSampleForm_MEPTOrderForm_SerialNo,MEPTSampleForm_MEPTOrderForm_OldSerialNo,' +
                'MEPTSampleForm_MEPTOrderForm_PatCardNo,MEPTSampleForm_MEPTOrderForm_InpatientNo,MEPTSampleForm_MEPTOrderForm_PatientID,' +
                'MEPTSampleForm_MEPTOrderForm_PatNo,MEPTSampleForm_MEPTOrderForm_CName,MEPTSampleForm_MEPTOrderForm_Birthday,' +
                'MEPTSampleForm_MEPTOrderForm_Age,MEPTSampleForm_MEPTOrderForm_Bed,MEPTSampleForm_MEPTOrderForm_DoctorID,' +
                'MEPTSampleForm_MEPTOrderForm_Doctor,MEPTSampleForm_MEPTOrderForm_Diag,MEPTSampleForm_MEPTOrderForm_Charge,' +
                //'MEPTSampleForm_MEPTOrderForm_CollectPart,MEPTSampleForm_MEPTOrderForm_TestAim,' +
                'MEPTSampleForm_MEPTOrderForm_Id,MEPTSampleForm_MEPTOrderForm_DataTimeStamp,' +
                'MEPTSampleForm_Id,MEPTSampleForm_DataTimeStamp',
            remoteSort:true,
            sorters:[],
            PageSize:25,
            hasCountToolbar:false,
            buffered:false,
            leadingBufferZone:null
        });
        me.defaultColumns = [ {
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        },{
            text : '选中',
            dataIndex : 'checkSelect',
            width : 50,
            editor:{
                xtype:'checkbox',
                cls:'x-grid-checkheader-editor'
            },
            align:'left',
            xtype:'checkcolumn'
         },{
            text:'委托单位',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_HRDept_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
            
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
            hideable:true,
            align:'left'
        }, {
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
            width:100,
            sortable:false,
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
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'病床',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Bed',
            width:100,
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
        }, /*{
            text:'是否免费',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_IsFree',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        },*/ {
            text:'费用',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Charge',
            width:100,
            sortable:false,
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
            text:'主键ID',
            dataIndex:'MEPTSampleForm_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'送检单位',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Client_CName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
            
        },{
            text:'送检单位ID',
            dataIndex:'MEPTSampleForm_MEPTOrderForm_Client_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
            
        },{
            text:'送检单位时间戳',
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
            text:'送检目的地',
            dataIndex:'MEPTSampleForm_MEPTSamplingGroup_Destination',
            width:100,
            sortable:false,
            hideable:true,
            align:'left'
        }, {
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
        me.columns = me.createColumns();
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
        var items = [/*{
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
                },'',
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
                text:'刷新',
                iconCls:'build-button-refresh'
            }];
        me.dockedItems = [{
                    xtype : 'toolbar',
                    dock : 'top',
                    itemId : 'toolbar-top',
                    items :items
                }, {
            xtype:'pagingtoolbar',
            store:me.store,
            dock:'bottom',
            displayInfo:true
        } ];
        me.deleteInfo = function(id, callback) {};
        this.callParent(arguments);
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
    }
});