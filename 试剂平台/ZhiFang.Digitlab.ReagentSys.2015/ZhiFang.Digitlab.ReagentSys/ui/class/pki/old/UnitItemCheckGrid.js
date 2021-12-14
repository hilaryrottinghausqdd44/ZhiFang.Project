/**
 * 经销商/送检单位项目选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.UnitItemCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'经销商/送检单位项目选择列表',
    width:290,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchDUnitItemByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = me.searchInfo || {width:165,emptyText:'项目名称',isLike:true,fields:['dunititem.BTestItem.CName']};
		
		//数据列
		me.columns = [{
			dataIndex:'DUnitItem_BTestItem_CName',text:'项目名称',width:150,defaultRenderer:true
		},{
			dataIndex:'DUnitItem_UnitType',text:'单位类型',width:70,
			renderer:function(value,meta){
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
		        if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        return v;
		    }
		},{
			dataIndex:'DUnitItem_Id',text:'主键ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'DUnitItem_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		},{
			dataIndex:'DUnitItem_BTestItem_Id',text:'项目ID',hidden:true,hideable:false
		},{
			dataIndex:'DUnitItem_BTestItem_DataTimeStamp',text:'项目时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});