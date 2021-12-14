Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	//获取随机字符串
	var getRandomStr = function(len){
        var x = "123456789poiuytrewqasdfghjklmnbvcxzQWERTYUIPLKJHGFDSAZXCVBNM";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += x.charAt(Math.ceil(Math.random() * 100000000) % x.length);
        }
        return str;
    };
    //说去随机数字
    var getRandom = function(min, max){
        return parseInt(Math.random() * (max - min) + min);
    };
	//获取随机的列表数据
	var getListData = function(){
		var data = [];
		var count = 100;//列表数据量
		for(var i=0;i<count;i++){
			data.push({
				name:getRandomStr(8),
				code:getRandomStr(12),
				type:getRandom(1,10)
			});
		}
		return data;
	};
	//获取随机31天的数据
	var getMonthData = function(num){
		var count = num || 0;
		var list = [];
		for(var i=0;i<count;i++){
			list.push({
				name:i+1+'',
				data1:getRandom(1,100),
				data2:getRandom(1,100),
				data3:getRandom(1,100),
				data4:getRandom(1,100),
				data5:getRandom(1,100)
			});
		}
		return list;
	};
	
	//列表
	var listStore = Ext.create('Ext.data.Store',{
		fields:['name','code','type'],
		data:getListData()
	});
	var list = {
		region:'west',xtype:'grid',width:260,header:false,
		collapsible:true,split:true,store:listStore,
		tbar:[{xtype:'datefield',width:180,labelWidth:60,fieldLabel:'检索时间',format:'Y年m月d日',labelAlign:'right'}],
		columns:[
			{text:'项目名称',dataIndex:'name',width:80},
			{text:'项目编码',dataIndex:'code',width:100},
			{text:'项目类型',dataIndex:'type',width:60}
		]
	};
	//饼图、柱状图(一月份结果)[高、偏高、正常、偏低、低]
	var getChart1Data = function(){
		var data = [
    		{name:'高',data:getRandom(1,20)},
    		{name:'偏高',data:getRandom(1,20)},
    		{name:'正常',data:getRandom(1,20)},
    		{name:'偏低',data:getRandom(1,20)},
    		{name:'低',data:getRandom(1,20)}
    	];
    	return data;
	};
	var chartStore = Ext.create('Ext.data.Store',{
		fields:['name','data'],
		data:getChart1Data()
	});
	var top = {
		region:'north',xtype:'panel',height:200,
		collapsible:true,split:true,
		title:'一月份结果',layout:'hbox',header:false,
		items:[{
	        xtype:'chart',animate:true,store:chartStore,shadow:true,id:'chartCmp',
            legend:{position:'right'},insetPadding:5,theme:'Base:gradients',
            width:300,height:190,
            series:[{
                type:'pie',field:'data',showInLegend:true,donut:false,
                tips:{
                  	trackMouse:true,width:140,height:28,
                  	renderer:function(storeItem,item) {
                    	var total = 0;
                    	chartStore.each(function(rec){total += rec.get('data');});
                    	this.setTitle(storeItem.get('name') + ': ' + Math.round(storeItem.get('data') / total * 100) + '%');
                  	}
                },
                highlight:{segment:{margin:20}},
                label:{field:'name',display:'rotate',contrast:true,font:'12px Arial'}
            }]
		},{
            xtype:'chart',style:'background:#fff',
            animate:true,shadow:true,store:chartStore,
            width:400,height:190,margin:'8 8 8 38',
            axes:[{
                type:'Numeric',position:'left',fields:['data'],
                label:{renderer:Ext.util.Format.numberRenderer('0,0')},
                grid:true,minimum:0
            },{
                type:'Category',position:'bottom',fields:['name']
            }],
            series:[{
                type:'column',axis:'left',highlight:true,
                tips:{
                  	trackMouse:true,width:140,height:28,
                  	renderer:function(storeItem, item){this.setTitle(storeItem.get('name') + ': ' + storeItem.get('data'));}
                },
                label:{
                  	display:'insideEnd','text-anchor':'middle',field:'data',
                    renderer:Ext.util.Format.numberRenderer('0'),
                    orientation:'vertical',color:'#333'
                },
                xField:'name',
                yField:'data'
            }]
		}]
	};
	//曲线图
	var chartStore2 = Ext.create('Ext.data.Store',{
		fields:['name','data1','data2','data3','data4','data5'],
		data:getMonthData(31)
	});
	var infoStore = Ext.create('Ext.data.Store',{
    	fields:['name','data'],
    	data:[
    		{name:'data1',data:10},
    		{name:'data2',data:10},
    		{name:'data3',data:10},
    		{name:'data4',data:10},
    		{name:'data5',data:10}
    	]
    });
	var center = {//曲线图(一月份结果)[31天]
		region:'center',title:'一月份结果',xtype:'panel',layout:'fit',header:false,
		items:[{
			xtype:'chart',animate:true,shadow:true,store:chartStore2,
            axes:[
            	{type:'Numeric',position:'left',fields:['data1'],title:false,grid:true},
            	{type:'Category',position:'bottom',fields:['name'],title:false}
            ],
            series:[{
                type:'line',axis:'left',gutter:80,xField:'name',yField:['data1'],
                highlight:{size:7,radius:7},
                markerConfig:{type:'circle',size:4,radius:4,'stroke-width':0},
                tips:{
                    trackMouse:true,width:580,height:170,layout:'fit',style:{background:'#fff'},
                    items:{xtype:'container',layout:'hbox',items:[{
                    	xtype:'chart',width:100,height:100,margin:5,store:infoStore,
				        animate:false,shadow:false,insetPadding:0,theme:'Base:gradients',
				        series:[{
				            type:'pie',field:'data',showInLegend:false,
				            label:{field:'name',display:'rotate',contrast:true,font:'9px Arial'}
				        }]
                    },{
                    	xtype:'grid',height:130,width:200,margin:5,border:false,store:infoStore,
				        columns:[
				            {text:'name',dataIndex:'name',width:90},
				            {text:'data',dataIndex:'data',width:90}
				        ]
                    }]},
                    renderer:function(klass,item){
                        var storeItem = item.storeItem;
                        var data = [
                        	{name:'data1',data:storeItem.get('data1')}, 
                        	{name:'data2',data:storeItem.get('data2')}, 
                        	{name:'data3',data:storeItem.get('data3')}, 
                        	{name:'data4',data:storeItem.get('data4')}, 
                        	{name:'data5',data:storeItem.get('data5')}
                        ];
                        this.setTitle("1月" + storeItem.get('name') + "日信息-1");
                        infoStore.loadData(data);
                    }
                }
            },{
                type:'line',axis:'left',gutter:80,xField:'name',yField:['data2'],
                highlight:{size:7,radius:7},
                markerConfig:{type:'circle',size:4,radius:4,'stroke-width':0},
                tips:{
                    trackMouse:true,width:580,height:170,layout:'fit',style:{background:'#fff'},
                    items:{xtype:'container',layout:'hbox',items:[{
                    	xtype:'chart',width:100,height:100,margin:5,store:infoStore,
				        animate:false,shadow:false,insetPadding:0,theme:'Base:gradients',
				        series:[{
				            type:'pie',field:'data',showInLegend:false,
				            label:{field:'name',display:'rotate',contrast:true,font:'9px Arial'}
				        }]
                    },{
                    	xtype:'grid',height:130,width:200,margin:5,border:false,store:infoStore,
				        columns:[
				            {text:'name',dataIndex:'name',width:90},
				            {text:'data',dataIndex:'data',width:90}
				        ]
                    }]},
                    renderer:function(klass,item){
                        var storeItem = item.storeItem;
                        var data = [
                        	{name:'data1',data:storeItem.get('data1')}, 
                        	{name:'data2',data:storeItem.get('data2')}, 
                        	{name:'data3',data:storeItem.get('data3')}, 
                        	{name:'data4',data:storeItem.get('data4')}, 
                        	{name:'data5',data:storeItem.get('data5')}
                        ];
                        this.setTitle("1月" + storeItem.get('name') + "日信息-2");
                        infoStore.loadData(data);
                    }
                }
            }]
		}]
	};
	//整体面板
	var panel = {
		title:'列表联动图表',
		bodyPadding:2,
		layout:{type:'border',regionWeights:{west:2,north:1}},
		items:[list,top,center]
	};
	//监听
	list.listeners = {
		select:function(com,record,index,eOpts){
			chartStore.loadData(getChart1Data());
			chartStore2.loadData(getMonthData(31));
		}
	};
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
});