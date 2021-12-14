Ext.define('Shell.ux.panel.AppPanel',{
	extend:'Shell.ux.panel.Panel',
	
	/**已准备完毕的内部应用数量*/
	isReadyCount:0,
	/**内部应用信息*/
	apps:[],
	/**内部边缘距离*/
	bodyPadding:1,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.header && me.header.on({
			dblclick:function(header,e){
				header.hasHide = !header.hasHide;
				
				var items = header.ownerCt.items.items,
					length = items.length;
				
				for(var i=0;i<length;i++){
					if(items[i].region != 'center'){
						items[i][header.hasHide ? 'hide' : 'show']();
					}
				}
			}
		});
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建内部应用*/
	createItems:function(){
		var me = this,
			apps = me.apps || [],
			length = apps.length,
			items = [];
			
		for(var i=0;i<length;i++){
			items.push(Ext.create(apps[i].className,Ext.applyIf({
				boxIsReady:function(){me.childIsReady();}
			},apps[i])));
		}
		
		me.items = items;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			toolbars = me.toolbars || [],
			length = toolbars.length,
			dockedItems = [];
			
		for(var i=0;i<length;i++){
			dockedItems.push(Ext.apply({
				autoScroll:true,
				dock:'top',
				xtype:'uxbuttonstoolbar',
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					}
				}
			},toolbars[i]));
		}
			
		return dockedItems;
	},
	/**内部应用准备完毕*/
	childIsReady:function(){
		var me = this,
			count = me.apps.length;
		
		me.isReadyCount++;
		
		if(me.isReadyCount == count){
			me.initListeners();
		}
	},
	/**初始化监听*/
	initListeners:function(){}
});