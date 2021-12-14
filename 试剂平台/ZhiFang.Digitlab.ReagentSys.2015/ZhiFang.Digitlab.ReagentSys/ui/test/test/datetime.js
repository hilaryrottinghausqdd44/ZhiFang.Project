Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var store = Ext.create('Ext.data.Store', {
//	    fields:[
//	        { name: 'symbol', type: 'string' },
//	        { name: 'date',   type: 'date' },
//	        { name: 'change', type: 'number' },
//	        { name: 'volume', type: 'number' },
//	        { name: 'topday', type: 'date' }                        
//	    ],
		fields:['symbol','date','change','volume','topday','x'],
	    data:[
	        { symbol: "msft",   date: '2011-04-22', change: 2.43, volume: 61606325, topday: '04/01/2010',x:'123.0000' },
	        { symbol: "goog",   date: '2011/04/25', change: 0.81, volume: 3053782,  topday: '04/11/2010' ,x:'124.0000'},
	        { symbol: "apple",  date: '2013/05/05 11:35:30', change: 1.35, volume: 24484858, topday: '04/28/2010' ,x:'125.0000'},            
	        { symbol: "sencha", date: '2011/04/22', change: 8.85, volume: 5556351,  topday: '04/22/2010' ,x:'126.0000'}            
	    ]
	});
	
	var panel = Ext.create('Ext.grid.Panel', {
	    title: 'Date Column Demo',
	    store: store,
	    columns: [
	        { text: 'Symbol',   dataIndex: 'symbol', flex: 1 },
	        { text: '日期',     dataIndex: 'date',   xtype: 'datecolumn',   format:'Y-m-d'},
	        { text: '时间',     dataIndex: 'date',   xtype: 'datecolumn',   format:'H:i:s'},
	        { text: 'Change',   dataIndex: 'change', xtype: 'numbercolumn', format:'0.00' },
	        { text: 'Volume',   dataIndex: 'volume', xtype: 'numbercolumn', format:'0,000' },
	        { text: 'Top Day',  dataIndex: 'topday', xtype: 'datecolumn',   format:'l' }  ,
	        {text:'测试',xtype:'numbercolumn',dataIndex:'x',format:'0.00',sortable:false,hideable:true,hidden:false,editor:false,align:'left'}
	    ],
	    height: 200,
	    width: 600,
	    renderTo: Ext.getBody()
	});
	
	var date = Ext.create('Ext.form.field.Date',{
		value:'2013/05/05',
		format:'Y/m/d'
	});
	var time = Ext.create('Ext.form.field.Time',{
		value:'11:35:30',
		format:'H:i:s'
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		//layout:'fit',
		items:[panel,date,time]
	});
});