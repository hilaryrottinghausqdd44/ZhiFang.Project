/**
	@name：table
	@author：liangyl
	@version 
 */
layui.define('table',function(exports) {
	"use strict";
     var $ = layui.$,
        table =layui.table,
	    uxutil= layui.uxutil;
	var uxtable = {
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
		},
		/**@overwrite 改变返回的数据*/
		parseData : function(res) {
			if(res.rowCount == 0 ||res.rowCount == '0'){
				res.code = 0;
				res.data = [];
				return;
			}
			return {
				"code": 0,
				"msg": "",
				"count": res.length,
				"data": res
			}
		},
	    /**清空数据,禁用功能按钮*/
		clearData: function(tableId) {
		    tableId.reload({
	            data: []
	        }); //清空数据
		},
		/**创建数据字段*/
		getStoreFields: function(tableId,isString) {
			var columns = tableId.config.cols[0] || [],
				length = columns.length,
				fields = [];
			for (var i = 0; i < length; i++) {
				if (columns[i].field) {
					var obj = isString ? columns[i].field : {
						name: columns[i].field,
						type: columns[i].type ? columns[i].type : 'string'
					};
					fields.push(obj);
				}
			}
			return fields;
		},
		/**获取带查询参数的URL*/
		getLoadUrl: function(tableId,url,where) {
			var me = this,
				arr = [];
	
			var url = (url.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + url;
	
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + this.getStoreFields(tableId,true).join(',');
	
//			//默认条件
//			if (me.defaultWhere && me.defaultWhere != '') {
//				arr.push(me.defaultWhere);
//			}
//			//内部条件
//			if (me.internalWhere && me.internalWhere != '') {
//				arr.push(me.internalWhere);
//			}
//			//外部条件
//			if (me.externalWhere && me.externalWhere != '') {
//				arr.push(me.externalWhere);
//			}
//			var where = arr.join(") and (");
//			if (where) where = "(" + where + ")";
//			if (where) {
//				url += '&where=' + JShell.String.encode(where);
//			}
	
			return url;
		},
		//列表数据加载
	    onSearch : function(tableId,url,where){
			uxutil.server.ajax({
				url:url
//					url:getLoadUrl(basic.CollectionTable.myTable,url,where)
			},function(data){
				if(data){
					tableId.reload({
                        data: data.data
                    })
				}else{
					layer.msg(data.msg);
				}
			});
	   }
	};
    //点击行checkbox选中
    $(document).on("click",".layui-table-body table.layui-table tbody tr", function () {
        var index = $(this).attr('data-index');
        var tableBox = $(this).parents('.layui-table-box');
        //存在固定列
        if (tableBox.find(".layui-table-fixed.layui-table-fixed-l").length>0) {
            var tableDiv = tableBox.find(".layui-table-fixed.layui-table-fixed-l");
        } else {
            var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
        }
        var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
        if (checkCell.length>0) {
            checkCell.click();
        }
    });

    $(document).on("click", "td div.laytable-cell-checkbox div.layui-form-checkbox", function (e) {
        e.stopPropagation();
    });
	//暴露接口
	exports('uxtable', uxtable);
});
