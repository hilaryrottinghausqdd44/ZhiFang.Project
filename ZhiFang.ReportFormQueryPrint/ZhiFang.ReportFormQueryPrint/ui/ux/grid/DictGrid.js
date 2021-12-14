/**
 * 字典基础类
 * @author Jcall
 * @version 2014-07-25
 */
Ext.define('Shell.ux.DictGrid',{
	extend:'Ext.grid.Panel',
	alias:'widget.dictgrid',
	
	mixins:['Shell.ux.Ajax'],
	
	/**服务地址*/
	url:'',
	/**固定的数据*/
	data:[],
	/**
	 * 数据列信息
	 * @type 
	 * @example
	 * [{
	 * 	dataIndex:'name',text:'姓名',width:80
	 * },{
	 * 	dataIndex:'age',text:'年龄',width:50
	 * }]
	 */
	columns:[],
	
	/**是否只读*/
	readOnly:false,
	/**分页栏组件*/
	pagingtoolbar:null,
	
	initComponent:function(){
		var me = this;
		me.store = me.createStore();
		me.columns = me.createColumns();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this,
			config = {fields:me.getFields()};
		
		if(url && url != ''){
			 config.proxy = {
			 	type:'ajax',
			 	url:Shell.util.Path.rootPath + config.url,
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:me.responseToList
			 };
		}else{
			config.data = me.data;
		}
		
		return Ext.create('Ext.data.Store',config);
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this,
			columns = me.columns;
			
		return columns;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this,
			top = {
				xtype:'toolbar',
				items:[{text:'刷新'}]
			},
			bottom = {};
			
		if(!me.readOnly){
			
		}
		
		var dockedItems = [top,bottom];
			
		return dockedItems;
	},
	/**
	 * 获取需要的字段
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me = this,
			columns = me.columns,
			length = columns.length,
			fields = [];
			
		for(var i=0;i<length;i++){
			fields.push(columns[i].dataIndex);
		}
		
		return fields.join(',');
	}
});