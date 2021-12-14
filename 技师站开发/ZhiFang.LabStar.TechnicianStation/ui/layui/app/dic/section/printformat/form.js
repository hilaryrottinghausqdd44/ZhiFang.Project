/**
	@name：打印格式表单
	@author：liangyl
	@version 2019-10-26
 */
layui.extend({
	insertSelect:'app/dic/section/printformat/insertSelect',
}).define(['form','insertSelect'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		insertSelect = layui.insertSelect,
		form = layui.form;
	
	//变量	
    var config ={
    	formtype:'add',
		PK:null,
		//当前已加载的数据
		currData:[],
		SectionID:null,
		PrintFormatArr:[],
		BDefPrintArr:[]//存在的缺省打印机
    };
    		
	//打印格式新增服务地址
	var ADD_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBSectionPrint";
	//打印格式修改服务地址
	var EDIT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionPrintByField";
	//打印格式查询服务地址
	var SELECT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionPrintById?isPlanish=true";
    //打印格式查询服务地址
	var DEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBSectionPrint";
	//样本类型查询服务地址
	var SELECT_SAMPLETYPE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true";
	//就诊类型查询服务
	var SELECT_SICKTYPE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true";
    //检验项目查询服务地址
    var SELECT_ITEM_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true";
	//获取打印格式列表数据
	var SELECT_PRINT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionPrintByHQL?isPlanish=true';
    
	var printformatform={
		//全局项
		config:{
			//小组ID
			SectionID:null	
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
		me.config = $.extend({},me.config,printformatform.config,setings);
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
	    if(list.LBSectionPrint_BDefPrint=="false")list.LBSectionPrint_BDefPrint="";
	    config.currData=list;
		return list;
	};
	//核心入口
	printformatform.render = function(options){
		var me = new Class(options);
		me.initItem();
		me.initSickType();
		me.initSampleType();
		me.initPrintFormat();
//	    me.iniPrinterList();
		me.loadData = Class.pt.load;
        me.isAdd = Class.pt.isAdd;
        me.onSaveClick = Class.pt.onSaveClick;
        me.onDelClick =Class.pt.onDelClick;
		me.initFilterListeners();
		return me;
	};
	//加载 
	Class.pt.load = function(id,SectionID) {
		var me = this;
		config.PK = id;
		config.SectionID = SectionID;
		if(!config.PK){
			me.isAdd();
			return;
		}
		//加载数据
		me.loadDatas(config.PK,function(data){
			$('#printformType').removeAttr("layui-hide");
	    	$('#printformType').html("编辑");
	    	config.formtype='edit';
	    	me.isDelEnable(true);
    	    me.isSaveEnable(true);
			form.val('LBSectionPrint',me.changeResult(data.ResultDataValue));
		});
	};
	 //新增
    Class.pt.isAdd = function(SectionID){
    	var me = this;
    	$('#printformType').removeAttr("layui-hide");
    	$('#printformType').html("新增");
    	config.PK=null;
    	config.formtype='add';
        config.currData={};
        config.SectionID =SectionID;
    	me.onResetClick();
    	me.isDelEnable(false);
    	me.isSaveEnable(true);
    	
    };
    
    //查询小组是否已存在缺省打印机
	Class.pt.getBDefPrint = function(callback){
		var me = this;
		var url = SELECT_PRINT_LIST_URL+'&fields=LBSectionPrint_Id' +
		'&where=lbsectionprint.BDefPrint=1 and lbsectionprint.LBSection.Id='+config.SectionID;
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var ResultDataValue = data.ResultDataValue;
				if(ResultDataValue)ResultDataValue = $.parseJSON(ResultDataValue);
				var list =[];
				if(ResultDataValue){
					var list = ResultDataValue.list;
					if(list.length>0)config.BDefPrintArr = list;
				}
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//样本类型-下拉框加载
	Class.pt.initSampleType = function(){
		var me = this;
		var url = SELECT_SAMPLETYPE_URL+ '&lbsampletype.IsUse=1'+
		'&fields=LBSampleType_CName,LBSampleType_Id';
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
				var tempAjax = "<option value=''>请选择</option>";
                if(!value)return;
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSampleType_Id + "'>" + value.list[i].LBSampleType_CName + "</option>";
                    $("#LBSectionPrint_SampleTypeID").empty();
                    $("#LBSectionPrint_SampleTypeID").append(tempAjax);
                    
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//就诊类型-下拉框加载
	Class.pt.initSickType = function(){
		var me = this;
		var url = SELECT_SICKTYPE_URL+ '&lbsicktype.IsUse=1'+
		'&fields=LBSickType_CName,LBSickType_Id';
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
				var tempAjax = "<option value=''>请选择</option>";
                if(!value)return;
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSickType_Id + "'>" + value.list[i].LBSickType_CName + "</option>";
                    $("#LBSectionPrint_SickTypeID").empty();
                    $("#LBSectionPrint_SickTypeID").append(tempAjax);
                    
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	};
    //包含项目-下拉框加载
	Class.pt.initItem = function(){
		var me = this;
		var url = SELECT_ITEM_URL+ '&lbitem.IsUse=1'+
		'&fields=LBItem_CName,LBItem_Id';
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
				var tempAjax = "<option value=''>请选择</option>";
                if(!value)return;
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBItem_Id + "'>" + value.list[i].LBItem_CName + "</option>";
                    $("#LBSectionPrint_LBItem_Id").empty();
                    $("#LBSectionPrint_LBItem_Id").append(tempAjax);
                    
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	};
	Class.pt.unique = function(arr){
			var result = [],
			hash = {};
		for(var i = 0, elem;
			(elem = arr[i]) != null; i++) {
			if(!hash[elem]) {
				result.push(elem);
				hash[elem] = true;
			}
		}
		return result;
	};
	
//	//获取本地电脑可选的打印机(驱动),打印机
//	Class.pt.iniPrinterList = function () {
//		var list = clodopprint.getPrinterList();
//		var tempAjax = "<option value=''>请选择</option><option value='站点默认打印机'>站点默认打印机</option>";
//      if(!list)return;
//      for (var i = 0; i < list.length; i++) {
//          tempAjax += "<option value='" + list[i][1] + "'>" + list[i][1] + "</option>";
//      }
//      $("#LBSectionPrint_DefPrinter").empty();
//      $("#LBSectionPrint_DefPrinter").append(tempAjax);
//		form.render('select');//需要渲染一下;
//	};
	 //获取打印格式-下拉框加载
	Class.pt.initPrintFormat = function(){
		var me = this;
		var url = SELECT_PRINT_LIST_URL+
		'&fields=LBSectionPrint_PrintFormat';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				var ResultDataValue = data.ResultDataValue;
				if(ResultDataValue)ResultDataValue = $.parseJSON(ResultDataValue);
				var list = ResultDataValue.list;
				if(!list)return false;
				var arr = [];
				for(var i = 0; i < list.length; i++ ){
					arr.push(list[i].LBSectionPrint_PrintFormat)
				}
				var arr3 = me.unique(arr);
				config.PrintFormatArr = arr3;
				me.selectPrintFormat(arr3);
				
			}else{
				layer.msg(data.msg);
			}
		});
	};
	Class.pt.selectPrintFormat = function(arr3){
		var me = this;
		var tempAjax = "<option value=''>请选择</option>";
        for (var i = 0; i < arr3.length; i++) {
    		tempAjax += "<option value='" + arr3[i] + "'>" + arr3[i] + "</option>";
            $("#LBSectionPrint_PrintFormat").empty();
            $("#LBSectionPrint_PrintFormat").append(tempAjax);
        }
        var obj={
        	elem:'#LBSectionPrint_PrintFormat',
        	data:[],
        	tempInput:""
        };
        insertSelect.render(obj);
	};
    /**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBSectionPrint_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(entity.BDefPrint) entity.BDefPrint = entity.BDefPrint ? 1 :0;
		if(!entity.PrintOrder)delete entity.PrintOrder;
		if(!entity.SampleTypeID)delete entity.SampleTypeID;
		if(!entity.SickTypeID)delete entity.SickTypeID;
		if(!entity.ItemCountMax)delete entity.ItemCountMax;
		if(!entity.ItemCountMin)delete entity.ItemCountMin;
		if(!entity.Id)delete entity.Id;
		
		if(entity.LBItem_Id){
        	entity.LBItem={
        		Id:entity.LBItem_Id,
        		DataTimeStamp:[0,0,0,0,0,0,0,0]
        	};
        	delete entity.LBItem_Id;
        }else{
        	delete entity.LBItem_Id;
        }
        entity.LBSection={
    		Id:config.SectionID,
    		DataTimeStamp:[0,0,0,0,0,0,0,0]
    	};
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = 'Id,BDefPrint,PrintOrder,PrintFormat,PrintProgram,DefPrinter,SampleTypeID,SickTypeID,LBItem_Id,LBSection_Id,ItemCountMax,ItemCountMin,Nodename,Microattribute';
		if (data["LBSectionPrint_Id"])
			entity.entity.Id = data["LBSectionPrint_Id"];
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.upBDefPrint = function(list,BDefPrint) {
		var params = {
			"entity":{
				"Id":list[0].LBSectionPrint_Id,
				"BDefPrint":BDefPrint
			},
			fields:"Id,BDefPrint"
		};
		uxutil.server.ajax({
			url:EDIT_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			if(result.success){
			}else{
				layer.msg(result.msg);
			}
		},true);
	};
	
	//表单保存处理
	Class.pt.onSaveClick = function(SectionID,data,callback) {
		var me = this;
	    //判断是否为缺省值
	    if(data.field.LBSectionPrint_BDefPrint)var BDefPrint = data.field.LBSectionPrint_BDefPrint ? 1 :0;
        if(BDefPrint==1){//比较该小组是否已存在缺省打印机，如果存在更新已存在的缺省打印机
            if(config.formtype == 'add'){
            	if(config.BDefPrintArr.length>0){
            		me.upBDefPrint(config.BDefPrintArr,0);
            	}
            }else{
            	me.getBDefPrint(function(list){
            		if(list.length>0){
            			me.upBDefPrint(list,0);
            		}
            	});
            }
        }
		config.SectionID = SectionID;
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
				callback(id,config.formtype);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
   
    //删除方法 
	Class.pt.onDelClick = function(callback){
		var me = this;
		var id = document.getElementById("LBSectionPrint_Id").value;    
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
    	   $("#delPrint").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	else 
    	   $("#delPrint").addClass("layui-btn-disabled").attr('disabled',true);
    };
     //保存按钮是否禁用 del
    Class.pt.isSaveEnable =function(bo){
    	if(bo){
    	   $("#savePrint").removeClass("layui-btn-disabled").removeAttr('disabled',true);
    	}
    	else {
    	   $("#savePrint").addClass("layui-btn-disabled").attr('disabled',true);
    	}
    };
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
	Class.pt.onResetClick=function(){
		var me = this;
		if(config.formtype=='add'){
			$("#LBSectionPrint").find('input[type=text],select,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
	        me.getBDefPrint(function(list){
	    		if(list.length>0){
	    			$("input[name='LBSectionPrint_BDefPrint']").prop( 'checked', false );
	    			form.render() ;
		    	}else{
			        $("input[name='LBSectionPrint_BDefPrint']").prop( 'checked', true );
			        form.render() ;
		    	}
		    });
		}else{
			form.val('LBSectionPrint',config.currData);
			$("input[name='LBSectionPrint_BDefPrint']").prop( 'checked', false );
			form.render() ;
		}
	};
    //事件处理
    Class.pt.initFilterListeners = function(){
    	var me = this;
    	//重置
    	$('#resetPrint').on('click',function(){
    		me.onResetClick();
    	});
    	//设置打印机
    	$('#selectDefPrinter').on('click',function(){
    		var DefPrinter = document.getElementById('LBSectionPrint_DefPrinter').value;
			layer.open({
				title:'设置打印机',
				type:2,
				content:'printformat/setprinter.html?Print='+DefPrinter,
				maxmin:false,
				toolbar:true,
				resize:false,
				area:['400px','260px'],
				success: function(layero, index){
	       	       
		        }
			});
			
    	});
    };
    Class.pt.afterDefPrinterUpdate = function(DefPrinter){
        var me = this;
        document.getElementById('LBSectionPrint_DefPrinter').value = DefPrinter;
    };
    window.afterDefPrinterUpdate = Class.pt.afterDefPrinterUpdate;
	//暴露接口
	exports('printformatform',printformatform);
});