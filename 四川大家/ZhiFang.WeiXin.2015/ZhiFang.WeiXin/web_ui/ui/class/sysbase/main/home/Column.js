/**
 * 首页Home-列布局模式
 * @author Jcall
 * @version 2016-09-22
 */
Ext.define('Shell.class.sysbase.main.home.Column',{
    extend:'Shell.class.sysbase.main.home.Basic',
    
    layout:'column',
    /**列数*/
    columnCount:2,
    
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 初始化组件类数据*/
	initClassListData:function(callback){
		var me = this,
			items = [];
			
		var url = JShell.System.Path.UI + '/config/Home_' + JShell.System.CODE + '.json?t=' + new Date().getTime();;
		JShell.Server.get(url,function(data){
			if(data.success && data.value){
				me.CLASS_LIST = data.value.list || [];
			}else{
				me.CLASS_LIST = [];
			}
			callback();
		});
	},
	/**@overwrite 初始化所有组件*/
	initAllItems:function(){
		var me = this,
			columnWidth = 1/me.columnCount,
			list = me.CLASS_LIST,
			len = list.length,
			items = [];
			
		me.PANEL_ITEMS = [];
		
		for(var i=0;i<me.columnCount;i++){
			me['COLUMN_' + (i+1)] = {
				columnWidth:columnWidth,
				border:false,
				items:[]
			};
			items.push(me['COLUMN_' + (i+1)]);
		}
			
		for(var i=0;i<len;i++){
			var data = list[i];
			var columnPanel = me['COLUMN_' + data.classConfig.columnIndex];
			if(columnPanel){
				columnPanel.items.push(me.createPanel(i,data));
			}
		}
		
		//删除加载元素
		me.removeLoadingHtml(me.getId());
		me.removeAll();
		me.add(items);
	},
	/**展开所有的卡片*/
	onExpandAllCards:function(tool){
		var me = this,
			columns = me.items.items,
			items = [];
			
		for(var i in columns){
			items = items.concat(columns[i].items.items);
		}
		
		for(var i in items){
			items[i].expand();
		}
	},
	/**收缩所有的卡片*/
	onCollapseAllCards:function(tool){
		var me = this,
			columns = me.items.items,
			items = [];
			
		for(var i in columns){
			items = items.concat(columns[i].items.items);
		}
		
		for(var i in items){
			items[i].collapse();
		}
	}
});