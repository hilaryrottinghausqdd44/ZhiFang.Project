/**
	@name：仪器特定小组表单
	@author：liangyl
	@version 2020-04-14
 */
layui.extend({
}).define(['form','uxutil'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	//仪器ID
    var EQUIPID =null;
	//变量	
    var config ={
    	formtype:'add',
		PK:null,
		//当前已加载的数据
		currData:[]
    };
    		
	//获取特定小组列表数据
	var GET_EQUIP_SECTION_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipSectionById?isPlanish=true';
	//新增仪器特定小组
    var ADD_EQUIP_SECTION_URL  = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBEquipSection';
    //修改仪器特定小组
    var EDIT_EQUIP_SECTION_URL  = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipSectionByField';
    //删除仪器特定小组
    var DEL_EQUIP_SECTION_URL  = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBEquipSection';
	//小组下拉框数据
	var SECTIONLIST=[];
    //检验小组查询
    var SELECT_SECTION_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
    //仪器项目查询
    var SELECT_EQUIP_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemVOByHQL?isPlanish=true';

	var equipsectionform={
		//全局项
		config:{
			formtype:'add',
			PK:null,
			//当前已加载的数据
			currData:[]
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,equipsectionform.config,setings);
	};
	Class.pt = Class.prototype;
	/**创建数据字段*/
	Class.pt.getStoreFields =  function() {
		var fields = ["LBEquipSection_Id","LBEquipSection_LBSection_Id","LBEquipSection_CompValue1","LBEquipSection_CompValue2","LBEquipSection_LBEquip_Id",
		"LBEquipSection_LBItem_Id","LBEquipSection_SampleNoCode"];
		return fields;
	};
	//加载表单数据	
	Class.pt.loadDatas = function(id,EquipID){
		var me = this;
		EQUIPID = EquipID;
		config.currData =[];
		if(!EQUIPID){
			layer.msg('请先维护仪器');
			me.onResetClick();
			return;
		}
		config.formtype='edit';
		me.isDelEnable(true);
		var url = GET_EQUIP_SECTION_LIST_URL + '&id=' + id+
		'&fields='+me.getStoreFields().join(',');
		var index = layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(index);
			if (data.success) {
			    var list =  JSON.parse(data.ResultDataValue);
			    config.currData = list;
			    form.val('EquipSection',list);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
	//核心入口
	equipsectionform.render = function(options){
		var me = new Class(options);
		me.loadSection();
		me.loadData = Class.pt.loadDatas;
		me.loadEquipItem = Class.pt.loadEquipItem
        me.onSaveClick = Class.pt.onSaveClick;
        me.onDelClick = Class.pt.onDelClick;
        me.isAdd = Class.pt.isAdd;
		me.initFilterListeners();
		return me;
	};
	 //检验小组-下拉框加载
    Class.pt.loadSection = function () {
        var me = this;
        var url = SELECT_SECTION_URL + '&where=lbsection.IsUse=1' +
            '&fields=LBSection_CName,LBSection_Id';
            
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSection_Id + "'>" + value.list[i].LBSection_CName + "</option>";
                    $("#LBEquipSection_LBSection_Id").empty();
                    $("#LBEquipSection_LBSection_Id").append(tempAjax);

                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
     //仪器项目-下拉框加载
    Class.pt.loadEquipItem = function (id) {
        var me = this;
        var url = SELECT_EQUIP_ITEM_URL + '&where=lbequip.Id=' +id+
            '&fields=LBEquipItemVO_LBItem_Id,LBEquipItemVO_LBItem_CName';
            
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBEquipItemVO_LBItem_Id + "'>" + value.list[i].LBEquipItemVO_LBItem_CName + "</option>";
                    $("#LBEquipSection_LBItem_Id").empty();
                    $("#LBEquipSection_LBItem_Id").append(tempAjax);
                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
	 //新增
    Class.pt.isAdd = function(EquipID){
    	var me = this;
    	EQUIPID = EquipID;
    	config.formtype='add';
		me.onResetClick();
		me.setDisabled(false);
		me.isDelEnable(false);
		me.isSaveEnable(true);
	};
	//编辑
	Class.pt.isEdit = function (id,EquipID) {
		var me = this;
		EQUIPID = EquipID;
		config.formtype = 'edit';
		me.loadDatas(id, EquipID);
		me.setDisabled(false);
		me.isDelEnable(true);
		me.isSaveEnable(true);
	};
	//查看
	Class.pt.isShow = function (id,EquipID) {
		var me = this;
		EQUIPID = EquipID;
		config.formtype = 'add';
		me.loadDatas(id, EquipID);
		me.setDisabled(true);
		me.isDelEnable(true);
		me.isSaveEnable(false);
	};

	//禁用处理
	Class.pt.setDisabled = function (isDisabled) {
		$("#EquipSection :input").each(function (i, item) {
			if ($(item)[0].nodeName == 'BUTTON') return true;
			$(item).attr("disabled", isDisabled);
			if (isDisabled) {
				if (!$(item).hasClass("layui-disabled")) $(item).addClass("layui-disabled");
			} else {
				if ($(item).hasClass("layui-disabled")) $(item).removeClass("layui-disabled");
			}
		});
		form.render();
	};
    /**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBEquipSection_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(!entity.Id)delete entity.Id;
		entity.LBEquip ={Id:EQUIPID, DataTimeStamp: [0,0,0,0,0,0,0,0]};
	    if(entity.LBSection_Id)entity.LBSection ={Id:entity.LBSection_Id, DataTimeStamp: [0,0,0,0,0,0,0,0]};
		if(entity.LBSection_Id)delete entity.LBSection_Id;
		if(entity.LBItem_Id)entity.LBItem ={Id:entity.LBItem_Id, DataTimeStamp: [0,0,0,0,0,0,0,0]};
		if(entity.LBItem_Id)delete entity.LBItem_Id;
		if(entity.SampleNoCode)entity.SampleNoCode = entity.SampleNoCode.replace(/，/g,",");
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = 'Id,CompValue1,CompValue2,LBEquip_Id,LBSection_Id,SampleNoCode,LBItem_Id';
		return entity;
	};

	
	//表单保存处理
	Class.pt.onSaveClick = function(data,callback) {
		var me = this;
		if(!EQUIPID){
			layer.msg('请先维护仪器');
			return;
		}
		var url = config.formtype == 'add' ? ADD_EQUIP_SECTION_URL : EDIT_EQUIP_SECTION_URL;
		var params = config.formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var obj = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(obj, function(data) {
			//隐藏遮罩层
			if (data.success) {
				id = config.formtype == 'add' ? data.value.id : id;
				id += '';
				callback(config.formtype,id);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
    //删除方法 
	Class.pt.onDelClick = function(callback){
		var me = this;
		var id = document.getElementById("LBEquipSection_Id").value;    
        if(!id)return;
    	var url = DEL_EQUIP_SECTION_URL+'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    callback(id);
				}else{
					layer.msg(data.msg, { icon: 5, anim: 6 });
				}
			});
        });
	};
	
    //删除按钮是否禁用 del
    Class.pt.isDelEnable =function(bo){
    	if(bo)
    	   $("#delEquipSection").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#delEquipSection").addClass("layui-btn-disabled").attr('disabled',true);
	};
	//保存按钮是否禁用 save
	Class.pt.isSaveEnable = function (bo) {
		if (bo)
			$("#saveEquipSection").removeClass("layui-btn-disabled").removeAttr('disabled', true);
		else
			$("#saveEquipSection").addClass("layui-btn-disabled").attr('disabled', true);
	};
   
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
	Class.pt.onResetClick=function(){
		var me = this;
		if(config.formtype=='add'){
			$("#EquipSection").find('input[type=text],select,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
		}else{
			form.val('EquipSection',config.currData);
			form.render() ;
		}
	};
    //事件处理
    Class.pt.initFilterListeners = function(){
    	var me = this;
    	
    };
	//暴露接口
	exports('equipsectionform',equipsectionform);
});