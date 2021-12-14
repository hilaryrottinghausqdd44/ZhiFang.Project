/**
	@name：layui.ux.data 数据处理
	@author：Jcall
	@version 2019-06-26
 */
layui.define('jquery',function(exports){
	"use strict";
	
	var $ = layui.$;
	
	//外部接口
	var uxdata = {
		//ES6开始支持Map对象
		Map:function(){
			//let items = {};//ES6开始支持，部分浏览器不支持
			var items = {};
			this.size = 0;
			// 操作方法
			// has方法
			this.has = function(val) {
				return items.hasOwnProperty(val);
			};
			// set(key, val)方法
			this.set = function(key, val) {
				var hasKey = this.has(key);
				if(!this.has(key)){
					this.size++;
				}
				items[key] = val;
				this.listeners.set(items[key],hasKey);
				this.listeners.change(this,'set',items[key],hasKey);
			};
			// get(key)方法
			this.get = function(key) {
				return this.has(key) ? items[key] : undefined;
			};
			//update(key,fields)方法
			this.update = function(key,fields){
				if(!this.has(key)){
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
			this.del = function(key) {
				if (this.has(key)) {
					delete items[key];
					this.size--;
					this.listeners.del(key,true);
					this.listeners.change(this,'del',key,true);
					return true;
				}
				this.listeners.del(key,false);
				this.listeners.change(this,'del',key,false);
				return false;
			};
			// clear()方法
			this.clear = function() {
				items = {};
				this.size = 0;
				this.listeners.clear(this);
				this.listeners.change(this,'clear');
			};
			// 遍历方法
			// keys()方法
			this.keys = function() {
				return Object.keys(items);
			};
			// values()方法
			this.values = function() {
				return items;
				//return Object.values(items);
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
			this.listeners = {
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