/**
	@name：BS血库下拉选择框工具类
	@author：longfc
	@version 2019-07-19
 */
layui.extend({
	uxutil: 'ux/util',
	uxdata: 'ux/data',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
}).define(['jquery', 'uxutil', "cachedata"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	//var uxdata = layui.uxdata;
	var cachedata = layui.cachedata;

	var bloodSelectData = {
		commonDataKey: "BloodSelectData",
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
			var win = me.getTop(); //layui.bloodSelectData
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
			 * @desc 部门下拉选择配置信息 
			 * @param {Object} inst:"Department"
			 * @param {Object} options:{}
			 */
			Department: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true";
				bloodSelectData.getOptionList("Department", selectUrl, "Department_Id", "Department_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 医生下拉选择配置信息 
			 * @param {Object} inst:"Doctor"
			 * @param {Object} options:{}
			 */
			Doctor: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDoctorByHQL?isPlanish=true";//&page=1&start=0&limit=10000
				bloodSelectData.getOptionList("Doctor", selectUrl, "Doctor_Id", "Doctor_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 病区下拉选择配置信息 
			 * @param {Object} inst:"Department"
			 * @param {Object} options:{}
			 */
			WardType: function(callback) {
				var me = this;
				var html = "";//&page=1&start=0&limit=10000
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchWardTypeByHQL?isPlanish=true";
				bloodSelectData.getOptionList("WardType", selectUrl, "WardType_Id", "WardType_CName", function(html) {
					if(callback) callback(html);
				});
			},
			/***
			 * @desc 医嘱作废原因下拉选择配置信息 
			 * @param {Object} inst:"Department"
			 * @param {Object} options:{}
			 */
			BReqFormObsolete: function(callback) {
				var me = this;
				var html = "";
				var selectUrl = uxutil.path.ROOT + "/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true";
				var hql = "&where=bdict.IsUse=1 and bdict.BDictType.DictTypeCode='BReqFormObsolete'";
				selectUrl += hql;
				bloodSelectData.getOptionList("BReqFormObsolete", selectUrl, "BDict_Id", "BDict_CName", function(html) {
					if(callback) callback(html);
				});
			}
		}
	};

	//暴露接口
	exports('bloodSelectData', bloodSelectData);
});