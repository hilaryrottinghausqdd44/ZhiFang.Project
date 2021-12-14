Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.zhifangux', '../../zhifangux');
	
	var date = Ext.create('Ext.zhifangux.DateTime',{
		x:10,y:10,
		setType:'date',
		//value:'2013/05/05',
		format:'Y/m/d'
	});
	var time = Ext.create('Ext.zhifangux.DateTime',{
		x:10,y:40,
		setType:'time',
		//value:'11:35:30',
		format:'H:i:s'
	});
	
	var but1 = {
		x:10,y:70,
		xtype:'button',
		text:'日期赋值',
		handler:function(){
			date.setValue("2013-05-05");
		}
	};
	
	var but2 = {
		x:10,y:100,
		xtype:'button',
		text:'时间赋值',
		handler:function(){
			time.setValue("11:35:30");
		}
	};
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'absolute',
		items:[date,time,but1,but2]
		//items:date
	});
});