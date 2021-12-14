/**
 * 文档发布
 * @author Jcall
 * @version 2017-02-12
 */
Ext.define('Shell.class.small.qms.file.file.ReleaseGrid',{
    extend: 'Shell.class.small.qms.file.file.ActionGrid',
    
	/**处理环节字段*/
	ActionField:'PublisherId',
	/**状态值*/
	ActionStatusValue:'4',
	
    /**默认树节点ID*/
	IDS:'',
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'标题',dataIndex:'FFile_Title',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'审批人',dataIndex:'FFile_ApprovalName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'审批时间',dataIndex:'FFile_ApprovalDateTime',
			width:130,isDate:true,hasTime:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'FFile_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});