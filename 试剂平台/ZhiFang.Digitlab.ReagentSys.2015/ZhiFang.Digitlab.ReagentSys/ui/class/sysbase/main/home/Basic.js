/**
 * 首页Home基础面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.home.Basic',{
    extend:'Ext.panel.Panel',
    title:'首页',
    /**自动滚轮*/
    autoScroll:true,
    /**小卡片默认属性*/
    CardConfig:{
    	layout:'fit',
    	height:200,
		collapsible:true,
		collapsed:true,
		margin:5
    },
    /**面板列表*/
    PANEL_ITEMS:[],
    afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		//初始化组件类数据
		me.initClassListData(function(){
			//初始化组件
			me.initAllItems();
			
			me.onShowAllItems();
		});
	},
	initComponent:function(){
		var me = this;
		me.html = me.getLoadingHtml();
		me.callParent(arguments);
	},
	/**@overwrite 初始化组件类数据*/
	initClassListData:function(callback){
		
	},
	/**初始化所有组件*/
	initAllItems:function(){
		var me = this,
			list = me.CLASS_LIST,
			len = list.length,
			items = [];
			
		me.PANEL_ITEMS = [];
			
		for(var i=0;i<len;i++){
			var panel = me.createPanel(i,list[i]);
			items.push(panel);
		}
		
		//删除加载元素
		me.removeLoadingHtml(me.getId());
		me.removeAll();
		me.add(items);
	},
	/**创建面板*/
	createPanel:function(index,data){
		var me = this,
			config = Ext.clone(this.CardConfig);
			config = Ext.apply(config,data.classConfig);
		
		config.CLASS_LIST_INDEX = index;
		config.classConfig = data.classConfig;
		config.className = data.className;
		if(data.classConfig.height){
			config.height = data.classConfig.height;
		}
		if(data.more){
			config.more = data.more;
			config.tools = [{
				type:'search',
				tooltip: '查看更多',
			    handler: function(event, toolEl, tool){
			    	var more = tool.ownerCt.more;
			    	more.icon = window.VIEWPORT.MODULE_ICON_PATH_16 + '/' + more.icon;
			        window.VIEWPORT.ContentTab.insertTab(more);
			    }
			}];
		}else{
			config.more = null;
			config.tools = [];
		}
		
		config.tools.push({
			type:'plus',
			tooltip: '展开所有的功能卡片',
		    handler: function(event, toolEl, tool){
		    	me.onExpandAllCards(tool);
		    }
		},{
			type:'minus',
			tooltip: '收缩所有的功能卡片',
		    handler: function(event, toolEl, tool){
		    	me.onCollapseAllCards(tool);
		    }
		});
		
		var panel = Ext.create('Ext.panel.Panel',config);
		panel.update(me.getLoadingHtml(panel.getId()));
		
		me.PANEL_ITEMS.push(panel);
		
		return panel;
	},
	/**获取加载HTML*/
	getLoadingHtml:function(id){
		var html = 
		'<div id="' + (id || this.getId()) + '_loading_div" class="loading-div">' +
			'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
			'<div style="padding-top:10px;">页面加载中</div>' +
		'</div>';
		return html;
	},
	/**删除加载HTML*/
	removeLoadingHtml:function(id){
		var loading = Ext.getDom(id + '_loading_div');
		if(loading){
			//loading.remove();
			loading.parentNode.removeChild(loading);
		}
	},
	onShowAllItems:function(){
		var me = this,
			items = me.PANEL_ITEMS,
			len = items.length;
			
		for(var i=0;i<len;i++){
			var item = items[i];
			me.createContent(item,{
				className:item.className,
				classConfig:item.classConfig
			});
		}
	},
	createContent:function(panel,classInfo,times){
		var me = this;
		
		setTimeout(function(){
			me.removeLoadingHtml(panel.getId());
			panel.add(Ext.create(
				classInfo.className,
				Ext.apply(classInfo.classConfig,{
					header:false,
					border:false
				})
			));
		},(times || 1000));
	},
	/**展开所有的卡片*/
	onExpandAllCards:function(tool){
		var items = tool.ownerCt.ownerCt.items.items;
		for(var i in items){
			items[i].expand();
		}
	},
	/**收缩所有的卡片*/
	onCollapseAllCards:function(tool){
		var items = tool.ownerCt.ownerCt.items.items;
		for(var i in items){
			items[i].collapse();
		}
	}
});
