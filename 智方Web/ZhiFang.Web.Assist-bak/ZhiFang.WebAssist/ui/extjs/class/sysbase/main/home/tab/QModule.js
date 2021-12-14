/**
 * 功能面板，常用的功能
 * @author Jcall
 * @version 2018-03-22
 */
Ext.define('Shell.class.sysbase.main.home.tab.QModule',{
    extend:'Ext.panel.Panel',
    
    bodyPadding:0,
    //自动滚轮
    autoScroll:true,
    //表格布局
    layout:'absolute',
//	layout: {
//		type:'table',
//		columns:4
//	},
	columns:5,
    //卡片公共信息
    cardInfo:{
    	xtype:'panel',
    	width:120,
    	height:120,
    	mMargin:10,
    	bodyStyle:'text-align:center;background-color:#89cff0;border:1px solid white;',
    	border:true,
    	header:false
    },
    //开始移动的边框样式
    startBorderStyle:'2px dashed red',
    //结束移动的边框样式
    endBorderStyle:'1px solid white',
    //卡片位置Map
    CardPositionMap:{},
    
    //图片信息
    imgInfo:{
    	width:64,
    	height:64,
    	margin:'15px 0 5px 0'
    },
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	me.onLoadData();
    },
    
    initComponent:function(){
    	var me = this;
    	
    	//卡片可拖动
    	//me.cardInfo.draggable = true;
    	me.cardInfo.draggable = {
    		//拖动开始
    		startDrag:function(e){
    			me.onCardDragStart(this.panel,e);
    		},
    		//拖动结束
    		endDrag:function(e){
				this.panelProxy.hide();
				this.panel.saveState();
    			me.onCardDragEnd(this.panel,e);
    		},
    		//拖动中
    		onDrag:function(e){
    			me.onCardDrag(this.panel,e);
    		}
    	};
    	
    	me.callParent(arguments);
    },
    
    //加载模块数据
    onLoadData:function(){
    	var me = this;
    	var url = JShell.System.Path.UI + '/config/Home_' + JShell.System.CODE + '.json?t=' + new Date().getTime();
		JShell.Server.get(url,function(data){
			if(data.success){
				//初始化内容
				me.initContent(data.value.list || []);
			}
		});
    },
    //初始化内容
    initContent:function(list){
    	var me = this;
    	var items = me.createCards(list);
    	me.removeAll();
    	me.add(items);
    	me.onChangeCardPosition();
    },
    //创建内部卡片
    createCards:function(list){
    	var me = this,
    		items = [],
    		len = list.length;
    	
    	for(var i=0;i<len;i++){
    		var src = JShell.System.Path.MODULE_ICON_ROOT_64 + '/' + list[i].icon;
    		var html = 
    			'<div style="text-align:center;color:#ffffff;">' + 
    				'<img style="' + 
    					'width:' + me.imgInfo.width + 'px;' + 
    					'height:' + me.imgInfo.height + 'px;' +
    					'margin:' + me.imgInfo.margin + 
    					';" src="' + src +
    				'"></img></br>' +
    				'<span>' + '【' + i + '】' +list[i].text + '</span>' +
    			'</div>';
    		
    		var classInfo = Ext.clone(list[i]);
    		if(classInfo.icon) classInfo.icon = JShell.System.Path.MODULE_ICON_ROOT_16 + '/' + classInfo.icon;
    		var x = i % me.columns * me.cardInfo.width + (me.bodyPadding + me.cardInfo.mMargin) * (i % me.columns + 1) + 1;
    		var y = parseInt(i / me.columns) * me.cardInfo.height + (me.bodyPadding + me.cardInfo.mMargin) * parseInt(i / me.columns + 1) + 1;
    		
    		var item = Ext.applyIf({
    			html:html,
    			cardInfo:list[i],
    			classInfo:classInfo,
    			x:x,
    			y:y,
    			listeners:{
    				render:function(){
    					var p = this;
    					this.getEl().on('dblclick',function(a,b,c,d){
    						me.onCardClick(p);
    					});
    				}
    			}
    		},me.cardInfo);
    		
    		items.push(item);
    	}
    	
    	return items;
    },
    //卡片点击处理
    onCardClick:function(p){
    	var me = this,
    		SystemViewport = Ext.getCmp("SystemViewport"),
    		view = SystemViewport ? SystemViewport.getComponent("view") : null,
    		ContentTab = view ? view.getComponent("ContentTab") : null;
    		
    	if(ContentTab){
    		ContentTab.insertTab(p.classInfo);
    	}else{
    		JShell.Msg.error("请登录系统！");
    	}
    },
    //卡片拖动开始处理
    onCardDragStart:function(p,e){
    	var me = this,
    		items = me.items.items,
    		len = items.length;
    		
    	me.CardPositionMap = {};
    	for(var i=0;i<len;i++){
    		me.CardPositionMap[items[i].cardInfo.tid] = items[i].getPosition();
    	}
    	
    	me.onChangeCardStyle(true);
    	console.log("【拖动开始】：" + p.classInfo.text);
    	me.onChangeCardPosition();
    	
    	var me = this,
    		items = me.items.items,
    		len = items.length,
    		list = [];
    	
    	var info = [];
    	for(var i=0;i<len;i++){
    		//list.push(items[len-1-i].cardInfo);
    		info.push(items[i].getPosition());
    	}
    	console.log("【拖动中】：=【" + info.join("】【") +  "】");
    	//me.initContent(list);
    },
    //卡片开始结束处理
    onCardDragEnd:function(p,e){
    	var me = this;
    	me.onChangeCardStyle(false);
    	console.log("【拖动结束】：" + p.classInfo.text);
    	console.log("【拖动中】：鼠标xy=【" + e.getXY() +  "】" + 
    		"【拖动中】：面板左上角xy=【" + p.getPosition() +  "】");
    },
    //卡片拖动中处理
    onCardDrag:function(p,e){
    	var me = this;
//  	console.log("【拖动中】：鼠标xy=【" + e.getXY() +  "】" + 
//  		"【拖动中】：面板左上角xy=【" + p.getPosition() +  "】【" + p.getPosition(true) +  "】");
    },
    //修改卡片样式
    onChangeCardStyle:function(isStart){
    	var me = this,
    		items = me.items.items,
    		len = items.length;
    		
    	var borderStyle = isStart ? me.startBorderStyle : me.endBorderStyle;
    	for(var i=0;i<len;i++){
    		//console.log("兄弟节点：" + items[i].classInfo.text);
    		items[i].setBodyStyle('border',borderStyle);
    	}
    },
    //修改卡片位置
    onChangeCardPosition:function(){
    	var me = this,
    		items = me.items.items,
    		len = items.length,
    		list = [];
    	
    	var info = [];
    	var info2 = [];
    	for(var i=0;i<len;i++){
    		//list.push(items[len-1-i].cardInfo);
    		info.push(items[i].getPosition());
    		var x = i % me.columns * me.cardInfo.width + (me.bodyPadding + me.cardInfo.mMargin) * (i % me.columns + 1) + 1;
    		var y = parseInt(i / me.columns) * me.cardInfo.height + (me.bodyPadding + me.cardInfo.mMargin) * parseInt(i / me.columns + 1) + 1;
    		info2.push([x,y]);
    	}
    	console.log("【拖动中】：=【" + info.join("】【") +  "】");
    	console.log("【拖动中-计算结果】：=【" + info2.join("】【") +  "】");
    	//me.initContent(list);
    	
    	
    	
    }
});
	