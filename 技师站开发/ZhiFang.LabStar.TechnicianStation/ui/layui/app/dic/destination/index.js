/**
   @Name：送检目的地
   @Author：liangyl
   @version 2021-06-29
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table','form'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//获取列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDestinationByHQL?isPlanish=true';
	//删除数据服务
	var DEL_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBDestination';
	//新增数据服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBDestination';
	//修改数据服务
	var EDIT_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBDestinationByField';
    //表单数据加载
    var GET_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDestinationById?isPlanish=true';
    //提取中文字符串拼音字头
    var GET_PINYIN_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou?chinese=';//获得拼音字头
    //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBDestination&entityField=DispOrder';

    //表单状态
    var formtype = 'add';
    //存储表单临时数据
    var DATA_LIST = [];
    //当前操作ID
    var PK = null;
    
	//列表配置
	var config = {
		elem: '#destination-table',
		height:'full-65',
		title:'送检目的地',
		initSort: {
			field:'LBDestination_DispOrder',
			type:'asc'
		},
		page: true,
		limit: 50,
		size:'sm',
		cols: [[
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'LBDestination_Id',width:180,title:'ID',hide:true},
			{field:'LBDestination_CName',width:150,title:'名称'},
			{field:'LBDestination_SName',width:150,title:'简称'},
			{field:'LBDestination_UseCode',width:100,title:'代码'},
			{field:'LBDestination_PinYinZiTou',width:100,title:'拼音字头'},
			{field:'LBDestination_IsUse',width:60,title:'使用',templet:function(d){
				var arr = [
					'<div style="color:#FF5722;text-align:center;">否</div>',
					'<div style="color:#009688;text-align:center;">是</div>'
				];
				var result = d.LBDestination_IsUse == 'true' ? arr[1] : arr[0];
				
				return result;
			}},
			{field:'LBDestination_DispOrder',width:80,title:'次序',sort:true},
			{field:'LBDestination_Memo',minWidth:150,title:'备注'}
		]],
		loading:true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
	        var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
	                if (res.data[i].LBDestination_Id == PK) {
	              	    rowIndex=i;
	              	    break;
	                }
	            }
	            //默认选择行	
	            var tableDiv = $("#destination-table+div .layui-table-body.layui-table-body.layui-table-main");
		        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		        thatrow.click();
			}
		}
	};
	//列表实例
	var tableInd = table.render(config);
	
	//监听行单击事件
	table.on('row(destination-table)', function(obj){
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	   isEdit(obj.data.LBDestination_Id);
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
	//名称填写完赋值拼音字头和快捷码
    $("input[name='LBDestination_CName']").blur(function () {
        getPinYinZiTou($(this).val(), function (str) {
            $("input[name='LBDestination_Shortcode']").val(str);
            $("input[name='LBDestination_PinYinZiTou']").val(str);
        });
    });
	//初始化
	function init(){
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
				searchWhere.push("lbdestination." + searchFields[i] + " like '%" + searchValue + "%'");
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
    	tableInd.config.instance.layMain.html('<div class="layui-none">暂无相关数据</div>');
    	formtype = 'add';
    	onResetClick();
    	showTypeSign('add');
    }
     //编辑
    function isEdit(id){
    	formtype = 'edit';
    	showTypeSign('edit');
        loadDataByID(id,function(data){
        	DATA_LIST = changeResult(data.ResultDataValue);
            form.val('Destination',DATA_LIST);
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
		$("#Destination :input").each(function(){ 
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
		data = data.replace(/\\u000d\\u000a/g,'').replace(/\\u000a/g,'<br />').replace(/[\r\n]/g,'<br />');
		var list =  JSON.parse(data);
		if(list.LBDestination_IsUse=="false")list.LBDestination_IsUse="";
		var reg = new RegExp("<br />", "g");
		list.LBDestination_Comment = list.LBDestination_Comment.replace(reg, "\r\n");
		return list;
	}
	 /**@overwrite 获取新增的数据*/
	function getAddParams(data) {
		var entity = JSON.stringify(data).replace(/LBDestination_/g, "");
		if(entity) entity = JSON.parse(entity);
		entity.IsUse = entity.IsUse ? 1 :0;
		if(!entity.DispOrder)delete entity.DispOrder;
		if(!entity.Id)delete entity.Id;
		return {entity: entity};
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		
		entity.fields = 'Id,CName,EName,SName,UseCode,StandCode,DeveCode,Shortcode,PinYinZiTou,DispOrder,IsUse,Comment';
		if (data["LBDestination_Id"])
			entity.entity.Id = data["LBDestination_Id"];
		return entity;
	};
	//表单保存处理
	function onSaveClick(data) {
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
				var msg =formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//删除方法 
	function onDelClick(){
		var id = document.getElementById("LBDestination_Id").value;    
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
					layer.msg("删除失败！", { icon: 5, anim: 6 });
				}
			});
        });
	}
	  //重置
    function onResetClick() {
        var me = this;
        if(formtype=='add'){
			$("#Destination").find('input[type=text],select,textarea,input[type=hidden],input[type=number]').each(function() {
	           $(this).val('');
	        });
		    getMaxNo(function (val) {
	            document.getElementById('LBDestination_DispOrder').value = val;
	        });
	        if (!$("#LBDestination_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBDestination_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBDestination_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBDestination_IsUse")[0].checked = true;
            }
        }else{
			form.val('Destination',DATA_LIST);
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
	 //拼音字头
    function getPinYinZiTou(val, callback) {
        var me = this;
        var url = GET_PINYIN_LIST_URL + encodeURI(val);
        if (val == "") {
            if (typeof (callback) == "function") {
                callback(val);
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
               layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }

});