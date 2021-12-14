$(function() {
	//获取页面传递参数
	var params = Shell.util.Page.getParams(true);
	//如果参数不存在，不让使用
	if(!params || !params.WEBLISSOURCEORGID || !params.WEBLISSOURCEORGNAME || !params.CONSUMERAREAID){
		$("body").html('<div style="padding:20px;text-align:center;"><b>没有传递正确参数，不能进行功能访问！</b></div>');
		return;
	}
	
	//列表数据
	LIST_DATA = [];
	
	//获取已锁定消费码列表服务
	var SELECT_URL = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/SearchUnConsumerUserOrderFormList";
	//取消消费采样服务
	var UN_CONSUME_URL = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/UnConsumerUserOrderForm";
	$("#list").datagrid({
		rownumbers:true,//行号
		fitColumns:false,
		toolbar:'#toolbar',
		emptyMsg:'没有找到记录！',
		columns: [[
			{field:'ConsumerStartTime',title:'消费开始时间',width:130},
			{field:'DoctorName',title:'医生姓名',width:80},
			{field:'UserName',title:'用户姓名',width:80},
			{field:'PayCode',title:'消费码',width:120},
			{field:'UOFCode',title:'用户订单编号',width:120},
			{field:'OS_UserConsumerFormCode',title:'消费单编号',width:120},
			{field:'_operate',title:'操作',formatter: function(value,row,index){
				return '<a href="#" onclick="onUnConsumeByPayCode(' + row.PayCode + ')">解锁</a>'; 
			}}
		]]
	});
	
	//查询按钮监听
	$('#search').click(function() {
		onSearch();
	});
	//当按下回车键时查询	
	$('#payCode').textbox('textbox').keydown(function(e) {
		if(e.keyCode == 13) {
			onSearch();
		}
	});
	//查询数据
	function onSearch() {
        var entity= {
        	PayCode:$('#payCode').textbox('getValue'),
        	WeblisSourceOrgID:params.WEBLISSOURCEORGID,
    		WeblisSourceOrgName:params.WEBLISSOURCEORGNAME,
    		ConsumerAreaID:params.CONSUMERAREAID
        };
        onMaskShow('数据加载中，请稍候……');//弹出遮罩层
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: SELECT_URL,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
            	onMaskHide();//取消遮罩层
                if (result.success) {
                	var value = result.ResultDataValue || "[]";
                	onLoadSuccess(Shell.util.JSON.decode(value));
                } else {
                	onLoadError(result.ErrorInfo);
                }
            },
            error: function (request, strError) {
            	onMaskHide();//取消遮罩层
            	onLoadError(strError);
            }
        });
	}
	//根据下标取消锁定
	function onUnConsumeByPayCode(PayCode){
		$.messager.confirm('提示', '是否解锁选中数据?', function (r) {
			if(!r) return;
			onUnConsume(PayCode,function(success,msg){
				if(success){
					$.messager.alert("", "解锁成功！", "info");
					
					var list = LIST_DATA,
						len = list.length;
					for(var i=0;i<len;i++){
						if(list[i].PayCode == PayCode){
							list.splice(i,1);break;
						}
					}
					onLoadSuccess(list);
				}else{
					$.messager.alert("", msg, "error");
				}
			});
		});
	}
	window.onUnConsumeByPayCode = onUnConsumeByPayCode;
	//取消锁定
	function onUnConsume(payCode,callback){
        onMaskShow('消费码锁定取消中，请稍候……');//弹出遮罩层
    	
        var entity= {
        	PayCode:payCode,
        	WeblisSourceOrgID:params.WEBLISSOURCEORGID,
        	WeblisSourceOrgName:params.WEBLISSOURCEORGNAME,
        	ConsumerAreaID:params.CONSUMERAREAID
        };
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: UN_CONSUME_URL,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
            	onMaskHide();//取消遮罩层
                if (result.success) {
                	callback(true);
                } else {
                	callback(false,result.ErrorInfo);
                }
            },
            error: function (request, strError) {
            	onMaskHide();//取消遮罩层
            	callback(false,strError);
            }
        });
	}
	//弹出遮罩层
	function onMaskShow(msg) {
		msg = '<div style="text-align:center;">' + msg + '</div>';
		$.messager.progress({
			//title: '提示', 
			msg: msg,
			text: '' 
		});
	}
	//取消遮罩层  
	function onMaskHide() {
		$.messager.progress('close');
	}
	//加载成功处理
	function onLoadSuccess(data){
		LIST_DATA = data;
		$('#list').datagrid("loadData",data);
	}
	//加载失败处理
	function onLoadError(msg){
		$.messager.alert("", msg, "error");
	}
	//加载数据
	onSearch();
});