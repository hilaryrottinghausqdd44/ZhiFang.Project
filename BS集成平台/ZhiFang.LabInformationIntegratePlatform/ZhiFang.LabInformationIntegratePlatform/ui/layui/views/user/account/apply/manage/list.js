layui.extend({
	uxutil: 'ux/util'
}).use(['table','uxutil','form'], function(){
	var $ = layui.$,
		table = layui.table,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//获取列表服务
	var GET_SACCOUNT_REGISTER_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSAccountRegisterByHQL';
	
	//page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}
	//列表配置
	var config = {
		elem: '#table',
		toolbar: '#table-toolbar-top',
		height:'full-40',
		title: '账号申请数据表',
		initSort:false,
		cols: [[
			{field:'Id', width:180, title: 'ID', sort: true,hide:true},
			{field:'Name', width:100, title: '姓名', sort: true},
			{field:'SexName', width:80, title: '性别', sort: true},
			{field:'MobileTel', width:120, title: '手机号'},
			{field:'ApplyInfo', minWidth:150, title: '备注'},
			{field:'DataAddTime',width:160,title:'产生时间',sort:true},
			{ field: 'StatusName', width: 100, title: '状态', sort: true },
			{ field: 'HospitalName', width: 180, title: '医院名称', sort: false, hide: true },
			{ field: 'ProvinceName', width: 180, title: '省份名称', sort: true, hide: true },
			{ field: 'CityName', width: 180, title: '城市名称', sort: true, hide: true },
			{ field: 'CountyName', width: 180, title: '区县名称', sort: true, hide: true },
			{fixed: 'right', title:'操作', toolbar: '#barDemo', width:80}
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
//			case 'approve':onApproveClick();break;
		}
	});
	//头工具栏事件
	table.on('sort(table)', function(obj){
		var where = [];
		if(obj.type){
			where.sort = '[{"property":"' + obj.field + '","direction":"' + obj.type + '"}]';
		}
		onLoad(where);
	});
	//监听行工具事件
	table.on('tool(table)', function(obj){
	    var data = obj.data;
	    if(obj.event === 'edit')onOpenForm(data);
	});
	//查询
	function onSearch(){
		var where = [];
		var	val = $("#searchText").val(),
			status = $('#status option:selected').val();
	
		//过滤条件：状态
		if(status)where.push("StatusId=" + status);
		var userWhere = [];
		if(val)userWhere.push("Name like '%" + val + "%' or IdNumber like '%" + val + "%' or MobileTel like '%" + val + "%'");
		if(userWhere.length > 0){
			where.push("(" + userWhere.join(" and ") + ")");
		}

		onLoad({"where":where.join(' and ')});
		$("#searchText").val(val);
		$("#status").val(status);
	};
//	//审批
//	function onApproveClick(id){
//		layer.open({
//			title:'账号申请-审批',
//			type:2,
//			content:'approve.html?t=' + new Date().getTime()+'&id='+id,
//			maxmin:true,
//			toolbar:true,
//			resize:true,
//			area:['700px','480px']
//		});
//	}
	//打开详细页面
	function onOpenForm(data) {
		console.log(data);
		layer.open({
			title:'账号申请-审批',
			type:2,
			content:'approve.html?t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['810px','560px'],
			success:function(layero,index){
				setTimeout(function(){
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.initData(data,function(data){
						layer.close(index);
						onSearch();
					});
				},100);
			}
		});
	} 
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push(cols[i].field);
		}
			
		tableInd.reload({
			url:GET_SACCOUNT_REGISTER_LIST_URL,
			where:$.extend({
				isPlanish:false
			},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	
	
	//默认查询
	onSearch();
});