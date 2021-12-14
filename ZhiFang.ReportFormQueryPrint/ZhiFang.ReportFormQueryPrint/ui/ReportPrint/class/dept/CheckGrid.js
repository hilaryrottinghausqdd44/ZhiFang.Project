/**
 * 部门选择列表
 * @author Jcall
 * @version 2018-08-08
 */
Ext.define('Shell.ReportPrint.class.dept.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'员工选择列表',
    
	/**获取数据服务路径*/
	selectUrl:'/ServiceWCF/DictionaryService.svc/GetDeptListPaging',
	
	pagingtoolbar:'simple',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		//服务路径
		me.selectUrl += '?fields=' + me.getStoreFields().join(',');
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		//数据集属性
	    me.storeConfig = me.storeConfig || {};
	    me.storeConfig.proxy = me.storeConfig.proxy || {
	        type: 'ajax',
	        url: '',
	        reader: { type: 'json', totalProperty: 'total', root: 'rows' },
	        extractResponseData: function (response) {
	            var result = Ext.JSON.decode(response.responseText);

	            if (result.success) {
	                var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	                result.total = ResultDataValue.total;
	                result.rows = ResultDataValue.rows;
	            } else {
	                result.total = 0;
	                result.rows = [];
	                Shell.util.Msg.showError(result.ErrorInfo);
	            }

	            response.responseText = Ext.JSON.encode(result);
	            return response;
	        }
	    };
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			xtype:'rownumberer',text:'序号',width:40,align:'center'
		},{
			text:'名称',dataIndex:'CName',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'DeptNo',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});