Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var str = "2013-11-13 00:25:00";
	
	var panel = {
		xtype:'form',
		title:'日期时间赋值测试',
		bodyPadding:10,
		items:[{
			xtype:'zhifangux_datefield',
			itemId:'date',
			format:'Y年m月d日',
			fieldLabel:'日期',
			labelWidth:40,
			width:160
		},{
			xtype:'zhifangux_timefield',
			itemId:'time',
			fieldLabel:'时间',
			labelWidth:40,
			width:160
		},{
			xtype:'button',
			text:'获取日期格式',
			handler:function(but){
				var date = but.ownerCt.getComponent('date');
				alert(date.format);
			}
		},{
			xtype:'button',
			text:'获取时间格式',
			handler:function(but){
				var time = but.ownerCt.getComponent('time');
				alert(time.format);
			}
		},{
			xtype:'button',
			text:'日期赋值',
			handler:function(but){
				var date = but.ownerCt.getComponent('date');
				date.setValue(str);
			}
		},{
			xtype:'button',
			text:'时间赋值',
			handler:function(but){
				var time = but.ownerCt.getComponent('time');
				time.setValue(str);
			}
		},{
			xtype:'button',
			text:'获取日期数据',
			handler:function(but){
				var date = but.ownerCt.getComponent('date');
				alert(date.getValue());
			}
		},{
			xtype:'button',
			text:'获取时间数据',
			handler:function(but){
				var time = but.ownerCt.getComponent('time');
				alert(time.getValue());
			}
		},{
			xtype:'button',
			text:'列表代码解析',
			handler:function(but){
				var DesignCode = "123456";
				var ClassCode = ResolveList.resolve(DesignCode);
				alert(ClassCode);
			}
		}]
	};
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});