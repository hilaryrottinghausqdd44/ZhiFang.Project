layui.extend({
  	uxutil:'ux/util',
	dataadapter:'ux/dataadapter'
}).define(['table', 'uxutil', 'dataadapter'], function(exports){
	"use strict";
	
	var $ = layui.$;
    var table = layui.table;
    var uxutil = layui.uxutil;
    var dataadapter = layui.dataadapter;
    
    var BloodBaseTable = function(){
	    	var me = this;
	        me.searchInfo = {
	        	isLike: true,
	        	fields:[]
	        };
	        
	        me.config = {
	            page: true,
				limit: 10,
				url: '',
				renderOver: false,
				defaultToolbar: ['filter'],
				/**默认数据条件*/
			    defaultWhere: '',
			    /**内部数据条件*/
			    internalWhere: '',
			    /**外部数据条件*/
			    externalWhere: '',
			    response: dataadapter.toResponse(), 
			    parseData: function(res) {
					if (res.success){
						var result = dataadapter.toList(res);
						return result;
					} else{
						layer.msg(res.ErrorInfo);
					}
				},
			    done:function(res, curr, response){
			    	bloodBaseTable.doAutoSelect(this.table, 0);
			    }
		    };
	    };
	    
	BloodBaseTable.prototype.getLikeWhere = function(value){
		var me = this,
		    searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];
			
		//查询栏不为空时先处理内部条件再查询
		if ((searchInfo == null) || (len <= 0) || (!value)) return "";

		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	}
	
	/***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	BloodBaseTable.prototype.doAutoSelect = function(curTable, rowIndex){
		var data = table.cache[curTable.key] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var filter = curTable.config.elem.attr('lay-filter');
		var thatrow = $(curTable.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function () {
				table.cache[curTable.key][rowIndex] = [];
				tr.remove();
				curTable.scrollPatch();
			},
			updte:{}
		};
		setTimeout(function(){
		  layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 500);
				
	}
	
	//获取查询Fields
	BloodBaseTable.prototype.getFields = function (isString) {
		var me = this,
			columns = me.config.cols ? me.config.cols[0] : [],
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
	}
	
	//设置默认的查询where
	BloodBaseTable.prototype.setDefaultWhere = function (where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	}
	
	//设置外部传入的查询where
	BloodBaseTable.prototype.setExternalWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	}
	
	//获取查询where
	BloodBaseTable.prototype.getWhere = function () {
		var me = this,
			arr = [];
		//默认条件
		if (me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if (me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if (me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		var where = "";
		if (arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		where = {
			"where": where,
			'fields': me.getFields(true).join(',')
		};
		return where;
	}
	
	//获取参数,返回一个数组，包含两个元素，一个是对象，另一个是数组
	BloodBaseTable.prototype.getParams = function(tableName, field){
		var me = this;
		var fields;
		var fieldName;
		var entity = {};
		var fieldArr = [];
		var paramsArr = [];
		var reg = new RegExp(tableName + '_', 'i');
		var cols = me.config.cols ? me.config.cols : [];
		 for(var i = 0 ; i < cols.length; i++)
		 {
		 	fields = cols[i];
		 	layui.each(fields, function(key, item){
		 		fieldName = item.field ? item.field.replace(reg, "") : "";
		 		if (fieldName){
		 			fieldArr.push(fieldName);
		 			entity[fieldName] = field[item.field] ? field[item.field] : "";
		 		}
		 	})
		 }
		paramsArr.push(entity);
		paramsArr.push(fieldArr); 
		return paramsArr;
	}
	
	//获取新增参数
	BloodBaseTable.prototype.getAddParams = function(tableName, field){
		var me = this;
		var entity;
		var params = me.getParams(tableName, field);
        entity = params[0];
        //新增Id需要删除   
        if (entity.hasOwnProperty('Id')) delete entity.Id;
		return {
			entity: entity
		};	
	}
	
	//获取编辑的实体参数
    BloodBaseTable.prototype.getEditParams = function(tableName, field) {
		var me = this;
		var entity;
		var fieldList;
		var params = me.getParams(tableName, field);
		entity = params[0];
        fieldList = params[1].join(',');
 		return {
			entity: entity,
			fields: fieldList
		};	
	}; 
	
	var bloodBaseTable = new BloodBaseTable();
	
	exports("bloodBaseTable", bloodBaseTable);
})
