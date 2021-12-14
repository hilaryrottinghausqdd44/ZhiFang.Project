/**
 * 基础应用
 * @author liangyl
 * @version 2017-06-05
 */
Ext.define('Shell.class.wfm.task.new.basic.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '基础应用',

	width: 600,
	height: 400,
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**是否存在错误*/
	isError: false,
	/**任务类型树节点*/
	treeIdStr: "",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Tree.on({
			select: function(RowModel, record) {
				me.getTreeIdStr(record);
				me.selectOneRow(record);
			}
		});
		me.Grid.on({
			change:function(com,value){		
				var records = me.Tree.getSelectionModel().getSelection();
				if(value){//本节点
					var typeId=records[0].get('tid');
					me.Grid.loadTaskTypeData(typeId);
				}else{//本节点和所有子节点
					var record = records[0];
					me.getTreeIdStr(record);
					me.selectOneRow(record);
				}
			}
		});
	},

	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Tree = Ext.create('Shell.class.wfm.task.new.basic.Tree', {
			region: 'west',
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		me.Grid = Ext.create('Shell.class.wfm.task.new.oneaudit.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Tree, me.Grid];
	},
	
	
	getTreeIdStr: function(record) {
		var me = this;

        var checkBDictTreeId = me.Grid.getComponent('buttonsToolbar').getComponent('checkBDictTreeId');
	    if(checkBDictTreeId.getValue()){//本节点
			me.treeIdStr="";
			var rec = me.Tree.getSelectionModel().getSelection();
			var typeId=rec[0].get('tid');
			me.treeIdStr=typeId;
		}else{
			
			JShell.Action.delay(function() {
				me.treeIdStr = "";
				if(!record) return;
				var root = me.Tree.getRootNode();
				var node = root.findChild('tid', record.data.tid, true);
				if(node) {
					me.treeIdStr = node.data.tid + ",";
					me.getChildIdStr(node);
					if(me.treeIdStr && me.treeIdStr.length > 0)
						me.treeIdStr = me.treeIdStr.substring(0, me.treeIdStr.length - 1);
				}
			},300);
		}
	},
	//递归子节点    
	getChildIdStr: function(node) {
		var me = this;
		node.eachChild(function(child) {
			if(me.treeIdStr.indexOf(child.data.tid) == -1)
				me.treeIdStr += child.data.tid + ",";
			me.getChildIdStr(child);
		});
	},
	/**选一行处理*/
	selectOneRow: function(record) {
		var me = this;
		JShell.Action.delay(function() {
            me.Grid.loadTaskTypeData(me.treeIdStr);
        },300);
	}
});