/**
	@name：下拉选择框数据处理工具类
	@author：longfc
	@version 2019-07-19
 */
layui.extend({
	uxutil: 'ux/util',
	uxdata: 'ux/data',
	cachedata: 'common/cachedata',
}).define(['jquery', 'uxutil', "cachedata"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var cachedata = layui.cachedata;

	var dictselect = {
		commonDataKey: "DictSelectData",
		setCache: function(dictKey, data) {
			var me = this;
			var win = me.getTop(window);
			if(!win.CachedOptionList) win.CachedOptionList = {};
			win.CachedOptionList[dictKey] = data;
		},
		getCache: function(dictKey) {
			var me = this;
			var data = "";
			var commonData = "";
			var win = me.getTop();
			if(!win) return commonData;
			if(win.CachedOptionList) {
				data = win.CachedOptionList[dictKey];
			}
			return data;
		},
		getTop: function(curWin) {
			var me = this;
			curWin = curWin || window;
			var win = curWin.top == curWin ? curWin : me.getTop(curWin.top);
			return win;
		},
		getOptionList: function(dictKey, url, valueKey, nameKey, callback) {
			var me = this;
			if(!dictKey && callback) return callback("");

			var html = me.getCache(dictKey);
			if(html && html.length > 0 && callback) return callback(html);

			var fields = [];
			fields.push(valueKey);
			if(valueKey != nameKey)
				fields.push(nameKey);
			url = url + "&fields=" + fields.join(",");
			uxutil.server.ajax({
				url: url
			}, function(data) {
				html = "<option value=''></option>";
				if(data.success) {
					var result = data.value || [];
					if(result && result.list) result = result.list;
					for(var i = 0; i < result.length; i++) {
						html += '<option value="' + result[i][valueKey] + '">' + result[i][nameKey] + '</option>';
					}
				}
				me.setCache(dictKey, html);
				if(callback) callback(html);
			});
		},
		loadAllDict: function() {
			var me = this;
			var commonData = {};
			layui.each(me.dictList, function(dictKey, value) {
				me.dictList[dictKey](function(html) {
					//commonData[dictKey]=html;
				});
			});
			//me.setCache(me.commonDataKey, commonData);
		},
		dictList: {
			/***
			 * @desc 就诊类型下拉选择配置信息 
			 * @param {Object} inst:"LBSickType"
			 * @param {Object} options:{}
			 */
			LBSickType: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&page=1&start=0&limit=1000";
				dictselect.getOptionList("LBSickType", selectUrl, "LBSickType_Id", "LBSickType_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 检验小组下拉选择配置信息 
			 * @param {Object} inst:"LBSection"
			 * @param {Object} options:{}
			 */
			LBSection: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&page=1&start=0&limit=1000";
				dictselect.getOptionList("LBSection", selectUrl, "LBSection_Id", "LBSection_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 样本类型下拉选择配置信息 
			 * @param {Object} inst:"LBSampleType"
			 * @param {Object} options:{}
			 */
			LBSampleType: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&page=1&start=0&limit=1000";
				dictselect.getOptionList("LBSampleType", selectUrl, "LBSampleType_Id", "LBSampleType_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 检验大组下拉选择配置信息 
			 * @param {Object} inst:"LBSuperSection"
			 * @param {Object} options:{}
			 */
			LBSuperSection: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSuperSectionByHQL?isPlanish=true&page=1&start=0&limit=1000";
				dictselect.getOptionList("LBSuperSection", selectUrl, "LBSuperSection_Id", "LBSuperSection_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 检验项目下拉选择配置信息 
			 * @param {Object} inst:"LBItem"
			 * @param {Object} options:{}
			 */
			LBItem: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true&page=1&start=0&limit=1000";
				dictselect.getOptionList("LBItem", selectUrl, "LBItem_Id", "LBItem_CName", function(html) {
					if(callback) callback(html);
				});
			}
		}
	};

	//暴露接口
	exports('dictselect', dictselect);
});