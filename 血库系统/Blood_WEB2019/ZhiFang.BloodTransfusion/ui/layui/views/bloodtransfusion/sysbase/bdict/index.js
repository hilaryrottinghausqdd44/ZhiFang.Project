layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	gridpanel: 'ux/gridpanel',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bdicttypeTable: '/views/bloodtransfusion/sysbase/bdicttype/bdicttypeTable',
	bdictTable: '/views/bloodtransfusion/sysbase/bdict/bdictTable',
	bdictForm: '/views/bloodtransfusion/sysbase/bdict/bdictForm'
}).use(['uxutil', 'form', 'table', 'dataadapter', 'gridpanel', 'bdicttypeTable', 'bdictTable', 'bdictForm', 'cachedata'], function() {
	"use strict";

	var $ = layui.jquery;
	var util = layui.util;
	var form = layui.form;
	var table = layui.table;
	var bdicttypeTable = layui.bdicttypeTable;
	var bdictTable = layui.bdictTable;
	var bdictForm = layui.bdictForm;
	var cachedata = layui.cachedata;
	var height1 = $(document).height();
	//初始化默认传入参数信息
	function initDefaultParams() {

	};
	//当前字典类型Id
	var curBDictTypeId =null;
	
	var bdicttypeTable1 = null;
	//字典类型列表配置信息
	function getBDictTypeTableConfig() {
		return {
			title: '字典类型',
			//width: 260,
			height: 'full-150',
			elem: '#LAY-app-table-BDictType',
			id: "LAY-app-table-BDictType",
			toolbar: "",
			defaultToolbar: null,
			externalWhere: "",
			cols: [
				[{
					field: 'BDictType_Id',
					width: 140,
					hide: true,
					title: 'ID'
				}, {
					field: 'BDictType_CName',
					width: 160,
					sort: true,
					title: '名称'
				}]
			]
		};
	};
	//初始化或刷新字典类型列表
	function initBDictTypeTable() {
		bdicttypeTable1 = bdicttypeTable.render(getBDictTypeTableConfig());
		onBDictTypeTable();
	};
	//字典类型列表监听
	function onBDictTypeTable() {
		//监听行单击事件
		table.on('row(LAY-app-table-BDictType)', function(obj) {
			setTimeout(function() {
				onLoadDtlTable(obj);
			}, 300);
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
			onBDictTypeTable();
		});
	};
	//刷新字典类型列表
	function onRefreshBDictTypeTable() {
		//获取查询条件
		bdicttypeTable1.getSearchWhere();
		table.reload(bdicttypeTable1.config.id, bdicttypeTable1.config);
	};
	/**字典类型查询表单事件监听*/
	function onSearchForm() {
		$('.layui-form .layui-btn').on('click', function() {
			var type = $(this).data('type');
			onBDictTypeActive[type] ? onBDictTypeActive[type].call(this) : '';
		});
		$('#LAY-app-table-BDictType-Search-LikeSearch').on('keydown', function(event) {
			if(event.keyCode == 13) onRefreshBreqFormTable();
		});
	};
	//字典类型列表查询表单按钮事件联动
	var onBDictTypeActive = {
		refresh: function() {
			onRefreshBDictTypeTable();
		},
		search: function() {
			onRefreshBDictTypeTable();
		}
	};
	//字典类型列表联动字典列表
	function onLoadDtlTable(obj) {
		curBDictTypeId= obj.data["BDictType_Id"];
		bdictTable1.config.bdictTypeId=curBDictTypeId;
		onRefreshBDictTable();
	};

	var bdictForm1 = null;
	//字典表单配置信息
	function getBDictFormConfig() {
		return {
			title: '表单信息',
			elem: '#LAY-app-form-BDict',
			id: "LAY-app-form-BDict",
			formfilter: "LAY-app-form-BDict"
		};
	};
	/**初始化字典表单信息*/
	function initBDictForm() {
		var config = getBDictFormConfig();
		bdictForm1 = bdictForm.render(config);
		onBDictForm();
	};
	//字典表单监听
	function onBDictForm() {
		//表单提交事件监听
		layui.form.on('submit(LAY-app-submit-BDict)', function(formData) {
			var submitData = {
				"PK": bdictForm1.config.PK,
				"formtype": bdictForm1.config.formtype,
				"formData": formData
			};
			//表单保存处理
			bdictForm1.onSave(bdictForm1, submitData, function(result) {
				var success = false;
				if(result) success = result.success;
				if(success == true) {
					onRefreshBDictTable();
				}
			});
		});
	};
	//字典表单数据加载
	function onLoadForm(obj) {
		var id = obj.data["BDict_Id"];
		bdictForm1.config.bdictTypeId =curBDictTypeId;
		bdictForm1.config.PK = id;
		bdictForm1.config.formtype = "edit";
		bdictForm1.load(id, function(result) {

		});
	};
	var bdictTable1 = null;
	//列表配置信息
	function getBDictTableConfig() {
		return {
			title: '列表信息',
			height: 'full-150',
			elem: '#LAY-app-table-BDict',
			id: "LAY-app-table-BDict",
			toolbar: "",
			defaultToolbar: null,
			externalWhere: ""
		};
	};
	//初始化或刷新字典列表
	function initBDictTable() {
		bdictTable1 = bdictTable.render(getBDictTableConfig());
		onBDictTable();
	};
	//字典列表监听
	function onBDictTable() {
		//监听工具条
		table.on('tool(LAY-app-table-BDict)', function(obj) {
			var data = obj.data;

		});
		//监听行单击事件
		table.on('row(LAY-app-table-BDict)', function(obj) {
			//设置当前行为选中状态
			bdictTable1.setRadioCheck(obj);
			setTimeout(function() {
				onLoadForm(obj);
			}, 300);
		});
		//监听行双击事件
		table.on('rowDouble(LAY-app-table-BDict)', function(obj) {
			//是否弹出编辑或查看?
		});
		//监听排序事件 
		table.on('sort(LAY-app-table-BDict)', function(obj) {
			var sort = [];
			var cur = {
				"property": obj.field,
				"direction": obj.type
			};
			sort.push(cur);
			bdictTable1.setSort(sort);
			onBDictTable();
		});
	};
	//刷新字典列表
	function onRefreshBDictTable() {
		//获取查询条件
		bdictTable1.getSearchWhere();
		table.reload(bdictTable1.config.id, bdictTable1.config);
	};
	/**查询字典表单事件监听*/
	function onSearchForm() {
		$('.layui-form .layui-btn').on('click', function() {
			var type = $(this).data('type');
			onBDictActive[type] ? onBDictActive[type].call(this) : '';
		});
		$('#LAY-app-table-BDict-Search-LikeSearch').on('keydown', function(event) {
			if(event.keyCode == 13) onRefreshBreqFormTable();
		});
	};
	//列表查询表单按钮事件联动
	var onBDictActive = {
		add: function() {
			addBDict();
		},
		edit: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BDict");
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
			onRefreshBDictTable();
		},
		search: function() {
			onRefreshBDictTable();
		},
		reset: function() {
			bdictForm1.resetLoad();
		}
	};
	//新增字典
	function addBDict() {
		bdictForm1.config.PK = "";
		bdictForm1.config.formtype = "add";
		bdictForm1.isAdd();
	};
	//初始化
	function initAll() {
		initDefaultParams();
		initBDictTypeTable()
		initBDictTable();
		initBDictForm();
		onSearchForm();
	};
	initAll();
});