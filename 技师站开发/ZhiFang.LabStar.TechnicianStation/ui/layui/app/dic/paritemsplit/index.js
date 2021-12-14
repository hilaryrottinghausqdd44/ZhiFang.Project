/**
	@name：组合项目拆分
	@author：longfc
	@version 2019-10-14
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	dictselect: 'modules/pre/dictselect',
	lbItemTable: 'app/dic/paritemsplit/lbItemTable',
	paritemsplitTable: 'app/dic/paritemsplit/paritemsplitTable',
	cachedata: 'common/cachedata'
}).use(['uxutil', 'dictselect', 'table', 'form', 'lbItemTable', 'paritemsplitTable', 'cachedata'],
	function() {
		"use strict";

		var $ = layui.$,
			element = layui.element,
			table = layui.table,

			form = layui.form,
			uxutil = layui.uxutil,
			dictselect = layui.dictselect,
			lbItemTable = layui.lbItemTable,
			cachedata = layui.cachedata,
			paritemsplitTable = layui.paritemsplitTable;
		//列表实例对象	
		var lbItemTable1 = null,
			paritemsplitTable1 = null;
		var height = $(document).height();
		//左侧列表当前选择行信息
		var testitemCurRow = null;
		//右侧列表当前编辑行信息
		var paritemsplitCurRow = null;
		/**默认传入参数*/
		var defaultParams = null;
		//初始化默认传入参数信息
		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
		};
		//获取左侧列表高度
		function getTableHeight1() {
			var height1 = 0; // $('[lay-filter="div_fieldset_testitem"]').height();
			var height2 = $('[lay-filter="div_filter_of_testitem"]').height();
			var height3 = height - height1 - height2 - 20;
			return height3;
		};
		//已拆分组合项目列表配置信息
		function getLBItemTableConfig() {
//			var height3 = getTableHeight1();
			return {
				title: '已拆分组合项目信息',
				elem: '#table_testitem',
				id: "table_testitem",
				filter: "table_testitem",
				height: 'full-78',
				size:'sm'
				//height: height3
			};
		};
		//获取右侧列表高度
		function getTableHeight2() {
			var height1 = 0; // $('[lay-filter="div_fieldset_paritemsplit"]').height();
			var height2 = $('[lay-filter="div_toolbar_paritemsplit"]').height();
			var height3 = $('[lay-filter="div_table_paritemsplit_memo"]').height();
			var height4 = height - height1 - height2 - height3 - 35;
			return height4;
		};
		//组合项目拆分列表配置信息
		function getparitemsplitTableConfig() {
			var height3 = getTableHeight2();
			return {
				title: '组合项目拆分信息',
				elem: '#table_paritemsplit',
				id: "table_paritemsplit",
				filter: "table_paritemsplit",
				height: 'full-78',
				size:'sm'
				//height: height3
			};
		};
		//组合项目列表事件监听
		function onLBItemTable() {
			table.on('row(table_testitem)', function(obj) {
				//行选中样式
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				testitemCurRow = obj.data;
				setTimeout(function() {
					onRefreshparitemsplitTable();
				}, 300);
			});
		};
		//各表单组件事件监听
		function onFormEvent() {
			//检验小组下拉选择
			form.on('select(select_section)', function(data) {
				onSelectSection(data);
			});
			//采样组下拉选择
			form.on('select(select_LBParItemSplit_LBSamplingGroup_Id)', function(data) {
				var tr=data.othis.parents('tr').eq(0);
				var rowIndex= tr.data('index');
				var fields =table.cache[paritemsplitTable1.config.id][rowIndex];
				var textContent = $(data.elem).find("option:selected").text();
				fields["LBParItemSplit_LBSamplingGroup_Id"] = data.value;
				fields["LBParItemSplit_LBSamplingGroup_CName"] = textContent;
				paritemsplitTable1.updateRow(fields,rowIndex) //修改当前行数据
				form.render();
			});
			//是否合并检验报告开关监听
			form.on('switch(checkbox_IsAutoUnion)', function(data) {
				var tr=data.othis.parents('tr').eq(0);
				var rowIndex= tr.data('index');
				var fields =table.cache[paritemsplitTable1.config.id][rowIndex];
				fields["LBParItemSplit_IsAutoUnion"] = data.elem.checked;
				paritemsplitTable1.updateRow(fields,rowIndex) //修改当前行数据
				form.render();
			});
			//按钮事件
			var active = {
				search_testitem: function() {
					onRefreshLBItemTable();
				},
				search_paritemsplit: function() {
					onRefreshparitemsplitTable();
				},
				//新增
				add: function() {
					onAdd();
				},
				//删除关系
				delete: function() {
					onDelete();
				},
				save: function() {
					onSave();
				}
			};
			$('.layui-btn').on('click', function() {
				var type = $(this).data('type');
				active[type] ? active[type].call(this) : '';
			});
			 //回车事件
		    $("#table_testitem_like_search").on('keydown', function (event) {
		        if (event.keyCode == 13) {
		        	onRefreshLBItemTable();
		            return false;
		        }
		    });
		};
		//检验小组下拉选择事件
		function updateRow(data) {

		};
		//检验小组下拉选择事件
		function onSelectSection(data) {
			//先清空当前关系列表数据
			paritemsplitTable1.clearData();
			lbItemTable1.config.lbsectionId = data.value;
			onRefreshLBItemTable();
		};
		/**初始化已拆分组合项目列表*/
		function initLBItemTable() {
			var config = getLBItemTableConfig();
			lbItemTable1 = lbItemTable.render(config);
			onLBItemTable();
		};
		/**初始化组合项目拆分关系列表*/
		function initparitemsplitTable() {
			var config = getparitemsplitTableConfig();
			paritemsplitTable1 = paritemsplitTable.render(config);
			onparitemsplitTable();
		};
		//组合项目拆分关系列表
		function onparitemsplitTable() {
			//行单击事件
			table.on('row(table_paritemsplit)', function(obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				paritemsplitCurRow = obj;				
			});
			//监听单元格编辑
			table.on('edit(table_paritemsplit)', function(obj) {

			});
		};
		//初始化列表
		function initTable() {
			initLBItemTable();
			initparitemsplitTable();
		};
		//刷新已拆分组合项目列表
		function onRefreshLBItemTable() {
			paritemsplitCurRow = null;
			lbItemTable1.config.defaultParams = defaultParams;
			lbItemTable1 = lbItemTable1.onSearch();
		};
		//刷新组合项目拆分关系列表
		function onRefreshparitemsplitTable() {
			var where = "";
			if (testitemCurRow) {
				where = "lbparitemsplit.ParItem.Id=" + testitemCurRow["LBItem_Id"];
			}
			if (where) paritemsplitTable1.setExternalWhere(where);
			paritemsplitTable1 = paritemsplitTable1.onSearch(where);
		};
		//初始化检验小组下拉选择项
		function initSelectSection(callback) {
			dictselect.dictList.LBSection(function(html) {
				var filter = "select_section";
				$('[lay-filter="' + filter + '"]').empty().append(html);
				form.render('select', "form_testitem");
				if (callback) callback();
			});
		};
		//初始化
		function initAll() {
			initParams();
			initTable();
			onFormEvent();
		};
		//弹出新增窗体
		function onAdd(callback) {
			var title = "新增检验项目拆分";
			var content = 'add/index.html?';
			var params = [];
			params = params.join('&');
			var url = content + params;
			layer.open({
				type: 2,
				title: title,
				area: ['82%', '92%'],
				content: url,
				id: "add_paritemsplit_index",
				btn: null,
				yes: function(index, layero) {
				},
				end: function() {
					var success = cachedata.getCache("onAddLBParItemSplit");
					if (success == true) {
						cachedata.delete("onAddLBParItemSplit");
						onRefreshLBItemTable();
						if (callback) callback();
					}
				},
				cancel: function(index, layero) {

				}
			});
		};
		//按已拆分的组合项目ID删除组合项目拆分关系
		function onDelete() {
			if (!testitemCurRow) {
				layer.msg('请选择数据后再操作')
				return;
			}
			layer.confirm('确定要删除当前选择行信息吗？', function(index) {
				layer.close(index);
				var DELETE_URL = uxutil.path.ROOT +
					'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBParItemSplitByParItemId';
				var id = testitemCurRow["LBItem_Id"];
				var url = DELETE_URL + '?parItemId=' + id;
				//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
				url = url + "&t=" + new Date().getTime();
				var config = {
					type: "GET",
					url: url
				};
				setTimeout(function() {
					uxutil.server.ajax(config, function(result) {
						if (result.success) {
							testitemCurRow = null;
							//清空当前关系列表数据
							paritemsplitTable1.clearData();
							onRefreshLBItemTable();
						} else {
							layer.msg(result.msg);
						}
					}, true);
				}, 100);
			});
		};

		function onSave() {
			var result = paritemsplitTable1.onSaveVerify();
			if (result.success == false) {
				layer.msg(result.msg);
				return;
			}
			var entityList = paritemsplitTable1.getEntityList();
			if (!entityList || entityList.length <= 0) {
				layer.msg("获取组合项目拆分信息为空!");
				return;
			}
			var url = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBParItemSplitList';
			var entity = {
				"entityList": entityList
			};
			var params = JSON.stringify(entity);
			//显示遮罩层
			var config = {
				type: "POST",
				url: url,
				data: params
			};
			uxutil.server.ajax(config, function(data) {
				cachedata.setCache("onAddLBParItemSplit", data.success);
				//隐藏遮罩层
				if (data.success) {
					var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
					parent.layer.close(index);
					layer.msg("组合项目拆分保存成功!");
				} else {
					if (!data.msg) data.msg = '新增组合项目拆分保存失败!';
					layer.msg(data.msg, {
						icon: 5,
						anim: 6
					});
				}
			});
		};
		initAll();
	});
