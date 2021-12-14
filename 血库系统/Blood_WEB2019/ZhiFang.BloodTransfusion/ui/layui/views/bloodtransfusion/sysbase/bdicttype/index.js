layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	gridpanel: 'ux/gridpanel',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bdicttypeTable: '/views/bloodtransfusion/sysbase/bdicttype/bdicttypeTable',
	bdicttypeForm: '/views/bloodtransfusion/sysbase/bdicttype/bdicttypeForm'
}).use(['uxutil', 'form', 'table', 'dataadapter', 'gridpanel', 'bdicttypeTable', 'bdicttypeForm', 'cachedata'], function() {
	"use strict";

	var $ = layui.jquery;
	var util = layui.util;
	var form = layui.form;
	var table = layui.table;
	var bdicttypeTable = layui.bdicttypeTable;
	var bdicttypeForm = layui.bdicttypeForm;
	var cachedata = layui.cachedata;
	var height1 = $(document).height();
	//初始化默认传入参数信息
	function initDefaultParams() {

	};

	var bdicttypeForm1 = null;
	//表单配置信息
	function getDictTypeFormConfig() {
		return {
			title: '表单信息',
			elem: '#LAY-app-form-BDictType',
			id: "LAY-app-form-BDictType",
			formfilter: "LAY-app-form-BDictType"
		};
	};
	/**初始化表单信息*/
	function initBDictTypeForm() {
		var config = getDictTypeFormConfig();
		bdicttypeForm1 = bdicttypeForm.render(config);
		onBDictTypeForm();
	};
	//表单监听
	function onBDictTypeForm() {
		//表单提交事件监听
		layui.form.on('submit(LAY-app-submit-BDictType)', function(formData) {
			var submitData = {
				"PK": bdicttypeForm1.config.PK,
				"formtype": bdicttypeForm1.config.formtype,
				"formData": formData
			};
			//表单保存处理
			bdicttypeForm1.onSave(bdicttypeForm1, submitData, function(result) {
				var success = false;
				if(result) success = result.success;
				if(success == true) {
					onRefreshBDictTypeTable();
				}
				//医嘱申请保存结果存储到父窗体里
				//cachedata.setCache("bdicttypeFormSave", success);				
			});
		});
		//表单高度
		var height = $(document).height() - $("#LAY-app-form-BDictType").offset().top-35;
		$('#LAY-app-form-BDictType').css("height",height);	
	};
	//表单数据加载
	function onLoadForm(obj) {
		var id = obj.data["BDictType_Id"];
		bdicttypeForm1.config.PK = id;
		bdicttypeForm1.config.formtype = "edit";
		bdicttypeForm1.load(id, function(result) {

		});
	};
	var bdicttypeTable1 = null;
	//列表配置信息
	function getBDictTypeTableConfig() {
		return {
			title: '列表信息',
			height: 'full-110',
			elem: '#LAY-app-table-BDictType',
			id: "LAY-app-table-BDictType",
			toolbar:'',
			defaultToolbar: ['filter'],
			externalWhere: ""
		};
	};
	//初始化或刷新列表
	function initBDictTypeTable() {
		bdicttypeTable1 = bdicttypeTable.render(getBDictTypeTableConfig());
		onBDictTypeTable();
	};
	//列表监听
	function onBDictTypeTable() {
		//监听工具条
		table.on('tool(LAY-app-table-BDictType)', function(obj) {
			var data = obj.data;

		});
		//监听行单击事件
		table.on('row(LAY-app-table-BDictType)', function(obj) {
			//设置当前行为选中状态
			//bdicttypeTable1.setRadioCheck(obj);
			setTimeout(function() {
				onLoadForm(obj);
			}, 300);
		});
		//监听行双击事件
		table.on('rowDouble(LAY-app-table-BDictType)', function(obj) {
			//是否弹出编辑或查看?
		});
		//监听排序事件 
		table.on('sort(LAY-app-table-BDictType)', function(obj) {
			var sort = [];
			var cur = {
				"property": obj.field,
				"direction": obj.type
			};
			sort.push(cur);
			bdicttypeTable1.setSort(sort);
			//onBDictTypeTable();
		});
	};
	//刷新主单列表
	function onRefreshBDictTypeTable() {
		//获取查询条件
		bdicttypeTable1.getSearchWhere();
		table.reload(bdicttypeTable1.config.id, bdicttypeTable1.config);
		//onBDictTypeTable();
	};
	/**查询表单事件监听*/
	function onSearchForm() {
		$('.layui-form .layui-btn').on('click', function() {
			var type = $(this).data('type');
			onDocActive[type] ? onDocActive[type].call(this) : '';
		});
		$('#LAY-app-table-BDictType-Search-LikeSearch').on('keydown', function(event) {
			if(event.keyCode == 13) onRefreshBDictTypeTable();
		});
	};
	//列表查询表单按钮事件联动
	var onDocActive = {
		add: function() {
			addDicttype();
		},
		edit: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BDictType");
			var data = checkStatus.data;
			if(data.length == 1) {
				var curRow = data[0];
				onLoadForm(curRow);
			} else {
				layer.msg('请选择字典类型后再操作!', {
					time: 2000
				});
			}
		},
		refresh: function() {
			onRefreshBDictTypeTable();
		},
		search: function() {
			onRefreshBDictTypeTable();
		},
		reset: function() {
			bdicttypeForm1.resetLoad();
		}
	};
	//新增字典类型
	function addDicttype() {
		bdicttypeForm1.config.PK = "";
		bdicttypeForm1.config.formtype = "add";
		bdicttypeForm1.isAdd();
	};
	//初始化
	function initAll() {
		initDefaultParams();
		initBDictTypeTable();
		initBDictTypeForm();
		onSearchForm();
	};
	initAll();
});