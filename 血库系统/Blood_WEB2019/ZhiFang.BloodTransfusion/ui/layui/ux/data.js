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
		Map:function(setting){
			var me =this;
			
			if(setting && setting.top === true){
				var win = uxdata.getTop(window);
				win.CommonData = me;
			}
			
			//let items = {};//ES6开始支持，部分浏览器不支持
			var items = {};
			me.size = 0;
			// 操作方法
			// has方法
			me.has = function(val) {
				return items.hasOwnProperty(val);
			};
			// set(key, val)方法
			me.set = function(key, val) {
				var hasKey = me.has(key);
				if(!me.has(key)){
					me.size++;
				}
				items[key] = val;
				me.listeners.set(items[key],hasKey);
				me.listeners.change(this,'set',items[key],hasKey);
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
			// delete(key)方法
			me.delete = function(key) {
				if (me.has(key)) {
					delete items[key];
					me.size--;
					me.listeners.delete(key,true);
					me.listeners.change(this,'delete',key,true);
					return true;
				}
				me.listeners.delete(key,false);
				me.listeners.change(this,'delete',key,false);
				return false;
			};
			// clear()方法
			me.clear = function() {
				items = {};
				this.size = 0;
				this.listeners.clear(this);
				this.listeners.change(this,'clear');
			};
			// 遍历方法
			// keys()方法
			me.keys = function() {
				return Object.keys(items);
			};
			// values()方法
			me.values = function() {
				return Object.values(items);
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
				delete:function(key,hasKey){},
				//清空数据
				clear:function(map){}
			};
		}
	};
	//获取顶级页面
	uxdata.getTop = function(curWin){
		curWin = curWin || window;
		var win = curWin.top == curWin ? curWin : uxdata.getTop(curWin.top);
		return win;
	}
	
	//暴露接口
	exports('uxdata',uxdata);
});