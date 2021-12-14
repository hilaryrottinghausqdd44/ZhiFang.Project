Ext.define('myPanel',{
	extend:'Ext.panel.Panel',
	width:835,
	height:349,
	title:'部门维护',
	layout:'border',
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//创建真实的内部应用
		me.createItems();
	},
	createItems:function(){
		var me = this;
		var center = function(info){
			var par = {
				width:478,height:322,
				region:'center'
			};
			var p = me.getPanel(info,par);
			me.add(p);
		};
		var east = function(info){
			var par = {
				collapsible:true,split:true,
				width:350,height:322,
				region:'east'
			};
			var p = me.getPanel(info,par);
			me.add(p);
		}
		
		var id = "4935525169831333190";
		getAppObject(id,center);
		var id = "4764298649770970364";
		getAppObject(id,east);
	},
	getPanel:function(info,par){
		var me = this;
		var p = null;
		
		par.autoScroll = true;
		
		if(info.success){
			p = Ext.create(info.data,par);
		}else{
			par.html = info.ErrorInfo;
			p = Ext.create('Ext.panel.Panel',par);
		}
		return p;
	}
});