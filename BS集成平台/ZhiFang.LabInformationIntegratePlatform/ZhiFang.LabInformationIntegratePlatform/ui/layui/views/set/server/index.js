layui.extend({
	uxutil:'ux/util'
}).use(['form','uxutil'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//获取系统列表服务
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL';
	//获取系统数据字段
	var SYSTEM_FIELDS = ['Id','SystemName','SystemCode','SystemHost','DispOrder'];
	//系统列表
	var SYSTEM_LIST = [];
	
	//获取系统列表
	function getSystemList(callback){
		var index = layer.load();
		uxutil.server.ajax({
			url:GET_SYSTEM_LIST_URL,
			data:{
				fields:'IntergrateSystemSet_' + SYSTEM_FIELDS.join(',IntergrateSystemSet_'),
				where:'intergratesystemset.IsUse=1'
			}
		},function(data){
			layer.close(index);
			if(data.success){
				SYSTEM_LIST = (data.value || {}).list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//初始化系统卡片
	function initSystemCards(){
		//获取系统列表
		getSystemList(function(){
			for(var i in SYSTEM_LIST){
				if(SYSTEM_LIST[i].SystemCode == 'ZF_LIIP') continue;
				//创建系统卡片HTML
				createSystemCardHtml(SYSTEM_LIST[i]);
				//初始化系统的数据库信息
				initSystemDatabaseInfo(SYSTEM_LIST[i]);
			}
		});
	};
	//创建系统卡片HTML
	function createSystemCardHtml(systemInfo){
		var html = [];
		html.push('<div class="layui-col-xs12 layui-col-sm6 layui-col-md4 layui-col-lg3">');
		html.push('<div class="card-text">');
		
		html.push('<div class="card-text-top">');
		html.push('<i class="layui-icon layui-icon-website"></i>');
		html.push('<a>' + systemInfo.SystemName + '</a>');
		html.push('</div>');
		
		html.push('<p class="card-text-center" id="' + systemInfo.Id + '">');
		html.push('<div class="loading"><div class="loading-bg"></div></div>');
		html.push('</p>');
		
		html.push('<p class="card-text-bottom">');
		html.push('<a class="loading">' + systemInfo.SystemCode + '</a>');
		html.push('<span>' + systemInfo.DispOrder + '</span>');
		html.push('</p>');
		
		html.push('</div>');
		html.push('</div>');
		
		$("#cards_div").append(html.join(""));
	};
	//初始化系统数据库信息
	function initSystemDatabaseInfo(systemInfo){
		var url = uxutil.path.LOCAL + '/' + systemInfo.SystemHost + '/ServerWCF/CommonService.svc/GetSystemVersion';
		//获取系统数据库信息
		getSystemDatabaseInfo(url,function(result){
			initSystemDatabaseInfoHTML(systemInfo.Id,result);
		});
	};
	//初始化系统数据库信息HTML
	function initSystemDatabaseInfoHTML(systemId,result){
		var server = '',
			database = '',
			html = [];
			
		if(result.success){
			server = (result.value || {}).DBVersion || '';
			database = (result.value || {}).SYSVersion || '';
				
			html.push('<a>服务版本:' + server + '</a></br>');
			html.push('<a>数据库版本:<span' + (server != database ? ' style="color:#FF5722;"' : '') +'>' + database + '</span></a>');
			
			//服务与数据库版本不一致
			if(server != database){
				html.push('<button type="button" class="layui-btn" data="' + systemId + '">升级</button>');
			}else{
				html.push('<button type="button" class="layui-btn layui-btn-disabled">升级</button>');
			}
		}else{
			if(result.msg == '地球上找不到这个地址'){
				html.push('<a style="color:#d2d2d2;">该系统不具备升级功能</a>');
				html.push('<button type="button" class="layui-btn layui-btn-disabled">升级</button>');
			}else{
				html.push('<div style="color:#FF5722;">' + result.msg + '</div>');
			}
		}
		//显示数据库当前服务和数据库版本信息
		$("#" + systemId).html(html.join(''));
		
		if(result.success){
			//服务于数据库版本不一致
			if(server != database){
				//更改可升级标志
				$("#" + systemId).prev().append('<span style="color:#FF5722;">可升级</span>');
				//初始化升级按钮监听
				initButtonListeners(systemId);
			}else{
				//更改可升级标志
				$("#" + systemId).prev().append('<span style="color:#009688;">最新版本</span>');
			}
		}else{
			//更改可升级标志
			$("#" + systemId).prev().append('<span style="color:#c2c2c2">无版本信息</span>');
		}
	};
	//初始化升级按钮监听
	function initButtonListeners(systemId){
		$("#" + systemId).find("button").on("click",function(){
			updateDatabase($(this).attr("data"),function(result){
				if(result.success){
					layer.msg("升级成功！");
					var systemInfo = null;
					for(var i in SYSTEM_LIST){
						if(SYSTEM_LIST[i].Id == systemId){
							systemInfo = SYSTEM_LIST[i];
							break;
						}
					}
					initSystemDatabaseInfoHTML(systemInfo);
				}else{
					layer.msg(result.msg);
				}
			});
		});
	};
	//获取系统数据库信息
	function getSystemDatabaseInfo(url,callback){
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//升级系统数据库版本
	function updateDatabase(systemId,callback){
		var SystemHost = ''
			url = '';
		
		for(var i in SYSTEM_LIST){
			if(SYSTEM_LIST[i].Id == systemId){
				SystemHost = SYSTEM_LIST[i].SystemHost;
				break;
			}
		}
		url = uxutil.path.LOCAL + '/' + SystemHost + '/ServerWCF/CommonService.svc/GetUpDateVersion';
		
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//初始化
	function init(){
		//初始化系统卡片
		initSystemCards();
	};
	init();
});