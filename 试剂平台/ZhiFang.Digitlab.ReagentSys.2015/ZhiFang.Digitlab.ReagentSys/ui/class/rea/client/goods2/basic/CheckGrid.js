/**
 * 产品选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.goods2.basic.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'产品选择列表',
    width:450,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}

		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'产品编号/名称',
			fields:['reagoods.GoodsNo','reagoods.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'ReaGoods_CName',text: '产品名称',flex: 1,minWidth:100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsNo',text: '产品编号',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',text: '单位',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',text: '规格',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		
		return columns;
	}
	
});