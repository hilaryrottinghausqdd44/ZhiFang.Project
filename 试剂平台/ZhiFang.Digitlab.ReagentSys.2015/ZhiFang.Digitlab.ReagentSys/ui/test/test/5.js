Ext.define('myPanel',{
	extend:'Ext.panel.Panel',
	
	//------------配置代码----------
	applist:[],
	//-----------------------------
	count:0,
	//----------------处理代码---------------
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//外引用的内部应用
		me.initInfos();
	},
	initInfos:function(){
		var me = this;
		var applist = me.applist;
		for(var i in applist){
			var app = applist[i];
			me.addApp(app);
		}
	},
	addApp:function(app){
		var me = this;
		var id  = app.id;
		var par = app.params || {};
		var callback = me.createCallback(par);
		getAppObject(id,callback);
		
	},
	createCallback:function(par){
		var me = this;
		var callback = function(info){
			var p = me.getPanel(info,par);
			me.add(p);
		};
		return callback;
	},
	getPanel:function(info,par){
		var me = this;
		var p = null;
		
		par.autoScroll = true;
		
		if(info.success){
			p = Ext.create(info.data,par);
		}else{
			par.html = info.ErrorInfo;
			p = Ext.create('Ext.panel.Panel',par);
		}
		return p;
	},
	//---------------------------------------
	/**
	 * 内部全部渲染完毕
	 * @private
	 */
	allover:function(){
		var me = this;
		me.count++;
		if(me.count == me.applist.length){
			me.initListeners();
		}
		if(Ext.typeOf(me.callback)=='function'){
			me.callback(me);
		}
	},
	//-----------------定制代码---------------
	width:835,
	height:349,
	title:'部门维护',
	layout:'border',
	initComponent:function(){
		var me = this;
		me.applist = [{
			id:'4716092656592728318',
			params:{
				width:478,
				height:322,
				region:'center',
				itemId:'list',
				callback:function(){me.allover();}
			}
		},{
			id:'4764298649770970364',
			params:{
				collapsible:true,
				split:true,
				width:350,
				height:322,
				region:'east',
				itemId:'form',
				callback:function(){me.allover();}
			}
		}];
		me.callParent(arguments);
	},
	initListeners:function(){
		var me = this;
		var list = me.getComponent('list');
		list.on({
			addClick:function(){
				var par = {
					autoScroll:true,
		    		modal:true,//模态
		    		floating:true,//漂浮
					closable:true,//有关闭按钮
					resizable:true,//可变大小
					draggable:true//可移动
				};
				var callback = function(info){
					var p = me.getPanel(info,par);
					p.show();
				};
				getAppObject('4764298649770970364',callback);
			}
		});
	}
});