/**
	@name：layui.ux.data 数据处理
	@author：Jcall
	@version 2019-06-26
 */
layui.define(function(exports){
	"use strict";
	
	//外部接口
	var uxdata = {
		//ES6开始支持Map对象
		Map:function(){
			var me = this;
			//数据
			var items = {};
			//总数
			me.size = 0;
			
			// has方法
			me.has = function(key) {
				return items[key] ? true : false;
			};
			// set(key, val)方法
			me.set = function(key, val) {
				var hasKey = me.has(key);
				if(!hasKey){
					me.size++;
				}
				items[key] = val;
				me.listeners.set(items[key],hasKey);
				me.listeners.change(me,'set',items[key],hasKey);
			};
			// get(key)方法
			me.get = function(key) {
				return me.has(key) ? items[key] : undefined;
			};
			//update(key,fields)方法
			me.update = function(key,fields){
				if(!me.has(key)){
					return false;
				}else{
					var data = items[key];
					for(var i in fields){
						data[i] = fields[i];
					}
					return true;
				}
			};
			// del(key)方法
			me.del = function(key) {
				if (me.has(key)) {
					delete items[key];
					me.size--;
					me.listeners.del(key,true);
					me.listeners.change(me,'del',key,true);
					return true;
				}else{
					me.listeners.del(key,false);
					me.listeners.change(me,'del',key,false);
					return false;
				}
			};
			// clear()方法
			me.clear = function() {
				items = {};
				me.size = 0;
				me.listeners.clear(me);
				me.listeners.change(me,'clear');
			};
			// keys()方法
			me.keys = function() {
				//return Object.keys(items);
			};
			// values()方法
			me.values = function() {
				return items;
			};
			// forEach(fn, context)方法
//			this.forEach = function(fn, context = this) {
//				for (let i = 0; i < this.size; i++) {
//					let key = Object.keys(items)[i];
//					let value = Object.values(items)[i];
//					fn.call(context, value, key, items);
//				}
//			};
			//监听
			//map：字典对象；
			//eventName：动作名称；set/delete/clear
			//value：value/key值；set(Value添加的数据)，delete(key删除的主键)
			//hasKey：是否存在主键
			me.listeners = {
				//数据变化
				change:function(map,eventName,value,hasKey){},
				//添加数据
				set:function(value,hasKey){},
				//删除数据
				del:function(key,hasKey){},
				//清空数据
				clear:function(map){}
			}
		}
	};
	
	//暴露接口
	exports('uxdata',uxdata);
});