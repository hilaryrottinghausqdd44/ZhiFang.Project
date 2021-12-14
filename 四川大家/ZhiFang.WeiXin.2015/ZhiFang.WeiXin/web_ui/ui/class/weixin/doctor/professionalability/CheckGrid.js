/**
 * 专业级别选择列表
 * @author liangyl	
 * @version 2017-02-17
 */
Ext.define('Shell.class.weixin.doctor.professionalability.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'专业级别选择列表',
    width:370,
    height:300,
    
    /**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProfessionalAbilityByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'bprofessionalability.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称/简称/代码',isLike:true,
			fields:['bprofessionalability.Name','bprofessionalability.Shortcode','bprofessionalability.SName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'名称',dataIndex:'BProfessionalAbility_Name',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BProfessionalAbility_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'BProfessionalAbility_Shortcode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BProfessionalAbility_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	}
});