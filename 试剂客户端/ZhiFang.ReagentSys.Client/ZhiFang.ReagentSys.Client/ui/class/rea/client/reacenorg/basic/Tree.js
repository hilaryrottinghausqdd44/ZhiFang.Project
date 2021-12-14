/**
 * 所有机构
 * @author liangyl
 * @version 2018-01-31
 */
Ext.define('Shell.class.rea.client.reacenorg.basic.Tree', {
	extend: 'Shell.class.rea.client.reacenorg.Tree',
	
	title:'设置上级机构',
	
	/**是否带清除按钮*/
	hasClearButton: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**机构类型*/
	OrgType:'0',
	/**是否显示根节点*/
	rootVisible:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				me.fireEvent('accept',me,record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//列表字段
		me.columns = me.createGridColumns();
		//获取树列表
		me.callParent(arguments);
	},
    //=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '机构名称',xtype:'treecolumn',
			dataIndex: 'text',flex:1,editor:{},sortable: false
		}, {
			dataIndex: 'OrgNo',width: 80,text: '机构编号',
			type:'int',defaultRenderer: true
		},{
			dataIndex: 'Id',text: '主键ID',hidden: true,
			hideable: false,isKey: true
		}];
		return columns;
	},
	/**
	 * 树字段对象
	 * @type 
	 */
    treeFields:{
    	/**
		 * 基础字段数组
		 * @type 
		 */
		defaultFields:[
			{name:'text',type:'auto'},//默认的现实字段
			{name:'expanded',type:'auto'},//是否默认展开
			{name:'leaf',type:'auto'},//是否叶子节点
			{name:'icon',type:'auto'},//图标
			{name:'url',type:'auto'},//地址
			{name:'tid',type:'auto'},//默认ID号
			{name:'value',type:'auto'}
		],
		/**
		 * 模块对象字段数组
		 * @type 
		 */
		moduleFields:[
			{name:'OrgNo',type:'auto'},
			{name:'Id',type:'auto'}
		]
    },
    	/**获取数据字段*/
	getStoreFields: function() {
		var me = this;
		var treeFields = me.treeFields;
		var defaultFields = treeFields.defaultFields;
		var moduleFields = treeFields.moduleFields;
		var fields = defaultFields.concat(moduleFields);
		return fields;
	},
      /**
	 * 数据适配
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeStoreData: function(response){
		var me = this;
    	var data = Ext.JSON.decode(response.responseText);
        if(data.ResultDataValue){
        	var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    	    data[me.defaultRootProperty] = ResultDataValue.Tree;
	    	var changeNode = function(node){
	    		var value = node['value'];
	    		for(var i in value){
	    			node[i] = value[i];
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
	    	
	    	response.responseText = Ext.JSON.encode(data);
	        //已获取到数据
	        me.hasResponseData = true;
         }
    	return response;
    }
});