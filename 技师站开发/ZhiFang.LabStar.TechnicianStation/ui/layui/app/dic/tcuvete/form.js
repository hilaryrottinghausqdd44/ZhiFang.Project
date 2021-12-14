layui.extend({
}).define(['form','colorpicker'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		colorpicker = layui.colorpicker,
		form = layui.form;
	
	//变量	
    var config ={
    	formtype:'show',
		PK:null,
		//当前已加载的数据
		currData:[]
    };
	var tcuveteform={
		//全局项
		config:{
			addUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTcuvete',
			editUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBTcuveteByField',
			selectUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTcuveteById?isPlanish=true',
	    	delUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBTcuvete',
	    	 //获取指定实体字段的最大号
		    GET_MAXNO_URL : uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBTcuvete&entityField=DispOrder'
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
		me.config = $.extend({},me.config,tcuveteform.config,setings);
	};
	Class.pt = Class.prototype;
	/**创建数据字段*/
	Class.pt.getStoreFields =  function() {
		var fields = [];
		$(":input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	};
	//加载表单数据	
	Class.pt.loadDatas = function(id,callback){
		var me = this;
		var url = me.config.selectUrl + '&id=' + id+
		'&fields='+me.getStoreFields().join(',');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				me.isDelEnable(true);
				callback(data);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	 /**@overwrite 返回数据处理方法*/
	Class.pt.changeResult = function(data){
		var me = this;
		var list =  JSON.parse(data);
	    if(list.LBTcuvete_IsUse=="false")list.LBTcuvete_IsUse="";
	    if(list.LBTcuvete_IsPrep=="false")list.LBTcuvete_IsPrep="";
	    config.currData=list;
		return list;
	};
    /**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBTcuvete_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(entity.IsPrep) entity.IsPrep = entity.IsPrep ? 1 :0;
		if(entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if(!entity.MinCapability)delete entity.MinCapability;
		if(!entity.Capacity)delete entity.Capacity;
		if(!entity.DispOrder)delete entity.DispOrder;
		if(!entity.Id)delete entity.Id;
		if(!entity.ColorValue)delete entity.ColorValue;
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		
		entity.fields = 'Id,CName,Color,Capacity,Synopsis,Unit,SName,Code,MinCapability,ColorValue,IsPrep,SCode,'+
		'IsUse,DispOrder';
		if (data["LBTcuvete_Id"])
			entity.entity.Id = data["LBTcuvete_Id"];
		return entity;
	};
	//表单保存处理
	Class.pt.onSaveClick = function(data) {
		var me = this;
		var url = config.formtype == 'add' ? me.config.addUrl : me.config.editUrl;
		var params = config.formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config1, function(data) {
			//隐藏遮罩层
			if (data.success) {
				id = config.formtype == 'add' ? data.value.id : id;
				id += '';
				layui.event("form", "save", {id:id,formtype:config.formtype});
			} else {
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	 //获取指定实体字段的最大号
    Class.pt.getMaxNo = function(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: me.config.GET_MAXNO_URL
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
               layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    };
    //新增
    Class.pt.isAdd=function(){
    	var me = this;
    	me.showTypeSign('add');
    	config.PK=null;
    	config.formtype='add';
        config.currData={};
    	me.onResetClick();
    	me.isDelEnable(false);
    	me.isSaveEnable(true);
    	//表单赋值(颜色)
		colorpicker.render({
		    elem: '#color-form',
		    done: function(color){
             $('#LBTcuvete_ColorValue').val(color);
            }
		});
    };
     //显示编辑新增标识
    Class.pt.showTypeSign = function(type) {
        if (type == 'add') {
            if ($("#formType").hasClass("layui-hide")) {
                $("#formType").removeClass("layui-hide").html("新增");
            } else {
                $("#formType").html("新增");
            }
        } else if (type == 'edit') {
            if ($("#formType").hasClass("layui-hide")) {
                $("#formType").removeClass("layui-hide").html("编辑");
            } else {
                $("#formType").html("编辑");
            }
        }
    };
    //隐藏编辑新增标识
    Class.pt.hideTypeSign = function() {
        if (!$("#formType").hasClass("layui-hide")) {
            $("#formType").addClass("layui-hide");
        }
    }
    //删除方法 
	Class.pt.onDelClick = function(){
		var me = this;
		var id = document.getElementById("LBTcuvete_Id").value;    
        if(!id)return;
    	var url = me.config.delUrl+'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    config.currData={};
                  	layui.event("form", "del", {id:id});
				}else{
					layer.msg("删除失败！", { icon: 5, anim: 6 });
				}
			});
        });
	};
     //编辑
    Class.pt.isEdit = function(id){
    	var me = this;
    	me.showTypeSign('edit');
    	config.PK=id;
    	config.formtype='edit';
        //加载数据
		me.loadDatas(config.PK,function(data){
			var res = JSON.parse(data.ResultDataValue);
			//表单赋值(颜色)
			colorpicker.render({
			    elem: '#color-form',
			    color: res.LBTcuvete_ColorValue,
			    done: function(color){
			        $('#LBTcuvete_ColorValue').val(color);
			    }
			});
			form.val('LBTcuvete',me.changeResult(data.ResultDataValue));
		});
    };

    //删除按钮是否禁用 del
    Class.pt.isDelEnable =function(bo){
    	if(bo)
    	   $("#del").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#del").addClass("layui-btn-disabled").attr('disabled',true);
    };
     //保存按钮是否禁用 del
    Class.pt.isSaveEnable =function(bo){
    	if(bo){
    	   $("#save").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	}
    	else {
    	   $("#save").addClass("layui-btn-disabled").attr('disabled',true);
    	}
    };
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
	Class.pt.onResetClick=function(){
		var me = this;
		if(config.formtype=='add'){
			$("#LBTcuvete").find('input[type=text],select,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
	      
            if (!$("#LBTcuvete_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTcuvete_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTcuvete_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBTcuvete_IsUse")[0].checked = true;
            }
            $("#LBTcuvete_IsPrep").removeAttr('checked');
            me.getMaxNo(function (val) {
	            document.getElementById('LBTcuvete_DispOrder').value = val;
	        })
		}else{
			form.val('LBTcuvete',config.currData);
			$("input[name='LBTcuvete_IsPrep']:checked").prop({
                checked: false
            });
		}
        form.render('select');
        form.render('checkbox');
	};
	Class.pt.initHtml= function(){
		var me = this;
	};
    //事件处理
    Class.pt.initListeners = function(){
    	var me = this;
    	//表单赋值(颜色)
		colorpicker.render({
		    elem: '#color-form',
		    done: function(color){
		        $('#LBTcuvete_ColorValue').val(color);
		    }
		});
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
    
    //主入口
	tcuveteform.render = function(options){
		var me =  new Class(options);
		//初始化HTML
		me.initHtml();
		//实例化表单
		me.form = form.render();
		//监听事件
		me.initListeners();
		
		//修改、查看状态，加载数据并显示
		if((me.config.formtype == 'edit' || me.config.formtype == 'show') && me.config.PK){
			me.loadDatas(config.PK,function(data){
				form.val('LBTcuvete',me.changeResult(data.ResultDataValue));
			});
		}
		return me;
	};
	//暴露接口
	exports('tcuveteform',tcuveteform);
});