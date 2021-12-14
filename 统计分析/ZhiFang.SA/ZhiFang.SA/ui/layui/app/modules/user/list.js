/**
	@name：layui.ux.modules.user.list 用户列表
	@author：Jcall
	@version 2019-04-29
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
	
	//用户列表功能参数配置
	var config = {
		//获取用户列表服务路径
		get_user_list_url:uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
		//根据部门ID获取用户列表服务路径
		get_user_list_by_dept_url:uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
		
	};
	
	var userlist = {
		//核心入口
		render:function(options){
			options.url = config.get_user_list_url;
			
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
			var table_options = {
				elem:options.elem,
				toolbar:'#user_table_toolbar',
				cols:[[
					{type:'checkbox', fixed: 'left'},
					{field:'ClientName', minWidth:100, title: '人员姓名', sort: true},
					{field:'LoginTime', width:160, title: '登录时间', sort: true},
					{field:'ClientTypeFlagName', width:70, title: '类型', sort: true}
				]],
			};
			//标题
			if(options.title){
				table_options.title = options.title;
			}
			//默认加载
			if(options.loading){
				table_options.loading = options.loading;
			}
			
			return uxtable.render(table_options);
		}
	};
	
	//暴露接口
	exports('userlist',userlist);
});