/**
 * 字典类型列表-开发商功能
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.type.system.Grid',{
    extend: 'Shell.class.wfm.dict.type.Grid',
    
    title:'字典类型列表-开发商功能',
    
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'类型名称',dataIndex:'BDictType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'类型编码',dataIndex:'BDictType_DictTypeCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'开发商代码',dataIndex:'BDictType_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BDictType_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'创建时间',dataIndex:'BDictType_DataAddTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'类型描述',dataIndex:'BDictType_Memo',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'次序',dataIndex:'BDictType_DispOrder',width:60,
			defaultRenderer:true,align:'center',type:'int'
		},{
			text:'主键ID',dataIndex:'BDictType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});