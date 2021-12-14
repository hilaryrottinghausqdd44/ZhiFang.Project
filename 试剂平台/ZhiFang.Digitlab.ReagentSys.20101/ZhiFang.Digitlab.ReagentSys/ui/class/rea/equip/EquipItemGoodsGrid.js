/**
 * 仪器项目试剂历史列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.EquipItemGoodsGrid',{
    extend:'Shell.ux.grid.Panel',
    title:'仪器项目试剂历史列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:JShell.System.Path.UI + '/server/rea/getEquipItemGoodsList.js?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	
	/**默认加载数据*/
	defaultLoad:false,
	/**排序字段*/
	//defaultOrderBy:[{property:'TestEquipItem_DispOrder',direction:'ASC'}],
	
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
			dataIndex: 'TestEquipItemGoods_TestEquipItem_TestEquip_CName',
			text: '仪器名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquipItemGoods_TestEquipItem_Item_CName',
			text: '项目名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquipItemGoods_Goods_CName',
			text: '试剂名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquipItemGoods_StartDate',
			text: '开始时间',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquipItemGoods_EndDate',
			text: '截止时间',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'TestEquipItemGoods_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'TestEquipItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});