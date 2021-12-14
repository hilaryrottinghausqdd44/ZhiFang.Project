/***
 * 站点参数设置
 * 判断依据(参数类型+选择站点+参数编码值组成惟一数据)
 * 1.先判断需要复制的站点参数的参数值是否与默认站点(所有站点)的参数值相同(字符串值比较),相同就不添加该站点参数信息,不相同时可以添加或修改
 * 
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bparameterApp', {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.bparameterApp",
    title:"站点参数维护",
    header:false,
    border:false,
    /**
     * 查询参数列表服务地址
     * @type 
     */
    searchBParameterUrl:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
    fieldsBParameter:"BParameter_Id,BParameter_Name,BParameter_ParaType,BParameter_ParaValue,BParameter_BNodeTable_Id,BParameter_BNodeTable_Name,BParameter_ParaNo,BParameter_GroupNo",
    
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
     * 初始化
     */
    initComponent:function(){
        var me = this;
        me.bodyPadding = 2;
        me.items=me.createItems();
        me.callParent(arguments);
    },
    detailsListLoad:function(record) {
        var me = this;
        if (record!=null) {
	        //站点参数编码
	        var paraNo = record.get('BParameter_ParaNo');
	        var paraType = record.get('BParameter_ParaType');
	        var nodeId = record.get('BParameter_BNodeTable_Id');
	        var hqlWhere="";
            if(paraNo!=""){
                hqlWhere=hqlWhere+" bparameter.ParaNo='"+paraNo+"'";
            }else if(paraNo==""){
                hqlWhere=hqlWhere+" ( bparameter.ParaNo is null or bparameter.ParaNo='')";
            }
	        if(paraType!=""){
	            hqlWhere=hqlWhere+" and bparameter.ParaType='"+paraType+"'";
	        }
	    }else{
            hqlWhere="1=2";
        }
        var bparameterDetailsList=me.getbparameterDetailsList();
        bparameterDetailsList.load(hqlWhere);
    },
    bparameterListIndex:0,
    detailsListIndex:0,
    initLink:function() {
        var me = this;
        var bparameterForm=me.getbparameterForm();
        var bparameterList=me.getbparameterList();
        var bparameterDetailsList=me.getbparameterDetailsList();
        bparameterList.on({
            select:function(rowModel,record, index, eOpts){
                if (record!=null) {
                    me.bparameterListIndex=index;
                    bparameterList.autoSelect=index;
                    me.detailsListLoad(record);
                }
            }
        });
        if(bparameterDetailsList&&bparameterForm){
            bparameterForm.on({
                //表单提交验证
                beforeSave:function(from){
                    var paraNo=from.getComponent("BParameter_ParaNo");
                    var name=from.getComponent("BParameter_Name");
                    var nodeId=from.getComponent("BParameter_BNodeTable_Id");
                    
                    var paraType=from.getComponent("BParameter_ParaType");
                    var paraTypeValue=paraType.getValue();
                    
                    var paraNoValue=paraNo.getValue();
                    var paraValueCom=from.getComponent("BParameter_ParaValue");
                    var nodeParaValue=paraValueCom.getValue();
                    
                    var nameValue=name.getValue();
                    var nodeIdValue=nodeId.getValue();
                    var result=true,msg="";
                    if(paraNoValue==""||paraNoValue==null){
                        msg='参数编码不能为空!<br />';
                        result=false;
                    }
                    if(nameValue==""||nameValue==null){
                        msg=msg+'名称不能为空!<br />';
                        result=false;
                    }
                    if(nodeIdValue==""||nodeIdValue==null){
                        msg=msg+'请选择所属站点!<br />';
                        result=false;
                    }
                    if(paraTypeValue==""||paraTypeValue==null){
                        msg=msg+'参数类型不能为空!<br />';
                        result=false;
                    }
                    if(nodeParaValue==""||nodeParaValue==null){
                        msg=msg+'参数值不能为空!<br />';
                        result=false;
                    }
                   //验证提交的参数值是否与默认站点的参数值是否相同,相同时提示并不能添加
                    var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                    var allNodeParaDesc="";
                    //如果新增的站点参数不是默认站点,就需要验证
                    if(nodeIdValue!=allNodeId){
                        var obj=compareBParameterParaValue(paraTypeValue,paraNoValue,nodeIdValue,nodeParaValue);
                        result=obj["result"];
                        if(result==false){
                            msg=msg+"<b style='color:red'>" +"参数值与默认站点相同,请不要重复添加!"+ "</b><br />";
                        }
                    }
                  if(from.type=="add"){
                        //查询参数类型为'条件选择类型',参数编码为"MEPT"
                        var url=me.searchBParameterUrl+"&fields="+me.fieldsBParameter;
                        var hqlWhere="bparameter.ParaType='"+paraTypeValue+"'"+" and bparameter.ParaNo='"+paraNoValue+"'"+ " and bparameter.BNodeTable.Id='"+nodeIdValue+"'";
                        var lists=getServerLists(url,hqlWhere,false);
                        if(lists.length>0){
                            msg=msg+"<b style='color:red'>" +"参数类型,选择站点,参数编码值"+ "</b>已经存在,请不要重复添加!<br />";
                            result=false;
                        }
                    }
                   if(msg!=""){
                        alertInfo(msg);
                    }
                   return result;
                },
                saveClick:function(){
                    bparameterList.load(true);
                    if(bparameterList.store.getCount()>0){
                        bparameterList.getSelectionModel().select(me.bparameterListIndex);
                    }
                }
            });
	        bparameterDetailsList.on({
                select:function(rowModel,record, index, eOpts){
	                if (record!=null) {
                        me.detailsListIndex=index;
                        bparameterDetailsList.autoSelect=index;
                        var id = record.get('BParameter_Id');
                        bparameterForm.type='edit';
                        bparameterForm.dataId=id;
                        bparameterForm.isEdit(id,null);
                    }
	            },
                copySaveAfterClick:function(panel,panelWin){
                    bparameterList.load(true);
                    bparameterDetailsList.autoSelect=0;
                },
                closeAfterClick:function(panel){
                    bparameterList.load(true);
                    bparameterDetailsList.autoSelect=0;
                    panel.close();
                },
                closeAppClick:function(panel){
                    bparameterList.load(true);
                    bparameterDetailsList.autoSelect=0;
                },
                delClick:function(View ,record, item,index,e, eOpts){
                    bparameterList.store.load();
                },
	            addClick:function(grid,record){
	               bparameterForm.type='add';
	               bparameterForm.dataId='-1';
                   bparameterForm.setTitle(bparameterForm.defaultTitle+'-新增');
                   bparameterForm.hideButtons(false);
                   bparameterForm.setReadOnly(false);
	               //bparameterForm.isAdd();
                   var comId=bparameterForm.getComponent("BParameter_Id");
                   if(comId&&comId!=undefined){
                        comId.setValue('-1');
                   }
                   var comName=bparameterForm.getComponent("BParameter_Name");
                   if(comName&&comName!=undefined){
                        comName.focus(true,100);
                   }
                   var comIsUse=bparameterForm.getComponent("BParameter_IsUse");
                   if(comIsUse&&comIsUse!=undefined){
                        comIsUse.setValue(true);
                   }
                   var comDataAddTime=bparameterForm.getComponent("BParameter_DataAddTime");
                   if(comDataAddTime&&comDataAddTime!=undefined){
                        comDataAddTime.setValue(new Date());
                   }
                   var comDataUpdateTime=bparameterForm.getComponent("BParameter_DataUpdateTime");
                   if(comDataUpdateTime&&comDataUpdateTime!=undefined){
                        comDataUpdateTime.setValue(new Date());
                   }
	            },
	            showClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('BParameter_Id');
	                    bparameterForm.type='show';
	                    bparameterForm.dataId=id;
	                    bparameterForm.isShow(id,null);
	                }else {
	                    alertInfo('请选择数据进行操作！');
	                }
	            },
	            editClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('BParameter_Id');
	                    bparameterForm.type='edit';
	                    bparameterForm.dataId=id;
	                    bparameterForm.isEdit(id,null);
	                }else {
	                    alertInfo('请选择数据进行操作！');
	                }
	            }
	        });
        }
    },
    getbparameterForm:function() {
        var me = this;
        var com=me.getComponent("bparameterForm");
        return com;
    },
    getbparameterList:function() {
        var me = this;
        var com=me.getComponent("bparameterList");
        return com;
    },
    getbparameterDetailsList:function() {
        var me = this;
        var com=me.getComponent("bparameterDetailsList");
        return com;
    },
   createItems:function() {
        var me = this;
        loaderJSFields();
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.manage.bparameter.bparameterForm', getRootPath() +'/ui/manage/class/bparameter/bparameterForm.js');
        Ext.Loader.setPath('Ext.manage.bparameter.bparameterList', getRootPath() +'/ui/manage/class/bparameter/bparameterList.js');
        Ext.Loader.setPath('Ext.manage.bparameter.bparameterDetailsList', getRootPath() +'/ui/manage/class/bparameter/bparameterDetailsList.js');

        var bparameterForm=Ext.create('Ext.manage.bparameter.bparameterForm',
            {
            header:false,
            itemId:'bparameterForm',
            name:'bparameterForm',
            region:'east',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var bparameterList=Ext.create('Ext.manage.bparameter.bparameterList',
            {
            header:false,
            itemId:'bparameterList',
            name:'bparameterList',
            region:'west',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var bparameterDetailsList=Ext.create('Ext.manage.bparameter.bparameterDetailsList',
            {
            header:false,
            itemId:'bparameterDetailsList',
            name:'bparameterDetailsList',
            region:'center',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var appInfos =[bparameterForm,bparameterList,bparameterDetailsList]; 
        return appInfos;
    }
});