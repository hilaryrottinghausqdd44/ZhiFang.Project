Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	//保存到后台
	var saveToServer = function(obj){
		var url = getRootPath() + "/ConstructionService.svc/CS_UDTO_AddBTDAppComponentsRefList";
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				async:false,//非异步
				url:url,
				method:'POST',
				params:Ext.JSON.encode(obj),
				timeout:2000,
				success:function(response,opts){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){
						Ext.Msg.alert('提示','保存成功！');
					}else{
						Ext.Msg.alert('提示','保存失败！错误信息【<b style="color:red">'+ result.errorInfo +"</b>】");
					}
				},
				failure : function(response,options){ 
					Ext.Msg.alert('提示','保存请求失败！');
				}
			});
	};
	
	//保存方法
	var save = function(){
		var obj = {
			BTDAppComponentsRefList:[
				{RefAppComID:'1',RefAppComIncID:'aaa'},
				{RefAppComID:'2',RefAppComIncID:'bbb'},
				{RefAppComID:'3',RefAppComIncID:'ccc'}
			]
		};
		saveToServer(obj);
	};
	
	
	//保存按钮
	var button = {
		text:'保存',
		handler:function(){
			save();
		}
	};
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[button]
	});
});