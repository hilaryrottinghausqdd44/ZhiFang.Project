/**
	@name：当前库存信息列表
	@author：longfc
	@version 2019-10-28
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	stockTable: 'views/bloodtransfusion/doctorstation/stock/stockTable'
}).use(['uxutil', 'stockTable'],
	function() {
		"use strict";

		var $ = layui.$,
			uxutil = layui.uxutil,
			stockTable = layui.stockTable;
		//列表实例对象	
		var stockTable1 = null;
		/**默认传入参数*/
		var defaultParams = {
			ReqFormNo: "", //申请单号
		};
		//初始化默认传入参数信息
		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
			//医嘱申请单号
			if (params["reqFormNo"]) defaultParams.ReqFormNo = params["reqFormNo"];
		};
		//各表单组件事件监听
		function onFormEvent() {		
			
		};
		//当前库存列表配置信息
		function getstockTableConfig() {
			return {
				title: '当前库存信息',
				elem: '#table_stock',
				id: "table_stock",
				filter: "table_stock",
				height: 'full-20'
			};
		};
		/**初始化当前库存信息列表*/
		function initstockTable() {
			var config = getstockTableConfig();
			stockTable1 = stockTable.render(config);
			onstockTable();
		};
		//当前库存信息列表
		function onstockTable() {
			
		};
		//初始化列表
		function initTable() {
			initstockTable();
		};
		//刷新当前库存信息列表
		function onRefreshstockTable() {
			stockTable1.setReqFormNo(defaultParams.ReqFormNo);
			stockTable1 = stockTable1.onSearch();
		};
		//初始化
		function initAll() {
			initParams();
			initTable();
			onFormEvent();
		};
		initAll();
	});
