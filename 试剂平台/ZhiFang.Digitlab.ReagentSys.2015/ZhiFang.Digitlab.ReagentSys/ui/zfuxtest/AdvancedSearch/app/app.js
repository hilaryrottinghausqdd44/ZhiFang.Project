//alert("app.js");
Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	Ext.Loader.setPath('Ext.ux', '../../../extjs/ux/');
    Ext.Loader.setPath('Ext.zhifangux', '../../../extjs/zhifangux/');
	Ext.require([
		'Ext.ux.data.PagingMemoryProxy',
		'Ext.ux.CheckColumn',
        'Ext.zhifangux.DateTime'
	]);
	Ext.application({
		name:'ZhiFang',//应用名字
		appFolder:'app',//应用目录
		autoCreateViewport:true,//是否开启默认视图
		launch:function(){//当前页面加载完成后执行的函数
		},
		controllers: ['AdvancedSearchController']//加载控制
	})
});
