/***
 * 外送样本
 */
Ext.ns('Ext.mept');
Ext.define('Ext.mept.SampleDeliveryModule.sendSampleApp', {
    extend:'Ext.panel.Panel',
    panelType:'Ext.panel.Panel',
    alias:'widget.sendSampleApp',
    title:'外送样本',
    header:false,
    border:false,
    /***
     * 操作人工号的前缀字符
     * @type String
     */
    valuePrefix:'HG',
    /***
     * 某个样本操作类型中文名称模糊查询:送达
     * @type String
     */
    bSampleOperateTypeCY:'',
    /***
     * 某个样本状态类型中文名称模糊查询:已送达
     * @type String
     */
    bSampleOperateTypeYCY:'',
    /**
     * 某个操作对象类型信息中文名称模糊查询:样本单类型
     * @type String
     */
    bOperateObjectTypeYBD:'',
    /***
     * 操作对象类型信息:获取样本单类型
     * @type 
     */
    bOperateObjectType:null,
    /***
     * 样本操作类型--采样,送检,分发等
     * @type 
     */
    bSampleOperateType:null,
    /***
     * 样本状态类型--已采样,已送检,已分发等
     * @type 
     */
    bSampleStatusType:null, 
    /**
     * 新增外送清单服务地址
     * @type 
     */
    AddMEPTSampleDeliveryUrl:getRootPath()+'/MEPTService.svc/MEPT_UDTO_AddMEPTSampleDelivery',
    /***
     * 批量增加样本外送关系记录
     * @type String
     */
    AddBatchMEPTSampleDeliveryUrl:getRootPath()+'/MEPTService.svc/MEPT_UDTO_AddBatchMEPTSampleDeliveryConditon',
    /***
     * 根据操作类型生成操作批次号码
     */
    getBatchNumberByOperateTypeUrl:getRootPath() +'/MEPTService.svc/MEPT_UDTO_GetBatchNumberByOperateType',
    /***
     * 获取委托单位信息
     */
    searchSearchBLaboratoryUrl:getRootPath() +'/SingleTableService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true&fields=BLaboratory_CName,BLaboratory_Id,BLaboratory_DataTimeStamp,BLaboratory_EName',
    
    /***
     * 获取样本单信息
     */
    searchMEPTSampleFormByHQLUrl:getRootPath() +'/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleFormByHQL?isPlanish=true&fields=MEPTSampleForm_BarCode,MEPTSampleForm_BarCodeOpTime,MEPTSampleForm_SerialScanTime,MEPTSampleForm_Id,MEPTSampleForm_DataTimeStamp',
    
    /***
     * 获取外送清单信息
     * MEPT_UDTO_SearchMEPTSampleDeliveryConditonByHQL
     * MEPT_UDTO_SearchMEPTSampleDeliveryByHQL
     */
    SearchMEPTSampleDeliveryByHQLUrl:getRootPath() +'/MEPTService.svc/MEPT_UDTO_SearchMEPTSampleDeliveryByHQL?isPlanish=true&fields=MEPTSampleDeliveryConditon_Id,MEPTSampleDeliveryConditon_DataTimeStamp,MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryNo,' +
            'MEPTSampleDeliveryConditon_MEPTSampleDelivery_SampleDeliveryMan,MEPTSampleDeliveryConditon_MEPTSampleDelivery_Id,MEPTSampleDeliveryConditon_MEPTSampleDelivery_DataTimeStamp',
    /***
     * 操作人工号
     * @type String
     */
    operaterId:'', 
    /***
     * 操作人姓名
     * @type String
     */
    operater:'', 
    /***
     * 操作人工号的前缀字符长度
     * @type Number
     */
    valuePrefixLength:2,
    /***
     * 条码输入框扫描次数
     * @type Number
     */
    barCodeCounts:0,    
    layout:{
        type:'border',
        regionWeights:{
            north:4,
            south:3,
            west:2,
            east:1
        }
    },
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.initLink();
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
        var employeeID=Ext.util.Cookies.get('000200')//员工ID
        var employeeName=Ext.util.Cookies.get('000201')//员工姓名
        me.operater=employeeName;
        if(employeeID!=""){
            me.operaterId=employeeID;
        }
        //操作人的ID,测试数据
        else{
            me.operaterId="4995112150440367218";
        }
        
    },
    /****
     * 提交外送样本单信息
     */
    sumbitBatchRecord:function(list){
        var me=this;        
        //{MEPTSampleDeliveryConditon:obj,meptSampleFormIDList:'111,222,4444'}
        if(list!=null&&list!=''){
            var LabID=Ext.util.Cookies.get('000100')//实验室ID
            //批次号
            var batchNumer='';
            var batchId='';
            if(LabID==''){
                LabID='0';
            }
            //获取服务器时间
            var dateValue='';
            var bLaboratoryDataTimeStamp='';            
            //委托单位Id
            var bLaboratoryId='';
            var f=function(timeValue)
            {
                dateValue=timeValue;
            };
            getServerInformation(f);
            //样本操作信息
            for(var i=0;i<list.length;i++)
            {
                //委托单位ID号
                var workId=list[i];
                //获取批次号
                batchNumer=me.getBatchNumberByOperateType(workId);
                alert('新生成批次号：'+batchNumer);
                //委托单位对象
                var myUrl=me.searchSearchBLaboratoryUrl+'&where=blaboratory.Id='+workId;
                var company=function(responseText)
                {
                    var result = Ext.JSON.decode(responseText);
                     if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            var r = Ext.JSON.decode(result.ResultDataValue);
                            bLaboratoryId=r.list[0]['BLaboratory_Id'];
                            bLaboratoryDataTimeStamp=r.list[0]['BLaboratory_DataTimeStamp'];                            
                        }
                     }
                }
                getToServer(myUrl,company,false);
                var objArr = [];
                if (bLaboratoryDataTimeStamp && bLaboratoryDataTimeStamp != undefined) {
                    objArr = bLaboratoryDataTimeStamp.split(",");
                }
                bLaboratory={
                    id:bLaboratoryId,  //SampleDeliveryID
                    DataTimeStamp:objArr                
                };
                dateValue=dateValue!=''?new Date(dateValue):new Date();
                var objdate=convertJSONDateToJSDateObject(dateValue);
                //外送清单表
                var meptSampleDelivery ={
	                //Id:batchNumer,  //按单位生成批次号
                    LabID:LabID,
                    SampleDeliveryID:batchNumer,  //外送清单ID
                    SampleDeliveryNo:'',   //外送清单编号
                    SampleDeliveryDate:objdate,   //外送日期
                    SampleDeliveryMan:me.operater,//操作人
                    DeliveryDeptID:'',//外送科室
                    DeliveryLabID:bLaboratory, //送出单位ID
                    PrintCount:0  //打印次数
	            };
                
            var objMEPTSampleDeliveryConditon={'entity':meptSampleDelivery,
                   meptSampleFormIDList:'DataTimeStamp,SampleDeliveryID,' +
                   'SampleDeliveryNo,SampleDeliveryDate,SampleDeliveryMan,' +
                   'DeliveryDeptID,DeliveryLabID,PrintCount'};
                   
             var addObj = Ext.JSON.encode(objMEPTSampleDeliveryConditon);
             var defaultPostHeader='application/json';
             var async=false;
             var callback=function(text)
             {
                var result = Ext.JSON.decode(text);
                if(result.success){
                    var data = Ext.JSON.decode(result.ResultDataValue);
                    batchId=data.id;
                    //外送单位添加成功后，增加样本外送关系明细
                    var tabPanel=me.getsendSampleTab();        
			        //未外送清单列表
			        var unsendSampleList=tabPanel.getunSendSampleList();
                    //var records=unsendSampleList.getSelectionModel().getSelection();
                    var records=unsendSampleList.store.data.items;
                    for(var k=0;k<records.length;k++){
                        //重新匹配委托单位
                        if(records[k].data['MEPTSampleForm_MEPTOrderForm_Client_Id']==workId&&
                           records[k].data['checkSelect']==true){
                            //样本单obj对象
                            var meptSampleFormId='';
                            var meptSampleFormDataTimeStamp='';
                            var sampleFormId=records[k].data['MEPTSampleForm_Id'];
                            var mysampleformUrl=me.searchMEPTSampleFormByHQLUrl+'&where=meptsampleform.id='+sampleFormId;
                            var f=function(responseText){
                                var result = Ext.JSON.decode(responseText);
			                    if (result.success) {
			                        if (result.ResultDataValue && result.ResultDataValue != '') {
			                            var r = Ext.JSON.decode(result.ResultDataValue);
			                            meptSampleFormId=r.list[0]['MEPTSampleForm_Id'];
			                            meptSampleFormDataTimeStamp=r.list[0]['MEPTSampleForm_DataTimeStamp'];                            
			                        }                                
			                    }
                                
                            }
                            getToServer(mysampleformUrl,f,false);
                            //样本单ID
                            var objdataArr = [];
			                if (meptSampleFormDataTimeStamp && meptSampleFormDataTimeStamp != undefined) {
			                    objdataArr = meptSampleFormDataTimeStamp.split(",");
			                }
                            var sampleFormObj={id:meptSampleFormId,
                             DataTimeStamp:objdataArr};
                             
                            //配置外送清单obj对象SampleDelivery外送清单表
                             var SampleDeliveryId='';
                             var SampleDeliveryDataTimeStamp='';
                             var mySampleDeliveryUrl=me.SearchMEPTSampleDeliveryByHQLUrl+'&where=meptsampledelivery.id='+batchId;
                             var c=function(responseText){                                
                                //var SampleDeliveryObj={id:sampleFormId,DataTimeStamp:''};
                                var result = Ext.JSON.decode(responseText);
                                if (result.success) {
                                    if (result.ResultDataValue && result.ResultDataValue != '') {
                                        var r = Ext.JSON.decode(result.ResultDataValue);
                                        SampleDeliveryId=r.list[0]['MEPTSampleDeliveryConditon_Id'];
                                        SampleDeliveryDataTimeStamp=r.list[0]['MEPTSampleDeliveryConditon_DataTimeStamp'];                            
                                    }                                
                                } 
                             }
                            getToServer(mySampleDeliveryUrl,c,false);
                            //样本单ID
                            var objdataArr1 = [];
                            if (SampleDeliveryDataTimeStamp && SampleDeliveryDataTimeStamp != undefined) {
                                objdataArr1 = SampleDeliveryDataTimeStamp.split(",");
                            }
                            var SampleDeliveryObj={id:batchId,
                             DataTimeStamp:objdataArr1};
                             
                            //保存样本外送关系表  SampleDeliveryConditon样本外送关系表
			                var meptSampleDeliveryConditon ={
			                    //Id:batchNumer,  //按单位生成批次号
			                    LabID:0,
			                    SampleFormID:sampleFormObj,  //样本单ID
			                    SampleDeliveryID:SampleDeliveryObj,   //外送清单ID
			                    DispOrder:1   //显示次序
			                    //DataAddTime:'',//创建时间
			                    //DataUpdateTime:'',//更新时间
			                    //DataTimeStamp:'' //时间戳
			                };
			                
				            var objMEPTSampleDeliveryConditon={'entity':meptSampleDeliveryConditon,
				                   meptSampleFormIDList:'LabID,SampleFormID,SampleDeliveryID,DispOrder'};
			                   
				             var addMEPTSampleDeliveryConditon = Ext.JSON.encode(objMEPTSampleDeliveryConditon);
				             var defaultPostHeader1='application/json';
				             var async1=false;
                             var callbackNew=function(text){
                                var result = Ext.JSON.decode(text);
					                if(result.success){
                                        alert('新增外送关系表数据');
					                    var data = Ext.JSON.decode(result.ResultDataValue);
					                 };
                             };
                             postToServer(me.AddBatchMEPTSampleDeliveryUrl,addMEPTSampleDeliveryConditon,callbackNew,defaultPostHeader1,async1);
                        }
                        
                    }
                    
                }
                //alert();//....
                
             }
             //新增外送清单
             postToServer(me.AddMEPTSampleDeliveryUrl,addObj,callback,defaultPostHeader,async);
           
           } 
            
        }
        
    },
    submitRecord:function(record) {
        var me = this;
        if(record!=null&&record!=''){
            var LabID=Ext.util.Cookies.get('000100')//实验室ID
            if(LabID==''){
                LabID='0';
            }
            var sampleFormId=record.get('MEPTSampleForm_Id');
            var form=me.getsamplingForm();
            var gridList=me.getservedonList();
            var barCode=me.gettxtBarCode();
            var store=gridList.getStore();
            if(store.getCount()>0){
                if(me.bOperateObjectType==null){
                    //操作对象类型信息:获取样本单类型
                    var hqlWhereObjectType="boperateobjecttype.Name like '"+me.bOperateObjectTypeYBD+"%'";
                    me.bOperateObjectType=me.getServerLists(me.selectBOperateObjectTypeUrl,hqlWhereObjectType,false);
                }
                if(me.bSampleOperateType==null){
                    //样本操作类型:获取样本操作(采样)类型
                    var hqlWhereOperateType="bsampleoperatetype.Name like '"+me.bSampleOperateTypeCY+"%'";
                    me.bSampleOperateType=me.getServerLists(me.selectBSampleOperateTypeUrl,hqlWhereOperateType,false);
                }
                if(me.bSampleStatusType==null){
                    //样本状态类型:获取样本状态(已采样)类型
                    var hqlWhereBSampleStatusType="bsamplestatustype.Name like '"+me.bSampleOperateTypeYCY+"%'";
                    me.bSampleStatusType=me.getServerLists(me.selectBSampleStatusTypeUrl,hqlWhereBSampleStatusType,false);
                }
               //操作对象类型信息:样本单操作
                var bOperateObjectType={};
                if(me.bOperateObjectType.length>0){
                    var id=me.bOperateObjectType[0]['BOperateObjectType_Id'];
                    var strDTStamp=me.bOperateObjectType[0]['BOperateObjectType_DataTimeStamp'];
                    var arrTemp=strDTStamp.split(',');
                    bOperateObjectType={Id:id,DataTimeStamp:arrTemp};
                }
                
                //样本操作类型
                var operateType={};
                if(me.bSampleOperateType.length>0){
                    var id=me.bSampleOperateType[0]['BSampleOperateType_Id'];
                    var strDTStamp=me.bSampleOperateType[0]['BSampleOperateType_DataTimeStamp'];
                    var arrTemp=strDTStamp.split(',');
                    operateType={Id:id,DataTimeStamp:arrTemp};
                }
                
                //样本状态类型
                var sampleStatusType={};
                if(me.bSampleStatusType.length>0){
                    var id=me.bSampleStatusType[0]['BSampleStatusType_Id'];
                    var strDTStamp=me.bSampleStatusType[0]['BSampleStatusType_DataTimeStamp'];
                    var arrTemp=strDTStamp.split(',');
                    sampleStatusType={Id:id,DataTimeStamp:arrTemp};
                }
                var dateTime=new Date();
                var dateValue=convertJSONDateToJSDateObject(dateTime);
               var objectIDList='';
                var meptsampleForm={};

                objectIDList=sampleFormId;
                //样本操作信息
                var bSampleOperate ={
                    Id:'-1',
                    LabID:LabID,
                    OperaterID:me.operaterId,//操作人ID
                    Operater:me.operater,//操作人
                    OperateTime:dateValue,//操作时间
                    OperateHost:'',//操作计算机()
                    OperateMemo:'',//操作说明
                    BOperateObjectType:bOperateObjectType,//操作对象类型--样本单
                    OperateType:operateType//样本操作类型
                };
                    
                //样本状态信息
                var sStatusType ={
                    Id:'-1',
                    LabID:LabID,
                    DataAddTime:dateValue,//创建时间
                    Comment:'',//操作说明
                    BOperateObjectType:bOperateObjectType,//操作对象类型--样本单
                    BSampleStatusType:sampleStatusType//样本状态类型
                }; 
                    
                //样本操作批量保存 18688236872
                var defaultPostHeader='application/json';
                var async=false;
                
                var objOperate={
                'entity':bSampleOperate,
                objectIDList:objectIDList
                };
                var paramsOperate = Ext.JSON.encode(objOperate);
                var cOperate = function(text){
                    var result = Ext.JSON.decode(text);
                    if(result.success){
                        record.set('dataStatus','<b style="color:green">样本操作成功</b>');
                        record.commit();
                    }else{
                        record.set('dataStatus','<b style="color:green">样本操作失败</b>');
                        record.commit();
                    }
                }
                postToServer(me.addMEPTSampleOperateUrl,paramsOperate,cOperate,defaultPostHeader,async);
                
                //样本状态批量更新
                var objStatus={
                'entity':sStatusType,
                objectIDList:objectIDList
                };
                var cStatus = function(text){
                    var result = Ext.JSON.decode(text);
                    if(result.success){
                        record.set('dataStatus','<b style="color:green">状态操作成功</b>');
                        record.commit();
                    }else{
                        record.set('dataStatus','<b style="color:green">状态操作失败</b>');
                        record.commit();
                    }
                }
                var paramsStatus = Ext.JSON.encode(objStatus);
                postToServer(me.addMEPTSampleStatusUrl,paramsStatus,cStatus,defaultPostHeader,async);
                
            }
        }
    },
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        //条码输入框扫描次数归零
        me.barCodeCounts=0;
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.mept.SampleDeliverModule.sendSampleTab', getRootPath() + '/ui/mept/SampleDeliveryModule/class/sendSampleTab.js');
        
        me.items=me.createItems();
        me.dockedItems=me.createdockedItems();
        me.callParent(arguments);
    },

    initLink:function() {
        var me = this;
        //保存外送清单
        var btnsaveQD=me.getToptoolbar().getComponent('btnsaveQD');
        var tabPanel=me.getsendSampleTab();
        
        //未外送清单列表
        var unsendSampleList=tabPanel.getunSendSampleList();
        
        btnsaveQD.on({
            click:function(but){
                //未外送清单列是否有选中列
                var bool=false;
                var records=unsendSampleList.getSelectionModel().getSelection();
                var dataList=unsendSampleList.store.data;
                var count=dataList.length;
                for(var i=0;i<count;i++)
                {
                    if(dataList.items[i].data['checkSelect'])
                    {
                        bool=true;
                        break;
                    }
                    
                }
                if(!bool)
                {
                    //alert('保存外送清单:'+count);
                    alert('请选择要保存外送清单');
                }
                else
                {
                    //批次条码号
                    var unsendSampleList1=tabPanel.getunSendSampleList();
                    var operType='外送';
                    var batchNumber=me.getBatchNumberByOperateType(operType);
                    var clientArr=[];
                    var tempId='';
                    for(var k=0;k<count;k++){
                        //得到选中单位
                        if(dataList.items[k].data['checkSelect'])
	                    {
	                        var clientId=dataList.items[k].data['MEPTSampleForm_MEPTOrderForm_Client_Id']; 
                            clientArr.push(clientId);  
	                    }
                        
                    }
                   alert('分前数据组合：'+clientArr);
                  //==================
                  
                 /* //去掉数组中重复数据
                  var a = {};  
                  var len = clientArr.length;  
                  for(var i=0; i < len; i++)  {    
                      if(typeof a[clientArr[i]] == "undefined")    
                      a[clientArr[i]] = 1;    
                  }
                  clientArr.length = 0;    
                  for(var i in a)    
                  clientArr[clientArr.length] = i;*/ 
                  clientArr1=me.getArray(clientArr);
                  me.sumbitBatchRecord(clientArr1);
                  alert('确定保存外送清单吗？'+batchNumber+'分组数据：'+clientArr1);
                }
                
                
            
            }
        });
        
        //打印送检清单
        var btnprintQD=me.getToptoolbar().getComponent('btnprintQD');
        btnprintQD.on({
            click:function(but){
                alert('打印送检清单');
            
            }
        });
        
        //预览送检清单
        var btnprintSee=me.getToptoolbar().getComponent('btnprintSee');
        btnprintSee.on({
            click:function(but){
                alert('预览送检清单');
            
            }
        });
        
        //保存并打印送检清单
        var btnsaveAndprint=me.getToptoolbar().getComponent('btnsaveAndprint');
        btnsaveAndprint.on({
            click:function(but){
                alert('保存并打印送检清单');
            
            }
        });
        
        //关闭
        var btnclose=me.getToptoolbar().getComponent('btnclose');
        btnclose.on({
            click:function(but){
                //alert('关闭');
                me.close();
            
            }
        });
        
        //改变标签页显示工具栏按钮
        tabPanel.on({
            tabchange:function(tabPanel,newCard,oldCard,eOpts){
                //alert('24234');
                if(tabPanel.activeTab.itemId=='unSendSampleList'){
                    btnsaveQD.setVisible(true);
                    btnprintQD.setVisible(false);
                    btnprintSee.setVisible(false);
                    btnsaveAndprint.setVisible(true);
                    
                }
                if(tabPanel.activeTab.itemId=='sendSampleList'){
                    btnsaveQD.setVisible(false);
                    btnprintQD.setVisible(true);
                    btnprintSee.setVisible(true);
                    btnsaveAndprint.setVisible(false);
                }
                
            }
        });
        
        
    },
    //===========公共方法===========================
    /***
     * 去掉数组中重复数据，返回数组
     * @return {}
     */
    getArray:function(arr){
        var a = {};  
        var len = arr.length;  
        for(var i=0; i < len; i++)  {    
          if(typeof a[arr[i]] == "undefined")    
          a[arr[i]] = 1;    
        }
        arr.length = 0;    
        for(var i in a)    
        arr[arr.length] = i; 
        return arr;
    },
    /**
     * 获取顶部工具栏Id
     * @return {}
     */
    getToptoolbar:function() {
        var me = this;
        var com=me.getComponent('tlbTop');
        return com;
    },
    /**
     * 获取应用标签页ID
     * @return {}
     */
    getsendSampleTab:function() {
        var me = this;
        var com=me.getComponent('sendSampleTab');
        return com;
    },
    /***
     * 获取应用标签页-已外送标签页
     * @return {}
     */
    getsendDeliveryApp:function() {
        var me = this;
        var com=me.getsendSampleTab().getComponent('sendDeliveryApp');
        return com;
    },

    //====================================
        /***
     * 根据操作类型生成操作批次号码
     */
    getBatchNumberByOperateType:function(operateTypeValue){
        var me=this;
        var operateType="";
        var myUrl="";
        if(operateTypeValue&&operateTypeValue!=null){
            myUrl=me.getBatchNumberByOperateTypeUrl+'?operateType='+operateTypeValue;
            //查询数据
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:myUrl,
                method:'GET',
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        if(result['ResultDataValue'] && result['ResultDataValue'] != ""){
                            operateType =result['ResultDataValue'];
                        }
                    }else{
                        operateType="";
                    }
                },
                failure : function(response,options){
                     operateType="";
                }
            });
        }else{
            operateType="";
        }
        return operateType;
    },
    /***
     * 获取数据集
     * 1.依操作对象(样本单)ID查询样本状态类型表
     * 2.依操作对象ID(样本单)查询样本操作信息列表
     * 3.查询样本操作对象类型信息列表
     * 4.查询样本操作类型(录入,采样,接收,签收)
     * 5.查询样本状态类型信息列表
     */
    getServerLists:function(url,hqlWhere,async){
        var me=this;
        var arrLists=[];
        var myUrl='';
        if(hqlWhere&&hqlWhere!=null){
            myUrl=url+'&where='+encodeString(hqlWhere);;
        }else{
            myUrl=url;
        }
        //查询数据过滤条件行记录
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:async,//非异步
            url:myUrl,
            method:'GET',
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                var ResultDataValue = {count:0,list:[]};
                if(result['ResultDataValue'] && result['ResultDataValue'] != ''){
                    ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                    arrLists=ResultDataValue.list;
                }
                var count = ResultDataValue['count'];
                }else{
                    arrLists=[];
                }
            },
            failure : function(response,options){
                 arrLists=[];
            }
        });
        return arrLists;
    },
   createItems:function() {
        var me = this;
/*        var qcItemTimeLists=Ext.create('Ext.iqc.batch.qcItemTimeLists',
            {
            header:true,
            itemId:'qcItemTimeLists',
            name:'qcItemTimeLists',
            region:'north',
            height:200,
            title:'质控项目',
            border:false,
            collapsible:false,split:true
        });*/
        var sendSampleTab=Ext.create('Ext.mept.SampleDeliverModule.sendSampleTab',
            {
            header:false,
            border:false,
            itemId:'sendSampleTab',
            name:'sendSampleTab',
            region:'center',
            collapsible:true,
            split:true
        });
        var appInfos =[sendSampleTab]; 
        return appInfos;
    },
    //
    createdockedItems:function()
    {
        var me=this;
        var dockedItems = [{
	        xtype:'toolbar',dock:'top',itemId:'tlbTop',
	        items:['-',{
	            itemId:'btnsaveQD',text:'保存送检清单',hidden:false,iconCls:'build-button-save'
	        },{
                itemId:'btnprintQD',text:'打印送检清单',hidden:true,iconCls:'print'
            },'-',{
                itemId:'btnprintSee',text:'预览送检清单',hidden:true,iconCls:'build-button-see'
            },{
	            itemId:'btnsaveAndprint',text:'保存并打印送检清单',iconCls:'print'
	        },'-',{
	            itemId:'btnsend',text:'样本外送委托登记',disabled :true,iconCls:'build-button-search'
	        },'-',{
	            itemId:'btnsetup',text:'显示设置',iconCls:'build-button-edit'
	        },'-',{itemId:'btnclose',text:'关闭',iconCls:'imgdiv-close-show-16'}]
	    }];
    return dockedItems;
        
    }
});