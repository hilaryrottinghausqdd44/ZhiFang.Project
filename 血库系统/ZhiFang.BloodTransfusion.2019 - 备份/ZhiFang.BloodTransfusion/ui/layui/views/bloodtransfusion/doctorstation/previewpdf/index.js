/**
	@name：配血记录PDF查看
	@author：longfc
	@version 2019-10-24
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig',
	previewpdfTable: 'views/bloodtransfusion/doctorstation/previewpdf/previewpdfTable'
}).use(['uxutil', 'previewpdfTable', 'table',"cachedata", 'bloodsconfig'],
	function() {
		"use strict";

		var $ = layui.$,
			uxutil = layui.uxutil,
			table = layui.table,
			bloodsconfig = layui.bloodsconfig,
			previewpdfTable = layui.previewpdfTable;
		//列表实例对象	
		var previewpdfTable1 = null;
		/**默认传入参数*/
		var defaultParams = {
			ReqFormNo: "", //申请单号
			PdfType: "" //1:配血记录;2:发血记录;
		};
		//初始化默认传入参数信息
		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
			//医嘱申请单号
			if (params["reqFormNo"]) defaultParams.ReqFormNo = params["reqFormNo"];
			if (params["type"]) defaultParams.PdfType = params["type"];
		};
		//各表单组件事件监听
		function onFormEvent() {

		};
		//当前库存列表配置信息
		function getpreviewpdfTableConfig() {
			return {
				title: '配血记录',
				elem: '#table_previewpdf',
				id: "table_previewpdf",
				filter: "table_previewpdf",
				height: 'full-50', //full-50
				reqFormNo: defaultParams.ReqFormNo,
				pdfType: defaultParams.PdfType
			};
		};
		/**初始化配血记录列表*/
		function initpreviewpdfTable() {
			var config = getpreviewpdfTableConfig();
			previewpdfTable1 = previewpdfTable.render(config);
			onpreviewpdfTable();
		};
		//配血记录列表
		function onpreviewpdfTable() {
			//监听行单击事件
			table.on('row(table_previewpdf)', function(obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				setTimeout(function() {
					var id = obj.data["Id"];
					onPreviewIframeById(id);
				}, 300);
			});
		};
		//初始化列表
		function initTable() {
			initpreviewpdfTable();
		};
		//刷新配血记录列表
		function onRefreshpreviewpdfTable() {
			previewpdfTable1.config.reqFormNo = defaultParams.ReqFormNo;
			previewpdfTable1.config.pdfType = defaultParams.PdfType;
			previewpdfTable1 = previewpdfTable1.onSearch();
		};
		//按Iframe预览
		function onPreviewIframeById(id) {
			setTimeout(function() {
				var url = bloodsconfig.getBloodReportImageUrl() + "?sReqFormNo=" + defaultParams.ReqFormNo +
					"&sReportID=" + id + "&sType=" + defaultParams.PdfType;
				var iframe1 = $("#iframe_pdf_preview");
				var src = iframe1.attr("src");
				iframe1.attr("src", url);
				iframe1.width(parseInt(iframe1.width()*0.9));
				iframe1.height(parseInt(iframe1.height()*0.98));
			}, 300);
		};
		//初始化
		function initAll() {
			initParams();
			initTable();
			onFormEvent();
		};
		initAll();
	});
