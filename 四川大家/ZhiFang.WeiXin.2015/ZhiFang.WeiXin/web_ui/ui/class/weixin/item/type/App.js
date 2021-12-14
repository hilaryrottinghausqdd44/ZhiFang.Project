/**
 * 项目分类维护
 * @author Jcall
 * @version 2016-12-28
 */
Ext.define('Shell.class.weixin.item.type.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目分类维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.AreaGrid.on({
			itemclick:function(v, record){
				me.Tree.getSelectionModel().deselect(0);
				me.Grid.clearData();
				JShell.Action.delay(function(){
					//表单新增时设置默认值（区域）
				    me.Grid.AreaID=record.get('ClientEleArea_Id');
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
				    me.Grid.AreaID=record.get('ClientEleArea_Id');
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
				//表单新增时设置默认值（上级节点）
			    me.Grid.ParentID=record.get('tid');
			    me.Grid.ParentName=record.get('text');
             	
				JShell.Action.delay(function(){
					me.Grid.loadParentById(record.get('tid'),records[0].get('ClientEleArea_Id'));
				},null,500);
			},
			select:function(RowModel, record){
				var records = me.AreaGrid.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				//表单新增时设置默认值（上级节点）
			    me.Grid.ParentID=record.get('tid');
			    me.Grid.ParentName=record.get('text');
				JShell.Action.delay(function(){
					me.Grid.loadParentById(record.get('tid'),records[0].get('ClientEleArea_Id'));
				},null,500);
			}
		});
	
		me.Grid.on({
			itemclick:function(v, record){
				var rootNode = me.Tree.getRootNode();
				var n = rootNode.findChild('tid',record.get('OSItemProductClassTree_PItemProductClassTreeID'),true);
				if(n){
					me.Grid.editParentName=n.data.text;
				}else{
					me.Grid.editParentName='所有检测项目产品分类';
				}
			},
			select:function(RowModel, record){
				var rootNode = me.Tree.getRootNode();
				var n = rootNode.findChild('tid',record.get('OSItemProductClassTree_PItemProductClassTreeID'),true);
				if(n){
					me.Grid.editParentName=n.data.text;
				}else{
					me.Grid.editParentName='所有检测项目产品分类';
				}
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
		me.AreaGrid = Ext.create('Shell.class.weixin.item.type.AreaGrid', {
			region: 'west',
			header: false,
			itemId: 'AreaGrid',
			width:320,
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
			width:250,
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.weixin.item.type.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.AreaGrid,me.Tree,me.Grid];
	}
});