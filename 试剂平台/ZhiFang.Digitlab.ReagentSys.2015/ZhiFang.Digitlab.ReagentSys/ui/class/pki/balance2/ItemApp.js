/**
 * 月度财务锁定报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.ItemApp',{
    extend:'Ext.panel.Panel',
    title:'月度财务锁定报表',
    
    layout:'fit',
    
    width:1200,
    height:600,
    
    /**默认加载*/
	defaultLoad:true,
    reportType:'3',//报表类型
    ReportGridClass:'Shell.class.pki.balance2.ItemGrid',
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var FilterToolbar = me.getComponent('topToolbar').getComponent('FilterToolbar');
		var ReportGrid = me.getComponent('ReportGrid');
		
		FilterToolbar.on({
			search:function(p,params){
				ReportGrid.params = FilterToolbar.getParams();
				ReportGrid.onSearch();
			}
		});
		
		ReportGrid.on({
			select:function(rowModel,record){
				JShell.Action.delay(function(){
					me.showInfo(record);
				});
			},
			itemclick:function(rowModel,record){
				if(ReportGrid.doActionClick){
					ReportGrid.doActionClick = false;
					return;
				}
				JShell.Action.delay(function(){
					me.showInfo(record);
				});
			}
		});
		
		if(me.defaultLoad){
			JShell.Action.delay(function(){
				FilterToolbar.onFilterSearch();
			});
		}
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.dockedItems = [{
			dock:'top',
			itemId:'topToolbar',
			items:[
				Ext.create('Shell.class.pki.balance2.SearchToolbar',{
					border:false,
					itemId:'FilterToolbar'
				})
			]
		}];
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		items.push(Ext.create(me.ReportGridClass,{
			header:false,itemId:'ReportGrid',
			createGridColumns:me.createGridColumns
		}));
			
		return items;
	},
	createGridColumns:function(){
		return [];
	}
});