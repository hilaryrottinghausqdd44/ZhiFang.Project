/**
	@name：血制品选择
	@author：longfc
	@version 2019-06-27
 */
layui.extend({
	uxutil: 'ux/util',
	chooseBloodstyleTable: '/views/bloodtransfusion/doctorstation/edit/chooseBloodstyleTable'
}).use(['layer', 'uxutil', 'form', 'table', 'chooseBloodstyleTable'], function () {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var table = layui.table;
	var form = layui.form;
	var chooseBloodstyleTable = layui.chooseBloodstyleTable;
	var height = $(document).height();
	//血制品选择tableName
	var chooseTableName = "chooseBloodstyle";

	/**默认传入参数*/
	var defaultParams = {
		idStr: "" //已经选择的血制品Id
	};
	//初始化默认传入参数信息
	function initDefaultParams() {
		//接收传入参数
		var params = uxutil.params.get();
		//医生Id
		if (params && params["idStr"]) defaultParams.idStr = params["idStr"];
	};

	var chooseBloodstyleTable1 = null;
	//血制品选择列表配置信息
	function getBloodstyleTableConfig() {
		return {
			title: '血制品选择信息',
			elem: '#LAY-app-table-choose-Bloodstyle',
			id: "LAY-app-table-choose-Bloodstyle",
			toolbar: "",
			defaultToolbar: null,
			externalWhere: ""
		};
	};

	//按钮事件
	var active = {
		refreshBloodstyle: function () {
			onRefreshBloodstyle();
		},
		choose: function () {

		}
	};
	/**查询表单事件监听*/
	function onSearchForm() {
		$('.layui-form .layui-form-item .layui-inline .layui-btn').on('click', function () {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
		$('#LAY-app-table-choose-Bloodstyle-LikeSearch').on('keydown', function (event) {
			if (event.keyCode == 13) onRefreshBloodstyle();
		});
	};
	//选择列表监听
	function onBloodstyleTable() {
		//监听复选框选择
		/*		table.on('checkbox(LAY-app-table-choose-Bloodstyle)', function (obj) {
					sessionData();
				});
				//监听行双击事件
				table.on('rowDouble(LAY-app-table-choose-Bloodstyle)', function (obj) {
					//sessionData();
				});*/
		//监听行单击事件
		table.on('row(LAY-app-table-choose-Bloodstyle)', function (obj) {
			sessionData(obj);
		});
		//监听单元格编辑(不监听时,table对应的缓存申请量不生效)
		table.on('edit(LAY-app-table-choose-Bloodstyle)', function (obj) {
			sessionData(obj);
		});
	};
	//选择的血制品处理
	function sessionData(obj) {
		//当前选择行
//		var checkList = table.checkStatus('LAY-app-table-choose-Bloodstyle');
		var curList = obj.data; //checkList.data; 2020-10-10 by xiehz modify
		var rd = obj.tr.find('input[type="radio"]');
	    rd && rd.prop('checked', true); 
	    form.render('radio');
		//已经选择的血制品行
		var dataList = [];
		layui.sessionData(chooseTableName, { key: "dataList", remove: true });
		if (!curList || curList.length <= 0) dataList = [];
        dataList.push(curList);
//		$.each(curList, function (curIndex, curData) {
//			dataList.push(curData);
//		}); 2020-10-10 by xiehz modify
		var settings = { key: "dataList", value: dataList };
		layui.sessionData(chooseTableName, settings);
	};
	//刷新血制品选择列表
	function onRefreshBloodstyle() {
		//获取查询条件
		chooseBloodstyleTable1.getSearchWhere();
		table.reload(chooseBloodstyleTable1.config.id, chooseBloodstyleTable1.config);
		onBloodstyleTable();
	};
	//初始化
	function initBloodstyleTable() {
		//血制品选择列表初始化
		chooseBloodstyleTable1 = chooseBloodstyleTable.render(getBloodstyleTableConfig());
	};
	//初始化页面组件
	function initAll() {
		initDefaultParams();
		onSearchForm();
		initBloodstyleTable();
		onBloodstyleTable();
	}
	initAll();
});