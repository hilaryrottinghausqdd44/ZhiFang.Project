/**
	@name：小组列表
	@author：liangyl	
	@version 2019-05-31
 */
layui.extend({
//	uxtable:'ux/table'
}).define(['table','uxtable'],function(exports){
	"use strict";
	
	var $=layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		uxtable = layui.uxtable;
	//小组列表功能参数配置
	var config = {
		//获取小组列表服务路径
		get_section_list_url:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
		elem:''
	};
	var sectionTable = {
		config:{
			//小组id，用于判断定位选择行
			PK:null
		},
		//核心入口
		render:function(options){
			var me = this;
			options.url = config.get_section_list_url;
			config.elem = options.id;
			var table_options = {
				elem:options.elem,
				id:options.id,
				toolbar:'',
				page: false,
				limit: 1000,
				autoSort: false, //禁用前端自动排序
				loading : true,
				size: 'sm', //小尺寸的表格
				height:options.height ? options.height : 'full-220',
				cols:[[
					{type: 'numbers',title: '行号',fixed: 'left'},
					{field: 'LBSection_Id',width: 60,title: '主键ID',sort: true,hide: true},
                    {field:'LBSection_CName', minWidth:150,flex:1, title: '名称', sort: true}
							]],
				done: function(res, curr, count) {
                },
                parseData:function(res){//res即为原始返回的数据
					if(!res) return;
					var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					return {
						"code": res.success ? 0 : 1, //解析接口状态
						"msg": res.ErrorInfo, //解析提示文本
						"count": data.count || 0, //解析数据长度
						"data": data.list || []
					};
				},
				text: {none: '暂无相关数据' }
			};
			//标题
			if(options.title){
				table_options.title = options.title;
			}
			if(options.url){
				var fields = getStoreFields(table_options,true);
				table_options.url=options.url+'&fields='+fields;
				table_options.initSort = options.initSort;
				if(options.defaultOrderBy){
					table_options.url+='&sort='+options.defaultOrderBy;
				}
			}
			if(options.defaultToolbar)table_options.defaultToolbar=options.defaultToolbar;
			return uxtable.render(table_options);
		}
	};

	/**创建数据字段*/
	var getStoreFields =  function(tableId,isString) {
		var columns = tableId.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//暴露接口
	exports('sectionTable',sectionTable);
});