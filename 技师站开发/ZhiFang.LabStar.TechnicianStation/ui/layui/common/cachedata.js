/**
	@name：父窗体临时数据缓存
	@author：longfc
	@version 2019-08-12
 */
layui.extend({
	//uxutil: 'ux/util',
	//uxdata: 'ux/data'
}).define(['jquery'], function(exports) {
	"use strict";

	var $ = layui.jquery;

	var cachedata = {
		commonDataKey: "cachedata",
		/***
		 * 获取某一window的最顶级的父窗体对象
		 * @param {Object} curWin:当前window对象
		 */
		getTop: function(curWin) {
			var me = this;
			curWin = curWin || window;
			var win = curWin.top == curWin ? curWin : me.getTop(curWin.top);
			return win;
		},
		/***
		 * 设置父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 * @param {Object} data:需要缓存的数据信息
		 */
		setCache: function(dictKey, data) {
			var me = this;
			if(!dictKey) dictKey = me.commonDataKey;
			var win = me.getTop(window);
			if(!win.CacheData) win.CacheData = {};
			win.CacheData[dictKey] = data;
		},
		/***
		 * 获取父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 */
		getCache: function(dictKey) {
			var me = this;
			var data = "";
			if(!dictKey) dictKey = me.commonDataKey;
			var win = me.getTop(); //layui.cachedata
			if(!win) return data;
			if(win.CacheData) {
				data = win.CacheData[dictKey];
			}
			return data;
		},
		/***
		 * 删除父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey
		 */
		delete: function(dictKey) {
			var me = this;
			var win = me.getTop();
			if(!win) return;
			if(win.CacheData) {
				if(dictKey) {
					delete win.CacheData[dictKey];
				} else {
					win.CacheData = {};
				}
			}
		}
	};

	//暴露接口
	exports('cachedata', cachedata);
});