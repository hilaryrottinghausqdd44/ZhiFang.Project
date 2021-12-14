/**
   @Name：分发规则类型
   @Author：liangyl
   @version 2021-08-12
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table','form'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//获取列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeByHQL?isPlanish=true';
	//删除数据服务
	var DEL_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBTranRuleType';
	//新增数据服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleType';
	//修改数据服务
	var EDIT_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleTypeByField';
    //表单数据加载
    var GET_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeById?isPlanish=true';
    //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBTranRuleType&entityField=DispOrder';
     //是否超级管理员权限
    var GET_ROLE_EMP_DELBTN_URL= uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_GetModuleFunRoleByEmpId';
     
    //表单状态
    var formtype = 'add';
    //存储表单临时数据
    var DATA_LIST = [];
    //当前操作ID
    var PK = null;
     //外部参数
	var PARAMS = uxutil.params.get(true);
	var MODULEID = PARAMS.MODULEID;
    //列表实例
    //初始化列表
	var config = {
		elem: '#tranruletype_table',
		height:'full-65',
		title:'分发规则类型',
		initSort: {
			field:'LBTranRuleType_DispOrder',
			type:'asc'
		},
		page: true,
		limit: 50,
		loading : true,
		size:'sm',
		cols: [[
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'LBTranRuleType_Id',width:180,title:'ID',hide:true},
			{field:'LBTranRuleType_CName',minWidth:100,flex:1,title:'名称'},
			{field:'LBTranRuleType_IsUse',width:60,title:'使用',templet:function(d){
				var arr = [
					'<div style="color:#FF5722;text-align:center;">否</div>',
					'<div style="color:#009688;text-align:center;">是</div>'
				];
				var result = d.LBTranRuleType_IsUse == 'true' ? arr[1] : arr[0];
				
				return result;
			}},
			{field:'LBTranRuleType_DispOrder',width:80,title:'次序',sort:true}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var  ResultDataValue = res.ResultDataValue;
			ResultDataValue = ResultDataValue.replace(/[\r\n]/g, '<br />');
            var data = ResultDataValue ? $.parseJSON(ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		text: {none: '暂无相关数据' },
		done: function(res, curr, count) {
			if(count==0)isAdd();
			if(count>0){
				//默认选择第一行
				var rowIndex = 0;
	            for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].LBTranRuleType_Id == PK) {
	              	    rowIndex=i;
	              	    break;
	                }
	            }
	            //默认选择行	
	            var tableDiv = $("#tranruletype_table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}
		}
	};
	//列表实例
	var tableInd = table.render(config);
	//监听行单击事件
	table.on('row(tranruletype_table)', function(obj){
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    PK = obj.data.LBTranRuleType_Id;
	    isEdit(obj.data.LBTranRuleType_Id);
	});
    //列表查询
    form.on('submit(search)', function (data) {
    	onSearch();
    });
    //回车事件
    $("#search-input").on('keydown', function (event) {
        if (event.keyCode == 13) {
        	onSearch();
            return false;
        }
    });
    $('#add').on('click',function(){
		isAdd();
	});
	$('#edit').on('click',function(){
		isEdit(PK);
	});
	//保存
	form.on('submit(save)',function(obj){
		onSaveClick(obj);
	});
	//重置
	$('#reset').on('click',function(){
		onResetClick();
	});
	//删除
	$('#del').on('click',function(){
		onDelClick();
	});
	//初始化
	function init(){
		IsDelBtn(function (value) {
			if(value=='1')$('#del').removeClass('layui-hide');
		});
		onSearch();
	}
	//初始化
	init();
	//列表查询
	function onSearch(){
		var searchValue = $("#search-input").val(),
			searchFields = $("#search-input").attr("fields").split('/');
		var where = [];
		//名称/简称
		if(searchValue){
			var searchWhere = [],
				len = searchFields.length;
				
			for(var i=0;i<len;i++){
				searchWhere.push("lbtranruletype." + searchFields[i] + " like '%" + searchValue + "%'");
			}
			searchWhere = searchWhere.join(' or ');
			if(searchWhere.length > 0){
				searchWhere = '(' + searchWhere + ')';
			}
			where.push(searchWhere);
		}
		onLoad({"where":where.join(' and ')});
		$('#search-input').val(searchValue);
	};
    
    //新增
    function isAdd(){
    	formtype = 'add';
    	onResetClick();
    	showTypeSign('add');
    	$('#save').removeClass('layui-hide');
    	$('#edit').removeClass('layui-hide');
    }
     //编辑
    function isEdit(id){
    	formtype = 'edit';
    	showTypeSign('edit');
        loadDataByID(id,function(data){
        	$('#save').removeClass('layui-hide');
	    	$('#edit').removeClass('layui-hide');
		    
        	DATA_LIST = changeResult(data.ResultDataValue);
        	if(DATA_LIST.LBTranRuleType_CName=="默认规则"){
		    	$('#save').addClass('layui-hide');
		    	$('#edit').addClass('layui-hide');
		    }
            form.val('TranRuleType',DATA_LIST);
        });
    }
    //显示编辑新增标识
    function showTypeSign(type) {
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
    }
     /**获取表单数据字段*/
	function getStoreFields() {
		var fields = [];
		$("#TranRuleType :input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	}
    //加载表单数据	
	function loadDataByID(id,callback){
		var me = this;
		var url = GET_INFO_LIST_URL + '&id=' + id+
		'&fields='+getStoreFields().join(',');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				callback(data);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
    /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var me = this;
		data = data.replace(/\\u000d\\u000a/g,'').replace(/\\u000a/g,'<br />').replace(/[\r\n]/g,'<br />');
		var list =  JSON.parse(data);
		if(list.LBTranRuleType_IsUse=="false")list.LBTranRuleType_IsUse="";
					var reg = new RegExp("<br />", "g");
		list.LBTranRuleType_Comment = list.LBTranRuleType_Comment.replace(reg, "\r\n");
		return list;
	}
	 /**@overwrite 获取新增的数据*/
	function getAddParams(data) {
		var entity = JSON.stringify(data).replace(/LBTranRuleType_/g, "");
		if(entity.Comment){
			entity.Comment = entity.Comment.replace(/\\/g, '&#92');
			entity.Comment = entity.Comment.replace(/[\r\n]/g, '<br />');
		}
		if(entity) entity = JSON.parse(entity);
		if(entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if(!entity.DispOrder)delete entity.DispOrder;
		if(!entity.Id)delete entity.Id;
		return {entity: entity};
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		
		entity.fields = 'Id,CName,Comment,DispOrder,IsUse';
		if (data["LBTranRuleType_Id"])
			entity.entity.Id = data["LBTranRuleType_Id"];
		return entity;
	};
	//表单保存处理
	function onSaveClick(data) {
	    //不能保存名称为默认规则的数据、
	    var CName = data.field.LBTranRuleType_CName;
	    //去掉前后空格
	    CName = CName.replace(/(^\s*)|(\s*$)/g, "");
	    if(CName == "默认规则"){
	    	layer.msg('已经存在默认规则类型,不能保存!',{icon:5});
	    	return  false;
	    }
		var url = formtype == 'add' ? ADD_INFO_URL : EDIT_INFO_URL;
		var params = formtype == 'add' ? getAddParams(data.field) : getEditParams(data.field);
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
				PK = formtype == 'add' ? data.value.id : id;
				PK += '';
				onSearch();
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//删除方法 
	function onDelClick(){
		var id = document.getElementById("LBTranRuleType_Id").value;    
        if(!id)return;
    	var url = DEL_INFO_URL+'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    onSearch();
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	}
	  //重置
    function onResetClick() {
        var me = this;
        if(formtype=='add'){
			$("#TranRuleType").find('input[type=text],select,textarea,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
	        getMaxNo(function (val) {
	            document.getElementById('LBTranRuleType_DispOrder').value = val;
	        });
	        if (!$("#LBTranRuleType_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTranRuleType_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTranRuleType_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBTranRuleType_IsUse")[0].checked = false;
            }
		}else{
			form.val('TranRuleType',DATA_LIST);
		}
        form.render();
    }
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push(cols[i].field);
		}
			
		tableInd.reload({
			url:GET_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	 //获取指定实体字段的最大号
    function getMaxNo(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: GET_MAXNO_URL
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
                layer.msg(data.msg);layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }
        //生成下一样本号
    function IsDelBtn(callback){ 
     	var entity={
			moduleid:MODULEID,
			code:'DEL' 
		};
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: GET_ROLE_EMP_DELBTN_URL,
			data:  JSON.stringify(entity)
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			layer.close(index);
			//隐藏遮罩层
			if(data.success){
			   var value = data.value || "";
			   callback(value);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
   
});