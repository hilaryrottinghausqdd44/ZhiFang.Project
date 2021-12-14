/**
	@name：站点类型维护
	@author：longfc
	@version 2019-10-11
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	bhosttypeTable: 'views/syspara/bhosttype/bhosttypeTable',
	bhosttypeForm: 'views/syspara/bhosttype/bhosttypeForm',
	cachedata: 'views/pre/common/cachedata'
}).use(['uxutil', 'table', 'form', 'cachedata', 'bhosttypeTable', 'bhosttypeForm'],
	function() {
		"use strict";

		var $ = layui.$,
			table = layui.table,
			form = layui.form,
			uxutil = layui.uxutil,
			bhosttypeTable = layui.bhosttypeTable,
			bhosttypeForm = layui.bhosttypeForm,
			cachedata = layui.cachedata;
		//实例对象
		var bhosttypeTable1 = null,
			bhosttypeForm1 = null;
		var height = $(document).height();
		//左侧列表当前选择行信息
		var curRowInfo = {};
		/**默认传入参数*/
		var defaultParams = null;

		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
		};
		//表单配置信息
		function getFormConfig() {
			return {
				title: '表单信息',
				elem: '#form_bhosttype',
				id: "form_bhosttype",
				formfilter: "form_bhosttype"
			};
		};
		/**初始化表单信息*/
		function initForm() {
			var config = getFormConfig();
			bhosttypeForm1 = bhosttypeForm.render(config);
			onForm();
			//表单高度
			var height = $(document).height() - $("#form_bhosttype").offset().top-35;
			$('#form_bhosttype').css("height",height);			
		};
		//表单监听
		function onForm() {
			//表单提交事件监听
			layui.form.on('submit(form_submit_bhosttype)', function(formData) {
				var submitData = {
					"PK": bhosttypeForm1.config.PK,
					"formtype": bhosttypeForm1.config.formtype,
					"formData": formData
				};
				//表单保存处理
				bhosttypeForm1.onSave(bhosttypeForm1, submitData, function(result) {
					var success = false;
					if (result) success = result.success;
					if (success == true) {
						bhosttypeTable1.setSelectPK(result.value.id);
						onRefreshTable();
					}
				});
			});
		};
		//表单数据加载
		function onLoadForm(obj) {
			var id = obj.data["BHostType_Id"];
			bhosttypeForm1.config.PK = id;
			bhosttypeForm1.config.formtype = "edit";
			bhosttypeForm1.load(id, function(result) {

			});
		};
		//列表配置信息
		function getTableConfig() {
			return {
				title: '列表信息',
				height: 'full-110',
				elem: '#table_bhosttype',
				id: "table_bhosttype",
				toolbar: '',
				externalWhere: ""
			};
		};
		//初始化或刷新列表
		function initTable() {
			bhosttypeTable1 = bhosttypeTable.render(getTableConfig());
			onTable();
		};
		//列表监听
		function onTable() {
			//监听行单击事件
			table.on('row(table_bhosttype)', function(obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				setTimeout(function() {
					onLoadForm(obj);
				}, 300);
			});
			//监听排序事件 
			table.on('sort(table_bhosttype)', function(obj) {
				var sort = [];
				var cur = {
					"property": obj.field,
					"direction": obj.type
				};
				sort.push(cur);
				bhosttypeTable1.setSort(sort);
			});
		};
		//刷新列表
		function onRefreshTable() {
			bhosttypeTable1 = bhosttypeTable1.onSearch();
			onTable();
		};
		/**查询表单事件监听*/
		function onSearchForm() {
			//列表查询表单按钮事件联动
			var onDocActive = {
				refresh: function() {
					onRefreshTable();
				},
				search: function() {
					onRefreshTable();
				},
				add: function() {
					onAdd();
				},
				edit: function() {
					var checkStatus = table.checkStatus("table_bhosttype");
					var data = checkStatus.data;
					if (data.length == 1) {
						onLoadForm(data[0]);
					} else {
						layer.msg('请选择行后再操作!', {
							time: 2000
						});
					}
				},
				//删除关系
				delete: function() {
					onDelete();
				},
				form_reset: function() {
					bhosttypeForm1.resetLoad();
				}
			};
			$('.layui-form .layui-btn').on('click', function() {
				var type = $(this).data('type');
				onDocActive[type] ? onDocActive[type].call(this) : '';
			});
			$('#table_bhosttype_like_search').on('keydown', function(event) {
				if (event.keyCode == 13) onRefreshTable();
			});
		};
		//新增
		function onAdd() {
			bhosttypeForm1.config.PK = "";
			bhosttypeForm1.config.formtype = "add";
			bhosttypeForm1.isAdd();
		};
		function onDelete() {
			var checkStatus = table.checkStatus("table_bhosttype");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据后再操作');
			}
			layer.confirm('确定要删除当前选择行信息吗？', function(index) {
				layer.close(index);
				bhosttypeTable1.onDelete(data, function(result) {
					if (result.success == true) onRefreshTable();
				});
			});
		};
		//初始化
		function initAll() {
			initParams();
			initTable();
			initForm();
			onSearchForm();
		};
		initAll();
	});
