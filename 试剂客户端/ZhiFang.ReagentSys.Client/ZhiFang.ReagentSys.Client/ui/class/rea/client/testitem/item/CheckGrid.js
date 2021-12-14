/**
 * 项目选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.testitem.item.CheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'项目选择列表',
    width:850,
    height:400,
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestItemByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:300,isLike:true,itemId: 'Search',
			emptyText:'项目名称/英文名称/LisD代码/代码',
			fields: ['reatestitem.CName', 'reatestitem.EName', 'reatestitem.LisCode', 'reatestitem.ShortCode']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestItem_CName',text: '项目名称',
			sortable: true,width: 200,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_EName',text: '英文名称',
			sortable: true,width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Price',text: '价格',
			sortable: true,width: 80,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_LisCode',text: 'Lis代码',
			sortable: true,width: 80,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ShortCode',text: '代码',
			sortable: true,width: 80,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX1',text: 'ZX1',
			width: 100,editor:{},hidden:true,defaultRenderer: true
		},{
			dataIndex: 'ReaTestItem_ZX2',editor:{},text: 'ZX2',
			sortable: true,width: 80,hidden:true,defaultRenderer: true
		},{
			dataIndex: 'ReaTestItem_ZX3',editor:{},text: 'ZX3',
			sortable: true,width: 80,hidden:true,defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true,
			defaultRenderer: true
		}];
		
		return columns;
	}
});