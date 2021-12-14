/**
 * 首页Table布局
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.MainTable',{
    extend:'Ext.panel.Panel',
    title:'首页',
    autoScroll:true,
    layout:{
        type:'table',
        columns:3,
        tableAttrs: {
            style: {
                width: '100%'
            }
        }
    },
    
    afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		setTimeout(function(){
			me.initAllItems();
		},2000);
	},
	initComponent:function(){
		var me = this;
		//me.items = me.createItems();
		
		me.html = 
		'<div id="' + me.getId() + '_loading_div" style="margin:100px 10px;text-align:center;font-weight:bold;">' +
			'<img style="margin:10px;" src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
			'</br><span>页面加载中</span>' +
		'</div>';
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(me.createWeinXin({
			colspan:3
		}));
		items.push(me.createWeinXin({
			colspan:2
		}));
		items.push(me.createWeinXin({
			colspan:1
		}));
		items.push(me.createWeinXin({
			colspan:1
		}));
		items.push(me.createWeinXin({
			colspan:1
		}));
		items.push(me.createWeinXin({
			colspan:1
		}));
		
		return items;
	},
	createWeinXin:function(config){
		var me = this;
		var item = me.createCard({
			title:'微信公众号',
			//width:160,
			colspan:1,
			bodyPadding:5,
			html:
			'<div style="text-align:center;font-weight:bold;">' +
				'<span>请扫描下方二维码</span></br>' +
				'<img style="width:128px;height:128px;" src="' + 
					JShell.System.Path.UI + '/css/images/system/oa_barcord.jpg">' +
			'</div>'
		});
		
		item = Ext.apply(item,config);
		
		return item;
	},
	/**创建小卡片*/
	createCard:function(config){
		var me = this;
		var item = {
			//width:'100%',
			height:200,
			collapsible:true,
			colspan:3,
			margin:5
		};
		
		if(config){
			item = Ext.apply(item,config);
		}
		
		return item;
	},
	
	initAllItems:function(){
		var me = this,
			classData = me.getClassData(),
			items = [];
			
			
		for(var i in classData){
			items.push(me.createItemByClassName(classData[i]));
		}
		//删除加载元素
		var loading = Ext.getDom(me.getId() + '_loading_div');
		if(loading){
			loading.remove();
		}
		
		me.removeAll();
		me.add(items);
	},
	getClassData:function(){
		var me = this,
			items = [];
			
		items.push({
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{}
		},{
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{}
		},{
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{}
		});
		
		return items;
	},
	createItemByClassName:function(data){
		var me = this,
			config = data.classConfig || {};
			
		config = me.createCard(config);
		
		return Ext.create(data.className,config);
	}
});
	