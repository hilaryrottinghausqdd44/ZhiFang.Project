Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var list = [{
		text:'警报1',
		src:'../危险警报铃声/Alarm(苹果铃声).mp3'
	},{
		text:'警报2',
		src:'../危险警报铃声/警报_7344.mp3'
	},{
		text:'警报3',
		src:'../危险警报铃声/警报_ALARMED.mp3'
	},{
		text:'警报4',
		src:'../危险警报铃声/警报_alarms.mp3'
	}];
	
	var start = function(src){
		var div = document.getElementById('div');
		var audio = '<audio src="' + src + '" autoplay=true/>';
		div.innerHTML = audio;
	};
	
	var items = [];
	for(var i in list){
		items.push({
			xtype:'button',
			text:list[i].text,
			src:list[i].src,
			handler:function(but){
				start(but.src);
			}
		});
	}
	
	var panel = {
		xtype:'panel',
		title:'音频',
		items:items
	};
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});