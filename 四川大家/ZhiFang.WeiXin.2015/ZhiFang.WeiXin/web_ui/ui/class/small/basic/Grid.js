/**
 * 功能块-基础列表
 * @author Jcall
 * @version 2016-09-18
 */
Ext.define('Shell.class.small.basic.Grid',{
    extend: 'Shell.ux.grid.Panel',
    
    title:'功能块-基础列表',
    /**获取数据服务路径*/
	selectUrl:'',
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 5,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[5,5],[10,10],[20,20],[50,50]
	],
  	/**默认加载数据*/
	defaultLoad:false,
	/**默认选中数据*/
	autoSelect: false,
	/**默认排序字段*/
	//defaultOrderBy: [{ property: 'PTask_DataAddTime', direction: 'ASC' }],
	
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 40,
	
	/**是否默认触发boxready事件*/
	defaultFireBoxready:true,
	
  	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.on({
			boxready:function(){
				if(me.defaultFireBoxready){
					me.onSearch();
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	}
});