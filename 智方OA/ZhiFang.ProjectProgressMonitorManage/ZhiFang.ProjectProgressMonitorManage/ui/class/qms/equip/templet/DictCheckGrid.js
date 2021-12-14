/**
 * 选择模板类型树
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.DictCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'字典选择列表',
    width:270,
    height:300,
    
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true',
    defaultOrderBy:[{property:'PDict_DispOrder',direction:'ASC'}],
    /**是否单选*/
	checkOne:true,
	/**字典类型编号*/
	dictTypeCode:'',
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pdict.IsUse=1';
		
		if(me.dictTypeCode){
			me.defaultWhere += " and pdict.PDictType.DictTypeCode='" + me.dictTypeCode + "'";
		}
		
		//查询框信息
		me.searchInfo = {width:230,emptyText:'名称',isLike:true,
			fields:['pdict.CName']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'名称',dataIndex:'PDict_CName',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'备注',dataIndex:'PDict_Memo',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'PDict_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'PDict_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	}
});