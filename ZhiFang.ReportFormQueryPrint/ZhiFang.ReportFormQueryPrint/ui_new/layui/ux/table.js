/**
	@name：layui.ux.table 列表基类  
	@author：Jcall
	@version 2019-03-25
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		table = layui.table,
		ELEM_CELL = '.layui-table-cell';
		
	var uxtable = {
		config:{
			//主键字段
			PKField:"Id",
			//监听
			beforeRender:function(that){},
			afterRender:function(that){},
			//返回数据监听
			afterLoad:function(result){}
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				
				//数据监听
				var bo = me.config.afterLoad(res);
				if(bo === false){
					return {
						"code": 1, //解析接口状态
						"msg": '', //解析提示文本
						"count": 0, //解析数据长度
						"data": []
					};
				}
				
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue || {},
					list = [];
					
				if(res.success){
					if(type == 'string'){
						data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					}
					for(var i in data.list){
						list.push(me.changeData(data.list[i]));
					}
				}
				
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
				};
			}
		},me.config,uxtable.config,setings);
		
		if(typeof setings.changeData == 'function'){
			me.changeData = setings.changeData;
		}
	};
	Class.pt = Class.prototype;
	
	//数据处理，需要时可重写
	Class.pt.changeData = function(data){
		return data;
	};
	//数据操作
	//根据主键获取行数据
	Class.pt.getRowItemById = function(value){
		var me = this,
			that = me.instance.config.instance,
			list = me.table.cache[that.key] || [],
			len = list.length,
			data = null;
			
		for(var i=0;i<len;i++){
			if(list[i][me.config.PKField] == value){
				data = list[i];
				break;
			}
		}
		
		return data;
	};
	//根据主键删除一行数据
	Class.pt.deleteRowItem = function(id){
		var me = this,
			that = me.instance.config.instance,
			list = me.table.cache[that.key] || [],
			len = list.length,
			index = null;
			
		for(var i=0;i<len;i++){
			if(list[i][me.config.PKField] == id){
				index = i;
				break;
			}
		}
		
		if(index == null){//不存在
			return false;
		}else{
			var tr = that.layBody.find('tr[data-index="'+ index +'"]');
			list.splice(index,1);
			tr.remove();
			
			me.reloadTable();
			
			return index;
		}
	};
	//添加数据
	Class.pt.addRowItem = function(item,isTop){
		var me = this,
			list = me.table.cache[that.key] || [];
			
		if(isTop){
			list.unshift(item);
		}else{
			list.push(item);
		}
		me.reloadTable();
	};
	//重载数据
	Class.pt.reloadTable = function(callback){
		var me = this,
			that = me.instance.config.instance,
			list = me.table.cache[that.key] || [];
			
		me.instance.reload({data:list});
		me.config.data = list;
		that.scrollPatch();
		
		if(typeof callback === 'function'){
			callback();
		}
	};
	//重载数据
	Class.pt.reload = function(config){
		var me = this;
		//防止缓存
		config.where = config.where || {};
		config.where.t = new Date().getTime();
		//重新定位到第一页
		config.page = config.page || {curr:1};
		
		me.instance.reload(config);
	};
	//清空数据
	Class.pt.clearTable = function(){
		var me = this,
			that = me.instance.config.instance,
			list = me.table.cache[that.key] || [];
			
		list = [];
		
		me.reloadTable();
	};
	//更新一行数据
	Class.pt.updateRowItem = function(fields,key){
		var me = this,
			that = me.instance.config.instance,
			list = me.table.cache[that.key] || [],
			len = list.length,
			index = null;
			
		for(var i=0;i<len;i++){
			if(list[i][key] == fields[key]){
				index = i;
				break;
			}
		}
		
		if(index == null){//不存在
			return false;
		}else{
			var tr = that.layBody.find('tr[data-index="'+ index +'"]'),
				data = list[index],
				cacheData = table.cache[that.key][index];
				
			fields = fields || {};
			layui.each(fields, function(ind, value){
				if(ind in data){
					var templet, td = tr.children('td[data-field="'+ ind +'"]');
					data[ind] = value;
					cacheData[ind] = value;
					that.eachCols(function(i, item2){
						if(item2.field == ind && item2.templet){
							templet = item2.templet;
						}
					});
					td.children(ELEM_CELL).html(function(){
						return templet ? function(){
							return typeof templet === 'function' 
							? templet(data)
							: laytpl($(templet).html() || value).render(data)
						}() : value;
					}());
					td.data('content', value);
				}
			});
			
			return true;
		}
	};
	
	//主入口
	uxtable.render = function(options){
		var me = new Class(options);
		//渲染前处理
		me.config.beforeRender(me);
		//列表对象原型
		me.table = table;
		//列表对象实例
		me.instance = table.render($.extend({},me.config));
		//渲染后处理
		me.config.afterRender(me);
		
		return me;
	}
	
	//暴露接口
	exports('uxtable',uxtable);
});