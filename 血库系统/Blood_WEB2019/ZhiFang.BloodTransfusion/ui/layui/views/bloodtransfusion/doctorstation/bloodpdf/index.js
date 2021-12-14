/**
	@name：配血记录PDF查看
	@author：longfc
	@version 2019-10-24
 */
layui.extend({
	uxutil: '/ux/util',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig'
}).use(['uxutil', 'form', 'layer', 'cachedata',bloodsconfig'],
	function() {
		"use strict";

		var $ = layui.$,
			uxutil = layui.uxutil,
			layer = layui.layer,
			form = layui.form,
			bloodsconfig = layui.bloodsconfig;

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
		//初始化表单下拉选择框
		function initForm() {
			initSelectBlood(function(html, selectedId) {
				form.render();
				if (selectedId) onPreviewIframeById(selectedId);
				
			});
		};
		//下拉选择数据源加载
		function initSelectBlood(callback) {
			getOptionList(function(html, selectedId) {
				$('[lay-filter="select_blood"]').empty().append(html);
				if (callback) callback(html, selectedId);
			});
		};
		//获取下拉选择项集合信息
		function getOptionList(callback) {
			var me = this;
			var selectedId = "";
			var valueKey = "Id",
				nameKey = "Date";
			var url = bloodsconfig.getBloodBillInfoUrl() + "?sReqFormNo=" + defaultParams.ReqFormNo + "&sType=" +
				defaultParams.PdfType;
			uxutil.server.ajax({
				url: url
			}, function(result) {
				var html =[];
				 html.push("<option value=''></option>");
				if (result.success) {
					var type = typeof result.ResultDataValue;
					var data = result.ResultDataValue;
					if (type == 'string') {
						data = result.ResultDataValue ? $.parseJSON(result.ResultDataValue) : {};
					}
					for (var i = 0; i < data.length; i++) {
						var text = ""+data[i][valueKey];// + "||" + data[i][nameKey];
						if (i == 0) {
							selectedId = data[i][valueKey];
							html.push('<option selected value="' + data[i][valueKey] + '">' + text + '</option>');
						} else {
							html.push('<option value="' + data[i][valueKey] + '">' + text + '</option>');
						}
					}
				}
				html=html.join("");
				if (callback) callback(html, selectedId);
			});
		};
		//各表单组件事件监听
		function onFormEvent() {
			form.on('select(select_blood)', function(data) {
				onPreviewIframeById(data.value);
			});
			//按钮事件联动
			var onDocActive = {
				refresh: function() {
					onPreviewIframeById(2);
				},
				close: function() {
					var index = parent.layer.getFrameIndex(window.name);
					parent.layer.close(index);
				}
			};
			$('.layui-form .layui-form-item .layui-btn').on('click', function() {
				var type = $(this).data('type');
				onDocActive[type] ? onDocActive[type].call(this) : '';
			});
		};
		//按Iframe预览
		function onPreviewIframeById(id) {
			setTimeout(function() {
				var url = bloodsconfig.getBloodReportImageUrl() + "?sReqFormNo=" + defaultParams.ReqFormNo +
					"&sReportID=" + id + "&sType=" + defaultParams.PdfType;
				var iframe1 = $("#iframe_pdf_preview");
				var src = iframe1.attr("src");
				iframe1.attr("src", url);
			}, 500);
		};
		//初始化
		function initAll() {
			initParams();
			initForm();
			onFormEvent();
		};
		initAll();
	});
