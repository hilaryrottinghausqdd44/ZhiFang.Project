layui.config({
	base:'../../layuiadmin/' //静态资源所在路径
}).extend({
	index:'lib/index',//主入口模块
	uxutil: '../../../ux/util'
}).use(['index','table','uxutil'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		admin = layui.admin,
		table = layui.table;
		
	//获取人员列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/WcfService.svc/GetOnlineUserList?v=' + new Date().getTime();
		
	function initTable(){
		table.render({
			elem: '#UserTable',
			url: GET_LIST_URL,
			title: '用户数据表',
			toolbar: '#UserTableToolbar',
			cols: [[
				{type:'checkbox', fixed: 'left'},
				{field:'ClientName', minWidth:100, title: '人员姓名', sort: true},
				{field:'LoginTime', width:160, title: '登录时间', sort: true},
				{field:'ClientTypeFlagName', width:70, title: '类型', sort: true}
			]],
			loading:true,
			//page: true,
			parseData: function(res){ //res 即为原始返回的数据
				res = $.parseJSON(res);
				for(var i in res){
					res[i].ClientTypeFlagName = 
						res[i].ClientTypeFlag == 0 ? "用户" :
						res[i].ClientTypeFlag == 1 ? "仪器站点" : "站点";
				}
				return {
					"code": 0, //解析接口状态
					"msg": "", //解析提示文本
					"count": res.length, //解析数据长度
					"data": res
				};
			}
		});
		//头工具栏事件
		table.on('toolbar(UserTable)', function(obj){
			var checkStatus = table.checkStatus(obj.config.id);
			switch(obj.event){
				case 'onRefresh': table.reload('UserTable');break;
			};
		});
	}
	//获取选中的用户列表
	function getCheckedUserList(){
		var checkStatus = table.checkStatus('UserTable'),
			list = checkStatus.data;
			
		return list;
	}
	//获取选中的用户名称列表
	function getCheckedUserNameList(){
		var list = getCheckedUserList(),
			len = list.length,
			NameList = [];
			
		for(var i=0;i<len;i++){
			NameList.push(list[i].ClientName);
		}
			
		return NameList;
	}
	//初始化用户信息
	function initUserInfo(){
		var ACCOUNTNAME = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME) || '无';
		$("#UserName").html(ACCOUNTNAME);
		
		//初始化链接
		initConnection();
	}
	//初始化链接
	function initConnection(){
		var ACCOUNTNAME = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME) || '无';
		var chat = $.connection.publicHub;
        //私信
		chat.client.ReceiveMessage = function (name, message) {
			var encodedName = $('<div />').text(name).html(); 
			var encodedMsg = $('<div />').text(message).html();
			$('#discussion').prepend(
				'<dd>' +
					'<div class="layui-status-img" style="background-color:#FF5722;color:#ffffff;text-align:center;line-height:32px;">' +
						'私' +
					'</div>' +
					'<div>' +
						'<p>' + encodedName +' 说：' + encodedMsg + '</p>' +
						'<span>' + JcallShell.Date.toString(new Date()) + '</span>' +
					'</div>' +
					'</dd>' +
				'</dd>'
			);
		};
		$('#displayname').val(ACCOUNTNAME);
		$('#message').focus();
		$.connection.hub.start().done(function () {
			//初始化用户列表
			initTable();
            //私信
			$('#PrivateButton').click(function () {
				var userNameList = getCheckedUserNameList();
				var msg = $('#message').val();
				if(!msg){
					layer.tips('请输入信息',this,{tips:1,time:2000});
					$('#message').focus();
					return;
				}
				if(userNameList && userNameList.length > 0){
					chat.server.sendMessages(userNameList,msg,$('#displayname').val());
					$('#message').val('').focus();
				}else{
					layer.tips('请选择需要私信的人员',this,{tips:1,time:2000});
					$('#message').focus();
				}
			});
		});
	}
	//初始化用户信息
	initUserInfo();
});