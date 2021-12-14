
Ext.Loader.setConfig({enabled:true});
Ext.ns('Ext.mept.SampleDeliverModule');
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.define("Ext.mept.SampleDeliveryModule.sampleDeliveryApp", {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.sampleDeliveryApp",
    title:"外送单位与外送项目维护",
    width:1200,
    height:500,
    externalWhere:'',    
    layout:{
        type:"border",
        regionWeights:{
            west:2,
            east:1
        }
    },
    getAppInfoServerUrl:getRootPath() + "/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",    
    comNum:0,
    afterRender:function() {
        var me = this;
        me.initLink();
        me.callParent(arguments);             
    },
    createItems:function() {
      var me = this;      
	  var appInfos =[ {
	        xtype:'tabpanel',
	        header:false,
	        itemId:'center',
	        name:'center',
	        title:'',
	        region:'center',
	        split:false,
	        collapsible:false,
	        collapsed:false,
	        border:true,
	        sequencenum:0,
	        defaultactive:false,
            items:[
                 {title: '外送单位',
                  itemId: 'deliveryCompany',
                  layout:'border',
                  items:[                       
	                       {region: 'north',xtype:'Company',layout:'fit',height:200,itemId:'gridCompany',width:'100%',border:true,
	                            split:true,defaultactive:false},                           
	                       {region: 'center',xtype:'frmCompany',itemId:'frmCompany',border:true,header:false,width:'100%',height:180}//,
	                       //{region: 'south',xtype:'actionbtntbar',layout:'fit',itemId:'actionbtntbar',width:'100%',border:true,
	                       //     split:true,defaultactive:false}                       
                     ],
             dockedItems:[{
                        xtype: 'toolbar',
                        itemId:'buttonbars',
                        dock: 'bottom',
                        items: [
                                {xytpe:'button',itemId:'btnAdd',name:'btnAdd',iconCls:'build-button-add',text:'新增　'},
                                {xytpe:'button',itemId:'btnEdit',name:'btnEdit',iconCls:'build-button-edit',text:'修改　'},
                                {xytpe:'button',itemId:'btnDel',name:'btnDel',iconCls:'build-button-delete',text:'删除　'},
                                {xytpe:'button',itemId:'btnCancel',name:'btnCancel',disabled:true,iconCls:'build-button-refresh',text:'取消　'},
                                {
                                xtype:'button',itemId:'btnSave',x:25,y:7,height:26,iconCls:'build-button-save',
                                width:55,text:'保存　',name:'btnSave',disabled:true,
                                listeners: {
                                    click: {
                                        element: 'el',
                                        fn: function(){ 
                                            me.fireEvent('okClick');
                                        }
                                    }   
                                }                       
                               }
                        ]}]
                },
	        {title: '外送项目',
	         itemId: 'deliveryItems',//deliveryItems
	         hidden: false,
	         layout:'border',
	         items:[    //west                                                
	                 {region: 'west',xtype:'OutSideCompanyItem',itemId:'OutSideCompanyItem',header:false,border:false},  
	                 {region: 'center',xtype:'OutSideItems',itemId:'OutSideItems',header:false,width:'70%'}
	            ]
	        }
      ]
    }	
 ]; 
        return appInfos;
      },
    getCallback:function(appInfo) {
        var me = this;
        var callback = function(obj) {
            if (obj.success && obj.appInfo != "") {
                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
                var cl = eval(ClassCode);
                var callback2 = function(panel) {
                    me.initLink(panel);
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            } else {
                appInfo.html = obj.ErrorInfo;
                var panel = Ext.create("Ext.panel.Panel", appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            }
        };
        return callback;
    },
   /**
    * 模块之间联动代码
    * @param {} panel
    */
    initLink:function(panel) {
	    var me = this;
	    //外送单位标签页
	    //var tabfirtid=me.getComponent('center');
	    var lblId=me.getCompanyForm();
	    var toolbarId=lblId.getComponent('buttonbars');
	    //外送单位表单
	    var form=lblId.getComponent('frmCompany');
	    //外送单位列表
	    var list=lblId.getComponent('gridCompany');
	    //工具栏按钮
	    var btnAdd=toolbarId.getComponent('btnAdd');
	    var btnEdit=toolbarId.getComponent('btnEdit');
	    var btnDel=toolbarId.getComponent('btnDel');
	    var btnCancel=toolbarId.getComponent('btnCancel');
	    var btnSave=toolbarId.getComponent('btnSave');
        //监听外送单位表单事件
        form.on({
            saveClick:function()
             {
                //获取表单所有值
	            var params=form.getForm().getValues();
	            var userkey=form.objectName+'_Id';
	            var userid=params[userkey];
	            if(userid!='')
	            {
	                list.autoSelect=userid;
	                //新增、修改后保存更新外送单位列表
	                list.load(true);
                    btnCancel.setDisabled(true);
                    btnSave.setDisabled(true);
	            }       
             },
            //保存前检验
	         beforeSave:function() 
              {
	             //alert('保存前后处理事件4242!!!424！');
	          }
        
        });       
        
        //alert('列表记录总数'+list.store.count());
        list.on({
            select:function(){                
                //frmCompany
                var records=list.getSelectionModel().getSelection();
                if(records.length==1)
                {
                    //alert('列表记录总数'+list.store.count());                    
                    record=records[0];
                    var text=record.data['BLaboratory_CName'];  //BLaboratory_Id
                    var lblId=me.getCompanyForm();
                    var frm=lblId.getComponent('frmCompany');
                    var id=record.data['BLaboratory_Id'];
                    //alert('单位ID：'+id);
                    frm.isShow(id);
                }
            }
        });        

        //新增按钮
        btnAdd.on({
            click:function(){
                var lblId=me.getCompanyForm();
                var frm=lblId.getComponent('frmCompany');
                var isuserId=frm.getComponent('BLaboratory_IsUse');
                //alert(isuserId.getValue());                
                frm.isAdd();
                isuserId.setValue(true);
                btnCancel.setDisabled(false);
                btnSave.setDisabled(false);
            }
        });
        //修改按钮
        btnEdit.on({
            click:function(){
                btnSave.setDisabled(false);
                btnCancel.setDisabled(false);
                //外送单位列表
                var list=me.getCompanyDeliveryList();
                //外送单位表单
                var form=me.getCompanyDeliveryForm();
                var records=list.getSelectionModel().getSelection();
                if(records.length==1)
                {
                     record=records[0];
                     var id=record.data['BLaboratory_Id'];
                     form.isEdit(id);
                }
            }
        });
        //删除按钮
        btnDel.on({
            click:function(){
                alert('点击删除事件！');
            }
        });
        //取消按钮
        btnCancel.on({
            click:function(){
                //alert('点击取消事件！');
                btnCancel.setDisabled(true);
                btnSave.setDisabled(true);
                
                var north=me.getCompanyForm();
                var frm=north.getComponent('frmCompany');
                //外送单位列表
               var listgrid=north.getComponent('gridCompany');
               var records=listgrid.getSelectionModel().getSelection();
               if(records.length==1)
               {
                   record=records[0];
                   var id=record.data['BLaboratory_Id'];                   
                   frm.isShow(id);
               }
            }
        });
        //保存按钮       
        btnSave.on({
            click:function(){
                //alert('点击保存事件！');
                var frm=me.getCompanyDeliveryForm();
                //var type=frm.type;
                frm.submit();
            }
        });
        
        //外送项目标签联动
        var listCompany=me.getCompanyList();
        var listItem=listCompany.getComponent('OutSideCompanyItem');
        listItem.on({
            itemclick:function(View,record,item,index,e,eOpts ){
                var record=record;
                //BLaboratory_DataTimeStamp
                var id=record.data['BLaboratory_Id'];
                var Name=record.data['BLaboratory_CName'];
                var DataTimeStamp=record.data['BLaboratory_DataTimeStamp'];
                //alert(id+'单位名称：'+DataTimeStamp);
                
                //var sendid=record.data['MEPTSampleSendItem_Send_Id'];
                //var sendDataTimeStamp=record.data['MEPTSampleSendItem_Send_DataTimeStamp'];
                //alert('外送单位ID'+sendid+'单位时间：'+sendDataTimeStamp);
                var listDouble=me.getCompanyList();
                var listDoubleItem=listDouble.getComponent('OutSideItems');
                
                listDoubleItem.setLeftFilterValue(id);
                listDoubleItem.setleftDataTimeStampValue(DataTimeStamp);
                listDoubleItem.leftLoad(id);
                listDoubleItem.rightLoad(true);
                
                
            }
        });
        
       
    },
  //=====================内部方法=======================
        //批量增加,减少
    Jurisdiction:function(empIdList,roleIdList,flag){
        var EmpIdList=getRootPath() + '/' +'RBACService.svc/RABC_RJ_SetEmpRolesByEmpIdList';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url:EmpIdList+ '?empIdList=' + empIdList +'&'+'roleIdList=' + roleIdList+'&'+'flag='+ flag,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
               Ext.Msg.alert('提示','批量提交成功!');
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','请求服务失败!');
            }
       });
    },

  //==================================
      /**
     * 初始化表单构建组件
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        //Ext.ns('Ext.manage');
        //Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
        me.items=me.createItems();
        me.callParent(arguments);
       
    },
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async:false,
            url:url,
            method:"GET",
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = "";
                    if (result.ResultDataValue && result.ResultDataValue != "") {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:"没有获取到应用组件信息!"
                        };
                        if (appInfo != "") {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】"
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == "function") {
                    var obj = {
                        success:false,
                        ErrorInfo:"获取应用组件信息请求失败！"
                    };
                    callback(obj);
                }
            }
        });
    },
    //=============公共方法================
    //获取外送单位表单对象
    //外送单位标签页：center-deliveryCompany-gridCompany(frmCompany\buttonbars)
    //外送项目标签页：center-deliveryItems-(OutSideCompanyItem\OutSideItems)
    /**
     * 外送单位标签页ID
     * @return {}
     */
   getCompanyForm:function() {
        var me = this;
        var com=me.getComponent('center').getComponent('deliveryCompany');
        return com;
    },    
    /**
     * //外送项目标签页ID
     * @return {}
     */
    getCompanyList:function() {
        var me = this;
        var com=me.getComponent('center').getComponent('deliveryItems');
        return com;
    },
    /**
     * 获取外送单位标签的单位列表
     * 
     */
    getCompanyDeliveryList:function()
    {
        var me=this;
        var center=me.getCompanyForm();
        var com=center.getComponent('gridCompany');
        return com;
    },
    /**
     *获取外送单位标签的单位表单ID 
     */
    getCompanyDeliveryForm:function()
    {
        var me=this;
        var center=me.getCompanyForm();
        var com=center.getComponent('frmCompany');
        return com;
    }
     
});