/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-5-24
 * Time: 上午10:02
 * To change this template use File | Settings | File Templates.
 */
Ext.define('SampleDefine',{
    extend:'Ext.panel.Panel',
    alias:'widget.sampledefine',
    frame:true,
    objectRoot:'list',
    layout:'border',
    //采样管类型
    SampleTubeTypeDataURL:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingTubeByHQL?fields=MEPTSamplingTube_CName,MEPTSamplingTube_Id&where=1=1&isPlanish=true",
    //采样管容量的单位
    SampleTubeCapacityData:null,
    SampleTubeCapacityURL:'server/GetSampleTubeCapacity.txt',
    //是否贴管
    SampleIsTubeData:null,
    SampleIsTubeURL:'server/GetISSampleTube.txt',
    //样本类型
    SampleTypeURL:getRootPath()+"/SingleTableService.svc/ST_UDTO_SearchBSampleTypeByHQL?fields=BSampleType_Name,BSampleType_Id&where=1=1&isPlanish=true",
    //专业
    SampleSpecialtyURL:getRootPath()+"/SingleTableService.svc/ST_UDTO_SearchBSpecialtyByHQL?fields=BSpecialty_Name,BSpecialty_Id&where=1=1&isPlanish=true",
    //收费方式
    ChargeTypeURL:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTBChargeTypeByHQL?fields=MEPTBChargeType_Name,MEPTBChargeType_Id&where=1=1&isPlanish=true",
    GridListServerUrl:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingGroupByHQL?fields=MEPTSamplingGroup_Id,MEPTSamplingGroup_CName,MEPTSamplingGroup_SName,MEPTSamplingGroup_Destination,MEPTSamplingGroup_BSpecialty_Name,MEPTSamplingGroup_MEPTSamplingTube_CName,MEPTSamplingGroup_BSampleType_Name&where=1=1&isPlanish=true",
    SampleGroupFields:["MEPTSamplingGroup_Id","MEPTSamplingGroup_CName","MEPTSamplingGroup_SName","MEPTSamplingGroup_Destination","MEPTSamplingGroup_BSpecialty_Name","MEPTSamplingGroup_MEPTSamplingTube_CName","MEPTSamplingGroup_BSampleType_Name"],
    URL:'',
    OperateFlag:0,
    MEPTSamplingGroupId:null,
    initComponent:function() {
        var me = this;
        //获取采样管容量的单位
        me.getSampleTubeCapacityData();
        //获取采样管是否贴管
        me.getIsSampleTubeData();
        //初始化视图
        me.initView();
        /*Ext.getCmp('gridSample').getSelectionModel().select(0);*/
        me.callParent(arguments);
    },
    /**
     *获取采样管类型
     */
    getSampleTubeTypeData:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:['MEPTSamplingTube_CName','MEPTSamplingTube_Id'],
            proxy: {
                type: 'ajax',
                url: me.SampleTubeTypeDataURL,
                reader: {
                    type: 'json',
                    root: 'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;

                }
            },
            autoLoad: true
        });

        return myStore;
    },
    /**
     *获取采样管容量的单位
     */
    getSampleTubeCapacityData:function(){
        var me = this;
        var myGridStore=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.SampleTubeCapacityURL,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    myGridStore=Ext.create('Ext.data.Store', {
                        fields:['Capacity'],
                        data:result,
                        proxy: {
                            type: 'memory',
                            reader: {
                                type: 'json',
                                root:me.objectRoot
                            }
                        }
                    });
                }else{
                    Ext.Msg.alert('提示','获取信息失败222！');
                }
            },
            failure : function(response,options){
                Ext.Msg.alert('提示','获取信息请求失败4444！'+response.responseText);
            }
        });
        me.SampleTubeCapacityData= myGridStore;
    },
    /**
     *获取采样管是否贴管
     */
    getIsSampleTubeData:function(){
        var me = this;
        var myGridStore=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.SampleIsTubeURL,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    myGridStore=Ext.create('Ext.data.Store', {
                        fields:['name','value'],
                        data:result,
                        proxy: {
                            type: 'memory',
                            reader: {
                                type: 'json',
                                root:me.objectRoot
                            }
                        }
                    });
                }else{
                    Ext.Msg.alert('提示','获取信息失败222！');
                }
            },
            failure : function(response,options){
                Ext.Msg.alert('提示','获取信息请求失败4444！'+response.responseText);
            }
        });
        me.SampleIsTubeData= myGridStore;
    },
    /**
     *获取样本类型
     */
    getSampleType:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:['BSampleType_Name','BSampleType_Id'],
            proxy: {
                type: 'ajax',
                url: me.SampleTypeURL,
                reader: {
                    type: 'json',
                    root: 'ResultDataValue.list'
                },
                extractResponseData:function(response){
                     var data = Ext.JSON.decode(response.responseText);
                     var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                     data.ResultDataValue = ResultDataValue;
                     response.responseText = Ext.JSON.encode(data);
                    return response;
	   
                }
            },
        autoLoad: true
        });
        return myStore;
    },
    /**
     *获取专业
     */
    getSampleSpecialty:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:['BSpecialty_Name','BSpecialty_Id'],
            proxy: {
                type: 'ajax',
                url: me.SampleSpecialtyURL,
                reader: {
                    type: 'json',
                    root: 'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;

                }
            }
        });
        return myStore;
    },
    /**
     *获取收费方式
     */
    getChargeType:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:['MEPTBChargeType_Name','MEPTBChargeType_Id'],
            proxy: {
                type: 'ajax',
                url: me.ChargeTypeURL,
                reader: {
                    type: 'json',
                    root: 'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;

                }
            },
            autoLoad: true
        });
        return myStore;
    },
    initView:function(){
        var me=this;
        me.items=[{
            region:'west',
            width:'54%',
            xtype:'grid',
            forceFit:'true',
            id:'gridSample',
            title:'采样组列表',
            margins: '0 0 0 0',
            split:true,
            layout: 'fit',
            columns:[
                {text:'采样组名称',dataIndex:'MEPTSamplingGroup_CName'},
                {text:'采样组简称',dataIndex:'MEPTSamplingGroup_SName'},
                {text:'采样管类型',dataIndex:'MEPTSamplingGroup_MEPTSamplingTube_CName'},
                {text:'送检目的地',dataIndex:'MEPTSamplingGroup_Destination'},
                {text:'样本类型',dataIndex:'MEPTSamplingGroup_BSampleType_Name'},
                {text:'专业',dataIndex:'MEPTSamplingGroup_BSpecialty_Name'},
                {text:'操作', xtype:'actioncolumn', align:"center",
                items: [{
                    iconCls:'build-button-add hand',
                    tooltip: '增加',
                    handler:function(){
                        Ext.getCmp('txtGroupCname').setValue("");
                        Ext.getCmp('txtGroupSname').setValue("");
                        Ext.getCmp('txtSamplingTubeCName').setValue("");
                        Ext.getCmp('txtSamplingDestination').setValue("");
                        Ext.getCmp('txtDaiMa').setValue("");
                        Ext.getCmp('txtSampleType').setValue("");
                        Ext.getCmp('txtSpecialty').setValue("");
                        Ext.getCmp('txtDispOrder').setValue("");
                        Ext.getCmp('txtGroupCname').focus(true);
                        me.URL=getRootPath()+"/MEPTService.svc/MEPT_UDTO_AddMEPTSamplingGroup";
                    }
                },{
                    iconCls:'build-button-edit hand',
                    tooltip: '修改',
                    handler:function(view,rowIndex,colIndex,item,e,record){
                        Ext.getCmp('txtGroupCname').focus(true);
                        me.URL=getRootPath()+"/MEPTService.svc/MEPT_UDTO_UpdateMEPTSamplingGroup";
                        me.MEPTSamplingGroupId=record.data.MEPTSamplingGroup_Id;
                        alert(record);
                        me.OperateFlag=1
                    }
                },{
                    iconCls:'build-button-delete hand',
                    tooltip:'删除',
                    handler:function(view,rowIndex,colIndex,item,e,record){
                        Ext.Msg.confirm("警告","确定要删除吗？",function (button){
                            if(button == "yes"){
                                me.deleteSampleGroup(record);
                            }
                        })
                    }
                }]
                }
            ],
            store:me.GetGridListData(),
            listeners:{
                selectionchange: function(model, records) {
                    if (records[0]) {
                        Ext.getCmp('fff').getForm().loadRecord(records[0]);
                        var SamplingGroupID=records[0].data.MEPTSamplingGroup_Id;
                        me.searchSampleGroup(SamplingGroupID);
                    }
                }
            },
            dockedItems:[{
                xtype:'pagingtoolbar',
                id:'pageItem',
                pageSize: 50,
                store:me.GetGridListData(),   // same store GridPanel is using
                dock: 'bottom',
                displayInfo:'true'
            }]
        },{
            region:'center',
            xtype:"form",
            id:'fff',
            title:'详细信息',
            layout:'absolute',
            items:[{
                x:5,
                y:30,
                xtype:'textfield',
                id:'txtGroupCname',
                name:'MEPTSamplingGroup_CName',
                fieldLabel: '采样组名称',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:5,
                y:70,
                xtype:'textfield',
                name:'MEPTSamplingGroup_SName',
                labelAlign:"right",
                id:'txtGroupSname',
                fieldLabel: '采样组简称',
                labelWidth:70,
                width:220
            },{
                x:5,
                y:110,
                xtype:'combobox',
                queryMode: 'local',
                labelAlign:"right",
                id:'txtSamplingTubeCName',
                name:'MEPTSamplingGroup_MEPTSamplingTube_CName',
                fieldLabel: '采样管类型',
                store: me.getSampleTubeTypeData(),
                valueField:'MEPTSamplingTube_Id',
                displayField:'MEPTSamplingTube_CName',
                labelWidth:70,
                width:220,
                listeners:{
                    select:function(combor,ecords ){
                        var url=getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingTubeByHQL?" +
                            "fields=MEPTSamplingTube_Capacity,MEPTSamplingTube_Unit,MEPTSamplingTube_MinCapacity," +
                            "MEPTSamplingTube_MEPTBCharge_ChargeTypeID&where=SamplingTubeID="+ecords[0].data.MEPTSamplingTube_Id+"&isPlanish=true";
                        Ext.Ajax.request({
                            async:false,//非异步
                            url:url,
                            method:'GET',
                            timeout:2000,
                            success:function(response,opts){
                                var result = Ext.JSON.decode(response.responseText);
                                if(result.success){
                                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                                    Ext.getCmp('txtSamplingTubeMinCapacity').setValue(ResultDataValue.list[0].MEPTSamplingTube_MinCapacity);
                                    Ext.getCmp('txtChargeType').setValue(ResultDataValue.list[0].MEPTSamplingTube_MEPTBCharge_ChargeTypeID);
                                    Ext.getCmp('txtSamplingTubeCapacity').setValue(ResultDataValue.list[0].MEPTSamplingTube_Capacity);
                                    Ext.getCmp('txtUitl').setValue(ResultDataValue.list[0].MEPTSamplingTube_Unit);
                                }else{

                                }
                            },
                            failure:function(response,options){
                                Ext.Msg.alert('提示','调用服务失败！'+response.responseText);
                            }
                        });
                        }
                    }
            },{
                x:5,
                y:150,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtSamplingDestination',
                name:'MEPTSamplingGroup_Destination',
                fieldLabel: '送检目的地',
                labelWidth:70,
                width:220
            },{
                x:5,
                y:190,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtSamplingTubeCapacity',
                readOnly:true,
                name:'MEPTSamplingGroup_MEPTSamplingTube_Capacity',
                fieldLabel: '采样管容量',
                labelWidth:70,
                width:160
            },{
                x:155,
                y:190,
                xtype:'combobox',
                store: me.SampleTubeCapacityData,
                id:'txtUitl',
                valueField:'Capacity',
                displayField:'Capacity',
                width:70
            },{
                x:5,
                y:230,
                xtype:'textfield',
                fieldLabel: '最少标本量',
                readOnly:true,
                id:'txtSamplingTubeMinCapacity',
                name:'MEPTSamplingGroup_MEPTSamplingTube_MinCapacity',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:5,
                y:270,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtSamplingGroupPrinterChannel',
                readOnly:true,
                name:'MEPTSamplingGroup_MEPTSamplingGroupPrint_PrinterChannelCode1',
                fieldLabel: '打包机通道',
                labelWidth:70,
                width:220
            },{
                x:5,
                y:310,
                xtype:'numberfield',
                fieldLabel: '条码份数',
                readOnly:true,
                id:'txtSamplingGroupPrintNum',
                name:'MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum',
                labelAlign:"right",
                labelWidth:70,
                value: 0,
                minValue: 0,
                width:220
            },{
                x:5,
                y:350,
                xtype:'combobox',
                fieldLabel: '是否贴管',
                readOnly:true,
                labelAlign:"right",
                id:'txtIsPasteTube',
                store: me.SampleIsTubeData,
                valueField:'value',
                displayField:'value',
                labelWidth:70,
                width:220
            },{
                x:220,
                y:30,
                xtype:'combobox',
                fieldLabel: '样本类型',
                labelAlign:"right",
                id:'txtSampleType',
                name:'MEPTSamplingGroup_BSampleType_Name',
                store: me.getSampleType(),
                valueField:'BSampleType_Id',
                displayField:'BSampleType_Name',
                labelWidth:70,
                width:220
            },{
                x:220,
                y:70,
                xtype:'textfield',
                fieldLabel: '代码',
                id:'txtDaiMa',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:220,
                y:110,
                xtype:'combobox',
                fieldLabel: '专业',
                labelAlign:"right",
                id:'txtSpecialty',
                name:'MEPTSamplingGroup_BSpecialty_Name',
                store: me.getSampleSpecialty(),
                valueField:'BSpecialty_Id',
                displayField:'BSpecialty_Name',
                labelWidth:70,
                width:220
            },{
                x:220,
                y:150,
                xtype:'numberfield',
                fieldLabel: '显示次序',
                id:'txtDispOrder',
                labelAlign:"right",
                name:'MEPTSamplingGroup_BSpecialty_Name',
                labelWidth:70,
                value: 0,
                minValue: 0,
                width:220
            },{
                x:220,
                y:190,
                xtype:'combobox',
                fieldLabel: '收费方式',
                labelAlign:"right",
                readOnly:true,
                store: me.getChargeType(),
                id:'txtChargeType',
                name:'MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_ChargeTypeID',
                valueField:'MEPTBChargeType_Id',
                displayField:'MEPTBChargeType_Name',
                labelWidth:70,
                width:220
            },{
                x:220,
                y:230,
                xtype:'combobox',
                fieldLabel: '收费1',
                labelAlign:"right",
                readOnly:true,
                labelWidth:70,
                width:220
            },{
                x:220,
                y:270,
                xtype:'combobox',
                fieldLabel:'收费2',
                readOnly:true,
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:220,
                y:310,
                xtype:'combobox',
                fieldLabel: '收费3',
                readOnly:true,
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:300,
                y:400,
                xtype:'button',
                icon:'image/updated.png',
                iconAlign:'left',
                text:'确定',
                handler:function(){
                    var GroupCname=Ext.getCmp('txtGroupCname').value;
                    var SamplingGroSName=Ext.getCmp('txtGroupSname').value;
                    var Destination=Ext.getCmp('txtSamplingDestination').value;
                    var SamplingTubeID=Ext.getCmp('txtSamplingTubeCName').value;
                    var SampleType=Ext.getCmp('txtSampleType').value;
                    var ShortCode=Ext.getCmp('txtDaiMa').value;
                    var Specialty=Ext.getCmp('txtSpecialty').value;
                    var SamplingDispOrder=Ext.getCmp('txtDispOrder').value;
                   var MEPTSamplingGroup={
                        BSampleType:{
                            Id:SampleType,
                            DataTimeStamp:''
                        },
                        BSpecialty:{
                            Id:Specialty,
                            DataTimeStamp:''
                        },
                        CName:GroupCname,
                        MEPTSamplingTube:{
                            Id:SamplingTubeID,
                            DataTimeStamp:''
                        },
                        SName:SamplingGroSName,
                        Shortcode:ShortCode,
                        DispOrder:SamplingDispOrder,
                        Destination:Destination
                    };
                    alert(me.OperateFlag);
                    if(me.OperateFlag==1)
                    {
                        MEPTSamplingGroup.add("DataTimeStamp:''","SamplingGroupID:"+me.MEPTSamplingGroupId)
                    }
                    Ext.Ajax.defaultPostHeader = 'application/json';
                    Ext.Ajax.request({
                        async:false,//非异步
                        url:me.URL,
                        params:Ext.JSON.encode({entity:MEPTSamplingGroup}),
                        method:'POST',
                        timeout:2000,
                        success:function(response,opts){
                            var result = Ext.JSON.decode(response.responseText);
                            if(result.success){
                                //me.appId = result.id;
                                Ext.Msg.alert('提示','操作成功！');
                                Ext.getCmp('gridSample').store.load();
                                Ext.getCmp('pageItem').store.load();
                            }
                            else{
                                Ext.Msg.alert('提示','操作采样组失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                            }
                        },
                        failure : function(response,options){
                            Ext.Msg.alert('提示','服务请求失败！');
                        }
                    });
                }
            }]
        }]
    },
    /**
     *删除采样组设置
     */
    deleteSampleGroup:function(record){
        var me = this;
        var url =getRootPath()+"/MEPTService.svc/MEPT_UDTO_DelMEPTSamplingGroup?Id=" + record.data.MEPTSamplingGroup_Id;
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    Ext.Msg.alert('提示','删除成功！');
                    Ext.getCmp('gridSample').store.load();
                    Ext.getCmp('pageItem').store.load();

                }else{
                    Ext.Msg.alert('提示','删除失败！'+response.responseText);
                }
            },
            failure:function(response,options){
                Ext.Msg.alert('提示','删除失败！'+response.responseText);
            }
        });
    },
    searchSampleGroup:function(id){
        var me = this;
        var url =getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingGroupByHQL?" +
            "fields=MEPTSamplingGroup_Shortcode,MEPTSamplingGroup_MEPTSamplingTube_Capacity,MEPTSamplingGroup_MEPTSamplingTube_Unit,MEPTSamplingGroup_MEPTSamplingTube_MinCapacity," +
            "MEPTSamplingGroup_MEPTSamplingGroupPrint_PrinterChannelCode1,MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum," +
            "MEPTSamplingGroup_MEPTSamplingGroupPrint_IsPasteTube,MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_ChargeTypeID," +
            "MEPTSamplingGroup_DispOrder&where=SamplingGroupID="+id+"&isPlanish=true";
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    Ext.getCmp('txtSamplingTubeMinCapacity').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingTube_MinCapacity);
                    Ext.getCmp('txtChargeType').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingTube_MEPTBCharge_ChargeTypeID);
                    Ext.getCmp('txtDispOrder').setValue(ResultDataValue.list[0].MEPTSamplingGroup_DispOrder);
                    Ext.getCmp('txtDaiMa').setValue(ResultDataValue.list[0].MEPTSamplingGroup_Shortcode);
                    Ext.getCmp('txtIsPasteTube').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingGroupPrint_IsPasteTube);
                    Ext.getCmp('txtSamplingGroupPrintNum').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingGroupPrint_PrintNum);
                    Ext.getCmp('txtSamplingGroupPrinterChannel').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingGroupPrint_PrinterChannelCode1);
                    Ext.getCmp('txtSamplingTubeCapacity').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingTube_Capacity);
                    Ext.getCmp('txtUitl').setValue(ResultDataValue.list[0].MEPTSamplingGroup_MEPTSamplingTube_Unit);
                }else{

                }
            },
            failure:function(response,options){
                Ext.Msg.alert('提示','调用服务失败！'+response.responseText);
            }
        });
    },
    /**
     *绑定GridPanel数据
     */
    GetGridListData:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:me.SampleGroupFields,
            pageSize:50,
            proxy: {
                type: 'ajax',
                url: me.GridListServerUrl,
                reader: {
                    type: 'json',
                    root:  'ResultDataValue.list'
                },
                extractResponseData:function(response){
                    var data = Ext.JSON.decode(response.responseText);
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                    response.responseText = Ext.JSON.encode(data);
                    return response;
                }
            },
            autoLoad: true
        });
        return myStore;
    }
});

