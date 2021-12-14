/**
 * 医院选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.weixin.hospital.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'医院选择列表',
    width:370,
    height:300,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bhospital.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
			fields:['bhospital.Name']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'名称',dataIndex:'BHospital_Name',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医院编码',dataIndex:'BHospital_HospitalCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BHospital_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BHospital_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BHospital_AreaID',hidden:true,hideable:false
		}]
		
		return columns;
	}
});