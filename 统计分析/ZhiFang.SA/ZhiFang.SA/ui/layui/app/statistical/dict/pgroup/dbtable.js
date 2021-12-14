layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	gridpanel: 'ux/gridpanel',
	leftTable: 'app/statistical/dict/pgroup/leftTable',
	rightTable: 'app/statistical/dict/pgroup/rightTable'
}).use(['table', 'gridpanel','leftTable', 'rightTable'], function() {
	var $ = layui.jquery;
	var table = layui.table;
	var gridpanel = layui.gridpanel;
	var leftTable = layui.leftTable;
	var rightTable = layui.rightTable;

	//列表初始化
	leftTable.render({
		elem: '#LAY-app-table-leftTable',
		id: 'LAY-app-table-leftTable',
		toolbar: "#LAY-app-table-leftToolbar",
		defaultToolbar: ['filter']
	});
	rightTable.render({
		elem: '#LAY-app-table-rightTable',
		id: 'LAY-app-table-rightTable',
		toolbar: "#LAY-app-table-rightToolbar",
		defaultToolbar: ['filter']
	});
	//绑定左列表查询输入框自定义信息
	if (leftTable.searchInfo) {
		var leftSearch = document.getElementById("LAY-app-table-left-txt-search");
		$.data(leftSearch, "searchInfo", JSON.stringify(leftTable.searchInfo));
	}
	//监听左列表搜索
	table.on('toolbar(LAY-app-table-leftTable)', function(obj) {
		switch (obj.event) {
			case 'search':
				var where = "";
				var leftSearch = document.getElementById("LAY-app-table-left-txt-search");
				if (leftSearch.value) {
					var internalWhere = "";
					var searchInfo = $.data(leftSearch, "searchInfo");
					if (searchInfo) {
						searchInfo = JSON.parse(searchInfo);
						internalWhere = gridpanel.getSearchWhere(searchInfo, leftSearch.value);
					}
					where = {
						"where": internalWhere,
					};
				}
				//执行重载
				table.reload('LAY-app-table-leftTable', {
					where: where
				});
				break;
		};
	});
	//监听左列表行双击事件
	table.on('rowDouble(LAY-app-table-leftTable)', function(obj) {
		obj.del();
		var cache = table.cache["LAY-app-table-rightTable"];
		var data = cache || [];
		data.push(obj.data);
		var options = {
			data: data
		};
		table.reload('LAY-app-table-rightTable', options);
	});
	//监听右列表行双击事件
	table.on('rowDouble(LAY-app-table-rightTable)', function(obj) {
		console.log(obj.data) //得到当前行数据
		obj.del();
	});
	//全部左移按钮事件
	$('#LAY-app-table-btn-all-left').on('click', function() {

	});
	//选择左移按钮事件
	$('#LAY-app-table-btn-left').on('click', function() {

	});
	//全部右移按钮事件
	$('#LAY-app-table-btn-all-right').on('click', function() {

	});
	//选择右移按钮事件
	$('#LAY-app-table-btn-right').on('click', function() {

	});
	//确认按钮事件
	$('#LAY-app-table-btn-submit').on('click', function() {
		var cache = table.cache["LAY-app-table-rightTable"];
		//通过确认按钮关闭
		layui.data('pgrouptabData', {
			key: 'sumit',
			value: 0
		});
		layui.data('pgrouptabData', {
			key: 'data',
			value: cache
		});
		//关闭窗口
		var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
		parent.layer.close(index); //再执行关闭
	});
	//关闭按钮事件
	$('#LAY-app-table-btn-close').on('click', function() {
		//关闭窗口
		var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
		parent.layer.close(index); //再执行关闭
	});
});
