/**
 * 组织机构树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.org.Tree',{
    extend:'Shell.ux.tree.Panel',
	
	title:'组织机构',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_UseCode',
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
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			load:function(){
				me.getSelectionModel().select(me.root.tid);
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.topToolbar = me.topToolbar || ['-', {
			xtype: 'trigger',
			itemId: 'searchText',
			emptyText: '快速检索',
			width: 75,
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
		},'-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		me.callParent(arguments);
	},
	/**
	 * 根据显示文字过滤
	 * @private
	 * @param {} text
	 */
	filterByText: function(text) {
		this.filterBy(text, ['text']);
	},
	/**
	 * 根据值和字段过滤
	 * @private
	 * @param {} text 过滤的值
	 * @param {} byArr 过滤的字段数组
	 */
	filterBy: function(text, byArr) {
		this.clearFilter();
		if(!text) return;
		
		var view = this.getView(),
			me = this,
			nodesAndParents = [];

		this.getRootNode().cascadeBy(function(tree, view) {
			var currNode = this;
			if(!currNode) return;
			
			var isRight = false;
			for(var i in byArr){
				var data = currNode.data;
				var arr = byArr[i].split('.');
				for(var j in arr){
					data = data[arr[j]];
				}
				
				if(data){
					//节点的匹配判断逻辑-包含输入的文字，不区分大小写，可修改
					if(data.toString().toLowerCase().indexOf(text.toLowerCase()) > -1) {
						me.expandPath(currNode.getPath());
						while(currNode.parentNode) {
							nodesAndParents.push(currNode.id);
							currNode = currNode.parentNode;
						}
					}
				}
			}
		}, null, [me, view]);
		
		me.onPlusClick();//全部展开
		
		this.getRootNode().cascadeBy(function(tree, view) {
			var uiNode = view.getNodeByRecord(this);
			if(uiNode && !Ext.Array.contains(nodesAndParents, this.id)) {
				Ext.get(uiNode).setDisplayed('none');
			}
		}, null, [me, view]);
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
	
	createDockedItems:function(){
		var me = this;
		var dockedItems = me.callParent(arguments);
		
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		
		return dockedItems;
	},
	changeData:function(data){
		var me = this;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node.value && node.value.UseCode){
    			node.text = '[' + node.value.UseCode + '] ' + node.text;
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var children = data[me.defaultRootProperty];
    	changeChildren(children);
    	
    	return data;
	}
});
	