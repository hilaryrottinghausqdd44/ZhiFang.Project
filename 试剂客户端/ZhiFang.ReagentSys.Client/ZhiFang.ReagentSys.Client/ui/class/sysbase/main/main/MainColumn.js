/**
 * 首页Column布局
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.MainColumn',{
    extend:'Ext.panel.Panel',
    title:'首页',
    layout:'column',
    /**列数*/
    columnCount:3,
    /**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		
		for(var i=0;i<me.columnCount;i++){
			if(me['createColumn_' + (i+1)]){
				items.push(me.createColumn({
					items:me['createColumn_' + (i+1)]()
				}));//第I列
			}
		}
		
		return items;
	},
	/**创建列信息*/
	createColumn:function(config){
		var me = this;
		var item = {
			columnWidth:1/me.columnCount,
			border:false,
			padding:5,
			items:[]
		};
		
		if(config){
			item = Ext.apply(item,config);
		}
		
		return item;
	},
	/**创建第一列*/
	createColumn_1:function(){
		var me = this;
		
		var items = [me.createCard({
			title:'微信公众号',
			width:160,
			items:[{
				border:false,
				html:
				'<div style="margin:5px;text-align:center;font-weight:bold;">' +
					'<span>请扫描下方二维码</span></br>' +
					'<img style="width:128px;height:128px;" src="' + 
						JShell.System.Path.UI + '/css/images/system/oa_barcord.jpg">' +
				'</div>'
			}]
		})];
		
		return items;
	},
	/**创建第二列*/
	createColumn_2:function(){
		var me = this;
		var items = [me.createCard({
			title:'微信公众号',
			height:200,
			items:[{
				xtype:'image',
				src:JShell.System.Path.UI + '/css/images/system/oa_barcord.jpg'
			}]
		})];
		
		return items;
	},
	/**创建第三列*/
	createColumn_3:function(){
		var me = this;
		var items = [me.createCard({
			title:'微信公众号',
			height:200,
			items:[{
				xtype:'image',
				src:JShell.System.Path.UI + '/css/images/system/oa_barcord.jpg'
			}]
		})];
		
		
		return items;
	},
	/**创建小卡片*/
	createCard:function(config){
		var me = this;
		var item = {
			collapsible:true
		};
		
		if(config){
			item = Ext.apply(item,config);
		}
		
		return item;
	}
});
	