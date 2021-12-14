Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	//大小按钮图标
	var small = {
		xtype:'button',
		scale:'small',
		//text:'小图标',
		iconAlign:'top',
		icon:'../../css/images/main/search-16.png',
		tooltip:'小图标'
	};
	var medium = {
		xtype:'button',
		scale:'medium',
		//text:'中图标',
		iconAlign:'top',
		icon:'../../css/images/main/search-24.png',
		tooltip:'中图标'
	};
	var large = {
		xtype:'button',
		scale:'large',
		//text:'大图标',
		iconAlign:'top',
		icon:'../../css/images/main/search-32.png',
		tooltip:'大图标'
	};
	var but48 = {
		width:48,height:48,
		xtype:'button',
		scale:'large',
		text:'<B>大图标</B>',
		//iconAlign:'top',
		//icon:'../../css/images/main/exit-48.png',
		//url:'http://www.baidu.com',
		cls:'exit-img-48',
		border:false,
		tooltip:"<b>名称：<font color='blue'>大图标</font><br>说明：</b>这是一个大图标的说明"
		
	};
	
	var image = {
		xtype:'image',
		src:getLogoRootPath() + "/logo.png",
		frame:true,
		border:true
	};
	
	var myImg = Ext.create('MyImg',{
		xtype:'myimage',
		src:'../image/close.png',
		text:'测试文字',
		tooltip:'ASASASAS'
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		items:[small,medium,large,but48,image,myImg]
	});
});