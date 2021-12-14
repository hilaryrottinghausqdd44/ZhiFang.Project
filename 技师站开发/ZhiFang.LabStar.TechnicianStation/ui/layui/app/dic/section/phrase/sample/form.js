/**
	@name：小组短语表单
	@author：liangyl
	@version 2019-10-31
 */
layui.extend({
}).define(['form','uxutil'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	
	//变量	
    var config ={
    	formtype:'add',
		PK:null,
		//当前已加载的数据
		currData:[],
		SectionID:null,
		ObjectID:null,
		TypeName:null,
		TypeCode:null
    };
    		
	//短语新增服务地址
	var ADD_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase";
	//短语修改服务地址
	var EDIT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField";
	//短语查询服务地址
	var SELECT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseById?isPlanish=true";
    //短语查询服务地址
	var DEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase";
    //提取中文字符串拼音字头
    var	PINYIN_URL=uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou';
	
	var phraseform={
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
		me.config = $.extend({},me.config,phraseform.config,setings);
	};
	Class.pt = Class.prototype;
	/**创建数据字段*/
	Class.pt.getStoreFields =  function() {
		var fields = ["LBPhrase_Id","LBPhrase_CName","LBPhrase_Shortcode","LBPhrase_DispOrder"];
		return fields;
	};
	//加载表单数据	
	Class.pt.loadDatas = function(id,callback){
		var me = this;
		var url = SELECT_URL + '&id=' + id+
		'&fields='+me.getStoreFields().join(',');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				callback(data);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	 /**@overwrite 返回数据处理方法*/
	Class.pt.changeResult = function(data){
		var me = this;
		var list =  JSON.parse(data);
	    config.currData=list;
		return list;
	};
	//核心入口
	phraseform.render = function(options){
		var me = new Class(options);
		me.loadData = Class.pt.load;
        me.onSaveClick = Class.pt.onSaveClick;
        me.onDelClick = Class.pt.onDelClick;
        me.isAdd = Class.pt.isAdd;
		me.initFilterListeners();
		return me;
	};
	//加载 
	Class.pt.load = function(id,SectionID,TypeName,TypeCode) {
		var me = this;
		config.PK = id;
		if(!config.PK){
			me.isAdd(SectionID,TypeName,TypeCode);
			return;
		}
		config.SectionID=SectionID;
		config.TypeName=TypeName;
		config.TypeCode = TypeCode;
		//加载数据
		me.loadDatas(config.PK,function(data){
	    	config.formtype='edit';
	    	me.isDelEnable(true);
			form.val('LBPhrase',me.changeResult(data.ResultDataValue));
		});
	};
	 //新增
    Class.pt.isAdd = function(SectionID,TypeName,TypeCode){
    	var me = this;
    	config.SectionID=SectionID;
		config.TypeName=TypeName;
		config.TypeCode = TypeCode;
    	config.PK=null;
    	config.formtype='add';
        config.currData=[];
    	me.onResetClick();
    	me.isDelEnable(false);
    };
    
    /**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBPhrase_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(!entity.DispOrder)delete entity.DispOrder;
		if(!entity.Id)delete entity.Id;
		entity.ObjectID = config.SectionID;
		entity.ObjectType = 1;
		entity.IsUse =1;
		entity.PhraseType = 'SamplePhrase';
		entity.TypeName = config.TypeName;
		entity.TypeCode = config.TypeCode;
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = 'Id,CName,Shortcode,DispOrder';
		if (data["LBPhrase_Id"])
			entity.entity.Id = data["LBPhrase_Id"];
		return entity;
	};

	
	//表单保存处理
	Class.pt.onSaveClick = function(data,callback) {
		var me = this;
		var url = config.formtype == 'add' ? ADD_URL : EDIT_URL;
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
		var id = document.getElementById("LBPhrase_Id").value;    
        if(!id)return;
    	var url = DEL_URL+'?id='+ id;
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
    	   $("#delsamplephrase").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#delsamplephrase").addClass("layui-btn-disabled").attr('disabled',true);
    };
   
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
	Class.pt.onResetClick=function(){
		var me = this;
		if(config.formtype=='add'){
			$("#LBPhrase").find('input[type=text],select,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
	        
		}else{
			form.val('LBPhrase',config.currData);
			form.render() ;
		}
	};
    //事件处理
    Class.pt.initFilterListeners = function(){
    	var me = this;
    	
    	//重置
    	$('#resetsamplephrase').on('click',function(){
    		me.onResetClick();
    	});
    	 $('#LBPhrase_CName').on("input propertychange", function () {
        	me.changePinYinZiTou();
            //这里写你的处理代码
        });
    };
    Class.pt.changePinYinZiTou=function(){
		var me = this;
		var CName = document.getElementById("LBPhrase_CName"),
			Shortcode =document.getElementById('LBPhrase_Shortcode');
			
		var val = CName.value;
		if(val != ""){
			me.getPinYinZiTou(val,function(data){
				Shortcode.value=data;
			});
		}else{
			Shortcode.value('');
		}
	};			
    //拼音字头
	Class.pt.getPinYinZiTou = function(val,callback){
		var me = this;
		var url = PINYIN_URL+'?chinese=' + encodeURI(val);
		if (val == "") {
            if (typeof (callback) == "function") {
                callback(chinese);
            }
            return;
       }
        $.ajax({
            type: "get",
            url: url,
            dataType: 'json',
            success: function (res) {
                if (res.success) {
                    if (typeof (callback) == "function") {
                        callback(res.ResultDataValue);
                    }
                } else {
                    layer.msg("拼音字头获得失败！", { icon: 5, anim: 6 });
                }
            }
        });
	};
   
	//暴露接口
	exports('phraseform',phraseform);
});