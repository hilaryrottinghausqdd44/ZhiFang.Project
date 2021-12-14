/**
 * 仪器列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.EquipShowGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'仪器列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:JShell.System.Path.UI + '/server/rea/getEquipList.js?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'TestEquip_DispOrder',direction:'ASC'}],
	
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    },
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'TestEquip_Dept_CName',
			text: '部门名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_CName',
			text: '中文名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_EName',
			text: '英文名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_ShortCode',
			text: '代码',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'TestEquip_DispOrder',
			text: '显示次序',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_LisCode',
			text: 'LIS仪器编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquip_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});