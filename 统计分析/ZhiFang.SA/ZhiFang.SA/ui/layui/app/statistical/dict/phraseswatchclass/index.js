layui.extend({
	uxutil: 'ux/util',
	watchClassTree: 'app/statistical/dict/phraseswatchclass/watchClassTree',
	watchClassTable: 'app/statistical/dict/phraseswatchclass/watchClassTable',
	watchClassForm: 'app/statistical/dict/phraseswatchclass/watchClassForm'
}).use(['layer', 'form', 'table', 'watchClassTree', 'watchClassTable', 'watchClassForm'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var form = layui.form;
	var table = layui.table;
	var watchClassTree = layui.watchClassTree;
	var watchClassTable = layui.watchClassTable;

	//当前选择的树节点值
	var curNodeId = 0;
	var treefilter = 'LAY-app-tree-WatchClass',
		tablefilter = 'LAY-app-table-WatchClass';
	var watchClassTree1 = null,
		watchClassTable1 = null;

	//树节点事件监听
	function treeClick(node) {
		//加载列表
		curNodeId = node.tid;
		onRefresh();
	};
	var tableConfig = {
		title: '列表信息',
		elem: '#' + tablefilter,
		id: tablefilter,
		toolbar: "#LAY-app-table-WatchClass-Toolbar",
		defaultToolbar: null,
		externalWhere: ""
	};
	//打开新增或编辑表单
	function openForm(formtype, id) {
		var params = [];
		params.push("formtype=" + formtype);
		params.push("id=" + id);
		layer.open({
			type: 2,
			title: '类型信息',
			area: ['45%', '68%'],
			content: 'watchClassForm.html?' + params.join("&"),
			id: "LAY-app-form-open-watchClassForm",
			btn: ['确定', '取消'],
			yes: function(index, layero) {
				var iframeWindow = window['layui-layer-iframe' + index],
					submitID = 'LAY-app-watchClassForm-submit',
					submit = layero.find('iframe').contents().find('#' + submitID);
				//submit.trigger('click');
				//监听提交
				iframeWindow.layui.form.on('submit(' + submitID + ')', function(submitData) {
					layui.watchClassForm.newClass().onSave(formtype, submitData, id, function() {
						layer.close(index); //关闭弹层
						init(true);
					});
				});
				submit.trigger('click');
			},
			end: function() {

			}
		});
	};

	//事件
	var active = {
		batchdel: function() {
			var checkStatus = watchClassTable1.checkStatus(tablefilter),
				checkData = checkStatus.data; //得到选中的数据

			if (checkData.length === 0) {
				return layer.msg('请选择数据');
			}
			layer.prompt({
				formType: 1,
				title: '敏感操作，请验证口令'
			}, function(value, index) {
				layer.close(index);
				layer.confirm('确定删除选择行吗？', function(index) {
					//执行 Ajax 后重载
					//watchClassTable1.reload(tablefilter);
					layer.msg('已删除');
				});
			});
		},
		add: function() {
			openForm("add", "");
		},
		edit: function() {
			var checkStatus = table.checkStatus(tablefilter);
			var data = checkStatus.data;
			if (data.length == 1) {
				openForm("edit", data[0]["PhrasesWatchClass_Id"]);
			} else {
				layer.msg('请选中其中一行后再编辑!');
			}
		},
		refresh: function() {
			onRefresh();
		}
	};
	//获取当前选择节点,模糊输入框值
	function getExternalWhere() {
		var externalWhere = "";
		var arr = [];
		if (curNodeId) {
			arr.push("phraseswatchclass.ParentID=" + curNodeId + " or phraseswatchclass.Id=" + curNodeId + "");
		}
		var value = $('#LAY-app-table-WatchClass-txt-search').val();
		if (value) {
			arr.push("phraseswatchclass.CName like '%" + value + "%' or phraseswatchclass.ShortCode like '%" + value + "%'");
		}
		if (arr.length > 0)
			externalWhere = arr.join(") and (");
		if (externalWhere) externalWhere = "(" + externalWhere + ")";
		tableConfig.externalWhere = externalWhere;
		watchClassTable.config.externalWhere = tableConfig.externalWhere;
	};
	//刷新列表
	function onRefresh() {
		getExternalWhere();
		watchClassTable1 = watchClassTable.render(tableConfig);
		onBtns();
	};
	//列表查询
	function onSearch() {
		getExternalWhere();
		watchClassTable1 = watchClassTable.render(tableConfig);
		onBtns();
	};
	//按钮事件监听
	function onBtns() {
		$('.layui-btn-container .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
		$('#LAY-app-table-WatchClass-btn-search').on('click', function() {
			onSearch();
		});
		$('#LAY-app-table-WatchClass-txt-search').on('keydown', function(event) {
			if (event.keyCode == 13) {
				onSearch();
			}
		});
	};
	//初始化
	function init(refresh) {
		//树初始化
		watchClassTree1 = watchClassTree.render({
			title: '',
			elem: '#' + treefilter,
			id: treefilter,
			refresh: refresh,
			click: treeClick
		});
		//列表初始化
		watchClassTable1 = watchClassTable.render(tableConfig);
		onBtns();
	};
	init(true);
	//列表操作列监听
	table.on('tool(' + tablefilter + ')', function(obj) {
		var data = obj.data; //获得当前行数据
		var layEvent = obj.event;
		var tr = obj.tr; //获得当前行 tr 的DOM对象	
		if (layEvent === 'del') { //删除		
			layer.confirm('确定删除选择行吗？?', function(index) {
				watchClassTable1.delOneById(0, obj, function() {
					//obj.del();
					layer.close(index);
					layer.msg('删除成功!');
					onRefresh()
				});
			});
		} else if (layEvent === 'edit') { //编辑
			openForm("edit", data["PhrasesWatchClass_Id"]);
		}
	});
});
