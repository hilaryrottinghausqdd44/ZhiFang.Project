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
		SYS_KEY: "BLTF_SYS",
		SYS_CACHE_KEY: "CacheData",
		commonDataKey: "CacheData",
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
		setCache: function(dictKey, data, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			if(!dictKey) dictKey = me.commonDataKey;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};
			if(!win[me.SYS_KEY][me.SYS_CACHE_KEY]) win[me.SYS_KEY][me.SYS_CACHE_KEY] = {};
			win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey] = data;
		},
		/***
		 * 获取父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey:CacheData的key
		 */
		getCache: function(dictKey, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			var data = "";
			if(!dictKey) dictKey = me.commonDataKey;
			if(!win) return data;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};

			if(win[me.SYS_KEY][me.SYS_CACHE_KEY]) {
				data = win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey];
			}
			return data;
		},
		/***
		 * 删除父窗体对象(window)的缓存数据(CacheData)
		 * @param {Object} dictKey
		 */
		delete: function(dictKey, win) {
			var me = this;
			if(!win) win = window;
			win = me.getTop(win);
			if(!win) return;
			if(!win[me.SYS_KEY]) win[me.SYS_KEY] = {};
			if(win[me.SYS_KEY][me.SYS_CACHE_KEY]) {
				if(dictKey) {
					delete win[me.SYS_KEY][me.SYS_CACHE_KEY][dictKey];
				} else {
					win[me.SYS_KEY][me.SYS_CACHE_KEY] = {};;
				}
			}
		}
	};

	//暴露接口
	exports('cachedata', cachedata);
});