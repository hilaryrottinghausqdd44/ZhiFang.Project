layui.extend({
	uxutil: 'ux/util',
	//dataadapter: 'ux/modules/dataadapter',
	gridpanel: 'ux/gridpanel',
	watchClassTree: 'app/statistical/dict/phraseswatchclass/watchClassTree',
	watchClassTable: 'app/statistical/dict/phraseswatchclass/watchClassTable'
}).use(['layer', 'form', 'table', 'laypage', 'tree', 'watchClassTree', 'watchClassTable'], function() {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var form = layui.form;
	var table = layui.table;
	var tree = layui.tree;
	var gridpanel = layui.gridpanel;
	//使用layui分页
	var laypage = layui.laypage;
	
	var watchClassTree = layui.watchClassTree;
	var watchClassTable = layui.watchClassTable;
	//树初始化
	watchClassTree.render({
		title: '',
		elem: '#LAY-app-tree-WatchClass',
		id: 'LAY-app-tree-WatchClass',
		click: treeClick
	});

	var tableConfig = {
		title: '列表信息',
		elem: '#LAY-app-table-WatchClass',
		id: 'LAY-app-table-WatchClass',
		toolbar: "#LAY-app-table-WatchClass-Toolbar",
		defaultToolbar: null
	};
	//列表初始化
	var totalCount = 0;
	var vue = new Vue({
		el: "#vueContainer",
		data: {
			phraseswatchclassList: null
		}
	});
	
	function loadData(curr,limit){
		var url = uxutil.path.ROOT +
			"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassByHQL?isPlanish=true";
		var fields =
			"PhrasesWatchClass_Id,PhrasesWatchClass_CName,PhrasesWatchClass_SName,PhrasesWatchClass_ShortCode,PhrasesWatchClass_DispOrder";
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + fields+"&page="+curr+"&limit="+limit;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			console.log("data:" + data);
			if (data.success) {
				//var result= dataadapter.toList();
				vue.phraseswatchclassList = data.value.list;
				totalCount = data.value.count;
			} else {
				return;
			}
		});
	}
	loadData(1,5);
	
	laypage.render({
		elem: 'pagination',
		count: totalCount,
		limit:5,
		limits:[10, 20, 30, 40, 50],
		layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
		jump: function(obj, first) {
			//点击非第一页页码时的处理逻辑。比如这里调用了ajax方法，异步获取分页数据
			if (!first) {
				loadData(obj.curr, obj.limit); //第二个参数不能用变量pageSize，因为当切换每页大小的时候会出问题
			}
		}
	});

	//列表操作列监听
	table.on('tool(LAY-app-table-WatchClass)', function(obj) {
		var data = obj.data; //获得当前行数据
		var layEvent = obj.event;
		var tr = obj.tr; //获得当前行 tr 的DOM对象	
		if (layEvent === 'del') { //删除		
			layer.confirm('真的删除行么', function(index) {
				obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
				layer.close(index);
				//向服务端发送删除指令
			});
		} else if (layEvent === 'edit') { //编辑
			openForm("edit", data["PhrasesWatchClass_Id"]);
			//watchClassTable.del();
		}
	});
	//树节点事件监听
	function treeClick(node) {
		//加载列表
		var externalWhere = "";
		if (node && node.tid) {
			externalWhere = "(phraseswatchclass.ParentID=" + node.tid + " or phraseswatchclass.Id=" + node.tid + ")";
		}
		watchClassTable.config.externalWhere = externalWhere;
		watchClassTable.render(tableConfig);
	};
	//打开新增或编辑表单
	function openForm(formtype, id) {
		var params = [];
		params.push("formtype=" + formtype);
		params.push("id=" + id);
		layer.open({
			type: 2,
			title: '类型信息',
			//area:'500px',
			area: ['45%', '68%'],
			content: 'watchClassForm.html?' + params.join("&"),
			id: "LAY-app-form-open-watchClassForm",
			btn: ['确定', '取消'],
			yes: function(index, layero) {
				var iframeWindow = window['layui-layer-iframe' + index],
					submitID = 'LAY-app-watchClass-submit',
					submit = layero.find('iframe').contents().find('#' + submitID);

				//监听提交
				iframeWindow.layui.form.on('submit(' + submitID + ')', function(data) {
					var field = data.field; //获取提交的字段
					layer.close(index); //关闭弹层

					//table.reload('LAY-app-table-WatchClass'); //数据刷新
					watchClassTable.render(tableConfig);
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
			var checkStatus = watchClassTable.checkStatus('LAY-app-table-WatchClass'),
				checkData = checkStatus.data; //得到选中的数据

			if (checkData.length === 0) {
				return layer.msg('请选择数据');
			}
			layer.prompt({
				formType: 1,
				title: '敏感操作，请验证口令'
			}, function(value, index) {
				layer.close(index);
				layer.confirm('确定删除吗？', function(index) {
					//执行 Ajax 后重载
					watchClassTable.reload('LAY-app-table-WatchClass');
					layer.msg('已删除');
				});
			});
		},
		add: function() {
			//console.log("add");
			openForm("add", "");
		},
		edit: function() {
			//console.log("edit");
			openForm("edit", "");
		}
	};
	$('.layui-btn-group .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
});
