/***
 * 复制站点参数设置
 * 一.添加修改数据
 * 判断依据(参数类型+选择站点+参数编码值组成惟一数据)
 * 1.先判断需要复制的站点参数的参数值是否与默认站点(所有站点)的参数值相同(字符串值比较),相同就不添加该站点参数信息,不相同时可以添加
 * 2.判断需要复制的站点参数是否已经存在(1)存在时,更新该参数值,,不存在时,添加一条新的数据
 * 二.某一站点与默认站点的站点参数不相同的数据信息
 * 判断依据:只查询选择的站点的所有数据行
 * 
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bparameterCopyApp', {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.bparameterCopyApp",
    title:"站点参数维护",
    header:true,
    border:false,
    height:document.body.clientHeight*0.90||450,
    width:document.body.clientWidth*0.92||560,
    /**
     * 查询参数列表服务地址
     * @type 
     */
    searchBParameterUrl:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
    fieldsBParameter:"BParameter_Id,BParameter_Name,BParameter_ParaType,BParameter_ParaValue,BParameter_BNodeTable_Id,BParameter_BNodeTable_Name,BParameter_ParaNo,BParameter_GroupNo",
    
    addBParameterUrl :getRootPath() + '/SingleTableService.svc/ST_UDTO_AddBParameter',
    editBParameterUrl : getRootPath() + '/SingleTableService.svc/ST_UDTO_UpdateBParameterByField',
    selectBParameterUrlById : 'SingleTableService.svc/ST_UDTO_SearchBParameterById?isPlanish=true',
    selectBParameterUrlByHQL : 'SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
    
    layout:{
        type:"border",
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
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    },
    /**
     * 创建功能挂靠
     * @private
     * @return {}
     */
    createDockedItems:function(){
        var me = this;
        var dockedItems = [];
        var buttonsToolbar = me.createButtonsToolbar();//上部按钮功能栏
        buttonsToolbar && dockedItems.push(buttonsToolbar);
        return dockedItems;
    },
    /**
     * 创建顶部按钮功能栏
     * @private
     * @return {}
     */
    createButtonsToolbar:function(){
        var me = this;
        var toolbar = {
            xtype:'toolbar',
            itemId:'buttonstoolbar',
            dock:'bottom',
            items:[{
                xtype:'button',
                itemId:'ok',
                text:'确定',
                iconCls:'build-button-ok',
                handler:function(but){
                    me.clickOkButton();
                }
            },{
                xtype:'button',
                itemId:'cancel',
                text:'关闭',
                iconCls:'build-button-cancel',
                handler:function(but){
                    me.closeClick();
                }
            },'->']
        };
        return toolbar;
    },
    closeClick:function(){
        var me = this;
        me.fireEvent('closeAfterClick',me);
    },
    /**
     * 初始化
     */
    initComponent:function(){
        var me = this;
        me.bodyPadding = 2;
        me.items=me.createItems();
        me.dockedItems = me.createDockedItems();
        me.addEvents('copySaveAfterClick');
        me.addEvents('closeAfterClick');
        me.callParent(arguments);
    },

    initLink:function() {
	    var me = this;
        var bnodeTableCopyList=me.getbnodeTableCopyList();
        var bparameterCopyList=me.getbparameterCopyList();
        var bnodeTableIdCom=bparameterCopyList.getBNodeTableId();
        if(bnodeTableIdCom){
	        bnodeTableIdCom.on({
	            select:function(combo,records){
	                   var newValue=combo.getValue();
		               if(newValue!=""&&newValue!=null){
                            var hqlWhere="bparameter.BNodeTable.Id='"+newValue+"'";
                            bparameterCopyList.load(hqlWhere);
		               }
	            },
                change:function(combo,newValue,oldValue,e){
                       if(newValue!=""&&newValue!=null){
                            var hqlWhere="bnodetable.Id!='"+newValue+"'";
                            bnodeTableCopyList.load(hqlWhere);
                       }
                }
	        });
        }
    },

    getbnodeTableCopyList:function() {
        var me = this;
        var com=me.getComponent("bnodeTableCopyList");
        return com;
    },
    getbparameterCopyList:function() {
        var me = this;
        var com=me.getComponent("bparameterCopyList");
        return com;
    },
   clickOkButton:function() {
        var me = this;
        var bparameterList=me.getbparameterCopyList();
        var bnodeTableList=me.getbnodeTableCopyList();
        var bparameterStore=bparameterList.getStore();
        var bnodeTableStore=bnodeTableList.getStore();
        
        var bparameterChecks=[];//站点参数选中的行集合
        var bnodeTableChecks=[];//站点选中的行集合
        var LabID=Ext.util.Cookies.get('000100')//实验室ID
        if(LabID==""){
            LabID='0';
        }
        if(bparameterStore.getCount()>0&&bnodeTableStore.getCount()>0){
            bparameterStore.each(function(record){
                if(record.get("checkSelect")==true){
                    bparameterChecks.push(record);
                }
            });
            bnodeTableStore.each(function(record){
                if(record.get("checkSelect")==true){
                    bnodeTableChecks.push(record);
                }
            });
        }
        
        var addChecks=[],addNodeLists=[];//站点参数选中的行集合需要新增的数据
        var updateChecks=[],updateNodeLists=[];//站点选中的行集合需要更新的数据
        if(bparameterChecks.length>0&&bnodeTableChecks.length>0){
            //遍历站点
            var nodeIdValue="",nodeNameValue="",strDataTimeStamp="",paraTypeValue="",paraNoValue="",paraValue="",nameValue="";result=false;
            Ext.Array.each(bnodeTableChecks, function(bnodeTableModel, bnodeTableIndex, countriesItSelf1) {
                nodeIdValue=bnodeTableModel.get("BNodeTable_Id");
                nodeNameValue=bnodeTableModel.get("BNodeTable_Name");
                strDataTimeStamp=bnodeTableModel.get("BNodeTable_DataTimeStamp");
                //遍历站点参数
			    Ext.Array.each(bparameterChecks, function(bparameterModel, bparameterIndex, countriesItSelf2) {
                    paraTypeValue=""+bparameterModel.get("BParameter_ParaType");
                    paraNoValue=""+bparameterModel.get("BParameter_ParaNo");
                    id=""+bparameterModel.get("BParameter_Id");
                    paraValue=""+bparameterModel.get("BParameter_ParaValue");
                    nameValue=""+bparameterModel.get("BParameter_Name");
                    //先判断该站点参数的参数值与默认站点与默认站点的参数值是相同
                    var obj=compareBParameterParaValue(paraTypeValue,paraNoValue,nodeIdValue,null);
                    result=obj["result"];
                    if(result==false){
                    //如果参数值相同,不需要添加该站点参数
                        
                    }else if(result==true){
                        //如果该站点的站点参数参数值与默认站点不相同,可能是新增或者编辑
                        var listsNode=obj["listsNode"];//取返回的站点信息
                        if(listsNode.length>0){
	                        //参数值不相同并且已经存在该站点参数数据,更新参数值
	                        var eidtEntity={
                                 Id:id,
                                 ParaValue:paraValue
                            };
                            updateChecks.push(eidtEntity);
                            updateNodeLists.push({nodeName:nodeNameValue});
                        }else{
                            //参数值不相同并且不存在该站点参数数据,新增参数值
                            var DataTimeStamp=strDataTimeStamp.split(",");
                            var BNodeTable={
                                Id:nodeIdValue,
                                DataTimeStamp:DataTimeStamp
                            };
                            var addEntity={
                                 Id:'-1',
                                 Name:nameValue,
                                 LabID:LabID,
                                 ParaType:paraTypeValue,
                                 ParaNo:paraNoValue,
                                 IsUse:true,
                                 SName:bparameterModel.get("BParameter_SName"),
                                 PinYinZiTou:bparameterModel.get("BParameter_PinYinZiTou"),
                                 ParaValue:paraValue,
                                 BNodeTable:BNodeTable
                            };
                            addChecks.push(addEntity);
                            addNodeLists.push({nodeName:nodeNameValue});
                        }
                    }
                    
                });
			});
        }
        var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
        var win = Ext.create('Ext.form.Panel',{
            title:'批量操作站点参数信息',
            width:600,
            height:300,
            maxWidth:maxWidth,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            resizable:true,//可变大小
            draggable:true,//可移动
            bodyPadding:10,
            layout:'vbox'
        });
         var msgResult=true;   
        //循环保存新增的数据
         var paramsAdd="",nodeName="";
        if(addChecks.length>0){
	        Ext.Array.each(addChecks, function(model,index) {
			    paramsAdd = Ext.JSON.encode({
                        entity : model
                    });
                nodeName=addNodeLists[index]["nodeName"];
	            var callbackAdd = function(text) {
	                    var result = Ext.JSON.decode(text);
	                    if (result.success) {
	                        var value = '新增站点：【' + nodeName+ '】'+'参数编码【' + model["ParaNo"] + '】保存成功！';
	                        win.add({xtype:'label',text:value,style:'font-weight:bold;color:green;'});
	                    } else {
                            msgResult=false;
	                        var value = '新增站点：【' + nodeName  + '】'+'参数编码【' + model["ParaNo"] + '】保存失败！';
	                        win.add({xtype:'label',text:value,style:'font-weight:bold;color:red;'});
	                    }
	                };
	            postToServer(me.addBParameterUrl, paramsAdd, callbackAdd, false);
            });
        }
        //循环修改需要更新的数据
        if(updateChecks.length>0){
            var paramsEdit ="";
            Ext.Array.each(updateChecks, function(model,index) {
                nodeName=updateNodeLists[index]["nodeName"];
                paramsEdit = Ext.JSON.encode({
                    entity : model,
                    fields : "Id,ParaValue"
                 });
	             var callbackEdit = function(text) {
	                var result = Ext.JSON.decode(text);
	                if (result.success) {
	                    var value = '更新站点：【' + nodeName+ '】'+'参数编码【' + model["ParaNo"] + '】保存成功！';
                        win.add({xtype:'label',text:value,style:'font-weight:bold;color:green;'});
	                } else {
                        msgResult=false;
                        var value = '更新站点：【' + nodeName + '】'+'参数编码【' + model["ParaNo"] + '】保存失败！';
                        win.add({xtype:'label',text:value,style:'font-weight:bold;color:red;'});
	                }
	            };
                postToServer(me.editBParameterUrl, paramsEdit, callbackEdit, false);
            });
        }
        if(msgResult==false){
	        if(win.height > maxHeight){
	            win.height = maxHeight;
	        }
	        win.show();
        }
        me.fireEvent('copySaveAfterClick',me,win);
    },
   createItems:function() {
        var me = this;
        loaderJSFields();
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.manage.bparameter.bnodeTableCopyList', getRootPath() +'/ui/manage/class/bparameter/bnodeTableCopyList.js');
        Ext.Loader.setPath('Ext.manage.bparameter.bparameterCopyList', getRootPath() +'/ui/manage/class/bparameter/bparameterCopyList.js');

        var bnodeTableCopyList=Ext.create('Ext.manage.bparameter.bnodeTableCopyList',
            {
            header:false,
            itemId:'bnodeTableCopyList',
            name:'bnodeTableCopyList',
            region:'east',
            width : 320,
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var bparameterCopyList=Ext.create('Ext.manage.bparameter.bparameterCopyList',
            {
            header:false,
            itemId:'bparameterCopyList',
            name:'bparameterCopyList',
            region:'center',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var appInfos =[bnodeTableCopyList,bparameterCopyList]; 
        return appInfos;
    }
});