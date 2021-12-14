/**
 * 基础树
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.tree.CheckPanel', {
	extend: 'Shell.ux.tree.Panel',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			afteritemcollapse: function(node, opt) {
				me.setNode(node, node.isHalfSelected);
			},
			afteritemexpand: function(node) {
				me.setNode(node, node.isHalfSelected);
				me.setChildStyle(node);
			},
			checkchange: function(node, checked) {
				me.treeStoreSelectedSon(node, checked);
				me.treeStoreSelectedFather(node, checked);
			}
		});
	},
	//递归选子节点    
	treeStoreSelectedSon: function(node, checked) {
		var me = this;
		//node.expand();
		node.isHalfSelected = false;
		//循环下一级的所有子节点 
		node.eachChild(function(child) {
			//这里这么写是因为后台有些节点的checked没赋值，其在web上不显示复选框，这里就过滤掉对它们
			if (null != child.get('checked')); {
				child.set('checked', checked); //选中 
				me.treeStoreSelectedSon(child, checked); //递归选中子节点
			}
		});
	},

	//递归选父节点    
	treeStoreSelectedFather: function(node, checked) {
		var me = this;
		var parent = node.parentNode; //获取父节点 
		var flag = false;
		var hasUnCheckedChild = false;
		var isHalfSelected = false;
		if (null != parent) { //是否有子节点            
			parent.eachChild(function(child) { //循环下一级的所有子节点 

				if (child.get('checked') == true) {
					flag = true;
					if (child.isHalfSelected) {
						isHalfSelected = true;
					}
				} else if (child.get('checked') == false) {
					hasUnCheckedChild = true;
				}
			});

			parent.set('checked', flag);
			if ((flag && hasUnCheckedChild) || isHalfSelected) {
				parent.isHalfSelected = true;
				me.setNode(parent, true);
			} else {
				parent.isHalfSelected = false;
				me.setNode(parent, false);
			}
			me.treeStoreSelectedFather(parent, flag);
		}
	},

	setNode: function(node, value) {
		var me = this;
		var checkbox = me.getCheckbox(node);
		//checkbox.disabled=value;
		//半选中状态
		if (node.isHalfSelected != null) {
			if (value == true) {
				checkbox.className = checkbox.className.replace('Diy-mask', '') + ' Diy-mask';
			} else { //取消半选中
				checkbox.className = checkbox.className.replace('Diy-mask', '');
			}
		}
	},

	getCheckbox: function(node) {
		var td = this.getView().getNode(node).firstChild.firstChild;
		var checkbox = td.getElementsByTagName('input')[0];
		return checkbox;
	},

	setChildStyle: function(node) {
		var me = this;
		if (node.isExpanded()) {
			node.eachChild(function(child) { //循环下一级的所有子节点                      
				if (child.isHalfSelected != null) {
					var checkbox = me.getCheckbox(child);
					//半选中状态
					if (child.isHalfSelected == true) {
						checkbox.className = checkbox.className.replace(' Diy-mask', '') + ' Diy-mask';
					} else { //取消半选中
						checkbox.className = checkbox.className.replace(' Diy-mask', '');
					}
					me.setChildStyle(child);
				}
			});
		}
	},
	
	/**取消选中*/
	onCancelCheck:function(){
		var me = this,
			root = me.getRootNode();
			
		me.setNodefalse(root);
	},
	setNodefalse:function(node){
		var me = this,
			childs = node.childNodes,
			len = childs.length;
			
		me.doNodeFalse(node);
		
		for (var i = 0; i < len; i++) {
			me.doNodeFalse(childs[i]);
            if (childs[i].data.leaf == false) {
                me.setNodefalse(childs[i]);
            }
        }
	},
	doNodeFalse:function(node){
		if(node.data.checked){
			JShell.Msg.log(node.data.text + ':' + node.data.checked);
			node.data.checked = false;
	        node.updateInfo({checked:false});
		}
	},
	setNodeTrue:function(node){
		var me = this;
			
		node.data.checked = true;
        node.updateInfo({checked:true});
	},
	/**点击刷新按钮*/
	onRefreshClick: function() {
		var me = this;
		me.callParent(arguments);
		me.onCancelCheck();
	},
	showError:function(msg){
		var me = this;
		me.errorMk = me.errorMk || new Ext.LoadMask(me.getEl(), {
			msg: msg || '错误',
			removeMask: true
		});
		me.errorMk.show(); //显示遮罩层
	}
});