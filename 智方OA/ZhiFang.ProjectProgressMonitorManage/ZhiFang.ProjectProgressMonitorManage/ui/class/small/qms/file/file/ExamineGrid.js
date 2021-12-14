/**
 * 文档审核
 * @author Jcall
 * @version 2017-02-12
 */
Ext.define('Shell.class.small.qms.file.file.ExamineGrid',{
    extend: 'Shell.class.small.qms.file.file.ActionGrid',
    
	/**处理环节字段*/
	ActionField:'CheckerId',
	/**状态值*/
	ActionStatusValue:'2,10',
	
    /**默认树节点ID*/
	IDS:'',
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'标题',dataIndex:'FFile_Title',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'FFile_Status',width:65,
			sortable:false,menuDisabled:true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.FFileStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value];
				meta.tdAttr = 'data-qtip="<b style=\'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value] + '\'>' + v + '</b>"';
				return v;
			}
		},{
			text:'起草人',dataIndex:'FFile_DrafterCName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'起草时间',dataIndex:'FFile_DrafterDateTime',
			width:130,isDate:true,hasTime:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'FFile_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});