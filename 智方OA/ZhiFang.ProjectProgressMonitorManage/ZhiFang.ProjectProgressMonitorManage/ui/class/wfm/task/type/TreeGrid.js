/**
 * 任务类型树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.type.TreeGrid',{
    extend:'Shell.class.sysbase.dicttree.Tree',
    requires: ['Ext.ux.CheckColumn'],
    title:'任务类型树',
    
    width:600,
    height:400,
    
    /**字典树IDS*/
    IDS:'',
    /**获取树的最大层级数*/
	LEVEL:3,
    /**默认加载数据*/
	defaultLoad: true,
	rootVisible: false,
	
	/**原始数据*/
	RawData:null,
	/**默认任务类型数据*/
	CHECKED_TASK_TYPE_LIST:[],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			load: function() {
				me.onChangeData(me.CHECKED_TASK_TYPE_LIST);
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		me.topToolbar = me.topToolbar || [{
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: me.searchWidth,
			triggerCls: 'x-form-clear-trigger',
			enableKeyEvents: true,
			onTriggerClick: function() {
				this.setValue('');
				me.clearFilter();
			},
			listeners: {
				keyup: {
					fn: function(field, e) {
						var bo = Ext.EventObject.ESC == e.getKey();
						bo ? field.onTriggerClick() : me.filterByText(this.getRawValue());
					}
				}
			}
		}];
		
		me.columns = [{
			xtype:'treecolumn',text:'任务类型',dataIndex:'text',width:200,
			sortable:false,menuDisabled:true
		},{
			xtype:'checkcolumn',text:'二审',dataIndex:'TwoAudit',width:40,
			sortable:false,menuDisabled:true
		},{
			xtype:'checkcolumn',text:'分配',dataIndex:'Publish',width:40,
			sortable:false,menuDisabled:true
		}];
		
		me.callParent(arguments);
	},
	changeData: function(data) {
		data = this.callParent(arguments);
		
		if(data.Tree && data.Tree.length == 1){
			data.Tree = data.Tree[0].Tree || [];
		}
		
		this.RawData = data;
		
		return data;
	},
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		
		dockedItems.push({
			xtype: 'toolbar',
			dock: 'bottom',
			itemId: 'bottomToolbar',
			items: [{
				iconCls: 'button-save',
				itemId: 'save',
				text:'保存',
				tooltip: '保存',
				handler: function() {
					me.onSaveClick();
				}
			}]
		});
		
		return dockedItems;
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			nodes = me.getCheckedDatas();
			
		me.fireEvent('save',me,nodes);
	},
	/**获取勾选的数据*/
	getCheckedDatas:function(){
		var me = this,
			root = me.getRootNode(),
			nodes = [];
			
		var records = me.store.getModifiedRecords();
		var len = records.length;
		for(var i=0;i<len;i++){
			if(records[i].data.tid){
				nodes.push(records[i].data);
			}
		}
		
		return nodes;
	},
	/**重置数据*/
	onResetData:function(){
		var me = this,
			data = Ext.clone(me.RawData);
			
		me.store.setRootNode(data);
	},
	/**数据变换*/
	onChangeData:function(list){
		var me = this,
			arr = list || [],
			len = arr.length;
			
		me.CHECKED_TASK_TYPE_LIST = arr;
			
		me.onResetData();
		for(var i=0;i<len;i++){
			var obj = arr[i];
			var node = me.selectNode(obj.TaskTypeId);
			node.data.TwoAudit = obj.TwoAudit;
			node.data.Publish = obj.Publish;
		    node.updateInfo({TwoAudit:obj.TwoAudit,L2:obj.Publish});
		}
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;

		var fields = me.callParent(arguments);
		fields.push({
			name: 'TwoAudit',
			type: 'bool'
		},{
			name: 'Publish',
			type: 'bool'
		});

		return fields;
	}
});