/**
 * 选择模块树
 * @author liangyl		
 * @version 2018-05-15
 */
Ext.define('Shell.class.rea.center.register.ModuleCheckTree', {
	extend: 'Shell.class.sysbase.role.ModuleCheckTree',
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
	title: '功能模块',
	width: 300,
	height: 500,
	margin: '0 0 1 0',
	initComponent: function() {
		var me = this;

		me.addEvents('save');

		me.topToolbar = me.topToolbar || ['-', {
			xtype: 'label',
			text: '授权功能',
			margin: '0 0 10 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'KeyInfo',
			name: 'KeyInfo'
		}];

		me.callParent(arguments);
	},
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		me.topToolbar = [];
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);

		dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			hidden: true,
			itemId: 'bottomToolbar',
			items: ['->', 'save']
		}));

		return dockedItems;
	},
	changeData: function(data) {
		var me = this;
		me._lastData = data;
		var changeNode = function(node) {
			//图片地址处理
			if(node['icon'] && node['icon'] != '') {
				node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
			}
			var children = node[me.defaultRootProperty];
			if(children) {
				changeChildren(children);
			}
		};

		var changeChildren = function(children) {
			Ext.Array.each(children, changeNode);
		};

		var children = data[me.defaultRootProperty];
		changeChildren(children);

		return data;
	},
	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;

		var fields = [{
				name: 'checked',
				type: 'bool'
			},
			{
				name: 'text',
				type: 'auto'
			}, //默认的现实字段
			{
				name: 'expanded',
				type: 'auto'
			}, //是否默认展开
			{
				name: 'leaf',
				type: 'auto'
			}, //是否叶子节点
			{
				name: 'icon',
				type: 'auto'
			}, //图标
			{
				name: 'url',
				type: 'auto'
			}, //地址
			{
				name: 'tid',
				type: 'auto'
			} //默认ID号
		];

		return fields;
	},
	/**更改勾选*/
	changeChecked:function(ids){
		var me = this;
			arr = ids ? ids.split(',') : [],
			len = arr.length;
		
		me.autoSelectIds = ids;
		
		if(!me._lastData) return;
		
		me.disableControl();//禁用所有的操作功能
		
		var changeNode = function(node){
    		node['checked'] = false;//还原为不选中
    		node['expanded'] = false;//默认收起
    		
    		for(var i=0;i<len;i++){
    			if(node['tid'] == arr[i]){
    				node['checked'] = true;//选中
    				if(node['leaf']==false){
    					node['expanded'] = true;//展开
    				}
    				break;
    			}
    		}
    		
    		var children = node[me.defaultRootProperty];
    		if(children){
    			changeChildren(children);
    		}
    	};
    	
    	var changeChildren = function(children){
    		Ext.Array.each(children,changeNode);
    	};
    	
    	var root = me.root;
    	root.expanded = true;//默认展开
    	root.Tree = me._lastData.Tree;
    	
    	changeChildren(root.Tree);
    	me.setRootNode(root);
    	//me.getRootNode().expand();
    	//me.onMinusClick();
    	me.enableControl();//启用所有的操作功能
	}
});