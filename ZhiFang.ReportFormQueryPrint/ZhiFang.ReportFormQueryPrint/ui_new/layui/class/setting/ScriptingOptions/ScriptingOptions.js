/**
 * 升级脚本页面
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element','table'], function () {
	var uxutil = layui.uxutil,
		
		table = layui.table,
		$ = layui.jquery;
	var app = {};
	app.cols = {
		left: [
			[
				{ type: 'checkbox'},
				{field: 'ProcedureVersion',
					title: '程序集版本',
					width: 100,
					align: 'center',
					//hide: true,
					sort: false
				},
				{field: 'DBVersion',
					title: '目前版本',
					width: 100,
					align: 'center',
					hide: true,
					sort: false
				}
				
			]
		]
	};
	//服务地址
	app.url = {
		
		//获取版本
		selectUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/GetDBVersion',
		//更新
		updateUrl:  uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/DBupdate"
	};
	//初始化  
	app.init = function () {
		var me = this;
		me.initBottomTable();
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me=this;
		//头工具栏事件
		table.on('toolbar(versionTable)', function(obj){
			
			switch(obj.event){
			case 'oneClickUpgrade':
				
				me.updateVersion();
			break;
			
			};
		});
	}
	//初始化底部侧列表  
	app.initBottomTable = function () {
		var me = this;
		
		//page和limit默认会传
		var url = me.url.selectUrl;
		table.render({
			elem: '#versionTable',
			height: 'full-5',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			toolbar: '#toolbarDemo', //开启头部工具栏，并为其绑定左侧模板
			defaultToolbar:[],
			cols: me.cols.left,
			limit: 50,
			limits: [10, 20, 50, 100, 200],
			autoSort: true, //禁用前端自动排序		
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码	
					statusName: 'code', //code key	 
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				
				
				
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.length || 0, //解析数据长度	
					"data": data || []
				};
			},
			done: function (res, curr, count) {
				if(res){
					
					$("#nowVersionButton").html("当前数据库版本："+res.data[0].DBVersion)
				}
				
			}
		});
	};
	//一键升级数据库
	app.updateVersion=function(){
		var me=this;
		uxutil.server.ajax({
			url: me.url.updateUrl,
			async: false
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("升级成功");
					me.init();
				} else {
					layer.msg("升级失败");
				}

			} else {
				layer.msg("升级失败");
			}
		});
	}
	app.init();
});