layui.config({
	base:'../../../admin/layuiadmin/' //静态资源所在路径
}).extend({
	uxutil: '../../../ux/util'
}).use(['table','uxutil'], function(){
	//获取订单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL';
	
	
	var $ = layui.$,
		table = layui.table;
		
	table.render({
		elem: '#test-table-toolbar',
		url: GET_LIST_URL,
		toolbar: '#test-table-toolbar-toolbarDemo',
		title: '用户数据表',
		cols: [[
			{field:'CName', width:180, title: '消息类型名称', sort: true},
			{field:'Code', width:160, title: '消息类型代码', sort: true},  
			{field:'SystemCName', width:110, title: '所属系统名称', sort: true},
			{field:'SystemCode', width:100, title: '所属系统代码', sort: true},
			{field:'Url', width:200, title: '展现程序地址', sort: true},
			{field:'Visible', width:110, title: '是否可用', sort: true},
			{field:'Memo', width:150, title: '备注', sort: true},
			{width:80,align:'center',fixed:'right',toolbar:'#test-table-operate-barDemo'}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据1 
			if(!ADD_ORDER_INFO){
				ALL_ORDER_INFO = res.data;
			}else{
				ALL_ORDER_INFO.push(ADD_ORDER_INFO);
			}
			
			return {
				"code": res.code, //解析接口状态
				"msg": res.msg, //解析提示文本
				"count": res.count, //解析数据长度
				"data": ALL_ORDER_INFO //解析数据列表
			};
		}
	});
	
	//头工具栏事件
	table.on('toolbar(test-table-toolbar)', function(obj){
		var checkStatus = table.checkStatus(obj.config.id);
		switch(obj.event){
			case 'getOrderData': showAddOrderWin();break;
		};
	});
	//监听工具条
	table.on('tool(test-table-toolbar)', function(obj){
		var data = obj.data;
		if(obj.event === 'show'){
			onShowOrderInfoWin(data.OrderDocNo);
		}
	});
	function onShowOrderInfoWin(barcode){
		layer.open({
			type:2,
			title:'订单信息',
			shadeClose:true,
			shade:0.8,
			area:['90%','90%'],
			content: 'info.html?barcode=' + barcode
		}); 
	}
	
	//显示获取订单信息页面
	function showAddOrderWin(){
		layer.prompt({
			title: '获取订单'
		},function(value,index){
			onLoadOrderDataByBarcode(value);
		});
	}
	//根据条码获取订单信息
	function onLoadOrderDataByBarcode(value){
		var list = ALL_ORDER_INFO,
			len = list.length;
			
		for(var i=0;i<len;i++){
			if(list[i].OrderDocNo == value){
				layer.alert("订单已存在！");
				return;
			}
		}
		
		var url = GET_ORDER_INFO_BY_BARCODE_URL + value + '.json?v=' + new Date().getTime();
		JcallShell.Server.ajax({
			url:url
		},function(data){
			if(data){
				if(data.success){
					onReloadTable(data.value.Info);
				}else{
					layer.msg('根据订单编码未找到任何信息！');
				}
			}else{
				layer.msg(data.msg);
			}
		});
	}
	function onReloadTable(data){
		ADD_ORDER_INFO = data;
		table.reload('test-table-toolbar');
	}
});