Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var store = Ext.create('Ext.data.Store',{
		fields:[],
		data:[]
	});
	
	var data = [
		{L1:'1',L2:'2014-01-01',L3:'1'},
		{L1:'1',L2:'2014-01-01',L3:'1'},
		{L1:'2',L2:'2014-01-02',L3:'1'},
		{L1:'2',L2:'2014-01-02',L3:'1'},
		{L1:'3',L2:'2014-01-03',L3:'1'},
		{L1:'3',L2:'2014-01-03',L3:'1'}
	];
	
	var changeData = function(num){
		var data = [
			{L1:'1',L2:'2014-01-01',L3:'1',name:''},
			{L1:'1',L2:'2014-01-01',L3:'1'},
			{L1:'2',L2:'2014-01-02',L3:'1'},
			{L1:'2',L2:'2014-01-02',L3:'1'},
			{L1:'3',L2:'2014-01-03',L3:'1'},
			{L1:'3',L2:'2014-01-03',L3:'1'}
		];
	};
	
	var panel = Ext.create('Ext.grid.Panel',{
		title:'测试二维列表',
		columns:[
			{dataIndex:'L1',text:'总批次',width:50},
			{dataIndex:'L2',text:'日期',width:110,xtype :'datecolumn',format:'Y-d-m'},
			{dataIndex:'L3',text:'日序号',width:50}
		],
		store:store,
		tbar:{
			xtype:'toolbar',
			items:[{
				text:'数据一',handler:function(but){
					changeData(1);
				}
			},{
				text:'数据二',handler:function(but){
					changeData(2);
				}
			},{
				text:'数据三',handler:function(but){
					changeData(3);
				}
			}]
		}
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});