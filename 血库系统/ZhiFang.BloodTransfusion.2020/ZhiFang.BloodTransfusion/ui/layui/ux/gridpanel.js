/**
	@name：layui.ux.modules.gridpanel 数据列表工具方法
	@author：longfc
	@version 2019-04-29
 */
layui.define(function(exports) {
	"use strict";

	var gridpanel = {
		/**
		 * @method
		 * @param {Type} searchInfo 列表查询输入框查询信息
		 * @returns {Type} 列表部分查询条件
		 * @desc 查询栏不为空时先处理内部条件再查询
		 */
		getSearchWhere: function(searchInfo, value) {
			if (!value || !searchInfo) return "";

			var isLike = searchInfo.isLike,
				fields = searchInfo.fields || [],
				len = fields.length,
				where = [];
			for (var i = 0; i < len; i++) {
				if (isLike) {
					where.push(fields[i] + " like '%" + value + "%'");
				} else {
					where.push(fields[i] + "='" + value + "'");
				}
			}
			return where.join(' or ');
		}
	};
	//暴露接口
	exports('gridpanel', gridpanel);
});
