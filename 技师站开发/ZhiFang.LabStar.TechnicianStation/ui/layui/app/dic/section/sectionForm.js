/**
 * @name：检验小组信息
 * @author：liangyl
 * @version 2020-06-18
 */
layui.extend({
	CommonSelectDept:'app/dic/section/dept'
}).define(['form','commonzf','CommonSelectDept'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		commonzf = layui.commonzf,
		CommonSelectDept = layui.CommonSelectDept,
		uxutil = layui.uxutil,
		MOD_NAME = 'SectionForm';
	
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBSection';
	var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionByField';
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionById?isPlanish=true';
	//检验专业
	var GET_SPECICLTY_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true';
	//删除
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBSection';
	//获取指定实体字段的最大号
	var	GET_MAXNO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBSection&entityField=DispOrder';
	//提取中文字符串拼音字头
	var	GET_PINYIN_URL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou';

	//医嘱单列表
	var SectionForm = {
		formId:null,//表单ID
		//对外参数
		config:{
			domId:null,
			height:null,
			currData:{},
			formtype:'add',//add:新增,edit:修改,show:查看
			PK:null//ID
		},
		//内部表单参数
		formConfig:{
			
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SectionForm.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		//科室（执行科室）
		CommonSelectDept.render({
			domId:'LBSection_ExecDeptID',
			code:'1001104',
			done:function(){
				
			}
		});
		//专业大组
		CommonSelectDept.render({
			domId:'LBSection_SuperSectionID',
			code:'1001107',
			done:function(){
				
			}
		});
		me.loadSpecialty();
		me.SectionFun();
		form.render();
	};
		//小组类型-下拉框加载
	Class.prototype.SectionFun = function(){
		var me = this;
		//根据员工身份获取检验大组和检验科室
		commonzf.classdict.init('SectionFunType',function(){
			var list = commonzf.classdict.SectionFunType;
			
	        var len = list.length,
				htmls = [];
			var selectHtml ='';	
			for(var i=0;i<len;i++){
				htmls.push("<option value='" + list[i].Id +"' "+selectHtml+">" + list[i].Name + "</option>");
			}
			$("#LBSection_SectionFun").html(htmls.join(""));
			form.render('select');
		});
		commonzf.classdict.init('SectionType', function () {
			var list = commonzf.classdict.SectionType;

			var len = list.length,
				htmls = [];
			var selectHtml = '';
			for (var i = 0; i < len; i++) {
				htmls.push("<option value='" + list[i].Id + "' " + selectHtml + ">" + list[i].Name + "</option>");
			}
			$("#LBSection_SectionTypeID").html(htmls.join(""));
			form.render('select');
		});
		commonzf.classdict.init('SectionProDll', function () {
			var list = commonzf.classdict.SectionProDll;

			var len = list.length,
				htmls = [];
			var selectHtml = '';
			for (var i = 0; i < len; i++) {
				htmls.push("<option value='" + list[i].Id + "' " + selectHtml + ">" + list[i].Name + "</option>");
			}
			$("#LBSection_ProDLL").html(htmls.join(""));
			form.render('select');
		});
   };
    //检验专业-下拉框加载
	Class.prototype.loadSpecialty = function(){
		var me = this;
		var url = GET_SPECICLTY_LIST_URL+ '&where=lbspecialty.IsUse=1'+
		'&fields=LBSpecialty_CName,LBSpecialty_Id';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
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
                if(!value)return;
				var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSpecialty_Id + "'>" + value.list[i].LBSpecialty_CName + "</option>";
                    $("#LBSection_LBSpecialty_Id").empty();
                    $("#LBSection_LBSpecialty_Id").append(tempAjax);
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	};
	Class.prototype.changePinYinZiTou=function(){
		var me = this;
		var LBSection_CName = document.getElementById("LBSection_CName"),
			LBSection_PinYinZiTou = document.getElementById('LBSection_PinYinZiTou'),
			LBSection_Shortcode =document.getElementById('LBSection_Shortcode');
			
		var val = LBSection_CName.value;
		var Shortcode = LBSection_Shortcode.value;
		if(val != ""){
			me.getPinYinZiTou(val,function(data){
				LBSection_PinYinZiTou.value=data;//data
			});
		}else{
			LBSection_PinYinZiTou.value('');
			LBSection_Shortcode.value('');
		}
	};			
	//拼音字头
	Class.prototype.getPinYinZiTou = function(val,callback){
		var me = this;
		var url = GET_PINYIN_URL+'?chinese=' + encodeURI(val);
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
	//获取指定实体字段的最大号
	Class.prototype.getMaxNo = function(callback){
		var me = this;
		uxutil.server.ajax({
			url:GET_MAXNO_URL
		},function(data){
			if(data.success){
				callback(data.value || {});
			}else{
				layer.msg(data.msg);
			}
		});
	};
    //联动监听
    Class.prototype.initPinYinZiTouListeners =function(){
    	var me = this;
    	var LBSection_CName = document.getElementById("LBSection_CName");    
    	var LBSection_SName = document.getElementById("LBSection_SName");
        var LBSection_PinYinZiTou = document.getElementById("LBSection_PinYinZiTou");
        var LBSection_Shortcode = document.getElementById("LBSection_Shortcode");

        $('#LBSection_CName').on("input propertychange", function () {
        	me.changePinYinZiTou();
            //这里写你的处理代码
        });
         //简称 必有（输入名称后，简称为空时，简称-=名称）
        $("#LBSection_CName").on('blur',function(){
        	if(!LBSection_SName.value){
        		LBSection_SName.value=LBSection_CName.value;
        	}
        	//快捷码 为空时，也默认为汉字拼音字头
	        if(!LBSection_Shortcode.value && LBSection_PinYinZiTou.value){
	    		LBSection_Shortcode.value=LBSection_PinYinZiTou.value;
	    	}
        });
    };
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		me.initPinYinZiTouListeners();
		//新增
    	$('#add').on('click',function(){
    		me.isAdd();
    	});
		//保存
		form.on('submit(save)',function(obj){
			me.onSaveClick(obj);
		});
		//重置
    	$('#reset').on('click',function(){
    		me.onResetClick();
    	});
    	//删除
    	$('#del').on('click',function(){
    		me.onDelClick();
    	});
	};
	 //重置表单
    /**@overwrite 重置按钮点击处理方法*/
	Class.prototype.onResetClick=function(){
		var me = this;
		if(me.config.formtype=='add'){
			$("#LBSection").find('input[type=text],select,input[type=hidden]').each(function() {
	           $(this).val('');
	        });
			document.getElementById("LBSection_SectionFun").value = '10';//默认通用
			document.getElementById("LBSection_SectionTypeID").value = '0';//默认通用
			document.getElementById("LBSection_ProDLL").value = '0';//默认常规
	        if (!$("#LBSection_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBSection_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBSection_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBSection_IsUse")[0].checked = true;
            }
		}else{
			form.val('LBSection',me.config.currData);
		}
        form.render();
	};
	  //删除方法 
	Class.prototype.onDelClick = function(){
		var me = this;
		var id = document.getElementById("LBSection_Id").value;    
        if(!id)return;
    	var url = DEL_URL+'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					me.config.currData={};
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                  	layui.event("groupform", "del", {id:id});
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	 /**@overwrite 获取新增的数据*/
	Class.prototype.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBSection_/g, "");
		if (entity) entity = JSON.parse(entity);
		if (entity.IsImage) entity.IsImage = entity.IsImage ? 1 :0;
	    if (entity.IsVirtualGroup) entity.IsVirtualGroup = entity.IsVirtualGroup ? 1 :0;
		if (entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if (!entity.SuperSectionID) delete entity.SuperSectionID;
		if (!entity.SectionTypeID)
			delete entity.SectionTypeID;
		else {
			entity.SectionType = $("#LBSection_SectionTypeID option:selected").text();
		}
		if(!entity.ExecDeptID)delete entity.ExecDeptID;
		if (!entity.Id)delete entity.Id;
        if(entity.LBSpecialty_Id){
        	entity.LBSpecialty={
        		Id:entity.LBSpecialty_Id,
        		DataTimeStamp:[0,0,0,0,0,0,0,0]
        	};
        	delete entity.LBSpecialty_Id;
        }else{
        	delete entity.LBSpecialty_Id;
        }
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.prototype.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		
		entity.fields = 'Id,CName,EName,SName,SectionFun,ProDLL,UseCode,StandCode,DeveCode,Shortcode,PinYinZiTou,Comment,'+
		'SectionTypeID,SectionType,IsImage,IsUse,IsVirtualGroup,DispOrder,SuperSectionID,ExecDeptID,LBSpecialty_Id';//
		if (data["LBSection_Id"])
			entity.entity.Id = data["LBSection_Id"];
		return entity;
	};
		/**创建数据字段*/
	Class.prototype.getStoreFields =  function() {
		var fields = [];
		$(":input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	};
	//表单保存处理
	Class.prototype.onSaveClick = function(data) {
		var me = this;
		var url = me.config.formtype == 'add' ? ADD_URL : EDIT_URL;
		var params = me.config.formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				id = me.config.formtype == 'add' ? data.value.id : id;
				id += '';
				layui.event("form", "save", {id:id,formtype:me.config.formtype});
			} else {
				var msg = me.config.formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
	Class.prototype.isShow=function(id){
    	var me = this;
    	$('#formType').removeAttr("layui-hide");
    	$('#formType').html("查看");
    	me.config.PK=id;
    	me.config.formtype='show';
    	me.loadDatas(me.config.PK,function(data){
			form.val('LBSection',me.changeResult(data));
		});
    	me.isDelEnable(true);
    	//保存按钮禁用
    	me.isSaveEnable(false);
		me.SetDisabled(true, "LBSection");
    };
	Class.prototype.isEdit=function(id){
    	var me = this;
    	$('#formType').removeAttr("layui-hide");
    	$('#formType').html("编辑");
    	me.config.PK=id;
    	me.config.formtype='edit';
    	me.loadDatas(me.config.PK,function(data){
			form.val('LBSection',me.changeResult(data));
		});
    	me.isDelEnable(true);
		me.isSaveEnable(true);
		me.SetDisabled(false, "LBSection");
    };
      //新增
    Class.prototype.isAdd=function(){
    	var me = this;
    	$('#formType').removeAttr("layui-hide");
    	$('#formType').html("新增");
    	me.config.formtype='add';
    	me.onResetClick();
    	me.getMaxNo(function(val){
    		document.getElementById('LBSection_DispOrder').value=val;
    	});	  
    	me.isDelEnable(false);
		me.isSaveEnable(true);
		me.SetDisabled(false, "LBSection");
	};
	//禁用处理
	Class.prototype.SetDisabled = function (isDisabled, FormID) {
		if (!FormID) return;
		$("#" + FormID + " :input").each(function (i, item) {
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
    //删除按钮是否禁用 del
    Class.prototype.isDelEnable =function(bo){
    	if(bo)
    	   $("#del").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#del").addClass("layui-btn-disabled").attr('disabled',true);
    };
    //保存按钮禁用
    Class.prototype.isSaveEnable =function(bo){
    	if(bo)
    	   $("#save").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#save").addClass("layui-btn-disabled").attr('disabled',true);
    };
    //加载表单数据	
	Class.prototype.loadDatas = function(id,callback){
		var me = this;
		uxutil.server.ajax({
			url:GET_LIST_URL,
			data:{
				id:id,
				fields:me.getStoreFields().join(',')
			}
		},function(data){
            if (data.success){
           	   callback( data.value || {});
			}else{
				layer.msg(data.msg);
			}
		});
	};
	 /**@overwrite 返回数据处理方法*/
	Class.prototype.changeResult = function(list){
		var me = this;
		if(list.LBSection_IsVirtualGroup=="false")list.LBSection_IsVirtualGroup="";
	    if(list.LBSection_IsUse=="false")list.LBSection_IsUse="";
	    if(list.LBSection_IsImage=="false")list.LBSection_IsImage="";
		me.config.currData=list;
		return list;
	};
	//核心入口
	SectionForm.render = function(options){
		var me = new Class(options);
		//初始化HTML
		me.initHtml();
		//实例化表单
		me.form = form.render();
		//监听事件
		me.initListeners();
		
		//修改、查看状态，加载数据并显示
		if((me.config.formtype == 'edit' || me.config.formtype == 'show') && me.config.PK){
			me.loadDatas(me.config.PK);
		}
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,SectionForm);
});
