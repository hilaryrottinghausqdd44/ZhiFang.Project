layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	formSelects: 'src/formselects/dist/formSelects-v4.min'
}).use(['uxutil', 'formSelects', 'dataadapter'], function() {
	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var formSelects = layui.formSelects;
	var dataadapter = layui.dataadapter;
	formSelects.config('selectEquipment', {
		response: dataadapter.toResponse(),
		keyName: 'Equipment_CName',
		keyVal: 'Equipment_Id',
		beforeSuccess: function(id, url, searchVal, result) {
			return dataadapter.toList(result);
		}
	}, true);
	var selectUrl =
		"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipmentByHQL?isPlanish=true&fields=Equipment_CName,Equipment_Id&page=1&start=0&limit=200";
	formSelects.data('selectEquipment', 'server', {
		//linkage: true,   //开启联动模式
		url: uxutil.path.ROOT + selectUrl
	});
});
