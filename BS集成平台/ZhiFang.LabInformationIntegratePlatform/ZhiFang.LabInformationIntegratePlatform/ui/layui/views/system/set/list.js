layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table','form'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//获取列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL';
	//删除数据服务
	var DEL_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_DelIntergrateSystemSet';
	
	//列表配置
	var config = {
		elem: '#table',
		toolbar: '#table-toolbar-top',
		height:'full-40',
		title:'分布式系统设置',
		initSort: {
			field:'DispOrder',
			type:'asc'
		},
		cols: [[
			{type:'checkbox',fixed:'left'},
			{field:'IsUse',width:60,title:'使用',templet:function(d){
				var arr = [
					'<div style="color:#FF5722;text-align:center;">否</div>',
					'<div style="color:#009688;text-align:center;">是</div>'
				];
				var result = d.IsUse == true ? arr[1] : arr[0];
				
				return result;
			}},
			{field:'Id',width:180,title:'ID',hide:true},
			{field:'SystemName',width:150,title:'系统名称'},
			{field:'SystemCode',width:200,title:'系统编码'},
			{field:'SystemHost', width:300,title:'系统主地址'},
			{field:'SystemEntryAddress',width:200,title:'系统入口地址'},
			{field:'PinYinZiTou',width:100,title:'拼音字头'},
			{field:'DispOrder',width:80,title:'排序',sort:true},
			{field:'Memo',minWidth:150,title:'备注'}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		}
	};
	//列表实例
	var tableInd = table.render(config);
	
	//头工具栏事件
	table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'search':onSearch();break;
			case 'add':onAddClick();break;
			case 'edit':onEditClick();break;
			case 'del':onDelClick();break;
		}
	});
	//监听行双击事件
	table.on('rowDouble(table)', function(obj){
		onOpenForm('edit',obj.data);
	});
	form.on('select(IsUse)', function(data){
		onSearch();
	}); 
	//查询
	function onSearch(){
		var IsUse = $('#IsUse option:selected').val(),
			searchValue = $("#search-input").val(),
			searchFields = $("#search-input").attr("fields").split('/');
			
		var where = [];
		if(IsUse){
			where.push("intergratesystemset.IsUse=" + IsUse);
		}
		//系统名称/系统编码/拼音字头
		if(searchValue){
			var searchWhere = [],
				len = searchFields.length;
				
			for(var i=0;i<len;i++){
				searchWhere.push("intergratesystemset." + searchFields[i] + " like '%" + searchValue + "%'");
			}
			searchWhere = searchWhere.join(' or ');
			if(searchWhere.length > 0){
				searchWhere = '(' + searchWhere + ')';
			}
			where.push(searchWhere);
		}
			
		onLoad({"where":where.join(' and ')});
		
		$('#IsUse').val(IsUse);
		$('#search-input').val(searchValue);
	};
	//新增
	function onAddClick(){
		onOpenForm('add');
	}
	//修改
	function onEditClick(){
		//获取选中数据
		var checkStatus = table.checkStatus('table'),
			data = checkStatus.data,
			len = data.length;
			
		if(len != 1){
			layer.msg('请选择一行数据进行操作！');
		}else{
			onOpenForm('edit',data[0]);
		}
	}
	//删除
	var DelErrorList = [];
	function onDelClick(){
		//获取选中数据
		var checkStatus = table.checkStatus('table'),
			data = checkStatus.data,
			len = data.length;
		
		if(len > 0){
			layer.confirm('确定需要删除吗？', function(index){
				var ids = [];
				for(var i=0;i<len;i++){
					ids.push(data[i].Id);
				}
				DelErrorList = [];
				delOneInfo(ids,0);
				layer.close(index);
			});
		}else{
			layer.msg("请选择需要删除的数据！");
		}
	}
	//删除数据
	function delOneInfo(ids,index){
		var id = ids[index];
		
		uxutil.server.ajax({
			url:DEL_INFO_URL + '?id=' + id
		},function(data){
			if(!data.success){
				DelErrorList.push(data.msg);
			}
			if(index < ids.length-1){
				delOneInfo(ids,++index);
			}else{
				if(DelErrorList.length > 0){
					layer.msg(DelErrorList.join('</br>'));
				}else{
					onSearch();
					layer.msg("删除完毕！");
				}
			}
		});
	}
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('IntergrateSystemSet_' + cols[i].field);
		}
			
		tableInd.reload({
			url:GET_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	//打开详细页面
	function onOpenForm(type,data){
		var title = '';
		if(type == 'add'){title = '系统设置-新增';}
		if(type == 'edit'){title = '系统设置-修改';}
		layer.open({
			type:2,
			area:['600px','600px'],
			maxmin:true,
			title:title,
			content:'form.html?t=' + new Date().getTime(),
			success:function(layero,index){
				setTimeout(function(){
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.initData(type,data,function(data){
						layer.close(index);
						onSearch();
					});
				},100);
			}
		});
	} 
	
	//默认查询
	onSearch();
});