/***
 * 已外送清单--中部区域的应用
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliveryModule.sendDeliveryApp', {
    extend:"Ext.panel.Panel",
    alias:"widget.sendDeliveryApp",
    title:"",
    header:false,
    border:true,
    layout:{
        type:"border",
        regionWeights:{
            north:4,
            south:3,
            west:2,
            east:1
        }
    },
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
        Ext.Loader.setConfig({enabled:true});
        Ext.Loader.setPath('Ext.mept.SampleDeliveryModule.sendDeliveryList', getRootPath() + '/ui/mept/SampleDeliveryModule/class/sendDeliveryList.js');
        Ext.Loader.setPath('Ext.mept.SampleDeliveryModule.sendDeliveryDetaileList', getRootPath() + '/ui/mept/SampleDeliveryModule/class/sendDeliveryDetailedList.js');
        me.items=me.createItems();        
        
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
        var topTtems=['　　',
            dateintervals,'　',{
                type : 'refresh',
                itemId:'refresh',
                text:'刷新',
                iconCls:'build-button-refresh'
            }
        ];
        me.dockedItems = [{
                    xtype : 'toolbar',
                    dock : 'top',
                    itemId : 'toolbar-top',
                    items :topTtems
                }];
        me.callParent(arguments);
    },
    /***
     * 联动代码块
     */
   initLink:function() {
        var me=this;
        //刷新按钮
        var btnrefresh=me.getToptoolbar().getComponent('refresh');
       //日期
        var dateId=me.getToptoolbar().getComponent('dtDateintervals');
        //已外送列表清单ID
        var sendDeliveryList=me.getsendDeliveryList();
        //已外送清单明细列表ID
        var sendDeliveryDetaileList=me.getsendDeliveryDetaileList();
        btnrefresh.on({
            click:function()
            {
                //alert('刷新事件按钮！');
                var TimeValue=dateId.getValue();
                var tempArr=TimeValue.split("|");
                var arrStr=[];
                var beginDate='';
                var endDate='';
                var hqlWhere='';
                for(var i=0;i<tempArr.length;i++)
                {
                    if(tempArr[i]!==null&&tempArr[i]!=="")
                    {
                        arrStr.push(tempArr[i]);
                    }
                    
                }
                if(arrStr.length>0){
                    beginDate=arrStr[0]!=''?arrStr[0]:Ext.Date.format(new Date(),'Y-m-d');
                    endDate=arrStr[1]!=''?arrStr[1]:Ext.Date.format(new Date(),'Y-m-d');;
                    //alert('开始日期：'+beginDate+'结束日期：'+endDate);
                    var sendDeliveryList=me.getsendDeliveryList();
                    //insert into MEPT_SampleForm (LabID,SampleFormID,OrderFormID,BarCode,IsSpiltItem,IsSendOut,SampleTypeID,SamplingGroupID,SampleStatusID,SampleCap,SampleCount,BarCodeOpTime,SerialScanTime,BarCodeSource,PrintCount) 
                    //values (1,'4881317261940316067','4994091074496006011','1403240110',0,1,1,110,'4921702837562362463',0,0,'2014-03-25 09:10:45.000','2014-03-25 09:40:15.000',0,0)
                    hqlWhere=" (meptsampledeliveryconditon.MEPTSampleDelivery.SampleDeliveryDate between '"+beginDate+"' and '"+endDate+"') ";
                    alert('外送日期：'+hqlWhere);
                    sendDeliveryList.load(hqlWhere);
                
                }
            }
        });
        
        //监听已外送清单列表
        sendDeliveryList.on({
            itemclick:function(com,record,item,index,e,eOpts ){
                alert('监听已外送清单列表');
            
            },
            show:function( com,eOpts ){
                alert('监听已外送清单列表hghgfghs');
            },
            select:function(RowModel,record,index,eOpts ){
                alert('监听已外送清单列表5345352');
            }
        
        });

        
   },
   //====================公共方法============================
    /***
     * 外送清单打包列表
     * @return {}
     */
   getsendDeliveryList:function() {
        var me = this;
        var com=me.getComponent('sendDeliveryList');
        return com;
   },
    /***
     * 外送清单明细列表
     * @return {}
     */
   getsendDeliveryDetaileList:function() {
        var me = this;
        var com=me.getComponent('sendDeliveryDetaileList');
        return com;
   },
   /***
    * 工具栏
    * @return {}
    */
   getToptoolbar:function() {
        var me = this;
        var com=me.getComponent('toolbar-top');
        return com;
   },
   //==============================================
   
   createItems:function() {
        var me = this;
        var sendDeliveryList=Ext.create('Ext.mept.SampleDeliveryModule.sendDeliveryList',
            {
            header:true,
            itemId:'sendDeliveryList',
            name:'sendDeliveryList',
            region:'west',
            height:200,
            title:'外送清单列表',   //外送清单列表
            border:true,
            collapsible:true,
            split:true
        });
        var sendDeliveryDetaileList=Ext.create('Ext.mept.SampleDeliveryModule.sendDeliveryDetaileList',
            {
            header:true,
            border:false,
            itemId:'sendDeliveryDetaileList',
            name:'sendDeliveryDetaileList',
            title:'外送清单明细列表',
            region:'center',
            collapsible:false,
            split:true
        });
        var appInfos =[sendDeliveryList, sendDeliveryDetaileList]; 
        return appInfos;
    }
});