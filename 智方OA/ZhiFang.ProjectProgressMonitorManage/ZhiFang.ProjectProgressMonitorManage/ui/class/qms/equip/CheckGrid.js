/**
 * 仪器选择列表
 * @author liangyl
 * @version 2016-08-16
 */
Ext.define('Shell.class.qms.equip.CheckGrid', {
    extend:'Shell.ux.grid.CheckPanel',
    title:'仪器选择列表',
    width:550,
    height:350,
	/**获取数据服务路径*/
    selectUrl: '/QMSReport.svc/ST_UDTO_SearchEEquipByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'eequip.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:420,emptyText:'名称',isLike:true,
			fields:['eequip.CName']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'仪器名称',dataIndex:'EEquip_CName',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'仪器代码',dataIndex:'EEquip_UseCode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'快捷码',dataIndex:'EEquip_Shortcode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'EEquip_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'EEquip_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
	
	}
});