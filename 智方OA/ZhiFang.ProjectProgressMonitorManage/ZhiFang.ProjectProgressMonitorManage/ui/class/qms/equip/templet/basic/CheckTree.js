/**
 * 选择组织机构树
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.basic.CheckTree',{
    extend:'Shell.class.sysbase.org.Tree',
	
	title:'选择组织机构',
	
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有组织机构',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	searchWidth:135,
	width:350,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			itemclick:function(view,record){
				me.fireEvent('accept',me,record);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.topToolbar = me.topToolbar || ['-','->',{
			text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
			handler:function(){me.fireEvent('accept',me,null);}
		}, {
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
		},{
			xtype:'button',
			iconCls:'button-accept',
			text:'确定',
			tooltip:'确定',
			handler:function(){
				me.onAcceptClick();
			}
		}];
		
		me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('accept',me,records[0]);
	},
	/**
	 * 清空过滤
	 * @private
	 */
	clearFilter: function() {
		var view = this.getView();
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode) {
				Ext.get(uiNode).setDisplayed('table-row');
			}
		}, null, [this, view]);
	},
	/**
	 * 根据显示文字过滤
	 * @private
	 * @param {} text
	 */
	filterByText: function(text) {
		this.filterBy(text, 'text');
	},
	/**
	 * 根据值和字段过滤
	 * @private
	 * @param {} text 过滤的值
	 * @param {} by 过滤的字段
	 */
	filterBy: function(text, by) {
		this.clearFilter();
		var view = this.getView(),
			me = this,
			nodesAndParents = [];

		this.getRootNode().cascadeBy(function(tree, view) {
			var currNode = this;
			if(currNode && currNode.data[by]) {
				//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
				if(currNode.data[by].toString().toLowerCase().indexOf(text.toLowerCase()) > -1) {
					me.expandPath(currNode.getPath());
					while(currNode.parentNode) {
						nodesAndParents.push(currNode.id);
						currNode = currNode.parentNode;
					}
				}
			}
		}, null, [me, view]);

		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
				Ext.get(uiNode).setDisplayed('none');
			}
		}, null, [me, view]);
	}
});
	