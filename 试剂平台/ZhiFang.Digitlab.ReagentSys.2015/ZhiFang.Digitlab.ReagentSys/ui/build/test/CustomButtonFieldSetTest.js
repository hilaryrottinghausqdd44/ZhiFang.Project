Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	
//	var panel = Ext.create('Ext.build.CustomButtonFieldSet',{
//		type:'win'
//	});
	
	var panel = {
		xtype:'button',
		text:'弹出窗口',
		handler:function(but){
			var win = Ext.create('Ext.form.Panel',{
				autoScroll:true,
	    		modal:true,//模态
	    		floating:true,//漂浮
				closable:true,//有关闭按钮
				draggable:true,//可移动
				width:495,
				height:350,
				title:'按钮设置',
				bodyPadding:'0 5 0 5',
				layout:'fit',
				items:Ext.create('Ext.build.CustomButtonFieldSet',{
					type:'win'
				})
	    	});
	    	win.show();
		}
	};
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		//layout:'fit',
		items:panel
	});
});