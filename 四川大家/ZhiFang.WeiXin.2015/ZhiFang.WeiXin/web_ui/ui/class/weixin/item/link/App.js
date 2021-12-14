/**
 * 检测项目产品分类树关系
 * @author liangyl	
 * @version 2017-1-18
 */
Ext.define('Shell.class.weixin.item.link.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'检测项目产品分类树关系维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.AreaGrid.on({
			itemclick:function(v, record){
				me.Tree.getSelectionModel().deselect(0);
				me.Grid.clearData();
				JShell.Action.delay(function(){
					//表单新增时设置默认值（区域）
				    me.Grid.AreaID=record.get('ClientEleArea_ClientNo');
				    me.Grid.AreaNO=record.get('ClientEleArea_Id');
				    me.Grid.AreaName=record.get('ClientEleArea_AreaCName');
					me.Tree.AreaID=record.get('ClientEleArea_Id');
					me.Tree.load();
				},null,500);
			},
			select:function(RowModel, record){
			    me.Tree.getSelectionModel().deselect(0);
				me.Grid.clearData();
				JShell.Action.delay(function(){
					//表单新增时设置默认值（区域）
				    me.Grid.AreaID=record.get('ClientEleArea_ClientNo');
				    me.Grid.AreaNO=record.get('ClientEleArea_Id');
				    me.Grid.AreaName=record.get('ClientEleArea_AreaCName');
                    me.Tree.AreaID=record.get('ClientEleArea_Id');
					me.Tree.load();
				},null,500);
			}
		});
		me.Tree.on({
			itemclick:function(v, record){
				var records = me.AreaGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.Grid.ItemProductClassTreeID=record.get('tid');
				JShell.Action.delay(function(){
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
			    var records = me.AreaGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				me.Grid.ItemProductClassTreeID=record.get('tid');
				JShell.Action.delay(function(){
					me.Grid.onSearch();
				},null,500);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
        //区域列表
		me.AreaGrid = Ext.create('Shell.class.weixin.hospital.area.Grid', {
			region: 'west',
			header: false,
			itemId: 'AreaGrid',
			width:310,
			split: true,
			collapsible: true
		});
		me.Tree = Ext.create('Shell.class.weixin.item.type.Tree', {
			region: 'west',
			header: false,
			itemId: 'Tree',
			/**默认加载数据*/
	        defaultLoad:false,
	        selectId: 0,
			width:228,
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.weixin.item.link.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		
		return [me.AreaGrid,me.Tree,me.Grid];
	}
});