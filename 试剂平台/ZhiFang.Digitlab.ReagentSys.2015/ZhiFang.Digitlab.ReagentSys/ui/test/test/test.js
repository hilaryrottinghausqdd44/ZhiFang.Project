Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var getToServerJsonP = function(url,callback,async){
    	var arr = url.split("?");
    	if(arr.length > 1){
    		var b = arr[1].split("&");
    		var bo = false;
    		var callbackText = "callback=Ext.data.JsonP.jsonPCallback";
    		for(var i in b){
    			var c = b[i].split("=");
    			if(c[0] == 'callback'){
    				b[i] = callbackText;
    				bo = true;
    			}
    		}
    		if(!bo){
    			b.push(callbackText);
    		}
    		arr[1] = b.join("&");
    	}
    	var u = arr.join("?");
    	
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	var bo = async === false ? false :true;
		Ext.data.JsonP.request({
			url:u,
	        async:bo,
	        method:'GET',
			timeout: 300000,
			callbackKey:'callback',
			callbackName: "jsonPCallback",
			success:function(result){
	            if(Ext.typeOf(callback) == "function"){
	                callback(result);//回调函数
	            }
	        },
	        failure:function(result){
	        	if(Ext.typeOf(callback) == "function"){
	        		var value = "{success:false,ErrorInfo:'JsonP请求服务失败!',ResultDataValue:''}";
	                callback(value);//回调函数
	            }
	        }
		});
	};
	
	
	var panel = {
		bodyPadding:10,
		dockedItems:[{
			xtype:'toolbar',
			items:[{
				xtype:'textfield',
				itemId:'url',
				width:700,
				fieldLabel:'地址',
				labelWidth:40,
				labelAlign:'right'
			},{
				xtype:'button',
				text:'获取数据',
				handler:function(but){
					var p = but.ownerCt.ownerCt;
					var url = but.ownerCt.getComponent('url').getValue();
					var callback = function(text){
						var info = p.getComponent('info');
						var type = Ext.typeOf(info);
						if(type == 'string'){
							info.setValue(text);
						}else if(type == 'object' || type == 'array'){
							info.setValue(Ext.JSON.encode(text));
						}
					};
					getToServerJsonP(url,callback);
					
					store.load({address:123,casenum:{a:'asd'}});
				}
			}]
		}],
		items:[{
			xtype:'textarea',//readOnly:true,
			itemId:'info',
			width:800
		},{
			xtype:'htmleditor',
			width:800,
			height:300,
			//readOnly:true,
			listeners:{
				specialkey:function(editor,e){
				  	if (e.keyCode == Ext.EventObject.BACKSPACE) {
				  		alert("BACKSPACE");
				  	}
				  	if (e.keyCode == Ext.EventObject.ENTER) {
				  		alert("ENTER");
				  	}
				}
			}
		}],
		listeners:{
//			render:function(input){
//				new Ext.KeyMap(input.getEl(),[{
//			      	key:Ext.EventObject.BACKSPACE,
//			      	fn:function(){
//			       		alert("BACKSPACE111");
//			      	}
//		     	}]);
//		     	new Ext.KeyMap(input.getEl(),[{
//			      	key:Ext.EventObject.ENTER,
//			      	fn:function(){
//			       		alert("ENTER111");
//			      	}
//		     	}]);
//			}
			 specialkey:function(editor,e){
			  	if (e.keyCode == Ext.EventObject.BACKSPACE) {
			  		alert("BACKSPACE111");
			  	}
			  	if (e.keyCode == Ext.EventObject.ENTER) {
			  		alert("ENTER111");
			  	}
			}
		}
	};
	
	var params = {AA:123,BB:'ASD',CC:[{A:1},{A:2}]};
	var store = Ext.create('Ext.data.Store',{
		fields:['a'],
		proxy:{
			type:'ajax',
			url:'aaaa.js',
			actionMethods:{create:"POST",read:"POST",update:"POST",destroy:"POST"}
	        //getMethod: function(){ return 'POST'; }//亮点，设置请求方式,默认为GET
		},
		autoLoad:true,
		listeners: { 
			'beforeload':function (store, op, options) { 
//				var params = {address:123,casenum:{a:'asd'}}; 
//				Ext.apply(store.proxy.extraParams, params); 
			} 
		}
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});