/**
 * 字典对照
 * @author GHX
 * @version 2021-03-25
 */
var ITEMS = null;
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'layer', 'laydate', 'element', 'table', 'form'], function () {
	var $ = layui.jquery,
		layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		element = layui.element,
		laydate = layui.laydate,
		form = layui.form,
		$ = layui.jquery;
		
	var app = {
		
	};
	app.url={
		GET_SickTypeINFO: uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&fields=LBSickType_Id,LBSickType_CName,LBSickType_Shortcode'
	};
	/*-------初始化界面------*/
	app.init = function () {
		var  params = uxutil.params.get(false);
		var url = app.url.GET_SickTypeINFO + "";
		if(params.SickTypeID){
			url +="&where=(id not in("+params.SickTypeID+"))";
		}
		app.getItemInfo(url);
	};	
	app.getItemInfo = function (url) {
		var me = this;
		var loadIndex = layer.load();
		me.instance = table.render({
			elem: '#copytable',
			height: "full-35",
			defaultToolbar: false,
			toolbar: false,
			size: 'sm', //小尺寸的表格
			autoSort: false, //禁用前端自动排序
			loading: false,
			page: false,
			totalRow: false, //开启合计行
			limit: 9999999,
			url: url,
			cols: [[
				{type:'checkbox'},
				{ field: 'LBSickType_Id', width: 120, title: '索引', sort: false, hide: true },
				{ field: 'LBSickType_CName', width: 120, title: '系统名称', sort: false, hide: false },
				{ field: 'LBSickType_Shortcode', width: 120, title: '编码', sort: false, hide: false },
			]],
			text: {
				none: '没有可以复制的对接系统'
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
					"msg": res.ErrorInfo || "", //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				layer.close(loadIndex);
			}
		});
	};
	app.init();
});
