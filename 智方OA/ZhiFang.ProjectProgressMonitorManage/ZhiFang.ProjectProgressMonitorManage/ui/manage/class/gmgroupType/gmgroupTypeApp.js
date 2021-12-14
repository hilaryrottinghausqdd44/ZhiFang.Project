/***
 * 小组类型设置
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.gmgroupType.gmgroupTypeApp', {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.gmgroupTypeApp",
    title:"小组类型设置",
    header:false,
    border:false,
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

    initLink:function() {
        var me = this;
        var bnodeTableForm=me.getgmgroupTypeForm();
        var bnodeTableList=me.getgmgroupTypeList();
        if(bnodeTableList&&bnodeTableForm){
            bnodeTableForm.on({
                beforeSave:function(from){
                    var name=from.getComponent("GMGroupType_Name");
                    var nameValue=name.getValue();
                
                    var result=true,msg="";
                    if(nameValue==""||nameValue==null){
                        msg=msg+'名称不能为空!<br />';
                        result=false;
                    }
                   if(msg!=""){
                        alertInfo(msg);
                    }
                   return result;
                },
                saveClick:function(){
                    bnodeTableList.store.load();
                }
            
            });
	        bnodeTableList.on({
                select:function(rowModel,record, index, eOpts){
	                if (record!=null) {
                        var id = record.get('GMGroupType_Id');
                        bnodeTableForm.type='edit';
                        bnodeTableForm.dataId=id;
                        bnodeTableForm.isEdit(id,null);
                    }
	            },
                itemclick:function(View ,record, item,index,e, eOpts){
                    if (record!=null) {
                        var id = record.get('GMGroupType_Id');
                        bnodeTableForm.type='edit';
                        bnodeTableForm.dataId=id;
                        bnodeTableForm.isEdit(id,null);
                    }
                },
	            addClick:function(grid,record){
	               bnodeTableForm.type='add';
	               bnodeTableForm.dataId='-1';
//                   bnodeTableForm.setTitle(bnodeTableForm.defaultTitle+'-新增');
//                   bnodeTableForm.hideButtons(false);
//                   bnodeTableForm.setReadOnly(false);
	               bnodeTableForm.isAdd();
                   var comId=bnodeTableForm.getComponent("GMGroupType_Id");
                   if(comId&&comId!=undefined){
                        comId.setValue('-1');
                   }
                   var comName=bnodeTableForm.getComponent("GMGroupType_Name");
                   if(comName&&comName!=undefined){
                        comName.focus(true,100);
                   }
                   var comIsUse=bnodeTableForm.getComponent("GMGroupType_IsUse");
                   if(comIsUse&&comIsUse!=undefined){
                        comIsUse.setValue(true);
                   }
                   var comDataAddTime=bnodeTableForm.getComponent("GMGroupType_DataAddTime");
                   if(comDataAddTime&&comDataAddTime!=undefined){
                        comDataAddTime.setValue(new Date());
                   }
                   var comDataUpdateTime=bnodeTableForm.getComponent("GMGroupType_DataUpdateTime");
                   if(comDataUpdateTime&&comDataUpdateTime!=undefined){
                        comDataUpdateTime.setValue(new Date());
                   }
	            },
	            showClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('GMGroupType_Id');
	                    bnodeTableForm.type='show';
	                    bnodeTableForm.dataId=id;
	                    bnodeTableForm.isShow(id,null);
	                }else {
	                    alertInfo('请选择数据进行操作！');
	                }
	            },
	            editClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('GMGroupType_Id');
	                    bnodeTableForm.type='edit';
	                    bnodeTableForm.dataId=id;
	                    bnodeTableForm.isEdit(id,null);
	                }else {
	                    alertInfo('请选择数据进行操作！');
	                }
	            },
	            delClick:function(grid,record){
	               
	            }
	        });
        }
    },
    getgmgroupTypeForm:function() {
        var me = this;
        var com=me.getComponent("gmgroupTypeForm");
        return com;
    },
    getgmgroupTypeList:function() {
        var me = this;
        var com=me.getComponent("gmgroupTypeList");
        return com;
    },
   createItems:function() {
        var me = this;
        loaderJSFields();
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.manage.gmgroupType.gmgroupTypeForm', getRootPath() +'/ui/manage/class/gmgroupType/gmgroupTypeForm.js');
        Ext.Loader.setPath('Ext.manage.gmgroupType.gmgroupTypeList', getRootPath() +'/ui/manage/class/gmgroupType/gmgroupTypeList.js');

        var gmgroupTypeForm=Ext.create('Ext.manage.gmgroupType.gmgroupTypeForm',
            {
            header:false,
            itemId:'gmgroupTypeForm',
            name:'gmgroupTypeForm',
            region:'east',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var gmgroupTypeList=Ext.create('Ext.manage.gmgroupType.gmgroupTypeList',
            {
            header:false,
            itemId:'gmgroupTypeList',
            name:'gmgroupTypeList',
            region:'center',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var appInfos =[gmgroupTypeForm,gmgroupTypeList]; 
        return appInfos;
    }
});