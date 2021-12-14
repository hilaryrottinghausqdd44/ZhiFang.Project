layui.extend({
    tableSelect: '../src/tableSelect/tableSelect'
}).define(['form','tableSelect'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		tableSelect = layui.tableSelect,
		form = layui.form;
	
	//变量	
    var config ={
    	formtype:'show',
		PK:null,
		//当前已加载的数据
		currData:[]
    };
    
	var SamplingGroupForm={
		//全局项
		config:{
			addUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBSamplingGroup',
			editUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingGroupByField',
			selectUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingGroupById?isPlanish=true',
	    	delUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBSamplingGroup',
	    	selectSampleTypeUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
	    	selectTcuveteUrl : uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTcuveteByHQL?isPlanish=true',
	    	selectSpecialtyUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true',//专业	    
	    	selectDestinationUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDestinationByHQL?isPlanish=true',//送检目的地
		    //获取指定实体字段的最大号
		    GET_MAXNO_URL : uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBSamplingGroup&entityField=DispOrder'

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
		me.config = $.extend({},me.config,SamplingGroupForm.config,setings);
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
	    if(list.LBSamplingGroup_IsUse=="false")list.LBSamplingGroup_IsUse="";
	    if(!list.LBSamplingGroup_DestinationID)list.LBSamplingGroup_DestinationID=" ";
	    if(!list.LBSamplingGroup_SampleTypeID)list.LBSamplingGroup_SampleTypeID=" ";
	    if(!list.LBSamplingGroup_SpecialtyID)list.LBSamplingGroup_SpecialtyID=" ";
	    config.currData=list;
		return list;
	};
	//加载 
	Class.pt.load = function() {
		var me = this;
		if(config.PK){
			//加载数据
			me.loadDatas(config.PK,function(data){
				form.val('LBSamplingGroup',me.changeResult(data.ResultDataValue));
//			    $('#LBSamplingGroup_DestinationID').attr("width","180");
			});
		}
	};
    /**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/LBSamplingGroup_/g, "");
		if (entity) entity = JSON.parse(entity);
		if (entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if(!entity.PrintCount)delete entity.PrintCount;
		if(!entity.VirtualNo)delete entity.VirtualNo;
		if(!entity.DispOrder)delete entity.DispOrder;
        if(!entity.SampleTypeID || entity.SampleTypeID==' ')delete entity.SampleTypeID;
		if(!entity.SpecialtyID || entity.SpecialtyID==' ')delete entity.SpecialtyID;
		if(!entity.DestinationID || entity.DestinationID==' ')delete entity.DestinationID;
		if(entity.LBTcuvete_Id){
        	entity.LBTcuvete={
        		Id:entity.LBTcuvete_Id,
        		DataTimeStamp:[0,0,0,0,0,0,0,0]
        	};
        	delete entity.LBTcuvete_Id;
        }else{
        	delete entity.LBTcuvete_Id;
        }
		if (!entity.Id)delete entity.Id;
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		
		entity.fields = 'Id,CName,SampleTypeID,SpecialtyID,SName,SCode,DestinationID,'+
		'Synopsis,PrintCount,AffixTubeFlag,VirtualNo,IsUse,DispOrder,LBTcuvete_Id,PrepInfo';//
		if (data["LBSamplingGroup_Id"])
			entity.entity.Id = data["LBSamplingGroup_Id"];
		return entity;
	};
	//表单保存处理
	Class.pt.onSaveClick = function(data,callback) {
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
				var msg = '修改成功';
				if(config.formtype=='add')msg='新增成功!';
				layer.msg(msg,{icon:6,time:2000});
				callback(id);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5 });
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
    	//打印份数，新增默认为0
    	$('#LBSamplingGroup_PrintCount').val('0');
    	// 最大虚拟采样量，新增时默认0
    	$('#LBSamplingGroup_VirtualNo').val('0');
    	
    	$('#LBSamplingGroup_DestinationID').val(' ');
    	$('#LBSamplingGroup_SampleTypeID').val(' ');
    	$('#LBSamplingGroup_SpecialtyID').val(' ');
    	form.render();
    };
    //删除方法 
	Class.pt.onDelClick = function(callback){
		var me = this;
		var id = document.getElementById("LBSamplingGroup_Id").value;    
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
                    callback(id);
//                	layui.event("form", "del", {id:id});
				}else{
					layer.msg(data.ErrorInfo, { icon: 5});
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
			form.val('LBSamplingGroup',me.changeResult(data.ResultDataValue));
		});
    	me.isDelEnable(true);
    	if(!config.PK)me.isSaveEnable(false);
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
	Class.pt.onResetClick= SamplingGroupForm.onResetClick = function(){
		var me = this;
		if(config.formtype=='add'){
			$("#LBSamplingGroup").find('input[type=text],select,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	       });
	        me.getMaxNo(function (val) {
	            document.getElementById('LBSamplingGroup_DispOrder').value = val;
	        });
	        if (!$("#LBSamplingGroup_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBSamplingGroup_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBSamplingGroup_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBSamplingGroup_IsUse")[0].checked = true;
            }
		}else{
			form.val('LBSamplingGroup',config.currData);
		}
        form.render('select');
        form.render('checkbox');
	};
    //事件处理
    Class.pt.initFilterListeners = function(){
    	var me = this;
    	$('#add').on('click',function(){
    		me.isAdd();
    	});
		
		//重置
    	$('#reset').on('click',function(){
    		me.onResetClick();
    	});
    	//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
	    $("input.layui-input+.layui-icon").on('click', function () {
	        if (!$(this).hasClass("myDate") && !$(this).hasClass("myPhrase")) {
	            $(this).prev('input.layui-input')[0].click();
	            return false;//不加的话 不能弹出
	        }
	    });
    	
    };
	//初始化系统下拉框
	Class.pt.initSystemSelect = function(callback){
		var me= this;
		me.destinationList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择</option><option value=" "> </option>'];
				
			for(var i=0;i<len;i++){
				htmls.push("<option value='" + list[i].LBDestination_Id + "'>" + list[i].LBDestination_CName + "</option>");
			}
			$("#LBSamplingGroup_DestinationID").html(htmls.join(""));
			form.render('select');
		});
		me.sampleTypeList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择</option><option value=" "> </option>'];
				
			for(var i=0;i<len;i++){
				htmls.push("<option value='" + list[i].LBSampleType_Id + "'>" + list[i].LBSampleType_CName + "</option>");
			}
			$("#LBSamplingGroup_SampleTypeID").html(htmls.join(""));
			form.render('select');
		});
		me.specialtyList(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择</option><option value=" "> </option>'];
			for(var i=0;i<len;i++){
				htmls.push("<option value='" + list[i].LBSpecialty_Id + "'>" + list[i].LBSpecialty_CName + "</option>");
			}
			$("#LBSamplingGroup_SpecialtyID").html(htmls.join(""));
			form.render('select');
		});
        me.tcuveteList('LBSamplingGroup_LBTcuvete_CName','LBSamplingGroup_LBTcuvete_Id');
		callback();

	};
	//获取送检目的地
	Class.pt.destinationList = function(callback){
		var me =this;
		var fields = ['Id','CName'],
			url =  me.config.selectDestinationUrl+ '&where=lbdestination.IsUse=1';
		url += '&fields=LBDestination_' + fields.join(',LBDestination_');
	    url += '&sort=[{property:"LBDestination_DispOrder",direction:"ASC"}]';

		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	//获取样本类型
	Class.pt.sampleTypeList = function(callback){
		var me =this;
		var fields = ['Id','CName'],
			url =  me.config.selectSampleTypeUrl+ '&where=lbsampletype.IsUse=1';
		url += '&fields=LBSampleType_' + fields.join(',LBSampleType_');
		url += '&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	//获取专业
	Class.pt.specialtyList = function(callback){
		var me =this;
		var fields = ['Id','CName'],
			url =  me.config.selectSpecialtyUrl+ '&where=lbspecialty.IsUse=1';
		url += '&fields=LBSpecialty_' + fields.join(',LBSpecialty_');
		url += '&sort=[{property:"LBSpecialty_DispOrder",direction:"ASC"}]';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};

	 //初始化采样管下拉选择项
    Class.pt.tcuveteList = function(CNameElemID, IdElemID) {
    	var me =this;
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','SName','Shortcode'],
			url = me.config.selectTcuveteUrl + "&where=lbtcuvete.IsUse=1";
		url += '&fields=LBTcuvete_' + fields.join(',LBTcuvete_');
		url += '&sort=[{property:"LBTcuvete_DispOrder",direction:"ASC"}]';
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBTcuvete_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbtcuvete.CName,lbtcuvete.Shortcode,lbtcuvete.SName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/简称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'numbers', title: '行号' },
                    { field: 'LBTcuvete_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBTcuvete_CName', minWidth: 150,flex:1,title: '名称', sort: false },
                    { field: 'LBTcuvete_SName', width: 70, title: '简称', sort: false },
                    { field: 'LBTcuvete_Shortcode', width: 70, title: '快捷码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBTcuvete_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBTcuvete_Id"]);				
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    }
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
	//主入口
	SamplingGroupForm.render = function(options){
		var me =  new Class(options);
		me.initSystemSelect(function(){
				//新增
			me.isAdd = Class.pt.isAdd;
			//编辑
			me.IsEdit = Class.pt.isEdit;
			me.initFilterListeners();

		});
	
		return me;
	};
	//暴露接口
	exports('SamplingGroupForm',SamplingGroupForm);
});