/***
 * 站点参数设置
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bnodeTable.bnodeTableApp', {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.bnodeTableApp",
    title:"站点维护",
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
        var bnodeTableForm=me.getbnodeTableForm();
        var bnodeTableList=me.getbnodeTableList();
        if(bnodeTableList&&bnodeTableForm){
            bnodeTableForm.on({
                beforeSave:function(from){
                    var name=from.getComponent("BNodeTable_Name");
                    var nameValue=name.getValue();
                    var HRDeptId=from.getComponent("BNodeTable_HRDept_Id");
                    var HRDeptIdValue=HRDeptId.getValue();
                    var HRDeptDataTimeStamp=from.getComponent("BNodeTable_HRDept_DataTimeStamp");
                    var HRDeptDataTimeStampValue=HRDeptDataTimeStamp.getValue();
                    
                    HRDeptId.setValue("");
                    HRDeptDataTimeStamp.setValue("");
                    var result=true,msg="";
                    if(nameValue==""||nameValue==null){
                        msg=msg+'站点名称不能为空!<br />';
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
                        var id = record.get('BNodeTable_Id');
                        bnodeTableForm.type='edit';
                        bnodeTableForm.dataId=id;
                        bnodeTableForm.isEdit(id,null);
                    }
	            },
                itemclick:function(View ,record, item,index,e, eOpts){
                    if (record!=null) {
                        var id = record.get('bnodeTable_Id');
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
                   var comId=bnodeTableForm.getComponent("BNodeTable_Id");
                   if(comId&&comId!=undefined){
                        comId.setValue('-1');
                   }
                   var comName=bnodeTableForm.getComponent("BNodeTable_Name");
                   if(comName&&comName!=undefined){
                        comName.focus(true,100);
                   }
                   var comIsUse=bnodeTableForm.getComponent("BNodeTable_IsUse");
                   if(comIsUse&&comIsUse!=undefined){
                        comIsUse.setValue(true);
                   }
                   var comDataAddTime=bnodeTableForm.getComponent("BNodeTable_DataAddTime");
                   if(comDataAddTime&&comDataAddTime!=undefined){
                        comDataAddTime.setValue(new Date());
                   }
                   var comDataUpdateTime=bnodeTableForm.getComponent("BNodeTable_DataUpdateTime");
                   if(comDataUpdateTime&&comDataUpdateTime!=undefined){
                        comDataUpdateTime.setValue(new Date());
                   }
	            },
	            showClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('BNodeTable_Id');
	                    bnodeTableForm.type='show';
	                    bnodeTableForm.dataId=id;
	                    bnodeTableForm.isShow(id,null);
	                }else {
	                    alertInfo('请选择数据进行操作！');
	                }
	            },
	            editClick:function(grid,record){
	               if (record!=null) {
	                    var id = record.get('BNodeTable_Id');
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
    getbnodeTableForm:function() {
        var me = this;
        var com=me.getComponent("bnodeTableForm");
        return com;
    },
    getbnodeTableList:function() {
        var me = this;
        var com=me.getComponent("bnodeTableList");
        return com;
    },
   createItems:function() {
        var me = this;
        loaderJSFields();
        Ext.Loader.setConfig({enabled: true});//允许动态加载
        Ext.Loader.setPath('Ext.manage.bnodeTable.bnodeTableForm', getRootPath() +'/ui/manage/class/bnodeTable/bnodeTableForm.js');
        Ext.Loader.setPath('Ext.manage.bnodeTable.bnodeTableList', getRootPath() +'/ui/manage/class/bnodeTable/bnodeTableList.js');

        var bnodeTableForm=Ext.create('Ext.manage.bnodeTable.bnodeTableForm',
            {
            header:false,
            itemId:'bnodeTableForm',
            name:'bnodeTableForm',
            region:'east',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var bnodeTableList=Ext.create('Ext.manage.bnodeTable.bnodeTableList',
            {
            header:false,
            itemId:'bnodeTableList',
            name:'bnodeTableList',
            region:'center',
            split:true,
            border:true,
            collapsible:true,
            collapsed:false
        });
        var appInfos =[bnodeTableForm,bnodeTableList]; 
        return appInfos;
    }
});