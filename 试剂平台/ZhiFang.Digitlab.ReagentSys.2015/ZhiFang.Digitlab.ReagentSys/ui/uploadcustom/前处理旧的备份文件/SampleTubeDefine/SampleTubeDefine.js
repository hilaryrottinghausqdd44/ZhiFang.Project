/**
 * Created with JetBrains WebStorm.
 * User: 123
 * Date: 13-7-4
 * Time: 上午10:28
 * To change this template use File | Settings | File Templates.
 */
Ext.define('SampleTubeDefine',{
    extend:'Ext.panel.Panel',
    alias:'widget.sampletubedefine',
    frame:true,
    objectRoot:'list',
    layout:'border',
    ChargeURL:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTBChargeByHQL?fields=MEPTBCharge_CName,MEPTBCharge_Id&where=1=1&isPlanish=true",
    GridListServerUrl:getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingTubeByHQL?fields=MEPTSamplingTube_Id,MEPTSamplingTube_CName,MEPTSamplingTube_SName,MEPTSamplingTube_Capacity,MEPTSamplingTube_MinCapacity,MEPTSamplingTube_Comment&where=&isPlanish=true",
    SampleTubeFields:["MEPTSamplingTube_Id","MEPTSamplingTube_CName","MEPTSamplingTube_SName","MEPTSamplingTube_Capacity","MEPTSamplingTube_MinCapacity","MEPTSamplingTube_Comment"],
    URL:'',
    initComponent:function() {
        var me = this;
        //初始化视图
        me.initView();
        me.callParent(arguments);
    },
    /**
     *获取收费
     */
    getChargeType:function(){
        var me=this;
        var myStore = Ext.create('Ext.data.Store', {
            fields:['MEPTBCharge_CName','MEPTBCharge_Id'],
            proxy: {
                type: 'ajax',
                url: me.ChargeURL,
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
            width:'60%',
            xtype:'grid',
            forceFit:'true',
            id:'gridSampleTube',
            title:'采样管列表',
            margins: '0 0 0 0',
            split:true,
            layout: 'fit',
            columns:[
                {text:'采样管编号',dataIndex:'MEPTSamplingTube_Id'},
                {text:'采样管名称',dataIndex:'MEPTSamplingTube_CName'},
                {text:'采样管简称',dataIndex:'MEPTSamplingTube_SName'},
                {text:'容量',dataIndex:'MEPTSamplingTube_Capacity'},
                {text:'最小容量',dataIndex:'MEPTSamplingTube_MinCapacity'},
                {text:'说明',dataIndex:'MEPTSamplingTube_Comment'},
                {text:'操作', xtype:'actioncolumn', align:"center",
                    items: [{
                        iconCls:'build-button-add hand',
                        tooltip: '增加',
                        handler:function(){
                            Ext.getCmp('txtTubeId').setValue("");
                            Ext.getCmp('txtTubeCname').setValue("");
                            Ext.getCmp('txtTubeSName').setValue("");
                            Ext.getCmp('txtTubeMinCapacity').setValue("");
                            Ext.getCmp('txtSamplingTubeCapacity').setValue("");
                            Ext.getCmp('txtTubeColorName').setValue("");
                            Ext.getCmp('txtTubeColorValue').setValue("");
                            Ext.getCmp('txtTubeComment').setValue("");
                            Ext.getCmp('txtTubeUnit').setValue("");
                            Ext.getCmp('txtTubeId').focus(true);
                           /* me.URL="http://192.168.0.138/LabStarLIMS/MEPTService.svc/MEPT_UDTO_AddMEPTSamplingGroup";*/
                        }
                    },{
                        iconCls:'build-button-edit hand',
                        tooltip: '修改',
                        handler:function(){
                            Ext.getCmp('txtTubeId').focus(true);
                           /* me.URL="http://192.168.0.138/LabStarLIMS/MEPTService.svc/MEPT_UDTO_UpdateMEPTSamplingGroup";*/
                        }
                    },{
                        iconCls:'build-button-delete hand',
                        tooltip:'删除',
                        handler:function(view,rowIndex,colIndex,item,e,record){
                            Ext.Msg.confirm("警告","确定要删除吗？",function (button){
                                if(button == "yes"){
                                    me.deleteSampleTube(record);
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
                        var SamplingGroupID=records[0].data.MEPTSamplingTube_Id;
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
                x:65,
                y:30,
                xtype:'textfield',
                id:'txtTubeId',
                name:'MEPTSamplingTube_Id',
                fieldLabel: '采样管编号',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:65,
                y:70,
                xtype:'textfield',
                id:'txtTubeCname',
                name:'MEPTSamplingTube_CName',
                fieldLabel: '采样管名称',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:65,
                y:110,
                xtype:'textfield',
                id:'txtTubeSName',
                name:'MEPTSamplingTube_SName',
                fieldLabel: '采样管简称',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:65,
                y:150,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtTubeMinCapacity',
                name:'MEPTSamplingTube_MinCapacity',
                fieldLabel: '最小容量',
                labelWidth:70,
                width:220
            },{
                x:65,
                y:190,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtSamplingTubeCapacity',
                readOnly:true,
                name:'MEPTSamplingTube_Capacity',
                fieldLabel: '采样管容量',
                labelWidth:70,
                width:220
            },{
                x:65,
                y:230,
                xtype:'textfield',
                id:'txtTubeColorName',
                name:'MEPTSamplingTube_ColorName',
                fieldLabel: '颜色名称',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:65,
                y:270,
                xtype:'textfield',
                id:'txtTubeColorValue',
                name:'MEPTSamplingTube_ColorValue',
                fieldLabel: '颜色值',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:65,
                y:310,
                xtype:'textfield',
                labelAlign:"right",
                id:'txtTubeUnit',
                readOnly:true,
                name:'MEPTSamplingTube_Unit',
                fieldLabel: '容量单位',
                labelWidth:70,
                width:220
            },{
                x:65,
                y:350,
                xtype:'combobox',
                fieldLabel: '收费',
                labelAlign:"right",
                store: me.getChargeType(),
                id:'txtChargeType',
                name:'MEPTSamplingTube_MEPTBCharge_CName',
                valueField:'MEPTBCharge_Id',
                displayField:'MEPTBCharge_CName',
                labelWidth:70,
                width:220
            },{
                x:65,
                y:390,
                xtype:'textfield',
                name:'MEPTSamplingTube_Comment',
                fieldLabel: '说明',
                id:'txtTubeComment',
                labelAlign:"right",
                labelWidth:70,
                width:220
            },{
                x:250,
                y:430,
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
                            Id:SampleType
                        },
                        BSpecialty:{
                            Id:Specialty
                        },
                        CName:GroupCname,
                        MEPTSamplingTube:{
                            Id:SamplingTubeID
                        },
                        SName:SamplingGroSName,
                        ShortCode:ShortCode,
                        DispOrder:SamplingDispOrder,
                        Destination:Destination
                    };
                    Ext.Ajax.defaultPostHeader = 'application/json';
                    Ext.Ajax.request({
                        /*  async:false,//非异步*/
                        url:getRootPath()+"/MEPTService.svc/MEPT_UDTO_UpdateMEPTSamplingGroup",
                        params:Ext.JSON.encode({entity:MEPTSamplingGroup}),
                        method:'POST',
                        timeout:2000,
                        success:function(response,opts){
                            var result = Ext.JSON.decode(response.responseText);
                            if(result.success){
                                //me.appId = result.id;
                                Ext.Msg.alert('提示','保存成功！');
                                if(Ext.typeOf(callback) == "function"){
                                    callback();//回调函数
                                }
                            }
                            else{
                                Ext.Msg.alert('提示','操作采样组失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                            }
                        },
                        failure : function(response,options){
                            Ext.Msg.alert('提示','操作采样组请求失败！');
                        }
                    });
                }
            }]
        }]
    },
    /**
     *删除采样组设置
     */
    deleteSampleTube:function(record){
        var me = this;
        var url =getRootPath()+"/MEPTService.svc/MEPT_UDTO_DelMEPTSamplingTube?Id=" + record.data.MEPTSamplingTube_Id;
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    Ext.Msg.alert('提示','删除成功！');
                    Ext.getCmp('gridSampleTube').store.load();
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
        var url =getRootPath()+"/MEPTService.svc/MEPT_UDTO_SearchMEPTSamplingTubeByHQL?" +
            "fields=MEPTSamplingTube_ColorValue,MEPTSamplingTube_ColorName,MEPTSamplingTube_MEPTBCharge_ChargeTypeID" +
            ",MEPTSamplingTube_Unit&where=SamplingTubeId="+id+"&isPlanish=true";
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:2000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    Ext.getCmp('txtTubeUnit').setValue(ResultDataValue.list[0].MEPTSamplingTube_Unit);
                    Ext.getCmp('txtChargeType').setValue(ResultDataValue.list[0].MEPTSamplingTube_MEPTBCharge_ChargeTypeID);
                    Ext.getCmp('txtTubeColorValue').setValue(ResultDataValue.list[0].MEPTSamplingTube_ColorValue);
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
            fields:me.SampleTubeFields,
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