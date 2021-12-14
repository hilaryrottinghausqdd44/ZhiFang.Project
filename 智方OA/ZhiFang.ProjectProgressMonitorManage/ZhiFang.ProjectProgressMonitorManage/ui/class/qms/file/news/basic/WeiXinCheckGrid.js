/*
 * 新闻发布的微信内容选择
 * @author Jcall
 * @version 2017-01-10
 */
Ext.define('Shell.class.qms.file.news.basic.WeiXinCheckGrid', {
	extend:'Ext.panel.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.toolbar.Paging'
	],
	
	layout:'border',
	bodyPadding:1,
	
	ListLayout:{
		type:'table',
		columns:2,
	},
	cardDefault:{
		width:240,
		height:180,
		margin:5,
		padding:5
	},
	
	/**默认加载数据*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:true,
	/**默认每页数量*/
	defaultPageSize:100,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[1,1],[2,2],[10,10],[20,20],[50,50],[100,100],
		[200,200]
	],
	/**获取数据服务地址*/
	selectUrl:'/WeiXinAppService.svc/GetPermanentMediaNewsList',
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**消息信息样式*/
	msgFormat: '<div style="color:blue;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**错误信息*/
	errorInfo: null,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	
	/**微信新闻数据*/
	WEIXIN_DATA:[],
	/**选中的新闻卡片*/
	CheckedNewsCard:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.defaultLoad){
			setTimeout(function(){
				me.onSearch();
			},500);
		}
	},
	initComponent:function(){
		var me = this;
		
		me.width = me.ListLayout.columns * (me.cardDefault.width + me.cardDefault.margin) + 400 + 40;
		me.height = me.width * 0.75;
		
		//延时处理组件
		me.DelayedTask = new Ext.util.DelayedTask();
		
		me.store = me.createStore();
		me.dockedItems = me.createDockedItems();
		
		me.items = [{
			region:'center',
			itemId:'list',
			header:false,
			autoScroll:true,
			bodyPadding:'0 10 0 0',
			layout:me.ListLayout
		},{
			region:'east',
			itemId:'content',
			header:false,
			autoScroll:true,
			bodyPadding:5,
			width:400,
			split:true,
			collapsible:true
		}];
		
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'top',
			itemId:'topToolbar',
			items:['refresh','->','accept']
//			items:['refresh',{
//				type:'search',
//				info:{
//					width:200,emptyText:'新闻名称',isLike:true,itemId:'search'
//				}
//			},'->','accept']
		},{
			xtype:'uxPagingtoolbar',
			dock:'bottom',
			itemId:'pagingToolbar',
			store:me.store,
			defaultPageSize:me.defaultPageSize,//默认每页数量
			pageSizeList:me.pageSizeList//分页栏下拉框数据
		}];
		return dockedItems;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: [],
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'total_count',
					root: 'item'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value || {};
						if (info) {
							data.total_count = info.total_count || 0;
							data.item = info.item || [];
						} else {
							data.total_count = 0;
							data.item = [];
						}
						data = me.changeResult(data);
					} else {
						me.errorInfo = data.msg;
					}
					me.WEIXIN_DATA = data.item || [];
					
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		return data;
	},
	onBeforeLoad:function(){
		var me = this;
		//查询条件
		me.store.proxy.url = me.getLoadUrl();
		//清空新闻内容
		me.getComponent('content').update("");
		//显示遮罩
		me.showMask(me.loadingText);
	},
	onAfterLoad:function(records,successful){
		var me = this;
		//初始化内容
		me.initContent(successful);
		//隐藏遮罩
		me.hideMask();
	},
	
	load:function(){
		var me = this;
		me.store.currentPage = 1;
		me.store.load();
	},
	onSearch:function(){
		var me = this;
		me.load();
	},
	onRefreshClick: function() {
		this.onSearch();
	},
	onSearchClick:function(field,value){
		var me = this;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;

		var url = JShell.System.Path.ROOT + me.selectUrl;

		return url;
	},
	
	/**初始化内容*/
	initContent:function(successful){
		var me = this,
			list = me.WEIXIN_DATA,
			len = list.length,
			items = [];
			
		for(var i=0;i<len;i++){
			items.push(me.createCard(list[i]));
		}
		
		me.getComponent('list').removeAll();
		me.getComponent('list').add(items);
	},
	/**创建卡片*/
	createCard:function(data){
		var me = this,
			item = data.content.news_item[0] || {},
			media_id = data.media_id,
			title = item.title.length > 15 ? (item.title.slice(0,13) + '...') : item.title,
			digest = item.digest.length > 30 ? (item.digest.slice(0,28) + '...') : item.digest,
			imageSrc = JShell.System.Path.ROOT + '/' + item.thumb_media_Url.replace(/\\/g,'/');
		
		var card = {
			width:me.cardDefault.width,
			height:me.cardDefault.height,
			margin:me.cardDefault.margin,
			bodyPadding:me.cardDefault.padding,
			weixinInfo:{
				media_id:media_id
			},
			layout:'anchor',
		    defaults:{
		    	anchor:'100%'
		    },
			items:[{
				xtype:'label',
				style:'width:100%;color:#04408c;font-weight:bold;font-size:12px;text-align:center;',
				text:title
			},{
				xtype:'image',
				//width:'100%',
				height:me.cardDefault.height - 70,
				src:imageSrc
			},{
				xtype:'label',
				style:'width:100%;',
				text:digest
			}],
			listeners:{
				scope:this,
				click:{
					element:'el', 
					fn:function(){
						me.DelayedTask.delay(200, function(){
							me.onCardClick(media_id);
						}, me, ['click']);
					}
				},
				dblclick:{
					element:'el', 
					fn:function(){
						me.DelayedTask.delay(200, function(){
							me.onCardDoublueClick(media_id);
						}, me, ['double']);
					}
				}
			}
		};
		
		return card;
	},
	/**单击卡片处理*/
	onCardClick:function(media_id){
		var me = this;
		//选中卡片
		me.onCheckCrad(media_id);
		//显示新闻内容
		me.onShowInfo(media_id);
	},
	/**双击卡片处理*/
	onCardDoublueClick:function(media_id){
		var me = this;
		//选中卡片
		me.onCheckCrad(media_id);
		//显示新闻内容
		me.onShowInfo(media_id);
	},
	/**选中卡片*/
	onCheckCrad:function(media_id){
		var me = this,
			card = me.getNewsCardByMediaId(media_id);
			
		if(me.CheckedNewsCard){
			//去掉边框样式
			me.CheckedNewsCard.setBorder(1);
		}
		me.CheckedNewsCard = card;
		me.CheckedNewsCard.setBorder(5);
	},
	/**显示新闻内容*/
	onShowInfo:function(media_id){
		var me = this,
			data = me.getNewsInfoByMediaId(media_id),
			content = data.content || {},
			list = content.news_item || [],
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			html.push(list[i].content);
		}
			
		me.getComponent('content').update(html.join(""));
	},
	
	/**确认选中新闻*/
	onAcceptClick:function(){
		var me = this,
			media_id = me.CheckedNewsCard.weixinInfo.media_id,
			data = me.getNewsInfoByMediaId(media_id);
			
		me.fireEvent('accept',this,data);
	},
	
	/**根据ID获取新闻卡片*/
	getNewsCardByMediaId:function(media_id){
		var me = this,
			items = me.getComponent('list').items.items,
			panel = null;
			
		for(var i in items){
			if(items[i].weixinInfo.media_id == media_id){
				panel = items[i];
				break;
			}
		}
		
		return panel;
	},
	/**根据ID获取新闻信息*/
	getNewsInfoByMediaId:function(media_id){
		var me = this,
			items = me.WEIXIN_DATA,
			data = null;
			
		for(var i in items){
			if(items[i].media_id == media_id){
				data = items[i];
				break;
			}
		}
		
		return data;
	},
	
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});