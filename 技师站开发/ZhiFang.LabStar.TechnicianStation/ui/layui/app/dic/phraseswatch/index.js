/**
   @Name：拒收短语维护
   @Author：liangyl
   @version 2021-07-26
      说明，处理意见与让步没有关系，拒收原因有关系
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table','form'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//获取短语类型列表服务
	var GET_PHRASESWATCH_TYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassByHQL?isPlanish=false';
	//获取拒收短语列表服务
	var GET_PHRASESWATCH_CLASS_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassItemByHQL?isPlanish=true';
	//查拒收短语
	var GET_PHRASESWATCH_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchByHQL?isPlanish=false';
	//删除短语类型数据服务
	var DEL_PHRASESWATCH_TYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatchClass';
	//删除短语数据服务
	var DEL_PHRASESWATCH_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatch';
	//删除短语关系数据服务
	var DEL_PHRASESWATCH_CLASS_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatchClassItem';

	//新增短语类型数据服务
	var ADD_PHRASESWATCH_TYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatchClass';
	//新增短语数据服务
	var ADD_PHRASESWATCH_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatch';
	//拒收让步短语类型明细
	var ADD_PHRASESWATCH_CLASS_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatchClassItem';

	//修改短语类型服务
	var EDIT_PHRASESWATCH_TYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhrasesWatchClassByField';
    //修改短语数据服务
	var EDIT_PHRASESWATCH_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhrasesWatchByField';
    //选择行的数据
    var DATA_LIST = {},
        formtype = 'add';
    var PHRASESWATCH_TYPE_LIST = [];
    var PK ="";//用于新增和修改定位行；
    
	//列表配置
	var config = {
		elem: '#table',
		height:'full-72',
		title:'短语',
		page: true,
		limit: 50,
		loading : true,
		size:'sm',
		cols: [[
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'CName',minWidth:150,flex:1,title:'名称'},
			{field:'PhrasesType',minWidth:100,flex:1,title:'类型'},
			{field:'Id',width:180,title:'ID',hide:true},
			{field:'PhrasesWatchClassID',width:180,title:'拒收类型ID',hide:true},
			{field:'PhrasesWatchID',width:180,title:'拒收原因ID',hide:true}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			if($("#phrasetype").val()=='1'){//拒收原因
				if(data.list)data.list = changeResult(data.list);
			}
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		text: {none: '暂无相关数据' },
		done: function(res, curr, count) {
			if($('#phrasetype').val()=='4')PHRASESWATCH_TYPE_LIST = res.data;
			setTimeout(function(){
				var rowIndex = 0;
				for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].Id == PK) {
	              	    rowIndex=i;
	              	    break;
	                }
	            }
				var tr = tableInd.config.instance.layBody.find('tr:eq('+rowIndex+')');
				if(tr.length > 0){
					tr.click();
				}else{
					DATA_LIST = [];
				}
			},0);
		}
	};
	//列表实例
	var tableInd = table.render(config);
	//短语类型选择
	form.on('select(phrasetype)', function(data){
		if(data.value=='1'){//拒收原因
			tableInd.config.cols[0][2].hide = false;
			$('#row_phrasetype').removeClass('layui-hide');
		}else{   //拒收类型
			tableInd.config.cols[0][2].hide = true;
			$('#row_phrasetype').addClass('layui-hide');
		}
		//列表数据加载
	    onSearch();
	});      
      
	//监听行单击事件
	table.on('row(table)', function(obj){
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    DATA_LIST = obj.data;
	});
	//按钮事件
	var active = {
		//新增
		add: function() {
			formtype = 'add';
			onResetClick('add');
		},
		//修改
		edit: function() {
			formtype = 'edit';
			onResetClick('edit');
		},
		//删除关系
		del: function() {
			onDelClick();
		}
	};
	$('.phraseswatch .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});

	//保存
	form.on('submit(save)',function(obj){
		if($("#phrasetype").val()=='1'){//拒收原因
			var PhrasesWatchClassID = $('#PhrasesWatchClassID').val();
			if(!PhrasesWatchClassID){
				layer.msg('原因类型不能为空!',{ icon: 5, anim: 6 });
				return false;
			}
			if(formtype =='edit'){
				//先删除关系，再新增保存
				delPhrasesWatchClassItem(function(){
					onSaveClick(obj);
				});
			}else{
				onSaveClick(obj);
			}
	    }else{
	    	onSaveClick(obj);
	    }
	});
	
	//列表查询
	function onSearch(){
		//表单状态重置为新增
		formtype = 'add';
		onResetClick('add');
		
		var where = [],url="";
		if($("#phrasetype").val() =='1'){//拒收让步短语类型
			where.push('lbphraseswatchclassitem.LBPhrasesWatch.PhrasesType='+$("#phrasetype").val());
		}
		if($("#phrasetype").val() !='1' && $("#phrasetype").val() !='4'){//让步与处理意见
			where.push('lbphraseswatch.PhrasesType='+$("#phrasetype").val());
		}
		onLoad({"where":where.join(' and ')});
		
	}
    //获取短语类型
	function PhrasesWatchClassList(callback){
		var fields = ['Id','CName'],
			url = GET_PHRASESWATCH_TYPE_LIST_URL + '&where=lbphraseswatchclass.IsUse=true';
		url += '&fields=Id,CName';
		
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
	}
    /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var me = this;
		var list =[];
		for(var i=0;i<data.length;i++){
		   list.push({
		   	   Id:data[i].LBPhrasesWatchClassItem_Id,
		   	   PhrasesType:data[i].LBPhrasesWatchClassItem_LBPhrasesWatchClass_CName,
		       CName:data[i].LBPhrasesWatchClassItem_LBPhrasesWatch_CName,
		       PhrasesWatchID:data[i].LBPhrasesWatchClassItem_LBPhrasesWatch_Id,
		       PhrasesWatchClassID:data[i].LBPhrasesWatchClassItem_LBPhrasesWatchClass_Id

		   });
		}
		return list;
	}
	 /**@overwrite 获取新增的数据*/
	function getAddParams(data) {
		var entity = {
			IsUse:1,
			CName:data.CName
		};
		return {entity: entity};
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		
		entity.fields = 'Id,CName';
		if (data["Id"])
			entity.entity.Id = data["Id"];
		if($('#phrasetype').val()=='1'){//拒收原因id 取PhrasesWatchID
			if (DATA_LIST.PhrasesWatchID)
			entity.entity.Id = DATA_LIST.PhrasesWatchID;
		}
		return entity;
	};
    
	//表单保存处理
	function onSaveClick(data) {
		var params = formtype == 'add' ? getAddParams(data.field) : getEditParams(data.field);
		if (!params) return false;
		if($("#phrasetype").val()=='4'){
			var url = formtype == 'add' ? ADD_PHRASESWATCH_TYPE_URL : EDIT_PHRASESWATCH_TYPE_URL;
		}else{
			var url = formtype == 'add' ? ADD_PHRASESWATCH_URL : EDIT_PHRASESWATCH_URL;
			params.entity.PhrasesType = $("#phrasetype").val();
		}
		var id = params.entity.Id;
		params = JSON.stringify(params);
		var index = layer.load();
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config1, function(data) {
			layer.close(index);
			//隐藏遮罩层
			if (data.success) {
				if(formtype=='add')id = data.value ? data.value.id : "";
				PK = id;
				if($("#phrasetype").val()=='1'){
					addLink(PK);
				}else{
					layer.msg("保存成功！", { icon: 6, anim: 0 ,time:2000});
					onSearch();
				}
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//新增关系
	function addLink(Id){
		if(!Id)return false;
		var entity={
			LBPhrasesWatchClass:{
	    		Id:$('#PhrasesWatchClassID').val(),
	    		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        },
	        LBPhrasesWatch:{
	    		Id:Id,
	    		DataTimeStamp:[0,0,0,0,0,0,0,0]
	    	}
		};

		var params = JSON.stringify({entity:entity});
		//显示遮罩层
		var config = {
			type: "POST",
			url:ADD_PHRASESWATCH_CLASS_ITEM_URL ,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
		    layer.close(index);
			//隐藏遮罩层
			if (data.success) {
				layer.msg("保存成功！", { icon: 6, anim: 0 ,time:2000});
				PK = data.value.id || '';
				onSearch();
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//删除方法 
	function onDelClick(){
		var id = DATA_LIST.Id;    
        if(!id)return;
        if($("#phrasetype").val()=='1'){//拒收原因关系
			var url = DEL_PHRASESWATCH_CLASS_ITEM_URL+'?id='+ id;
		}else{
			var url = DEL_PHRASESWATCH_TYPE_URL+'?id='+ id;
		}
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					if($("#phrasetype").val()=='1'){ //还需要再删拒收原因
						delPhrasesWatch();
					}else{
						layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                        onSearch();
					}
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	}
	//删除短原因语方法 
	function delPhrasesWatch(){
		var id = DATA_LIST.PhrasesWatchID;    
        if(!id)return;
        var url = DEL_PHRASESWATCH_URL+'?id='+ id;
        var index = layer.load();
	    uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.close(index);
			if(data.success === true) {
                layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                onSearch();
			}else{
				layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
			}
		});
	}
	//删除原因关系法 
	function delPhrasesWatchClassItem(callback){
		var id = DATA_LIST.Id;    
        if(!id)return;
        var url = DEL_PHRASESWATCH_CLASS_ITEM_URL+'?id='+ id;
        var index = layer.load();
	    uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.close(index);
			if(data.success === true) {
                callback();
			}else{
				layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
			}
		});
	}
	  //重置
    function onResetClick(formtype) {
        var me = this;
        $("#PhrasesWatchClassID").html(comList());
        if(formtype=='add'){
			$("#phrase").find('input[type=text],select,input[type=hidden],input[type=number]').each(function(i, item) {
	           if(item.name!='phrasetype')$(this).val('');
	        });
		}else{
			form.val('phrase',DATA_LIST);
		}
        form.render();
    }
    //下拉框数据
	function comList() {
		var list = [],
			htmls = ['<option value="">请选择类型</option>'];
		for(var i=0;i<PHRASESWATCH_TYPE_LIST.length;i++){
		 	htmls.push("<option value='" + PHRASESWATCH_TYPE_LIST[i].Id +"'>" + PHRASESWATCH_TYPE_LIST[i].CName + "</option>");
		}
		return htmls.join("");
	}
	//加载数据
	function onLoad(whereObj,url){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		if($("#phrasetype").val()=='1'){//查拒收原因表
		    var url = GET_PHRASESWATCH_CLASS_ITEM_LIST_URL;
		    fields = ['LBPhrasesWatchClassItem_LBPhrasesWatchClass_CName','LBPhrasesWatchClassItem_LBPhrasesWatch_CName','LBPhrasesWatchClassItem_LBPhrasesWatch_Id','LBPhrasesWatchClassItem_Id','LBPhrasesWatchClassItem_LBPhrasesWatchClass_Id' ];
		}else if($("#phrasetype").val()=='4'){//查拒收类型表
			var url = GET_PHRASESWATCH_TYPE_LIST_URL;
		}else{ //让步与处理意见
			var url = GET_PHRASESWATCH_LIST_URL;
		}
		tableInd.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	//初始化
	function init(){
		//加载原因类型下拉数据
		PhrasesWatchClassList(function(list){
			PHRASESWATCH_TYPE_LIST =list; 
			$("#PhrasesWatchClassID").html(comList());
			form.render('select');
		});
		//列表数据加载
		onSearch();
	}
	//初始化
	init();
});