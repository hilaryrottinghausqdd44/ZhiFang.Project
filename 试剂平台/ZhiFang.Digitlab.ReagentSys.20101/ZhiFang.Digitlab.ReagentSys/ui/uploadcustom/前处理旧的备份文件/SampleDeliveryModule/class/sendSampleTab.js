/***
 * 样本外送Tabpanel页
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliverModule.sendSampleTab', {
    extend:'Ext.zhifangux.BasicTabPanel',
    alias:"widget.sendSampleTab",
    title:"",
    header:false,
    border:false,
    activeTab: 0,//初始显示第几个Tab页     
    deferredRender: false,//是否在显示每个标签的时候再渲染标签中的内容.默认true     
    tabPosition: 'top',//表示TabPanel头显示的位置,只有两个值top和bottom.默认是top.     
    enableTabScroll: true,//当Tab标签过多时,出现滚动条 
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.initLink();
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
       
    },

    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.zhifangux.BasicTabPanel', getRootPath() + '/ui/zhifangux/BasicTabPanel.js');
        Ext.Loader.setPath('Ext.zhifangux.FormPanel', getRootPath() + '/ui/zhifangux/FormPanel.js');
        Ext.Loader.setPath('Ext.zhifangux.GridPanel', getRootPath() + '/ui/zhifangux/GridPanel.js');
        Ext.Loader.setPath('Ext.mept.SampleDeliverModule.unSendSampleList', getRootPath() + '/ui/mept/SampleDeliveryModule/class/unSendSampleList.js');
        Ext.Loader.setPath('Ext.mept.SampleDeliveryModule.sendDeliveryApp', getRootPath() + '/ui/mept/SampleDeliveryModule/class/sendDeliveryApp.js');
        me.items=me.createItems();
        me.callParent(arguments);
    },

    /***
     * 联动代码块
     */
   initLink:function() {
        var me=this;     
        
        //获取未外送列表
        var list=me.getunSendSampleList();
        //获取未外送样本单顶部、底部工具栏ID
        var tlbId=me.getunSendSampleList().getComponent('toolbar-top');
        var tlbbottom=me.getunSendSampleList().getComponent('toolbar-bottom');
        //指定新外送单位ID
        var cmbBLaboratory=tlbbottom.getComponent('BLaboratoryId');
        
        var btnrefresh=tlbId.getComponent('btnrefresh');
        var btnSet=tlbbottom.getComponent('btnset');
        
        //全选
        var btnCheckAll=tlbId.getComponent('btnCheckAll');
        //全否
         var btnUnCheckAll=tlbId.getComponent('btnUnCheckAll');
        //时间ID
        var dtDateintervals=tlbId.getComponent('dtDateintervals');
        //委托单位ID
        var deptId=tlbId.getComponent('HRDept_Id');
        //送检单位
        var bLaboratoryId=tlbId.getComponent('BLaboratory_Id');
        btnrefresh.on({
            click:function(but){
                btnUnCheckAll.setIconCls('build-button-unchecked');
                btnCheckAll.setIconCls('build-button-unchecked');
                var dateValue=dtDateintervals.getValue();
                var deptValue=deptId.getValue();
                var bLaboratoryIdValue=bLaboratoryId.getValue();
                var unSendSampleList=me.getunSendSampleList();
                var arrStr=[];
                arrStr=dateValue.split('|');
                var beginDate=arrStr[0]!=''?arrStr[0]:Ext.Date.format(new Date(), 'Y-m-d');
                var endDate=arrStr[1]!=''?arrStr[1]:Ext.Date.format(new Date(), 'Y-m-d');
                //alert('默认当前日期：'+Ext.Date.format(new Date(), 'Y-m-d')+'未外送清单刷新列表！'+beginDate+'结束日期：'+endDate+'委托单位ID号：'+deptValue+'送检单位:'+bLaboratoryIdValue);
                var hqlWhere="(meptsampleform.BarCodeOpTime between '"+beginDate+"' and '"+endDate+"')";
                
                if(deptValue)
                {
                    hqlWhere=hqlWhere+'  and meptsampleform.MEPTOrderForm.HRDept.Id='+deptValue
                }
                if(bLaboratoryIdValue)
                {
                    hqlWhere=hqlWhere+'  and meptsampleform.MEPTOrderForm.Client.Id='+bLaboratoryIdValue
                }
                //alert(hqlWhere);
                unSendSampleList.load(hqlWhere);
            }
        });
        btnCheckAll.on({
            click:function(but){
                btnUnCheckAll.setIconCls('build-button-unchecked');
                but.setIconCls('build-button-checked');
                var count=list.store.data.length;
                if(count>0)
                {
                    list.store.each(function(record) {
                        record.set('checkSelect', true);
                        record.commit();
                    });                    
                }
                
            }
        });
        btnUnCheckAll.on({
            click:function(but){
                btnCheckAll.setIconCls('build-button-unchecked');
                but.setIconCls('build-button-checked');
                var count=list.store.data.length;
                if(count>0)
                {
                    list.store.each(function(record) {
                        record.set('checkSelect', false);
                        record.commit();
                    });                    
                }
                
            }
        });
        //设置指定新的外送单位
        btnSet.on({
            click:function(but){
                //var bt=cmbBLaboratory;
                var valueField=cmbBLaboratory.getValue();
                var displayValue=cmbBLaboratory.getRawValue();
                //alert('设置指定新的外送单位'+txtValue+'显示值：'+txtValue1);
                var records=list;
                var num=list.store.data.length;
                if(list.store.data.length>0){
                    for(var i=0;i<num;i++)
                    {
                        record=list.store.data.items[i];
                        //选中更改行
                        var checkValue=record.get('checkSelect');
	                    if(valueField!=''&&valueField!=null&&checkValue){
	                        record.set('MEPTSampleForm_MEPTOrderForm_Client_Id',valueField);
                            //record.set('MEPTSampleForm_MEPTOrderForm_Client_DataTimeStamp',);
	                        //record.set('MEPTSampleForm_MEPTOrderForm_Client_CName',displayValue);
                            record.set('MEPTSampleForm_MEPTOrderForm_Client_CName','<b style="color:green">'+displayValue+'</b>');
                            record.commit();
	                        
	                    }
                    }                    
                }
            }
        
        });
    
   },
   //=====================公共方法======================
    /**
     * 获取未外送样本列表Id
     * @return {}
     */
    getunSendSampleList:function() {
        var me = this;
        var com=me.getComponent('unSendSampleList').getComponent('unSendSampleList');
        return com;
    },
    /***
     * 已外送清单列表-工具栏ID
     * @return {}
     */
    getsendDeliveryApp:function()
    {
        var me = this;
        var com=me.getComponent('sendDeliveryApp').getComponent('toolbar-top');
        return com;
        
    },
    
    
    
    //====================================================
   createItems:function() {
        var me = this;
        var sendDeliveryApp=Ext.create('Ext.mept.SampleDeliveryModule.sendDeliveryApp',
            {
            header:false,
            title:'已外送样本清单',
            itemId:'sendDeliveryApp',
            name:'sendDeliveryApp',
            collapsible:false
        });
        var unSendSampleList=Ext.create('Ext.mept.SampleDeliverModule.unSendSampleList',
            {
            header:false,
            title:'外送样本信息',
            itemId:'unSendSampleList',
            name:'unSendSampleList',
            collapsible:false
        });

        var items=[
                {
                title: '未外送样本',
                layout:'fit',
                itemId:'unSendSampleList',
                name:'unSendSampleList',                
                tabConfig: {
                    title: '未外送样本',
                    width:230,
                    tooltip: '未外送样本'
                },
                listeners: {
                    render:function(){
                        
                    },
                    activate : function(com,eOpts){ 
                     // alert("详细质控数据");
                    } 
                },
                items:[unSendSampleList]//[qcmaterialForm]
              },{
                title: '已外送样本',
                layout:'fit',
                itemId:'sendSampleList',
                name:'sendSampleList',
                tabConfig: {
                    title: '已外送样本',
                    width:230,
                    tooltip: '已外送样本'
                },
                listeners: {
                    render:function(){
                        
                    },
                    activate : function(com,eOpts){ 
                     // alert("详细质控数据");
                    } 
                },
                items:[sendDeliveryApp]
              }
            ];
        return items;
    }
});