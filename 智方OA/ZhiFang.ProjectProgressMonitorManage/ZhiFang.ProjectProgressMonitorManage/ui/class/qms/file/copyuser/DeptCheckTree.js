/**
 * 选择选择部门树
 * @author liangyl	
 * @version 2020-02-03
 */
Ext.define('Shell.class.qms.file.copyuser.DeptCheckTree',{
    extend:'Shell.ux.tree.CheckPanel',
	
	title:'选择部门',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp',
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
	/**是否显示根节点*/
	rootVisible:false,
	/**默认选中节点*/
	autoSelectIds:null,
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);		
		var IsLeaf = me.getComponent('topToolbar').getComponent('IsLeaf');
		me.store.on({
			load:function(){
				me.fireEvent('loadtree', me);
				me.autoSelectIds = me.selectId;
				var arr =[];
				if(me.autoSelectIds){
					//默认选择本节点
					me.changeChecked(me.autoSelectIds);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('loadtree','accept');
		me.callParent(arguments);
	},
	
	createDockedItems: function() {
		var me = this;

		var items = [{
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		},'-', {
			xtype: "checkbox",boxLabel: "本节点",inputValue: "true",
			uncheckedValue: "false",checked: true,margin: '0 0 0 5',
			labelWidth: 5,itemId: "IsLeaf",name: "IsLeaf",
			fieldLabel: ""
		},'-',{
			text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',margin: '0 0 0 5',
			handler:function(){
				me.onCancelCheck();
				me.fireEvent('accept',me,null);
				
			}
		},'->',{text:'确定',tooltip:'确定',iconCls:'button-accept',
		     handler:function(){
		     	var nodes = me.getChecked(); 
		     	var records =[];
		     	
		     	for(var i=0;i<nodes.length;i++){
		     		if(!nodes[i].isHalfSelected && nodes[i].raw.tid!=0){//真正选择的节点并且不能是节点为0的根节点(所有机构)
		     			records.push(nodes[i]);
		     		}
		     	}
		     	me.fireEvent('accept',me,records);
				me.onSaveClick();
		}}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	},

	changeData:function(data){
		var me = this;
    	var changeNode = function(node){
    		//图片地址处理
    		if(node['icon'] && node['icon'] != ''){
    			node['icon'] = JShell.System.Path.getModuleIconPathBySize(16) + "/" + node['icon'];
    		}
    		node.DataTimeStamp = '0,0,0,0,0,0,0,0';
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
	},
	
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this;
		
		var fields = [
			{name:'Id',type:'auto'},//ID
			{name:'DataTimeStamp',type:'auto'},//时间戳
			{name:'checked',type:'bool'},
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'}//默认ID号
		];
		return fields;
	},
	/**更改勾选*/
	changeChecked:function(ids){
		var me = this;
			arr = ids ? ids.split(',') : [],
			len = arr.length;
		
		me.autoSelectIds = ids;
		
		//先将所有节点设置为不选中状态
        me.onCancelCheck();
        //收缩
        me.onMinusClick();
        
		for(var i=0;i<len;i++){
			var node = me.selectNode(arr[i]);
			me.setNodeTrue(node);
			me.setChildStyle(node);
		}
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			nodes = me.getChecked();
		me.fireEvent('save',me,nodes);
	},
	 //递归选子节点    （重写,为判断是否是本节点)
	treeStoreSelectedSon: function(node, checked) {
		var me = this;
		var IsLeaf = me.getComponent('topToolbar').getComponent('IsLeaf');
		//本节点，勾选多少个节点就选多少个，不用递归
		if(IsLeaf.getValue())return;
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
	//递归选父节点   （重写,为判断是否是本节点)
	treeStoreSelectedFather: function(node, checked) {
		var me = this;
		var IsLeaf = me.getComponent('topToolbar').getComponent('IsLeaf');
		//本节点，选什么节点就勾什么节点
		if(IsLeaf.getValue())return;
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
		//半选中状态
		if (node.isHalfSelected != null && checkbox) {
			if (value == true) {
				checkbox.className = checkbox.className.replace('Diy-mask', '') + ' Diy-mask';
			} else { //取消半选中
				checkbox.className = checkbox.className.replace('Diy-mask', '');
			}
		}
	},
	getCheckbox: function(node) {
		if(!this.getView().getNode(node))return;
		var td = this.getView().getNode(node).firstChild.firstChild;
		var checkbox = td.getElementsByTagName('input')[0];
		return checkbox;
	}
});